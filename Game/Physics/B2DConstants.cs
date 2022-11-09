namespace PlatformaniaCS.Game.Physics;

public abstract class B2DConstants
{
    public const float StepTime           = ( 1.0f / 60.0f );
    public const int   VelocityIterations = 8;
    public const int   PositionIterations = 3;

    // -----------------------------------------
        
    public const float ZeroFriction    = 0.0f;
    public const float LowFriction     = 0.1f;
    public const float FullFriction    = 1.0f;
    public const float DefaultFriction = 0.8f;
    public const float ZeroRestitution = 0.0f;
    public const float LowRestitution  = 0.1f;
    public const float HighRestitution = 0.8f;
    public const float FullRestitution = 1.0f;
}