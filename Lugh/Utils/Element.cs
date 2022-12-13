// ##################################################

// ##################################################

namespace Lugh.Utils;

public class Element
{
    public string                      Name       { get; set; }
    public ObjectMap< string, string > Attributes { get; set; }

    private Array< Element > children;
    private string           text;
    private Element          parent;

    public Element( string name, Element parent )
    {
        this.Name   = name;
        this.parent = parent;
    }

    /** @throws GdxRuntimeException if the attribute was not found. */
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

    public int getChildCount()
    {
        if ( children == null ) return 0;
        return children.size;
    }

    /** @throws GdxRuntimeException if the element has no children. */
    public Element getChild( int index )
    {
        if ( children == null ) throw new GdxRuntimeException( "Element has no children: " + Name );
        return children.get( index );
    }

    public void addChild( Element element )
    {
        if ( children == null ) children = new Array( 8 );
        children.add( element );
    }

    public string getText()
    {
        return text;
    }

    public void setText( string text )
    {
        this.text = text;
    }

    public void removeChild( int index )
    {
        if ( children != null ) children.removeIndex( index );
    }

    public void removeChild( Element child )
    {
        if ( children != null ) children.removeValue( child, true );
    }

    public void remove()
    {
        parent.removeChild( this );
    }

    public Element getParent()
    {
        return parent;
    }

    public string toString()
    {
        return toString( "" );
    }

    public string toString( string indent )
    {
        StringBuilder buffer = new StringBuilder( 128 );
        buffer.append( indent );
        buffer.append( '<' );
        buffer.append( Name );

        if ( Attributes != null )
        {
            for ( Entry< string, string > entry :
            Attributes.entries())

            {
                buffer.append( ' ' );
                buffer.append( entry.key );
                buffer.append( "=\"" );
                buffer.append( entry.value );
                buffer.append( '\"' );
            }
        }

        if ( children == null && ( text == null || text.length() == 0 ) )
            buffer.append( "/>" );
        else
        {
            buffer.append( ">\n" );
            string childIndent = indent + '\t';

            if ( text != null && text.length() > 0 )
            {
                buffer.append( childIndent );
                buffer.append( text );
                buffer.append( '\n' );
            }

            if ( children != null )
            {
                for ( Element child :
                children) {
                    buffer.append( child.toString( childIndent ) );
                    buffer.append( '\n' );
                }
            }

            buffer.append( indent );
            buffer.append( "</" );
            buffer.append( Name );
            buffer.append( '>' );
        }

        return buffer.toString();
    }

    /** @param name the name of the child {@link Element}
		 * @return the first child having the given name or null, does not recurse */
    public @Null Element getChildByName( string name )
    {
        if ( children == null ) return null;

        for ( int i = 0; i < children.size; i++ )
        {
            Element element = children.get( i );
            if ( element.Name.equals( name ) ) return element;
        }

        return null;
    }

    public bool hasChild( string name )
    {
        if ( children == null ) return false;
        return getChildByName( name ) != null;
    }

    /** @param name the name of the child {@link Element}
		 * @return the first child having the given name or null, recurses */
    public @Null Element getChildByNameRecursive( string name )
    {
        if ( children == null ) return null;

        for ( int i = 0; i < children.size; i++ )
        {
            Element element = children.get( i );
            if ( element.Name.equals( name ) ) return element;
            Element found = element.getChildByNameRecursive( name );
            if ( found != null ) return found;
        }

        return null;
    }

    public bool hasChildRecursive( string name )
    {
        if ( children == null ) return false;
        return getChildByNameRecursive( name ) != null;
    }

    /** @param name the name of the children
		 * @return the children with the given name or an empty {@link Array} */
    public Array< Element > getChildrenByName( string name )
    {
        Array< Element > result = new Array< Element >();
        if ( children == null ) return result;

        for ( int i = 0; i < children.size; i++ )
        {
            Element child = children.get( i );
            if ( child.Name.equals( name ) ) result.add( child );
        }

        return result;
    }

    /** @param name the name of the children
		 * @return the children with the given name or an empty {@link Array} */
    public Array< Element > getChildrenByNameRecursively( string name )
    {
        Array< Element > result = new Array< Element >();
        getChildrenByNameRecursively( name, result );
        return result;
    }

    private void getChildrenByNameRecursively( string name, Array< Element > result )
    {
        if ( children == null ) return;

        for ( int i = 0; i < children.size; i++ )
        {
            Element child = children.get( i );
            if ( child.Name.equals( name ) ) result.add( child );
            child.getChildrenByNameRecursively( name, result );
        }
    }

    /** @throws GdxRuntimeException if the attribute was not found. */
    public float getFloatAttribute( string name )
    {
        return Float.parseFloat( getAttribute( name ) );
    }

    public float getFloatAttribute( string name, float defaultValue )
    {
        string value = getAttribute( name, null );
        if ( value == null ) return defaultValue;
        return Float.parseFloat( value );
    }

    /** @throws GdxRuntimeException if the attribute was not found. */
    public int getIntAttribute( string name )
    {
        return Integer.parseInt( getAttribute( name ) );
    }

    public int getIntAttribute( string name, int defaultValue )
    {
        string value = getAttribute( name, null );
        if ( value == null ) return defaultValue;
        return Integer.parseInt( value );
    }

    /** @throws GdxRuntimeException if the attribute was not found. */
    public bool getBooleanAttribute( string name )
    {
        return Boolean.parseBoolean( getAttribute( name ) );
    }

    public bool getBooleanAttribute( string name, bool defaultValue )
    {
        string value = getAttribute( name, null );
        if ( value == null ) return defaultValue;
        return Boolean.parseBoolean( value );
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public string get( string name )
    {
        string value = get( name, null );

        if ( value == null )
            throw new GdxRuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );

        return value;
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public string get( string name, string defaultValue )
    {
        if ( Attributes != null )
        {
            string value = Attributes.get( name );
            if ( value != null ) return value;
        }

        Element child = getChildByName( name );
        if ( child == null ) return defaultValue;
        string value = child.getText();
        if ( value == null ) return defaultValue;
        return value;
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public int getInt( string name )
    {
        string value = get( name, null );

        if ( value == null )
            throw new GdxRuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );

        return Integer.parseInt( value );
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public int getInt( string name, int defaultValue )
    {
        string value = get( name, null );
        if ( value == null ) return defaultValue;
        return Integer.parseInt( value );
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public float getFloat( string name )
    {
        string value = get( name, null );

        if ( value == null )
            throw new GdxRuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );

        return Float.parseFloat( value );
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public float getFloat( string name, float defaultValue )
    {
        string value = get( name, null );
        if ( value == null ) return defaultValue;
        return Float.parseFloat( value );
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public bool getBoolean( string name )
    {
        string value = get( name, null );

        if ( value == null )
            throw new GdxRuntimeException( "Element " + this.Name + " doesn't have attribute or child: " + name );

        return Boolean.parseBoolean( value );
    }

    /** Returns the attribute value with the specified name, or if no attribute is found, the text of a child with the name.
		 * @throws GdxRuntimeException if no attribute or child was not found. */
    public bool getBoolean( string name, bool defaultValue )
    {
        string value = get( name, null );
        if ( value == null ) return defaultValue;
        return Boolean.parseBoolean( value );
    }
}

}
