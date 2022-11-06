
namespace Lugh.Input
{
    public interface IGdxButton
    {
        enum Type
        {
            _SWITCH,
            _GAME_BUTTON,
            _ANIMATED_BUTTON,
            _BUTTON_REGION,
        }

        bool CheckPress( int touchX, int touchY );

        bool CheckRelease( int touchX, int touchY );

        void PressOnCondition( bool condition );

        bool IsDrawable() => false;

        void SetDrawable( bool state ) { }

        void ToggleDisabled();

        void TogglePressed();

        Type GetType();
    }
}