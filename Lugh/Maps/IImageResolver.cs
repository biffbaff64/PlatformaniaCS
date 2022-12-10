using Lugh.Assets;

namespace Lugh.Maps;

public interface IImageResolver
{
    TextureRegion GetImage( string name );
}

public class DirectImageResolver : IImageResolver
{
    private ObjectMap< string, Texture > _images;

    public DirectImageResolver( ObjectMap< string, Texture > images )
    {
        _images = images;
    }

    public TextureRegion GetImage( string name )
    {
        return null;
    }
}

public class AssetManagerImageResolver : IImageResolver
{
    private readonly AssetManager _assetManager;

    public AssetManagerImageResolver( AssetManager assetManager )
    {
        this._assetManager = assetManager;
    }

    public TextureRegion GetImage( string name )
    {
        return new TextureRegion( _assetManager.Get( name, typeof( Texture ) ) );
    }
}

public class TextureAtlasImageResolver : IImageResolver
{
    private TextureAtlas _atlas;

    public TextureAtlasImageResolver( TextureAtlas atlas )
    {
        this._atlas = atlas;
    }

    public TextureRegion GetImage( string name )
    {
        return _atlas.FindRegion( name );
    }
}
