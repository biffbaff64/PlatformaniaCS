// ############################################################

// ############################################################

using Lugh.Assets.Resolvers;
using Lugh.Files;

namespace Lugh.Assets
{
    public class AssetLoader : AssetLoaderParameters
    {
        private IFileHandleResolver _resolver;

        protected AssetLoader( FileHandleResolver resolver )
        {
        }
    }
}