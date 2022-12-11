// ############################################################

// ############################################################

namespace Lugh.Assets
{
    public interface IAssetErrorListener
    {
        void Error( AssetDescriptor asset, Exception exception );
    }
}