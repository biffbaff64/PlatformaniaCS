// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class MapLayers
    {
        public List< MapLayer > Layers { get; set; } = new List< MapLayer >();

        /// <summary>
        /// Gets the map layer specified by the supplied name.
        /// </summary>
        public MapLayer Get( string name )
        {
            for ( int i = 0, n = Layers.Count; i < n; i++ )
            {
                var layer = Layers[ i ];

                if ( name.Equals( layer.Name ) )
                {
                    return layer;
                }
            }

            return null;
        }

        public int GetIndex( string name ) => GetIndex( Get( name ) );

        public int GetIndex( MapLayer layer ) => Layers.IndexOf( layer );

        public int GetCount() => Layers.Count;

        public int Size() => Layers.Count;

        public void Add( MapLayer layer )
        {
            this.Layers.Add( layer );
        }

        public void Remove( int index )
        {
            Layers.RemoveAt( index );
        }

        public void Remove( MapLayer layer )
        {
            Layers.Remove( layer );
        }

        /// <summary>
        /// 
        /// </summary>
        public List< T > GetByType< T >( Type type ) where T : MapLayer
        {
            return GetByType( type, new List< T >() );
        }

        /// <summary>
        /// 
        /// </summary>
        public List< T > GetByType< T >( Type type, List< T > fill ) where T : MapLayer
        {
            fill.Clear();

            for ( int i = 0, n = Layers.Count; i < n; i++ )
            {
                var layer = Layers[ i ];

                if ( Layers.GetType() == type )
                {
                    fill.Add( ( T )layer );
                }
            }

            return fill;
        }

        public void Dispose()
        {
        }
    }
}
