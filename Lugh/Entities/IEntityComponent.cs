
using System.Drawing;

namespace Lugh.Entities;

public interface IEntityComponent
{
    ActionStates GetActionState();

    void SetActionState( ActionStates action );

    /**
     * Gets the {@link PhysicsBody} attached to this sprite.
     */
    PhysicsBody GetPhysicsBody();

    Rectangle GetBodyBox();

    /**
     * Gets the current X position of the {@link PhysicsBody}
     * attached to this sprite.
     */
    float GetBodyX();

    /**
     * Gets the current Y position of the {@link PhysicsBody}
     * attached to this sprite.
     */
    float GetBodyY();

    void Tidy( int index );

    short GetBodyCategory();

    short GetCollidesWith();

    int GetSpriteNumber();

    int GetLink();

    void SetLink( int lnk );

    bool IsLinked();

    bool IsHittingSame();

    GraphicID GetGID();

    GraphicID GetType();

    void SetDying();
}