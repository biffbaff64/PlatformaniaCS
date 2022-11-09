// ##################################################

using PlatformaniaCS.Game.Audio;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.UI;

// ##################################################

namespace PlatformaniaCS.Game.Core;

public class MainGameHandler
{
    private bool _isWaitingForPlayer;

    public MainGameHandler()
    {
        Trace.CheckPoint();
    }

    public void Update()
    {
        switch ( App.AppState )
        {
            //
            // Initialise the current level.
            // If the level is restarting, that will
            // also be handled here.
            case StateID._STATE_SETUP:
            {
                StateSetup();
                break;
            }

            //
            // Display and update the 'Get Ready' message.
            case StateID._STATE_GET_READY:
            {
                StateGetReady();
                break;
            }

            //
            // The main game 'loop'
            case StateID._STATE_DEVELOPER_PANEL:
            case StateID._STATE_WELCOME_PANEL:
            case StateID._STATE_SETTINGS_PANEL:
            case StateID._STATE_GAME:
            {
                StateGame();
                break;
            }

            case StateID._STATE_PAUSED:
            {
                StatePaused();
                break;
            }

            case StateID._STATE_MESSAGE_PANEL:
            {
                StateMessagePanel();
                break;
            }

            //
            // Player lost a life.
            // Trying again.
            case StateID._STATE_PREPARE_LEVEL_RETRY:
            case StateID._STATE_LEVEL_RETRY:
            {
                StateSetForRetry();
                break;
            }

            case StateID._STATE_PREPARE_LEVEL_FINISHED:
            case StateID._STATE_LEVEL_FINISHED:
            {
                StateSetForLevelFinished();
                break;
            }

            case StateID._STATE_PREPARE_GAME_OVER_MESSAGE:
            {
                StateSetForGameOverMessage();
                break;
            }

            case StateID._STATE_GAME_OVER:
            case StateID._STATE_GAME_FINISHED:
            {
                StateWaitForGameOverMessage();
                break;
            }

            //
            // Back to TitleScene.
            case StateID._STATE_END_GAME:
            {
                StateSetForEndGame();
                break;
            }

            default:
            {
                Trace.Dbg( message: "Unsupported gameState: ", args: App.AppState );
                break;
            }
        }
    }


    /**
     * Initialise the current level.
     * If the level is restarting, that will also be handled here.
     * _STATE_SETUP
     */
    private void StateSetup()
    {
        Trace.Dbg( message: "_STATE_SETUP: firstTime = ", args: App.MainScene.FirstTime );

        App.LevelManager.PrepareCurrentLevel( App.MainScene.FirstTime );

        if ( App.MainScene.FirstTime )
        {
            App.BaseRenderer.EnableAllCameras();
            App.BaseRenderer.DisableLerping();

            App.GameAudio.PlayTune( true );
        }

        App.Hud.PanelManager.AddZoomPanel( GameAssets.GetreadyMsgAsset, 1500 );

        App.AppState                   = StateID._STATE_GET_READY;
        App.GameProgress.GameSetupDone = true;

        _isWaitingForPlayer = true;

        Trace.Dbg( message: "Setup done." );
    }

    /// <summary>
    /// Display and update the 'Get Ready' message.
    /// _STATE_GET_READY
    /// </summary>
    private void StateGetReady()
    {
        App.Hud.Update();

        //
        // If there is no 'Get Ready' message on screen then setup
        // flow control to play the game.
        if ( !App.Hud.PanelManager.PanelExists( GameAssets.GetreadyMsgAsset ) )
        {
            Trace.Dbg( message: "----- START GAME (GET READY) -----" );

            App.AppState       = StateID._STATE_GAME;
            App.Hud.HudStateID = StateID._STATE_PANEL_UPDATE;

            // If game has virtual/onscreen controls...
            if ( GdxSystem.Inst().AvailableInputs.Contains( ControllerType._VIRTUAL ) )
            {
                App.Hud.ShowControls( true );
            }

            App.MainScene.FirstTime = false;
        }
    }

    /// <summary>
    /// Update the game for states:-
    /// STATE_WELCOME_PANEL
    /// STATE_DEVELOPER_PANEL
    /// STATE_SETTINGS_PANEL
    /// STATE_GAME
    /// </summary>
    private void StateGame()
    {
        App.Hud.Update();

        switch ( App.AppState )
        {
            case StateID._STATE_DEVELOPER_PANEL:
            {
                if ( App.Developer.DeveloperPanelState == StateID._STATE_DISABLED )
                {
                    App.AppState       = StateID._STATE_GAME;
                    App.Hud.HudStateID = StateID._STATE_PANEL_UPDATE;
                }

                break;
            }

            case StateID._STATE_WELCOME_PANEL:
            {
                App.EntityManager.UpdateSprites();
                App.EntityManager.TidySprites();

                // Stay here until the HUD changes the AppState.
                break;
            }

            default:
            {
                // ---------------------------------------------
                if ( _isWaitingForPlayer && ( App.GetPlayer().ActionState == ActionStates._STANDING ) )
                {
                    if ( App.Settings.IsEnabled( Settings.IntroPanel ) )
                    {
                        App.Hud.IntroPanel = new IntroPanel();
                        App.Hud.IntroPanel.Create();

                        App.Hud.HudStateID = StateID._STATE_WELCOME_PANEL;
                        App.AppState       = StateID._STATE_WELCOME_PANEL;
                        App.GetPlayer().SetActionState( ActionStates._WAITING );
                    }

                    _isWaitingForPlayer = false;
                }

                // ---------------------------------------------
                else
                {
                    App.EntityManager.UpdateSprites();
                    App.EntityManager.TidySprites();

                    // Check for game ending
                    if ( !App.MainScene.EndgameManager.Update() )
                    {
                        // Tasks to perform if the game has not ended
                        if ( App.AppState == StateID._STATE_PAUSED )
                        {
                            if ( !GdxSystem.Inst().GamePaused )
                            {
                                App.AppState = StateID._STATE_GAME;
                            }
                        }
                    }
                }

                break;
            }
        }
    }

    /// <summary>
    /// Handles game actions, if any, during pause mode.
    /// _STATE_PAUSED
    /// </summary>
    private void StatePaused()
    {
        App.Hud.Update();
    }

    /// <summary>
    /// Handles the message panel which appears when the player
    /// speaks to a villager/guide
    /// _STATE_MESSAGE_PANEL
    /// </summary>
    private void StateMessagePanel()
    {
        App.Hud.Update();
        App.MapData.Update();

//        if ( !App.getConversationManager().update() )
//        {
//            App.getAppState().set( StateID._STATE_GAME );
//
//            App.getConversationManager().dispose();
//
//            App.getHud().showControls( true );
//        }
    }

    /// <summary>
    /// Handles the preparation for retrying the current
    /// level, after Player loses a life.
    /// _STATE_PREPARE_LEVEL_RETRY
    /// _STATE_LEVEL_RETRY
    /// </summary>
    private void StateSetForRetry()
    {
        App.Hud.Update();

        if ( App.AppState == StateID._STATE_PREPARE_LEVEL_RETRY )
        {
//            try
//            {
//                badLuckMessage = MathUtils.random(GameAssets.badLuckMessages.length - 1);
//            }
//            catch (ArrayIndexOutOfBoundsException boundsException)
//            {
//                badLuckMessage = 0;
//            }
//
//            App.getHud().getPanelManager().addZoomPanel(GameAssets.badLuckMessages[badLuckMessage], 2500);

            App.AppState = StateID._STATE_LEVEL_RETRY;
        }
        else
        {
//            if (!App.getHud().getPanelManager().panelExists(GameAssets.badLuckMessages[badLuckMessage]))
            {
                App.AppState = StateID._STATE_SETUP;
            }
        }
    }

    /// <summary>
    /// Handles finishing the current level and
    /// moving on to the next one.
    /// _STATE_PREPARE_LEVEL_FINISHED:
    /// _STATE_LEVEL_FINISHED:
    /// </summary>
    private void StateSetForLevelFinished()
    {
        App.LevelManager.CloseCurrentLevel();
        App.Hud.Update();

        App.MainScene.Reset();

        App.AppState       = StateID._STATE_SETUP;
        App.Hud.HudStateID = StateID._STATE_PANEL_START;
    }

    /// <summary>
    /// Initialise the 'Game Over' message.
    /// _STATE_PREPARE_GAME_OVER_MESSAGE
    /// </summary>
    private void StateSetForGameOverMessage()
    {
        App.Hud.PanelManager.AddZoomPanel( GameAssets.GameoverMsgAsset, 3000 );

        App.GameAudio.StartSound( AudioData.SfxLost );

        App.AppState = StateID._STATE_GAME_OVER;
    }

    /// <summary>
    /// Game Over, due to all levels being completed.
    ///Game Over, due to losing all lives.
    ///(Waits for the 'Game Over' message to disappear.)
    /// _STATE_GAME_OVER
    /// _STATE_GAME_FINISHED
    /// </summary>
    private void StateWaitForGameOverMessage()
    {
        App.Hud.Update();

        if ( !App.Hud.PanelManager.PanelExists( GameAssets.GameoverMsgAsset ) )
        {
            App.AppState = StateID._STATE_END_GAME;
        }
    }

    /// <summary>
    /// Game Ended, hand control back to MainMenuScreen.
    /// Control is also passed to here if forceQuitToMenu or quitToMainMenu are set.
    /// _STATE_END_GAME
    /// </summary>
    private void StateSetForEndGame()
    {
        Trace.BoxedDbg( message: "***** GAME OVER *****" );

        App.GameAudio.PlayTune( false );
        App.Scene = App.TitleScene;
    }
}