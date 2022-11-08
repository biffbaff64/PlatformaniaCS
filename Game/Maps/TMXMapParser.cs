
#region using

using System.Collections;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Physics;
using PlatformaniaCS.Game.Entities.Objects;

#endregion using

namespace PlatformaniaCS.Game.Maps
{
    public class TMXMapParser : IDisposable
    {
        public const int Layer1         = 0;
        public const int Layer2         = 1;
        public const int Layer3         = 2;
        public const int Layer4         = 3;
        public const int Overlays       = 4;
        public const int ObjectMarkers  = 5;
        public const int CollisionLayer = 6;

        public ArrayList        PlacementTiles   { get; set; }
        public TiledMap         CurrentMap       { get; set; }
        public TiledMapRenderer TiledMapRenderer { get; set; }
        public string           CurrentMapName   { get; set; }

        public void InitialiseLevelMap()
        {
            CurrentMapName = App.RoomManager.GetCurrentMapNameWithPath();
            CurrentMap     = App.MainGame.Content.Load<TiledMap>( CurrentMapName );

            if ( TiledMapRenderer == null )
            {
                TiledMapRenderer = new TiledMapRenderer( App.MainGame.GraphicsDevice, CurrentMap );
            }
            
            SetGameLevelMap();

            App.MapData.ScrollDirection.Set( Movement._DIRECTION_STILL, Movement._DIRECTION_STILL );
        }

        public void CreatePositioningData()
        {
        }

        public void ParseObjectBasedMarkerTiles()
        {
        }

        // TODO: For future use, parse collision areas from tiles.
        public void ParseTiledCollision()
        {
        }

        public void ParseAABBObjects()
        {
        }

        public SpriteDescriptor CreatePlacementTile( TiledMapObject mapObject, SpriteDescriptor descriptor ) => new SpriteDescriptor();

        public void SetGameLevelMap()
        {
        }

        public void Dispose()
        {
        }
    }
}