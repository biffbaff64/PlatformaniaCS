namespace PlatformaniaCS.Game.Physics;

public class CollisionFilter
{
    public ushort BodyCategory { get; set; } = 0;
    public ushort CollidesWith { get; set; } = 0;
    public bool   IsSensor     { get; set; } = false;

    public CollisionFilter( ushort bodyCategory, ushort collidesWith, bool sensor )
    {
        this.BodyCategory = bodyCategory;
        this.CollidesWith = collidesWith;
        this.IsSensor     = sensor;
    }
}