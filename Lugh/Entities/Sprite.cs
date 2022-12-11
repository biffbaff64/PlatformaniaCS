// ##################################################

// ##################################################

using System.Drawing;

namespace Lugh.Entities
{
    public class Sprite
    {
        public Rectangle BoundingRectangle { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public int Width  { get; set; }
        public int Height { get; set; }
    }
}
