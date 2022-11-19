// ##################################################

using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.ViewportAdapters;

using PlatformaniaCS.Game.Core;

using OrthographicCamera = MonoGame.Extended.OrthographicCamera;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

// ##################################################

namespace PlatformaniaCS.Game.Graphics.Camera;

public class OrthoGameCamera
{
    public RenderTarget2D RenderTarget2D   { get; set; }
    public string         Name             { get; set; }
    public Vector3        LerpVector       { get; set; }
    public bool           IsInUse          { get; set; }
    public bool           IsLerpingEnabled { get; set; }
    public float          DefaultZoom      { get; set; }

    public OrthoGameCamera( float sceneWidth, float sceneHeight, string name )
    {
        Name             = name;
        IsInUse          = false;
        IsLerpingEnabled = false;
        LerpVector       = new Vector3();
        DefaultZoom      = Zoom.DefaultZoom;

        RenderTarget2D = new RenderTarget2D
            (
             App.MainGame.GraphicsDevice,
             ( int )sceneWidth,
             ( int )sceneHeight,
             false,
             App.MainGame.GraphicsDevice.PresentationParameters.BackBufferFormat,
             DepthFormat.Depth24
            );
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