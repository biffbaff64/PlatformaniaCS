// ##################################################


using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    // Google Play Store - to be removed
    public const string PlayServices = "play services";  // Enables Google Play Services
    public const string SignInStatus = "sign in status"; // Google Services sign in status (Android)
    public const string Achievements = "achievements";   // Enables In-Game Achievements
    public const string Challenges   = "challenges";     // Enables In-Game challenges
    public const string Events       = "events";         // Enables In-Game events

    /// <summary>
    /// Storage file details.
    /// </summary>
    private readonly string _filePath;

    private readonly string _propertiesFile;

    /// <summary>
    /// Dictionary holding preferences information.
    /// </summary>
    private readonly Dictionary< string, object > _preferences;

    /// <summary>
    /// In-App preferences management.
    /// </summary>
    public Settings()
    {
        Trace.CheckPoint();

        try
        {
            _filePath       = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "//.prefs//";
            _propertiesFile = "platformaniacs.json";
            _preferences    = new Dictionary< string, object >();

            if ( !File.Exists( _filePath + _propertiesFile ) )
            {
                if ( _preferences.Count <= 0 )
                {
                    ResetToDefaults();
                }

                WriteSettingsJson();
            }

            LoadPreferencesJson();
        }
        catch ( Exception e )
        {
            Trace.Err( message: e.ToString() );
        }
    }

    /// <summary>
    /// Load the Json file holding preferences data, and
    /// populate the Dictionary.
    /// </summary>
    private void LoadPreferencesJson()
    {
        _preferences.Clear();

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
            _preferences.Add( obj.Key, obj.Value );
        }
    }

    /// <summary>
    /// Write the preferences Dictionary to file.
    /// </summary>
    private void WriteSettingsJson()
    {
        var opt = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize( _preferences, opt );

        File.WriteAllText( _filePath + _propertiesFile, json );
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsEnabled( string pref ) => GetBoolean( pref );

    /// <summary>
    /// 
    /// </summary>
    public bool IsDisabled( string pref ) => !GetBoolean( pref );

    /// <summary>
    /// Enable the specified preference.
    /// </summary>
    public void Enable( string preference )
    {
        if ( _preferences.ContainsKey( preference ) )
        {
            _preferences[ preference ] = true;

            WriteSettingsJson();
        }
    }

    /// <summary>
    /// Disable the specified preference.
    /// </summary>
    public void Disable( string preference )
    {
        if ( _preferences.ContainsKey( preference ) )
        {
            _preferences[ preference ] = false;

            WriteSettingsJson();
        }
    }

    /// <summary>
    /// Flip the state of the specified preference.
    /// </summary>
    public void ToggleState( string preference )
    {
        if ( _preferences.ContainsKey( preference )
             && _preferences[ preference ] is bool )
        {
            _preferences[ preference ] = !( bool )_preferences[ preference ];

            WriteSettingsJson();
        }
    }

    // TODO:
    // Organise the following PutXXXX and GetXXXX methods better.
    // They all do essentially the same thing except for
    // the TYPE of argument 'val'.
    // These methods have been included as fillers until library is working.

    /// <summary>
    /// 
    /// </summary>
    public void PutBoolean( string key, bool val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );
        }
        else
        {
            _preferences[ key ] = val;
        }

        WriteSettingsJson();
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutInteger( string key, int val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );
        }
        else
        {
            _preferences[ key ] = val;
        }

        WriteSettingsJson();
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutLong( string key, long val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );
        }
        else
        {
            _preferences[ key ] = val;
        }

        WriteSettingsJson();
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutFloat( string key, float val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );
        }
        else
        {
            _preferences[ key ] = val;
        }

        WriteSettingsJson();
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutString( string key, string val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );
        }
        else
        {
            _preferences[ key ] = val;
        }

        WriteSettingsJson();
    }

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
    public bool GetBoolean( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return ( bool )_preferences[ key ];
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
    public int GetInteger( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return ( int )_preferences[ key ];
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
    public long GetLong( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return ( long )_preferences[ key ];
        }

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
    public float GetFloat( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return ( float )_preferences[ key ];
        }

        return 0f;
    }

    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
    public string GetString( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return ( string )_preferences[ key ];
        }

        return string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    public void FreshInstallCheck()
    {
        if ( IsEnabled( Installed ) is false )
        {
            ResetToDefaults();

            Stats.ResetAllMeters();

            Enable( Installed );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetToDefaults()
    {
        Trace.CheckPoint();

        _preferences.Clear();

        // ---------- Configuration ----------
        _preferences.Add( ShaderProgram,  PrefFalseDefault );
        _preferences.Add( UsingAshleyECS, PrefFalseDefault );
        _preferences.Add( Box2DPhysics,   PrefTrueDefault );
        _preferences.Add( Installed,      PrefFalseDefault );
        _preferences.Add( ShowHints,      PrefTrueDefault );
        _preferences.Add( Vibrations,     PrefTrueDefault );
        _preferences.Add( JoystickLeft,   PrefTrueDefault );

        // --------------- Audio ---------------
        _preferences.Add( FxVolume,      AudioData.DefaultFxVolume );
        _preferences.Add( MusicVolume,   AudioData.DefaultMusicVolume );
        _preferences.Add( MusicEnabled,  PrefTrueDefault );
        _preferences.Add( SoundsEnabled, PrefTrueDefault );

        // ---------- Google Services ----------
        _preferences.Add( PlayServices, PrefFalseDefault );
        _preferences.Add( Achievements, PrefFalseDefault );
        _preferences.Add( Challenges,   PrefFalseDefault );
        _preferences.Add( Events,       PrefFalseDefault );
        _preferences.Add( SignInStatus, PrefFalseDefault );

        // ------------------- Development Flags -------------------
        _preferences.Add( MenuScene,        PrefTrueDefault );
        _preferences.Add( LevelSelect,      PrefTrueDefault );
        _preferences.Add( ScrollDemo,       PrefFalseDefault );
        _preferences.Add( SpriteBoxes,      PrefFalseDefault );
        _preferences.Add( TileBoxes,        PrefFalseDefault );
        _preferences.Add( ButtonBoxes,      PrefFalseDefault );
        _preferences.Add( ShowFPS,          PrefFalseDefault );
        _preferences.Add( ShowDebug,        PrefFalseDefault );
        _preferences.Add( Spawnpoints,      PrefFalseDefault );
        _preferences.Add( MenuHeaps,        PrefFalseDefault );
        _preferences.Add( CullSprites,      PrefTrueDefault );
        _preferences.Add( GlProfiler,       PrefFalseDefault );
        _preferences.Add( AndroidOnDesktop, PrefFalseDefault );
        _preferences.Add( Autoplay,         PrefFalseDefault );
        _preferences.Add( DisableEnemies,   PrefTrueDefault );
        _preferences.Add( DisablePlayer,    PrefTrueDefault );
    }

    /// <summary>
    /// 
    /// </summary>
    public void DebugReport()
    {
        Trace.CheckPoint();

        // ---------- Configuration ----------
        Trace.Info( message: ShaderProgram + " : " + GetBoolean( key: ShaderProgram ) );
        Trace.Info( message: UsingAshleyECS + " : " + GetBoolean( key: UsingAshleyECS ) );
        Trace.Info( message: Box2DPhysics + " : " + GetBoolean( key: Box2DPhysics ) );
        Trace.Info( message: Installed + " : " + GetBoolean( key: Installed ) );
        Trace.Info( message: ShowHints + " : " + GetBoolean( key: ShowHints ) );
        Trace.Info( message: Vibrations + " : " + GetBoolean( key: Vibrations ) );
        Trace.Info( message: JoystickLeft + " : " + GetBoolean( key: JoystickLeft ) );

        // --------------- Audio ---------------
        Trace.Info( message: FxVolume + " : " + GetInteger( key: FxVolume ) );
        Trace.Info( message: MusicVolume + " : " + GetInteger( key: MusicVolume ) );
        Trace.Info( message: MusicEnabled + " : " + GetBoolean( key: MusicEnabled ) );
        Trace.Info( message: SoundsEnabled + " : " + GetBoolean( key: SoundsEnabled ) );

        // ---------- Google Services ----------
        Trace.Info( message: PlayServices + " : " + GetBoolean( key: PlayServices ) );
        Trace.Info( message: Achievements + " : " + GetBoolean( key: Achievements ) );
        Trace.Info( message: Challenges + " : " + GetBoolean( key: Challenges ) );
        Trace.Info( message: Events + " : " + GetBoolean( key: Events ) );
        Trace.Info( message: SignInStatus + " : " + GetBoolean( key: SignInStatus ) );

        // ------------------- Development Flags -------------------
        Trace.Info( message: MenuScene + " : " + GetBoolean( key: MenuScene ) );
        Trace.Info( message: LevelSelect + " : " + GetBoolean( key: LevelSelect ) );
        Trace.Info( message: ScrollDemo + " : " + GetBoolean( key: ScrollDemo ) );
        Trace.Info( message: SpriteBoxes + " : " + GetBoolean( key: SpriteBoxes ) );
        Trace.Info( message: TileBoxes + " : " + GetBoolean( key: TileBoxes ) );
        Trace.Info( message: ButtonBoxes + " : " + GetBoolean( key: ButtonBoxes ) );
        Trace.Info( message: ShowFPS + " : " + GetBoolean( key: ShowFPS ) );
        Trace.Info( message: ShowDebug + " : " + GetBoolean( key: ShowDebug ) );
        Trace.Info( message: Spawnpoints + " : " + GetBoolean( key: Spawnpoints ) );
        Trace.Info( message: MenuHeaps + " : " + GetBoolean( key: MenuHeaps ) );
        Trace.Info( message: CullSprites + " : " + GetBoolean( key: CullSprites ) );
        Trace.Info( message: GlProfiler + " : " + GetBoolean( key: GlProfiler ) );
        Trace.Info( message: AndroidOnDesktop + " : " + GetBoolean( key: AndroidOnDesktop ) );
        Trace.Info( message: Autoplay + " : " + GetBoolean( key: Autoplay ) );
        Trace.Info( message: DisableEnemies + " : " + GetBoolean( key: DisableEnemies ) );
        Trace.Info( message: DisablePlayer + " : " + GetBoolean( key: DisablePlayer ) );
    }

    public void Dispose()
    {
    }
}