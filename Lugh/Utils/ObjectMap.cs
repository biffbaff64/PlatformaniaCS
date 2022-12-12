// ##################################################

// ##################################################

namespace Lugh.Utils
{
    public class ObjectMap< TK, TV >
    {
        public int Size { get; set; }

        protected int Shift { get; set; }
        protected int Mask  { get; set; }

        private static object _dummy = new object();

        private TK[]  _keyTable;
        private TV[]  _valueTable;
        private float _loadFactor;
        private int   _threshold;

        public ObjectMap( int initialCapacity = 51, float loadFactor = 0.8f )
        {
            if ( loadFactor <= 0f || loadFactor >= 1f )
            {
                throw new ArgumentException( "loadFactor must be > 0 and < 1: " + loadFactor );
            }

            this._loadFactor = loadFactor;

            int tableSize = NumberUtils.TableSize( initialCapacity, loadFactor );

            _threshold = ( int )( tableSize * loadFactor );
            Mask       = tableSize - 1;
            Shift      = Long.NumberOfLeadingZeros( Mask );

            _keyTable   = new object[ tableSize ] as TK[];
            _valueTable = new object[ tableSize ] as TV[];
        }

        public bool ContainsKey( string key )
        {
            return false;
        }
    }
}
