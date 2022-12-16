// ##################################################

// ##################################################

using System.Collections;

namespace Lugh.Collections
{
    public abstract class MapIterator< TK, TV, TI > : IEnumerable, IEnumerator< TI >
    {
        protected bool                HasNext { get; set; }
        protected StringObjectMap< TK, TV > map     { get; set; }

        public int  NextIndex    { get; set; }
        public int  CurrentIndex { get; set; }
        public bool Valid        { get; set; } = true;

        public MapIterator( StringObjectMap< TK, TV > map )
        {
            this.map = map;

            Reset();
        }

        public void Reset()
        {
            CurrentIndex = -1;
            NextIndex    = -1;

            FindNextIndex();
        }

        protected void FindNextIndex()
        {
            var keyTable = map.KeyTable;

            for ( int n = keyTable.Length; ++NextIndex < n; )
            {
                if ( keyTable[ NextIndex ] != null )
                {
                    HasNext = true;
                    return;
                }
            }

            HasNext = false;
        }

        public void Remove()
        {
            var i = CurrentIndex;

            if ( i < 0 )
            {
                throw new MethodAccessException( "next must be called before remove." );
            }

            var mask = map.Mask;
            var next = i + 1 & mask;
            TK  key;

            while ( ( key = map.KeyTable[ next ] ) != null )
            {
                var placement = map.Place( key );

                if ( ( next - placement & mask ) > ( i - placement & mask ) )
                {
                    map.KeyTable[ i ]   = key;
                    map.ValueTable[ i ] = map.ValueTable[ next ];

                    i = next;
                }

                next = next + 1 & mask;
            }

            map.KeyTable[ i ]   = default;
            map.ValueTable[ i ] = default;
            map.Size--;

            if ( i != CurrentIndex )
            {
                --NextIndex;
            }

            CurrentIndex = -1;
        }

        public IEnumerator GetEnumerator()
        {
            yield break;
        }

        public bool MoveNext()
        {
            return false;
        }

        public TI Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
