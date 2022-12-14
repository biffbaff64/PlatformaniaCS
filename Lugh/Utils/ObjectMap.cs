// ##################################################

// ##################################################

using System.Collections;

namespace Lugh.Utils
{
    public class ObjectMap< TK, TV > : IEnumerable< ObjectMap< TK, TV >.Entry< TK, TV > >
    {
        public int Size { get; set; }

        protected int Shift { get; set; }
        protected int Mask  { get; set; }

        private static object _dummy = new object();

        private TK[]  _keyTable;
        private TV[]  _valueTable;
        private float _loadFactor;
        private int   _threshold;

        [NonSerialized] Entries entries1, entries2;
        [NonSerialized] Values  values1,  values2;
        [NonSerialized] Keys    keys1,    keys2;

        public ObjectMap( int initialCapacity = 51, float loadFactor = 0.8f )
        {
            if ( loadFactor <= 0f || loadFactor >= 1f )
            {
                throw new ArgumentException( "loadFactor must be > 0 and < 1: " + loadFactor );
            }

            this._loadFactor = loadFactor;

            var tableSize = NumberUtils.TableSize( initialCapacity, loadFactor );

            _threshold = ( int )( tableSize * loadFactor );
            Mask       = tableSize - 1;
            Shift      = Long.NumberOfLeadingZeros( Mask );

            _keyTable   = new object[ tableSize ] as TK[];
            _valueTable = new object[ tableSize ] as TV[];
        }

        /// <summary>
        /// Returns an index >= 0 and <= mask for the specified item. The default
        /// implementation uses Fibonacci hashing on the item's object.hashCode():
        /// the hashcode is multiplied by a long constant (2 to the 64th, divided
        /// by the golden ratio) then the uppermos bits are shifted into the lowest
        /// positions to obtain an index in the desired range. Multiplication by a
        /// long may be slower than int (eg on GWT) but greatly improves rehashing,
        /// allowing even very poor hashcodes, such as those that only differ in their
        /// upper bits, to be used without high collision rates. Fibonacci hashing has
        /// increased collision rates when all or most hashcodes are multiples of
        /// larger Fibonacci numbers (see Malte Skarupke's blog post). This method can
        /// be overriden to customizing hashing. This may be useful eg in the unlikely
        /// event that most hashcodes are Fibonacci numbers, if keys provide poor or
        /// incorrect hashcodes, or to simplify hashing if keys provide high quality
        /// hashcodes and don't need Fibonacci hashing.
        /// </summary>
        protected int Place( TK item )
        {
            return ( int )( ( ( ( ulong )item.GetHashCode() ) * 0x9E3779B97F4A7C15L ) >> Shift );
        }

        private int LocateKey( TK key )
        {
            if ( key == null )
            {
                throw new ArgumentException( "key cannot be null." );
            }

            for ( var i = Place( key );; i = i + 1 & Mask )
            {
                var other = _keyTable[ i ];

                if ( other == null ) return -( i + 1 ); // Empty space is available.

                if ( other.Equals( key ) ) return i; // Same key was found.
            }
        }

        public bool ContainsKey( string key )
        {
            return false;
        }

        public TV Get< T >( T key ) where T : TK
        {
            var i = LocateKey( key );

            return i < 0 ? default : _valueTable[ i ];
        }

        public TV Put( TK key, TV value )
        {
            var i = LocateKey( key );

            if ( i >= 0 )
            {
                // Existing key was found.
                var oldValue = _valueTable[ i ];
                _valueTable[ i ] = value;

                return oldValue;
            }

            i = -( i + 1 ); // Empty space was found.

            _keyTable[ i ]   = key;
            _valueTable[ i ] = value;

            if ( ++Size >= _threshold ) Resize( _keyTable.Length << 1 );

            return default;
        }

        // TODO: Finish and double check this against LibGDX version
        public void PutAll( ObjectMap< TK, TV > map )
        {
            EnsureCapacity( map.Size );

            for ( int i = 0, n = _keyTable.Length; i < n; i++ )
            {
                var key = _keyTable[ i ];

                if ( key != null )
                {
                    Put( key, _valueTable[ i ] );
                }
            }
        }

        private void PutResize( TK key, TV value )
        {
            for ( int i = Place( key );; i = ( i + 1 ) & Mask )
            {
                if ( _keyTable[ i ] == null )
                {
                    _keyTable[ i ]   = key;
                    _valueTable[ i ] = value;
                    return;
                }
            }
        }

        public TV Remove( TK key )
        {
            var i = LocateKey( key );

            if ( i < 0 ) return default;

            var oldValue = _valueTable[ i ];

            var mask = this.Mask;
            var next = i + 1 & mask;

            while ( ( key = _keyTable[ next ] ) != null )
            {
                var placement = Place( key );

                if ( ( next - placement & mask ) > ( i - placement & mask ) )
                {
                    _keyTable[ i ]   = key;
                    _valueTable[ i ] = _valueTable[ next ];

                    i = next;
                }

                next = next + 1 & mask;
            }

            _keyTable[ i ]   = default;
            _valueTable[ i ] = default;

            Size--;

            return oldValue;
        }

        public void EnsureCapacity( int additionalCapacity )
        {
            var tableSize = NumberUtils.TableSize( Size + additionalCapacity, _loadFactor );

            if ( _keyTable.Length < tableSize )
            {
                Resize( tableSize );
            }
        }

        public void Resize( int newSize )
        {
            var oldCapacity = _keyTable.Length;

            _threshold = ( int )( newSize * _loadFactor );
            Mask       = newSize - 1;
            Shift      = Long.NumberOfLeadingZeros( Mask );

            var oldKeyTable   = _keyTable;
            var oldValueTable = _valueTable;

            _keyTable   = new object[ newSize ] as TK[];
            _valueTable = new object[ newSize ] as TV[];

            if ( Size > 0 )
            {
                for ( var i = 0; i < oldCapacity; i++ )
                {
                    var key = oldKeyTable[ i ];

                    if ( key != null )
                    {
                        PutResize( key, oldValueTable[ i ] );
                    }
                }
            }
        }

        public void Clear( int newMaximumCapacity )
        {
            int tableSize = NumberUtils.TableSize( newMaximumCapacity, _loadFactor );

            if ( _keyTable.Length <= tableSize )
            {
                Clear();
                return;
            }

            Size = 0;
            Resize( tableSize );
        }

        public void Clear()
        {
            if ( Size > 0 )
            {
                Size = 0;

                Array.Fill( _keyTable,   default );
                Array.Fill( _valueTable, default );
            }
        }

        public bool NotEmpty()
        {
            return Size > 0;
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        public Values< string > Values()
        {
            if ( Collections.allocateIterators )
            {
                return new Values( this );
            }

            if ( values1 == null )
            {
                values1 = new Values( this );
                values2 = new Values( this );
            }

            if ( !values1.valid )
            {
                values1.reset();
                values1.valid = true;
                values2.valid = false;

                return values1;
            }

            values2.reset();
            values2.valid = true;
            values1.valid = false;

            return values2;
        }

        public Keys< TK > Keys()
        {
            if ( Collections.allocateIterators )
            {
                return new Keys( this );
            }

            if ( keys1 == null )
            {
                keys1 = new Keys( this );
                keys2 = new Keys( this );
            }

            if ( !keys1.valid )
            {
                keys1.reset();
                keys1.valid = true;
                keys2.valid = false;

                return keys1;
            }

            keys2.reset();
            keys2.valid = true;
            keys1.valid = false;

            return keys2;
        }

        public Entries< TK, TV > Entries()
        {
            if ( Collections.allocateIterators )
            {
                return new Entries( this );
            }

            if ( entries1 == null )
            {
                entries1 = new Entries( this );
                entries2 = new Entries( this );
            }

            if ( !entries1.valid )
            {
                entries1.reset();
                entries1.valid = true;
                entries2.valid = false;

                return entries1;
            }

            entries2.reset();
            entries2.valid = true;
            entries1.valid = false;

            return entries2;
        }

        public IEnumerator< Entry< TK, TV > > GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // ################################################

        public class Entry< TKey, TValue >
        {
            public TKey   key;
            public TValue value;

            public new string ToString()
            {
                return key + "=" + value;
            }
        }

        // ################################################

        private abstract class MapIterator< TK, TV, TI > : IEnumerable, IEnumerator< TI >
        {
            public bool HasNext { get; set; }

            private ObjectMap< TK, TV > _map;

            private int  _nextIndex;
            private int  _currentIndex;
            private bool _valid = true;

            public MapIterator( ObjectMap< TK, TV > map )
            {
                this._map = map;
                reset();
            }

            public void reset()
            {
                _currentIndex = -1;
                _nextIndex    = -1;
                FindNextIndex();
            }

            void FindNextIndex()
            {
                var keyTable = _map._keyTable;

                for ( int n = keyTable.Length; ++_nextIndex < n; )
                {
                    if ( keyTable[ _nextIndex ] != null )
                    {
                        HasNext = true;
                        return;
                    }
                }

                HasNext = false;
            }

            public void Remove()
            {
                var i = _currentIndex;

                if ( i < 0 )
                {
                    throw new MethodAccessException( "next must be called before remove." );
                }

                var mask = _map.Mask;
                var next = i + 1 & mask;
                TK  key;

                while ( ( key = _map._keyTable[ next ] ) != null )
                {
                    var placement = _map.Place( key );

                    if ( ( next - placement & mask ) > ( i - placement & mask ) )
                    {
                        _map._keyTable[ i ]   = key;
                        _map._valueTable[ i ] = _map._valueTable[ next ];

                        i = next;
                    }

                    next = next + 1 & mask;
                }

                _map._keyTable[ i ]   = default;
                _map._valueTable[ i ] = default;
                _map.Size--;

                if ( i != _currentIndex )
                {
                    --_nextIndex;
                }
                
                _currentIndex = -1;
            }
        }

        // ################################################

        public static class Values< TV > : MapIterator< object, TV, TV >
        {
            public Values( ObjectMap<?, TV> map )
            {
                super( ( ObjectMap< object, TV > )map );
            }

            public boolean hasNext()
            {
                if ( !valid ) throw new GdxRuntimeException( "#iterator() cannot be used nested." );
                return hasNext;
            }

            public @Null V next()
            {
                if ( !hasNext ) throw new NoSuchElementException();
                if ( !valid ) throw new GdxRuntimeException( "#iterator() cannot be used nested." );
                V value = map.valueTable[ nextIndex ];
                currentIndex = nextIndex;
                findNextIndex();
                return value;
            }

            public Values< V > iterator()
            {
                return this;
            }

            /** Returns a new array containing the remaining values. */
            public Array< V > toArray()
            {
                return toArray( new Array( true, map.size ) );
            }

            /** Adds the remaining values to the specified array. */
            public Array< V > toArray( Array< V > array )
            {
                while ( hasNext )
                    array.add( next() );

                return array;
            }
        }

        static public class Keys< K >

        extends MapIterator< K, object, K > {

        public Keys( ObjectMap< K,?> map )
        {
            super( ( ObjectMap< K, object > )map );
        }

        public boolean hasNext()
        {
            if ( !valid ) throw new GdxRuntimeException( "#iterator() cannot be used nested." );
            return hasNext;
        }

        public K next()
        {
            if ( !hasNext ) throw new NoSuchElementException();
            if ( !valid ) throw new GdxRuntimeException( "#iterator() cannot be used nested." );
            K key = map.keyTable[ nextIndex ];
            currentIndex = nextIndex;
            findNextIndex();
            return key;
        }

        public Keys< K > iterator()
        {
            return this;
        }

        /** Returns a new array containing the remaining keys. */
        public Array< K > toArray()
        {
            return toArray( new Array< K >( true, map.size ) );
        }

        /** Adds the remaining keys to the array. */
        public Array< K > toArray( Array< K > array )
        {
            while ( hasNext )
                array.add( next() );

            return array;
        }
    }
}

}
