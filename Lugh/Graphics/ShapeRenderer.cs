namespace Lugh.Graphics
{
    public class ShapeRenderer
    {
        public enum ShapeType
        {
            Point,
            Line,
            Filled
        }

        public void Rect
        (
                float x,       float y,
                float originX, float originY,
                float width,   float height,
                float scaleX,  float scaleY,
                float degrees
        )
        {
        }
        
        public void Set( ShapeType type )
        {
        }

        public void SetColor( Color color )
        {
        }

        public void SetColor( float r, float g, float b, float a )
        {
        }
    }
}