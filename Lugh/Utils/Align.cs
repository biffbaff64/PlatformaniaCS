namespace Lugh.Utils;

public class Align
{
    public static readonly int center = 1 << 0;
    public static readonly int top    = 1 << 1;
    public static readonly int bottom = 1 << 2;
    public static readonly int left   = 1 << 3;
    public static readonly int right  = 1 << 4;

    public static readonly int topLeft     = top | left;
    public static readonly int topRight    = top | right;
    public static readonly int bottomLeft  = bottom | left;
    public static readonly int bottomRight = bottom | right;

}
