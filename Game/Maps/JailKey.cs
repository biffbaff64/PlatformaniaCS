namespace PlatformaniaCS.Game.Maps
{
    public class JailKey
    {
        public bool IsCollected { get; set; }
        public bool IsUsed      { get; set; }

        public JailKey()
        {
            this.IsCollected = false;
            this.IsUsed      = false;
        }
    }
}