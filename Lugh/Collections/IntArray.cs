namespace Lugh.Collections
{
    using System.Collections.Generic;

    public class IntArray
    {
        public List< int > Items     { get; set; }
        public bool        IsOrdered { get; set; }

        public int Size => Items.Count;

        public IntArray() : this( true, 16 )
        {
        }

        public IntArray( int capacity ) : this( true, capacity )
        {
        }

        public IntArray( bool ordered, int capacity )
        {
            IsOrdered = ordered;
            Items     = new List< int >( capacity );
        }

        public IntArray( IntArray array )
        {
            IsOrdered = array.IsOrdered;
            Items     = new List< int >( array.Items );
        }

        public IntArray( int[] array )
            : this( true, array, 0, array.Length )
        {
        }

        public IntArray( bool ordered, int[] array, int startIndex, int count )
            : this( ordered, count )
        {
            Items = new List< int >();

            for ( var i = 0; i < count; i++ )
            {
                Items.Add( array[ i ] );
            }
        }

        public void AddAll( IntArray array )
        {
            AddAll( array.Items, 0, array.Size );
        }

        public void AddAll( IntArray array, int offset, int length )
        {
            AddAll( array.Items, offset, length );
        }

        public void AddAll( List< int > array, int offset, int length )
        {
            for ( var i = offset; i < length; i++ )
            {
                Items.Add( array[ i ] );
            }
        }
    }
}