using Microsoft.Xna.Framework.Graphics;

using Scene2DCS.Utils;

namespace Scene2DCS;

public class Group : Actor, ICullable
{
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

    public T FindActor<T>( string name )
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