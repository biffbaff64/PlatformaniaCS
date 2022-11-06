using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace PlatformaniaCS.Game.Physics;

public class Box2DWorldContactListener : IContactListener
{
    public void BeginContact( Contact contact )
    {
    }

    public void EndContact( Contact contact )
    {
    }

    public void PreSolve( Contact contact, in Manifold oldManifold )
    {
    }

    public void PostSolve( Contact contact, in ContactImpulse impulse )
    {
    }
}