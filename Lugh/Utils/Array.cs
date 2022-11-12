// ##################################################

using System.Diagnostics.CodeAnalysis;

// ##################################################

namespace Lugh.Utils;

[SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" )]
public class Array< T >
{
    public T[] Items { get; set; }

    public int  Size    { get; set; }
    public bool Ordered { get; set; }

    public Array()
    {
    }

    public Array( Array array )
    {
    }

    public Array( bool ordered, int capacity, Type arrayType )
    {
    }

    public Array( bool ordered, int capacity )
    {
    }

    public Array( bool ordered, T[] array, int startIndex, int count )
    {
    }

    public Array( Type arrayType )
    {
    }

    public Array( int capacity )
    {
    }

    public Array( T[] array )
    {
    }
}