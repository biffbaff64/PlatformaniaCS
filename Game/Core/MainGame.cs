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

        Window.AllowUserResizing = false;
        Content.RootDirectory    = "Content/bin/Assets";
        IsMouseVisible           = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        
        GdxSystem.Inst().Setup();
        GdxSystem.Inst().LogLevel = GdxSystem.LogDebug;

        Trace.EnableWriteToFile = true;
        Trace.OpenDebugFile( "log.txt", true );
        Trace.CheckPoint();

        App.MainGame = this;

        Gfx.SetDesktopDimensions();

        _splashScreen = new SplashScreen();
        _splashScreen.Setup( GameAssets.SplashScreenAsset );

        // Initialise all essential objects required before
        // the main screen is initialised.
        App.AppConfig = new AppConfig();
        App.AppConfig.Setup();
    }

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
            App.Scene.Update();
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
            App.Scene.Render( 0f );
        }
    }
}