// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class MapObjects
    {
        private Array< MapObject > _objects;

        public MapObjects()
        {
            _objects = new Array< MapObject >();
        }

        public MapObject Get( int index )
        {
            return _objects.get( index );
        }

        public MapObject Get( string name )
        {
            for ( int i = 0, n = _objects.size; i < n; i++ )
            {
                MapObject object = _objects.get( i );

                if ( name.equals( object.getName() ) )
                {
                    return object;
                }
            }

            return null;
        }

        public int GetIndex( string name )
        {
            return GetIndex( Get( name ) );
        }

        public int GetIndex( MapObject object)
        {
            return _objects.indexOf( object, true );
        }

        public int GetCount()
        {
            return _objects.size;
        }

        public void Add( MapObject object)
        {
            this._objects.add( object );
        }

        public void Remove( int index )
        {
            _objects.removeIndex( index );
        }

        public void Remove( MapObject object)
        {
            _objects.removeValue( object, true );
        }

        public <T Extends MapObject> Array< T > GetByType( Class< T > type )
        {
            return GetByType( type, new Array< T >() );
        }

        public <T Extends MapObject> Array< T > GetByType( Class< T > type, Array< T > fill )
        {
            fill.clear();

            for ( int i = 0, n = _objects.size; i < n; i++ )
            {
                MapObject object = _objects.get( i );

                if ( ClassReflection.isInstance( type, object ) )
                {
                    fill.add( ( T )object );
                }
            }

            return fill;
        }
    }
}
