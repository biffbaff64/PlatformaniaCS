// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class MapProperties
    {
        private ObjectMap< string, object > _properties;

        /// <summary>
        /// Creates an empty properties set.
        /// </summary>
        public MapProperties()
        {
            _properties = new ObjectMap< string, object >();
        }

        public bool ContainsKey( string key )
        {
            return _properties.ContainsKey( key );
        }

        public object Get( string key )
        {
            return _properties.Get( key );
        }

        public T Get< T >( string key, Type type )
        {
            return ( T )Get( key );
        }

        public T Get< T >( string key, T defaultValue, Type type )
        {
            object obj = Get( key );
            return ( obj == null ) ? default : ( T )obj;
        }

        public void Put( string key, Object value )
        {
            _properties.Put( key, value );
        }

        public void PutAll( MapProperties properties )
        {
            this._properties.PutAll( properties._properties );
        }

        public void Remove( string key )
        {
            _properties.Remove( key );
        }

        public void Clear()
        {
            _properties.Clear();
        }

        public IEnumerator< string > GetKeys()
        {
            return _properties.Keys();
        }

        public IEnumerator< object > GetValues()
        {
            return _properties.Values();
        }
    }
}
