// ##################################################

// ##################################################

namespace Lugh.Maps.Objects
{
    public class TextureMapObject : MapObject
    {
        public TextureMapObject() : this( null )
        {
        }

        public TextureMapObject( TextureRegion textureRegion ) : base()
        {
            this.TextureRegion = textureRegion;
        }

        public float         X             { get; set; } = 0.0f;
        public float         Y             { get; set; } = 0.0f;
        public float         OriginX       { get; set; } = 0.0f;
        public float         OriginY       { get; set; } = 0.0f;
        public float         ScaleX        { get; set; } = 1.0f;
        public float         ScaleY        { get; set; } = 1.0f;
        public float         Rotation      { get; set; } = 0.0f;
        public TextureRegion TextureRegion { get; set; } = null;
    }
}
