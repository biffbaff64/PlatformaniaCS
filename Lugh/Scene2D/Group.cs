// ##################################################

using Microsoft.Xna.Framework.Graphics;

using Scene2DCS.Utils;

// ##################################################

namespace Scene2DCS;

/// <summary>
/// 2D scene graph node that may contain other actors.
/// Actors have a z-order equal to the order they were inserted into the group.
/// Actors inserted later will be drawn on top of actors added earlier. Touch
/// events that hit more than one actor are distributed to topmost actors first.
/// </summary>
public class Group : Actor, ICullable
{
    public SnapshotArray< Actor > Children { get; set; }
        = new SnapshotArray< Actor >( true, 4, typeof( Actor ));

    private Vector2 _tmp = new Vector2();

    private Affine2                _worldTransform    = new Affine2();
    private Matrix4                _computedTransform = new Matrix4();
    private Matrix4                _oldTransform      = new Matrix4();
    private bool                   _transform         = true;
    private Rectangle              _cullingArea;

    public new void Act( float delta )
    {
    }

    public new void Draw( SpriteBatch batch, float parentAlpha )
    {
    }

    public new void DrawDebug( ShapeRenderer shapes )
    {
    }

    public void SetCullingArea( Rectangle cullingArea )
    {
    }

    public Rectangle GetCullingArea()
    {
        return default;
    }

    public new Actor Hit( float x, float y, bool touchable )
    {
        return default;
    }

    public void AddActor( Actor actor )
    {
    }

    public void AddActorAt( int index, Actor actor )
    {
    }

    public void AddActorBefore( Actor actorBefore, Actor actor )
    {
    }

    public void AddActorAfter( Actor actorAfter, Actor actor )
    {
    }

    public bool RemoveActor( Actor actor )
    {
        return false;
    }

    public bool RemoveActor( Actor actor, bool unfocus )
    {
        return false;
    }

    public Actor RemoveActorAt( int index, bool unfocus )
    {
        return default;
    }

    public void ClearChildren()
    {
    }

    public new void Clear()
    {
    }

    public T FindActor< T >( string name )
    {
        return default;
    }

    public bool SwapActor( int first, int second )
    {
        return false;
    }

    public bool SwapActor( Actor first, Actor second )
    {
        return false;
    }

    public Actor GetChild( int index )
    {
        return default;
    }

    public SnapshotArray< Actor > GetChildren()
    {
        return default;
    }

    public bool HasChildren()
    {
        return false;
    }

    public void SetTransform( bool transform )
    {
    }

    public bool IsTransform()
    {
        return false;
    }

    public Vector2 LocalToDescendantCoordinates( Actor descendant, Vector2 localCoords )
    {
        return default;
    }

    public void SetDebug( bool enabled, bool recursively )
    {
    }

    public Group DebugAll()
    {
        return this;
    }

    public new string ToString()
    {
        return String.Empty;
    }
}