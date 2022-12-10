using System.Drawing;

namespace Scene2DCS.Utils;

public interface ICullable
{
    /// <summary>
    /// Allows a parent to set the area that is visible on a child actor
    /// to allow the child to cull when drawing itself. This must only be
    /// used for actors that are not rotated or scaled.
    /// </summary>
    void SetCullingArea( Rectangle cullingArea );
}