// ##################################################

using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.Graphics.Camera;
using PlatformaniaCS.Game.UI.Panels;

// ##################################################

namespace PlatformaniaCS.Game.UI
{
    public class HeadsUpDisplay : IDisposable
    {
        private const int VeryLargeFontSize = 48;
        private const int LargeFontSize     = 35;
        private const int MidFontSize       = 25;
        private const int SmallFontSize     = 18;

        private const int Joystick   = 0;
        private const int ButtonX    = 1;
        private const int ButtonY    = 2;
        private const int ButtonB    = 3;
        private const int ButtonA    = 4;
        private const int Pause      = 5;
        private const int Keys       = 6;
        private const int Lives      = 7;
        private const int Health     = 8;
        private const int Villagers  = 9;
        private const int Level      = 10;
        private const int Coins      = 11;
        private const int Gems       = 12;
        private const int DevOptions = 13;
        private const int DevUp      = 14;
        private const int DevDown    = 15;
        private const int DevLeft    = 16;
        private const int DevRight   = 17;
        private const int DevA       = 18;
        private const int DevB       = 19;
        private const int DevX       = 20;
        private const int DevY       = 21;

        private const int X      = 0;
        private const int Y      = 1;
        private const int Width  = 2;
        private const int Height = 3;

    //@formatter:off
    private readonly int[ , ] _displayPos =
    {
        {   25,   25,  240,  240 },     // Joystick
        {  959,   97,   96,   96 },     // X
        { 1060,  186,   96,   96 },     // Y
        { 1157,   97,   96,   96 },     // B (Attack)
        { 1060,   14,   96,   96 },     // A (Action)
        { 1179,  630,   66,   66 },     // Pause Button

        // ----------------------------------------
        // Y is distance from the TOP of the screen
        {  180,   70,    0,    0 },     // Keys total
        {  932,  103,   40,    0 },     // Lives
        {  933,   53,    0,    0 },     // Health bar
        {   64,   52,    0,    0 },     // Villagers
        { 1200,   64,    0,    0 },     // Level
        {  308,   30,    0,    0 },     // Coins
        {  308,   70,    0,    0 },     // Gems

        // ----------------------------------------
        // ????
        {  911,  717,    0,    0 },     // Developer Options
        { 1008,  621,    0,    0 },     // 'UP'
        { 1008,  644,    0,    0 },     // 'DOWN'
        { 1008,  667,    0,    0 },     // 'LEFT'
        { 1008,  690,    0,    0 },     // 'RIGHT'
        { 1170,  681,    0,    0 },     // 'A'
        { 1170,  635,    0,    0 },     // 'B'
        { 1100,  681,    0,    0 },     // 'X'
        { 1100,  635,    0,    0 },     // 'Y'
    };
//@formatter:on

        public float      HudOriginX  { get; set; }
        public float      HudOriginY  { get; set; }
        public StateID    HudStateID  { get; set; }
        public Switch     ButtonUp    { get; set; }
        public Switch     ButtonDown  { get; set; }
        public Switch     ButtonLeft  { get; set; }
        public Switch     ButtonRight { get; set; }
        public Switch     ButtonPause { get; set; }
        public Switch     AButton     { get; set; }
        public Switch     BButton     { get; set; }
        public Switch     XButton     { get; set; }
        public Switch     YButton     { get; set; }
        public IntroPanel IntroPanel  { get; set; }
        public HudDebug   HudDebug    { get; set; }

        public Texture2D    ScorePanel     { get; set; }
        public Texture2D[]  SmallMan       { get; set; }
        public BitmapFont   SmallFont      { get; set; }
        public BitmapFont   MidFont        { get; set; }
        public BitmapFont   BigFont        { get; set; }
        public PanelManager PanelManager   { get; set; }
        public PausePanel   PausePanel     { get; set; }
        public ItemBar      ItemBar        { get; set; }
        public int          ItemPanelIndex { get; set; }
        public ProgressBar  HealthBar      { get; set; }
        public Message      MessageHandler { get; set; }
        public YesNoDialog  YesNoDialog    { get; set; }
        public TextPanel    TextPanel      { get; set; }

        public HeadsUpDisplay()
        {
        }

        public void CreateHud()
        {
            Trace.CheckPoint();

            CreateHudFonts();
            CreateSmallMen();

            ScorePanel = AssetUtils.LoadAsset<Texture2D>( GameAssets.HUDPanelAsset );

            MessageHandler = new Message();
            PanelManager   = new PanelManager();
            PausePanel     = new PausePanel( 0, 0 );
            ItemBar        = new ItemBar();
            ItemPanelIndex = 0;

            TextPanel = new TextPanel();
            HudDebug  = new HudDebug();

            CreateHealthBar();
            CreateHUDButtons();

            HudStateID = StateID._STATE_PANEL_START;
        }

        public void Update()
        {
        }

        private void UpdateLives()
        {
        }

        private void UpdateBars()
        {
        }

        private void CheckButtons()
        {
        }

        public void Render( OrthoGameCamera camera, bool canDrawControls = false )
        {
        }

        private void DrawPanels()
        {
        }

        private void DrawItems()
        {
        }

        private void DrawMessages()
        {
        }

        public void ShowControls( bool visible )
        {
        }

        public void EnableHUDButtons()
        {
        }

        public void ReleaseDirectionButtons()
        {
        }

        public void ReleaseABXYButtons()
        {
        }

        public void RefillItems()
        {
        }

        private void RemovePausePanel()
        {
            if ( PausePanel != null )
            {
                PausePanel.Dispose();
                PausePanel = null;
            }

            App.AppConfig.UnPause();

            ButtonPause.IsPressed = false;
        }

        /// <summary>
        /// Create the 'small man' images used for displaying
        /// the number of lives remaining.
        /// </summary>
        private void CreateSmallMen()
        {
            SmallMan = new Texture2D[ GameConstants.MaxLives ];

            for ( var i = 0; i < GameConstants.MaxLives; i++ )
            {
                SmallMan[ i ] = AssetUtils.LoadAsset<Texture2D>( GameAssets.SmallMan );
            }
        }

        private void CreateHudFonts()
        {
        }

        private void CreateHealthBar()
        {
            HealthBar = new ProgressBar
                (
                 1,
                 0,
                 GameConstants.MaxProgressbarLength,
                 "bar9",
                 false
                );

            HealthBar.SetPosition
                (
                 ( int )HudOriginX + _displayPos[ Health, X ],
                 ( int )HudOriginY + ( Gfx.HudHeight - _displayPos[ Health, Y ] )
                );

            HealthBar.SetHeightColorScale( 19f, Color.Green, 2.0f );
        }

        /// <summary>
        /// Creates any buttons used for the HUD.
        /// HUD buttons are just switches so that they can be set/unset
        /// by keyboard OR on-screen virtual buttons.
        /// </summary>
        private void CreateHUDButtons()
        {
            AButton     = new Switch();
            BButton     = new Switch();
            XButton     = new Switch();
            YButton     = new Switch();
            ButtonLeft  = new Switch();
            ButtonRight = new Switch();
            ButtonUp    = new Switch();
            ButtonDown  = new Switch();
            ButtonPause = new Switch();
        }

        private void DrawHudDebug()
        {
        }

        public void Dispose()
        {
        }
    }
}