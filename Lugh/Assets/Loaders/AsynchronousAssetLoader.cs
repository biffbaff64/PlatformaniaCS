// ############################################################

// ############################################################

using Lugh.Files;

namespace Lugh.Assets
{
    public class AsynchronousAssetLoader< T, TP >  : AssetLoader where TP : AssetLoaderParameters
    {
        protected AsynchronousAssetLoader( FileHandleResolver resolver ) : base( resolver )
        {
        }
    }
}
