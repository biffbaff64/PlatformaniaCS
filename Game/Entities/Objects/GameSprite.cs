// ##################################################

using Microsoft.Xna.Framework.Graphics;

using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.Physics;

using Animation = Lugh.Entities.Animation;

// ##################################################

namespace PlatformaniaCS.Game.Entities.Objects;
// TODO: Can IEntityComponent be removed?

public class GameSprite : IEntityComponent, IDisposable
{
    // -----------------------------------------------
    // Identity etc.
    //
    public GraphicID    GID             { get; set; }
    public GraphicID    Type            { get; set; }
    public ActionStates ActionState     { get; set; }
    public int          SpriteNumber    { get; set; }
    public bool         IsMainCharacter { get; set; }
    public int          Strength        { get; set; }

    // -----------------------------------------------
    // Movement / Transform / Positioning
    //
    public Direction   Direction        { get; set; }
    public Direction   LookingAt        { get; set; }
    public SimpleVec2F Distance         { get; set; }
    public SimpleVec2F Speed            { get; set; }
    public SimpleVec3F InitXYZ          { get; set; }
    public bool        IsFlippedX       { get; set; }
    public bool        IsFlippedY       { get; set; }
    public bool        CanFlip          { get; set; }
    public bool        IsRotating       { get; set; }
    public float       RotateSpeed      { get; set; }
    public SimpleVec3F Position         { get; set; }
    public Vector2     Origin           { get; set; }
    public float       ZPosition        { get; set; }
    public bool        ShouldForceTurnX { get; set; }
    public bool        ShouldForceTurnY { get; set; }
    public bool        IsHeld           { get; set; }
    public BoxF        BoundsBox        { get; set; }

    // -----------------------------------------------
    // Collision Related
    //
    public int    B2dBodyIndex     { get; set; } // Index into the Body array
    public ushort BodyCategory     { get; set; } // Bit-mask entity collision type (See Gfx()).
    public ushort CollidesWith     { get; set; } // Bit-mask of entity types that can be collided with
    public bool   IsHittable       { get; set; } // ( Might be losing this flag... )
    public bool   IsTouchingPlayer { get; set; }
    public bool   IsTouchingEnemy  { get; set; }
    public bool   IsHittingWeapon  { get; set; }
    public bool   IsOnPlatform     { get; set; }
    public bool   IsHittingSame    { get; set; }

    // -----------------------------------------------
    // Animation related
    //
    public Animation       Animation        { get; set; }
    public TextureRegion[] AnimFrames       { get; set; }
    public TextureRegion   Region           { get; set; }
    public float           ElapsedAnimTime  { get; set; }
    public bool            IsAnimating      { get; set; }
    public bool            IsLoopingAnim    { get; set; }
    public int             FrameWidth       { get; set; } // Width in pixels, or width of frame for animations
    public int             FrameHeight      { get; set; } // Width in pixels, or width of frame for animations
    public bool            IsDrawable       { get; set; }
    public bool            IsActive         { get; set; }
    public bool            IsSetupCompleted { get; set; }
    public bool            IsLinked         { get; set; }
    public int             Link             { get; set; }

    // --------------------------------------------------------------
    // Code
    // --------------------------------------------------------------

    /// <summary>
    /// Default constructor.
    /// Creates a GdxSprite object with a default GraphicID of G_NO_ID.
    /// </summary>
    public GameSprite() : this( GraphicID.G_NO_ID )
    {
    }

    /// <summary>
    /// Create a GdxSprite object with the supplied GraphicID
    /// </summary>
    public GameSprite( GraphicID graphicID )
    {
        GID = graphicID;
    }

    /// <summary>
    /// Initialise this GdxSprite object.
    /// Override in any entity classes and add any other relevant
    /// initialisation code AFTER the call to create().
    /// </summary>
    /// <param name="descriptor">
    /// The SpriteDescriptor holding all setup info.
    /// </param>
    public void Initialise( SpriteDescriptor descriptor )
    {
        Create( descriptor );
    }

    /// <summary>
    /// Performs the actual setting up of the GdxSprite, according
    /// to the information provided in the supplied SpriteDescriptor.
    /// </summary>
    /// <param name="descriptor">
    /// The SpriteDescriptor holding all setup info.
    /// </param>
    public void Create( SpriteDescriptor descriptor )
    {
        Direction = new Direction();
        LookingAt = new Direction();
        Speed     = new SimpleVec2F();
        Distance  = new SimpleVec2F();
        InitXYZ   = new SimpleVec3F();
        Position  = new SimpleVec3F();
        BoundsBox = new BoxF();

        IsDrawable      = true;
        IsRotating      = false;
        IsFlippedX      = false;
        IsFlippedY      = false;
        CanFlip         = true;
        IsMainCharacter = false;
        IsHittable      = true;
        IsActive        = false;

        SpriteNumber = descriptor.Index;
        Type         = descriptor.Type;
        IsAnimating  = ( descriptor.Frames > 1 );
        ActionState  = ActionStates._NO_ACTION;
        B2dBodyIndex = App.WorldModel.BodiesList.Count;
        Strength     = GameConstants.MaxStrength;

        App.WorldModel.BodiesList.Add( new PhysicsBody() );

        if ( descriptor.Asset != null )
        {
            SetAnimation( descriptor );
        }

        // Determine the initial starting position by
        // multiplying the marker tile position by tile size
        // and then adding on any supplied modifier value.
        var vec3M = GetPositionModifier();

        var vec3 = new SimpleVec3
            (
             descriptor.Position.X                      + vec3M.X,
             descriptor.Position.Y                      + vec3M.Y,
             App.EntityUtils.GetInitialZPosition( GID ) + vec3M.Z
            );

        InitPosition( vec3 );

        DefinePhysicsBodyBox( false );

        IsLinked = ( descriptor.Link > 0 );
        Link     = descriptor.Link;

        IsSetupCompleted = true;
    }

    public void CreateBody( PhysicsBodyType physicsBodyType )
    {
        var physicsBody = GetPhysicsBody();

        physicsBody.Body = App.WorldModel.BodyBuilder.NewBody
            (
             physicsBody.BodyBox,
             BodyCategory,
             CollidesWith,
             physicsBodyType
            );

        if ( App.WorldModel.BodiesList[ B2dBodyIndex ].Body != null )
        {
            physicsBody.Body.UserData = new BodyIdentity( this, GID, Type );
            physicsBody.IsAlive       = true;

            App.WorldModel.BodiesList[ B2dBodyIndex ] = physicsBody;
        }
    }

    /// <summary>
    /// Sets the initial starting position for this sprite.
    /// NOTE: It is important that frameWidth & frameHeight
    /// are initialised before this method is called.
    /// </summary>
    public void InitPosition( SimpleVec3 vec3 )
    {
        // Determine the initial starting position by
        // multiplying the marker tile position by tile size
        // and then adding on any supplied modifier value.
        InitXYZ.Set
            (
             ( vec3.X * App.MapData.TileWidth ),
             ( vec3.Y * App.MapData.TileHeight ),
             vec3.Z
            );

        Position.Set( InitXYZ.X, InitXYZ.Y, InitXYZ.Z );
        BoundsBox.Set( InitXYZ.X, InitXYZ.Y, FrameWidth, FrameHeight );

        // The CENTRE of this sprite
        Origin = new Vector2
            (
             InitXYZ.X + ( FrameWidth  / 2f ),
             InitXYZ.Y + ( FrameHeight / 2f )
            );

        ZPosition = vec3.Z;
    }

    /// <summary>
    /// Provides an init position modifier value.
    /// GdxSprites are placed on TiledMap boundaries and
    /// some may need that position adjusting.
    /// </summary>
    public SimpleVec3 GetPositionModifier() => new SimpleVec3( 0, 0, 0 );

    /// <summary>
    /// Provides the facility for some sprites to perform certain
    /// actions before the main update method.
    /// Some sprites may not need to do this, or may need to do extra
    /// tasks, in which case this can be overridden.
    /// </summary>
    public void PreUpdate()
    {
        if ( App.GameProgress.LevelCompleted
             && !IsMainCharacter
             && ( ActionState != ActionStates._DEAD )
             && ( ActionState != ActionStates._DYING ) )
        {
            SetActionState( ActionStates._DYING );
        }
        else
        {
            if ( ActionState == ActionStates._DYING )
            {
                Position.Set( InitXYZ.X, InitXYZ.Y, InitXYZ.Z );
            }
        }
    }

    /// <summary>
    /// The main update method.
    /// This is the MINIMUM that should be performed for each
    /// sprite. Most sprites should override this to perform
    /// their various actions.
    /// </summary>
    public void Update()
    {
        Animate();

        UpdateCommon();
    }

    public void PostUpdate()
    {
    }

    public void UpdateCommon()
    {
        if ( IsRotating )
        {
//                sprite.rotate( rotateSpeed );
        }

        if ( CanFlip )
        {
//                sprite.setFlip( isFlippedX, isFlippedY );
        }
    }

    public void Tidy( int index )
    {
    }

    public void PreDraw()
    {
    }

    public void Draw( SpriteBatch spriteBatch )
    {
        SetPositionFromBody();

        if ( IsDrawable )
        {
            try
            {
            }
            catch ( NullReferenceException nre )
            {
                Trace.Err( message: GID + " : " + nre );
            }
        }
    }

    public void Animate()
    {
        if ( IsAnimating )
        {
            Region.SetRegion( App.EntityUtils.GetKeyFrame( Animation, ElapsedAnimTime, IsLoopingAnim ) );

            ElapsedAnimTime += Gfx.GameTime.GetElapsedSeconds();
        }
        else
        {
            Region.SetRegion( AnimFrames[ 0 ] );
        }
    }

    public void SetAnimation( SpriteDescriptor descriptor )
    {
        try
        {
            AnimFrames = new TextureRegion[ descriptor.Frames ];

            TextureRegion asset = App.Assets.GetAnimationRegion( descriptor.Asset );

            if ( descriptor.Size != null )
            {
                FrameWidth  = descriptor.Size.X;
                FrameHeight = descriptor.Size.Y;
            }
            else
            {
                FrameWidth  = asset.RegionWidth / descriptor.Frames;
                FrameHeight = asset.RegionHeight;
            }

            TextureRegion[ , ] tmpFrames = asset.Split( FrameWidth, FrameHeight );

            var i = 0;

            foreach ( var tmpFrame in tmpFrames )
            {
                foreach ( TextureRegion textureRegion in tmpFrame )
                {
                    if ( i < descriptor.Frames )
                    {
                        AnimFrames[ i++ ] = textureRegion;
                    }
                }
            }

            Animation = new Animation( descriptor.AnimRate / 6f, AnimFrames )
            {
                Mode = descriptor.PlayMode
            };

            ElapsedAnimTime = 0;

            IsLoopingAnim = ( ( descriptor.PlayMode    != Animation.PlayMode._NORMAL )
                              && ( descriptor.PlayMode != Animation.PlayMode._REVERSED ) );

            Region = AnimFrames[ 0 ];

//                sprite.setSize( frameWidth, frameHeight );
        }
        catch ( NullReferenceException npe )
        {
            Trace.Divider( '#', 100 );
            Trace.Err( message: npe.ToString() );
            descriptor.Debug();
            Trace.Divider( '#', 100 );
        }
    }

    /// <summary>
    /// Sets the current <see cref="ActionStates"/> for this sprite.
    /// Note: this is a method instead of a property as it can be
    /// overridden by child classes to set state-dependant animations.
    /// </summary>
    public void SetActionState( ActionStates action )
    {
        ActionState = action;
    }

    /// <summary>
    /// Sets the sprite position from the physics body coordinates
    /// so that it is drawn at the correct location.
    /// </summary>
    public void SetPositionFromBody()
    {
        if ( GetPhysicsBody().Body == null )
        {
            Position.Set
                (
                 ( GetPhysicsBody().Body.GetPosition().X * Gfx.PPM ) - ( GetPhysicsBody().BodyBox.Width  / 2f ),
                 ( GetPhysicsBody().Body.GetPosition().Y * Gfx.PPM ) - ( GetPhysicsBody().BodyBox.Height / 2f ),
                 ZPosition
                );
        }
    }

    /// <summary>
    /// Defines a box to use for Box2D Physics body creation,
    /// based on this sprite's co-ordinates and size.
    /// This is the default implementation, and sets the box
    /// size to the exact dimensions of an animation frame.
    /// Override to create differing sizes.
    ///</summary>
    public void DefinePhysicsBodyBox( bool useBodyPos )
    {
        GetPhysicsBody().SetBodyBox
            (
             ( int )( useBodyPos ? GetBodyX() : Position.X ),
             ( int )( useBodyPos ? GetBodyY() : Position.Y ),
             FrameWidth,
             FrameHeight
            );
    }

    public void SetLink( int link )
    {
        Link     = link;
        IsLinked = ( link > 0 );
    }

    public void SetDying()
    {
        Speed.SetEmpty();
        GetPhysicsBody().Body.SetLinearVelocity( System.Numerics.Vector2.Zero );
        ActionState = ActionStates._DYING;
    }

    /// <summary>
    /// Helper method to fetch the physics body for this sprite.
    /// </summary>
    public PhysicsBody GetPhysicsBody() => App.WorldModel.BodiesList[ B2dBodyIndex ];

    /// <summary>
    /// Helper method to fetch the physics body bounding box for this sprite.
    /// </summary>
    public Rectangle GetBodyBox() => App.WorldModel.BodiesList[ B2dBodyIndex ].BodyBox;

    /// <summary>
    /// Gets the current X position of the physics body attached to this sprite.
    /// </summary>
    public float GetBodyX() => GetPhysicsBody().Body == null ? 0 : ( GetPhysicsBody().Body.GetPosition().X * Gfx.PPM );

    /// <summary>
    /// Gets the current Y position of the physics body attached to this sprite.
    /// </summary>
    public float GetBodyY() => GetPhysicsBody().Body == null ? 0 : ( GetPhysicsBody().Body.GetPosition().Y * Gfx.PPM );

    public void Dispose()
    {
        Array.Fill( AnimFrames, null );

//            Sprite      = null;
        Direction  = null;
        LookingAt  = null;
        Distance   = null;
        Speed      = null;
        InitXYZ    = null;
        Animation  = null;
        AnimFrames = null;
    }

    // ---------------------------------------------------------------

    public CollisionObject GetCollisionObject() =>
        // TO BE REMOVED
        null;

    public void SetCollisionObject( float xPos, float yPos )
    {
        // TO BE REMOVED
    }
}