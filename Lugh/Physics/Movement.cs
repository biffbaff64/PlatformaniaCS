namespace Lugh.Physics
{
    public class Movement
    {
        public const int _HORIZONTAL       = 1;
        public const int _VERTICAL         = 2;
        public const int _DIRECTION_IN     = 1;
        public const int _DIRECTION_OUT    = -1;
        public const int _FORWARDS         = 1;
        public const int _BACKWARDS        = -1;
        public const int _DIRECTION_RIGHT  = 1;
        public const int _DIRECTION_LEFT   = -1;
        public const int _DIRECTION_UP     = 1;
        public const int _DIRECTION_DOWN   = -1;
        public const int _DIRECTION_STILL  = 0;
        public const int _DIRECTION_CUSTOM = 2;

        private readonly string[,] _aliases =
        {
            { "LEFT ", "STILL", "RIGHT" },
            { "DOWN ", "STILL", "UP   " },
        };

        public string GetAliasX( int dir ) => _aliases[ 0, dir + 1 ];
        public string GetAliasY( int dir ) => _aliases[ 1, dir + 1 ];
    }
}