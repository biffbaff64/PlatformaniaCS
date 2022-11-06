
namespace Lugh.Maths;

public interface IShape2D
{
    bool Contains( Vec2F point );
    bool Contains( float x, float y );
    bool Contains( int   x, int   y );
}