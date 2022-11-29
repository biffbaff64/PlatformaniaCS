// ##################################################

using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;

// ##################################################

namespace PlatformaniaCS.Game.Config;

public class Developer
{
    public StateID DeveloperPanelState { get; private set; }
    public bool    IsDevMode           { get; private set; }
    public bool    IsGodMode           { get; private set; }

    public void SetTempDeveloperSettings()
    {
        if ( IsDevMode )
        {
            Trace.Divider();
            Trace.Dbg( message: "Temporary Development Settings" );

            IsGodMode = false;
//            IsGodMode = true;
            
            string[] disableList =
            {
                Preferences.ShowFPS,
                Preferences.CullSprites,
                Preferences.ButtonBoxes,
                Preferences.DisablePlayer,
                Preferences.ShaderProgram,
                Preferences.TileBoxes,
                Preferences.ScrollDemo,
                Preferences.SpriteBoxes,
                Preferences.LevelSelect,
                Preferences.MenuScene,
            };

            string[] enableList =
            {
                Preferences.ShowDebug,
                Preferences.Box2DPhysics,
                Preferences.Vibrations,
                Preferences.Installed,
            };

            foreach ( var str in disableList )
            {
                App.Preferences.Disable( str );
            }

            foreach ( var str in enableList )
            {
                App.Preferences.Enable( str );
            }
        }
    }

    /// <summary>
    /// Reads the environment variable 'DEV_MODE', and sets 'IsDevMode' accordingly.
    /// </summary>
    public void SetDeveloperModeState()
    {
        try
        {
            IsDevMode = "TRUE".Equals(System.Environment.GetEnvironmentVariable( "_DEV_MODE" ));
        }
        catch ( NullReferenceException e )
        {
            Trace.Err( message: e.ToString() );
            
            IsDevMode = false;
            IsGodMode = false;
        }

        Trace.Dbg( message: "Developer Mode is ", args: IsDevMode ? "ENABLED." : "DISABLED." );
    }

    /// <summary>
    /// Enables or disables the Developer Settings Panel.
    /// The only valid states are:-
    /// StateID.STATE_DISABLED
    /// StateID.STATE_ENABLED
    /// All other states will default to STATE_DISABLED.
    /// </summary>
    /// <param name="state">The Panel State.</param>
    public void SetDeveloperPanelState( StateID state )
    {
        switch ( state )
        {
            case StateID._STATE_DISABLED:
            {
                DeveloperPanelState = state;
                break;
            }

            default:
            {
                DeveloperPanelState = StateID._STATE_DISABLED;
                break;
            }
        }
    }

    public void ConfigReport()
    {
        if ( IsDevMode )
        {
            Trace.Divider();
            Trace.Dbg( message: "isDevMode()         : ", args: IsDevMode );
            Trace.Dbg( message: "isGodMode()         : ", args: IsGodMode );
            Trace.Divider();
            Trace.Dbg( message: "DESKTOP_WIDTH       : ", args: Gfx.DesktopWidth );
            Trace.Dbg( message: "DESKTOP_HEIGHT      : ", args: Gfx.DesktopHeight );
            Trace.Dbg( message: "VIEW_WIDTH          : ", args: Gfx.ViewWidth );
            Trace.Dbg( message: "VIEW_HEIGHT         : ", args: Gfx.ViewHeight );
            Trace.Dbg( message: "HUD_WIDTH           : ", args: Gfx.HudWidth );
            Trace.Dbg( message: "HUD_HEIGHT          : ", args: Gfx.HudHeight );
            Trace.Dbg( message: "GAME_SCENE_WIDTH    : ", args: Gfx.GameSceneWidth );
            Trace.Dbg( message: "GAME_SCENE_HEIGHT   : ", args: Gfx.GameSceneHeight );
            Trace.Dbg( message: "HUD_SCENE_WIDTH     : ", args: Gfx.HudSceneWidth );
            Trace.Dbg( message: "HUD_SCENE_HEIGHT    : ", args: Gfx.HudSceneHeight );
            Trace.Divider();
            Trace.Dbg( message: "_PPM                : " + Gfx.PPM );
            Trace.Divider();

            Trace.Dbg
                (
                 message: "_VIRTUAL?           : ",
                 args: LughSystem.Inst().AvailableInputs.Contains( ControllerType._VIRTUAL )
                );

            Trace.Dbg
                (
                 message: "_EXTERNAL?          : ",
                 args: LughSystem.Inst().AvailableInputs.Contains( ControllerType._EXTERNAL )
                );

            Trace.Dbg
                (
                 message: "_KEYBOARD?          : ",
                 args: LughSystem.Inst().AvailableInputs.Contains( ControllerType._KEYBOARD )
                );

            Trace.Dbg( message: "controllerPos       : ", args: LughSystem.Inst().VirtualControllerPos );
            Trace.Dbg( message: "controllersFitted   : ", args: LughSystem.Inst().ControllersFitted );
            Trace.Dbg( message: "usedController      : ", args: LughSystem.Inst().CurrentController );
            Trace.Divider();
        }
    }
}