// ############################################################

using Lugh.Assets.Resolvers;

using TextureFilter = Lugh.Graphics.TextureFilter;

// ############################################################

namespace Lugh.Assets;

public class TextureLoader : AsynchronousAssetLoader< Texture, TextureLoader.TextureParameter >
{
    public TextureLoader( IFileHandleResolver resolver )
    {
    }

    public class TextureParameter : AssetLoaderParameters
    {
        // the format of the final Texture. Uses the source images format if null
        public Pixmap.Format Format = Pixmap.Format.None;

        // whether to generate mipmaps
        public bool GenMipMaps = false;

        // The texture to put the TextureData in, optional.
        public Texture Texture = null;

        // TextureData for textures created on the fly, optional.
        // When set, all format and genMipMaps are ignored.
        public TextureData TextureData = null;

        public TextureFilter MinFilter = TextureFilter.Nearest;
        public TextureFilter MagFilter = TextureFilter.Nearest;
        public TextureWrap   WrapU     = TextureWrap.ClampToEdge;
        public TextureWrap   WrapV     = TextureWrap.ClampToEdge;
    }
}
