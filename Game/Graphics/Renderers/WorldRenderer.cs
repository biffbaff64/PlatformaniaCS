using PlatformaniaCS.Game.Core;

namespace PlatformaniaCS.Game.Graphics.Renderers;

public class WorldRenderer
{
    public void Render()
    {
        if ( !GdxSystem.Inst().ShutDownActive )
        {
            switch ( App.AppState )
            {
                case StateID._STATE_SETUP:
                case StateID._STATE_GET_READY:
                case StateID._STATE_PAUSED:
                case StateID._STATE_LEVEL_RETRY:
                case StateID._STATE_PREPARE_LEVEL_FINISHED:
                case StateID._STATE_LEVEL_FINISHED:
                case StateID._STATE_GAME:
                case StateID._STATE_SETTINGS_PANEL:
                case StateID._STATE_WELCOME_PANEL:
                case StateID._STATE_DEBUG_HANG:
                {
                    App.EntityManager.DrawSprites();
                    break;
                }

                case StateID._STATE_MAIN_MENU:
                case StateID._STATE_CLOSING:
                case StateID._STATE_GAME_OVER:
                case StateID._STATE_END_GAME:
                default:
                {
                    break;
                }
            }
        }
    }
}