// ##################################################

// ##################################################

using Lugh.Collections;

namespace Lugh.Maps.Tiled.Tiles
{
    public class AnimatedTiledMapTile : ITiledMapTile
    {
        public int                      ID        { get; set; }
        public ITiledMapTile.BlendModes BlendMode { get; set; } = ITiledMapTile.BlendModes.NONE;

        public AnimatedTiledMapTile( IntArray intervals, List< StaticTiledMapTile > staticTiles )
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
}
