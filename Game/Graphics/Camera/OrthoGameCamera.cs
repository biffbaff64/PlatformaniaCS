// ##################################################
#region using

using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

#endregion using
// ##################################################

namespace PlatformaniaCS.Game.Graphics.Camera;

public class OrthoGameCamera
{
    public string  Name             { get; set; }
    public Vector3 LerpVector       { get; set; }
    public bool    IsInUse          { get; set; }
    public bool    IsLerpingEnabled { get; set; }
    public float   DefaultZoom      { get; set; }

    public OrthoGameCamera( float sceneWidth, float sceneHeight, string name )
    {
        Name             = name;
        IsInUse          = false;
        IsLerpingEnabled = false;
        LerpVector       = new Vector3();
        DefaultZoom      = Zoom.DefaultZoom;
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