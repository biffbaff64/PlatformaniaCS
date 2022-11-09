using Box2DSharp.Dynamics;

namespace Lugh.Physics;

public struct PhysicsBody
{
    public Body      Body;
    public int       Index;
    public bool      IsAlive;
    public int       ContactCount;
    public Rectangle BodyBox;

    public void SetBodyBox( int x, int y, int width, int height )
    {
        BodyBox.X      = x;
        BodyBox.Y      = y;
        BodyBox.Width  = width;
        BodyBox.Height = height;
    }
}