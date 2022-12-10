using System.Numerics;

using Lugh.Maths.Collision;

namespace Lugh.Graphics.Camera;

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
        _tmpVec = VectorUtils.Set( x, y, z );

        Vector3.Subtract( _tmpVec, Position );

        _tmpVec = Vector3.Normalize( _tmpVec );

        if ( !_tmpVec.Equals( Vector3.Zero ) )
        {
            var dot = Vector3.Dot( _tmpVec, Up ); // up and direction must ALWAYS be orthonormal vectors

            if ( Math.Abs( dot - 1 ) < 0.000000001f )
            {
                // Collinear
                Up = VectorUtils.Set( Direction );
                Up = VectorUtils.Scl( Up, -1 );
            }
            else if ( Math.Abs( dot + 1 ) < 0.000000001f )
            {
                // Collinear opposite
                Up = VectorUtils.Set( Direction );
            }

            Direction = VectorUtils.Set( _tmpVec );

            NormalizeUp();
        }
    }

    /// <summary>
    /// Recalculates the direction of the camera to look at the point (x, y, z).
    /// </summary>
    /// <param name="target">The target point to look at.</param>
    public void LookAt( Vector3 target )
    {
        this.LookAt( target.X, target.Y, target.Z );
    }

    /// <summary>
    /// Normalizes the up vector by first calculating the right vector
    /// via a cross product between direction and up, and then recalculating
    /// the up vector via a cross product between right and direction. 
    /// </summary>
    public void NormalizeUp()
    {
        _tmpVec = VectorUtils.Set( Direction );

        Up = VectorUtils.Set( _tmpVec );
        Up = Vector3.Cross( Up, Direction );
        Up = Vector3.Normalize( Up );
    }

    /// <summary>
    /// Rotates the direction and up vector of this camera by the given
    /// angle around the given axis. The direction and up vector will
    /// not be 'orthogonalized'.
    /// </summary>
    /// <param name="angle">The angle.</param>
    /// <param name="axisX">The x-component of the axis.</param>
    /// <param name="axisY">The y-component of the axis.</param>
    /// <param name="axisZ">The z-component of the axis.</param>
    public void Rotate( float angle, float axisX, float axisY, float axisZ )
    {
    }

    /// <summary>
    /// Rotates the direction and up vector of this camera by the given
    /// angle around the given axis. The direction and up vector will
    /// not be 'orthogonalized'.
    /// </summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle, in degrees.</param>
    public void Rotate( Vector3 axis, float angle )
    {
    }

    /// <summary>
    /// Rotates the direction and up vector of this camera by the given
    /// rotation matrix. The direction and up vector will not be 'orthogonalized'.
    /// </summary>
    /// <param name="transform">The rotation matrix.</param>
    public void Rotate( Matrix4 transform )
    {
    }

    /// <summary>
    /// Rotates the direction and up vector of this camera by the given
    /// Quaternion. The direction and up vector will not be 'orthogonalized'.
    /// </summary>
    /// <param name="quat"></param>
    public void Rotate( Quaternion quat )
    {
    }

    /// <summary>
    /// Rotates the direction and up vector of this camera by the given
    /// angle around the given axis, with the axis attached to given point.
    /// The direction and up vector will not be 'orthogonalized'.
    /// </summary>
    /// <param name="point">The point to attach the axis to.</param>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle, in degrees.</param>
    public void RotateAround( Vector3 point, Vector3 axis, float angle )
    {
    }

    /// <summary>
    /// Transform the position, direction and up vector by the given matrix.
    /// </summary>
    public void Transform( Matrix4 transform )
    {
    }

    /// <summary>
    /// Moves the camera by the given amount on each axis.
    /// </summary>
    /// <param name="x">The displacement on the x-axis.</param>
    /// <param name="y">The displacement on the y-axis.</param>
    /// <param name="z">The displacement on the z-axis.</param>
    public void Translate( float x, float y, float z )
    {
    }

    /// <summary>
    /// Moves the camera by the given vector.
    /// </summary>
    /// <param name="vector3">The displacement vector.</param>
    public void Translate( Vector3 vector3 )
    {
    }

    /// <summary>
    /// Function to translate a point given in screen coordinates to
    /// world space. It's the same as GLU gluUnProject, but does not
    /// rely on OpenGL. The x- and y-coordinate of vec are assumed to
    /// be in screen coordinates (origin is the top left corner, y
    /// pointing down, x pointing to the right) as reported by the touch
    /// methods in Input. A z-coordinate of 0 will return a point on the
    /// near plane, a z-coordinate of 1 will return a point on the far
    /// plane. This method allows you to specify the viewport position#
    /// and dimensions in the coordinate system expected by
    /// GL20.glViewport(int, int, int, int), with the origin in the bottom
    /// left corner of the screen.
    /// </summary>
    /// <param name="screenCoords"></param>
    /// <param name="viewportX"></param>
    /// <param name="viewportY"></param>
    /// <param name="viewportWidth"></param>
    /// <param name="viewportHeight"></param>
    /// <returns></returns>
    public Vector3 Unproject
        ( Vector3 screenCoords, float viewportX, float viewportY, float viewportWidth, float viewportHeight )
    {
        return default;
    }

    public void Unproject( Vector3 tmp, int screenX, int screenY, int screenWidth, int screenHeight )
    {
    }

    public void Project( Vector3 tmp, int screenX, int screenY, int screenWidth, int screenHeight )
    {
    }

    public Ray GetPickRay( float screenX, float screenY, int i, int screenY1, int screenWidth, int screenHeight )
    {
        return default;
    }
}
