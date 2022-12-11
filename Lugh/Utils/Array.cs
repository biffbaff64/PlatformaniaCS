// ##################################################

using System.Diagnostics.CodeAnalysis;

// ##################################################

namespace Lugh.Utils
{
    /// <summary>
    /// A resizable, ordered or unordered array of objects.
    /// If unordered, this class avoids a memory copy when removing
    /// elements (the last element is moved to the removed element's position).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" )]
    public class Array< T >
    {
        public T[] Items { get; set; }

        public int  Size    { get; set; }
        public bool Ordered { get; set; }

        // ----------------------------------------------------
        // Code
        // ----------------------------------------------------

        /// <summary>
        /// Creates an ordered array with a capacity of 16.
        /// </summary>
        public Array() : this( true, 16 )
        {
        }

        /// <summary>
        /// Creates an ordered array with the specified capacity.
        /// </summary>
        public Array( int capacity ) : this( true, capacity )
        {
        }

        /// <summary>
        /// Creates an array with the specified capacity.
        /// </summary>
        /// <param name="ordered">
        ///     If false, methods that remove elements may change the order
        ///     of other elements in the array, which avoids a memory copy.
        /// </param>
        /// <param name="capacity">
        ///     Any elements added beyond this will cause the backing array
        ///     to be grown.
        /// </param>
        public Array( bool ordered, int capacity )
        {
            Ordered = ordered;
            Items   = new T[ capacity ];
        }

        /// <summary>
        /// Creates a new ordered array containing the elements in the specified
        /// array. The new array will have the same type of backing array. The
        /// capacity is set to the number of elements, so any subsequent elements
        /// added will cause the backing array to be grown.
        /// </summary>
        public Array( T[] array )
        {
            Ordered = true;
            Items   = array;
        }

        /// <summary>
        /// Creates a new array containing the elements in the specified array,
        /// starting from the specified 'startIndex' to 'startIndex + count'.
        /// The new array will have the same type of backing array. The capacity
        /// is set to the number of elements, so any subsequent elements added
        /// will cause the backing array to be grown.
        /// </summary>
        /// <param name="ordered"></param>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        public Array( bool ordered, T[] array, int startIndex, int count )
            : this( ordered, count )
        {
            Items = new T[ ( array.Length - startIndex ) ];

            for ( var i = 0; i < count; i++ )
            {
                Items[ i ] = array[ startIndex + i ];
            }
        }

        public void Add( T value )
        {
            if ( Size == Items.Length )
            {
                Items = Resize( Math.Max( 8, ( int )( Size * 1.75f ) ) );
            }

            Items[ Size++ ] = value;
        }

        public void AddAll()
        {
            // TODO
        }
    
        /// <summary>
        /// Gets the item from the backing array at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T Get( int index )
        {
            if ( index >= Size )
            {
                throw new IndexOutOfRangeException( "index cannot be >= size: " + index + " >= " + Size );
            }

            return Items[ index ];
        }

        protected void Set( int index, T value )
        {
            if ( index >= Size )
            {
                throw new IndexOutOfRangeException("index can't be >= size: " + index + " >= " + Size);
            }

            Items[ index ] = value;
        }

        /// <summary>
        /// Insert the specified value at the supplied index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void Insert( int index, T value )
        {
            if ( index > Size )
            {
                throw new IndexOutOfRangeException
                    (
                     "index can't be > size: " + index + " > " + Size
                    );
            }

            var items = this.Items;

            if ( Size == Items.Length )
            {
                Items = Resize( Math.Max( 8, ( int )( Size * 1.75f ) ) );
            }

            if ( Ordered )
            {
                Array.Copy
                    (
                     Items,
                     index,
                     Items,
                     index + 1,
                     Size  - index
                    );
            }
            else
            {
                Items[ Size ] = Items[ index ];
            }

            Size++;

            Items[ index ] = value;
        }

        /// <summary>
        /// Inserts the specified number of items at the specified index.
        /// The new items will have values equal to the values at those
        /// indices before the insertion. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        protected void InsertRange( int index, int count )
        {
        }

        protected void Swap( int first, int second )
        {
        }

        /// <summary>
        /// Returns true if this array contains the specified value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains( T value )
        {
            var i = Size - 1;

            while ( i >= 0 )
            {
                if ( Items[ i-- ].Equals( value ) )
                {
                    return true;
                }
            }

            return false;
        }
    
        /// <summary>
        /// Returns the index of first occurrence of value in the array,
        /// or -1 if no such value exists.
        /// </summary>
        public int IndexOf( T value )
        {
            for ( int i = 0, n = Size; i < n; i++ )
            {
                if ( Items[ i ].Equals( value ) )
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Removes the first instance of the specified value in the array.
        /// </summary>
        /// <param name="value">The value to remove ( May be null ).</param>
        /// <returns>true if value was found and removed, false otherwise.</returns>
        public bool RemoveValue( T value )
        {
            for ( int i = 0, n = Size; i < n; i++ )
            {
                if ( Items[ i ].Equals( value ) )
                {
                    RemoveIndex( i );

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes and returns the item at the specified index.
        /// </summary>
        public T RemoveIndex( int index )
        {
            if ( index >= Size )
            {
                throw new IndexOutOfRangeException
                    (
                     "index can't be >= size: " + index + " >= " + Size
                    );
            }

            var value = Items[ index ];

            Size--;

            if ( Ordered )
            {
                Array.Copy
                    (
                     Items,
                     index + 1,
                     Items,
                     index,
                     Size - index
                    );
            }
            else
            {
                Items[ index ] = Items[ Size ];
            }

            Items[ Size ] = default;

            return value;
        }

        protected void RemoveRange( int start, int end )
        {
        }

        protected bool RemoveAll( object array )
        {
            return false;
        }

        /// <summary>
        /// Removes and returns the item at size-1.
        /// </summary>
        protected T Pop()
        {
            return default;
        }

        /// <summary>
        /// Returns the item at size-1, but does not remove it.
        /// </summary>
        public T Peek()
        {
            return default;
        }

        /// <summary>
        /// Returns the item at index 0.
        /// </summary>
        public T First()
        {
            return default;
        }
    
        protected void Clear()
        {
        }

        public T[] Shrink()
        {
            return default;
        }

        /// <summary>
        /// Increases the size of the backing array to accommodate the
        /// specified number of additional items. Useful before adding
        /// many items to avoid multiple backing array resizes.
        /// </summary>
        /// <param name="additionalCapacity">The capacity to add.</param>
        /// <returns>The resized array.</returns>
        public T[] AddCapacity( int additionalCapacity )
        {
            return default;
        }
    
        protected T[] SetSize( int newSize )
        {
            return new T[ newSize ];
        }

        /// <summary>
        /// Resizes the backing array to the specified new size.
        /// </summary>
        /// <param name="newSize"></param>
        /// <returns></returns>
        protected T[] Resize( int newSize )
        {
            var items    = this.Items;
            var newItems = new T[ newSize ];

            Array.Copy
                (
                 items,
                 0,
                 newItems,
                 0,
                 Math.Min( Size, newItems.Length )
                );
            this.Items = newItems;

            return newItems;
        }

        protected void Sort()
        {
        }

        protected void Reverse()
        {
        }

        protected void Shuffle()
        {
        }

        protected void Truncate( int newSize )
        {
        }
    }
}