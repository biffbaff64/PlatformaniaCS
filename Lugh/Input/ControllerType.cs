namespace Lugh.Input;

public enum ControllerType
{
    _VIRTUAL,  // Is using a virtual, onscreen controller
    _EXTERNAL, // Is using an external bluetooth/wired controller
    _KEYBOARD, // Is using Keyboard input, possibly combined with other types
}