// ##################################################

using PlatformaniaCS.Game.Audio;

// ##################################################

namespace Lugh.Logging;

public class Settings : IDisposable
{
    public class GS
    {
        public bool SpriteBoxes      { get; set; } // Shows sprite AABB Boxes
        public bool TileBoxes        { get; set; } // Shows game tile AABB Boxes
        public bool AndroidOnDesktop { get; set; } //
        public bool Box2DPhysics     { get; set; } // Enables Box2D Physics
        public bool UsingAshleyECS   { get; set; } // Enables use of Ashley Entity Component System

        //
        // Development options
        public bool ScrollDemo     { get; set; } // Enables Game Scroll Demo mode
        public bool ButtonBoxes    { get; set; } // Shows GameButton bounding boxes
        public bool ShowFPS        { get; set; } // Shows current FPS on-screen
        public bool ShowDebug      { get; set; } // Enables on-screen debug printing
        public bool Spawnpoints    { get; set; } // Shows spawn point tiles from game map
        public bool MenuHeaps      { get; set; } // Show Heap Sizes on Menu Page if true
        public bool MenuScene      { get; set; } //
        public bool LevelSelect    { get; set; } //
        public bool CullSprites    { get; set; } // Enables Sprite Culling when off screen
        public bool GlProfiler     { get; set; } // Enables/Disables the LibGdx OpenGL Profiler
        public bool DisableEnemies { get; set; } //
        public bool DisablePlayer  { get; set; } //
        public bool Autoplay       { get; set; } //
        public bool IntroPanel     { get; set; } //

        //
        // Configuration settings
        public bool Installed     { get; set; } //
        public bool ShaderProgram { get; set; } // Enables/Disables global shader program

        //
        // Game settings
        public bool Vibrations    { get; set; } // Enables/Disables device vibrations
        public bool ShowHints     { get; set; } // Enables/Disables In-Game Hints
        public bool JoystickLeft  { get; set; } // Controls Joystick screen pos ( left or right )
        public bool MusicEnabled  { get; set; } // Enables/Disables Music
        public bool SoundsEnabled { get; set; } // Enables/Disables Sound FX
        public int  MusicVolume   { get; set; } //
        public int  FxVolume      { get; set; } //

        //
        // Google Play Store
        public bool PlayServices { get; set; } // Enables Google Play Services
        public bool SignInStatus { get; set; } // Google Services sign in status (Android)
        public bool Achievements { get; set; } // Enables In-Game Achievements
        public bool Challenges   { get; set; } // Enables In-Game challenges
        public bool Events       { get; set; } // Enables In-Game events
    }

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

    private readonly Preferences _prefsObj;

    public Settings()
    {
        Trace.CheckPoint();

        try
        {
            _prefsObj = new Preferences( "config.json" );
        }
        catch ( Exception e )
        {
            Trace.Err( message: e.ToString() );
        }
    }

    public bool IsEnabled( string  pref ) => _prefsObj != null && _prefsObj.GetBoolean( pref );
    public bool IsDisabled( string pref ) => _prefsObj != null && !_prefsObj.GetBoolean( pref );

    public void Enable( string preference )
    {
        if ( _prefsObj != null )
        {
            _prefsObj.PutBoolean( preference, true );
            _prefsObj.Flush();
        }
    }

    public void Disable( string preference )
    {
        if ( _prefsObj != null )
        {
            _prefsObj.PutBoolean( preference, false );
            _prefsObj.Flush();
        }
    }

    public void ToggleState( string preference )
    {
        if ( _prefsObj != null )
        {
            _prefsObj.PutBoolean( preference, !_prefsObj.GetBoolean( preference ) );
            _prefsObj.Flush();
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

    private void ResetToDefaults()
    {
        Trace.CheckPoint();

        if ( _prefsObj != null )
        {
            _prefsObj.PutBoolean( key: DefaultOn,  val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: DefaultOff, val: PrefFalseDefault );

            // ---------- Configuration ----------
            _prefsObj.PutBoolean( key: ShaderProgram,  val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: UsingAshleyECS, val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: Box2DPhysics,   val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: Installed,      val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: ShowHints,      val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: Vibrations,     val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: JoystickLeft,   val: PrefTrueDefault );

            // --------------- Audio ---------------
            _prefsObj.PutInteger( key: FxVolume,    val: AudioData.DefaultFxVolume );
            _prefsObj.PutInteger( key: MusicVolume, val: AudioData.DefaultMusicVolume );
            _prefsObj.PutBoolean( key: MusicEnabled,  val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: SoundsEnabled, val: PrefTrueDefault );

            // ---------- Google Services ----------
            _prefsObj.PutBoolean( key: PlayServices, val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: Achievements, val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: Challenges,   val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: Events,       val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: SignInStatus, val: PrefFalseDefault );

            // ------------------- Development Flags -------------------
            _prefsObj.PutBoolean( key: MenuScene,        val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: LevelSelect,      val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: ScrollDemo,       val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: SpriteBoxes,      val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: TileBoxes,        val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: ButtonBoxes,      val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: ShowFPS,          val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: ShowDebug,        val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: Spawnpoints,      val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: MenuHeaps,        val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: CullSprites,      val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: GlProfiler,       val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: AndroidOnDesktop, val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: Autoplay,         val: PrefFalseDefault );
            _prefsObj.PutBoolean( key: DisableEnemies,   val: PrefTrueDefault );
            _prefsObj.PutBoolean( key: DisablePlayer,    val: PrefTrueDefault );

            _prefsObj.Flush();
        }
    }

    public void DebugReport()
    {
        Trace.CheckPoint();

        if ( _prefsObj == null )
        {
            Trace.Err( message: "Preferences Object is UNDEFINED." );
        }
        else
        {
            // ---------- Configuration ----------
            Trace.Info( message: ShaderProgram,  args: _prefsObj.GetBoolean( key: ShaderProgram ) );
            Trace.Info( message: UsingAshleyECS, args: _prefsObj.GetBoolean( key: UsingAshleyECS ) );
            Trace.Info( message: Box2DPhysics,   args: _prefsObj.GetBoolean( key: Box2DPhysics ) );
            Trace.Info( message: Installed,      args: _prefsObj.GetBoolean( key: Installed ) );
            Trace.Info( message: ShowHints,      args: _prefsObj.GetBoolean( key: ShowHints ) );
            Trace.Info( message: Vibrations,     args: _prefsObj.GetBoolean( key: Vibrations ) );
            Trace.Info( message: JoystickLeft,   args: _prefsObj.GetBoolean( key: JoystickLeft ) );

            // --------------- Audio ---------------
            Trace.Info( message: FxVolume,      args: _prefsObj.GetInteger( key: FxVolume ) );
            Trace.Info( message: MusicVolume,   args: _prefsObj.GetInteger( key: MusicVolume ) );
            Trace.Info( message: MusicEnabled,  args: _prefsObj.GetBoolean( key: MusicEnabled ) );
            Trace.Info( message: SoundsEnabled, args: _prefsObj.GetBoolean( key: SoundsEnabled ) );

            // ---------- Google Services ----------
            Trace.Info( message: PlayServices, args: _prefsObj.GetBoolean( key: PlayServices ) );
            Trace.Info( message: Achievements, args: _prefsObj.GetBoolean( key: Achievements ) );
            Trace.Info( message: Challenges,   args: _prefsObj.GetBoolean( key: Challenges ) );
            Trace.Info( message: Events,       args: _prefsObj.GetBoolean( key: Events ) );
            Trace.Info( message: SignInStatus, args: _prefsObj.GetBoolean( key: SignInStatus ) );

            // ------------------- Development Flags -------------------
            Trace.Info( message: MenuScene,        args: _prefsObj.GetBoolean( key: MenuScene ) );
            Trace.Info( message: LevelSelect,      args: _prefsObj.GetBoolean( key: LevelSelect ) );
            Trace.Info( message: ScrollDemo,       args: _prefsObj.GetBoolean( key: ScrollDemo ) );
            Trace.Info( message: SpriteBoxes,      args: _prefsObj.GetBoolean( key: SpriteBoxes ) );
            Trace.Info( message: TileBoxes,        args: _prefsObj.GetBoolean( key: TileBoxes ) );
            Trace.Info( message: ButtonBoxes,      args: _prefsObj.GetBoolean( key: ButtonBoxes ) );
            Trace.Info( message: ShowFPS,          args: _prefsObj.GetBoolean( key: ShowFPS ) );
            Trace.Info( message: ShowDebug,        args: _prefsObj.GetBoolean( key: ShowDebug ) );
            Trace.Info( message: Spawnpoints,      args: _prefsObj.GetBoolean( key: Spawnpoints ) );
            Trace.Info( message: MenuHeaps,        args: _prefsObj.GetBoolean( key: MenuHeaps ) );
            Trace.Info( message: CullSprites,      args: _prefsObj.GetBoolean( key: CullSprites ) );
            Trace.Info( message: GlProfiler,       args: _prefsObj.GetBoolean( key: GlProfiler ) );
            Trace.Info( message: AndroidOnDesktop, args: _prefsObj.GetBoolean( key: AndroidOnDesktop ) );
            Trace.Info( message: Autoplay,         args: _prefsObj.GetBoolean( key: Autoplay ) );
            Trace.Info( message: DisableEnemies,   args: _prefsObj.GetBoolean( key: DisableEnemies ) );
            Trace.Info( message: DisablePlayer,    args: _prefsObj.GetBoolean( key: DisablePlayer ) );
        }
    }

    public void Dispose()
    {
    }
}