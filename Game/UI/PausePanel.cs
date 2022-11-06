
using Lugh.UI;

namespace PlatformaniaCS.Game.UI
{
    public class PausePanel : AbstractBasePanel
    {
        public const int Music         = 0;
        public const int Sounds        = 1;
        public const int NumCheckBoxes = 2;
    
        public PausePanel( int x, int y ) : base( x, y )
        {
            NameID = "Pause Panel";
        }

        public override void Initialise( TextureRegion region, string nameID, params object[] args )
        {
        }

        public override void Setup()
        {
        }
    }
}