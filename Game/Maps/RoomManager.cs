using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Entities.Objects;

namespace PlatformaniaCS.Game.Maps
{
    public class RoomManager
    {
        private const string Room001 = "map1.tmx";
        private const string Room002 = "map4.tmx";
        private const string Room003 = "map5.tmx";
        private const string Room004 = "map11.tmx";
        private const string Room005 = "map8.tmx";
        private const string Room006 = "map7.tmx";

        private const string Room007 = "map1.tmx";
        private const string Room008 = "map1.tmx";
        private const string Room009 = "map1.tmx";
        private const string Room010 = "map1.tmx";
        private const string Room011 = "map1.tmx";
        private const string Room012 = "map1.tmx";
        private const string Room013 = "map1.tmx";
        private const string Room014 = "map1.tmx";
        private const string Room015 = "map1.tmx";
        private const string Room016 = "map1.tmx";
        private const string Room017 = "map1.tmx";
        private const string Room018 = "map1.tmx";
        private const string Room019 = "map1.tmx";
        private const string Room020 = "map1.tmx";

        private const string MapsPath = "maps/";

        private readonly Room[] _roomMap =
        {
                // -----------------------------------------
                null,
                // -----------------------------------------

                new Room( Room001 ),
                new Room( Room002 ),
                new Room( Room003 ),
                new Room( Room004 ),
                new Room( Room005 ),
                new Room( Room006 ),
                new Room( Room007 ),
                new Room( Room008 ),
                new Room( Room009 ),
                new Room( Room010 ),
                new Room( Room011 ),
                new Room( Room012 ),
                new Room( Room013 ),
                new Room( Room014 ),
                new Room( Room015 ),
                new Room( Room016 ),
                new Room( Room017 ),
                new Room( Room018 ),
                new Room( Room019 ),
                new Room( Room020 ),

                // -----------------------------------------
                null
                // -----------------------------------------
        };

        private readonly int _worldHeight;

        private int _worldWidth;

        public RoomManager()
        {
            _worldWidth  = 1;
            _worldHeight = _roomMap.Length;
        }

        public Room ActiveRoom { get; set; }

        public void Initialise()
        {
            Trace.CheckPoint();

            ActiveRoom = new Room();

            SetRoom( App.GetLevel() );
        }

        public void SetRoom( int row )
        {
            if ( _roomMap[ row ] != null )
            {
                ActiveRoom.Set( _roomMap[ row ] );

                ActiveRoom.Row    = row;
                ActiveRoom.Column = 0;
            }
        }

        private int FindRoom( string roomName )
        {
            var roomPosition = 0;

            for ( var row = 0; row < _worldHeight; row++ )
            {
                if ( _roomMap[ row ] != null )
                {
                    if ( roomName.Equals( _roomMap[ row ].RoomName ) )
                    {
                        roomPosition = row;
                    }
                }
            }

            return roomPosition;
        }

        public string GetActiveRoomName()
        {
            var name = "null";

            if ( _roomMap[ ActiveRoom.Row ] != null )
            {
                name = _roomMap[ ActiveRoom.Row ].RoomName;
            }

            return name;
        }

        public string GetCurrentMapNameWithPath() => MapsPath + _roomMap[ ActiveRoom.Row ].RoomName;

        public void SetPlayerStart()
        {
            foreach ( SpriteDescriptor descriptor in App.MapData.PlacementTiles )
            {
                if ( descriptor.GID == GraphicID.G_PLAYER )
                {
                    App.GetPlayer().InitXYZ.X = ( descriptor.Position.X * App.MapData.TileWidth );
                    App.GetPlayer().InitXYZ.Y = ( descriptor.Position.Y * App.MapData.TileHeight );
                    App.GetPlayer().InitXYZ.Z = App.EntityUtils.GetInitialZPosition( GraphicID.G_PLAYER );
                }
            }
        }
    }
}