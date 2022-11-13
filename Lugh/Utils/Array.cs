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

    public T Get( int index )
    {
        if ( index >= Size )
        {
            throw new IndexOutOfRangeException( "index cannot be >= size: " + index + " >= " + Size );
        }

        return Items[ index ];
    }

    /// <summary>
    /// Removes the first instance of the specified value in the array.
    /// </summary>
    /// <param name="value">The value to remove ( May be null ).</param>
    /// <returns>true if value was found and removed, false otherwise.</returns>
    public bool RemoveValue( T value )
    {
        var items = this.Items;

        for ( int i = 0, n = Size; i < n; i++ )
        {
            if ( items[ i ].Equals( value ) )
            {
                RemoveIndex( i );

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Removes and returns the item at the specified index.
    /// </summary>
    public T RemoveIndex( int index )
    {
        if ( index >= Size )
        {
            throw new IndexOutOfRangeException( "index can't be >= size: " + index + " >= " + Size );
        }

        var items = this.Items;
        var value = items[ index ];

        Size--;

        if ( Ordered )
        {
            Array.Copy
            (
                items,
                index + 1,
                items,
                index,
                Size - index
            );
        }
        else
        {
            items[ index ] = items[ Size ];
        }

        items[ Size ] = default;

        return value;
    }

    public void insert( int index, T value )
    {
        if ( index > Size )
        {
            throw new IndexOutOfRangeException( "index can't be > size: " + index + " > " + Size );
        }

        T[] items = this.Items;

        if ( Size == items.Length )
        {
            items = Resize( Math.Max( 8, ( int )( Size * 1.75f ) ) );
        }

        if ( Ordered )
        {
            Array.Copy
            (
                items,
                index,
                items,
                index + 1,
                Size  - index
            );
        }
        else
        {
            items[ Size ] = items[ index ];
        }

        Size++;

        items[ index ] = value;
    }

    protected T[] Resize( int newSize )
    {
        var items = this.Items;
        var newItems = new T[ newSize ];

        Array.Copy( items, 0, newItems, 0, Math.Min( Size, newItems.Length ) );
        this.Items = newItems;

        return newItems;
    }
}