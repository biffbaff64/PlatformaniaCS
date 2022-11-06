using System.Collections;
using System.Data.Common;

using Microsoft.Xna.Framework.Graphics;

namespace Lugh.Graphics;

public class TextureRegion : IEnumerable
{
    public string    Name         { get; }
    public Texture2D Texture      { get; protected set; }
    public int       RegionX      { get; set; }
    public int       RegionY      { get; set; }
    public int       RegionWidth  { get; set; }
    public int       RegionHeight { get; set; }

    public Rectangle Bounds => new Rectangle( RegionX, RegionY, RegionWidth, RegionHeight );

    public TextureRegion()
    {
    }

    public TextureRegion( Texture2D texture )
        : this( texture, 0, 0, texture.Width, texture.Height )
    {
    }

    public TextureRegion( Texture2D texture, int width, int height )
        : this( texture, 0, 0, width, height )
    {
    }

    public TextureRegion( Texture2D texture, Rectangle region )
        : this( texture, region.X, region.Y, region.Width, region.Height )
    {
    }

    public TextureRegion( Texture2D texture, int x, int y, int width, int height )
    {
        Texture      = texture;
        RegionX      = x;
        RegionY      = y;
        RegionWidth  = width;
        RegionHeight = height;
    }

    public void SetRegion( TextureRegion region )
    {
        Texture = region.Texture;
    }

    public void SetRegion( float u, float v, float u2, float v2 )
    {
        var texWidth  = Texture.Width;
        var texHeight = Texture.Height;

        RegionWidth  = ( int )Math.Round( Math.Abs( u2 - u ) * texWidth );
        RegionHeight = ( int )Math.Round( Math.Abs( v2 - v ) * texHeight );
    }

    /// <summary>
    /// Helper method to create tiles out of this TextureRegion starting
    /// from the top left corner going to the right and ending at the bottom
    /// right corner. Only complete tiles will be returned so if the region's
    /// width or height are not a multiple of the tile width and height not
    /// all of the region will be used. This will not work on texture regions
    /// returned from a TextureAtlas that either have whitespace removed or
    /// where flipped before the region is split.
    /// </summary>
    /// <param name="tileWidth">a tile's width in pixels.</param>
    /// <param name="tileHeight">a tile's height in pixels.</param>
    /// <returns>2D array of TextureRegions indexed by [row][column].</returns>
    public TextureRegion[ , ] Split( int tileWidth, int tileHeight )
    {
        int x      = RegionX;
        int y      = RegionY;
        int width  = RegionWidth;
        int height = RegionHeight;
        int rows   = height / tileHeight;
        int cols   = width  / tileWidth;
        int startX = x;

        TextureRegion[ , ] tiles = new TextureRegion[ rows, cols ];

        for ( var row = 0; row < rows; row++, y += tileHeight )
        {
            x = startX;

            for ( var col = 0; col < cols; col++, x += tileWidth )
            {
                tiles[ row, col ] = new TextureRegion( Texture, x, y, tileWidth, tileHeight );
            }
        }

        return tiles;
    }

    public override string ToString()
    {
        return $"{( object )( Name ?? string.Empty )} {( object )Bounds}";
    }

    public IEnumerator GetEnumerator()
    {
        yield break;
    }
}