// ##################################################

using System.IO;
using System.Linq;

// ##################################################

namespace Lugh.Logging;

public class Preferences : IDisposable
{
    private readonly string _filePath;
    private readonly string _propertiesFile;

    private Dictionary< string, object > _preferences;

    public record Prefs
    {
        [JsonProperty( "Key" )]
        public string Key { get; set; }

        [JsonProperty( "Value" )]
        public object Value { get; set; }
    }

    public record Root
    {
        [JsonProperty( "prefs" )]
        public List< Prefs > PrefsList { get; set; } = new List< Prefs >();
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
                    var file = File.Create( _filePath + _propertiesFile );

                    file.Close();

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

        using var reader = new StreamReader( _filePath + _propertiesFile );

        var json = reader.ReadToEnd();

        var items = JsonConvert.DeserializeObject< Root >( json );

        foreach ( var pref in items.PrefsList )
        {
            _preferences.Add( pref.Key, pref.Value );
        }
    }

    /// <summary>
    /// Saves the preferences Dictionary to file.
    /// Any updated preferences will not persist between sessions
    /// unless this is called.
    /// </summary>
    public void Flush()
    {
        Trace.CheckPoint();

        var values = _preferences.Keys.Select
        (
            k => new
            {
                    Key   = k,
                    Value = _preferences[ k ]
            }
        );

        var toSerialise = new { prefs = values.ToList() };
        var serialised  = JsonConvert.SerializeObject( toSerialise, Formatting.Indented );

        File.WriteAllText( _filePath + _propertiesFile, serialised );
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
    public Dictionary< string, object > GetDictionary() => _preferences;

    /// <summary>
    /// Sets the preferences Dictionary to the supplied one.
    /// NB: Deliberately chosen Setter over property.
    /// </summary>
    public void SetDictionary( Dictionary< string, object > dictionary )
    {
        _preferences = dictionary;
    }

    /// <summary>
    /// Update the specified preference with the supplied value.
    /// If the specified preference does not exist it will be
    /// added to the preference list.
    /// </summary>
    /// <param name="key">The preference name.</param>
    /// <param name="val">The value.</param>
    public void Put( string key, object val )
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
    
    public bool GetBoolean( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return ( bool )_preferences[ key ];
        }

        return false;
    }

    public int GetInteger( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return Convert.ToInt32( _preferences[ key ] );
        }

        return 0;
    }

    public long GetLong( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return Convert.ToInt64( _preferences[ key ] );
        }

        return 0;
    }

    public float GetFloat( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return Convert.ToSingle( _preferences[ key ] );
        }

        return 0f;
    }

    public string GetString( string key )
    {
        if ( _preferences.ContainsKey( key ) )
        {
            return Convert.ToString( _preferences[ key ] );
        }

        return string.Empty;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose( bool disposing )
    {
        if ( disposing )
        {
            if ( _preferences != null )
            {
                _preferences.Clear();
                _preferences = null;
            }
        }
    }
}