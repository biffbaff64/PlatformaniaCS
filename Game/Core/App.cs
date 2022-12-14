// ##################################################

using PlatformaniaCS.Game.Audio;
using PlatformaniaCS.Game.Config;
using PlatformaniaCS.Game.Entities;
using PlatformaniaCS.Game.Entities.Actors.Hero;
using PlatformaniaCS.Game.Entities.Paths;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.Graphics.Renderers;
using PlatformaniaCS.Game.Input;
using PlatformaniaCS.Game.Maps;
using PlatformaniaCS.Game.Scenes;
using PlatformaniaCS.Game.UI;
using PlatformaniaCS.Game.Utils;

// ##################################################

namespace PlatformaniaCS.Game.Core
{
    public abstract class App
    {
        // -------------------------------------------------
        // Objects created on power-up (Essential Objects)
        public static AppConfig      AppConfig      { get; set; }
        public static StateID        AppState       { get; set; }
        public static GameAudio      GameAudio      { get; set; }
        public static SpriteBatch    SpriteBatch    { get; set; }
        public static Settings       Settings       { get; set; }
        public static BaseRenderer   BaseRenderer   { get; set; }
        public static MainGame       MainGame       { get; set; }
        public static WorldModel     WorldModel     { get; set; }
        public static TitleScene     TitleScene     { get; set; }
        public static HighScoreUtils HighScoreUtils { get; set; }

        // -------------------------------------------------
        // Objects created in TitleScene
        public static MainScene MainScene { get; set; }

        // -------------------------------------------------
        // Objects created in MainScene.
        // These will be destroyed in TitleScene if a transition
        // from MainScene to TitleScene is detected.
        public static GameProgress   GameProgress   { get; set; }
        public static EntityManager  EntityManager  { get; set; }
        public static EntityUtils    EntityUtils    { get; set; }
        public static EntityData     EntityData     { get; set; }
        public static AnimationUtils AnimationUtils { get; set; }
        public static HeadsUpDisplay Hud            { get; set; }
        public static LevelManager   LevelManager   { get; set; }
        public static TMXMapParser   TmxMapParser   { get; set; }
        public static MapData        MapData        { get; set; }
        public static MapUtils       MapUtils       { get; set; }
        public static RoomManager    RoomManager    { get; set; }
        public static PathUtils      PathUtils      { get; set; }
        public static AssetUtils     Assets         { get; set; }
        public static Developer      Developer      { get; set; }
        public static InputManager   InputManager   { get; set; }

        // ------------------------------------------------
        // General globals
        // TODO:

        // ------------------------------------------------
        // CODE
        // ------------------------------------------------

        public static MainPlayer GetPlayer() => EntityData.MainPlayer;
        public static int        GetLevel()  => GameProgress.GameLevel;

        /// <summary>
        /// Get/Set the current scene.
        /// If setting a scene, the current scene (if any) is hidden
        /// to be replaced by the new one.
        /// </summary>
        public static IScene Scene
        {
            get => LughSystem.Inst().CurrentScene;
            set
            {
                if ( LughSystem.Inst().CurrentScene != null )
                {
                    LughSystem.Inst().CurrentScene.Hide();
                }

                LughSystem.Inst().CurrentScene = value;

                if ( LughSystem.Inst().CurrentScene != null )
                {
                    LughSystem.Inst().CurrentScene.Show();
                }
            }
        }

        /// <summary>
        /// Creates all essential global objects that are needed
        /// before any scenes are opened.
        /// </summary>
        public static void CreateEssentialObjects()
        {
            Trace.CheckPoint();

            // GraphicsDeviceManager is created in MainGame().
            // SpriteBatch is created in MainGame().
            // AppConfig is created in MainGame().
            // AppSettings is created in MainGame().
            // Settings is created in MainGame().

            // -------------------------------------------------
            AppState    = StateID._INACTIVE;
            SpriteBatch = new SpriteBatch();
            EntityData  = new EntityData();

            // -------------------------------------------------
            Assets       = new AssetUtils();
            GameAudio    = new GameAudio();
            InputManager = new InputManager();
            Developer    = new Developer();

            // -------------------------------------------------
            BaseRenderer   = new BaseRenderer();
            WorldModel     = new WorldModel();
            GameProgress   = new GameProgress();
            HighScoreUtils = new HighScoreUtils();
        }

        /// <summary>
        /// Creates all global objects necessary for the MainScene.
        /// </summary>
        public static void CreateMainsceneObjects()
        {
            Trace.CheckPoint();

            // -------------------------------------------------
            LevelManager   = new LevelManager();
            MapData        = new MapData();
            Hud            = new HeadsUpDisplay();
            EntityManager  = new EntityManager();
            EntityUtils    = new EntityUtils();
            AnimationUtils = new AnimationUtils();
            TmxMapParser   = new TMXMapParser();
            MapUtils       = new MapUtils();
            RoomManager    = new RoomManager();
        }

        public static void DeleteEssentialObjects()
        {
            Trace.CheckPoint();

            SpriteBatch.Dispose();
            Assets.Dispose();
            Settings.Dispose();
            MapData.Dispose();

            MapData = null;

            // -------------------------------------------------
            if ( BaseRenderer != null )
            {
                BaseRenderer.Dispose();
                BaseRenderer = null;
            }

            if ( WorldModel != null )
            {
                WorldModel.Dispose();
                WorldModel = null;
            }

            if ( GameProgress != null )
            {
                GameProgress.Dispose();
                GameProgress = null;
            }

            // -----------------------

            AudioData.Tidy();
        }

        public static void DeleteMainsceneObjects()
        {
            Trace.CheckPoint();

            // -----------------------
            EntityData.Dispose();
            Hud.Dispose();
            EntityManager.Dispose();
            TmxMapParser.Dispose();
            MapUtils.Dispose();
            PathUtils.Dispose();

            // -----------------------

            EntityData     = null;
            LevelManager   = null;
            Hud            = null;
            EntityManager  = null;
            EntityUtils    = null;
            AnimationUtils = null;
            TmxMapParser   = null;
            MapUtils       = null;
            RoomManager    = null;

            // -----------------------
        }
    }
}