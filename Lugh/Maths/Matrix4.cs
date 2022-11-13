using System.Runtime.Serialization;

namespace Lugh.Maths;

public class Matrix4 : ISerializable
{
    public const int M00 = 0;
    public const int M01 = 4;
    public const int M02 = 8;
    public const int M03 = 12;
    public const int M10 = 1;
    public const int M11 = 5;
    public const int M12 = 9;
    public const int M13 = 13;
    public const int M20 = 2;
    public const int M21 = 6;
    public const int M22 = 10;
    public const int M23 = 14;
    public const int M30 = 3;
    public const int M31 = 7;
    public const int M32 = 11;
    public const int M33 = 15;

    public float[] Val { get; set; } = new float[ 16 ];

    public Matrix4()
    {
    }

    public void GetObjectData( SerializationInfo info, StreamingContext context )
    {
    }
}