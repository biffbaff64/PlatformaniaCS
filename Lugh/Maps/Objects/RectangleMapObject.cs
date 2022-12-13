// ##################################################

// ##################################################

using System.Drawing;

namespace Lugh.Maps.Objects;

public class RectangleMapObject : MapObject
{
    public Rectangle Rectangle { get; set; }

    /// <summary>
    /// Creates a rectangle object which lower left corner is at (0, 0) with width=1 and height=1
    /// </summary>
    public RectangleMapObject() : this( 0.0f, 0.0f, 1.0f, 1.0f )
    {
    }

    /// <summary>
    /// Creates a <see cref="Rectangle"/> object with the given X and Y coordinates
    /// along with a given width and height.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="width">Width of the Rectangle to be created..</param>
    /// <param name="height">Height of the Rectangle to be created..</param>
    public RectangleMapObject( float x, float y, float width, float height )
    {
        Rectangle = new Rectangle( ( int )x, ( int )y, ( int )width, ( int )height );
    }
}
