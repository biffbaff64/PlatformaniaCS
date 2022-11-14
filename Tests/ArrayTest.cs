// ##################################################

// ##################################################

using System.Runtime.CompilerServices;

namespace Tests;

public class ArrayTest
{
    private readonly int[] _testArray =
    {
        1,2,3,4,5,6,7,8,9,10
    };
    
    public void TestArrayClass()
    {
        var array1 = new Array< TextureRegion >();
        var array2 = new Array< string >( false, 32 );
        var array3 = new Array< int >( 16 );
        var array4 = new Array< int >( _testArray );
        var array5 = new Array< int >( array3.Items );
        var array6 = new Array< int >( true, _testArray, 4, 3 );
    }
}
