
namespace Lugh.Entities;

public interface IEntityComponent
{
    void SetActionState( ActionStates action );

    void SetCollisionObject( float xPos, float yPos );

    void Tidy( int index );
}