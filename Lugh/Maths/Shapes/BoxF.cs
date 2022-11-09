namespace Lugh.Maths;

public class BoxF
{
    public float X      { get; set; }
    public float Y      { get; set; }
    public float Width  { get; set; }
    public float Height { get; set; }

    public void Set( float x, float y, float width, float height )
    {
        X      = x;
        Y      = y;
        Width  = width;
        Height = height;
    }

    public bool Intersects( BoxF box ) => false;

    public bool Intersects( Rectangle rectangle ) => false;
}