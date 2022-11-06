// ##################################################

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

// ##################################################

namespace Lugh.Scene2D;

public class Actor
{
    [AllowNull] public Stage  Stage  { get; set; }
    [AllowNull] public Group  Parent { get; set; }
    [AllowNull] public string Name   { get; set; }

    public bool Visible { get; set; }

    public List<Action> Actions { get; set; }

    [AllowNull] private object _userObject;

    private DelayedRemovalArray<IEventListener> _listeners;
    private DelayedRemovalArray<IEventListener> _captureListeners;

    private Touchable _touchable;
    private Vec2F     _size;
    private Vec2F     _position;
    private Vec2F     _origin;
    private Vec2F     _scale;
    private float     _rotation;
    private Color     _color;
    private bool      _debugMode;

    // -----------------------------------------------
    // Code
    // -----------------------------------------------
    public void Draw( SpriteBatch batch, float parentAlpha )
    {
    }

    public void Act( float delta )
    {
    }

    public bool Fire( Event ev )
    {
        return false;
    }

    /// <summary>
    /// Notifies this actor's listeners of the event. The event is
    /// not propagated to any ascendants. The event must be set before
    /// calling this method. Before notifying the listeners, this actor
    /// is set as the listener actor.
    /// If this actor is not in the stage, the stage must be set before
    /// calling this method.
    /// </summary>
    public bool Notify( Event ev, bool capture )
    {
        return false;
    }

    /// <summary>
    /// Returns the deepest {@link #isVisible() visible} (and optionally,
    /// {@link #getTouchable() touchable}) actor that contains the specified
    /// point, or null if no actor was hit.The point is specified in the
    /// actor's local coordinate system (0,0 is the bottom left of the
    /// actor and width, height is the upper right).
    /// 
    /// This method is used to delegate touchDown, mouse, and enter/exit
    /// events. If this method returns null, those events will not occur
    /// on this Actor.
    /// The default implementation returns this actor if the point is within
    /// this actor's bounds and this actor is visible.
    /// </summary>
    public Actor Hit( float x, float y, bool touchable )
    {
        return null;
    }

    /// <summary>
    /// Removes this actor from its parent, if it has a parent.
    /// </summary>
    /// <returns>TRUE if successful.</returns>
    public bool Remove()
    {
        return false;
    }

    public bool AddListener( IEventListener listener )
    {
        return false;
    }

    public bool RemoveListener( IEventListener listener )
    {
        return false;
    }

    public DelayedRemovalArray<IEventListener> GetListeners()
    {
        return _listeners;
    }

    public bool AddCaptureListener( [DisallowNull] IEventListener listener )
    {
        if ( !_captureListeners.Contains( listener ) )
        {
            _captureListeners.Add( listener );
        }

        return true;
    }

    public bool RemoveCaptureListener( [DisallowNull] IEventListener listener )
    {
        return _captureListeners.Remove( listener );
    }

    public DelayedRemovalArray<IEventListener> GetCaptureListeners()
    {
        return _captureListeners;
    }

    public void AddAction( Action action )
    {
        action.Actor = this;
        Actions.Add( action );

//            if ( stage != null && stage.getActionsRequestRendering() )
//            {
//                Gdx.graphics.requestRendering();
//            }
    }

    public void RemoveAction( Action action )
    {
    }

    /// <summary>
    /// Returns true if the actor has one or more actions.
    /// </summary>
    public bool HasActions() => Actions.Count > 0;

    /// <summary>
    /// Removes all actions on this actor.
    /// </summary>
    public void ClearActions()
    {
    }

    /// <summary>
    /// Removes all listeners on this actor.
    /// </summary>
    public void ClearListeners()
    {
    }

    /// <summary>
    /// Removes all actions and listeners on this actor.
    /// </summary>
    public void Clear()
    {
        ClearActions();
        ClearListeners();
    }

    /// <summary>
    /// Returns true if this actor is the same as or is the
    /// descendant of the specified actor.
    /// </summary>
    public bool IsDescendantOf( Actor actor )
    {
        return false;
    }

    /// <summary>
    /// Returns true if this actor is the same as or is the
    /// ascendant of the specified actor.
    /// </summary>
    public bool IsAscendantOf( Actor actor )
    {
        return false;
    }

    /// <summary>
    /// Returns this actor or the first ascendant of this actor that
    /// is assignable with the specified type, or null if none were found.
    /// </summary>
    public T FirstAscendant<T>( T type ) where T : Actor
    {
        Actor actor = this;

        do
        {
            if ( type.GetType().IsInstanceOfType( actor ) )
            {
                return ( T )actor;
            }

            actor = actor.Parent;
        }
        while ( actor != null );

        return null;
    }

    public bool HasParent() => Parent != null;

    public bool IsTouchable() => _touchable == Touchable.Enabled;

    /// <summary>
    /// Called when the actor's position has been changed.
    /// </summary>
    protected void PositionChanged()
    {
    }

    /// <summary>
    /// Called when the actor's size has been changed.
    /// </summary>
    protected void SizeChanged()
    {
    }
        
    /// <summary>
    /// Called when the actor's scale has been changed.
    /// </summary>
    protected void ScaleChanged()
    {
    }

    /// <summary>
    /// Called when the actor's rotation has been changed.
    /// </summary>
    protected void RotationChanged()
    {
    }

    public float Rotation
    {
        get => _rotation;
        set
        {
            if ( Math.Abs( _rotation - value ) > 0.0001 )
            {
                _rotation = value;

                RotationChanged();
            }
        }
    }

    /// <summary>
    /// Adds the specified rotation to the current rotation.
    /// </summary>
    public void RotateBy( float degrees )
    {
        if ( degrees != 0 )
        {
            _rotation = ( _rotation + degrees ) % 360;

            RotationChanged();
        }
    }

    public Color GetColor() => _color;

    public void SetColor( Color color )
    {
        SetColor( color.R, color.G, color.B, color.A );
    }

    public void SetColor( byte r, byte g, byte b, byte a )
    {
        _color.R = r;
        _color.G = g;
        _color.B = b;
        _color.A = a;
            
            
    }

    /// <summary>
    /// Changes the z-order for this actor so it is in front of all siblings.
    /// </summary>
    public void ToFront()
    {
        SetZIndex( Int32.MaxValue );
    }

    /// <summary>
    /// Changes the z-order for this actor so it is in back of all siblings.
    /// </summary>
    public void ToBack()
    {
        SetZIndex( 0 );
    }

    public bool SetZIndex( int index )
    {
        return true;
    }

    public int GetZIndex()
    {
        return -1;
    }

    public bool ClipBegin()
    {
        return false;
    }

    public bool ClipBegin( float x, float y, float width, float height )
    {
        return false;
    }

    public void ClipEnd()
    {
    }

    public Vector2 ScreenToLocalCoordinates( Vector2 screenCoords )
    {
        return screenCoords;
    }

    public Vector2 StageToLocalCoordinates( Vector2 stageCoords )
    {
        return stageCoords;
    }

    public Vector2 ParentToLocalCoordinates( Vector2 parentCoords )
    {
        return parentCoords;
    }

    public Vector2 LocalToScreenCoordinates( Vector2 localCoords )
    {
        return localCoords;
    }

    public Vector2 LocalToStageCoordinates( Vector2 localCoords )
    {
        return localCoords;
    }

    public Vector2 LocalToParentCoordinates( Vector2 localCoords )
    {
        return localCoords;
    }

    public Vector2 LocalToAscendantCoordinates( Actor ascendant, Vector2 localCoords)
    {
        return localCoords;
    }

    public Vector2 LocalToActorCoordinates( Actor actor, Vector2 localCoords )
    {
        return localCoords;
    }

    public void DrawDebug( ShapeRenderer shapes )
    {
        DrawDebugBounds( shapes );
    }

    protected void DrawDebugBounds( ShapeRenderer shapes )
    {
        if ( !_debugMode )
        {
            return;
        }

        shapes.Set( ShapeRenderer.ShapeType.Line );

        if ( Stage != null )
        {
            shapes.SetColor( Stage.DebugColor );
        }

        shapes.Rect
            (
             _position.X, _position.Y,
             _origin.X, _origin.Y,
             _size.X, _size.Y,
             _scale.X, _scale.Y,
             _rotation
            );
    }

    public bool DebugMode
    {
        get => _debugMode;
        set
        {
            _debugMode = value;

            if ( DebugMode )
            {
                Stage.DebugMode = true;
            }
        }
    }

    public Actor Debug()
    {
        DebugMode = true;

        return this;
    }

    public override string ToString()
    {
        return "";
    }
}