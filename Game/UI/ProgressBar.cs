using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

using PlatformaniaCS.Game.Graphics;

namespace PlatformaniaCS.Game.UI;

public class ProgressBar : ItemF, IDisposable
{
    private const int DefaultBarHeight = 26;
    private const int DefaultInterval  = 100;

    public bool JustEmptied     { get; set; }
    public bool IsAutoRefilling { get; set; }
    public Vec2 Position        { get; set; }

    private int       _subInterval;
    private int       _addInterval;
    private float     _speed;
    private float     _height;
    private float     _scale;
    private Stopwatch _stopwatch;
    private NinePatch _ninePatch;
    private NinePatch _ninePatchBase;

    public ProgressBar( float speed, int size, int maxSize, String texture, bool hasBase )
    {
        _ninePatch = new NinePatch( AssetUtils.LoadAsset<Texture2D>( texture ), 1, 1, 1, 1 );

        if ( hasBase )
        {
            _ninePatchBase = new NinePatch( AssetUtils.LoadAsset<Texture2D>( texture ), 1, 1, 1, 1 );
            _ninePatchBase.SetColor( Color.Black );
        }

        this.Minimum         = 0;
        this.Maximum         = maxSize;
        this.RefillAmount    = 0;
        this._stopwatch      = new Stopwatch();
        this.Total           = size;
        this._height         = DefaultBarHeight;
        this.Position        = new Vec2();
        this.RefillAmount    = maxSize;
        this.JustEmptied     = false;
        this.IsAutoRefilling = false;
        this._scale          = 1;
        this._speed          = speed;
        this._addInterval    = DefaultInterval;
        this._subInterval    = DefaultInterval;
    }

    public void Draw( SpriteBatch spriteBatch )
    {
        if ( Total > 0 )
        {
            if ( _ninePatchBase != null )
            {
                _ninePatchBase.Draw( spriteBatch, Position.X, Position.Y, Maximum * _scale, _height );
            }

            _ninePatch.Draw( spriteBatch, Position.X, Position.Y, Total * _scale, _height );
        }
    }

    public void UpdateSlowDecrement()
    {
        JustEmptied = false;

        if ( Total > 0 )
        {
            if ( _stopwatch.ElapsedMilliseconds >= _subInterval )
            {
                Total -= _speed;

                if ( IsEmpty() )
                {
                    JustEmptied = true;
                }

                _stopwatch.Reset();
            }
        }
    }

    public void UpdateSlowDecrementWithWrap( int wrap )
    {
        JustEmptied = false;

        if ( Total > 0 )
        {
            if ( _stopwatch.ElapsedMilliseconds >= _subInterval )
            {
                Total -= _speed;
                Total =  Math.Max( 0, Total );

                if ( IsEmpty() )
                {
                    Total = wrap;
                }

                _stopwatch.Reset();
            }
        }
    }

    public bool UpdateSlowIncrement()
    {
        if ( Total < Maximum )
        {
            if ( _stopwatch.ElapsedMilliseconds >= _addInterval )
            {
                Total += _speed;

                _stopwatch.Reset();
            }
        }

        return IsFull();
    }

    public void SetHeightColorScale( float height, Color color, float scale )
    {
        this._height = height;
        this._ninePatch.SetColor( color );
        this._scale = scale;
    }

    public void SetHeight( float height )
    {
        this._height = height;
    }

    public void SetPosition( int x, int y )
    {
        Position.X = x;
        Position.Y = y;
    }

    public bool HasRefillRoom() => HasRoom() && ( Total < ( Maximum - 10 ) );

    public void SetColor( Color color )
    {
        this._ninePatch.SetColor( color );
    }

    public float GetSpeed() => _speed;

    public void SetSpeed( float speed )
    {
        this._speed = speed;
    }

    public void SetSubInterval( int subInterval )
    {
        this._subInterval = subInterval;
    }

    public void SetAddInterval( int addInterval )
    {
        this._addInterval = addInterval;
    }

    public void Dispose()
    {
        _ninePatchBase = null;
        _ninePatch     = null;
        _stopwatch     = null;
    }
}