// ##################################################

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Xna.Framework.Graphics;

using Color = Microsoft.Xna.Framework.Color;

// ##################################################

namespace Scene2DCS;

public class Actor
{
    [AllowNull] public Stage  Stage  { get; set; }
    [AllowNull] public Group  Parent { get; set; }
    [AllowNull] public string Name   { get; set; }

    public bool IsVisible { get; set; }

    public Vec2F        Size     { get; set; } = new();
    public Vec2F        Position { get; set; } = new();
    public Vec2F        Origin   { get; set; } = new();
    public List<Action> Actions  { get; set; } = new();

    [AllowNull] private object _userObject;

    private DelayedRemovalArray<IEventListener> _listeners        = new();
    private DelayedRemovalArray<IEventListener> _captureListeners = new();

    private Touchable _touchable = Touchable.Enabled;
    private Vec2F     _scale     = new();
    private float     _rotation;
    private Color     _color;
    private bool      _debugMode;

    // -----------------------------------------------
    // Code
    // -----------------------------------------------
    public void Act( float delta )
    {
    }

    public void Draw( SpriteBatch batch, float parentAlpha )
    {
    }

    public bool Fire( Event ev ) => false;

    /// <summary>
    /// Notifies this actor's listeners of the event. The event is
    /// not propagated to any ascendants. The event must be set before
    /// calling this method. Before notifying the listeners, this actor
    /// is set as the listener actor.
    /// If this actor is not in the stage, the stage must be set before
    /// calling this method.
    /// </summary>
    public bool Notify( Event ev, bool capture ) => false;

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
    public Actor Hit( float x, float y, bool touchable ) => null;

    /// <summary>
    /// Removes this actor from its parent, if it has a parent.
    /// </summary>
    /// <returns>TRUE if successful.</returns>
    public bool Remove() => false;

    public bool AddListener( IEventListener listener ) => false;

    public bool RemoveListener( IEventListener listener ) => false;

    public DelayedRemovalArray<IEventListener> GetListeners() => _listeners;

    public bool AddCaptureListener( [DisallowNull] IEventListener listener )
    {
        if ( !_captureListeners.Contains( listener ) )
        {
            _captureListeners.Add( listener );
        }

        return true;
    }

    public bool RemoveCaptureListener( [DisallowNull] IEventListener listener ) => _captureListeners.Remove( listener );

    public DelayedRemovalArray<IEventListener> GetCaptureListeners() => _captureListeners;

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
    public bool IsDescendantOf( Actor actor ) => false;

    /// <summary>
    /// Returns true if this actor is the same as or is the
    /// ascendant of the specified actor.
    /// </summary>
    public bool IsAscendantOf( Actor actor ) => false;

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

    protected void SetSize( float width, float height )
    {
        if ( ( Math.Abs( Size.X - width ) > 0.001f )
             || ( Math.Abs( Size.Y - height ) > 0.001f ) )
        {
            Size.X = width;
            Size.Y = height;

            SizeChanged();
        }
    }

    /// <summary>
    /// Adds the specified size to the current size.
    /// </summary>
    public void SizeBy( float size )
    {
        if ( size != 0 )
        {
            Size.X += size;
            Size.Y += size;

            SizeChanged();
        }
    }

    /// <summary>
    /// Adds the specified size to the current size.
    /// </summary>
    public void SizeBy( float width, float height )
    {
        if ( width != 0 || height != 0 )
        {
            Size.X += width;
            Size.Y += height;

            SizeChanged();
        }
    }

    /// <summary>
    /// Called when the actor's size has been changed.
    /// </summary>
    protected void SizeChanged()
    {
    }

    public float ScaleX
    {
        get => _scale.X;
        set
        {
            if ( Math.Abs( _scale.X - value ) > 0f )
            {
                _scale.X = value;

                ScaleChanged();
            }
        }
    }

    public float ScaleY
    {
        get => _scale.Y;
        set
        {
            if ( Math.Abs( _scale.Y - value ) > 0f )
            {
                _scale.Y = value;

                ScaleChanged();
            }
        }
    }

    /// <summary>
    /// Sets the scale for X and Y to the same value.  
    /// </summary>
    public void SetScale( float scale )
    {
        if ( ( Math.Abs( _scale.X - scale ) > 0f )
             || ( Math.Abs( _scale.Y - scale ) > 0f ) )
        {
            ScaleX = scale;
            ScaleY = scale;

            ScaleChanged();
        }
    }

    /// <summary>
    /// Sets the scaleX and scaleY.  
    /// </summary>
    public void SetScale( float scaleX, float scaleY )
    {
        if ( ( Math.Abs( _scale.X - scaleX ) > 0f )
             || ( Math.Abs( _scale.Y - scaleY ) > 0f ) )
        {
            ScaleX = scaleX;
            ScaleY = scaleY;

            ScaleChanged();
        }
    }

    /// <summary>
    /// Adds the specified scale to the current scale.
    /// </summary>
    public void ScaleBy( float scale )
    {
        if ( scale != 0f )
        {
        }
    }

    /// <summary>
    /// Called when the actor's scale has been changed.
    /// </summary>
    protected void ScaleChanged()
    {
    }

    public void SetOrigin( float originX, float originY )
    {
        Origin.X = originX;
        Origin.Y = originY;
    }

    public void SetOrigin( int alignment )
    {
        if ( ( alignment & Align.left ) != 0 )
        {
            Origin.X = 0;
        }
        else if ( ( alignment & Align.right ) != 0 )
        {
            Origin.X = Size.X;
        }
        else
        {
            Origin.X = Size.X / 2;
        }

        if ( ( alignment & Align.bottom ) != 0 )
        {
            Origin.Y = 0;
        }
        else if ( ( alignment & Align.top ) != 0 )
        {
            Origin.Y = Size.Y;
        }
        else
        {
            Origin.Y = Size.Y / 2;
        }
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

    public bool SetZIndex( int index ) => true;

    public int GetZIndex() => -1;

    public bool ClipBegin() => false;

    public bool ClipBegin( float x, float y, float width, float height ) => false;

    public void ClipEnd()
    {
    }

    /// <summary>
    /// Transforms the specified point in screen coordinates to the
    /// actor's local coordinate system.
    /// </summary>
    public Vector2 ScreenToLocalCoordinates( Vector2 screenCoords )
    {
        var stage = this.Stage;

        if ( stage == null )
        {
            return screenCoords;
        }

        return StageToLocalCoordinates( stage.ScreenToStageCoordinates( screenCoords ) );
    }

    /// <summary>
    /// Transforms the specified point in the stage's coordinates
    /// to the actor's local coordinate system.
    /// </summary>
    public Vector2 StageToLocalCoordinates( Vector2 stageCoords )
    {
        if ( Parent != null )
        {
            Parent.StageToLocalCoordinates( stageCoords );
        }

        ParentToLocalCoordinates( stageCoords );

        return stageCoords;
    }

    /// <summary>
    /// Converts the coordinates given in the parent's coordinate
    /// system to this actor's coordinate system.
    /// </summary>
    public Vector2 ParentToLocalCoordinates( Vector2 parentCoords )
    {
        var rotation = this.Rotation;
        var scaleX   = this.ScaleX;
        var scaleY   = this.ScaleY;
        var childX   = Position.X;
        var childY   = Position.Y;

        if ( rotation == 0 )
        {
            if ( scaleX == 1f && scaleY == 1f )
            {
                parentCoords.X -= childX;
                parentCoords.Y -= childY;
            }
            else
            {
                float originX = this.Origin.X;
                float originY = this.Origin.Y;

                parentCoords.X = ( parentCoords.X - childX - originX ) / scaleX + originX;
                parentCoords.Y = ( parentCoords.Y - childY - originY ) / scaleY + originY;
            }
        }
        else
        {
            float cos     = ( float )Math.Cos( rotation * MathUtils.DegreesToRadians );
            float sin     = ( float )Math.Sin( rotation * MathUtils.DegreesToRadians );
            float originX = this.Origin.X;
            float originY = this.Origin.Y;

            float tox = parentCoords.X - childX - originX;
            float toy = parentCoords.Y - childY - originY;

            parentCoords.X = ( tox * cos + toy * sin ) / scaleX + originX;
            parentCoords.Y = ( tox * -sin + toy * cos ) / scaleY + originY;
        }

        return parentCoords;
    }

    public Vector2 LocalToScreenCoordinates( Vector2 localCoords ) => localCoords;

    public Vector2 LocalToStageCoordinates( Vector2 localCoords ) => localCoords;

    public Vector2 LocalToParentCoordinates( Vector2 localCoords ) => localCoords;

    /// <summary>
    /// Converts coordinates for this actor to those of an ascendant.
    /// The ascendant is not required to be the immediate parent.
    /// </summary>
    public Vector2 LocalToAscendantCoordinates( Actor ascendant, Vector2 localCoords )
    {
        Actor actor = this;

        do
        {
            actor.LocalToParentCoordinates( localCoords );
            actor = actor.Parent;

            if ( actor == ascendant ) break;
        }
        while ( actor != null );

        return localCoords;
    }

    /// <summary>
    /// Converts coordinates for this actor to those of another actor,
    /// which can be anywhere in the stage.
    /// </summary>
    public Vector2 LocalToActorCoordinates( Actor actor, Vector2 localCoords )
    {
        LocalToStageCoordinates( localCoords: localCoords );

        return actor.StageToLocalCoordinates( localCoords );
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
             Position.X, Position.Y,
             Origin.X, Origin.Y,
             Size.X, Size.Y,
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
        var name = Name;

        if ( name == null )
        {
            name = this.GetType().Name;

            var dotIndex = name.LastIndexOf( '.' );

            if ( dotIndex != -1 )
            {
                name = name.Substring( dotIndex + 1 );
            }
        }

        return name;
    }
}