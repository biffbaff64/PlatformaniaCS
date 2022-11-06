// ##################################################

using Microsoft.Xna.Framework.Graphics;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;

using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

// ##################################################

namespace PlatformaniaCS.Game.Scenes;

public class TitleScene : AbstractBaseScene
{
    private Texture2D _background;
    private Texture2D _foreground;

    private int _currentPage;

    public TitleScene()
    {
        Trace.CheckPoint();
    }

    public override void Initialise()
    {
//            optionsPage = new OptionsPage();
//            menuPage    = new MenuPage();
//            panels      = new ArrayList<>();
//            currentPage = _MENU_PAGE;

//            panels.add( _MENU_PAGE,    menuPage );
//            panels.add( _OPTIONS_PAGE, optionsPage );
//            panels.add( _CREDITS_PAGE, new CreditsPage() );

//            menuPage.initialise();
//            menuPage.show();
    }

    public override void Update()
    {
    }

    public void Draw()
    {
        if ( App.AppState.CurrentState == StateID._STATE_MAIN_MENU )
        {
            App.SpriteBatch.Draw
                (
                 _background,
                 new Rectangle( 0, 0, Gfx.DesktopWidth, Gfx.DesktopHeight ),
                 Color.White
                );
            App.SpriteBatch.Draw
                (
                 _foreground,
                 new Rectangle( 0, 0, Gfx.DesktopWidth, Gfx.DesktopHeight ),
                 Color.White
                );
        }
    }

    public override void Render( float delta )
    {
        App.BaseRenderer.Render();
    }

    public override void Show()
    {
        Trace.CheckPoint();

        if ( GdxSystem.Inst().CurrentScreenID == ScreenID._GAME_SCREEN )
        {
            // If moving to the TitleScene from MainScene, then all objects
            // that are unnecessary at this point must be destroyed.
            App.DeleteMainsceneObjects();
        }

        GdxSystem.Inst().CurrentScreenID = ScreenID._MAIN_MENU;
        App.AppState.CurrentState        = StateID._STATE_MAIN_MENU;

        LoadImages();

        App.BaseRenderer.ResetCameraZoom();
        App.BaseRenderer.EnableCamera( CamID._PARALLAX, CamID._HUD, CamID._STAGE );

        Initialise();
    }

    public override void Hide()
    {
    }

    public override void LoadImages()
    {
        Trace.CheckPoint();

        _background = App.MainGame.Content.Load<Texture2D>( "title_background" );
        _foreground = App.MainGame.Content.Load<Texture2D>( "title_foreground" );
    }

    public override string Name()
    {
        return "Title Scene";
    }

    public override void Dispose()
    {
    }

    private void ChangePageTo( int nextPage )
    {
    }
}