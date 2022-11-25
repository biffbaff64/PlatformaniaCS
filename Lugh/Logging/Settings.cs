// ##################################################


using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using Newtonsoft.Json.Linq;

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

    private readonly Preferences _prefsObj;

    private readonly string _filePath;
    private readonly string _propertiesFile;

    private readonly Dictionary< string, object > _properties;

    public Settings()
    {
        Trace.CheckPoint();

        try
        {
//            _prefsObj = new Preferences( "config.json" );

            _filePath       = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "//.prefs//";
            _propertiesFile = "platformaniacs.json";
            _properties     = new Dictionary< string, object >();

            if ( !File.Exists( _filePath + _propertiesFile ) )
            {
                CreateSettingsFile();
            }

            LoadJson();
        }
        catch ( Exception e )
        {
            Trace.Err( message: e.ToString() );
        }
    }

    private void LoadJson()
    {
        _properties.Clear();

        var json = File.ReadAllText( _filePath + _propertiesFile );

        var parent     = JObject.Parse( json );
        var resultData = parent.Value< JObject >( "properties" ).Properties();

        var enumerable = resultData as JProperty[] ?? resultData.ToArray();

        var resultDict = enumerable.ToDictionary
            (
             k => k.Name,
             v => v.Value
            );

        foreach ( var obj in resultDict )
        {
            _properties.Add( obj.Key, obj.Value );
        }

        Trace.Info( "Objects found: " + _properties.Count );
    }

    private void CreateSettingsFile()
    {
        var opt = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize< Dictionary< string, object > >( _properties, opt );

        File.WriteAllText( _filePath + _propertiesFile, json );
    }

    public bool IsEnabled( string  pref ) => _prefsObj != null && _prefsObj.GetBoolean( pref );
    public bool IsDisabled( string pref ) => _prefsObj != null && !_prefsObj.GetBoolean( pref );

    public void Enable( string preference )
    {
//        if ( _prefsObj != null )
//        {
//            _prefsObj.PutBoolean( preference, true );
//            _prefsObj.Flush();
//        }
    }

    public void Disable( string preference )
    {
//        if ( _prefsObj != null )
//        {
//            _prefsObj.PutBoolean( preference, false );
//            _prefsObj.Flush();
//        }
    }

    public void ToggleState( string preference )
    {
//        if ( _prefsObj != null )
//        {
//            _prefsObj.PutBoolean( preference, !_prefsObj.GetBoolean( preference ) );
//            _prefsObj.Flush();
//        }
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

        _properties.Clear();

        // ---------- Configuration ----------
        _properties.Add( ShaderProgram,  PrefFalseDefault );
        _properties.Add( UsingAshleyECS, PrefFalseDefault );
        _properties.Add( Box2DPhysics,   PrefTrueDefault );
        _properties.Add( Installed,      PrefFalseDefault );
        _properties.Add( ShowHints,      PrefTrueDefault );
        _properties.Add( Vibrations,     PrefTrueDefault );
        _properties.Add( JoystickLeft,   PrefTrueDefault );

        // --------------- Audio ---------------
        _properties.Add( FxVolume,      AudioData.DefaultFxVolume );
        _properties.Add( MusicVolume,   AudioData.DefaultMusicVolume );
        _properties.Add( MusicEnabled,  PrefTrueDefault );
        _properties.Add( SoundsEnabled, PrefTrueDefault );

        // ---------- Google Services ----------
        _properties.Add( PlayServices, PrefFalseDefault );
        _properties.Add( Achievements, PrefFalseDefault );
        _properties.Add( Challenges,   PrefFalseDefault );
        _properties.Add( Events,       PrefFalseDefault );
        _properties.Add( SignInStatus, PrefFalseDefault );

        // ------------------- Development Flags -------------------
        _properties.Add( MenuScene,        PrefTrueDefault );
        _properties.Add( LevelSelect,      PrefTrueDefault );
        _properties.Add( ScrollDemo,       PrefFalseDefault );
        _properties.Add( SpriteBoxes,      PrefFalseDefault );
        _properties.Add( TileBoxes,        PrefFalseDefault );
        _properties.Add( ButtonBoxes,      PrefFalseDefault );
        _properties.Add( ShowFPS,          PrefFalseDefault );
        _properties.Add( ShowDebug,        PrefFalseDefault );
        _properties.Add( Spawnpoints,      PrefFalseDefault );
        _properties.Add( MenuHeaps,        PrefFalseDefault );
        _properties.Add( CullSprites,      PrefTrueDefault );
        _properties.Add( GlProfiler,       PrefFalseDefault );
        _properties.Add( AndroidOnDesktop, PrefFalseDefault );
        _properties.Add( Autoplay,         PrefFalseDefault );
        _properties.Add( DisableEnemies,   PrefTrueDefault );
        _properties.Add( DisablePlayer,    PrefTrueDefault );

        // ================================================

//        if ( _prefsObj != null )
//        {
//            _prefsObj.PutBoolean( key: DefaultOn,  val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: DefaultOff, val: PrefFalseDefault );

            // ---------- Configuration ----------
//            _prefsObj.PutBoolean( key: ShaderProgram,  val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: UsingAshleyECS, val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: Box2DPhysics,   val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: Installed,      val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: ShowHints,      val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: Vibrations,     val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: JoystickLeft,   val: PrefTrueDefault );

            // --------------- Audio ---------------
//            _prefsObj.PutInteger( key: FxVolume,    val: AudioData.DefaultFxVolume );
//            _prefsObj.PutInteger( key: MusicVolume, val: AudioData.DefaultMusicVolume );
//            _prefsObj.PutBoolean( key: MusicEnabled,  val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: SoundsEnabled, val: PrefTrueDefault );

            // ---------- Google Services ----------
//            _prefsObj.PutBoolean( key: PlayServices, val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: Achievements, val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: Challenges,   val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: Events,       val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: SignInStatus, val: PrefFalseDefault );

            // ------------------- Development Flags -------------------
//            _prefsObj.PutBoolean( key: MenuScene,        val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: LevelSelect,      val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: ScrollDemo,       val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: SpriteBoxes,      val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: TileBoxes,        val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: ButtonBoxes,      val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: ShowFPS,          val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: ShowDebug,        val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: Spawnpoints,      val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: MenuHeaps,        val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: CullSprites,      val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: GlProfiler,       val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: AndroidOnDesktop, val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: Autoplay,         val: PrefFalseDefault );
//            _prefsObj.PutBoolean( key: DisableEnemies,   val: PrefTrueDefault );
//            _prefsObj.PutBoolean( key: DisablePlayer,    val: PrefTrueDefault );

//            _prefsObj.Flush();
//        }
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
            Trace.Info( message: ShaderProgram + " : " + _prefsObj.GetBoolean( key: ShaderProgram ) );
            Trace.Info( message: UsingAshleyECS + " : " + _prefsObj.GetBoolean( key: UsingAshleyECS ) );
            Trace.Info( message: Box2DPhysics + " : " + _prefsObj.GetBoolean( key: Box2DPhysics ) );
            Trace.Info( message: Installed + " : " + _prefsObj.GetBoolean( key: Installed ) );
            Trace.Info( message: ShowHints + " : " + _prefsObj.GetBoolean( key: ShowHints ) );
            Trace.Info( message: Vibrations + " : " + _prefsObj.GetBoolean( key: Vibrations ) );
            Trace.Info( message: JoystickLeft + " : " + _prefsObj.GetBoolean( key: JoystickLeft ) );

            // --------------- Audio ---------------
            Trace.Info( message: FxVolume + " : " + _prefsObj.GetInteger( key: FxVolume ) );
            Trace.Info( message: MusicVolume + " : " + _prefsObj.GetInteger( key: MusicVolume ) );
            Trace.Info( message: MusicEnabled + " : " + _prefsObj.GetBoolean( key: MusicEnabled ) );
            Trace.Info( message: SoundsEnabled + " : " + _prefsObj.GetBoolean( key: SoundsEnabled ) );

            // ---------- Google Services ----------
            Trace.Info( message: PlayServices + " : " + _prefsObj.GetBoolean( key: PlayServices ) );
            Trace.Info( message: Achievements + " : " + _prefsObj.GetBoolean( key: Achievements ) );
            Trace.Info( message: Challenges + " : " + _prefsObj.GetBoolean( key: Challenges ) );
            Trace.Info( message: Events + " : " + _prefsObj.GetBoolean( key: Events ) );
            Trace.Info( message: SignInStatus + " : " + _prefsObj.GetBoolean( key: SignInStatus ) );

            // ------------------- Development Flags -------------------
            Trace.Info( message: MenuScene + " : " + _prefsObj.GetBoolean( key: MenuScene ) );
            Trace.Info( message: LevelSelect + " : " + _prefsObj.GetBoolean( key: LevelSelect ) );
            Trace.Info( message: ScrollDemo + " : " + _prefsObj.GetBoolean( key: ScrollDemo ) );
            Trace.Info( message: SpriteBoxes + " : " + _prefsObj.GetBoolean( key: SpriteBoxes ) );
            Trace.Info( message: TileBoxes + " : " + _prefsObj.GetBoolean( key: TileBoxes ) );
            Trace.Info( message: ButtonBoxes + " : " + _prefsObj.GetBoolean( key: ButtonBoxes ) );
            Trace.Info( message: ShowFPS + " : " + _prefsObj.GetBoolean( key: ShowFPS ) );
            Trace.Info( message: ShowDebug + " : " + _prefsObj.GetBoolean( key: ShowDebug ) );
            Trace.Info( message: Spawnpoints + " : " + _prefsObj.GetBoolean( key: Spawnpoints ) );
            Trace.Info( message: MenuHeaps + " : " + _prefsObj.GetBoolean( key: MenuHeaps ) );
            Trace.Info( message: CullSprites + " : " + _prefsObj.GetBoolean( key: CullSprites ) );
            Trace.Info( message: GlProfiler + " : " + _prefsObj.GetBoolean( key: GlProfiler ) );
            Trace.Info( message: AndroidOnDesktop + " : " + _prefsObj.GetBoolean( key: AndroidOnDesktop ) );
            Trace.Info( message: Autoplay + " : " + _prefsObj.GetBoolean( key: Autoplay ) );
            Trace.Info( message: DisableEnemies + " : " + _prefsObj.GetBoolean( key: DisableEnemies ) );
            Trace.Info( message: DisablePlayer + " : " + _prefsObj.GetBoolean( key: DisablePlayer ) );
        }
    }

    public void Dispose()
    {
    }
}