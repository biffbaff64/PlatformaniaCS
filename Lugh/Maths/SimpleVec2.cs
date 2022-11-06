using System.Reflection;

namespace Lugh.Maths
{
    public class SimpleVec2 : Vec2
    {
        public SimpleVec2() : base()
        {
        }
        
        public SimpleVec2( int x, int y ) : base( x, y )
        {
        }

        public void Add( int x, int y )
        {
            Set( X + x, Y + y );
        }

        public void Sub( int x, int y )
        {
            Set( X - x, Y - y );
        }

        public void AddX( int value )
        {
            this.X += value;
        }

        public void AddY( int value )
        {
            this.Y += value;
        }

        public void SubX( int value )
        {
            this.X -= value;
        }

        public void SubY( int value )
        {
            this.Y -= value;
        }

        public void Mul( int mulX, int mulY )
        {
            this.X *= mulX;
            this.Y *= mulY;
        }

        public void Set( float x, float y )
        {
            this.X = ( int ) x;
            this.Y = ( int ) y;
        }

        public bool IsEmpty()
        {
            return ( ( X == 0 ) && ( Y == 0 ) );
        }

        public void SetEmpty()
        {
            this.X = 0;
            this.Y = 0;
        }

        public override string ToString()
        {
            return "X: " + X + ", Y:" + Y;
        }
    }
}