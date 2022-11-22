// ##################################################

using Microsoft.Xna.Framework.Input;
using PlatformaniaCS.Game.Config;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.UI;

// ##################################################

namespace PlatformaniaCS.Game.Core;

public class MainGame : Microsoft.Xna.Framework.Game
{
    private SplashScreen _splashScreen;

    public MainGame()
    {
        App.GraphicsDeviceManager = new GraphicsDeviceManager( this );

        App.GraphicsDeviceManager.PreferredBackBufferWidth  = Gfx.DesktopWidth;
        App.GraphicsDeviceManager.PreferredBackBufferHeight = Gfx.DesktopHeight;
        App.GraphicsDeviceManager.ApplyChanges();
        
        Window.AllowUserResizing = false;
        Content.RootDirectory    = "Content/bin/Assets";
        IsMouseVisible           = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        LughSystem.Inst().Setup();
        LughSystem.Inst().LogLevel = LughSystem.LogDebug;

        Trace.EnableWriteToFile = true;
        Trace.OpenDebugFile( "log.txt", true );
        Trace.CheckPoint();

        // Initialise the preferences object for in-game settings.
        App.Settings = new Settings();
        App.MainGame = this;

        _splashScreen = new SplashScreen();
        _splashScreen.Setup( GameAssets.SplashScreenAsset );

        // Initialise all essential objects required before
        // the main screen is initialised.
        App.AppConfig = new AppConfig();
        App.AppConfig.Setup();
    }

    /// <summary>
    /// Monogame uses this to load assets at the start of the game,
    /// but I will be (eventually) using a LibGDX style assetManager.
    /// </summary>
    protected override void LoadContent()
    {
    }

    protected override void Update( GameTime gameTime )
    {
        if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed
          || Keyboard.GetState().IsKeyDown( Keys.Escape ) )
        {
            Exit();
        }

        if ( _splashScreen.IsAvailable )
        {
            _splashScreen.Update();

            if ( !_splashScreen.IsAvailable )
            {
                _splashScreen.Dispose();

                Trace.Dbg( message: "Splashscreen Closed." );

                App.AppConfig.CloseStartup();
            }
        }
        else
        {
            App.Scene.Render( gameTime );
        }
    }

    protected override void Draw( GameTime gameTime )
    {
        if ( _splashScreen.IsAvailable )
        {
            if ( !App.AppConfig.IsStartupDone )
            {
                App.AppConfig.StartApp();
            }

            _splashScreen.Render();
        }
        else
        {
            App.Scene.Render( gameTime );
        }
    }
}