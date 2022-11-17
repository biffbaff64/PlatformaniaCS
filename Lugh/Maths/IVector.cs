namespace Lugh.Maths;

/// <summary>
/// Encapsulates a general vector. Allows chaining operations by
/// returning a reference to itself in all modification methods.
/// </summary>
public interface IVector< T >
{
    /// <summary>
    /// return a copy of this vector
    /// </summary>
    T Cpy();

    float Len();

    float Len2();

    T Limit( float limit );

    T Limit2( float limit2 );

    T SetLength( float len );

    T SetLength2( float len2 );

    T Clamp( float min, float max );

    T Set( T v );

    T Sub( T v );

    T Nor();

    T Add( T v );

    float Dot( T v );

    T Scl( float scalar );

    T Scl( T v );

    float Dst( T v );

    float Dst2( T v );

    T Lerp( T target, float alpha );

    T Interpolate( T target, float alpha, Interpolation interpolator );

    T SetToRandomDirection();

    bool IsUnit();

    bool IsUnit( float margin );

    bool IsZero();

    bool IsZero( float margin );

    bool IsOnLine( T other, float epsilon );

    bool IsOnLine( T other );

    bool IsCollinear( T other, float epsilon );

    bool IsCollinear( T other );

    bool IsCollinearOpposite( T other, float epsilon );

    bool IsCollinearOpposite( T other );

    bool IsPerpendicular( T other );

    bool IsPerpendicular( T other, float epsilon );

    bool HasSameDirection( T other );

    bool HasOppositeDirection( T other );

    bool EpsilonEquals( T other, float epsilon );

    T MulAdd( T v, float scalar );

    T MulAdd( T v, T mulVec );

    T SetZero();
}