namespace PlatformaniaCS.Game.Maps
{
    public class Room
    {
        public const int North     = 0;
        public const int East      = 1;
        public const int South     = 2;
        public const int West      = 3;
        public const int Start     = 4;
        public const int Undefined = 5;

        public Room() : this( "" )
        {
        }

        public Room( string roomName )
        {
            RoomName               = roomName;
            Key                    = new JailKey();
            MysteryChestsAvailable = 0;

            Row    = 0;
            Column = 0;

            CompassPoints          = new SimpleVec2[ 5 ];
            CompassPoints[ North ] = new SimpleVec2();
            CompassPoints[ East ]  = new SimpleVec2();
            CompassPoints[ South ] = new SimpleVec2();
            CompassPoints[ West ]  = new SimpleVec2();
            CompassPoints[ Start ] = new SimpleVec2();
        }

        public string       RoomName               { get; set; }
        public int          Row                    { get; set; }
        public int          Column                 { get; set; }
        public SimpleVec2[] CompassPoints          { get; set; }
        public JailKey      Key                    { get; set; }
        public int          MysteryChestsAvailable { get; set; }

        public void Set( Room reference )
        {
            RoomName        = reference.RoomName;
            Key.IsCollected = false;
            Key.IsUsed      = false;

            Row    = reference.Row;
            Column = reference.Column;

            CompassPoints[ North ] = reference.CompassPoints[ North ];
            CompassPoints[ East ]  = reference.CompassPoints[ East ];
            CompassPoints[ South ] = reference.CompassPoints[ South ];
            CompassPoints[ West ]  = reference.CompassPoints[ West ];
            CompassPoints[ Start ] = reference.CompassPoints[ Start ];
        }
    }
}