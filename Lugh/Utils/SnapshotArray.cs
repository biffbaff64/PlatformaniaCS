using System.Collections.Generic;

namespace Lugh.Utils;

public class SnapshotArray< T > : List< T >
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

    public SnapshotArray( bool ordered, int capacity, Class arrayType )
    {
        base( ordered, capacity, arrayType );
    }

    public SnapshotArray( bool ordered, int capacity )
    {
        base( ordered, capacity );
    }

    public SnapshotArray( bool ordered, T[] array, int startIndex, int count )
    {
        base( ordered, array, startIndex, count );
    }

    public SnapshotArray( Class arrayType )
    {
        base( arrayType );
    }

    public SnapshotArray( int capacity )
    {
        base( capacity );
    }

    public SnapshotArray( T[] array )
    {
        base( array );
    }

    /** Returns the backing array, which is guaranteed to not be modified before {@link #end()}. */
    public T[] Begin()
    {
        Modified();
        _snapshot = items;
        _snapshots++;
        return items;
    }

    /** Releases the guarantee that the array returned by {@link #begin()} won't be modified. */
    public void End()
    {
        _snapshots = Math.max( 0, _snapshots - 1 );
        if ( _snapshot == null ) return;

        if ( _snapshot != items && _snapshots == 0 )
        {
            // The backing array was copied, keep around the old array.
            _recycled = _snapshot;

            for ( int i = 0, n = _recycled.length; i < n; i++ )
                _recycled[ i ] = null;
        }

        _snapshot = null;
    }

    private void Modified()
    {
        if ( _snapshot == null || _snapshot != items ) return;

        // Snapshot is in use, copy backing array to recycled array or create new backing array.
        if ( _recycled != null && _recycled.length >= size )
        {
            System.arraycopy( items, 0, _recycled, 0, size );
            items    = _recycled;
            _recycled = null;
        }
        else
            resize( items.length );
    }

    public void Set( int index, T value )
    {
        Modified();
        base.set( index, value );
    }

    public void Insert( int index, T value )
    {
        Modified();
        base.insert( index, value );
    }

    public void InsertRange( int index, int count )
    {
        Modified();
        base.insertRange( index, count );
    }

    public void Swap( int first, int second )
    {
        Modified();
        base.swap( first, second );
    }

    public bool RemoveValue( T value, bool identity )
    {
        Modified();
        return base.removeValue( value, identity );
    }

    public T RemoveIndex( int index )
    {
        Modified();
        return base.removeIndex( index );
    }

    public void RemoveRange( int start, int end )
    {
        Modified();
        base.removeRange( start, end );
    }

    public bool RemoveAll( Array<? extends T> array, bool Identity) {
        Modified();
        return base.removeAll( array, identity );
    }

    public T Pop()
    {
        Modified();
        return base.pop();
    }

    public void Clear()
    {
        Modified();
        base.clear();
    }

    public void Sort()
    {
        Modified();
        base.sort();
    }

    public void Sort( Comparator<? base T> comparator) {
        Modified();
        base.sort( comparator );
    }

    public void Reverse()
    {
        Modified();
        base.reverse();
    }

    public void Shuffle()
    {
        Modified();
        base.shuffle();
    }

    public void Truncate( int newSize )
    {
        Modified();
        base.truncate( newSize );
    }

    public T[] SetSize( int newSize )
    {
        Modified();
        return base.setSize( newSize );
    }

    static public <T> SnapshotArray< T > With( T...array )
    {
        return new SnapshotArray( array );
    }
}