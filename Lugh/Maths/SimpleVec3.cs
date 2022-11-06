namespace Lugh.Maths;

public class SimpleVec3 : Vec3
{
    public SimpleVec3() : base()
    {
    }
        
    public SimpleVec3( int x, int y, int z ) : base( x, y, z )
    {
    }

    public void Add( int x, int y, int z )
    {
        Set( X + x, Y + y, Z + z );
    }

    public void Sub( int x, int y, int z )
    {
        Set( X - x, Y - y, Z - z );
    }

    public void AddX( int value )
    {
        this.X += value;
    }

    public void AddY( int value )
    {
        this.Y += value;
    }

    public void AddZ( int value )
    {
        this.Z += value;
    }

    public void SubX( int value )
    {
        this.X -= value;
    }

    public void SubY( int value )
    {
        this.Y -= value;
    }

    public void SubZ( int value )
    {
        this.Z -= value;
    }

    public void Mul( int mulX, int mulY, int mulZ )
    {
        this.X *= mulX;
        this.Y *= mulY;
        this.Z *= mulZ;
    }

    public bool IsEmpty()
    {
        return ( ( X == 0 ) && ( Y == 0 ) && ( Z == 0 ) );
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