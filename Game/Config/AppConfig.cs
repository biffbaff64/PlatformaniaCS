// ##################################################

using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.Scenes;

// ##################################################

namespace PlatformaniaCS.Game.Config
{
    public class AppConfig
    {
        private StateID _startupState;

        public AppConfig()
        {
            Trace.CheckPoint();

            _startupState = StateID._STATE_BEGIN_STARTUP;
        }

        public void Setup()
        {
            Trace.CheckPoint();

            App.CreateEssentialObjects();

            // -------------------------------------
            App.Developer.SetDeveloperModeState();
            App.Developer.SetTempDeveloperSettings();
            // -------------------------------------

            Gfx.Initialise();
            GdxSystem.Inst().Setup();

            Stats.Setup( "PlatformaniaCS.meters" );

            //
            // These essential objects have now been created.
            // Setup/Initialise for any essential objects required
            // before TitleScene can be created is mostly
            // performed in startApp().
        }

        public void StartApp()
        {
            Trace.CheckPoint();

            App.WorldModel.CreateWorld();
            App.Assets.Initialise();
            App.Settings.FreshInstallCheck();
            App.Settings.DebugReport();

            App.BaseRenderer.CreateCameras();
            App.WorldModel.CreateB2DRenderer();
            App.GameAudio.Setup();
            App.InputManager.Setup();

            _startupState = StateID._STATE_END_STARTUP;
        }

        /// <summary>
        /// Ends the startup process by handing control to the
        /// <see cref="TitleScene"/> or, if TitleScene is disabled,
        /// control is passed to <see cref="MainScene"/>
        /// </summary>
        public void CloseStartup()
        {
            Trace.CheckPoint();

            App.Developer.ConfigReport();

            // Development option, to allow skipping of the main menu
            // and moving straight to the game scene.
            if ( App.Developer.IsDevMode && App.Settings.IsDisabled( Settings.MenuScene ) )
            {
                Trace.Dbg( message: "Triggering Main Scene." );

                App.CreateMainsceneObjects();
                App.MainScene = new MainScene();
                App.MainScene.Reset();
                App.Scene = App.MainScene;
            }
            else
            {
                Trace.Dbg( message: "Triggering Title Scene." );

                App.TitleScene = new TitleScene();
                App.Scene      = App.TitleScene;
            }
        }

        /// <summary>
        /// Pause the game.
        /// Ensure all necessary states are set correctly.
        /// </summary>
        public void Pause()
        {
            App.AppState                = StateID._STATE_PAUSED;
            GdxSystem.Inst().GamePaused = true;

            if ( ( App.Hud.HudStateID != StateID._STATE_SETTINGS_PANEL )
              && ( App.Hud.HudStateID != StateID._STATE_DEVELOPER_PANEL ) )
            {
                App.Hud.HudStateID = StateID._STATE_PAUSED;
            }
        }

        /// <summary>
        /// Un-Pause the game.
        /// </summary>
        public void UnPause()
        {
            App.AppState                = StateID._STATE_GAME;
            GdxSystem.Inst().GamePaused = false;
            App.Hud.HudStateID          = StateID._STATE_PANEL_UPDATE;
        }

        public bool IsStartupDone
        {
            get => ( _startupState == StateID._STATE_END_STARTUP );
        }

        public static bool GameScreenActive
        {
            get => ( GdxSystem.Inst().CurrentScreenID == ScreenID._GAME_SCREEN );
        }
    }
}