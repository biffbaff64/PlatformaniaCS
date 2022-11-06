
namespace PlatformaniaCS.Game.Maps;

public class Doorway
{
    public Rectangle Box     { get; set; }
    public Dir       NextDir { get; set; }

    public Doorway( Rectangle box, Dir nextDir )
    {
        Box     = box;
        NextDir = nextDir;
    }
}