// ##################################################

// ##################################################

namespace Lugh.Maps.Objects;

public class PolylineMapObject : MapObject
{
    public Polyline Polyline { get; set; }

    /** Creates empty polyline */
    public PolylineMapObject() : this( Array.Empty< float >() )
    {
    }

    /** @param vertices polyline defining vertices */
    public PolylineMapObject( float[] vertices )
    {
        Polyline = new Polyline( vertices );
    }

    /** @param polyline the polyline */
    public PolylineMapObject( Polyline polyline )
    {
        this.Polyline = polyline;
    }
}
