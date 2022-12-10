// ########################################################

using System.Numerics;

using Lugh.Maths.Collision;

using Scene2DCS.Utils;

using Rectangle = System.Drawing.Rectangle;

// ########################################################

namespace Lugh.Graphics.Camera;

/// <summary>
/// Manages a <see cref="Graphics.Camera.Camera"/> and determines how world
/// coordinates are mapped to and from the screen.
/// </summary>
public abstract class Viewport
{
    public Camera Camera       { get; set; }
    public float  WorldWidth   { get; set; }
    public float  WorldHeight  { get; set; }
    public int    ScreenX      { get; set; }
    public int    ScreenY      { get; set; }
    public int    ScreenWidth  { get; set; }
    public int    ScreenHeight { get; set; }

    private Vector3 _tmp = new Vector3();

    /// <summary>
    /// Applies the viewport to the camera.
    /// </summary>
    /// <param name="centerCamera">
    ///     If true, the camera position is set to the center of the world.
    /// </param>
    public void Apply( bool centerCamera = false )
    {
//        HdpiUtils.glViewport( _screenX, _screenY, _screenWidth, _screenHeight );

        Camera.ViewportWidth  = WorldWidth;
        Camera.ViewportHeight = WorldHeight;

        if ( centerCamera )
        {
            Camera.Position = VectorUtils.Set( WorldWidth / 2, WorldHeight / 2, 0 );
        }

        Camera.Update();
    }

    /// <summary>
    /// Configures this viewport's screen bounds using the specified
    /// screen size and calls <see cref="Apply"/>.
    /// </summary>
    /// <param name="screenWidth">The width of the viewport screen bounds.</param>
    /// <param name="screenHeight">The height of the viewport screen bounds.</param>
    /// <param name="centerCamera">
    ///     If true, the camera position is set to the center of the world.
    /// </param>
    /// <remarks>The default implementation only calls <see cref="Apply"/>.</remarks>
    public void Update( int screenWidth, int screenHeight, bool centerCamera = false )
    {
        Apply( centerCamera );
    }

    /// <summary>
    /// Transforms the specified world coordinate to screen coordinates.
    /// </summary>
    /// <returns>
    ///     The vector that was passed in, transformed to world coordinates.
    /// </returns>
    public Vector2 Project( Vector2 worldCoords )
    {
        _tmp.X = worldCoords.X;
        _tmp.Y = worldCoords.Y;
        _tmp.Z = 1;

        Camera.Project( _tmp, ScreenX, ScreenY, ScreenWidth, ScreenHeight );

        worldCoords.X = _tmp.X;
        worldCoords.Y = _tmp.Y;

        return worldCoords;
    }

    /// <summary>
    /// Transforms the specified world coordinate to screen coordinates.
    /// </summary>
    /// <returns>
    ///     The vector that was passed in, transformed to world coordinates.
    /// </returns>
    public Vector3 Project( Vector3 worldCoords )
    {
        Camera.Project( worldCoords, ScreenX, ScreenY, ScreenWidth, ScreenHeight );

        return worldCoords;
    }

    /// <summary>
    /// Transforms the specified screen coordinate to world coordinates.
    /// </summary>
    /// <returns>
    ///     The vector that was passed in, transformed to world coordinates.
    /// </returns>
    public Vector2 Unproject( Vector2 screenCoords )
    {
        _tmp.X = screenCoords.X;
        _tmp.Y = screenCoords.Y;
        _tmp.Z = 1;

        Camera.Unproject( _tmp, ScreenX, ScreenY, ScreenWidth, ScreenHeight );

        screenCoords.X = _tmp.X;
        screenCoords.Y = _tmp.Y;

        return screenCoords;
    }

    /// <summary>
    /// Transforms the specified screen coordinate to world coordinates.
    /// </summary>
    /// <returns>
    ///     The vector that was passed in, transformed to world coordinates.
    /// </returns>
    public Vector3 Unproject( Vector3 screenCoords )
    {
        Camera.Unproject( screenCoords, ScreenX, ScreenY, ScreenWidth, ScreenHeight );

        return screenCoords;
    }

    /// <summary>
    ///     <see cref="Graphics.Camera.Camera"/>
    /// </summary>
    public Ray GetPickRay( float screenX, float screenY )
    {
        return Camera.GetPickRay( screenX, screenY, ScreenX, ScreenY, ScreenWidth, ScreenHeight );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="batchTransform"></param>
    /// <param name="area"></param>
    /// <param name="scissor"></param>
    public void CalculateScissors( Matrix4 batchTransform, Rectangle area, Rectangle scissor )
    {
        ScissorStack.CalculateScissors( Camera, ScreenX, ScreenY, ScreenWidth, ScreenHeight, batchTransform, area, scissor );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="worldCoords"></param>
    /// <param name="transformMatrix"></param>
    /// <returns></returns>
    public Vector2 ToScreenCoordinates( Vector2 worldCoords, Matrix4 transformMatrix )
    {
        _tmp.X = worldCoords.X;
        _tmp.Y = worldCoords.Y;
        _tmp.Z = 0;

        _tmp = VectorUtils.Mul( _tmp, transformMatrix );

        Camera.Project( _tmp, ScreenX, ScreenY, ScreenWidth, ScreenHeight );

        _tmp.Y = ( LughSystem.GetDisplayHeight() - _tmp.Y );

        worldCoords.X = _tmp.X;
        worldCoords.Y = _tmp.Y;

        return worldCoords;
    }

    public void SetWorldSize( float worldWidth, float worldHeight )
    {
    }

    public void SetScreenPosition( int screenX, int screenY )
    {
        this.ScreenX = screenX;
        this.ScreenY = screenY;
    }

    public void SetScreenSize( int screenWidth, int screenHeight )
    {
        this.ScreenWidth  = screenWidth;
        this.ScreenHeight = screenHeight;
    }

    public void SetScreenBounds( int screenX, int screenY, int screenWidth, int screenHeight )
    {
        this.ScreenX      = screenX;
        this.ScreenY      = screenY;
        this.ScreenWidth  = screenWidth;
        this.ScreenHeight = screenHeight;
    }


    /** Returns the left gutter (black bar) width in screen coordinates. */
    public int GetLeftGutterWidth()
    {
        return ScreenX;
    }

    /** Returns the right gutter (black bar) x in screen coordinates. */
    public int GetRightGutterX()
    {
        return ScreenX + ScreenWidth;
    }

    /** Returns the right gutter (black bar) width in screen coordinates. */
    public int GetRightGutterWidth()
    {
        return LughSystem.GetDisplayWidth() - ( ScreenX + ScreenWidth );
    }

    /** Returns the bottom gutter (black bar) height in screen coordinates. */
    public int GetBottomGutterHeight()
    {
        return ScreenY;
    }

    /** Returns the top gutter (black bar) y in screen coordinates. */
    public int GetTopGutterY()
    {
        return ScreenY + ScreenHeight;
    }

    /** Returns the top gutter (black bar) height in screen coordinates. */
    public int GetTopGutterHeight()
    {
        return LughSystem.GetDisplayHeight() - ( ScreenY + ScreenHeight );
    }
}