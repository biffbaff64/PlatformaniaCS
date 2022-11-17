namespace Lugh.Maths;

public class Vector3 : IVector< Vector3 >
{
    /** the x-component of this vector **/
    public float X;

    /** the y-component of this vector **/
    public float Y;

    /** the z-component of this vector **/
    public float Z;

    public static Vector3 XC   = new Vector3( 1, 0, 0 );
    public static Vector3 YC   = new Vector3( 0, 1, 0 );
    public static Vector3 ZC   = new Vector3( 0, 0, 1 );
    public static Vector3 Zero = new Vector3( 0, 0, 0 );

    /// <summary>
    /// Creates a vector at 0, 0, 0.
    /// </summary>
    public Vector3()
    {
        Set( 0, 0, 0 );
    }

    /// <summary>
    /// Creates a vector with the given components.
    /// </summary>
    public Vector3( float x, float y, float z )
    {
        Set( x, y, z );
    }

    /// <summary>
    /// Sets the vector to the given components.
    /// </summary>
    public Vector3 Set( float x, float y, float z )
    {
        this.X = x;
        this.Y = y;
        this.Z = z;

        return this;
    }

    public Vector3 Cpy() => null;

    public float Len() => 0;

    public float Len2() => 0;

    public Vector3 Limit( float limit ) => null;

    public Vector3 Limit2( float limit2 ) => null;

    public Vector3 SetLength( float len ) => null;

    public Vector3 SetLength2( float len2 ) => null;

    public Vector3 Clamp( float min, float max ) => null;

    public Vector3 Set( Vector3 v ) => null;

    public Vector3 Sub( Vector3 v ) => null;

    public Vector3 Nor() => null;

    public Vector3 Add( Vector3 v ) => null;

    public float Dot( Vector3 v ) => 0;

    public Vector3 Scl( float scalar ) => null;

    public Vector3 Scl( Vector3 v ) => null;

    public float Dst( Vector3 v ) => 0;

    public float Dst2( Vector3 v ) => 0;

    public Vector3 Lerp( Vector3 target, float alpha ) => null;

    public Vector3 Interpolate( Vector3 target, float alpha, Interpolation interpolator ) => null;

    public Vector3 SetToRandomDirection() => null;

    public bool IsUnit() => false;

    public bool IsUnit( float margin ) => false;

    public bool IsZero() => false;

    public bool IsZero( float margin ) => false;

    public bool IsOnLine( Vector3 other, float epsilon ) => false;

    public bool IsOnLine( Vector3 other ) => false;

    public bool IsCollinear( Vector3 other, float epsilon ) => false;

    public bool IsCollinear( Vector3 other ) => false;

    public bool IsCollinearOpposite( Vector3 other, float epsilon ) => false;

    public bool IsCollinearOpposite( Vector3 other ) => false;

    public bool IsPerpendicular( Vector3 other ) => false;

    public bool IsPerpendicular( Vector3 other, float epsilon ) => false;

    public bool HasSameDirection( Vector3 other ) => false;

    public bool HasOppositeDirection( Vector3 other ) => false;

    public bool EpsilonEquals( Vector3 other, float epsilon ) => false;

    public Vector3 MulAdd( Vector3 v, float scalar ) => null;

    public Vector3 MulAdd( Vector3 v, Vector3 mulVec ) => null;

    public Vector3 SetZero() => null;
}