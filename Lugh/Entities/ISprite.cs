using PlatformaniaCS.Game.Physics;

namespace Lugh.Entities
{
    public interface ISprite
    {
        /// <summary>
        /// 
        /// </summary>
        public void Temp()
        {
        }
    
        /// <summary>
        /// Initialise this Sprite.
        /// Override in any entity classes and add any other relevant
        /// initialisation code AFTER the call to create().
        /// </summary>
        /// <param name="descriptor">The <see cref="ISpriteDescriptor"/>holding all setup information.</param>
        void Initialise( ISpriteDescriptor descriptor );

        /// <summary>
        /// Performs the actual setting up of the GdxSprite,
        /// according to the information provided in the
        /// supplied <see cref="ISpriteDescriptor"/>.
        /// </summary>
        void Create( ISpriteDescriptor descriptor );

        /// <summary>
        /// Performs the creation of the Box2D physics body which
        /// will be attached to this sprite.
        /// </summary>
        void CreateBody( PhysicsBodyType physicsBodyType );

        /// <summary>
        /// Sets the initial starting position for this sprite.
        /// NOTE: It is important that frameWidth & frameHeight
        /// are initialised before this method is called.
        /// </summary>
        void InitPosition( ISpriteDescriptor descriptor );

        /// <summary>
        /// Provides an init position modifier value.
        /// GdxSprites are placed on TiledMap boundaries and
        /// some may need that position adjusting.
        /// </summary>
        SimpleVec3 GetPositionModifier();

        /// <summary>
        /// Provides the facility for some sprites to perform certain
        /// actions before the main update method.
        /// Some sprites may not need to do this, or may need to do extra
        /// tasks, in which case this can be overridden.
        /// </summary>
        void PreUpdate();

        void Update();

        void PostUpdate();

        void UpdateCommon();

        void PreDraw();

        void Draw( SpriteBatch spriteBatch );

        void Animate();

        void SetAnimation( ISpriteDescriptor descriptor );

        void SetRegion( TextureRegion region );
        
        /// <summary>
        /// Ensures that the AABB collision rectangle for this sprite
        /// is positioned correctly in relation to the sprites map position.
        /// </summary>
        void UpdateCollisionObject();
    }
}