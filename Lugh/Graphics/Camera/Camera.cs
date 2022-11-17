namespace Lugh.Graphics.Camera;

public abstract class Camera
{
    // the position of the camera
    public Vector3 Position { get; set; } = new();

    // the unit length direction vector of the camera
    public Vector3 Direction { get; set; } = new(0, 0, -1);

    // the unit length up vector of the camera
    public Vector3 Up { get; set; } = new(0, 1, 0);

    // the projection matrix
    public Matrix4 Projection { get; set; } = new();

    // the view matrix
    public Matrix4 View { get; set; } = new();

    // the combined projection and view matrix
    public Matrix4 Combined { get; set; } = new();

    // the inverse combined projection and view matrix
    public Matrix4 InvProjectionView { get; set; } = new();

    // the near clipping plane distance, has to be positive
    public float Near { get; set; } = 1;

    // the far clipping plane distance, has to be positive
    public float Far { get; set; } = 100;

    // the viewport width
    public float ViewportWidth { get; set; } = 0;

    // the viewport height
    public float ViewportHeight { get; set; } = 0;

    // the frustum
    public Frustrum Frustrum { get; set; } = new();

    private Vector3 _tmpVec = new();
    private Ray     _ray    = new(new Vector3(), new Vector3());

    // ----------------------------------------------------
    // CODE
    // ----------------------------------------------------

    /// <summary>
    /// Recalculates the projection and view matrix of this camera and
    /// the <see cref="Frustrum"/> planes. Use this after you've manipulated
    /// any of the attributes of the camera.
    /// </summary>
    public abstract void Update( bool updateFrustrum = false );

    /// <summary>
    /// Recalculates the direction of the camera to look at the point (x, y, z).
    /// This function assumes the up vector is normalized.
    /// </summary>
    /// <param name="x">The x-coordinate of the point to look at.</param>
    /// <param name="y">The y-coordinate of the point to look at.</param>
    /// <param name="z">The z-coordinate of the point to look at.</param>
    public void lookAt( float x, float y, float z )
    {
        _tmpVec.Set( x, y, z ).sub( Position ).nor();

        if ( !_tmpVec.IsZero() )
        {
            float dot = _tmpVec.Dot( Up ); // up and direction must ALWAYS be orthonormal vectors

            if ( Math.Abs( dot - 1 ) < 0.000000001f )
            {
                // Collinear
                Up.Set( Direction ).scl( -1 );
            }
            else if ( Math.Abs( dot + 1 ) < 0.000000001f )
            {
                // Collinear opposite
                up.set( direction );
            }

            Direction.Set( _tmpVec );
            NormalizeUp();
        }
    }
}