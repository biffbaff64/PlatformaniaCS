using System.Drawing;

namespace Scene2DCS.Utils;

public class ScissorStack
{
    public static bool PushScissors( Rectangle scissorBounds )
    {
        return false;
    }

    public static Rectangle PopScissors()
    {
        return default;
    }

    public static void CalculateScissors( Camera camera, int screenX, int screenY, int screenWidth, int screenHeight, Matrix4 batchTransform, Rectangle area, Rectangle scissor )
    {
    }
}
