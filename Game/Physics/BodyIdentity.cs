
using PlatformaniaCS.Game.Entities.Objects;

namespace PlatformaniaCS.Game.Physics
{
    public class BodyIdentity
    {
        public GraphicID  Gid    { get; private set; }
        public GraphicID  Type   { get; private set; }
        public GameSprite Entity { get; private set; }

        public BodyIdentity( GameSprite entity, GraphicID gid, GraphicID type )
        {
            this.Entity = entity;
            this.Gid    = gid;
            this.Type   = type;
        }
        
        public override string ToString() =>
            "BodyIdentity{" +
            "gid="          + Gid    +
            ", type="       + Type   +
            ", entity="     + Entity +
            '}';
    }
}