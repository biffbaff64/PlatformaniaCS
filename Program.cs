// ##################################################
#region using

using PlatformaniaCS.Game.Core;

#endregion using
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