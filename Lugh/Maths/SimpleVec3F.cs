namespace Lugh.Maths;

public class SimpleVec3F : Vec3F
{
    public SimpleVec3F() : base()
    {
    }
        
    public SimpleVec3F( float x, float y, float z ) : base( x, y, z )
    {
    }

    public void Add( float x, float y, float z )
    {
        Set( X + x, Y + y, Z + z );
    }

    public void Sub( float x, float y, float z )
    {
        Set( X - x, Y - y, Z - z );
    }

    public void AddX( float value )
    {
        this.X += value;
    }

    public void AddY( float value )
    {
        this.Y += value;
    }

    public void AddZ( float value )
    {
        this.Z += value;
    }

    public void SubX( float value )
    {
        this.X -= value;
    }

    public void SubY( float value )
    {
        this.Y -= value;
    }

    public void SubZ( float value )
    {
        this.Z -= value;
    }

    public void Mul( float mulX, float mulY, float mulZ )
    {
        this.X *= mulX;
        this.Y *= mulY;
        this.Z *= mulZ;
    }

    public bool IsEmpty()
    {
        const float tolerance = 0.0001f;
            
        return ( ( Math.Abs( X ) < tolerance ) && ( Math.Abs( Y ) < tolerance ) && ( Math.Abs( Z ) < tolerance ) );
    }

    public void SetEmpty()
    {
        this.X = 0;
        this.Y = 0;
        this.Z = 0;
    }

    public override string ToString()
    {
        return "X: " + X + ", Y:" + Y + ", Z:" + Z;
    }
}