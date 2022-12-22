namespace LibGDX.Net
{
    /// <summary>
    /// Environment class holding references to the Application,
    /// Graphics, Audio, Files and Input instances.
    /// </summary>
    public class Gdx
    {
        public static IApplication App      { get; set; }
        public static IGraphics    Graphics { get; set; }
        public static IAudio       Audio    { get; set; }
        public static IInput       Input    { get; set; }
        public static IFiles       Files    { get; set; }
        public static INet         Net      { get; set; }

        public static GL20 Gl   { get; set; }
        public static GL20 Gl20 { get; set; }
        public static GL30 Gl30 { get; set; }
    }
}
