using System.Drawing;

using Box2DSharp.Dynamics;

namespace Lugh.Physics;

public struct PhysicsBody
{
    public Body      Body         { get; set; }
    public int       Index        { get; set; }
    public bool      IsAlive      { get; set; }
    public int       ContactCount { get; set; }
    public Rectangle BodyBox      { get; set; }

    public void SetBodyBox( int x, int y, int width, int height )
    {
        BodyBox = new Rectangle( x, y, width, height );
    }
}