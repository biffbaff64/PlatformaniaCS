namespace LibGDX.Net
{
    public interface IGraphics
    {
        enum GraphicsType
        {
            AndroidGL,
            LWJGL,
            WebGL,
            iOSGL,
            JGLFW,
            Mock,
            LWJGL3
        }

        class DisplayMode
        {
            public int Width        { get; set; }
            public int Height       { get; set; }
            public int RefreshRate  { get; set; }
            public int BitsPerPixel { get; set; }

            protected DisplayMode( int width, int height, int refreshRate, int bitsPerPixel )
            {
                this.Width        = width;
                this.Height       = height;
                this.RefreshRate  = refreshRate;
                this.BitsPerPixel = bitsPerPixel;
            }

            public new string ToString()
            {
                return Width + "x" + Height + ", bpp: " + BitsPerPixel + ", hz: " + RefreshRate;
            }
        }

        class Monitor
        {
            public int    VirtualX { get; set; }
            public int    VirtualY { get; set; }
            public string Name     { get; set; }

            protected Monitor( int virtualX, int virtualY, string name )
            {
                this.VirtualX = virtualX;
                this.VirtualY = virtualY;
                this.Name     = name;
            }
        }

        class BufferFormat
        {
            public int  R, G, B, A;
            public int  Depth;
            public int  Stencil;
            public int  Samples;
            public bool CoverageSampling;

            public BufferFormat( int  r, int g, int b, int a, int depth, int stencil, int samples,
                                 bool coverageSampling )
            {
                this.R                = r;
                this.G                = g;
                this.B                = b;
                this.A                = a;
                this.Depth            = depth;
                this.Stencil          = stencil;
                this.Samples          = samples;
                this.CoverageSampling = coverageSampling;
            }

            public new string ToString()
            {
                return "r: " + R + ", g: " + G + ", b: " + B + ", a: " + A + ", depth: " + Depth + ", stencil: " +
                       Stencil
                       + ", num samples: " + Samples + ", coverage sampling: " + CoverageSampling;
            }
        }
    }
}
