// ##################################################

using System.Collections;

using Lugh.Collections;

// ##################################################

namespace Lugh.Collections
{
    public class StringObjectMap : IEnumerable< Entry< string, object > >
    {
        public int      Size       { get; set; }
        public string[] KeyTable   { get; set; }
        public object[] ValueTable { get; set; }
        public int      Shift      { get; set; }
        public int      Mask       { get; set; }

        private float _loadFactor;
        private int   _threshold;

        [NonSerialized] private Entries _entries1;
        [NonSerialized] private Entries _entries2;
        [NonSerialized] private Values  _values1;
        [NonSerialized] private Values  _values2;
        [NonSerialized] private Keys    _keys1;
        [NonSerialized] private Keys    _keys2;

        public StringObjectMap( int initialCapacity = 51, float loadFactor = 0.8f )
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

            KeyTable   = new string[ tableSize ];
            ValueTable = new object[ tableSize ];
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
        public int Place( string item )
        {
            return ( int )( ( ( ( ulong )item.GetHashCode() ) * 0x9E3779B97F4A7C15L ) >> Shift );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private int LocateKey( string key )
        {
            if ( key == null )
            {
                throw new ArgumentException( "key cannot be null." );
            }

            for ( var i = Place( key );; i = i + 1 & Mask )
            {
                var other = KeyTable[ i ];

                if ( other == null )
                {
                    return -( i + 1 ); // Empty space is available.
                }

                if ( other.Equals( key ) )
                {
                    return i; // Same key was found.
                }
            }
        }

        public bool ContainsKey( string key )
        {
            return false;
        }

        public object Get( string key )
        {
            var i = LocateKey( key );

            return i < 0 ? default : ValueTable[ i ];
        }

        public object Get( string key, object defaultValue )
        {
            var i = LocateKey( key );

            return i < 0 ? defaultValue : ValueTable[ i ];
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public object Put( string key, object value )
        {
            var i = LocateKey( key );

            if ( i >= 0 )
            {
                // Existing key was found.
                var oldValue = ValueTable[ i ];
                ValueTable[ i ] = value;

                return oldValue;
            }

            i = -( i + 1 ); // Empty space was found.

            KeyTable[ i ]   = key;
            ValueTable[ i ] = value;

            if ( ++Size >= _threshold ) Resize( KeyTable.Length << 1 );

            return default;
        }

        // TODO: Finish and double check this against LibGDX version
        public void PutAll( StringObjectMap map )
        {
            EnsureCapacity( map.Size );

            for ( int i = 0, n = map.KeyTable.Length; i < n; i++ )
            {
                var key = map.KeyTable[ i ];

                if ( key != null )
                {
                    Put( key, map.ValueTable[ i ] );
                }
            }
        }

        private void PutResize( string key, object value )
        {
            for ( int i = Place( key );; i = ( i + 1 ) & Mask )
            {
                if ( KeyTable[ i ] == null )
                {
                    KeyTable[ i ]   = key;
                    ValueTable[ i ] = value;

                    return;
                }
            }
        }

        public object Remove( string key )
        {
            var i = LocateKey( key );

            if ( i < 0 ) return default;

            var oldValue = ValueTable[ i ];

            var mask = this.Mask;
            var next = i + 1 & mask;

            while ( ( key = KeyTable[ next ] ) != null )
            {
                var placement = Place( key );

                if ( ( next - placement & mask ) > ( i - placement & mask ) )
                {
                    KeyTable[ i ]   = key;
                    ValueTable[ i ] = ValueTable[ next ];

                    i = next;
                }

                next = next + 1 & mask;
            }

            KeyTable[ i ]   = default;
            ValueTable[ i ] = default;

            Size--;

            return oldValue;
        }

        public void EnsureCapacity( int additionalCapacity )
        {
            var tableSize = NumberUtils.TableSize( Size + additionalCapacity, _loadFactor );

            if ( KeyTable.Length < tableSize )
            {
                Resize( tableSize );
            }
        }

        public void Resize( int newSize )
        {
            var oldCapacity = KeyTable.Length;

            _threshold = ( int )( newSize * _loadFactor );
            Mask       = newSize - 1;
            Shift      = Long.NumberOfLeadingZeros( Mask );

            var oldKeyTable   = KeyTable;
            var oldValueTable = ValueTable;

            KeyTable   = new string[ newSize ];
            ValueTable = new object[ newSize ];

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

            if ( KeyTable.Length <= tableSize )
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

                Array.Fill( KeyTable,   default );
                Array.Fill( ValueTable, default );
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

        public Values GetValues()
        {
            if ( Collections.AllocateIterators )
            {
                return new Values( this );
            }

            if ( _values1 == null )
            {
                _values1 = new Values( this );
                _values2 = new Values( this );
            }

            if ( !_values1.Valid )
            {
                _values1.Reset();
                _values1.Valid = true;
                _values2.Valid = false;

                return _values1;
            }

            _values2.Reset();
            _values2.Valid = true;
            _values1.Valid = false;

            return _values2;
        }

        public Keys GetKeys()
        {
            if ( Collections.AllocateIterators )
            {
                return new Keys( this );
            }

            if ( _keys1 == null )
            {
                _keys1 = new Keys( this );
                _keys2 = new Keys( this );
            }

            if ( !_keys1.Valid )
            {
                _keys1.Reset();
                _keys1.Valid = true;
                _keys2.Valid = false;

                return _keys1;
            }

            _keys2.Reset();
            _keys2.Valid = true;
            _keys1.Valid = false;

            return _keys2;
        }

        public Entries GetEntries()
        {
            if ( Collections.AllocateIterators )
            {
                return new Entries( this );
            }

            if ( _entries1 == null )
            {
                _entries1 = new Entries( this );
                _entries2 = new Entries( this );
            }

            if ( !_entries1.Valid )
            {
                _entries1.Reset();
                _entries1.Valid = true;
                _entries2.Valid = false;

                return _entries1;
            }

            _entries2.Reset();
            _entries2.Valid = true;
            _entries1.Valid = false;

            return _entries2;
        }

        public IEnumerator< Entry< string, object > > GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // ########################################################################################

        public class Entries : MapIterator< string, object, Entry< string, object > >
        {
            private Entry< string, object > _entry = new Entry< string, object >();

            public Entries( StringObjectMap map ) : base( map )
            {
            }

            /** Note the same entry instance is returned each time this method is called. */
            public Entry< string, object > Next()
            {
                if ( !HasNext ) throw new NoSuchElementException();

                if ( !Valid ) throw new RuntimeException( "#iterator() cannot be used nested." );

                string[] keyTable = map.KeyTable;

                _entry.key   = keyTable[ NextIndex ];
                _entry.value = map.ValueTable[ NextIndex ];

                CurrentIndex = NextIndex;

                FindNextIndex();

                return _entry;
            }

            public Entries Iterator()
            {
                return this;
            }
        }

        // ########################################################################################

        public class Keys : MapIterator< string, object, string >
        {
            public Keys( StringObjectMap map ) : base( map )
            {
            }

            public string Next()
            {
                if ( !HasNext )
                {
                    throw new NoSuchElementException();
                }

                if ( !Valid )
                {
                    throw new RuntimeException( "#iterator() cannot be used nested." );
                }

                var key = map.KeyTable[ NextIndex ];

                CurrentIndex = NextIndex;

                FindNextIndex();

                return key;
            }

            public Keys Iterator()
            {
                return this;
            }

            /** Returns a new array containing the remaining keys. */
            public List< string > ToList()
            {
                return ToList( new List< string >( map.Size ) );
            }

            /** Adds the remaining keys to the array. */
            public List< string > ToList( List< string > list )
            {
                while ( HasNext )
                {
                    list.Add( Next() );
                }

                return list;
            }
        }

        // ########################################################################################

        public class Values : MapIterator< object, object, object >
        {
            public Values( StringObjectMap map ) : base( map )
            {
            }

            public object Next()
            {
                if ( !HasNext ) throw new NoSuchElementException();

                if ( !Valid ) throw new RuntimeException( "#iterator() cannot be used nested." );

                var value = map.ValueTable[ NextIndex ];

                CurrentIndex = NextIndex;

                FindNextIndex();

                return value;
            }

            public Values Iterator()
            {
                return this;
            }

            public List< object > ToList()
            {
                return ToList( new List< object >( map.Size ) );
            }

            public List< object > ToList( List< object > list )
            {
                while ( HasNext )
                {
                    list.Add( Next() );
                }

                return list;
            }
        }
    }
}
