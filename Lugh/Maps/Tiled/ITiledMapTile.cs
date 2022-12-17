namespace Lugh.Maps.Tiled
{
    public interface ITiledMapTile
    {
        public enum BlendModes
        {
            NONE,
            ALPHA
        }

        public int        ID        { get; set; }
        public BlendModes BlendMode { get; set; }

        /// <returns>The texture region used to render the tile.</returns>
        public TextureRegion GetTextureRegion();

        /// <summary>
        /// Sets the texture region used to render the tile
        /// </summary>
        public void SetTextureRegion( TextureRegion textureRegion );

        /// <returns>the amount to offset the x position when rendering the tile</returns>
        public float GetOffsetX();

        /// <summary>
        /// Set the amount to offset the x position when rendering the tile.
        /// </summary>
        public void SetOffsetX( float offsetX );

        /// <returns>the amount to offset the y position when rendering the tile</returns>
        public float GetOffsetY();

        /// <summary>
        /// Set the amount to offset the y position when rendering the tile.
        /// </summary>
        public void SetOffsetY( float offsetY );

        /// <returns>The propereties set for the tile.</returns>
        public MapProperties GetProperties();

        /// <returns>Collection of objects contained in the tile.</returns>
        public MapObjects GetObjects();
    }
}
