using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;

using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;

using Shape = Box2DSharp.Collision.Shapes.Shape;

namespace PlatformaniaCS.Game.Physics;

public class BodyBuilder
{
    public Body CreateDynamicBox( Rectangle rectangle, CollisionFilter filter, float density, float friction,
                                  float     restitution )
    {
        var shape      = CreatePolygonShape( rectangle );
        var bodyDef    = CreateBodyDef( BodyType.DynamicBody, rectangle );
        var fixtureDef = CreateFixtureDef( filter, shape, density, friction, restitution );
        var body       = BuildBody( bodyDef, fixtureDef );

        if ( fixtureDef.IsSensor )
        {
        }

        return body;
    }

    public Body CreateDynamicBox( Rectangle rectangle, B2BodyDescriptor descriptor )
    {
        var shape   = CreatePolygonShape( rectangle );
        var bodyDef = CreateBodyDef( BodyType.DynamicBody, rectangle );

        var fixtureDef = CreateFixtureDef( descriptor.Filter,
                                          shape,
                                          descriptor.Density,
                                          descriptor.Friction,
                                          descriptor.Restitution );

        var body = BuildBody( bodyDef, fixtureDef );

        if ( fixtureDef.IsSensor )
        {
        }

        return body;
    }

    public Body CreateDynamicCircle( CircleF circle, CollisionFilter filter, float density, float friction,
                                     float   restitution )
    {
        var shape      = CreateCircleShape( circle );
        var bodyDef    = CreateBodyDef( BodyType.DynamicBody, circle );
        var fixtureDef = CreateFixtureDef( filter, shape, density, friction, restitution );
        var body       = BuildBody( bodyDef, fixtureDef );

        if ( fixtureDef.IsSensor )
        {
        }

        return body;
    }

    /**
         * Creates a Kinematic Box2D body which can be assigned to a GdxSprite.
         * <p>
         * Kinematic bodies are somewhat in between static and dynamic bodies.
         * Like static bodies, they do not react to forces, but like dynamic bodies,
         * they do have the ability to move. Kinematic bodies are great for things
         * where you, the programmer, want to be in full control of a body's motion,
         * such as a moving platform in a platform game.
         * It is possible to set the position on a kinematic body directly, but it's
         * usually better to set a velocity instead, and letting Box2D take care of
         * position updates.
         *
         * @param _density     Object density
         * @param _restitution The object restitution.
         */
    public Body CreateKinematicBody( Rectangle rectangle, CollisionFilter filter, float density, float friction,
                                     float     restitution )
    {
        var shape      = CreatePolygonShape( rectangle );
        var bodyDef    = CreateBodyDef( BodyType.KinematicBody, rectangle );
        var fixtureDef = CreateFixtureDef( filter, shape, density, friction, restitution );
        var body       = BuildBody( bodyDef, fixtureDef );

        return body;
    }

    /**
         * Creates a Static Box2D body.
         * <p>
         * Static bodies are objects which do not move and are not affected by forces.
         * Dynamic bodies are affected by static bodies. Static bodies are perfect for
         * ground, walls, and any object which does not need to move. Static bodies
         * require less computing power.
         */
    public Body CreateStaticBody( Rectangle rectangle, CollisionFilter filter )
    {
        var shape      = CreatePolygonShape( rectangle );
        var bodyDef    = CreateBodyDef( BodyType.StaticBody, rectangle );
        var fixtureDef = CreateFixtureDef( filter, shape, 1.0f, 1.0f, 0.15f );
        var body       = BuildBody( bodyDef, fixtureDef );

        return body;
    }

    public void CreateStaticBody( Shape shape, CollisionFilter filter )
    {
        var bodyDef = new BodyDef
        {
            BodyType = BodyType.StaticBody
        };

        var fixtureDef = CreateFixtureDef( filter, shape, 1.0f, 1.0f, 0.15f );
        var body       = BuildBody( bodyDef, fixtureDef );
    }

    private Body BuildBody( BodyDef bodyDef, FixtureDef fixtureDef )
    {
        var body = App.WorldModel.Box2DWorld.CreateBody( bodyDef );
        body.CreateFixture( fixtureDef );

        return body;
    }

    private BodyDef CreateBodyDef( BodyType bodyType, Rectangle rectangle )
    {
        var bodyDef = new BodyDef
        {
            BodyType = bodyType, FixedRotation = true
        };

        bodyDef.Position.Set( ( rectangle.X + ( rectangle.Width  / 2f ) ) / Gfx.PPM,
                             ( rectangle.Y  + ( rectangle.Height / 2f ) ) / Gfx.PPM );

        return bodyDef;
    }

    private BodyDef CreateBodyDef( BodyType bodyType, CircleF circle )
    {
        var bodyDef = new BodyDef
        {
            BodyType = bodyType, FixedRotation = true
        };

        bodyDef.Position.Set( ( circle.Position.X + ( circle.Radius ) ) / Gfx.PPM,
                             ( circle.Position.X  + ( circle.Radius ) ) / Gfx.PPM );

        return bodyDef;
    }

    private FixtureDef CreateFixtureDef( CollisionFilter filter,
                                         Shape           shape,
                                         float           density,
                                         float           friction,
                                         float           restitution )
    {
        var fixtureDef = new FixtureDef
        {
            Shape       = shape,
            Density     = density,
            Friction    = friction,
            Restitution = restitution,
            Filter =
            {
                MaskBits = filter.CollidesWith, CategoryBits = filter.BodyCategory
            },
            IsSensor = filter.IsSensor
        };

        return fixtureDef;
    }

    private PolygonShape CreatePolygonShape( Rectangle rectangle )
    {
        var shape = new PolygonShape();

        shape.SetAsBox( ( ( rectangle.Width / 2f ) / Gfx.PPM ),
                       ( ( rectangle.Height / 2f ) / Gfx.PPM ) );

        return shape;
    }

    private CircleShape CreateCircleShape( CircleF circle )
    {
        var shape = new CircleShape
        {
            Radius = circle.Radius
        };

        return shape;
    }

    public Body NewBody( Rectangle physicsBodyBodyBox, ushort bodyCategory, ushort collidesWith, PhysicsBodyType physicsBodyType )
    {
        throw new NotImplementedException();
    }
}