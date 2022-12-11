// ########################################################

using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;
using OrthographicCamera = MonoGame.Extended.OrthographicCamera;

// ########################################################

namespace PlatformaniaCS.Game.UI
{
    public class SplashScreen
    {
        public bool IsAvailable { get; set; }

        private OrthographicCamera _camera;
        private SpriteBatch        _batch;
        private Texture2D          _background;
        private Stopwatch          _stopwatch;
        private string             _assetName;

        public void Setup( string assetName )
        {
            Trace.CheckPoint();

            _stopwatch  = Stopwatch.StartNew();
            _background = AssetUtils.LoadAsset< Texture2D >( assetName );
            _assetName  = assetName;
            _batch      = new SpriteBatch( App.MainGame.GraphicsDevice );

            var viewportAdapter = new BoxingViewportAdapter
                (
                 App.MainGame.Window,
                 App.MainGame.GraphicsDevice,
                 Gfx.DesktopWidth,
                 Gfx.DesktopHeight
                );

            _camera = new OrthographicCamera( viewportAdapter );

            IsAvailable = true;
        }

        public void Update()
        {
            if ( _stopwatch.ElapsedMilliseconds > 2500 )
            {
                IsAvailable = false;
            }
        }

        public void Render()
        {
            if ( IsAvailable )
            {
                var transformMatrix = _camera.GetViewMatrix();

                _batch.Begin( transformMatrix: transformMatrix );

                _batch.Draw
                    (
                     _background,
                     new Rectangle( 0, 0, Gfx.DesktopWidth, Gfx.DesktopHeight ),
                     Color.White
                    );

                _batch.End();
            }
        }

        public void Dispose()
        {
            Trace.CheckPoint();

            AssetUtils.UnloadAsset( _assetName );
        
            _background.Dispose();
            _background = null;

            _stopwatch = null;

            _camera = null;
            _camera = null;

            _batch.Dispose();
            _batch = null;
        }
    }
}