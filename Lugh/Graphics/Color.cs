// ##################################################

// ##################################################

namespace Lugh.Graphics;

public class Color
{
    public static readonly Color white      = new Color( 1, 1, 1, 1 );
    public static readonly Color lightGray  = new Color( 0xbfbfbfff );
    public static readonly Color gray       = new Color( 0x7f7f7fff );
    public static readonly Color darkGray   = new Color( 0x3f3f3fff );
    public static readonly Color black      = new Color( 0, 0, 0,    1 );
    public static readonly Color clear      = new Color( 0, 0, 0,    0 );
    public static readonly Color blue       = new Color( 0, 0, 1,    1 );
    public static readonly Color navy       = new Color( 0, 0, 0.5f, 1 );
    public static readonly Color royal      = new Color( 0x4169e1ff );
    public static readonly Color slate      = new Color( 0x708090ff );
    public static readonly Color sky        = new Color( 0x87ceebff );
    public static readonly Color cyan       = new Color( 0, 1,    1,    1 );
    public static readonly Color teal       = new Color( 0, 0.5f, 0.5f, 1 );
    public static readonly Color green      = new Color( 0x00ff00ff );
    public static readonly Color chartreuse = new Color( 0x7fff00ff );
    public static readonly Color lime       = new Color( 0x32cd32ff );
    public static readonly Color forest     = new Color( 0x228b22ff );
    public static readonly Color olive      = new Color( 0x6b8e23ff );
    public static readonly Color yellow     = new Color( 0xffff00ff );
    public static readonly Color gold       = new Color( 0xffd700ff );
    public static readonly Color goldenrod  = new Color( 0xdaa520ff );
    public static readonly Color orange     = new Color( 0xffa500ff );
    public static readonly Color brown      = new Color( 0x8b4513ff );
    public static readonly Color tan        = new Color( 0xd2b48cff );
    public static readonly Color firebrick  = new Color( 0xb22222ff );
    public static readonly Color red        = new Color( 0xff0000ff );
    public static readonly Color scarlet    = new Color( 0xff341cff );
    public static readonly Color coral      = new Color( 0xff7f50ff );
    public static readonly Color salmon     = new Color( 0xfa8072ff );
    public static readonly Color pink       = new Color( 0xff69b4ff );
    public static readonly Color magenta    = new Color( 1, 0, 1, 1 );
    public static readonly Color purple     = new Color( 0xa020f0ff );
    public static readonly Color violet     = new Color( 0xee82eeff );
    public static readonly Color maroon     = new Color( 0xb03060ff );

    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }

    public Color() : this( 0, 0, 0, 0 )
    {
    }

    public Color( int rgba8888 ) : this( ( uint )rgba8888 )
    {
    }

    public Color( uint rgba8888 )
    {
        RGBA8888ToColor( this, rgba8888 );
    }

    public Color( float r, float g, float b, float a )
    {
        R = r;
        G = g;
        B = b;
        A = a;

        Clamp();
    }

    public Color( Color color )
    {
        Set( color );
    }

    public Color Set( Color color )
    {
        R = color.R;
        G = color.G;
        B = color.B;
        A = color.A;

        return this;
    }

    /// <summary>
    /// Sets the Color components using the specified integer value
    /// in the format RGBA8888. This is inverse to the
    /// RGBA8888(r, g, b, a) method.
    /// </summary>
    /// <param name="color">The Color to be modified.</param>
    /// <param name="value">An integer color value in RGBA8888 format.</param>
    private void RGBA8888ToColor( Color color, uint value )
    {
        color.R = ( ( value & 0xff000000 ) >> 24 ) / 255f;
        color.G = ( ( value & 0x00ff0000 ) >> 16 ) / 255f;
        color.B = ( ( value & 0x0000ff00 ) >> 8 ) / 255f;
        color.A = ( ( value & 0x000000ff ) ) / 255f;
    }

    public int RGBA8888( float r, float g, float b, float a ) =>
        ( ( int )( r * 255 ) << 24 )
        | ( ( int )( g * 255 ) << 16 )
        | ( ( int )( b * 255 ) << 8 )
        | ( int )( a * 255 );

    /// <summary>
    /// Clamps this Color's components to a valid range [0 - 1]
    /// </summary>
    /// <returns>This Color for chaining.</returns>
    private Color Clamp()
    {
        if ( R < 0 )
        {
            R = 0;
        }
        else if ( R > 1 )
        {
            R = 1;
        }

        if ( G < 0 )
        {
            G = 0;
        }
        else if ( G > 1 )
        {
            G = 1;
        }

        if ( B < 0 )
        {
            B = 0;
        }
        else if ( B > 1 )
        {
            B = 1;
        }

        if ( A < 0 )
        {
            A = 0;
        }
        else if ( A > 1 )
        {
            A = 1;
        }

        return this;
    }
}
