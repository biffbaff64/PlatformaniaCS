using Microsoft.Xna.Framework.Input;

namespace PlatformaniaCS.Game.Input;

public class Keyboard
{
    public const Keys DefaultValueUp       = Keys.W;
    public const Keys DefaultValueDown     = Keys.S;
    public const Keys DefaultValueLeft     = Keys.A;
    public const Keys DefaultValueRight    = Keys.D;
    public const Keys DefaultValueA        = Keys.NumPad2;
    public const Keys DefaultValueB        = Keys.NumPad6;
    public const Keys DefaultValueX        = Keys.NumPad1;
    public const Keys DefaultValueY        = Keys.NumPad5;
    public const Keys DefaultValueHudInfo  = Keys.F9;
    public const Keys DefaultValuePause    = Keys.Escape;
    public const Keys DefaultValueSettings = Keys.F10;

    public bool CtrlHeld  { get; set; } = false;
    public bool ShiftHeld { get; set; } = false;
    
    
}