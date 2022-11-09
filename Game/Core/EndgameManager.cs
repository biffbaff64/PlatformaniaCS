// ##################################################

using PlatformaniaCS.Game.UI;

// ##################################################

namespace PlatformaniaCS.Game.Core;

public class EndgameManager
{
    public EndgameManager()
    {
    }

    public bool Update()
    {
        bool returnFlag = false;

        if ( ( ( App.GetPlayer() != null ) && ( App.GetPlayer().ActionState == ActionStates._DEAD ) )
             || GdxSystem.Inst().ForceQuitToMenu )
        {
            App.AppState = StateID._STATE_PREPARE_GAME_OVER_MESSAGE;

            GdxSystem.Inst().QuitToMainMenu = true;

            returnFlag = true;
        }
        else
        {
            if ( App.GameProgress.GameCompleted )
            {
                Trace.BoxedDbg( message: "GAME COMPLETED" );

                App.MainScene.GameCompletedPanel = new GameCompletedPanel();
                App.MainScene.GameCompletedPanel.Setup();

                App.Hud.HudStateID = StateID._STATE_GAME_FINISHED;
                App.AppState       = StateID._STATE_GAME_FINISHED;

                returnFlag = true;
            }
            else
            {
                if ( App.GameProgress.LevelCompleted )
                {
                    Trace.BoxedDbg( message: "LEVEL COMPLETED" );

                    App.Hud.HudStateID = StateID._STATE_PANEL_UPDATE;
                    App.AppState       = StateID._STATE_PREPARE_LEVEL_FINISHED;

                    returnFlag = true;
                }
                else
                {
                    if ( App.GameProgress.IsRestarting )
                    {
                        if ( App.GetPlayer().ActionState == ActionStates._RESETTING )
                        {
                            Trace.BoxedDbg( message: "LIFE LOST - TRY AGAIN" );

                            App.AppState = StateID._STATE_PREPARE_LEVEL_RETRY;
                        }

                        returnFlag = true;
                    }
                }
            }
        }

        return returnFlag;
    }
}