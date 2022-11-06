
namespace PlatformaniaCS.Game.Core
{
    public class LevelManager
    {
        private bool _isFirstTime;

        public LevelManager()
        {
            _isFirstTime = true;
        }

        /// <summary>
        /// Prepare the current level by setting up maps, entities
        /// and any relevant flags/variables.
        /// </summary>
        public void PrepareCurrentLevel( bool firstTime )
        {
            Trace.CheckPoint();
            
            if ( App.GameProgress.IsRestarting )
            {
                RestartCurrentLevel();
            }
            else if ( firstTime || App.GameProgress.LevelCompleted )
            {
                SetupForNewLevel();
            }

            GdxSystem.Inst().GamePaused      = false;
            GdxSystem.Inst().QuitToMainMenu  = false;
            GdxSystem.Inst().ForceQuitToMenu = false;

            App.GameProgress.IsRestarting   = false;
            App.GameProgress.LevelCompleted = false;
            App.GameProgress.GameOver       = false;

            if ( firstTime )
            {
                App.Hud.RefillItems();
                App.Hud.Update();
            }
        }

        /// <summary>
        /// Sets up the map and entities for a new level.
        /// </summary>
        private void SetupForNewLevel()
        {
            Trace.CheckPoint();

            App.RoomManager.SetRoom( App.GetLevel() );
            
            App.TmxMapParser.InitialiseLevelMap();
            App.TmxMapParser.CreatePositioningData();
            App.EntityManager.InitialiseForLevel();

            //
            // Create entity paths if any relevant data
            // exists in the tilemap data.
            App.PathUtils.Setup();
        }

        /// <summary>
        /// Reset all entity positions, and re-initialise
        /// the main player, ready to replay the current level.
        /// </summary>
        private void RestartCurrentLevel()
        {
            App.EntityUtils.KillAllExcept( GraphicID.G_PLAYER );
            App.EntityData.EntityMap.RemoveRange( 1, App.EntityData.EntityMap.Count - 1 );
            App.EntityManager.InitialiseForLevel();
        }

        /// <summary>
        /// Actions to perform when a level has been completed.
        /// Remove all entities/pickups/etc from the level, but
        /// make sure that the main player is untouched.
        /// </summary>
        public void CloseCurrentLevel()
        {
            Trace.CheckPoint();

            App.EntityUtils.KillAllExcept( GraphicID.G_PLAYER );
            App.EntityData.EntityMap.RemoveRange( 1, App.EntityData.EntityMap.Count - 1 );
            App.GetPlayer().KillBody();
            App.MapUtils.DestroyBodies();
            App.TmxMapParser.PlacementTiles.Clear();
        }

        public void GameLevelUp()
        {
            App.GameProgress.GameLevel = Math.Min( App.GetLevel() + 1, GameConstants.MaxLevel );
        }

        public void GameLevelDown()
        {
            App.GameProgress.GameLevel = Math.Max( App.GetLevel() - 1, GameConstants.MinLevel );
        }

        /// <summary>
        /// Set up everything necessary for a new game,
        /// called from MainScene#initialise.
        /// </summary>
        public void PrepareNewGame()
        {
            Trace.CheckPoint();

            if ( _isFirstTime )
            {
                Trace.CheckPoint();
                
                //
                // Make sure all progress counters are initialised.
                App.GameProgress.ResetProgress();

                //
                // Initialise the room that the game will start in.
                App.RoomManager.Initialise();

                //
                // Only the HUD camera is enabled initially.
                App.BaseRenderer.EnableCamera( CamID._HUD );

                App.EntityManager.Initialise();
                App.MapData.Update();

                // Score, Lives display etc.
                App.Hud.CreateHud();
            }

            _isFirstTime = false;
        }
    }
}