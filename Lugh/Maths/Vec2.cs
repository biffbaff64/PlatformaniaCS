namespace Lugh.Maths
{
    public class Vec2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vec2() : this( 0, 0 )
        {
        }

        public Vec2( int x, int y )
        {
            Set( x, y );
        }

        public void Set( int x, int y )
        {
            this.X = x;
            this.Y = y;
        }
    }
}