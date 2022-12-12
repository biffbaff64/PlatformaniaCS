// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class Map : IDisposable
    {
        public MapLayers     Layers     { get; set; } = new MapLayers();
        public MapProperties Properties { get; set; } = new MapProperties();

        public void Dispose()
        {
            GC.SuppressFinalize( this );

            Layers     = null;
            Properties = null;
        }
    }
}
