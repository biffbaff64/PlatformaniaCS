// ##################################################


// ##################################################

namespace Scene2DCS;

public class Stage
{
    public static bool  DebugMode  { get; set; }
    public static Color DebugColor { get; set; }

    public Vector2 ScreenToStageCoordinates( Vector2 screenCoords )
    {
        return default;
    }

    public Vector2 StageToScreenCoordinates( Vector2 localToAscendantCoordinates )
    {
        return default;
    }

    public void CalculateScissors( Rectangle tableBounds, Rectangle scissorBounds )
    {
    }
}