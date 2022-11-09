using Microsoft.Xna.Framework.Graphics;

namespace Lugh.UI;

public abstract class AbstractBasePanel : IUserInterfacePanel
{
    public StateID       StateManager  { get; set; }
    public Vec2F         Position      { get; set; }
    public string        NameID        { get; set; }
    public bool          IsActive      { get; set; }
    public int           OriginX       { get; set; }
    public int           OriginY       { get; set; }
    public TextureRegion TextureRegion { get; set; }

    public AbstractBasePanel() : this( 0, 0 )
    {
    }

    public AbstractBasePanel( int x, int y )
    {
        StateManager = StateID._INACTIVE;
        Position     = new Vec2F();
        IsActive     = false;
        NameID       = "";
        OriginX      = x;
        OriginY      = y;
    }

    // ------------------------------------------------------

    public void Open()
    {
        Setup();
    }

    public virtual bool Update()                    => false;
    public virtual int  GetWidth()                  => TextureRegion.RegionWidth;
    public virtual int  GetHeight()                 => TextureRegion.RegionHeight;
    public virtual bool NameExists( string nameID ) => nameID.Equals( NameID );

    public virtual void Initialise( TextureRegion region, string nameID, params object[] args )
    {
    }

    public virtual void Set( SimpleVec2F xy, SimpleVec2F distance, Direction direction, SimpleVec2F speed )
    {
    }

    public virtual void Draw( SpriteBatch spriteBatch )
    {
    }

    public virtual void Setup()
    {
    }

    public virtual void PopulateTable()
    {
    }

    public virtual void Dispose()
    {
        Position      = null;
        TextureRegion = null;
        NameID        = null;
    }

    // ------------------------------------------------------

    public void SetPosition( float x, float y )
    {
        Position.Set( x, y );
    }

    public void ForceZoomOut()
    {
    }

    public void SetPauseTime( int time )
    {
    }

    public void Close()
    {
        Dispose();
    }
}