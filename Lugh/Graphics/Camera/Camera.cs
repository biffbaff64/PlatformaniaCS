namespace Lugh.Graphics.Camera;

using Vector3 = Maths.Vector3;

public abstract class Camera
{
    // the position of the camera
    public Vector3 Position { get; set; } = new Vector3();

    // the unit length direction vector of the camera
    public Vector3 Direction { get; set; } = new Vector3( 0, 0, -1 );

    // the unit length up vector of the camera
    public Vector3 Up { get; set; } = new Vector3( 0, 1, 0 );

    // the projection matrix
    public Matrix4 Projection { get; set; } = new Matrix4();

    // the view matrix
    public Matrix4 View { get; set; } = new Matrix4();

    // the combined projection and view matrix
    public Matrix4 Combined { get; set; } = new Matrix4();

    // the inverse combined projection and view matrix
    public Matrix4 InvProjectionView { get; set; } = new Matrix4();

    // the near clipping plane distance, has to be positive
    public float Near { get; set; } = 1;

    // the far clipping plane distance, has to be positive
    public float Far { get; set; } = 100;

    // the viewport width
    public float ViewportWidth { get; set; } = 0;

    // the viewport height
    public float ViewportHeight { get; set; } = 0;

    // the frustum
    public Frustrum Frustrum { get; set; } = new Frustrum();

    private Vector3 _tmpVec = new Vector3();
    private Ray     _ray    = new Ray( new Vector3(), new Vector3() );

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
    public void LookAt( float x, float y, float z )
    {
        _tmpVec.Set( x, y, z ).Sub( Position ).Nor();

        if ( !_tmpVec.IsZero() )
        {
            float dot = _tmpVec.Dot( Up ); // up and direction must ALWAYS be orthonormal vectors

            if ( Math.Abs( dot - 1 ) < 0.000000001f )
            {
                // Collinear
                Up.Set( Direction ).Scl( -1 );
            }
            else if ( Math.Abs( dot + 1 ) < 0.000000001f )
            {
                // Collinear opposite
                Up.X = Direction.X;
                Up.Y = Direction.Y;
                Up.Z = Direction.Z;
            }

            Direction.Set( _tmpVec );
            NormalizeUp();
        }
    }
}