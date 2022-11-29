// ##################################################

// ##################################################

using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace Lugh.Logging;

public class JsonSettings
{
    // ----------------------------------------------------

    private readonly string _filePath;
    private readonly string _propertiesFile;

    private Dictionary< string, object > _preferences;

    // ----------------------------------------------------

    public JsonSettings( string filename )
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
                    InitialiseSettings();
                }

                WriteJson();
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

    private void WriteJson()
    {
    }

    private void InitialiseSettings()
    {
    }
}
