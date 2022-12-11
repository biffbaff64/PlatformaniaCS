namespace Lugh.Utils
{
    public class ArrayReflection
    {
        public static object NewInstance< T >( int size ) => new Array< T >( size );
    }
}
