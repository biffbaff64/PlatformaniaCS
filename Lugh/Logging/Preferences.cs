// ##################################################

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

// ##################################################

namespace Lugh.Logging;

public class Preferences
{
    private readonly string                       _filePath;
    private readonly string                       _propertiesFile;
    private readonly Dictionary< string, object > _properties;

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

        LoadXml();
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
    }

    public void Remove( string key )
    {
    }

    public void Flush()
    {
    }

    private void LoadXml()
    {
        XmlReaderSettings settings = new XmlReaderSettings
        {
                IgnoreWhitespace = true
        };

        // From: https://dotnetcoretutorials.com/2020/04/23/how-to-parse-xml-in-net-core/
        
        using ( var fileStream = File.Open( _filePath + _propertiesFile, FileMode.Open ) )
        {
            //Load the file and create a navigator object. 
            XPathDocument xPath     = new XPathDocument(fileStream);
            var           navigator = xPath.CreateNavigator();

            //Compile the query with a namespace prefix. 
            XPathExpression query = navigator.Compile("ns:MyDocument/ns:MyProperty");

            //Do some BS to get the default namespace to actually be called ns. 
            var nameSpace = new XmlNamespaceManager(navigator.NameTable);
            nameSpace.AddNamespace("ns", "http://www.dotnetcoretutorials.com/namespace");
            query.SetContext(nameSpace);

            Console.WriteLine("My Property Value : " + navigator.SelectSingleNode(query).Value);
        }
    }

    private void CreateSettingsFile()
    {
        var doc = new XDocument();

        doc.Save( _filePath + _propertiesFile );
    }
}