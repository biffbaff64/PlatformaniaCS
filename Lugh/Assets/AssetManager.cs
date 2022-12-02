// ############################################################

// ############################################################

using Lugh.Assets.Resolvers;

using Microsoft.Xna.Framework.Media;

using MonoGame.Extended.BitmapFonts;

namespace Lugh.Assets;

public class AssetManager
{
//    private ObjectMap< Type, ObjectMap< string, RefCountedContainer > > _assets;
    private ObjectMap< string, Type >                                   _assetTypes;
    private ObjectMap< string, List< string > >                         _assetDependencies;
    private ObjectSet< string >                                         _injected;
    private ObjectMap< Type, ObjectMap< string, AssetLoader > >         _loaders;

    private List< AssetDescriptor >  _loadQueue;
//    private AsyncExecutor            _executor;
    private List< AssetLoadingTask > _tasks;
    private IAssetErrorListener      _listener;

    public IFileHandleResolver Resolver { get; private set; }

    private int _loaded;
    private int _toLoad;
    private int _peakTasks;

    public AssetManager() : this( new InternalFileHandleResolver() )
    {
    }

    public AssetManager( IFileHandleResolver resolver )
        : this( resolver, true )
    {
    }

    public AssetManager( IFileHandleResolver resolver, bool defaultLoaders = true )
    {
        this.Resolver = resolver;

        if ( defaultLoaders )
        {
//            SetLoader( typeof( BitmapFont ),     new BitmapFontLoader( resolver ) );
//            SetLoader( typeof( Song ),           new MusicLoader( resolver ) );
//            SetLoader( typeof( Pixmap ),         new PixmapLoader( resolver ) );
//            SetLoader( typeof( Sound ),          new SoundLoader( resolver ) );
//            SetLoader( typeof( TextureAtlas ),   new TextureAtlasLoader( resolver ) );
//            SetLoader( typeof( Texture ),        new TextureLoader( resolver ) );
//            SetLoader( typeof( Skin ),           new SkinLoader( resolver ) );
//            SetLoader( typeof( ParticleEffect ), new ParticleEffectLoader( resolver ) );
//            SetLoader( typeof( I18NBundle ),     new I18NBundleLoader( resolver ) );
//            SetLoader( typeof( ShaderProgram ),  new ShaderProgramLoader( resolver ) );
//            SetLoader( typeof( Cubemap ),        new CubemapLoader( resolver ) );

//            SetLoader( typeof( PolygonRegion ),    new PolygonRegionLoader(resolver));
//            SetLoader(Model,                 ".g3dj", new G3dModelLoader(new JsonReader(),   resolver));
//            SetLoader(Model,                 ".g3db", new G3dModelLoader(new UBJsonReader(), resolver));
//            SetLoader(Model,                 ".obj",  new ObjLoader(resolver));
        }

//        _executor = new AsyncExecutor( 1, "AssetManager" );
    }

    public void SetLoader( Type assetType, AssetLoader< T, P > loader )
    {
    }
}
