// ##################################################

using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Graphics;

// ##################################################

namespace PlatformaniaCS
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new MainGame();
            game.Run();
        }
    }
}