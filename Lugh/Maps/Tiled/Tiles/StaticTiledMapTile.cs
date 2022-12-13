// ##################################################

// ##################################################

namespace Lugh.Maps.Tiled.Tiles;

public class StaticTiledMapTile : ITiledMapTile
{
    public StaticTiledMapTile( TextureRegion textureRegion )
    {
    }

    public int GetId()
    {
        return 0;
    }

    public void SetId( int id )
    {
    }

    public ITiledMapTile.BlendMode GetBlendMode()
    {
        return ITiledMapTile.BlendMode.NONE;
    }

    public void SetBlendMode( ITiledMapTile.BlendMode blendMode )
    {
    }

    public TextureRegion GetTextureRegion()
    {
        return null;
    }

    public void SetTextureRegion( TextureRegion textureRegion )
    {
    }

    public float GetOffsetX()
    {
        return 0;
    }

    public void SetOffsetX( float offsetX )
    {
    }

    public float GetOffsetY()
    {
        return 0;
    }

    public void SetOffsetY( float offsetY )
    {
    }

    public MapProperties GetProperties()
    {
        return null;
    }

    public MapObjects GetObjects()
    {
        return null;
    }
}
