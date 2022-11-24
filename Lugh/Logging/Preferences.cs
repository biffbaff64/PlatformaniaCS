// ##################################################

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using Newtonsoft.Json.Linq;

// ##################################################

namespace Lugh.Logging;

public class Preferences
{
    private readonly string _filePath;
    private readonly string _propertiesFile;

    private readonly Dictionary< string, object > _properties;

    private readonly Settings.Gs _gSettings = new Settings.Gs();
    
    public Preferences( string fileName )
    {
        Trace.CheckPoint();

        _filePath       = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile ) + "//.prefs//";
        _propertiesFile = fileName;
        _properties     = new Dictionary< string, object >();

        if ( !File.Exists( _filePath + "GSettings.json" ) )
        {
            CreateGSettingsFile();
        }

        LoadGSettings();
        
        LoadJson();
    }

    private void LoadGSettings()
    {
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

    private void CreateGSettingsFile()
    {
        var opt = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize< Settings.Gs >( _gSettings, opt );
        
        File.WriteAllText( _filePath + "GSettings.json", json );
    }

    // TODO:
    // Organise the following PutXXXX and GetXXXX methods better.
    // They all do essentially the same thing except for
    // the TYPE of argument 'val'.
    // These methods have been included as fillers until library is working.
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