// ##################################################

// ##################################################

namespace PlatformaniaCS.Game.Config;

public class Settings
{
    public StringObjectPair[] Gs = new StringObjectPair[]
    {

    };
    
    //
    // Development options
    public const string SpriteBoxes    = "sprite boxes";    // Shows sprite AABB Boxes
    public const string TileBoxes      = "tile boxes";      // Shows game tile AABB Boxes
    public const string Box2DPhysics   = "using box2d";     // Enables Box2D Physics
    public const string UsingAshleyECS = "ashley ecs";      // Enables use of Ashley Entity Component System
    public const string ScrollDemo     = "scroll demo";     // Enables Game Scroll Demo mode
    public const string ButtonBoxes    = "button boxes";    // Shows GameButton bounding boxes
    public const string ShowFPS        = "show fps";        // Shows current FPS on-screen
    public const string ShowDebug      = "show debug";      // Enables on-screen debug printing
    public const string Spawnpoints    = "spawn points";    // Shows spawn point tiles from game map
    public const string MenuScene      = "menu scene";      //
    public const string LevelSelect    = "level select";    //
    public const string CullSprites    = "cull sprites";    // Enables Sprite Culling when off screen
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
    // Google Play Store - to be removed
    public const string PlayServices = "play services";  // Enables Google Play Services
    public const string SignInStatus = "sign in status"; // Google Services sign in status (Android)
    public const string Achievements = "achievements";   // Enables In-Game Achievements
    public const string Challenges   = "challenges";     // Enables In-Game challenges
    public const string Events       = "events";         // Enables In-Game events

    // ----------------------------------------------------

    public Preferences Prefs { get; set; }

    // ----------------------------------------------------

    public Settings( string filename )
    {
        try
        {
            Prefs = new Preferences( filename );
        }
        catch ( Exception e )
        {
            Trace.Err( message: e.Message );

            Prefs = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void FreshInstallCheck()
    {
        if ( Prefs.IsEnabled( Installed ) is false )
        {
            ResetToDefaults();

            Stats.ResetAllMeters();

            Prefs.Enable( Installed );
        }
    }

    private void CreateDictionary()
    {
        var dictionary = new Dictionary< string, object >
        {
            // ---------- Configuration ----------
            { ShaderProgram, Preferences.PrefFalseDefault },
            { UsingAshleyECS, Preferences.PrefFalseDefault },
            { Box2DPhysics, Preferences.PrefTrueDefault },
            { Installed, Preferences.PrefFalseDefault },
            { ShowHints, Preferences.PrefTrueDefault },
            { Vibrations, Preferences.PrefTrueDefault },
            { JoystickLeft, Preferences.PrefTrueDefault },

            // --------------- Audio ---------------
            { FxVolume, AudioData.DefaultFxVolume },
            { MusicVolume, AudioData.DefaultMusicVolume },
            { MusicEnabled, Preferences.PrefTrueDefault },
            { SoundsEnabled, Preferences.PrefTrueDefault },

            // ---------- Google Services ----------
            { PlayServices, Preferences.PrefFalseDefault },
            { Achievements, Preferences.PrefFalseDefault },
            { Challenges, Preferences.PrefFalseDefault },
            { Events, Preferences.PrefFalseDefault },
            { SignInStatus, Preferences.PrefFalseDefault },

            // ------------------- Development Flags -------------------
            { MenuScene, Preferences.PrefTrueDefault },
            { LevelSelect, Preferences.PrefTrueDefault },
            { ScrollDemo, Preferences.PrefFalseDefault },
            { SpriteBoxes, Preferences.PrefFalseDefault },
            { TileBoxes, Preferences.PrefFalseDefault },
            { ButtonBoxes, Preferences.PrefFalseDefault },
            { ShowFPS, Preferences.PrefFalseDefault },
            { ShowDebug, Preferences.PrefFalseDefault },
            { Spawnpoints, Preferences.PrefFalseDefault },
            { CullSprites, Preferences.PrefTrueDefault },
            { Autoplay, Preferences.PrefFalseDefault },
            { DisableEnemies, Preferences.PrefTrueDefault },
            { DisablePlayer, Preferences.PrefTrueDefault },
        };

        Prefs.Set( dictionary );
    }

    /// <summary>
    /// Resets all preferences to their default values.
    /// </summary>
    public void ResetToDefaults()
    {
        Trace.CheckPoint();

        Prefs.Clear();

        // ---------- Configuration ----------
        Prefs.PutBoolean( ShaderProgram,  Preferences.PrefFalseDefault );
        Prefs.PutBoolean( UsingAshleyECS, Preferences.PrefFalseDefault );
        Prefs.PutBoolean( Box2DPhysics,   Preferences.PrefTrueDefault );
        Prefs.PutBoolean( Installed,      Preferences.PrefFalseDefault );
        Prefs.PutBoolean( ShowHints,      Preferences.PrefTrueDefault );
        Prefs.PutBoolean( Vibrations,     Preferences.PrefTrueDefault );
        Prefs.PutBoolean( JoystickLeft,   Preferences.PrefTrueDefault );

        // --------------- Audio ---------------
        Prefs.PutInteger( FxVolume,    AudioData.DefaultFxVolume );
        Prefs.PutInteger( MusicVolume, AudioData.DefaultMusicVolume );
        Prefs.PutBoolean( MusicEnabled,  Preferences.PrefTrueDefault );
        Prefs.PutBoolean( SoundsEnabled, Preferences.PrefTrueDefault );

        // ---------- Google Services ----------
        Prefs.PutBoolean( PlayServices, Preferences.PrefFalseDefault );
        Prefs.PutBoolean( Achievements, Preferences.PrefFalseDefault );
        Prefs.PutBoolean( Challenges,   Preferences.PrefFalseDefault );
        Prefs.PutBoolean( Events,       Preferences.PrefFalseDefault );
        Prefs.PutBoolean( SignInStatus, Preferences.PrefFalseDefault );

        // ------------------- Development Flags -------------------
        Prefs.PutBoolean( MenuScene,      Preferences.PrefTrueDefault );
        Prefs.PutBoolean( LevelSelect,    Preferences.PrefTrueDefault );
        Prefs.PutBoolean( ScrollDemo,     Preferences.PrefFalseDefault );
        Prefs.PutBoolean( SpriteBoxes,    Preferences.PrefFalseDefault );
        Prefs.PutBoolean( TileBoxes,      Preferences.PrefFalseDefault );
        Prefs.PutBoolean( ButtonBoxes,    Preferences.PrefFalseDefault );
        Prefs.PutBoolean( ShowFPS,        Preferences.PrefFalseDefault );
        Prefs.PutBoolean( ShowDebug,      Preferences.PrefFalseDefault );
        Prefs.PutBoolean( Spawnpoints,    Preferences.PrefFalseDefault );
        Prefs.PutBoolean( CullSprites,    Preferences.PrefTrueDefault );
        Prefs.PutBoolean( Autoplay,       Preferences.PrefFalseDefault );
        Prefs.PutBoolean( DisableEnemies, Preferences.PrefTrueDefault );
        Prefs.PutBoolean( DisablePlayer,  Preferences.PrefTrueDefault );
    }

    /// <summary>
    /// 
    /// </summary>                                      
    public void DebugReport()
    {
        Trace.CheckPoint();

        // ---------- Configuration ----------
        Trace.Info( message: ShaderProgram + " : " + ( Prefs.GetBoolean( ShaderProgram ) ? "true" : "false" ) );
        Trace.Info( message: UsingAshleyECS + " : " + Prefs.GetBoolean( UsingAshleyECS ) );
        Trace.Info( message: Box2DPhysics + " : " + Prefs.GetBoolean( Box2DPhysics ) );
        Trace.Info( message: Installed + " : " + Prefs.GetBoolean( Installed ) );
        Trace.Info( message: ShowHints + " : " + Prefs.GetBoolean( ShowHints ) );
        Trace.Info( message: Vibrations + " : " + Prefs.GetBoolean( Vibrations ) );
        Trace.Info( message: JoystickLeft + " : " + Prefs.GetBoolean( JoystickLeft ) );

        // --------------- Audio ---------------
        Trace.Info( message: FxVolume + " : " + Prefs.GetInteger( FxVolume ) );
        Trace.Info( message: MusicVolume + " : " + Prefs.GetInteger( MusicVolume ) );
        Trace.Info( message: MusicEnabled + " : " + Prefs.GetBoolean( MusicEnabled ) );
        Trace.Info( message: SoundsEnabled + " : " + Prefs.GetBoolean( SoundsEnabled ) );

        // ---------- Google Services ----------
        Trace.Info( message: PlayServices + " : " + Prefs.GetBoolean( PlayServices ) );
        Trace.Info( message: Achievements + " : " + Prefs.GetBoolean( Achievements ) );
        Trace.Info( message: Challenges + " : " + Prefs.GetBoolean( Challenges ) );
        Trace.Info( message: Events + " : " + Prefs.GetBoolean( Events ) );
        Trace.Info( message: SignInStatus + " : " + Prefs.GetBoolean( SignInStatus ) );

        // ------------------- Development Flags -------------------
        Trace.Info( message: MenuScene + " : " + Prefs.GetBoolean( MenuScene ) );
        Trace.Info( message: LevelSelect + " : " + Prefs.GetBoolean( LevelSelect ) );
        Trace.Info( message: ScrollDemo + " : " + Prefs.GetBoolean( ScrollDemo ) );
        Trace.Info( message: SpriteBoxes + " : " + Prefs.GetBoolean( SpriteBoxes ) );
        Trace.Info( message: TileBoxes + " : " + Prefs.GetBoolean( TileBoxes ) );
        Trace.Info( message: ButtonBoxes + " : " + Prefs.GetBoolean( ButtonBoxes ) );
        Trace.Info( message: ShowFPS + " : " + Prefs.GetBoolean( ShowFPS ) );
        Trace.Info( message: ShowDebug + " : " + Prefs.GetBoolean( ShowDebug ) );
        Trace.Info( message: Spawnpoints + " : " + Prefs.GetBoolean( Spawnpoints ) );
        Trace.Info( message: CullSprites + " : " + Prefs.GetBoolean( CullSprites ) );
        Trace.Info( message: Autoplay + " : " + Prefs.GetBoolean( Autoplay ) );
        Trace.Info( message: DisableEnemies + " : " + Prefs.GetBoolean( DisableEnemies ) );
        Trace.Info( message: DisablePlayer + " : " + Prefs.GetBoolean( DisablePlayer ) );
    }
}