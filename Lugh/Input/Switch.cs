namespace Lugh.Input;

/// <summary>
/// A Simple ON/OFF switch class
/// </summary>
public class Switch : IGdxButton
{
    private bool _isPressed;

    public bool IsPressed
    {
        get => _isPressed;
        set
        {
            if ( !IsDisabled )
            {
                _isPressed = value;
            }
        }
    }

    public bool IsDisabled { get; set; }

    public Switch()
    {
        IsPressed  = false;
        IsDisabled = false;
    }

    /// <summary>
    /// Sets the Switch IsPressed state according to the result
    /// of the supplied condition.
    /// </summary>
    public void PressOnCondition( bool condition )
    {
        IsPressed = condition;
    }

    public bool CheckPress( int   touchX, int touchY ) => false;
    public bool CheckRelease( int touchX, int touchY ) => false;

    public new IGdxButton.Type GetType() => IGdxButton.Type._SWITCH;

    public void TogglePressed()
    {
        IsPressed = !IsPressed;
    }

    public void ToggleDisabled()
    {
        IsDisabled = !IsDisabled;
    }
}