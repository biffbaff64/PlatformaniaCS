
namespace PlatformaniaCS.Game.Entities.Objects
{
    public class SpriteDescriptor
    {
        public string             Name;
        public GraphicID          GID;      // ID. See GraphicID class for options.
        public TileID             Tile;     // The placement tile associated with this sprite.
        public string             Asset;    // The initial image asset.
        public int                Frames;   // Number of frames in the asset above.
        public GraphicID          Type;     // _MAIN, _INTERACTIVE, _PICKUP etc
        public SimpleVec2         Position; // X, Y Pos of tile, in TileWidth units, Z-Sort value.
        public SimpleVec2         Size;     // Width and Height.
        public int                Index;    // This entity's position in the entity map.
        public Animation.PlayMode PlayMode; // Animation playmode for the asset frames above.
        public float              AnimRate; // Animation speed
        public GameSprite         Parent;   // Parent GDXSprite (if applicable).
        public int                Link;     // Linked GDXSprite (if applicable).
        public Direction          Dir;      // Initial direction of travel. Useful for moving blocks etc.
        public SimpleVec2F        Dist;     // Initial travel distance. Useful for moving blocks etc.
        public SimpleVec2F        Speed;    // Initial speed. Useful for moving blocks etc.
        public Box                Box;      // 

        public SpriteDescriptor()
        {
            Name     = "";
            GID      = GraphicID.G_NO_ID;
            Type     = GraphicID.G_NO_ID;
            Position = new SimpleVec2();
            Size     = new SimpleVec2();
            Index    = 0;
            Frames   = 0;
            PlayMode = Animation.PlayMode._NORMAL;
            AnimRate = 1.0f;
            Asset    = "";
            Link     = 0;
            Tile     = 0;
            Parent   = null;
            Dir      = null;
            Dist     = null;
            Speed    = null;
            Box      = null;
        }

        public SpriteDescriptor( GraphicID graphicID,
                                 GraphicID type,
                                 string    asset,
                                 int       frames,
                                 TileID    tileID ) : this()
        {
            this.GID    = graphicID;
            this.Type   = type;
            this.Asset  = asset;
            this.Frames = frames;
            this.Tile   = tileID;
        }

        public SpriteDescriptor( string             name,
                                 GraphicID          graphicID,
                                 GraphicID          type,
                                 string             asset,
                                 int                frames,
                                 SimpleVec2         assetSize,
                                 Animation.PlayMode playMode,
                                 TileID             tileID ) : this( graphicID, type, asset, frames, tileID )
        {
            this.Name     = name;
            this.PlayMode = playMode;
            this.Size     = assetSize;
        }

        public SpriteDescriptor( SpriteDescriptor descriptor )
        {
            Set( descriptor );
        }

        public void Set( SpriteDescriptor descriptor )
        {
            Name     = descriptor.Name;
            GID      = descriptor.GID;
            Type     = descriptor.Type;
            Index    = descriptor.Index;
            Frames   = descriptor.Frames;
            PlayMode = descriptor.PlayMode;
            AnimRate = descriptor.AnimRate;
            Asset    = descriptor.Asset;
            Link     = descriptor.Link;
            Tile     = descriptor.Tile;
            Position = descriptor.Position;
            Size     = descriptor.Size;
            Parent   = descriptor.Parent;
            Dir      = descriptor.Dir;
            Dist     = descriptor.Dist;
            Speed    = descriptor.Speed;
            Box      = descriptor.Box;
        }

        public void Debug()
        {
        }
    }
}