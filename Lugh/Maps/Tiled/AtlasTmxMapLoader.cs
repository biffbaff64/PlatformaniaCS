// ##################################################

// ##################################################

using Lugh.Assets;
using Lugh.Assets.Resolvers;

namespace Lugh.Maps.Tiled
{
    public class AtlasTmxMapLoader : BaseTmxMapLoader< AtlasTmxMapLoader.AtlasTiledMapLoaderParameters >
    {
        public class AtlasTiledMapLoaderParameters : BaseTmxMapLoader.Parameters
        {
            public bool ForceTextureFilters { get; set; } = false;
        }

        protected interface IAtlasResolver : IImageResolver
        {
            public TextureAtlas GetAtlas();

            public static class DirectAtlasResolver : AtlasTmxMapLoader.IAtlasResolver
            {
                private TextureAtlas _atlas;

                public DirectAtlasResolver( TextureAtlas atlas )
                {
                    this._atlas = atlas;
                }

                public TextureAtlas GetAtlas()
                {
                    return _atlas;
                }

                public TextureRegion GetImage( String name )
                {
                    return _atlas.findRegion( name );
                }
            }

            public static class AssetManagerAtlasResolver
                : AtlasTmxMapLoader.
                    IAtlasResolver
            {
                private AssetManager _assetManager;
                private String       _atlasName;

                public
                    AssetManagerAtlasResolver( AssetManager assetManager, String atlasName )
                {
                    this._assetManager = assetManager;
                    this._atlasName    = atlasName;
                }

                public TextureAtlas GetAtlas()
                {
                    return _assetManager.get( _atlasName, TextureAtlas.class);
                }

                public TextureRegion GetImage( String name )
                {
                    return GetAtlas().findRegion( name );
                }
            }
        }

        protected Array< Texture > trackedTextures = new Array< Texture >();
        protected IAtlasResolver   atlasResolver;

        public AtlasTmxMapLoader()
        {
            base( new InternalFileHandleResolver() );
        }

        public AtlasTmxMapLoader( FileHandleResolver resolver )
        {
            base( resolver );
        }

        public TiledMap Load( String fileName )
        {
            return Load( fileName, new AtlasTiledMapLoaderParameters() );
        }

        public TiledMap Load( String fileName, AtlasTiledMapLoaderParameters parameter )
        {
            FileHandle tmxFile = resolve( fileName );

            this.Root = Xml.parse( tmxFile );

            FileHandle   atlasFileHandle = GetAtlasFileHandle( tmxFile );
            TextureAtlas atlas           = new TextureAtlas( atlasFileHandle );
            this.atlasResolver = new IAtlasResolver.DirectAtlasResolver( atlas );

            TiledMap map = loadTiledMap( tmxFile, parameter, atlasResolver );
            map.setOwnedResources( new Array< TextureAtlas >( new TextureAtlas[] { atlas } ) );
            SetTextureFilters( parameter.TextureMinFilter, parameter.TextureMagFilter );
            return map;
        }

        public void LoadAsync
            ( AssetManager manager, String fileName, FileHandle tmxFile, AtlasTiledMapLoaderParameters parameter )
        {
            FileHandle atlasHandle = GetAtlasFileHandle( tmxFile );
            this.atlasResolver = new IAtlasResolver.AssetManagerAtlasResolver( manager, atlasHandle.path() );

            this.Map = loadTiledMap( tmxFile, parameter, atlasResolver );
        }

        public TiledMap LoadSync
            ( AssetManager manager, String fileName, FileHandle file, AtlasTiledMapLoaderParameters parameter )
        {
            if ( parameter != null )
            {
                SetTextureFilters( parameter.TextureMinFilter, parameter.TextureMagFilter );
            }

            return Map;
        }

        protected Array< AssetDescriptor > GetDependencyAssetDescriptors
            ( FileHandle tmxFile, TextureLoader.TextureParameter textureParameter )
        {
            Array< AssetDescriptor > descriptors = new Array< AssetDescriptor >();

            // Atlas dependencies
            FileHandle atlasFileHandle = GetAtlasFileHandle( tmxFile );

            if ( atlasFileHandle != null )
            {
                descriptors.add( new AssetDescriptor( atlasFileHandle, TextureAtlas.class));
            }

            return descriptors;
        }

        protected void AddStaticTiles
            (
                FileHandle tmxFile, ImageResolver imageResolver, TiledMapTileSet tileSet, Element element,
                Array< Element > tileElements, String name, int firstgid, int tilewidth, int tileheight, int spacing,
                int margin,
                String source, int offsetX, int offsetY, String imageSource, int imageWidth, int imageHeight, FileHandle image
            )
        {
            TextureAtlas atlas       = atlasResolver.GetAtlas();
            String       regionsName = name;

            for ( Texture texture :
            atlas.getTextures()) {
                trackedTextures.add( texture );
            }

            MapProperties props = tileSet.getProperties();
        
            props.put( "imagesource", imageSource );
            props.put( "imagewidth",  imageWidth );
            props.put( "imageheight", imageHeight );
            props.put( "tilewidth",   tilewidth );
            props.put( "tileheight",  tileheight );
            props.put( "margin",      margin );
            props.put( "spacing",     spacing );

            if ( imageSource != null && imageSource.length() > 0 )
            {
                int lastgid = firstgid + ( ( imageWidth / tilewidth ) * ( imageHeight / tileheight ) ) - 1;
                for ( AtlasRegion region :
                atlas.findRegions( regionsName )) {
                    // Handle unused tileIds
                    if ( region != null )
                    {
                        int tileId = firstgid + region.index;

                        if ( tileId >= firstgid && tileId <= lastgid )
                        {
                            addStaticTiledMapTile( tileSet, region, tileId, offsetX, offsetY );
                        }
                    }
                }
            }

            // Add tiles with individual image sources
            for ( Element tileElement :
            tileElements) {
                int          tileId = firstgid + tileElement.getIntAttribute( "id", 0 );
                TiledMapTile tile   = tileSet.getTile( tileId );

                if ( tile == null )
                {
                    Element imageElement = tileElement.getChildByName( "image" );

                    if ( imageElement != null )
                    {
                        String regionName = imageElement.getAttribute( "source" );
                        regionName = regionName.substring( 0, regionName.lastIndexOf( '.' ) );
                        AtlasRegion region = atlas.findRegion( regionName );

                        if ( region == null )
                            throw new GdxRuntimeException( "Tileset atlasRegion not found: " + regionName );

                        addStaticTiledMapTile( tileSet, region, tileId, offsetX, offsetY );
                    }
                }
            }
        }

        protected FileHandle GetAtlasFileHandle( FileHandle tmxFile )
        {
            Element properties = Root.GetChildByName( "properties" );

            String atlasFilePath = null;

            if ( properties != null )
            {
                for ( Element property :
                properties.GetChildrenByName( "property" )) {
                    String name = property.getAttribute( "name" );

                    if ( name.startsWith( "atlas" ) )
                    {
                        atlasFilePath = property.getAttribute( "value" );
                        break;
                    }
                }
            }

            if ( atlasFilePath == null )
            {
                throw new GdxRuntimeException( "The map is missing the 'atlas' property" );
            }
            else
            {
                FileHandle fileHandle = GetRelativeFileHandle( tmxFile, atlasFilePath );

                if ( !fileHandle.exists() )
                {
                    throw new GdxRuntimeException( "The 'atlas' file could not be found: '" + atlasFilePath + "'" );
                }

                return fileHandle;
            }
        }

        protected void SetTextureFilters( Texture.TextureFilter min, Texture.TextureFilter mag )
        {
            foreach ( Texture texture in trackedTextures)
            {
                texture.setFilter( min, mag );
            }

            trackedTextures.clear();
        }
    }
}
