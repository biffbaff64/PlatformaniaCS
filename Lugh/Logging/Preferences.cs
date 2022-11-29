// ##################################################

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

// ##################################################

namespace Lugh.Logging;

public class Preferences : IDisposable
{
    //
    // Defaults
    public const bool PrefFalseDefault = false;
    public const bool PrefTrueDefault  = true;

    public const string DefaultOn  = "default on";
    public const string DefaultOff = "default off";

    // ----------------------------------------------------

    private readonly string _filePath;
    private readonly string _propertiesFile;

    private Dictionary< string, object > _preferences;

    // ----------------------------------------------------

    public class Entry
    {
        public string Key   { get; set; }
        public object Value { get; set; }
    }

    // ----------------------------------------------------

    /// <summary>
    /// In-App preferences management.
    /// </summary>
    public Preferences( string filename )
    {
        Trace.CheckPoint();

        try
        {
            _filePath = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "//.prefs//";

            _propertiesFile = filename;
            _preferences    = new Dictionary< string, object >();

            if ( !File.Exists( _filePath + _propertiesFile ) )
            {
                if ( _preferences.Count <= 0 )
                {
                    File.Create( _filePath + _propertiesFile );

                    Flush();
                }
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
    /// Saves the preferences Dictionary to file.
    /// </summary>
    private void Flush()
    {
    }

    /// <summary>
    /// Clear the preferences Dictionary.
    /// </summary>
    public void Clear()
    {
        _preferences.Clear();
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
        }
    }

    /// <summary>
    /// Returns the current preferences Dictionary.
    /// NB: Deliberately chosen Getter over property.
    /// </summary>
    public Dictionary< string, object > Get() => _preferences;

    /// <summary>
    /// Sets the preferences Dictionary to the supplied one.
    /// NB: Deliberately chosen Setter over property.
    /// </summary>
    public void Set( Dictionary< string, object > dictionary )
    {
        _preferences = dictionary;
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

            Trace.Dbg( message: "New preference added: ", args: key );
        }
        else
        {
            _preferences[ key ] = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutInteger( string key, int val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );

            Trace.Dbg( message: "New preference added: ", args: key );
        }
        else
        {
            _preferences[ key ] = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutLong( string key, long val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );

            Trace.Dbg( message: "New preference added: ", args: key );
        }
        else
        {
            _preferences[ key ] = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutFloat( string key, float val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );

            Trace.Dbg( message: "New preference added: ", args: key );
        }
        else
        {
            _preferences[ key ] = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PutString( string key, string val )
    {
        if ( !_preferences.ContainsKey( key ) )
        {
            _preferences.Add( key, val );

            Trace.Dbg( message: "New preference added: ", args: key );
        }
        else
        {
            _preferences[ key ] = val;
        }
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
    
    public void Dispose()
    {
    }
}