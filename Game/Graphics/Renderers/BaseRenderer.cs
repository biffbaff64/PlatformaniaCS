using Microsoft.Xna.Framework.Graphics;
using PlatformaniaCS.Game.Config;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics.Camera;
using PlatformaniaCS.Game.Graphics.Parallax;

namespace PlatformaniaCS.Game.Graphics.Renderers
{
    public class BaseRenderer : IDisposable
    {
        private const float DefaultHudZoom      = 1.0f;
        private const float DefaultParallaxZoom = 1.0f;
        private const float DefaultMapZoom      = 1.0f;

        private Vector2       _cameraPos;
        private HUDRenderer   _hudRenderer;
        private WorldRenderer _worldRenderer;

        public OrthoGameCamera    HudGameCamera      { get; set; }
        public OrthoGameCamera    OverlayCamera      { get; set; }
        public OrthoGameCamera    SpriteGameCamera   { get; set; }
        public OrthoGameCamera    TiledGameCamera    { get; set; }
        public OrthoGameCamera    ParallaxCamera     { get; set; }
        public Zoom               GameZoom           { get; set; }
        public Zoom               HudZoom            { get; set; }
        public ParallaxBackground ParallaxBackground { get; set; }
        public ParallaxUtils      ParallaxUtils      { get; set; }
        public bool               IsDrawingStage     { get; set; }

        /// <summary>
        /// Create all game cameras and associated viewports.
        /// <remarks>
        /// For 'Camera' read 'RenderTarget2D' until my Camera utils are finished.
        /// </remarks>
        /// </summary>
        public void CreateCameras()
        {
            Trace.CheckPoint();

            LughSystem.Inst().CamerasReady = false;

            // --------------------------------------------------------
            // Camera used for parallax scrolling backgrounds.

            ParallaxCamera = new OrthoGameCamera
                (
                 Gfx.ParallaxSceneWidth,
                 Gfx.ParallaxSceneHeight,
                 "Parallax Cam"
                );

            ParallaxBackground = new ParallaxBackground();
            ParallaxUtils      = new ParallaxUtils();

            // --------------------------------------------------------
            // Camera used for displaying Tiled Maps.

            TiledGameCamera = new OrthoGameCamera
                (
                 Gfx.GameSceneWidth,
                 Gfx.GameSceneHeight,
                 "Tiled Cam"
                );

            // --------------------------------------------------------
            // Camera used for displaying Sprites.

            SpriteGameCamera = new OrthoGameCamera
                (
                 Gfx.GameSceneWidth,
                 Gfx.GameSceneHeight,
                 "Sprite Cam"
                );

            // --------------------------------------------------------
            // Secondary Camera used for display Tiled Map layers
            // intended for drawing on top of the sprite layer.

            OverlayCamera = new OrthoGameCamera
                (
                 Gfx.GameSceneWidth,
                 Gfx.GameSceneHeight,
                 "Overlay Cam"
                );

            // --------------------------------------------------------
            // Camera used for displaying the HUD.

            HudGameCamera = new OrthoGameCamera
                (
                 Gfx.HudWidth,
                 Gfx.HudHeight,
                 "HUD Cam"
                );

            // --------------------------------------------------------

            GameZoom = new Zoom();
            HudZoom  = new Zoom();

            _cameraPos     = new Vector2();
            _worldRenderer = new WorldRenderer();
            _hudRenderer   = new HUDRenderer();

            // --------------------------------------------------------

//            ParallaxCamera.Viewport.Zoom   = DefaultParallaxZoom;
//            TiledGameCamera.Viewport.Zoom  = DefaultMapZoom;
//            SpriteGameCamera.Viewport.Zoom = DefaultMapZoom;
//            OverlayCamera.Viewport.Zoom    = DefaultMapZoom;
//            HudGameCamera.Viewport.Zoom    = DefaultHudZoom;

            // --------------------------------------------------------

            IsDrawingStage                 = false;
            LughSystem.Inst().CamerasReady = true;
        }

        public void Render( float delta )
        {
            var positionSet = false;

            if ( AppConfig.GameScreenActive )
            {
                if ( ( App.GetPlayer() != null ) && App.AppState > StateID._STATE_SETUP )
                {
                    positionSet = true;
                }
            }

            if ( !positionSet && !AppConfig.GameScreenActive )
            {
                App.MapData.MapPosition.Set( 0, 0 );
            }

            DrawParallaxLayers();
            DrawTiledMap();
            DrawSprites();
            DrawOverlays();
            UpdateHudCamera();

            if ( IsDrawingStage )
            {
                // TODO:
            }

            GameZoom.Stop();
            HudZoom.Stop();

            App.WorldModel.DrawDebugMatrix();
        }

        private void DrawParallaxLayers()
        {
            if ( ParallaxCamera.IsInUse )
            {
            }
        }

        private void DrawGameScreenBackdrop()
        {
        }

        private void DrawTiledMap()
        {
            if ( TiledGameCamera.IsInUse )
            {
            }
        }

        private void DrawSprites()
        {
            if ( SpriteGameCamera.IsInUse )
            {
            }
        }

        private void DrawOverlays()
        {
            if ( OverlayCamera.IsInUse )
            {
            }
        }

        private void UpdateHudCamera()
        {
            if ( HudGameCamera.IsInUse )
            {
                App.MainGame.GraphicsDevice.SetRenderTarget( HudGameCamera.RenderTarget2D );
                App.MainGame.GraphicsDevice.DepthStencilState = new DepthStencilState()
                {
                    DepthBufferEnable = true
                };
                App.MainGame.GraphicsDevice.Clear( ClearOptions.Target, Color.Black, 1.0f, 0 );

                App.SpriteBatch.Begin();
                _hudRenderer.Render();
                App.SpriteBatch.End();

                App.MainGame.GraphicsDevice.SetRenderTarget( null );

                App.SpriteBatch.Begin();

                App.SpriteBatch.Draw
                    (
                     texture: HudGameCamera.RenderTarget2D,
                     destinationRectangle: new Rectangle( 0, 0, Gfx.DesktopWidth, Gfx.DesktopHeight ),
                     sourceRectangle: HudGameCamera.RenderTarget2D.Bounds,
                     color: Color.White
                    );

                App.SpriteBatch.End();
            }
        }

        public void ResizeCameras( int width, int height )
        {
        }

        public void EnableCamera( params CamID[] cameraList )
        {
            DisableAllCameras();

            if ( cameraList.Length > 0 )
            {
                foreach ( var id in cameraList )
                {
                    if ( id == CamID._PARALLAX )
                    {
                        ParallaxCamera.IsInUse = true;
                    }
                    else if ( id == CamID._TILED )
                    {
                        TiledGameCamera.IsInUse = true;
                    }
                    else if ( id == CamID._SPRITE )
                    {
                        SpriteGameCamera.IsInUse = true;
                    }
                    else if ( id == CamID._OVERLAY )
                    {
                        OverlayCamera.IsInUse = true;
                    }
                    else if ( id == CamID._HUD )
                    {
                        HudGameCamera.IsInUse = true;
                    }
                    else if ( id == CamID._STAGE )
                    {
                        // TODO:
                    }
                }
            }
        }

        public void ResetCameraZoom()
        {
        }

        public void DisableLerping()
        {
            ParallaxCamera.IsLerpingEnabled   = false;
            TiledGameCamera.IsLerpingEnabled  = false;
            SpriteGameCamera.IsLerpingEnabled = false;
            OverlayCamera.IsLerpingEnabled    = false;
            HudGameCamera.IsLerpingEnabled    = false;
        }

        public void EnableAllCameras()
        {
            ParallaxCamera.IsInUse   = true;
            TiledGameCamera.IsInUse  = true;
            SpriteGameCamera.IsInUse = true;
            OverlayCamera.IsInUse    = true;
            HudGameCamera.IsInUse    = true;
        }

        public void DisableAllCameras()
        {
            ParallaxCamera.IsInUse   = false;
            TiledGameCamera.IsInUse  = false;
            SpriteGameCamera.IsInUse = false;
            OverlayCamera.IsInUse    = false;
            HudGameCamera.IsInUse    = false;
        }

        public void Dispose()
        {
        }
    }
}
