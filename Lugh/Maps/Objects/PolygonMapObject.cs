// ##################################################

// ##################################################

namespace Lugh.Maps.Objects
{
    public class PolygonMapObject : MapObject
    {
        public Polygon Polygon { get; set; }

        /** Creates empty polygon map object */
        public PolygonMapObject() : this( Array.Empty< float >() )
        {
        }

        /** @param vertices polygon defining vertices (at least 3) */
        public PolygonMapObject( float[] vertices )
        {
            Polygon = new Polygon( vertices );
        }

        /** @param polygon the polygon */
        public PolygonMapObject( Polygon polygon )
        {
            this.Polygon = polygon;
        }
    }
}
