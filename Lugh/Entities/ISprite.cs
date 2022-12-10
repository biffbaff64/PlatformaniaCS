using PlatformaniaCS.Game.Physics;

namespace Lugh.Entities;

public interface ISprite
{
    /**
         * Initialise this Sprite.
         * Override in any entity classes and add any
         * other relevant initialisation code AFTER the
         * call to create().
         *
         * @param descriptor The {@link SpriteDescriptor} holding
         *                   all setup information.
         */
    void Initialise( ISpriteDescriptor descriptor );

    /**
         * Performs the actual setting up of the GdxSprite,
         * according to the information provided in the
         * supplied {@link SpriteDescriptor}.
         */
    void Create( ISpriteDescriptor descriptor );

    /**
         * Performs the creation of the Box2D physics body which
         * will be attached to this sprite.
         */
    void CreateBody( PhysicsBodyType physicsBodyType );

    /**
         * Sets the initial starting position for this sprite.
         * NOTE: It is important that frameWidth & frameHeight
         * are initialised before this method is called.
         */
    void InitPosition( ISpriteDescriptor descriptor );

    /**
         * Provides an init position modifier value.
         * GdxSprites are placed on TiledMap boundaries and
         * some may need that position adjusting.
         */
    SimpleVec3 GetPositionModifier();

    /**
         * Provides the facility for some sprites to perform certain
         * actions before the main update method.
         * Some sprites may not need to do this, or may need to do extra
         * tasks, in which case this can be overridden.
         */
    void PreUpdate();

    void Update();

    void PostUpdate();

    void UpdateCommon();

    void PreDraw();

    void Draw( SpriteBatch spriteBatch );

    void Animate();

    void SetAnimation( ISpriteDescriptor descriptor );

    void SetRegion( TextureRegion region );
        
    /**
         * Ensures that the AABB collision rectangle for this sprite
         * is positioned correctly in relation to the sprites map position.
         */
    void UpdateCollisionObject();
}