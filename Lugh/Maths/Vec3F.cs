namespace Lugh.Maths;

public class Vec3F
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vec3F() : this( 0, 0, 0 )
    {
    }

    public Vec3F( float x, float y, float z )
    {
        Set( x, y, z );
    }

    public void Set( float x, float y, float z )
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
}