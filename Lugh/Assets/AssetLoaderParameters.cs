// ############################################################

// ############################################################

namespace Lugh.Assets;

public class AssetLoaderParameters
{
    public interface ILoadedCallback
    {
        void FinishedLoading( AssetManager assetManager, string filename, Type type );
    }
    
    public ILoadedCallback LoadedCallback;
}