// ##################################################

using System.IO;
using System.Runtime.Serialization;

using ILNumerics;

using Lugh.Assets;
using Lugh.Files;
using Lugh.Files.Xml;
using Lugh.Maps.Objects;
using Lugh.Maps.Tiled.Tiles;

// ##################################################

namespace Lugh.Maps.Tiled;

public abstract class BaseTmxMapLoader< TP > : AsynchronousAssetLoader< TiledMap, TP >
    where TP : BaseTmxMapLoader< TP >.Parameters
{
    public class Parameters : AssetLoaderParameters< TiledMap >
    {
        /// <summary>
        /// Generate mipmaps?
        /// </summary>
        public bool GenerateMipMaps { get; set; } = false;

        /// <summary>
        /// The TextureFilter to use for minification.
        /// </summary>
        public TextureFilter TextureMinFilter { get; set; } = TextureFilter.Nearest;

        /// <summary>
        /// The TextureFilter to use for magnification.
        /// </summary>
        public TextureFilter TextureMagFilter { get; set; } = TextureFilter.Nearest;

        /// <summary>
        /// Whether to convert the objects' pixel position and
        /// size to the equivalent in tile space.
        /// </summary>
        public bool ConvertObjectToTileSpace { get; set; } = false;

        /// <summary>
        /// Whether to flip all Y coordinates so that Y positive is up.
        /// All Lugh renderers require flipped Y coordinates, and thus flipY
        /// set to true. This parameter is included for non-rendering related
        /// purposes of TMX files, or custom renderers.
        /// </summary>
        public bool FlipY { get; set; } = true;
    }

    public const uint Flag_Flip_Horizontally = 0x80000000;
    public const uint Flag_Flip_Vertically   = 0x40000000;
    public const uint Flag_Flip_Diagonally   = 0x20000000;
    public const uint Mask_Clear             = 0xE0000000;

    protected XmlReader Xml                      { get; set; } = new XmlReader();
    protected Element   Root                     { get; set; }
    protected bool      ConvertObjectToTileSpace { get; set; }
    protected bool      FlipY                    { get; set; } = true;
    protected int       MapTileWidth             { get; set; }
    protected int       MapTileHeight            { get; set; }
    protected int       MapWidthInPixels         { get; set; }
    protected int       MapHeightInPixels        { get; set; }
    protected TiledMap  Map                      { get; set; }

    public BaseTmxMapLoader( FileHandleResolver resolver ) : base( resolver )
    {
    }

    public Utils.Array< AssetDescriptor > GetDependencies( string fileName, FileHandle tmxFile, TP parameter )
    {
        this.Root = Xml.Parse( tmxFile );

        var textureParameter = new TextureLoader.TextureParameter();

        if ( parameter != null )
        {
            textureParameter.GenMipMaps = parameter.GenerateMipMaps;
            textureParameter.MinFilter  = parameter.TextureMinFilter;
            textureParameter.MagFilter  = parameter.TextureMagFilter;
        }

        return GetDependencyAssetDescriptors( tmxFile, textureParameter );
    }

    protected abstract Utils.Array< AssetDescriptor > GetDependencyAssetDescriptors
        ( FileHandle tmxFile, TextureLoader.TextureParameter textureParameter );

    /// <summary>
    /// Loads the map data, given the XML root element.
    /// </summary>
    protected TiledMap LoadTiledMap( FileHandle tmxFile, TP parameter, IImageResolver imageResolver )
    {
        this.Map = new TiledMap();

        if ( parameter != null )
        {
            this.ConvertObjectToTileSpace = parameter.ConvertObjectToTileSpace;
            this.FlipY                    = parameter.FlipY;
        }
        else
        {
            this.ConvertObjectToTileSpace = false;
            this.FlipY                    = true;
        }

        var mapOrientation     = Root.GetAttribute( "orientation", null );
        var mapWidth           = Root.GetIntAttribute( "width",         0 );
        var mapHeight          = Root.GetIntAttribute( "height",        0 );
        var tileWidth          = Root.GetIntAttribute( "tilewidth",     0 );
        var tileHeight         = Root.GetIntAttribute( "tileheight",    0 );
        var hexSideLength      = Root.GetIntAttribute( "hexsidelength", 0 );
        var staggerAxis        = Root.GetAttribute( "staggeraxis",     null );
        var staggerIndex       = Root.GetAttribute( "staggerindex",    null );
        var mapBackgroundColor = Root.GetAttribute( "backgroundcolor", null );

        var mapProperties = Map.GetProperties();

        if ( mapOrientation != null )
        {
            mapProperties.Put( "orientation", mapOrientation );
        }

        mapProperties.Put( "width",         mapWidth );
        mapProperties.Put( "height",        mapHeight );
        mapProperties.Put( "tilewidth",     tileWidth );
        mapProperties.Put( "tileheight",    tileHeight );
        mapProperties.Put( "hexsidelength", hexSideLength );

        if ( staggerAxis != null )
        {
            mapProperties.Put( "staggeraxis", staggerAxis );
        }

        if ( staggerIndex != null )
        {
            mapProperties.Put( "staggerindex", staggerIndex );
        }

        if ( mapBackgroundColor != null )
        {
            mapProperties.Put( "backgroundcolor", mapBackgroundColor );
        }

        this.MapTileWidth      = tileWidth;
        this.MapTileHeight     = tileHeight;
        this.MapWidthInPixels  = mapWidth * tileWidth;
        this.MapHeightInPixels = mapHeight * tileHeight;

        if ( mapOrientation != null )
        {
            if ( "staggered".Equals( mapOrientation ) )
            {
                if ( mapHeight > 1 )
                {
                    this.MapWidthInPixels  += tileWidth / 2;
                    this.MapHeightInPixels =  MapHeightInPixels / 2 + tileHeight / 2;
                }
            }
        }

        Element properties = Root.GetChildByName( "properties" );

        if ( properties != null )
        {
            loadProperties( Map.GetProperties(), properties );
        }

        Utils.Array< Element > tilesets = Root.GetChildrenByName( "tileset" );

        foreach ( Element element in tilesets )
        {
            LoadTileSet( element, tmxFile, imageResolver );
            Root.RemoveChild( element );
        }

        for ( int i = 0, j = Root.GetChildCount(); i < j; i++ )
        {
            Element element = Root.GetChild( i );
            LoadLayer( Map, Map.GetLayers(), element, tmxFile, imageResolver );
        }

        return Map;
    }

    protected void LoadLayer
        (
            TiledMap       map,
            MapLayers      parentLayers,
            Element        element,
            FileHandle     tmxFile,
            IImageResolver imageResolver
        )
    {
        var name = element.getName();

        if ( name.Equals( "group" ) )
        {
            loadLayerGroup( map, parentLayers, element, tmxFile, imageResolver );
        }
        else if ( name.Equals( "layer" ) )
        {
            loadTileLayer( map, parentLayers, element );
        }
        else if ( name.Equals( "objectgroup" ) )
        {
            loadObjectGroup( map, parentLayers, element );
        }
        else if ( name.Equals( "imagelayer" ) )
        {
            loadImageLayer( map, parentLayers, element, tmxFile, imageResolver );
        }
    }

    protected void loadLayerGroup
        (
            TiledMap       map,
            MapLayers      parentLayers,
            Element        element,
            FileHandle     tmxFile,
            IImageResolver imageResolver
        )
    {
        if ( element.getName().Equals( "group" ) )
        {
            MapGroupLayer groupLayer = new MapGroupLayer();
            loadBasicLayerInfo( groupLayer, element );

            Element properties = element.getChildByName( "properties" );

            if ( properties != null )
            {
                loadProperties( groupLayer.getProperties(), properties );
            }

            for ( int i = 0, j = element.getChildCount(); i < j; i++ )
            {
                Element child = element.getChild( i );
                loadLayer( map, groupLayer.getLayers(), child, tmxFile, imageResolver );
            }

            foreach ( MapLayer layer in groupLayer.getLayers() )
            {
                layer.setParent( groupLayer );
            }

            parentLayers.add( groupLayer );
        }
    }

    protected void loadTileLayer( TiledMap map, MapLayers parentLayers, Element element )
    {
        if ( element.getName().equals( "layer" ) )
        {
            int               width      = element.getIntAttribute( "width",  0 );
            int               height     = element.getIntAttribute( "height", 0 );
            int               tileWidth  = map.getProperties().get( "tilewidth",  Integer.class);
            int               tileHeight = map.getProperties().get( "tileheight", Integer.class);
            TiledMapTileLayer layer      = new TiledMapTileLayer( width, height, tileWidth, tileHeight );

            loadBasicLayerInfo( layer, element );

            int[]            ids      = getTileIds( element, width, height );
            TiledMapTileSets tilesets = map.getTileSets();

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    int  id               = ids[ y * width + x ];
                    bool flipHorizontally = ( ( id & Flag_Flip_Horizontally ) != 0 );
                    bool flipVertically   = ( ( id & Flag_Flip_Vertically ) != 0 );
                    bool flipDiagonally   = ( ( id & Flag_Flip_Diagonally ) != 0 );

                    TiledMapTile tile = tilesets.getTile( id & ~Mask_Clear );

                    if ( tile != null )
                    {
                        Cell cell = createTileLayerCell( flipHorizontally, flipVertically, flipDiagonally );
                        cell.setTile( tile );
                        layer.setCell( x, FlipY ? height - 1 - y : y, cell );
                    }
                }
            }

            Element properties = element.getChildByName( "properties" );

            if ( properties != null )
            {
                loadProperties( layer.getProperties(), properties );
            }

            parentLayers.add( layer );
        }
    }

    protected void loadObjectGroup( TiledMap map, MapLayers parentLayers, Element element )
    {
        if ( element.getName().equals( "objectgroup" ) )
        {
            MapLayer layer = new MapLayer();
            loadBasicLayerInfo( layer, element );
            Element properties = element.getChildByName( "properties" );

            if ( properties != null )
            {
                loadProperties( layer.getProperties(), properties );
            }

            for ( Element objectElement :
            element.getChildrenByName( "object" )) {
                loadObject( map, layer, objectElement );
            }

            parentLayers.add( layer );
        }
    }

    protected void loadImageLayer
        ( TiledMap map, MapLayers parentLayers, Element element, FileHandle tmxFile, ImageResolver imageResolver )
    {
        if ( element.getName().equals( "imagelayer" ) )
        {
            float x = 0;
            float y = 0;

            if ( element.hasAttribute( "offsetx" ) )
            {
                x = Float.parseFloat( element.getAttribute( "offsetx", "0" ) );
            }
            else
            {
                x = Float.parseFloat( element.getAttribute( "x", "0" ) );
            }

            if ( element.hasAttribute( "offsety" ) )
            {
                y = Float.parseFloat( element.getAttribute( "offsety", "0" ) );
            }
            else
            {
                y = Float.parseFloat( element.getAttribute( "y", "0" ) );
            }

            if ( FlipY ) y = MapHeightInPixels - y;

            TextureRegion texture = null;

            Element image = element.getChildByName( "image" );

            if ( image != null )
            {
                string     source = image.getAttribute( "source" );
                FileHandle handle = GetRelativeFileHandle( tmxFile, source );
                texture =  imageResolver.getImage( handle.path() );
                y       -= texture.getRegionHeight();
            }

            TiledMapImageLayer layer = new TiledMapImageLayer( texture, x, y );

            loadBasicLayerInfo( layer, element );

            Element properties = element.getChildByName( "properties" );

            if ( properties != null )
            {
                loadProperties( layer.getProperties(), properties );
            }

            parentLayers.add( layer );
        }
    }

    protected void loadBasicLayerInfo( MapLayer layer, Element element )
    {
        string name    = element.getAttribute( "name", null );
        float  opacity = Float.parseFloat( element.getAttribute( "opacity", "1.0" ) );
        bool   visible = element.getIntAttribute( "visible", 1 ) == 1;
        float  offsetX = element.getFloatAttribute( "offsetx", 0 );
        float  offsetY = element.getFloatAttribute( "offsety", 0 );

        layer.setName( name );
        layer.setOpacity( opacity );
        layer.setVisible( visible );
        layer.setOffsetX( offsetX );
        layer.setOffsetY( offsetY );
    }

    protected void loadObject( TiledMap map, MapLayer layer, Element element )
    {
        loadObject( map, layer.getObjects(), element, MapHeightInPixels );
    }

    protected void loadObject( TiledMap map, TiledMapTile tile, Element element )
    {
        loadObject( map, tile.getObjects(), element, tile.getTextureRegion().getRegionHeight() );
    }

    protected void loadObject( TiledMap map, MapObjects objects, Element element, float heightInPixels )
    {
        if ( element.getName().equals( "object" ) )
        {
            MapObject object = null;

            float scaleX = ConvertObjectToTileSpace ? 1.0f / MapTileWidth : 1.0f;
            float scaleY = ConvertObjectToTileSpace ? 1.0f / MapTileHeight : 1.0f;

            float x = element.getFloatAttribute( "x", 0 ) * scaleX;

            float y = ( FlipY
                ? ( heightInPixels - element.getFloatAttribute( "y", 0 ) )
                : element.getFloatAttribute( "y", 0 ) ) * scaleY;

            float width  = element.getFloatAttribute( "width",  0 ) * scaleX;
            float height = element.getFloatAttribute( "height", 0 ) * scaleY;

            if ( element.getChildCount() > 0 )
            {
                Element child = null;

                if ( ( child = element.getChildByName( "polygon" ) ) != null )
                {
                    string[] points   = child.getAttribute( "points" ).split( " " );
                    float[]  vertices = new float[ points.length * 2 ];

                    for ( int i = 0; i < points.length; i++ )
                    {
                        string[] point = points[ i ].split( "," );
                        vertices[ i * 2 ]     = Float.parseFloat( point[ 0 ] ) * scaleX;
                        vertices[ i * 2 + 1 ] = Float.parseFloat( point[ 1 ] ) * scaleY * ( FlipY ? -1 : 1 );
                    }

                    Polygon polygon = new Polygon( vertices );
                    polygon.setPosition( x, y );
                    object = new PolygonMapObject( polygon );
                }
                else if ( ( child = element.getChildByName( "polyline" ) ) != null )
                {
                    string[] points   = child.getAttribute( "points" ).split( " " );
                    float[]  vertices = new float[ points.length * 2 ];

                    for ( int i = 0; i < points.length; i++ )
                    {
                        string[] point = points[ i ].split( "," );
                        vertices[ i * 2 ]     = Float.parseFloat( point[ 0 ] ) * scaleX;
                        vertices[ i * 2 + 1 ] = Float.parseFloat( point[ 1 ] ) * scaleY * ( FlipY ? -1 : 1 );
                    }

                    Polyline polyline = new Polyline( vertices );
                    polyline.setPosition( x, y );
                    object = new PolylineMapObject( polyline );
                }
                else if ( ( child = element.getChildByName( "ellipse" ) ) != null )
                {
                    object = new EllipseMapObject( x, FlipY ? y - height : y, width, height );
                }
            }

            if ( object == null )
            {
                string gid = null;

                if ( ( gid = element.getAttribute( "gid", null ) ) != null )
                {
                    int  id               = ( int )Long.parseLong( gid );
                    bool flipHorizontally = ( ( id & Flag_Flip_Horizontally ) != 0 );
                    bool flipVertically   = ( ( id & Flag_Flip_Vertically ) != 0 );

                    TiledMapTile tile = map.getTileSets().getTile( id & ~Mask_Clear );

                    TiledMapTileMapObject tiledMapTileMapObject = new TiledMapTileMapObject
                        ( tile, flipHorizontally, flipVertically );

                    TextureRegion textureRegion = tiledMapTileMapObject.getTextureRegion();
                    tiledMapTileMapObject.getProperties().put( "gid", id );
                    tiledMapTileMapObject.setX( x );
                    tiledMapTileMapObject.setY( FlipY ? y : y - height );
                    float objectWidth  = element.getFloatAttribute( "width",  textureRegion.getRegionWidth() );
                    float objectHeight = element.getFloatAttribute( "height", textureRegion.getRegionHeight() );
                    tiledMapTileMapObject.setScaleX( scaleX * ( objectWidth / textureRegion.getRegionWidth() ) );
                    tiledMapTileMapObject.setScaleY( scaleY * ( objectHeight / textureRegion.getRegionHeight() ) );
                    tiledMapTileMapObject.setRotation( element.getFloatAttribute( "rotation", 0 ) );
                    object = tiledMapTileMapObject;
                }
                else
                {
                    object = new RectangleMapObject( x, FlipY ? y - height : y, width, height );
                }
            }

            object.setName( element.getAttribute( "name",       null ) );
            string rotation = element.getAttribute( "rotation", null );

            if ( rotation != null )
            {
                object.getProperties().put( "rotation", Float.parseFloat( rotation ) );
            }

            string type = element.getAttribute( "type", null );

            if ( type != null )
            {
                object.getProperties().put( "type", type );
            }

            int id = element.getIntAttribute( "id", 0 );

            if ( id != 0 )
            {
                object.getProperties().put( "id", id );
            }

            object.getProperties().put( "x", x );

            if ( object instanceof TiledMapTileMapObject) {
                object.getProperties().put( "y", y );
            } else {
                object.getProperties().put( "y", ( FlipY ? y - height : y ) );
            }

            object.getProperties().put( "width",  width );
            object.getProperties().put( "height", height );
            object.setVisible( element.getIntAttribute( "visible", 1 ) == 1 );
            Element properties = element.getChildByName( "properties" );

            if ( properties != null )
            {
                loadProperties( object.getProperties(), properties );
            }

            objects.add( object );
        }
    }

    protected void loadProperties( MapProperties properties, Element element )
    {
        if ( element == null ) return;

        if ( element.getName().equals( "properties" ) )
        {
            for ( Element property :
            element.getChildrenByName( "property" )) {
                string name  = property.getAttribute( "name",  null );
                string value = property.getAttribute( "value", null );
                string type  = property.getAttribute( "type",  null );

                if ( value == null )
                {
                    value = property.getText();
                }

                Object castValue = castProperty( name, value, type );
                properties.put( name, castValue );
            }
        }
    }

    protected Object castProperty( string name, string value, string type )
    {
        if ( type == null )
        {
            return value;
        }
        else if ( type.equals( "int" ) )
        {
            return Integer.valueOf( value );
        }
        else if ( type.equals( "float" ) )
        {
            return Float.valueOf( value );
        }
        else if ( type.equals( "bool" ) )
        {
            return bool.valueOf( value );
        }
        else if ( type.equals( "color" ) )
        {
            // Tiled uses the format #AARRGGBB
            string opaqueColor = value.substring( 3 );
            string alpha       = value.substring( 1, 3 );
            return Color.valueOf( opaqueColor + alpha );
        }
        else
        {
            throw new GdxRuntimeException
                (
                 "Wrong type given for property " + name + ", given : " + type
                 + ", supported : string, bool, int, float, color"
                );
        }
    }

    protected Cell createTileLayerCell( bool flipHorizontally, bool flipVertically, bool flipDiagonally )
    {
        Cell cell = new Cell();

        if ( flipDiagonally )
        {
            if ( flipHorizontally && flipVertically )
            {
                cell.setFlipHorizontally( true );
                cell.setRotation( Cell.ROTATE_270 );
            }
            else if ( flipHorizontally )
            {
                cell.setRotation( Cell.ROTATE_270 );
            }
            else if ( flipVertically )
            {
                cell.setRotation( Cell.ROTATE_90 );
            }
            else
            {
                cell.setFlipVertically( true );
                cell.setRotation( Cell.ROTATE_270 );
            }
        }
        else
        {
            cell.setFlipHorizontally( flipHorizontally );
            cell.setFlipVertically( flipVertically );
        }

        return cell;
    }

    static public int[] getTileIds( Element element, int width, int height )
    {
        Element data     = element.getChildByName( "data" );
        string  encoding = data.getAttribute( "encoding", null );

        if ( encoding == null )
        {
            // no 'encoding' attribute means that the encoding is XML
            throw new GdxRuntimeException( "Unsupported encoding (XML) for TMX Layer Data" );
        }

        int[] ids = new int[ width * height ];

        if ( encoding.equals( "csv" ) )
        {
            string[] array = data.getText().split( "," );

            for ( int i = 0; i < array.length; i++ )
                ids[ i ] = ( int )Long.parseLong( array[ i ].trim() );
        }
        else
        {
            if ( true )
                if ( encoding.equals( "base64" ) )
                {
                    InputStream is  = null;

                    try
                    {
                        string compression = data.getAttribute( "compression", null );
                        byte[] bytes       = Base64Coder.decode( data.getText() );

                        if ( compression == null )
                            is = new ByteArrayInputStream( bytes );
                        else if ( compression.equals( "gzip" ) )
                            is = new BufferedInputStream
                            ( new GZIPInputStream( new ByteArrayInputStream( bytes ), bytes.length ) );

                        else if ( compression.equals( "zlib" ) )
                            is = new BufferedInputStream
                            ( new InflaterInputStream( new ByteArrayInputStream( bytes ) ) );

                        else

                        throw new GdxRuntimeException
                            ( "Unrecognised compression (" + compression + ") for TMX Layer Data" );

                        byte[] temp = new byte[ 4 ];

                        for ( int y = 0; y < height; y++ )
                        {
                            for ( int x = 0; x < width; x++ )
                            {
                                int read = is.read( temp );

                                while ( read < temp.length )
                                {
                                    int curr = is.read( temp, read, temp.length - read );
                                    if ( curr == -1 ) break;
                                    read += curr;
                                }

                                if ( read != temp.length )
                                    throw new GdxRuntimeException
                                        ( "Error Reading TMX Layer Data: Premature end of tile data" );

                                ids[ y * width + x ] = unsignedByteToInt
                                        ( temp[ 0 ] ) | unsignedByteToInt( temp[ 1 ] ) << 8
                                                      | unsignedByteToInt( temp[ 2 ] ) << 16 | unsignedByteToInt
                                                          ( temp[ 3 ] ) << 24;
                            }
                        }
                    }
                    catch ( IOException e )
                    {
                        throw new GdxRuntimeException
                            ( "Error Reading TMX Layer Data - IOException: " + e.getMessage() );
                    }
                    finally
                    {
                        StreamUtils.closeQuietly(  is);
                    }
                }
                else
                {
                    // any other value of 'encoding' is one we're not aware of, probably a feature of a future version of Tiled
                    // or another editor
                    throw new GdxRuntimeException( "Unrecognised encoding (" + encoding + ") for TMX Layer Data" );
                }
        }

        return ids;
    }

    protected static int unsignedByteToInt( byte b )
    {
        return b & 0xFF;
    }

    protected static FileHandle GetRelativeFileHandle( FileHandle file, string path )
    {
        StringTokenizer tokenizer = new StringTokenizer( path, "\\/" );
        FileHandle      result    = file.parent();

        while ( tokenizer.hasMoreElements() )
        {
            string token = tokenizer.nextToken();

            if ( token.Equals( ".." ) )
            {
                result = result.Parent();
            }
            else
            {
                result = result.Child( token );
            }
        }

        return result;
    }

    protected void LoadTileSet( Element element, FileHandle tmxFile, IImageResolver imageResolver )
    {
        if ( element.GetName().equals( "tileset" ) )
        {
            int        firstgid    = element.getIntAttribute( "firstgid", 1 );
            string     imageSource = "";
            int        imageWidth  = 0;
            int        imageHeight = 0;
            FileHandle image       = null;

            string source = element.getAttribute( "source", null );

            if ( source != null )
            {
                FileHandle tsx = GetRelativeFileHandle( tmxFile, source );

                try
                {
                    element = Xml.parse( tsx );
                    Element imageElement = element.getChildByName( "image" );

                    if ( imageElement != null )
                    {
                        imageSource = imageElement.getAttribute( "source" );
                        imageWidth  = imageElement.getIntAttribute( "width",  0 );
                        imageHeight = imageElement.getIntAttribute( "height", 0 );
                        image       = GetRelativeFileHandle( tsx, imageSource );
                    }
                }
                catch ( SerializationException e )
                {
                    throw new GdxRuntimeException( "Error parsing external tileset." );
                }
            }
            else
            {
                Element imageElement = element.GetChildByName( "image" );

                if ( imageElement != null )
                {
                    imageSource = imageElement.getAttribute( "source" );
                    imageWidth  = imageElement.getIntAttribute( "width",  0 );
                    imageHeight = imageElement.getIntAttribute( "height", 0 );
                    image       = GetRelativeFileHandle( tmxFile, imageSource );
                }
            }

            var name       = element.Get( "name", null );
            var tilewidth  = element.GetIntAttribute( "tilewidth",  0 );
            var tileheight = element.GetIntAttribute( "tileheight", 0 );
            var spacing    = element.GetIntAttribute( "spacing",    0 );
            var margin     = element.GetIntAttribute( "margin",     0 );

            var offset  = element.GetChildByName( "tileoffset" );
            var offsetX = 0;
            var offsetY = 0;

            if ( offset != null )
            {
                offsetX = offset.getIntAttribute( "x", 0 );
                offsetY = offset.getIntAttribute( "y", 0 );
            }

            var tileSet = new TiledMapTileSet
            {
                Name = name
            };

            var tileSetProperties = tileSet.GetProperties();
            var properties        = element.GetChildByName( "properties" );

            if ( properties != null )
            {
                loadProperties( tileSetProperties, properties );
            }

            tileSetProperties.put( "firstgid", firstgid );

            // Tiles
            Utils.Array< Element > tileElements = element.getChildrenByName( "tile" );

            addStaticTiles
                (
                 tmxFile, imageResolver, tileSet, element, tileElements, name, firstgid, tilewidth, tileheight, spacing,
                 margin,  source,        offsetX, offsetY, imageSource,  imageWidth, imageHeight, image
                );

            Utils.Array< AnimatedTiledMapTile > animatedTiles = new Utils.Array< AnimatedTiledMapTile >();

            for ( Element tileElement :
            tileElements) {
                int          localtid = tileElement.getIntAttribute( "id", 0 );
                TiledMapTile tile     = tileSet.getTile( firstgid + localtid );

                if ( tile != null )
                {
                    AnimatedTiledMapTile animatedTile = createAnimatedTile( tileSet, tile, tileElement, firstgid );

                    if ( animatedTile != null )
                    {
                        animatedTiles.add( animatedTile );
                        tile = animatedTile;
                    }

                    AddTileProperties( tile, tileElement );
                    addTileObjectGroup( tile, tileElement );
                }
            }

            // replace original static tiles by animated tiles
            for ( AnimatedTiledMapTile animatedTile :
            animatedTiles) {
                tileSet.putTile( animatedTile.getId(), animatedTile );
            }

            Map.getTileSets().addTileSet( tileSet );
        }
    }

    protected abstract void addStaticTiles
        (
            FileHandle tmxFile, ImageResolver imageResolver, TiledMapTileSet tileset,
            Element element, Utils.Array< Element > tileElements, string name, int firstgid, int tilewidth,
            int tileheight,
            int spacing,
            int margin, string source, int offsetX, int offsetY, string imageSource, int imageWidth, int imageHeight,
            FileHandle image
        );

    protected void AddTileProperties( TiledMapTile tile, Element tileElement )
    {
        string terrain = tileElement.getAttribute( "terrain", null );

        if ( terrain != null )
        {
            tile.getProperties().put( "terrain", terrain );
        }

        string probability = tileElement.getAttribute( "probability", null );

        if ( probability != null )
        {
            tile.getProperties().put( "probability", probability );
        }

        Element properties = tileElement.GetChildByName( "properties" );

        if ( properties != null )
        {
            loadProperties( tile.getProperties(), properties );
        }
    }

    protected void addTileObjectGroup( TiledMapTile tile, Element tileElement )
    {
        Element objectgroupElement = tileElement.getChildByName( "objectgroup" );

        if ( objectgroupElement != null )
        {
            for ( Element objectElement :
            objectgroupElement.getChildrenByName( "object" )) {
                loadObject( Map, tile, objectElement );
            }
        }
    }

    protected AnimatedTiledMapTile CreateAnimatedTile
        (
            TiledMapTileSet tileSet,
            ITiledMapTile   tile,
            Element         tileElement,
            int             firstgid
        )
    {
        var animationElement = tileElement.GetChildByName( "animation" );

        if ( animationElement != null )
        {
            var staticTiles = new Utils.Array< StaticTiledMapTile >();
            var intervals   = new IntArray();

            foreach ( var frameElement in animationElement.GetChildrenByName( "frame" ) )
            {
                staticTiles.Add
                    ( tileSet.GetTile( firstgid + frameElement.GetIntAttribute( "tileid" ) ) );

                intervals.Items.Add( frameElement.GetIntAttribute( "duration" ) );
            }

            var animatedTile = new AnimatedTiledMapTile( intervals, staticTiles );
            animatedTile.SetId( tile.GetId() );

            return animatedTile;
        }

        return null;
    }

    protected void AddStaticTiledMapTile
        (
            TiledMapTileSet tileSet,
            TextureRegion   textureRegion,
            int             tileId,
            float           offsetX,
            float           offsetY
        )
    {
        ITiledMapTile tile = new StaticTiledMapTile( textureRegion );

        tile.SetId( tileId );
        tile.SetOffsetX( offsetX );
        tile.SetOffsetY( FlipY ? -offsetY : offsetY );

        tileSet.PutTile( tileId, tile );
    }
}
