// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class MapObjects
    {
        private List< MapObject > _objects;

        public MapObjects()
        {
            _objects = new List< MapObject >();
        }

        public MapObject Get( int index )
        {
            return _objects[ index ];
        }

        public MapObject Get( string name )
        {
            for ( int i = 0, n = _objects.Count; i < n; i++ )
            {
                var obj = _objects[ i ];

                if ( name.Equals( obj.Name ) )
                {
                    return obj;
                }
            }

            return null;
        }

        public int GetIndex( string name )
        {
            return GetIndex( Get( name ) );
        }

        public int GetIndex( MapObject mapObject )
        {
            return _objects.IndexOf( mapObject );
        }

        public int GetCount()
        {
            return _objects.Count;
        }

        public void Add( MapObject mapObject)
        {
            this._objects.Add( mapObject );
        }

        public void Remove( int index )
        {
            _objects.RemoveAt( index );
        }

        public void Remove( MapObject mapObject)
        {
            _objects.Remove( mapObject );
        }

        public List< T > GetByType< T >( Type type ) where T : MapObject
        {
            return GetByType( type, new List< T >() );
        }
        
        public List<T> GetByType< T >( Type type, List< T > fill ) where T : MapObject
        {
            fill.Clear();

            for ( int i = 0, n = _objects.Count; i < n; i++ )
            {
                var mapObject = _objects[ i ];

                if ( mapObject.GetType() == type )
                {
                    fill.Add( (T) mapObject );
                }
            }

            return fill;
        }
    }
}
