// ############################################################

// ############################################################

namespace Lugh.Assets
{
    public class AssetLoaderParameters
    {
        /// <summary>
        /// Callback interface that will be invoked when
        /// the <see cref="AssetManager"/> loaded an asset.
        /// </summary>
        public interface ILoadedCallback
        {
            void FinishedLoading( AssetManager assetManager, string filename, Type type );
        }
    
        public ILoadedCallback LoadedCallback;
    }
}