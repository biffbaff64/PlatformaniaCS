namespace Lugh.Maps.Tiled;

public interface ITiledMapTile
{
    public enum BlendMode
    {
        NONE,
        ALPHA
    }

    /// <returns></returns>
    public int GetId();

    /// <summary>
    /// </summary>
    public void SetId( int id );

    /// <returns>The BlendMode to use for rendering the tile.</returns>
    public BlendMode GetBlendMode();

    /// <summary>
    /// Sets the BlendMode to use for rendering the tile.
    /// </summary>
    /// <param name="blendMode">The blend mode to use for rendering the tile.</param>
    public void SetBlendMode( BlendMode blendMode );

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
