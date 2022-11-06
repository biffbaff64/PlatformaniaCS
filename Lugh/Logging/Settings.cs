// ##################################################

using PlatformaniaCS.Game.Audio;

// ##################################################

namespace Lugh.Logging;

public class Settings : IDisposable
{
    //
    // Defaults
    public const bool PrefFalseDefault = false;
    public const bool PrefTrueDefault  = true;

    public const string DefaultOn  = "default on";
    public const string DefaultOff = "default off";

    //
    // TO BE REMOVED
    public const string SpriteBoxes      = "sprite boxes";    // Shows sprite AABB Boxes
    public const string TileBoxes        = "tile boxes";      // Shows game tile AABB Boxes
    public const string AndroidOnDesktop = "android desktop"; //
    public const string Box2DPhysics     = "using box2d";     // Enables Box2D Physics
    public const string UsingAshleyECS   = "ashley ecs";      // Enables use of Ashley Entity Component System

    //
    // Development options
    public const string ScrollDemo     = "scroll demo";     // Enables Game Scroll Demo mode
    public const string ButtonBoxes    = "button boxes";    // Shows GameButton bounding boxes
    public const string ShowFPS        = "show fps";        // Shows current FPS on-screen
    public const string ShowDebug      = "show debug";      // Enables on-screen debug printing
    public const string Spawnpoints    = "spawn points";    // Shows spawn point tiles from game map
    public const string MenuHeaps      = "menu heaps";      // Show Heap Sizes on Menu Page if true
    public const string MenuScene      = "menu scene";      //
    public const string LevelSelect    = "level select";    //
    public const string CullSprites    = "cull sprites";    // Enables Sprite Culling when off screen
    public const string GlProfiler     = "gl profiler";     // Enables/Disables the LibGdx OpenGL Profiler
    public const string DisableEnemies = "disable enemies"; //
    public const string DisablePlayer  = "disable player";  //
    public const string Autoplay       = "autoplay";        //
    public const string IntroPanel     = "intro panel";     //

    //
    // Configuration settings
    public const string Installed     = "installed";      //
    public const string ShaderProgram = "shader program"; // Enables/Disables global shader program

    //
    // Game settings
    public const string Vibrations    = "vibrations";    // Enables/Disables device vibrations
    public const string MusicEnabled  = "music enabled"; // Enables/Disables Music
    public const string SoundsEnabled = "sound enabled"; // Enables/Disables Sound FX
    public const string MusicVolume   = "music volume";  //
    public const string FxVolume      = "fx volume";     //
    public const string ShowHints     = "show hints";    // Enables/Disables In-Game Hints
    public const string JoystickLeft  = "joystick pos";  // Controls Joystick screen pos ( left or right )

    //
    // Google Play Store
    public const string PlayServices = "play services";  // Enables Google Play Services
    public const string SignInStatus = "sign in status"; // Google Services sign in status (Android)
    public const string Achievements = "achievements";   // Enables In-Game Achievements
    public const string Challenges   = "challenges";     // Enables In-Game challenges
    public const string Events       = "events";         // Enables In-Game events

    public Preferences Prefs { get; set; }

    public Settings()
    {
        CreatePreferencesObject();
    }

    private void CreatePreferencesObject()
    {
        Trace.CheckPoint();

        try
        {
            Prefs = new Preferences( "config.xml" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e );
        }
    }

    public bool IsEnabled( string  pref ) => ( ( Prefs != null ) && Prefs.GetBoolean( pref ) );
    public bool IsDisabled( string pref ) => ( ( Prefs != null ) && !Prefs.GetBoolean( pref ) );

    public void Enable( string preference )
    {
        if ( Prefs != null )
        {
            Prefs.PutBoolean( preference, true );
            Prefs.Flush();
        }
    }

    public void Disable( string preference )
    {
        if ( Prefs != null )
        {
            Prefs.PutBoolean( preference, false );
            Prefs.Flush();
        }
    }

    public void ToggleState( string preference )
    {
        if ( Prefs != null )
        {
            Prefs.PutBoolean( preference, !Prefs.GetBoolean( preference ) );
            Prefs.Flush();
        }
    }

    public void FreshInstallCheck()
    {
        if ( IsEnabled( Installed ) )
        {
            ResetToDefaults();

            Stats.ResetAllMeters();

            Enable( Installed );
        }
    }

    public void ResetToDefaults()
    {
        if ( Prefs != null )
        {
            Prefs.PutBoolean( DefaultOn,  PrefTrueDefault );
            Prefs.PutBoolean( DefaultOff, PrefFalseDefault );

            // ---------- Configuration ----------
            Prefs.PutBoolean( ShaderProgram,  PrefFalseDefault );
            Prefs.PutBoolean( UsingAshleyECS, PrefFalseDefault );
            Prefs.PutBoolean( Box2DPhysics,   PrefTrueDefault );
            Prefs.PutBoolean( Installed,      PrefFalseDefault );
            Prefs.PutBoolean( ShowHints,      PrefTrueDefault );
            Prefs.PutBoolean( Vibrations,     PrefTrueDefault );
            Prefs.PutBoolean( MusicEnabled,   PrefTrueDefault );
            Prefs.PutBoolean( SoundsEnabled,  PrefTrueDefault );
            Prefs.PutBoolean( JoystickLeft,   PrefTrueDefault );

            Prefs.PutInteger( FxVolume,    AudioData.DefaultFxVolume );
            Prefs.PutInteger( MusicVolume, AudioData.DefaultMusicVolume );

            // ---------- Google Services ----------
            Prefs.PutBoolean( PlayServices, PrefFalseDefault );
            Prefs.PutBoolean( Achievements, PrefFalseDefault );
            Prefs.PutBoolean( Challenges,   PrefFalseDefault );
            Prefs.PutBoolean( Events,       PrefFalseDefault );
            Prefs.PutBoolean( SignInStatus, PrefFalseDefault );

            // ------------------- Development Flags -------------------
            Prefs.PutBoolean( MenuScene,        PrefTrueDefault );
            Prefs.PutBoolean( LevelSelect,      PrefTrueDefault );
            Prefs.PutBoolean( ScrollDemo,       PrefFalseDefault );
            Prefs.PutBoolean( SpriteBoxes,      PrefFalseDefault );
            Prefs.PutBoolean( TileBoxes,        PrefFalseDefault );
            Prefs.PutBoolean( ButtonBoxes,      PrefFalseDefault );
            Prefs.PutBoolean( ShowFPS,          PrefFalseDefault );
            Prefs.PutBoolean( ShowDebug,        PrefFalseDefault );
            Prefs.PutBoolean( Spawnpoints,      PrefFalseDefault );
            Prefs.PutBoolean( MenuHeaps,        PrefFalseDefault );
            Prefs.PutBoolean( CullSprites,      PrefTrueDefault );
            Prefs.PutBoolean( GlProfiler,       PrefFalseDefault );
            Prefs.PutBoolean( AndroidOnDesktop, PrefFalseDefault );
            Prefs.PutBoolean( Autoplay,         PrefFalseDefault );
            Prefs.PutBoolean( DisableEnemies,   PrefTrueDefault );
            Prefs.PutBoolean( DisablePlayer,    PrefTrueDefault );

            Prefs.Flush();
        }
    }

    public void DebugReport()
    {
    }

    public void Dispose()
    {
        Prefs = null;
    }
}