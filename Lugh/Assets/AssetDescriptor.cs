// ############################################################

using System.Diagnostics.CodeAnalysis;
using System.Text;

// ############################################################

namespace Lugh.Assets
{
    /// <summary>
    /// Describes an asset to be loaded by its filename, type and
    /// <see cref="AssetLoaderParameters"/>. Instances of this are used
    /// in <see cref="AssetLoadingTask"/> to load the actual asset.
    /// </summary>
    [SuppressMessage( "ReSharper", "IntroduceOptionalParameters.Global" )]
    public class AssetDescriptor
    {
        public string                FileName { get; set; }
        public Type                  Type     { get; set; }
        public AssetLoaderParameters Params   { get; set; }

        public AssetDescriptor( string filename, Type assetType )
            : this( filename, assetType, null )
        {
        }

        public AssetDescriptor( string fileName, Type assetType, AssetLoaderParameters parameters )
        {
            this.FileName = fileName;
            this.Type     = assetType;
            this.Params   = parameters;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append( FileName );
            sb.Append( ", " );
            sb.Append( Type.Name );

            return sb.ToString();
        }
    }
}
