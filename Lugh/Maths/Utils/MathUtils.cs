namespace Lugh.Maths
{
    public class MathUtils
    {
        public const double RadiansToDegrees = 180f    / Math.PI;
        public const double DegreesToRadians = Math.PI / 180f;

        private static readonly Random random = new Random();
    
        public static float CosDeg( float degrees )
        {
            return 0;
        }

        public static float SinDeg( float degrees )
        {
            return 0;
        }

        public static int RndInt( int upper )
        {
            return random.Next( upper );
        }

        public static float RndFloat( int upper )
        {
            return random.NextSingle() * upper;
        }

        public static double RndDouble( int upper )
        {
            return random.NextDouble() * upper;
        }

        public static int NextPowerOfTwo( int max )
        {
            return 0;
        }
    }
}
