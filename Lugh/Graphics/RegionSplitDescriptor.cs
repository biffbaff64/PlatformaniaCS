namespace Lugh.Graphics
{
    public struct RegionSplitDescriptor
    {
        public int              Rows    { get; set; }
        public int              Columns { get; set; }
        public TextureRegion[,] Splits  { get; set; }
    }
}