using System.Diagnostics.CodeAnalysis;
using MonoGame.Extended.Collections;

using IPoolable = Lugh.IPoolable;

namespace Scene2DCS;

public abstract class Action : IPoolable
{
    [AllowNull] public Actor        Target { get; set; }
    [AllowNull] public Pool<object> Pool   { get; set; }

    private Actor _actor;

    public Action()
    {
    }

    [AllowNull]
    public Actor Actor
    {
        get => _actor;
        set
        {
            _actor = value;

            if ( Target == null )
            {
                Target = _actor;
            }

            if ( _actor == null )
            {
                if ( Pool != null )
                {
                    Pool.Free( this );
                    Pool = null;
                }
            }
        }
    }

    /// <summary>
    /// Resets the optional state of this action to as if it were
    /// newly created, allowing the action to be pooled and reused.
    /// State required to be set for every usage of this action or
    /// computed during the action does not need to be reset.
    ///
    /// The default implementation calls <see cref="Restart"/>
    ///
    /// If a subclass has optional state, it must override this method,
    /// call super, and reset the optional state.
    /// </summary>
    public void Reset()
    {
        Actor  = null;
        Target = null;
        Pool   = null;

        Restart();
    }

    /// <summary>
    /// Updates the action based on time. Typically this is
    /// called each frame by <see cref="Actor"/>.
    /// </summary>
    /// <param name="delta">Time in seconds since the last frame.</param>
    /// <returns>
    /// true if the action is done. This method may continue
    /// to be called after the action is done.
    /// </returns>
    public abstract bool Act( float delta );

    /// <summary>
    /// Sets the state of the action so it can be run again.
    /// </summary>
    public void Restart()
    {
    }

    public override string ToString()
    {
        string name     = GetType().Name;
        int    dotIndex = name.LastIndexOf( '.' );

        if ( dotIndex != -1 )
        {
            name = name.Substring( dotIndex + 1 );
        }

        if ( name.EndsWith( "Action" ) )
        {
            name = name.Substring( 0, name.Length - 6 );
        }

        return name;
    }
}