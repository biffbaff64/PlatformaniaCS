// ##################################################

// ##################################################

namespace Lugh.Utils;

public class Long
{
    public static int NumberOfLeadingZeros( long i )
    {
        if ( i > 0 )
        {
            var n = 1;
            var x = ( int )( i >> 32 );

            if ( x == 0 )
            {
                n += 32;
                x =  ( int )i;
            }

            if ( x >> 16 == 0 )
            {
                n +=  16;
                x <<= 16;
            }

            if ( x >> 24 == 0 )
            {
                n +=  8;
                x <<= 8;
            }

            if ( x >> 28 == 0 )
            {
                n +=  4;
                x <<= 4;
            }

            if ( x >> 30 == 0 )
            {
                n +=  2;
                x <<= 2;
            }

            n -= x >> 31;
        
            return n;
        }

        return 64;
    }
}
