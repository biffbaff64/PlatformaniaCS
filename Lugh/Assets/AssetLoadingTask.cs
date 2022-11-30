// ############################################################

// ############################################################

namespace Lugh.Assets;

/// <summary>
/// Responsible for loading an asset through an <see cref="AssetLoader"/>
/// based on an <see cref="AssetDescriptor"/>.
/// </summary>
public class AssetLoadingTask
{
    private AssetManager    _assetManager;
    private AssetDescriptor _assetDescriptor;
    private AssetLoader     _assetLoader;
//    final                   AsyncExecutor executor;
    private long            _startTime;

    private volatile bool                    _asyncDone;
    private volatile bool                    _dependenciesLoaded;
    private volatile List< AssetDescriptor > _dependencies;
//    volatile         AsyncResult<Void>       depsFuture;
//    volatile         AsyncResult<Void>       loadFuture;
    private volatile object _asset;
    private volatile bool   _cancel;

    public AssetLoadingTask()
    {
    }
}
