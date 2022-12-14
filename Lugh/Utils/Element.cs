// ##################################################

// ##################################################

using System.Text;

namespace Lugh.Utils;

public class Element
{
    public string                      Name       { get; set; }
    public ObjectMap< string, string > Attributes { get; set; }
    public string                      Text       { get; set; }

    private List< Element > _children;
    private Element         _parent;

    public Element( string name, Element parent )
    {
        this.Name    = name;
        this._parent = parent;
    }

    public string GetAttribute( string name )
    {
        if ( Attributes == null )
        {
            throw new RuntimeException( "Element " + this.Name + " doesn't have attribute: " + name );
        }

        var value = Attributes.Get( name );

        if ( value == null )
        {
            throw new RuntimeException( "Element " + this.Name + " doesn't have attribute: " + name );
        }

        return value;
    }

    public string GetAttribute( string name, string defaultValue )
    {
        if ( Attributes == null )
        {
            return defaultValue;
        }

        var value = Attributes.Get( name );

        if ( value == null )
        {
            return defaultValue;
        }

        return value;
    }

    public bool HasAttribute( string name )
    {
        return Attributes != null && Attributes.ContainsKey( name );
    }

    public void SetAttribute( string name, string value )
    {
        if ( Attributes == null )
        {
            Attributes = new ObjectMap< string, string >( 8 );
        }

        Attributes.Put( name, value );
    }

    public int GetChildCount()
    {
        return ( _children == null ) ? 0 : _children.Count;
    }

    public Element GetChild( int index )
    {
        if ( _children == null )
        {
            throw new RuntimeException( "Element has no children: " + Name );
        }

        return _children[ index ];
    }

    public void AddChild( Element element )
    {
        if ( _children == null )
        {
            _children = new List< Element >( 8 );
        }

        _children.Add( element );
    }

    public void RemoveChild( int index )
    {
        if ( _children != null )
        {
            _children.RemoveAt( index );
        }
    }

    public void RemoveChild( Element child )
    {
        if ( _children != null )
        {
            _children.Remove( child );
        }
    }

    public void Remove()
    {
        _parent.RemoveChild( this );
    }

    public Element GetParent()
    {
        return _parent;
    }

    public new string ToString( string indent )
    {
        var buffer = new StringBuilder( 128 );
        
        buffer.Append( indent );
        buffer.Append( '<' );
        buffer.Append( Name );

        if ( Attributes != null )
        {
            foreach ( ObjectMap< string, string >.Entry< string, string > entry in Attributes.Entries() )
            {
                buffer.Append( ' ' );
                buffer.Append( entry.key );
                buffer.Append( "=\"" );
                buffer.Append( entry.value );
                buffer.Append( '\"' );
            }
        }

        if ( _children == null && string.IsNullOrEmpty( Text ) )
        {
            buffer.Append( "/>" );
        }
        else
        {
            buffer.Append( ">\n" );
            
            var childIndent = indent + '\t';

            if ( Text != null && Text.Length > 0 )
            {
                buffer.Append( childIndent );
                buffer.Append( Text );
                buffer.Append( '\n' );
            }

            if ( _children != null )
            {
                foreach ( var child in _children )
                {
                    buffer.Append( child.ToString( childIndent ) );
                    buffer.Append( '\n' );
                }
            }

            buffer.Append( indent );
            buffer.Append( "</" );
            buffer.Append( Name );
            buffer.Append( '>' );
        }

        return buffer.ToString();
    }

    public Element GetChildByName( string name )
    {
        if ( _children == null )
        {
            return null;
        }

        foreach ( var element in _children )
        {
            if ( element.Name.Equals( name ) )
            {
                return element;
            }
        }

        return null;
    }

    public bool HasChild( string name )
    {
        if ( _children == null )
        {
            return false;
        }

        return GetChildByName( name ) != null;
    }

    public Element GetChildByNameRecursive( string name )
    {
        if ( _children == null )
        {
            return null;
        }

        for ( var i = 0; i < _children.Count; i++ )
        {
            var element = _children[ i ];

            if ( element.Name.Equals( name ) )
            {
                return element;
            }

            var found = element.GetChildByNameRecursive( name );

            if ( found != null )
            {
                return found;
            }
        }

        return null;
    }

    public bool HasChildRecursive( string name )
    {
        if ( _children == null )
        {
            return false;
        }

        return GetChildByNameRecursive( name ) != null;
    }

    public List< Element > GetChildrenByName( string name )
    {
        var result = new List< Element >();

        if ( _children == null )
        {
            return result;
        }

        foreach ( var child in _children )
        {
            if ( child.Name.Equals( name ) )
            {
                result.Add( child );
            }
        }

        return result;
    }

    public List< Element > GetChildrenByNameRecursively( string name )
    {
        var result = new List< Element >();

        GetChildrenByNameRecursively( name, result );

        return result;
    }

    private void GetChildrenByNameRecursively( string name, List< Element > result )
    {
        if ( _children == null )
        {
            return;
        }

        foreach ( var child in _children )
        {
            if ( child.Name.Equals( name ) )
            {
                result.Add( child );
            }

            child.GetChildrenByNameRecursively( name, result );
        }
    }

    public float GetFloatAttribute( string name )
    {
        return float.Parse( GetAttribute( name ) );
    }

    public float GetFloatAttribute( string name, float defaultValue )
    {
        var value = GetAttribute( name, null );

        if ( value == null )
        {
            return defaultValue;
        }

        return float.Parse( value );
    }

    public int GetIntAttribute( string name )
    {
        return int.Parse( GetAttribute( name ) );
    }

    public int GetIntAttribute( string name, int defaultValue )
    {
        var value = GetAttribute( name, null );

        if ( value == null )
        {
            return defaultValue;
        }

        return int.Parse( value );
    }

    public bool GetBooleanAttribute( string name )
    {
        return bool.Parse( GetAttribute( name ) );
    }

    public bool GetBooleanAttribute( string name, bool defaultValue )
    {
        var value = GetAttribute( name, null );

        if ( value == null )
        {
            return defaultValue;
        }

        return bool.Parse( value );
    }

    public string Get( string name )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            throw new RuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );
        }

        return value;
    }

    public string Get( string name, string defaultValue )
    {
        string value;

        if ( Attributes != null )
        {
            value = Attributes.Get( name );

            if ( value != null )
            {
                return value;
            }
        }

        var child = GetChildByName( name );

        if ( child?.Text == null )
        {
            return defaultValue;
        }

        value = child.Text;

        return value;
    }

    public int GetInt( string name )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            throw new RuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );
        }

        return int.Parse( value );
    }

    public int GetInt( string name, int defaultValue )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            return defaultValue;
        }

        return int.Parse( value );
    }

    /// <summary>
    /// Returns the attribute value with the specified name, or if no
    /// attribute is found, the text of a child with the name.
    /// </summary>
    /// <exception cref="RuntimeException">If attribute or child was not found.</exception>
    public float GetFloat( string name )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            throw new RuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );
        }

        return float.Parse( value );
    }

    /// <summary>
    /// Returns the attribute value with the specified name, or if no
    /// attribute is found, the text of a child with the name.
    /// </summary>
    /// <exception cref="RuntimeException">If attribute or child was not found.</exception>
    public float GetFloat( string name, float defaultValue )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            return defaultValue;
        }

        return float.Parse( value );
    }

    /// <summary>
    /// Returns the attribute value with the specified name, or if no
    /// attribute is found, the text of a child with the name.
    /// </summary>
    /// <exception cref="RuntimeException">If attribute or child was not found.</exception>
    public bool GetBoolean( string name )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            throw new RuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );
        }

        return bool.Parse( value );
    }

    /// <summary>
    /// Returns the attribute value with the specified name, or if no
    /// attribute is found, the text of a child with the name.
    /// </summary>
    /// <exception cref="RuntimeException">If attribute or child was not found.</exception>
    public bool GetBoolean( string name, bool defaultValue )
    {
        var value = Get( name, null );

        if ( value == null )
        {
            return defaultValue;
        }

        return bool.Parse( value );
    }
}
