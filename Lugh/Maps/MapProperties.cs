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

        /** @param key property name
	 * @return the value for that property if it exists, otherwise, null */
        public object Get( string key )
        {
            return _properties.Get( key );
        }

        /** Returns the object for the given key, casting it to clazz.
	 * @param key the key of the object
	 * @param clazz the class of the object
	 * @return the object or null if the object is not in the map
	 * @throws ClassCastException if the object with the given key is not of type clazz */
        public <T> T Get( string key, Class< T > clazz )
        {
            return ( T )Get( key );
        }

        /** Returns the object for the given key, casting it to clazz.
	 * @param key the key of the object
	 * @param defaultValue the default value
	 * @param clazz the class of the object
	 * @return the object or the defaultValue if the object is not in the map
	 * @throws ClassCastException if the object with the given key is not of type clazz */
        public <T> T Get( string key, T defaultValue, Class< T > clazz )
        {
            Object object = Get( key );
            return object == null ? defaultValue : ( T )object;
        }

        /** @param key property name
	 * @param value value to be inserted or modified (if it already existed) */
        public void Put( string key, Object value )
        {
            _properties.put( key, value );
        }

        /** @param properties set of properties to be added */
        public void PutAll( MapProperties properties )
        {
            this._properties.putAll( properties._properties );
        }

        /** @param key property name to be removed */
        public void Remove( string key )
        {
            _properties.remove( key );
        }

        /** Removes all properties */
        public void Clear()
        {
            _properties.clear();
        }

        /** @return iterator for the property names */
        public Iterator< string > GetKeys()
        {
            return _properties.keys();
        }

        /** @return iterator to properties' values */
        public Iterator< Object > GetValues()
        {
            return _properties.values();
        }
    }
}
