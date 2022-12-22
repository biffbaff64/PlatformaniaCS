using System;

namespace LibGDX.Net
{
    public interface IApplication
    {
        public enum ApplicationType
        {
            Android,
            Desktop,
            HeadlessDesktop,
            WebGL,
        }

        public const int LogNone  = 0;
        public const int LogError = 1;
        public const int LogInfo  = 2;
        public const int LogDebug = 3;

        public IApplicationListener ApplicationListener { get; set; }
        public IApplicationLogger   ApplicationLogger   { get; set; }
        public IGraphics            Graphics            { get; set; }
        public IAudio               Audio               { get; set; }
        public IInput               Input               { get; set; }
        public IFiles               Files               { get; set; }
        public INet                 Net                 { get; set; }

        public void Log( string tag, string message );
        public void Log( string tag, string message, Exception exception );

        public void Error( string tag, string message );
        public void Error( string tag, string message, Exception exception );

        public void Debug( string tag, string message );
        public void Debug( string tag, string message, Exception exception );

        public int             LogLevel { get; set; }
        public ApplicationType Type     { get; set; }

        public int          GetVersion();
        public IPreferences GetPreferences( string name );
        public void         Exit();
        public void         AddLifecycleListener( ILifecycleListener    listener );
        public void         RemoveLifecycleListener( ILifecycleListener listener );
    }
}
