using Box2DSharp.Dynamics;

using Shape = Box2DSharp.Collision.Shapes.Shape;

namespace PlatformaniaCS.Game.Physics
{
    public class B2BodyDescriptor
    {
        public readonly BodyType        BodyType;
        public readonly Shape           Shape;
        public readonly CollisionFilter Filter;
        public readonly float           Density;
        public readonly float           Friction;
        public readonly float           Restitution;

        public B2BodyDescriptor( BodyType        bodyType,
                                 Shape           shape,
                                 CollisionFilter filter,
                                 float           density,
                                 float           friction,
                                 float           restitution )
        {
            this.BodyType    = bodyType;
            this.Shape       = shape;
            this.Filter      = filter;
            this.Density     = density;
            this.Friction    = friction;
            this.Restitution = restitution;
        }
    }
}