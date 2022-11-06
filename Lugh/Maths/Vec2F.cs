namespace Lugh.Maths;

public class Vec2F
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vec2F() : this( 0, 0 )
    {
    }

    public Vec2F( float x, float y )
    {
        Set( x, y );
    }

    public void Set( float x, float y )
    {
        this.X = x;
        this.Y = y;
    }
}