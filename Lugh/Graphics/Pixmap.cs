// ############################################################

// ############################################################

namespace Lugh.Graphics;

public class Pixmap : IDisposable
{
    public enum Format
    {
        None,
        Alpha,
        Intensity,
        LuminanceAlpha,
        RGB565,
        RGBA4444,
        RGB888,
        RBGA8888
    }
    
    public void Dispose()
    {
    }
}

