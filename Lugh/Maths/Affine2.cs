using System.Runtime.Serialization;

namespace Lugh.Maths;

public class Affine2 : ISerializable
{
    private readonly long _serialVersionUID = 1524569123485049187L;

    public float M00 { get; set; } = 1;
    public float M01 { get; set; } = 0;
    public float M02 { get; set; } = 0;
    public float M10 { get; set; } = 0;
    public float M11 { get; set; } = 1;
    public float M12 { get; set; } = 0;

    public Affine2()
    {
    }

    /// <summary>
    /// Constructs a matrix from the given affine matrix.
    /// </summary>
    /// <param name="other">The affine matrix to copy.</param>
    public Affine2( Affine2 other )
    {
        Set( other );
    }

    /// <summary>
    /// Sets this matrix to the identity matrix.
    /// </summary>
    /// <returns>This matrix for the purpose of chaining operations.</returns>
    public Affine2 SetToIdm()
    {
        M00 = 1;
        M01 = 0;
        M02 = 0;
        M10 = 0;
        M11 = 1;
        M12 = 0;

        return this;
    }

    /// <summary>
    /// Copies the values from the provided affine matrix to this matrix.
    /// </summary>
    /// <param name="other">The affine matrix to copy.</param>
    /// <returns>This matrix for the purposes of chaining.</returns>
    public Affine2 Set( Affine2 other )
    {
        M00 = other.M00;
        M01 = other.M01;
        M02 = other.M02;
        M10 = other.M10;
        M11 = other.M11;
        M12 = other.M12;

        return this;
    }

    /// <summary>
    /// Copies the values from the provided matrix to this matrix.
    /// </summary>
    /// <param name="matrix">The matrix to copy, assumed to be an affine transformation.</param>
    /// <returns>This matrix for the purposes of chaining.</returns>
    public Affine2 Set( Matrix3 matrix )
    {
        float[] other = matrix.Val;

        M00 = other[ Matrix3.M00 ];
        M01 = other[ Matrix3.M01 ];
        M02 = other[ Matrix3.M02 ];
        M10 = other[ Matrix3.M10 ];
        M11 = other[ Matrix3.M11 ];
        M12 = other[ Matrix3.M12 ];

        return this;
    }

    /// <summary>
    /// Copies the 2D transformation components from the provided 4x4 matrix. The values are mapped as follows:
    ///
    ///      [  M00  M01  M03  ]
    ///      [  M10  M11  M13  ]
    ///      [   0    0    1   ]
    /// 
    /// </summary>
    /// <param name="matrix">
    /// The source matrix, assumed to be an affine transformation within XY plane.
    /// This matrix will not be modified.
    /// </param>
    /// <returns>This matrix for the purpose of chaining operations.</returns>
    public Affine2 Set( Matrix4 matrix )
    {
        float[] other = matrix.Val;

        M00 = other[ Matrix4.M00 ];
        M01 = other[ Matrix4.M01 ];
        M02 = other[ Matrix4.M03 ];
        M10 = other[ Matrix4.M10 ];
        M11 = other[ Matrix4.M11 ];
        M12 = other[ Matrix4.M13 ];

        return this;
    }

    /// <summary>
    /// Sets this matrix to a translation matrix.
    /// </summary>
    /// <param name="x">The translation in x.</param>
    /// <param name="y">The translation in y.</param>
    /// <returns>This matrix for the purpose of chaining operations.</returns>
    public Affine2 SetToTranslation( float x, float y )
    {
        M00 = 1;
        M01 = 0;
        M02 = x;
        M10 = 0;
        M11 = 1;
        M12 = y;

        return this;
    }

    /// <summary>
    /// Sets this matrix to a translation matrix.
    /// </summary>
    /// <param name="trn">The translation vector.</param>
    /// <returns>This matrix for the purpose of chaining operations.</returns>
    public Affine2 SetToTranslation( Vector2 trn ) => SetToTranslation( trn.X, trn.Y );

    /// <summary>
    /// Sets this matrix to a scaling matrix.
    /// </summary>
    /// <param name="scaleX">The scale in x.</param>
    /// <param name="scaleY">The scale in y.</param>
    /// <returns></returns>
    public Affine2 SetToScaling( float scaleX, float scaleY )
    {
        M00 = scaleX;
        M01 = 0;
        M02 = 0;
        M10 = 0;
        M11 = scaleY;
        M12 = 0;

        return this;
    }

    /// <summary>
    /// Sets this matrix to a scaling matrix.
    /// </summary>
    /// <param name="scale">The scale vector.</param>
    /// <returns>This matrix for the purpose of chaining operations.</returns>
    public Affine2 SetToScaling( Vector2 scale ) => SetToScaling( scale.X, scale.Y );

    /// <summary>
    /// Sets this matrix to a rotation matrix that will rotate any vector in counter-clockwise direction around the z-axis.
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public Affine2 SetToRotation( float degrees )
    {
        float cos = MathUtils.CosDeg( degrees );
        float sin = MathUtils.SinDeg( degrees );

        M00 = cos;
        M01 = -sin;
        M02 = 0;
        M10 = sin;
        M11 = cos;
        M12 = 0;
        
        return this;
    }

    public void GetObjectData( SerializationInfo info, StreamingContext context )
    {
    }
}