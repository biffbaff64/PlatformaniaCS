using System.Collections.Generic;

using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Entities.Objects;
using PlatformaniaCS.Game.Graphics;

namespace PlatformaniaCS.Game.Maps
{
    public class MapData : IDisposable
    {
        // ---------------------------------------------------------------------

        public int MapWidth   { get; set; }
        public int MapHeight  { get; set; }
        public int TileWidth  { get; set; }
        public int TileHeight { get; set; }

        public BoxF       ViewportBox         { get; set; }
        public BoxF       EntityWindow        { get; set; }
        public SimpleVec2 MapPosition         { get; set; }
        public Vec2       MaxScroll           { get; set; }
        public Vec2       MinScroll           { get; set; }
        public Vec2       ScrollDirection     { get; set; }
        public SimpleVec2 PreviousMapPosition { get; set; }
        public SimpleVec2 CheckPoint          { get; set; }
        public Rectangle  MapBox              { get; set; }

        public List<SpriteDescriptor> PlacementTiles   { get; set; }
        public List<Rectangle>        WaterList        { get; set; }
        public List<Doorway>          DoorList         { get; set; }
        public List<Rectangle>        CheckPointList   { get; set; }
        public int                    CheckPointNumber { get; set; }

        // ---------------------------------------------------------------------

        public MapData()
        {
            Trace.CheckPoint();

            ViewportBox         = new BoxF();
            EntityWindow        = new BoxF();
            MapPosition         = new SimpleVec2();
            PreviousMapPosition = new SimpleVec2();
            CheckPoint          = new SimpleVec2();
            MapBox              = new Rectangle();
            MaxScroll           = new Vec2();
            MinScroll           = new Vec2();
            ScrollDirection     = new Vec2();
            PlacementTiles      = new List<SpriteDescriptor>();
            WaterList           = new List<Rectangle>();
            DoorList            = new List<Doorway>();
            CheckPointList      = new List<Rectangle>();
            CheckPointNumber    = -1;
        }

        /// <summary>
        /// Update the screen virtual window. This box is used for checking
        /// that entities are visible on screen.
        /// An extended virtual window is also updated, which is larger than
        /// the visible screen, and can be used for tracking entities that
        /// are nearby.
        /// NOTE: These windows will NOT be updated if the MainPlayer has
        /// not been initialised, as they use its map position.
        /// </summary>
        public void Update()
        {
            if ( App.GetPlayer() != null )
            {
                ViewportBox.X      = ( App.GetPlayer().Position.X - ( Gfx.ViewWidth  / 2f ) );
                ViewportBox.Y      = ( App.GetPlayer().Position.Y - ( Gfx.ViewHeight / 2f ) );
                ViewportBox.Width  = Gfx.ViewWidth;
                ViewportBox.Height = Gfx.ViewHeight;
            }

            if ( App.GetPlayer() != null )
            {
                EntityWindow.X      = ( App.GetPlayer().Position.X - ( Gfx.ViewWidth  + ( Gfx.ViewWidth  / 2f ) ) );
                EntityWindow.Y      = ( App.GetPlayer().Position.Y - ( Gfx.ViewHeight + ( Gfx.ViewHeight / 2f ) ) );
                EntityWindow.Width  = ( Gfx.ViewWidth  * 3 );
                EntityWindow.Height = ( Gfx.ViewHeight * 3 );
            }
        }

        /// <summary>
        /// Updates the map position with the supplied coordinates.
        /// </summary>
        public void PositionAt( int x, int y )
        {
            PreviousMapPosition.Set( MapPosition.X, MapPosition.Y );

            float xPosition = x;
            float yPosition = y;

            if ( App.GetPlayer() != null )
            {
                xPosition += ( App.GetPlayer().FrameWidth  / 2f );
                yPosition += ( App.GetPlayer().FrameHeight / 2f );
            }

            xPosition -= ( float )( Gfx.ViewWidth  / 2f );
            yPosition -= ( float )( Gfx.ViewHeight / 2f );
            xPosition =  Math.Max( MinScroll.X, xPosition );
            xPosition =  Math.Min( xPosition, MaxScroll.X );
            yPosition =  Math.Max( MinScroll.Y, yPosition );
            yPosition =  Math.Min( yPosition, MaxScroll.Y );

            MapPosition.Set( xPosition, yPosition );
            App.BaseRenderer.ParallaxUtils.Scroll();
        }

        public void Dispose()
        {
        }
    }
}