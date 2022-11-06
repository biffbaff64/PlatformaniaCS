namespace Lugh.Maths
{
    public class SimpleVec2F : Vec2F
    {
        public SimpleVec2F() : base()
        {
        }
        
        public SimpleVec2F( float x, float y ) : base( x, y )
        {
        }

        public void Add( float x, float y )
        {
            Set( X + x, Y + y );
        }

        public void Sub( float x, float y )
        {
            Set( X - x, Y - y );
        }

        public void AddX( float value )
        {
            this.X += value;
        }

        public void AddY( float value )
        {
            this.Y += value;
        }

        public void SubX( float value )
        {
            this.X -= value;
        }

        public void SubY( float value )
        {
            this.Y -= value;
        }

        public void Mul( float mulX, float mulY )
        {
            this.X *= mulX;
            this.Y *= mulY;
        }

        public bool IsEmpty()
        {
            const float tolerance = 0.0001f;
            
            return ( ( Math.Abs( X ) < tolerance ) && ( Math.Abs( Y ) < tolerance ) );
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