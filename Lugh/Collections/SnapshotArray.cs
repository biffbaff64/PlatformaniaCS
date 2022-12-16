// ##################################################

// ##################################################

namespace Lugh.Collections
{
    public class SnapshotArray< T > : Array< T >
    {
        private T[] _snapshot;
        private T[] _recycled;
        private int _snapshots;

        public SnapshotArray() : base()
        {
        }

        public SnapshotArray( int capacity ) : this( true, capacity )
        {
        }

        public SnapshotArray( bool ordered, int capacity )
        {
        }

        public SnapshotArray( T[] array )
        {
        }

        public SnapshotArray( bool ordered, T[] array, int startIndex, int count )
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
                Array.Copy( Items, 0, _recycled, 0, Size );

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

        public new void Insert( int index, T value )
        {
            Modified();
            base.Insert( index, value );
        }

        public new void InsertRange( int index, int count )
        {
            Modified();
            base.InsertRange( index, count );
        }

        public new void Swap( int first, int second )
        {
            Modified();
            base.Swap( first, second );
        }

        public new bool RemoveValue( T value )
        {
            Modified();
            return base.RemoveValue( value );
        }

        public new T RemoveIndex( int index )
        {
            Modified();
            return base.RemoveIndex( index );
        }

        public new void RemoveRange( int start, int end )
        {
            Modified();
            base.RemoveRange( start, end );
        }

        public bool RemoveAll( Array< T > array )
        {
            Modified();
            return base.RemoveAll( array );
        }

        public new T Pop()
        {
            Modified();
            return base.Pop();
        }

        public new void Clear()
        {
            Modified();
            base.Clear();
        }

        public new void Sort()
        {
            Modified();
            base.Sort();
        }

#if _SCENE2DCS_RELEASE
    public void Sort( Comparator<? base T> comparator)
    {
        Modified();

        base.Sort( comparator );
    }
#endif

        public new void Reverse()
        {
            Modified();

            base.Reverse();
        }

        public new void Shuffle()
        {
            Modified();

            base.Shuffle();
        }

        public new void Truncate( int newSize )
        {
            Modified();

            base.Truncate( newSize );
        }

        public T[] SetSize( int newSize )
        {
            Modified();

            Size = newSize;
            return base.SetSize( newSize );
        }

#if _SCENE2DCS_RELEASE
    public static SnapshotArray< T > With( T array )
    {
        return new SnapshotArray< T >( array );
    }
#endif
    }
}
