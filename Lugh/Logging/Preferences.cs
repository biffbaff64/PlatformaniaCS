// ##################################################

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ILNumerics.Core.Platforms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

// ##################################################

namespace Lugh.Logging;

public class Preferences
{
    public class Entry
    {
        [JsonProperty( "key" )]
        public string Key { get; set; }

        [JsonProperty( "value" )]
        public object Value { get; set; }
    }

    public class Root
    {
        public List< Entry > Entries { get; set; }
    }

    private readonly string _filePath;
    private readonly string _propertiesFile;

    private Dictionary< string, object > _properties;

    public Preferences( string fileName )
    {
        Trace.CheckPoint();

        _filePath       = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "//.prefs//";
        _propertiesFile = fileName;
        _properties     = new Dictionary< string, object >();

        if ( !File.Exists( _filePath + _propertiesFile ) )
        {
            CreateSettingsFile();
        }

        LoadJson();
    }

    private void LoadJson()
    {
        using var r = new StreamReader( _filePath + _propertiesFile );

        var json    = r.ReadToEnd();
        var objList = JsonSerializer.Deserialize< Root[] >( json );
        
        _properties.Clear();

        foreach ( var obj in objList )
        {
            foreach ( var entry in obj.Entries )
            {
                _properties.Add( entry.Key, entry.Value );
            }
        }

        r.Close();
    }

    private void CreateSettingsFile()
    {
    }

    // TODO:
    // Organise the following PutXXXX methods better.
    // They all do essentially the same thing except for
    // the TYPE of argument 'val'.
    // These methods have been put as fillers until library is working.
    public void PutBoolean( string key, bool val )
    {
        if ( !_properties.ContainsKey( key ) )
        {
            _properties.Add( key, val );
        }
        else
        {
            _properties[ key ] = val;
        }
    }

    public void PutInteger( string key, int val )
    {
        if ( !_properties.ContainsKey( key ) )
        {
            _properties.Add( key, val );
        }
        else
        {
            _properties[ key ] = val;
        }
    }

    public void PutLong( string key, long val )
    {
        if ( !_properties.ContainsKey( key ) )
        {
            _properties.Add( key, val );
        }
        else
        {
            _properties[ key ] = val;
        }
    }

    public void PutFloat( string key, float val )
    {
        if ( !_properties.ContainsKey( key ) )
        {
            _properties.Add( key, val );
        }
        else
        {
            _properties[ key ] = val;
        }
    }

    public void PutString( string key, string val )
    {
        if ( !_properties.ContainsKey( key ) )
        {
            _properties.Add( key, val );
        }
        else
        {
            _properties[ key ] = val;
        }
    }

    public bool GetBoolean( string key )
    {
        if ( _properties.ContainsKey( key ) )
        {
            return ( bool )_properties[ key ];
        }

        return false;
    }

    public int GetInteger( string key )
    {
        if ( _properties.ContainsKey( key ) )
        {
            return ( int )_properties[ key ];
        }

        return 0;
    }

    public long GetLong( string key )
    {
        if ( _properties.ContainsKey( key ) )
        {
            return ( long )_properties[ key ];
        }

        return 0;
    }

    public float GetFloat( string key )
    {
        if ( _properties.ContainsKey( key ) )
        {
            return ( float )_properties[ key ];
        }

        return 0f;
    }

    public string GetString( string key )
    {
        if ( _properties.ContainsKey( key ) )
        {
            return ( string )_properties[ key ];
        }

        return "";
    }

    public Dictionary< string, object > Get() => _properties;

    public bool Contains( string key ) => _properties.ContainsKey( key );

    public void Clear()
    {
        _properties.Clear();
    }

    public void Remove( string key )
    {
    }

    public void Flush()
    {
    }
}