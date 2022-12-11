using System.Drawing;

namespace Lugh.Maths
{
    public class Box< T >
    {
        public T X      { get; set; }
        public T Y      { get; set; }
        public T Width  { get; set; }
        public T Height { get; set; }

        public void Set( T x, T y, T width, T height )
        {
            X      = x;
            Y      = y;
            Width  = width;
            Height = height;
        }

        public bool Overlaps( Box< T > box )
        {
            return false;
        }

        public bool Overlaps( Rectangle box )
        {
            return false;
        }

        public bool Intersects( Box< T > box ) => false;

        public bool Intersects( Rectangle rectangle ) => false;
    }
}
