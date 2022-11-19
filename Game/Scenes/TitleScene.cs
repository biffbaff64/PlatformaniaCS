// ##################################################

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.UI;
using Color = Microsoft.Xna.Framework.Color;
using OrthographicCamera = MonoGame.Extended.OrthographicCamera;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

// ##################################################

namespace PlatformaniaCS.Game.Scenes;

public class TitleScene : BaseScene
{
    private const int MenuPage    = 0;
    private const int OptionsPage = 1;
    private const int CreditsPage = 2;
    private const int ExitPage    = 3;

    private Texture2D                  _background;
    private Texture2D                  _foreground;
    private MenuPage                   _menuPage;
    private OptionsPage                _optionsPage;
    private CreditsPage                _creditsPage;
    private Dictionary< int, IUIPage > _pages;
    private YesNoDialog                _exitDialog;
    private int                        _currentPage;

    public TitleScene()
    {
        Trace.CheckPoint();
    }

    /// <summary>
    /// Initialise the Title scene, create the array of pages,
    /// and set the initial page to the options menu.
    /// </summary>
    public override void Initialise()
    {
        Trace.CheckPoint();

        _menuPage    = new MenuPage();
        _optionsPage = new OptionsPage();
        _creditsPage = new CreditsPage();
        _pages       = new Dictionary< int, IUIPage >();

        _currentPage = MenuPage;

        _pages.Add( MenuPage,    _menuPage );
        _pages.Add( OptionsPage, _optionsPage );
        _pages.Add( CreditsPage, _creditsPage );

        _menuPage.Initialise();
        _menuPage.Show();
    }

    public override void Update( GameTime gameTime )
    {
    }

    private void ChangePageTo( int nextPage )
    {
        Trace.CheckPoint();
        Trace.Info( "currentPage: ", _currentPage );
        Trace.Info( "nextPage: ",    nextPage );

        if ( _currentPage == ExitPage )
        {
            _exitDialog?.Dispose();
            _exitDialog = null;
        }
        else
        {
            if ( _pages[ _currentPage ] != null )
            {
                _pages[ _currentPage ].Hide();
                _pages[ _currentPage ].Dispose();
            }
        }

        _currentPage = nextPage;

        if ( _currentPage != ExitPage )
        {
            if ( _pages[ _currentPage ] != null )
            {
                _pages[ _currentPage ].Initialise();
                _pages[ _currentPage ].Show();
            }
        }
    }

    public override void Render( GameTime gameTime )
    {
        if ( App.AppState == StateID._STATE_MAIN_MENU )
        {
            Update( gameTime );

            App.BaseRenderer.Render( gameTime.GetElapsedSeconds() );
        }
    }

    public void Draw( /*OrthographicCamera camera*/ )
    {
        if ( App.AppState == StateID._STATE_MAIN_MENU )
        {
            App.SpriteBatch.Draw
            (
                _background,
                new Rectangle
                (
                    0,
                    0,
                    Gfx.HudWidth,
                    Gfx.HudHeight
                ),
                new Rectangle
                (
                    0, 0,
                    _background.Width,
                    _background.Height
                ),
                Color.White
            );

            switch ( _currentPage )
            {
                case MenuPage:
                case OptionsPage:
                case CreditsPage:
                {
                    DrawForeground();

                    _pages[ _currentPage ].Draw();

                    if ( _currentPage != MenuPage )
                    {
                        if ( LughSystem.Inst().BackButton is { IsVisible: true } )
                        {
                            LughSystem.Inst().BackButton.Position.Set( 20, 20 );
                        }
                    }

                    break;
                }

                case ExitPage:
                {
                    DrawForeground();
                    break;
                }

                default:
                {
                    Trace.Err( message: "Unknown Panel", args: _currentPage );
                    break;
                }
            }
        }
    }

    public override void Show()
    {
        Trace.CheckPoint();

        if ( LughSystem.Inst().CurrentScreenID == ScreenID._GAME_SCREEN )
        {
            // If moving to the TitleScene from MainScene, then all objects
            // that are unnecessary at this point must be destroyed.
            App.DeleteMainsceneObjects();
        }

        LughSystem.Inst().CurrentScreenID = ScreenID._MAIN_MENU;
        App.AppState                      = StateID._STATE_MAIN_MENU;

        LoadImages();

        App.BaseRenderer.ResetCameraZoom();
        App.BaseRenderer.EnableCamera( CamID._HUD, CamID._STAGE );

        Initialise();
    }

    public override void Hide()
    {
        Trace.CheckPoint();

        Dispose();
    }

    private void LoadImages()
    {
        Trace.CheckPoint();

        _background = AssetUtils.LoadAsset< Texture2D >( "title_background" );
        _foreground = AssetUtils.LoadAsset< Texture2D >( "title_foreground" );
    }

    public void Dispose()
    {
        Trace.CheckPoint();

        _menuPage.Hide();
        _menuPage.Dispose();

        if ( _pages != null )
        {
            _pages.Clear();
            _pages = null;
        }

        _optionsPage = null;
        _menuPage    = null;
        _exitDialog  = null;

        AssetUtils.UnloadAsset( "title_background" );
        AssetUtils.UnloadAsset( "title_foreground" );

        _foreground = null;
        _background = null;
    }

    public override string Name() => "Title Scene";

    private void DrawForeground()
    {
        App.SpriteBatch.Draw
        (
            _foreground,
            new Rectangle( 0, 0, Gfx.HudWidth, Gfx.HudHeight ),
            Color.White
        );
    }
}