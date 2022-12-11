// ##################################################

// ##################################################

namespace Lugh.Maps
{
    public class MapGroupLayer : MapLayer
    {
        public MapLayers MapLayers { get; set; } = new MapLayers();

        public new void InvalidateRenderOffset()
        {
            base.InvalidateRenderOffset();

            for ( var i = 0; i < MapLayers.Size(); i++ )
            {
                var child = MapLayers.Layers[ i ];
                
                child.InvalidateRenderOffset();
            }
        }
    }
}
