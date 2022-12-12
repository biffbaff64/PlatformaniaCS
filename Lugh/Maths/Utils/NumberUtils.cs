namespace Lugh.Maths
{
    public static class NumberUtils
    {
        public static int TableSize( int capacity, float loadFactor )
        {
            if ( capacity < 0 )
            {
                throw new ArgumentException( "capacity must be >= 0: " + capacity );
            }
            
            int tableSize = MathUtils.NextPowerOfTwo( Math.Max( 2, ( int )Math.Ceiling( capacity / loadFactor ) ) );

            if ( tableSize > 1 << 30 )
            {
                throw new ArgumentException( "The required capacity is too large: " + capacity );
            }

            return tableSize;
        }

        public static int GetCount( int currentTotal )
        {
            int count;

            if ( currentTotal >= 1000 )
            {
                count = 100;
            }
            else if ( currentTotal >= 100 )
            {
                count = 10;
            }
            else if ( currentTotal >= 50 )
            {
                count = 5;
            }
            else
            {
                count = 1;
            }

            return count;
        }
    }
}