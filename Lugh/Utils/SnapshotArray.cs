// ##################################################

// ##################################################

namespace Lugh.Utils;

public class SnapshotArray< T > : Array< T >
{
    private T[] _snapshot;
    private T[] _recycled;
    private int _snapshots;

    public SnapshotArray() : base()
    {
    }

    public SnapshotArray( Array array ) : base( array )
    {
    }

    public SnapshotArray( bool ordered, int capacity, Type arrayType )
        : base( ordered, capacity, arrayType )
    {
    }

    public SnapshotArray( bool ordered, int capacity )
        : base( ordered, capacity )
    {
    }

    public SnapshotArray( bool ordered, T[] array, int startIndex, int count )
        : base( ordered, array, startIndex, count )
    {
    }

    public SnapshotArray( Type arrayType ) : base( arrayType )
    {
    }

    public SnapshotArray( int capacity ) : base( capacity )
    {
    }

    public SnapshotArray( T[] array ) : base( array )
    {
    }

    /// <summary>
    /// Returns the backing array, which is guaranteed to not be
    /// modified before <see cref="End"/>
    /// </summary>
    public T[] Begin()
    {
        Modified();

        _snapshot = Items;
        _snapshots++;

        return Items;
    }

    /// <summary>
    /// Releases the guarantee that the array returned
    /// by <see cref="Begin"/> won't be modified.
    /// </summary>
    public void End()
    {
        _snapshots = Math.Max( 0, _snapshots - 1 );

        if ( _snapshot == null ) return;

        if ( ( _snapshot != Items ) && ( _snapshots == 0 ) )
        {
            // The backing array was copied, keep around the old array.
            _recycled = _snapshot;

            for ( int i = 0, n = _recycled.Length; i < n; i++ )
            {
                _recycled[ i ] = default( T );
            }
        }

        _snapshot = null;
    }

    private void Modified()
    {
        if ( _snapshot == null || _snapshot != Items ) return;

        // Snapshot is in use, copy backing array to recycled array or create new backing array.
        if ( _recycled != null && _recycled.Length >= Size )
        {
            System.Arraycopy( Items, 0, _recycled, 0, Size );

            Items     = _recycled;
            _recycled = null;
        }
        else
        {
            Resize( Items.Length );
        }
    }

    public void Set( int index, T value )
    {
        Modified();
        base.Set( index, value );
    }

    public void Insert( int index, T value )
    {
        Modified();
        base.Insert( index, value );
    }

    public void InsertRange( int index, int count )
    {
        Modified();
        base.InsertRange( index, count );
    }

    public void Swap( int first, int second )
    {
        Modified();
        base.Swap( first, second );
    }

    public bool RemoveValue( T value, bool identity )
    {
        Modified();
        return base.RemoveValue( value, identity );
    }

    public T RemoveIndex( int index )
    {
        Modified();
        return base.RemoveIndex( index );
    }

    public void RemoveRange( int start, int end )
    {
        Modified();
        base.RemoveRange( start, end );
    }

    public bool RemoveAll( Array<? extends T> array, bool Identity)
    {
        Modified();

        return base.RemoveAll( array, identity );
    }

    public T Pop()
    {
        Modified();
        return base.Pop();
    }

    public void Clear()
    {
        Modified();
        base.Clear();
    }

    public void Sort()
    {
        Modified();
        base.Sort();
    }

    public void Sort( Comparator<? base T> comparator)
    {
        Modified();

        base.Sort( comparator );
    }

    public void Reverse()
    {
        Modified();

        base.Reverse();
    }

    public void Shuffle()
    {
        Modified();

        base.Shuffle();
    }

    public void Truncate( int newSize )
    {
        Modified();

        base.Truncate( newSize );
    }

    public T[] SetSize( int newSize )
    {
        Modified();

        return base.SetSize( newSize );
    }

    public static SnapshotArray< T > With( T array )
    {
        return new SnapshotArray< T >( array );
    }
}