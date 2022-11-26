// ##################################################

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text.Json;
using System.Xml.Linq;

using Newtonsoft.Json;
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

    private readonly string _filePath;
    private readonly string _propertiesFile;

    private Dictionary< string, object > _preferences;

    // ----------------------------------------------------

    private class Entry
    {
        public string Key   { get; set; }
        public object Value { get; set; }
    }

    // ----------------------------------------------------

    /// <summary>
    /// In-App preferences management.
    /// </summary>
    public Settings()
    {
        Trace.CheckPoint();

        try
        {
            _filePath = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "//.prefs//";

            _propertiesFile = "platformaniacs.json";
            _preferences    = new Dictionary< string, object >();

            if ( !File.Exists( _filePath + _propertiesFile ) )
            {
                if ( _preferences.Count <= 0 )
                {
                    ResetToDefaults();
                }

                WritePreferences();
            }

            LoadPreferences();
        }
        catch ( Exception e )
        {
            Trace.Err( message: e.ToString() );
        }
    }

    /// <summary>
    /// Load the file holding preferences data, and
    /// populate the Dictionary.
    /// </summary>
    private void LoadPreferences()
    {
//        _preferences.Clear();

//        var json = File.ReadAllText( _filePath + _propertiesFile );

//        var parent = JObject.Parse( json );
//        var resultData = parent.Value< JObject >( "properties" ).Properties();

//        var enumerable = resultData as JProperty[] ?? resultData.ToArray();

//        var resultDict = enumerable.ToDictionary
//            (
//             k => k.Name,
//             v => v.Value
//            );

//        foreach ( var obj in resultDict )
//        {
//            _preferences.Add( obj.Key, obj.Value );
//        }
    }

    /// <summary>
    /// Write the preferences Dictionary to file.
    /// </summary>
    /// TODO: Learn the CORRECT way of doing this. Json Serialisation ??
    private void WritePreferences()
    {
//        var str = "{\n\"properties\": {\n";
//        var entryNum = 1;

//        foreach ( var entry in _preferences )
//        {
//            str += "\"entry" + entryNum++ + "\": {\n";
//            str += "\"key\": ";
//            str += "\"" + entry.Key + "\"";
//            str += ",\n";
//            str += "\"value\": ";
//            str += "\"" + entry.Value + "\"\n";
//            str += "},\n";
//        }

//        str += "}\n}\n";

//        File.WriteAllText( _filePath + _propertiesFile, str.ToLower() );
    }

    /// <summary>
    /// Returns TRUE if the specified preference is enabled.
    /// </summary>
    public bool IsEnabled( string pref ) => GetBoolean( pref );

    /// <summary>
    /// Returns TRUE if the specified preference is disabled.
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

            WritePreferences();
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

            WritePreferences();
        }
    }

    /// <summary>
    /// Flip the state of the specified preference.
    /// </summary>
    public void ToggleState( string preference )
    {
        if ( _preferences.ContainsKey( preference ) && _preferences[ preference ] is bool )
        {
            _preferences[ preference ] = !( bool )_preferences[ preference ];

            WritePreferences();
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

        WritePreferences();
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

        WritePreferences();
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

        WritePreferences();
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

        WritePreferences();
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

        WritePreferences();
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
        _preferences.Add( MenuScene,      PrefTrueDefault );
        _preferences.Add( LevelSelect,    PrefTrueDefault );
        _preferences.Add( ScrollDemo,     PrefFalseDefault );
        _preferences.Add( SpriteBoxes,    PrefFalseDefault );
        _preferences.Add( TileBoxes,      PrefFalseDefault );
        _preferences.Add( ButtonBoxes,    PrefFalseDefault );
        _preferences.Add( ShowFPS,        PrefFalseDefault );
        _preferences.Add( ShowDebug,      PrefFalseDefault );
        _preferences.Add( Spawnpoints,    PrefFalseDefault );
        _preferences.Add( CullSprites,    PrefTrueDefault );
        _preferences.Add( Autoplay,       PrefFalseDefault );
        _preferences.Add( DisableEnemies, PrefTrueDefault );
        _preferences.Add( DisablePlayer,  PrefTrueDefault );
    }

    /// <summary>
    /// 
    /// </summary>
    public void DebugReport()
    {
        Trace.CheckPoint();

        // ---------- Configuration ----------
        Trace.Info( message: ShaderProgram + " : " + ( GetBoolean( ShaderProgram ) ? "true" : "false" ) );
        Trace.Info( message: UsingAshleyECS + " : " + GetBoolean( UsingAshleyECS ) );
        Trace.Info( message: Box2DPhysics + " : " + GetBoolean( Box2DPhysics ) );
        Trace.Info( message: Installed + " : " + GetBoolean( Installed ) );
        Trace.Info( message: ShowHints + " : " + GetBoolean( ShowHints ) );
        Trace.Info( message: Vibrations + " : " + GetBoolean( Vibrations ) );
        Trace.Info( message: JoystickLeft + " : " + GetBoolean( JoystickLeft ) );

        // --------------- Audio ---------------
        Trace.Info( message: FxVolume + " : " + GetInteger( FxVolume ) );
        Trace.Info( message: MusicVolume + " : " + GetInteger( MusicVolume ) );
        Trace.Info( message: MusicEnabled + " : " + GetBoolean( MusicEnabled ) );
        Trace.Info( message: SoundsEnabled + " : " + GetBoolean( SoundsEnabled ) );

        // ---------- Google Services ----------
        Trace.Info( message: PlayServices + " : " + GetBoolean( PlayServices ) );
        Trace.Info( message: Achievements + " : " + GetBoolean( Achievements ) );
        Trace.Info( message: Challenges + " : " + GetBoolean( Challenges ) );
        Trace.Info( message: Events + " : " + GetBoolean( Events ) );
        Trace.Info( message: SignInStatus + " : " + GetBoolean( SignInStatus ) );

        // ------------------- Development Flags -------------------
        Trace.Info( message: MenuScene + " : " + GetBoolean( MenuScene ) );
        Trace.Info( message: LevelSelect + " : " + GetBoolean( LevelSelect ) );
        Trace.Info( message: ScrollDemo + " : " + GetBoolean( ScrollDemo ) );
        Trace.Info( message: SpriteBoxes + " : " + GetBoolean( SpriteBoxes ) );
        Trace.Info( message: TileBoxes + " : " + GetBoolean( TileBoxes ) );
        Trace.Info( message: ButtonBoxes + " : " + GetBoolean( ButtonBoxes ) );
        Trace.Info( message: ShowFPS + " : " + GetBoolean( ShowFPS ) );
        Trace.Info( message: ShowDebug + " : " + GetBoolean( ShowDebug ) );
        Trace.Info( message: Spawnpoints + " : " + GetBoolean( Spawnpoints ) );
        Trace.Info( message: CullSprites + " : " + GetBoolean( CullSprites ) );
        Trace.Info( message: Autoplay + " : " + GetBoolean( Autoplay ) );
        Trace.Info( message: DisableEnemies + " : " + GetBoolean( DisableEnemies ) );
        Trace.Info( message: DisablePlayer + " : " + GetBoolean( DisablePlayer ) );
    }

    public void Dispose()
    {
    }
}