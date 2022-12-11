using PlatformaniaCS.Game.Core;

namespace PlatformaniaCS.Game.Graphics.Renderers
{
    public class HUDRenderer
    {
        public void Render()
        {
            if ( !LughSystem.Inst().ShutDownActive )
            {
                switch ( App.AppState )
                {
                    case StateID._STATE_MAIN_MENU:
                    {
                        App.TitleScene?.Draw();
                        break;
                    }

                    case StateID._STATE_SETUP:
                    case StateID._STATE_GET_READY:
                    case StateID._STATE_PAUSED:
                    case StateID._STATE_PREPARE_LEVEL_FINISHED:
                    case StateID._STATE_LEVEL_FINISHED:
                    case StateID._STATE_LEVEL_RETRY:
                    case StateID._STATE_GAME:
                    case StateID._STATE_MESSAGE_PANEL:
                    case StateID._STATE_WELCOME_PANEL:
                    case StateID._STATE_DEBUG_HANG:
                    case StateID._STATE_GAME_OVER:
                    {
                        var availableInputs = LughSystem.Inst().AvailableInputs;

                        App.Hud?.Render
                            (
                             App.BaseRenderer.HudGameCamera,
                             availableInputs.Contains( ControllerType._VIRTUAL )
                            );
                        break;
                    }

                    case StateID._STATE_GAME_FINISHED:
                    {
                        App.Hud?.Render( App.BaseRenderer.HudGameCamera );
                        break;
                    }

                    case StateID._STATE_CLOSING:
                    default:
                    {
                        break;
                    }
                }
            }
        }
    }
}