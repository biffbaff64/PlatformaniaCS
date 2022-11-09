using Scene2DCS.Utils;

using Microsoft.Xna.Framework.Graphics;

namespace Scene2DCS;

public class Widget : Actor, ILayout
{
    public bool NeedsLayout { get; set; }

    public void Layout()
    {
    }

    public void Pack()
    {
        SetSize( GetPrefWidth(), GetPrefHeight() );
        Validate();
    }

    public void SetFillParent( bool fillParent )
    {
    }

    public void SetLayoutEnabled( bool enabled )
    {
    }

    public new void SizeChanged()
    {
    }

    /// <summary>
    /// If this method is overridden, the super method or validate() should
    /// be called to ensure the widget is laid out.
    /// </summary>
    public new void Draw( SpriteBatch batch, float parentAlpha )
    {
        Validate();
    }

    public float GetMinWidth()
    {
        return 0;
    }

    public float GetMinHeight()
    {
        return 0;
    }

    public float GetPrefWidth()
    {
        return 0;
    }

    public float GetPrefHeight()
    {
        return 0;
    }

    public float GetMaxWidth()
    {
        return 0;
    }

    public float GetMaxHeight()
    {
        return 0;
    }

    public void Validate()
    {
    }

    public void Invalidate()
    {
    }

    public void InvalidateHierarchy()
    {
    }
}