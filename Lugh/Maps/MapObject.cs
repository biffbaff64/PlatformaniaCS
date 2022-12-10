// ##################################################

// ##################################################

namespace Lugh.Maps;

public class MapObject
{
    public string        Name       { get; set; } = string.Empty;
    public Color         Color      { get; set; } = Color.white;
    public float         Opacity    { get; set; } = 1.0f;
    public bool          Visible    { get; set; } = true;
    public MapProperties Properties { get; set; } = new MapProperties();
}
