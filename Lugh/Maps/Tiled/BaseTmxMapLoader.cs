// ##################################################

using System.IO;
using System.Runtime.Serialization;

using ILNumerics;

using Lugh.Assets;
using Lugh.Collections;
using Lugh.Files;
using Lugh.Files.Xml;
using Lugh.Maps.Objects;
using Lugh.Maps.Tiled.Tiles;

// ##################################################

namespace Lugh.Maps.Tiled
{
    public abstract class BaseTmxMapLoader< TP > : AsynchronousAssetLoader< TiledMap, TP >
        where TP : BaseTmxMapLoader< TP >.Parameters
    {
        public class Parameters : AssetLoaderParameters
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

        public List< AssetDescriptor > GetDependencies( string fileName, FileHandle tmxFile, TP parameter )
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

        protected abstract List< AssetDescriptor > GetDependencyAssetDescriptors
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

            var mapWidth      = Root.GetIntAttribute( "width",         0 );
            var mapHeight     = Root.GetIntAttribute( "height",        0 );
            var tileWidth     = Root.GetIntAttribute( "tilewidth",     0 );
            var tileHeight    = Root.GetIntAttribute( "tileheight",    0 );
            var hexSideLength = Root.GetIntAttribute( "hexsidelength", 0 );

            var mapOrientation     = Root.GetAttribute( "orientation",     null );
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
                LoadProperties( Map.GetProperties(), properties );
            }

            var tilesets = Root.GetChildrenByName( "tileset" );

            foreach ( var element in tilesets )
            {
                LoadTileSet( element, tmxFile, imageResolver );
                Root.RemoveChild( element );
            }

            for ( int i = 0, j = Root.GetChildCount(); i < j; i++ )
            {
                var element = Root.GetChild( i );

                LoadLayer( Map, Map.Layers, element, tmxFile, imageResolver );
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
            var name = element.Name;

            if ( name.Equals( "group" ) )
            {
                LoadLayerGroup( map, parentLayers, element, tmxFile, imageResolver );
            }
            else if ( name.Equals( "layer" ) )
            {
                LoadTileLayer( map, parentLayers, element );
            }
            else if ( name.Equals( "objectgroup" ) )
            {
                LoadObjectGroup( map, parentLayers, element );
            }
            else if ( name.Equals( "imagelayer" ) )
            {
                LoadImageLayer( map, parentLayers, element, tmxFile, imageResolver );
            }
        }

        protected void LoadLayerGroup
            (
                TiledMap       map,
                MapLayers      parentLayers,
                Element        element,
                FileHandle     tmxFile,
                IImageResolver imageResolver
            )
        {
            if ( element.Name.Equals( "group" ) )
            {
                var groupLayer = new MapGroupLayer();

                LoadBasicLayerInfo( groupLayer, element );

                Element properties = element.GetChildByName( "properties" );

                if ( properties != null )
                {
                    LoadProperties( groupLayer.Properties, properties );
                }

                for ( int i = 0, j = element.GetChildCount(); i < j; i++ )
                {
                    Element child = element.GetChild( i );
                    loadLayer( map, groupLayer.getLayers(), child, tmxFile, imageResolver );
                }

                foreach ( MapLayer layer in groupLayer.getLayers() )
                {
                    layer.setParent( groupLayer );
                }

                parentLayers.add( groupLayer );
            }
        }

        protected void LoadTileLayer( TiledMap map, MapLayers parentLayers, Element element )
        {
            if ( element.getName().equals( "layer" ) )
            {
                int               width      = element.GetIntAttribute( "width",  0 );
                int               height     = element.GetIntAttribute( "height", 0 );
                int               tileWidth  = map.getProperties().get( "tilewidth",  Integer.class);
                int               tileHeight = map.getProperties().get( "tileheight", Integer.class);
                TiledMapTileLayer layer      = new TiledMapTileLayer( width, height, tileWidth, tileHeight );

                LoadBasicLayerInfo( layer, element );

                int[]            ids      = GetTileIds( element, width, height );
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
                            Cell cell = CreateTileLayerCell( flipHorizontally, flipVertically, flipDiagonally );
                            cell.setTile( tile );
                            layer.setCell( x, FlipY ? height - 1 - y : y, cell );
                        }
                    }
                }

                Element properties = element.GetChildByName( "properties" );

                if ( properties != null )
                {
                    LoadProperties( layer.getProperties(), properties );
                }

                parentLayers.add( layer );
            }
        }

        protected void LoadObjectGroup( TiledMap map, MapLayers parentLayers, Element element )
        {
            if ( element.getName().equals( "objectgroup" ) )
            {
                MapLayer layer = new MapLayer();
                LoadBasicLayerInfo( layer, element );
                Element properties = element.GetChildByName( "properties" );

                if ( properties != null )
                {
                    LoadProperties( layer.getProperties(), properties );
                }

                for ( Element objectElement :
                element.GetChildrenByName( "object" )) {
                    LoadObject( map, layer, objectElement );
                }

                parentLayers.add( layer );
            }
        }

        protected void LoadImageLayer
            ( TiledMap map, MapLayers parentLayers, Element element, FileHandle tmxFile, IImageResolver imageResolver )
        {
            if ( element.Name.Equals( "imagelayer" ) )
            {
                float x = 0;
                float y = 0;

                if ( element.HasAttribute( "offsetx" ) )
                {
                    x = float.Parse( element.GetAttribute( "offsetx", "0" ) );
                }
                else
                {
                    x = float.Parse( element.GetAttribute( "x", "0" ) );
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

                Element image = element.GetChildByName( "image" );

                if ( image != null )
                {
                    string     source = image.getAttribute( "source" );
                    FileHandle handle = GetRelativeFileHandle( tmxFile, source );
                    texture =  imageResolver.getImage( handle.path() );
                    y       -= texture.getRegionHeight();
                }

                TiledMapImageLayer layer = new TiledMapImageLayer( texture, x, y );

                LoadBasicLayerInfo( layer, element );

                Element properties = element.GetChildByName( "properties" );

                if ( properties != null )
                {
                    LoadProperties( layer.getProperties(), properties );
                }

                parentLayers.add( layer );
            }
        }

        protected void LoadBasicLayerInfo( MapLayer layer, Element element )
        {
            string name    = element.getAttribute( "name", null );
            float  opacity = Float.parseFloat( element.getAttribute( "opacity", "1.0" ) );
            bool   visible = element.GetIntAttribute( "visible", 1 ) == 1;
            float  offsetX = element.GetFloatAttribute( "offsetx", 0 );
            float  offsetY = element.GetFloatAttribute( "offsety", 0 );

            layer.setName( name );
            layer.setOpacity( opacity );
            layer.setVisible( visible );
            layer.setOffsetX( offsetX );
            layer.setOffsetY( offsetY );
        }

        protected void LoadObject( TiledMap map, MapLayer layer, Element element )
        {
            LoadObject( map, layer.getObjects(), element, MapHeightInPixels );
        }

        protected void LoadObject( TiledMap map, ITiledMapTile tile, Element element )
        {
            LoadObject( map, tile.getObjects(), element, tile.getTextureRegion().getRegionHeight() );
        }

        protected void LoadObject( TiledMap map, MapObjects objects, Element element, float heightInPixels )
        {
            if ( element.getName().equals( "object" ) )
            {
                MapObject object = null;

                float scaleX = ConvertObjectToTileSpace ? 1.0f / MapTileWidth : 1.0f;
                float scaleY = ConvertObjectToTileSpace ? 1.0f / MapTileHeight : 1.0f;

                float x = element.GetFloatAttribute( "x", 0 ) * scaleX;

                float y = ( FlipY
                    ? ( heightInPixels - element.GetFloatAttribute( "y", 0 ) )
                    : element.GetFloatAttribute( "y", 0 ) ) * scaleY;

                float width  = element.GetFloatAttribute( "width",  0 ) * scaleX;
                float height = element.GetFloatAttribute( "height", 0 ) * scaleY;

                if ( element.GetChildCount() > 0 )
                {
                    Element child = null;

                    if ( ( child = element.GetChildByName( "polygon" ) ) != null )
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
                    else if ( ( child = element.GetChildByName( "polyline" ) ) != null )
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
                    else if ( ( child = element.GetChildByName( "ellipse" ) ) != null )
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
                        float objectWidth  = element.GetFloatAttribute( "width",  textureRegion.getRegionWidth() );
                        float objectHeight = element.GetFloatAttribute( "height", textureRegion.getRegionHeight() );
                        tiledMapTileMapObject.setScaleX( scaleX * ( objectWidth / textureRegion.getRegionWidth() ) );
                        tiledMapTileMapObject.setScaleY( scaleY * ( objectHeight / textureRegion.getRegionHeight() ) );
                        tiledMapTileMapObject.setRotation( element.GetFloatAttribute( "rotation", 0 ) );
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

                int id = element.GetIntAttribute( "id", 0 );

                if ( id != 0 )
                {
                    object.getProperties().put( "id", id );
                }

                object.getProperties().put( "x", x );

                if ( object instanceof tiledMapTileMapObject) {
                    object.getProperties().put( "y", y );
                } else {
                    object.getProperties().put( "y", ( FlipY ? y - height : y ) );
                }

                object.getProperties().put( "width",  width );
                object.getProperties().put( "height", height );
                object.setVisible( element.GetIntAttribute( "visible", 1 ) == 1 );
                Element properties = element.GetChildByName( "properties" );

                if ( properties != null )
                {
                    LoadProperties( object.getProperties(), properties );
                }

                objects.Add( object );
            }
        }

        protected void LoadProperties( MapProperties properties, Element element )
        {
            if ( element == null ) return;

            if ( element.Name.Equals( "properties" ) )
            {
                foreach ( var property in element.GetChildrenByName( "property" ))
                {
                    var name  = property.GetAttribute( "name",  null );
                    var value = property.GetAttribute( "value", null );
                    var type  = property.GetAttribute( "type",  null );

                    if ( value == null )
                    {
                        value = property.Text;
                    }

                    var castValue = CastProperty( name, value, type );
                    
                    properties.Put( name, castValue );
                }
            }
        }

        protected object CastProperty( string name, string value, string type )
        {
            if ( type == null )
            {
                return value;
            }
            else if ( type.equals( "int" ) )
            {
                return Integer.valueOf( value );
            }
            else if ( type.Equals( "float" ) )
            {
                return float.ValueOf( value );
            }
            else if ( type.Equals( "bool" ) )
            {
                return bool.ValueOf( value );
            }
            else if ( type.Equals( "color" ) )
            {
                // Tiled uses the format #AARRGGBB
                var opaqueColor = value.Substring( 3 );
                var alpha       = value.Substring( 1, 3 );
                
                return Color.ValueOf( opaqueColor + alpha );
            }
            else
            {
                throw new RuntimeException
                    (
                     "Wrong type given for property " + name + ", given : " + type
                     + ", supported : string, bool, int, float, color"
                    );
            }
        }

        protected Cell CreateTileLayerCell( bool flipHorizontally, bool flipVertically, bool flipDiagonally )
        {
            var cell = new Cell();

            if ( flipDiagonally )
            {
                if ( flipHorizontally && flipVertically )
                {
                    cell.SetFlipHorizontally( true );
                    cell.SetRotation( Cell.ROTATE_270 );
                }
                else if ( flipHorizontally )
                {
                    cell.SetRotation( Cell.ROTATE_270 );
                }
                else if ( flipVertically )
                {
                    cell.SetRotation( Cell.ROTATE_90 );
                }
                else
                {
                    cell.SetFlipVertically( true );
                    cell.SetRotation( Cell.ROTATE_270 );
                }
            }
            else
            {
                cell.SetFlipHorizontally( flipHorizontally );
                cell.SetFlipVertically( flipVertically );
            }

            return cell;
        }

        public int[] GetTileIds( Element element, int width, int height )
        {
            var data     = element.GetChildByName( "data" );
            var encoding = data.GetAttribute( "encoding", null );

            if ( encoding == null )
            {
                // no 'encoding' attribute means that the encoding is XML
                throw new RuntimeException( "Unsupported encoding (XML) for TMX Layer Data" );
            }

            var ids = new int[ width * height ];

            if ( encoding.Equals( "csv" ) )
            {
                var array = data.Text.Split( "," );

                for ( var i = 0; i < array.Length; i++ )
                {
                    ids[ i ] = ( int )long.Parse( array[ i ].Trim() );
                }
            }
            else
            {
                if ( encoding.Equals( "base64" ) )
                {
                    InputStream istream = null;

                    try
                    {
                        string compression = data.GetAttribute( "compression", null );
                        byte[] bytes       = Base64Coder.decode( data.GetText() );

                        if ( compression == null )
                        {
                            istream = new ByteArrayInputStream( bytes );
                        }
                        else if ( compression.equals( "gzip" ) )
                        {
                            istream = new BufferedInputStream( new GZIPInputStream( new ByteArrayInputStream( bytes ), bytes.length ) );
                        }
                        else if ( compression.equals( "zlib" ) )
                        {
                            istream = new BufferedInputStream( new InflaterInputStream( new ByteArrayInputStream( bytes ) ) );
                        }
                        else
                        {
                            throw new RuntimeException( "Unrecognised compression (" + compression + ") for TMX Layer Data" );
                        }

                        var temp = new byte[ 4 ];

                        for ( var y = 0; y < height; y++ )
                        {
                            for ( var x = 0; x < width; x++ )
                            {
                                int read = istream.read( temp );

                                while ( read < temp.Length )
                                {
                                    int curr = istream.read( temp, read, temp.Length - read );

                                    if ( curr == -1 )
                                    {
                                        break;
                                    }

                                    read += curr;
                                }

                                if ( read != temp.Length )
                                {
                                    throw new RuntimeException( "Error Reading TMX Layer Data: Premature end of tile data" );
                                }

                                ids[ y * width + x ] = NumberUtils.UnsignedByteToInt( temp[ 0 ] )
                                                       | NumberUtils.UnsignedByteToInt( temp[ 1 ] ) << 8
                                                       | NumberUtils.UnsignedByteToInt( temp[ 2 ] ) << 16
                                                       | NumberUtils.UnsignedByteToInt( temp[ 3 ] ) << 24;
                            }
                        }
                    }
                    catch ( IOException e )
                    {
                        throw new RuntimeException( "Error Reading TMX Layer Data - IOException: " + e.Message );
                    }
                    finally
                    {
                        StreamUtils.closeQuietly( istream );
                    }
                }
                else
                {
                    // any other value of 'encoding' is one we're not aware of, probably a feature of a future version of Tiled
                    // or another editor
                    throw new RuntimeException( "Unrecognised encoding (" + encoding + ") for TMX Layer Data" );
                }
            }

            return ids;
        }

        protected static FileHandle GetRelativeFileHandle( FileHandle file, string path )
        {
            StringTokenizer tokenizer = new StringTokenizer( path, "\\/" );
            FileHandle      result    = file.Parent();

            while ( tokenizer.HasMoreTokens )
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
            if ( element.Name.Equals( "tileset" ) )
            {
                FileHandle image    = null;
                var        firstgid = element.GetIntAttribute( "firstgid", 1 );
                var        source   = element.GetAttribute( "source", null );

                var imageSource = "";
                var imageWidth  = 0;
                var imageHeight = 0;

                if ( source != null )
                {
                    var tsx = GetRelativeFileHandle( tmxFile, source );

                    try
                    {
                        element = Xml.Parse( tsx );
                        Element imageElement = element.GetChildByName( "image" );

                        if ( imageElement != null )
                        {
                            imageSource = imageElement.GetAttribute( "source" );
                            imageWidth  = imageElement.GetIntAttribute( "width",  0 );
                            imageHeight = imageElement.GetIntAttribute( "height", 0 );
                            image       = GetRelativeFileHandle( tsx, imageSource );
                        }
                    }
                    catch ( SerializationException e )
                    {
                        throw new RuntimeException( "Error parsing external tileset." );
                    }
                }
                else
                {
                    var imageElement = element.GetChildByName( "image" );

                    if ( imageElement != null )
                    {
                        imageSource = imageElement.GetAttribute( "source" );
                        imageWidth  = imageElement.GetIntAttribute( "width",  0 );
                        imageHeight = imageElement.GetIntAttribute( "height", 0 );
                        image       = GetRelativeFileHandle( tmxFile, imageSource );
                    }
                }

                var name = element.Get( "name", null );

                var tilewidth  = element.GetIntAttribute( "tilewidth",  0 );
                var tileheight = element.GetIntAttribute( "tileheight", 0 );
                var spacing    = element.GetIntAttribute( "spacing",    0 );
                var margin     = element.GetIntAttribute( "margin",     0 );

                var offset  = element.GetChildByName( "tileoffset" );
                var offsetX = 0;
                var offsetY = 0;

                if ( offset != null )
                {
                    offsetX = offset.GetIntAttribute( "x", 0 );
                    offsetY = offset.GetIntAttribute( "y", 0 );
                }

                var tileSet = new TiledMapTileSet
                {
                    Name = name
                };

                var tileSetProperties = tileSet.MapProperties;

                var properties = element.GetChildByName( "properties" );

                if ( properties != null )
                {
                    LoadProperties( tileSetProperties, properties );
                }

                tileSetProperties.Put( "firstgid", firstgid );

                var tileElements = element.GetChildrenByName( "tile" );

                AddStaticTiles
                    (
                     tmxFile,      imageResolver, tileSet,     element,
                     tileElements, name,          firstgid,    tilewidth,
                     tileheight,   spacing,       margin,      source,
                     offsetX,      offsetY,       imageSource, imageWidth,
                     imageHeight,  image
                    );

                var animatedTiles = new List< AnimatedTiledMapTile >();

                foreach ( Element tileElement in tileElements )
                {
                    var localtid = tileElement.GetIntAttribute( "id", 0 );

                    ITiledMapTile tile = tileSet.GetTile( firstgid + localtid );

                    if ( tile != null )
                    {
                        AnimatedTiledMapTile animatedTile = CreateAnimatedTile( tileSet, tile, tileElement, firstgid );

                        if ( animatedTile != null )
                        {
                            animatedTiles.Add( animatedTile );
                            tile = animatedTile;
                        }

                        AddTileProperties( tile, tileElement );
                        AddTileObjectGroup( tile, tileElement );
                    }
                }

                // replace original static tiles by animated tiles
                foreach ( AnimatedTiledMapTile animatedTile in animatedTiles )
                {
                    tileSet.PutTile( animatedTile.ID, animatedTile );
                }

                Map.GetTileSets().AddTileSet( tileSet );
            }
        }

        protected abstract void AddStaticTiles
            (
                FileHandle      tmxFile,
                IImageResolver  imageResolver,
                TiledMapTileSet tileset,
                Element         element,
                List< Element > tileElements,
                string          name,
                int             firstgid,
                int             tilewidth,
                int             tileheight,
                int             spacing,
                int             margin,
                string          source,
                int             offsetX,
                int             offsetY,
                string          imageSource,
                int             imageWidth,
                int             imageHeight,
                FileHandle      image
            );

        protected void AddTileProperties( ITiledMapTile tile, Element tileElement )
        {
            string terrain = tileElement.GetAttribute( "terrain", null );

            if ( terrain != null )
            {
                tile.GetProperties().Put( "terrain", terrain );
            }

            var probability = tileElement.GetAttribute( "probability", null );

            if ( probability != null )
            {
                tile.GetProperties().Put( "probability", probability );
            }

            var properties = tileElement.GetChildByName( "properties" );

            if ( properties != null )
            {
                LoadProperties( tile.GetProperties(), properties );
            }
        }

        protected void AddTileObjectGroup( ITiledMapTile tile, Element tileElement )
        {
            var objectgroupElement = tileElement.GetChildByName( "objectgroup" );

            if ( objectgroupElement != null )
            {
                foreach ( var objectElement in tileElement.GetChildrenByName( "object" ) )
                {
                    LoadObject( Map, tile, objectElement );
                }
            }
        }

        protected AnimatedTiledMapTile CreateAnimatedTile( TiledMapTileSet tileSet, ITiledMapTile tile, Element tileElement, int firstgid )
        {
            var animationElement = tileElement.GetChildByName( "animation" );

            if ( animationElement != null )
            {
                var staticTiles = new List< StaticTiledMapTile >();
                var intervals   = new IntArray();

                foreach ( var frameElement in animationElement.GetChildrenByName( "frame" ) )
                {
                    staticTiles.Add( tileSet.GetTile( firstgid + frameElement.GetIntAttribute( "tileid" ) ) );

                    intervals.Items.Add( frameElement.GetIntAttribute( "duration" ) );
                }

                var animatedTile = new AnimatedTiledMapTile( intervals, staticTiles )
                {
                    ID = tile.ID
                };

                return animatedTile;
            }

            return null;
        }

        protected void AddStaticTiledMapTile( TiledMapTileSet tileSet, TextureRegion textureRegion, int tileId, float offsetX, float offsetY )
        {
            ITiledMapTile tile = new StaticTiledMapTile( textureRegion );

            tile.ID = tileId;
            tile.SetOffsetX( offsetX );
            tile.SetOffsetY( FlipY ? -offsetY : offsetY );

            tileSet.PutTile( tileId, tile );
        }
    }
}
