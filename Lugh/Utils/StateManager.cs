
namespace Lugh.Utils
{
    public class StateManager
    {
        public StateID CurrentState { get; set; }

        public StateManager()
        {
            CurrentState = StateID._INACTIVE;
        }

        public StateManager( StateID state )
        {
            CurrentState = state;
        }

        public bool After( StateID state )
        {
            return ( CurrentState > state );
        }
    }
}