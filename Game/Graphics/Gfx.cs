using Microsoft.Xna.Framework.Graphics;

namespace PlatformaniaCS.Game.Graphics;

public static class Gfx
{
    //
    // Entity collision types. These are checked against an entity's
    // 'collidesWith' property to see if collision is allowed.
    // i.e. If EntityA.bodyCategory is set to CAT_PLAYER,
    // and EntityB.collidesWith has the bit set for CAT_PLAYER, then
    // EntityA and EntityB will register collisions.
    //
    public const short CatNothing      = 0x0000; // - 00 (0     )
    public const short CatPlayer       = 0x0001; // - 01 (1     )
    public const short CatMobileEnemy  = 0x0002; // - 02 (2     )
    public const short CatFixedEnemy   = 0x0004; // - 03 (4     )
    public const short CatPlayerWeapon = 0x0008; // - 04 (8     )
    public const short CatInteractive  = 0x0010; // - 05 (16    )
    public const short CatCollectible  = 0x0020; // - 06 (32    )
    public const short CatPlatform     = 0x0040; // - 07 (64    )
    public const short CatGround       = 0x0080; // - 08 (128   )
    public const short CatPushable     = 0x0100; // - 09 (256   )
    public const short CatVillager     = 0x0200; // - 10 (512   )
    public const short Undefined11     = 0x0400; // - 11 (1024  )
    public const short Undefined12     = 0x0800; // - 12 (2048  )
    public const short Undefined13     = 0x1000; // - 13 (4096  )
    public const short Undefined14     = 0x2000; // - 14 (8192  )
    public const short Undefined15     = 0x4000; // - 15 (16384 )
    public const short CatAll          = 0x7fff; // - 16 (32767 )

    public const int HudWidth           = 960;  // Width in pixels of the HUD
    public const int HudHeight          = 540;  // Height in pixels of the HUD
    public const int DesktopWidth       = 1280; // Width in pixels of the Desktop window
    public const int DesktopHeight      = 720;  // Height in pixels of the Desktop window
    public const int ViewWidth          = 1152; // Width in pixels of the game view
    public const int ViewHeight         = 650;  // Height in pixels of the game view
    public const int ParallaxViewWidth  = 480;  // Width in pixels of the parallax view
    public const int ParallaxViewHeight = 270;  // Height in pixels of the parallax view

    //
    // Combined Categories
    public const short CatEnemy    = ( CatMobileEnemy | CatFixedEnemy );
    public const short CatObstacle = ( CatGround      | CatPlatform );

    public const int   MaximumZDepth      = 20;
    public const float FPS                = 60f;
    public const float MinFPS             = 30f;
    public const float StepTime           = ( 1.0f / 60f );
    public const int   VelocityIterations = 8;
    public const int   PositionIterations = 3;
    public const float FallGravity        = 9.8f;

    public static float    DefaultZoom         { get; set; } = 1.0f;
    public static float    LerpSpeed           { get; set; } = 0.075f;
    public static int      TerminalVelocity    { get; set; }
    public static float    PixelsToMeters      { get; set; }
    public static float    HudSceneWidth       { get; set; }
    public static float    HudSceneHeight      { get; set; }
    public static float    GameSceneWidth      { get; set; }
    public static float    GameSceneHeight     { get; set; }
    public static float    ParallaxSceneWidth  { get; set; }
    public static float    ParallaxSceneHeight { get; set; }
    public static Vector2  WorldGravity        { get; set; }
    public static GameTime GameTime            { get; set; }

    //
    // Pixels Per Meter in the Box2D World, usually the length of a single TiledMap tile.
    public static float PPM { get; set; }

    public static SimpleVec2F PixelDimensions { get; set; } = new SimpleVec2F();
    public static SimpleVec2F MeterDimensions { get; set; } = new SimpleVec2F();

    // -----------------------------------------------------------
    // Code
    // -----------------------------------------------------------

    public static void Initialise()
    {
        Trace.CheckPoint();

        SetPPM( 16.0f );

        WorldGravity     = new Vector2( 0, -9.8f );
        TerminalVelocity = ( int )( PPM * FallGravity );
        PixelsToMeters   = ( 1.0f / PPM );

        SetSceneDimensions();
    }

    public static void SetPPM( float newPPM )
    {
        if ( newPPM.Equals( PPM ) )
        {
            // Trace message needed, warning of newPPM == PPM
        }
        else
        {
            PPM            = newPPM;
            PixelsToMeters = ( 1.0f / PPM );

            SetSceneDimensions();
        }
    }

    public static void SetSceneDimensions()
    {
        HudSceneWidth       = ( HudWidth           / PPM );
        HudSceneHeight      = ( HudHeight          / PPM );
        GameSceneWidth      = ( ViewWidth          / PPM );
        GameSceneHeight     = ( ViewHeight         / PPM );
        ParallaxSceneWidth  = ( ParallaxViewWidth  / PPM );
        ParallaxSceneHeight = ( ParallaxViewHeight / PPM );
    }

    public static SimpleVec2F GetScreenSizeInMeters()
    {
        MeterDimensions.Set( ViewWidth * PixelsToMeters, ViewHeight * PixelsToMeters );

        return MeterDimensions;
    }

    public static SimpleVec2F GetScreenSizeInPixels()
    {
        PixelDimensions.Set( ViewWidth, ViewHeight );

        return PixelDimensions;
    }

    public static float GetPixelsToMeters( float pixels ) => pixels * PixelsToMeters;
}