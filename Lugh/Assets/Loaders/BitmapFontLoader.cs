// ############################################################

using Lugh.Assets.Resolvers;

using TextureFilter = Lugh.Graphics.TextureFilter;

// ############################################################

namespace Lugh.Assets;

public class BitmapFontLoader : AsynchronousAssetLoader< BitmapFont, BitmapFontLoader.BitmapFontParameter >
{
    public BitmapFontLoader( IFileHandleResolver resolver )
    {
    }
    
    public class BitmapFontParameter : AssetLoaderParameters
    {
        // Flips the font vertically if true. Defaults to false.
        public bool Flip { get; set; }

        // Generates mipmaps for the font if true. Defaults to false.
        public bool GenMipMaps { get; set; }

        // The TextureFilter to use when scaling down the BitmapFont. Defaults to TextureFilter#Nearest.
        public TextureFilter MinFilter { get; set; }

        // The TextureFilter to use when scaling up the BitmapFont. Defaults to TextureFilter#Nearest.
        public TextureFilter MagFilter { get; set; }

        // Optional BitmapFontData to be used instead of loading the Texture directly.
        // Use this if your font is embedded in a Skin.
        public BitmapFontData BitmapFontData { get; set; }

        // The name of the TextureAtlas to load the BitmapFont itself from.
        //  If null, will look for a separate image.
        public string AtlasName { get; set; }
    }
}
