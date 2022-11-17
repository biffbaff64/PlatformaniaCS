// ##################################################

using MonoGame.Extended.ViewportAdapters;
using PlatformaniaCS.Game.Core;
using OrthographicCamera = MonoGame.Extended.OrthographicCamera;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

// ##################################################

namespace PlatformaniaCS.Game.Graphics.Camera;

public class OrthoGameCamera
{
    public ViewportAdapter    ViewportAdapter  { get; set; }
    public OrthographicCamera Camera           { get; set; }
    public string             Name             { get; set; }
    public Vector3            LerpVector       { get; set; }
    public bool               IsInUse          { get; set; }
    public bool               IsLerpingEnabled { get; set; }
    public float              DefaultZoom      { get; set; }

    public OrthoGameCamera( float sceneWidth, float sceneHeight, string name )
    {
        Name             = name;
        IsInUse          = false;
        IsLerpingEnabled = false;
        LerpVector       = new Vector3();
        DefaultZoom      = Zoom.DefaultZoom;

        ViewportAdapter = new BoxingViewportAdapter
        (
            App.MainGame.Window,
            App.MainGame.GraphicsDevice,
            ( int )( sceneWidth  * Gfx.PPM ),
            ( int )( sceneHeight * Gfx.PPM )
        );

        Camera = new OrthographicCamera( ViewportAdapter );
    }

    public void SetPosition( Vector2 position )
    {
        if ( IsInUse )
        {
        }
    }

    public void SetZoomDefault( float zoom )
    {
        DefaultZoom = zoom;
    }
}