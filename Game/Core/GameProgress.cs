// ##################################################

using PlatformaniaCS.Game.UI;

// ##################################################

namespace PlatformaniaCS.Game.Core
{
    public class GameProgress : IDisposable
    {
        public bool      IsRestarting   { get; set; } // TRUE If the game is restarting, i.e from losing a life
        public bool      LevelCompleted { get; set; } // ...
        public bool      GameCompleted  { get; set; } // ...
        public bool      GameOver       { get; set; } // ...
        public bool      GameSetupDone  { get; set; } // ...
        public int       GameLevel      { get; set; } // ...
        public float     GameDifficulty { get; set; }
        public bool[ , ] CollectItems   { get; set; }

        //
        // Stacks are used to allow counting up/down
        // (visually) of scores etc.
        public enum Stack
        {
            _GEM,
            _COIN,
            _KEY,
            _PRISONER,
            _LIVES,
        }

        public Item Lives       { get; set; }
        public Item GemCount    { get; set; }
        public Item CoinCount   { get; set; }
        public Item KeyCount    { get; set; }
        public Item RescueCount { get; set; }

        private int _livesStack;
        private int _gemsStack;
        private int _coinsStack;
        private int _keysStack;
        private int _prisonersStack;

        // -----------------------------------------------------------
        // Code
        // -----------------------------------------------------------

        public GameProgress()
        {
            Setup();
            ResetProgress();
        }

        public void Setup()
        {
            Lives       = new Item();
            GemCount    = new Item();
            CoinCount   = new Item();
            KeyCount    = new Item();
            RescueCount = new Item();

            CollectItems = new bool[ ItemBar.NumItemPanels, ItemBar.ItemsPerPanel ];
        }

        public void Update()
        {
            switch ( App.AppState )
            {
                case StateID._STATE_PAUSED:
                case StateID._STATE_GAME:
                case StateID._STATE_PREPARE_LEVEL_FINISHED:
                case StateID._STATE_MESSAGE_PANEL:
                {
                    if ( IsRestarting )
                    {
                        App.GetPlayer().SetActionState( ActionStates._RESETTING );
                    }

                    UpdateStacks();
                    UpdateDifficulty();
                    break;
                }

                default:
                    break;
            }
        }

        public void ResetProgress()
        {
            IsRestarting   = false;
            LevelCompleted = false;
            GameCompleted  = false;
            GameOver       = false;
            GameSetupDone  = false;

            GameLevel      = 1;
            GameDifficulty = 1.0f;

            _gemsStack      = 0;
            _coinsStack     = 0;
            _keysStack      = 0;
            _prisonersStack = 0;
            _livesStack     = 0;

            Lives.SetToMaximum();
            GemCount.SetToMinimum();
            CoinCount.SetToMinimum();
            KeyCount.SetToMinimum();
            RescueCount.SetToMinimum();

            for ( var i = 0; i < ItemBar.NumItemPanels; i++ )
            {
                for ( var j = 0; j < ItemBar.ItemsPerPanel; j++ )
                {
                    CollectItems[ i, j ] = false;
                }
            }
        }

        public void CloseLastGame()
        {
            Trace.CheckPoint();

            ResetProgress();
        }

        /// <summary>
        /// Pushes the supplied amount onto the update stack for
        /// the specified Stack ID. This value will then be added
        /// onto the relevant counter over the next few frames in
        /// the private method #updateStacks().
        /// </summary>
        public void StackPush( Stack stack, int amount )
        {
            switch ( stack )
            {
                case Stack._GEM:
                    _gemsStack += amount;
                    break;

                case Stack._COIN:
                    _coinsStack += amount;
                    break;

                case Stack._KEY:
                    _keysStack += amount;
                    break;

                case Stack._LIVES:
                    _livesStack += amount;
                    break;

                case Stack._PRISONER:
                    _prisonersStack += amount;
                    break;

                default:
                    break;
            }
        }

        private void UpdateStacks()
        {
            if ( _coinsStack > 0 )
            {
                var amount = NumberUtils.GetCount( _coinsStack );

                CoinCount.Add( amount );
                _coinsStack -= amount;
            }

            if ( _gemsStack > 0 )
            {
                var amount = NumberUtils.GetCount( _gemsStack );

                GemCount.Add( amount );
                _gemsStack -= amount;
            }

            if ( _prisonersStack > 0 )
            {
                var amount = NumberUtils.GetCount( _prisonersStack );

                RescueCount.Add( amount );
                _prisonersStack -= amount;
            }

            if ( _keysStack > 0 )
            {
                var amount = NumberUtils.GetCount( _keysStack );

                KeyCount.Add( amount );
                _keysStack -= amount;
            }

            if ( _livesStack > 0 )
            {
                var amount = NumberUtils.GetCount( _livesStack );

                Lives.Add( amount );
                _livesStack -= amount;
            }
        }

        public bool StacksAreEmpty() =>
            ( _coinsStack        == 0 )
            && ( _gemsStack      == 0 )
            && ( _prisonersStack == 0 )
            && ( _keysStack      == 0 )
            && ( _livesStack     == 0 );

        private void UpdateDifficulty()
        {
            GameDifficulty += 0.001f;
        }

        public void Dispose()
        {
            Lives       = null;
            GemCount    = null;
            CoinCount   = null;
            KeyCount    = null;
            RescueCount = null;
        }
    }
}