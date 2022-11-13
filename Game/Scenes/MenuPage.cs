using Microsoft.Xna.Framework.Graphics;
using PlatformaniaCS.Game.UI;

namespace PlatformaniaCS.Game.Scenes;

public class MenuPage : IUIPage
{
    public ImageButton ButtonStart   { get; set; }
    public ImageButton ButtonOptions { get; set; }
    public ImageButton ButtonCredits { get; set; }
    public ImageButton ButtonExit    { get; set; }

    private const int Start            = 0;
    private const int Options          = 1;
    private const int Credits          = 2;
    private const int Exit             = 3;
    private const int Poppy            = 4;
    private const int Xmas_Tree        = 5;
    private const int Indicator_Frames = 6;

        //@formatter:off
        private readonly int[,] _displayPos =
        {
            { 470, 435, 339,  35 },       //
            { 526, 495, 228,  35 },       //
            { 526, 556, 228,  35 },       //
            { 578, 615, 123,  35 },       //
            { 512, 120,   0,   0 },       //
            {   0,   0,   0,   0 },       //
        };
    //@formatter:on

    private Texture2D       _decoration;
    private Texture2D       _leftIndicator;
    private Texture2D       _rightIndicator;
    private TextureRegion[] _indicatorFrames;
    private Animation       _indicatorAnim;
    private float           _elapsedAnimTime;
    private bool            _indicatorDrawable;
    private int             _indicatorIndex;
    private Rectangle[]     _buttonBoxes;

    public MenuPage()
    {
    }

    public void Initialise()
    {
        Trace.CheckPoint();

        CreateButtons();
        CreateIndicator();

        AddDateSpecificItems( false );
    }

    public bool Update() => false;

    public void Show()
    {
        Trace.CheckPoint();

        ShowItems( true );
    }

    public void Hide()
    {
        Trace.CheckPoint();

        ShowItems( false );
    }

    public void Draw()
    {
        if ( _indicatorDrawable )
        {
        }
    }

    public void Dispose()
    {
        Trace.CheckPoint();
    }

    private void ShowItems( bool visible )
    {
        if ( ButtonStart != null )
        {
            ButtonStart.IsVisible = visible;
        }

        if ( ButtonOptions != null )
        {
            ButtonOptions.IsVisible = visible;
        }

        if ( ButtonCredits != null )
        {
            ButtonCredits.IsVisible = visible;
        }

        if ( ButtonExit != null )
        {
            ButtonExit.IsVisible = visible;
        }

//        if ( _decoration != null )
//        {
//            _decoration.IsVisible = visible;
//        }

        _indicatorDrawable = visible;
    }

    private void CreateButtons()
    {
    }

    private void CreateIndicator()
    {
    }

    private void AddDateSpecificItems( bool forceShow )
    {
    }
}