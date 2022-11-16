using PlatformaniaCS.Game.Config;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.UI;

namespace PlatformaniaCS.Game.Scenes;

public class MainScene : BaseScene
{
    public EndgameManager     EndgameManager     { get; set; }
    public MainGameHandler    MainGameHandler    { get; set; }
    public GameCompletedPanel GameCompletedPanel { get; set; }
    public bool               FirstTime          { get; set; }

    public MainScene()
    {
        FirstTime = true;
    }

    public override void Initialise()
    {
        if ( FirstTime )
        {
            Trace.Divider( '#' );
            Trace.Dbg( message: "NEW GAME:" );
            Trace.Dbg( message: "DEVMODE: ", args: App.Developer.IsDevMode );
            Trace.Dbg( message: "GODMODE: ", args: App.Developer.IsGodMode );
            Trace.Divider( '#' );

            EndgameManager  = new EndgameManager();
            MainGameHandler = new MainGameHandler();

            App.LevelManager.PrepareNewGame();
            App.AppState = StateID._STATE_SETUP;
        }
    }

    public override void Update( GameTime gameTime )
    {
        if ( AppConfig.GameScreenActive )
        {
            switch ( App.AppState )
            {
                case StateID._STATE_MAIN_MENU:
                case StateID._STATE_CLOSING:
                {
                    break;
                }

                case StateID._STATE_SETUP:
                case StateID._STATE_GET_READY:
                case StateID._STATE_WELCOME_PANEL:
                case StateID._STATE_DEVELOPER_PANEL:
                case StateID._STATE_PAUSED:
                case StateID._STATE_GAME:
                case StateID._STATE_MESSAGE_PANEL:
                case StateID._STATE_PREPARE_LEVEL_RETRY:
                case StateID._STATE_LEVEL_RETRY:
                case StateID._STATE_PREPARE_LEVEL_FINISHED:
                case StateID._STATE_LEVEL_FINISHED:
                case StateID._STATE_PREPARE_GAME_OVER_MESSAGE:
                case StateID._STATE_GAME_OVER:
                case StateID._STATE_GAME_FINISHED:
                case StateID._STATE_END_GAME:
                {
                    MainGameHandler.Update();
                    break;
                }
            }
        }
    }

    public override void Render( GameTime gameTime )
    {
        App.MapData.Update();
        App.GameProgress.Update();

        if ( AppConfig.GameScreenActive )
        {
            Update( gameTime );
            
            App.BaseRenderer.Render( gameTime.GetElapsedSeconds() );

            App.WorldModel.WorldStep();
        }
    }

    public override void Show()
    {
        Trace.CheckPoint();

        LoadImages();

        LughSystem.Inst().CurrentScreenID = ScreenID._GAME_SCREEN;

        App.BaseRenderer.DisableAllCameras();
        App.WorldModel.Activate();

        Initialise();

        App.AppState = StateID._STATE_SETUP;
    }

    public override void Hide()
    {
        Trace.CheckPoint();

        App.WorldModel.DeActivate();
    }

    public void Reset()
    {
        FirstTime = true;
    }

    private void LoadImages()
    {
    }

    public override string Name() => "Main Scene";
}