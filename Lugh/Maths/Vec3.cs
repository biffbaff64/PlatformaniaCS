namespace Lugh.Maths
{
    public class Vec3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vec3() : this( 0, 0, 0 )
        {
        }

        public Vec3( int x, int y, int z )
        {
            Set( x, y, z );
        }

        public void Set( int x, int y, int z )
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}