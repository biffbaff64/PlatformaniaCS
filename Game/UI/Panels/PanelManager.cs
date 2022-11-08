using System.Collections.Generic;

namespace PlatformaniaCS.Game.UI.Panels
{
    public class PanelManager
    {
        public class PanelDescriptor
        {
            public IPanel Panel   { get; set; }
            public bool   Enabled { get; set; }
            public string Name    { get; set; }

            public PanelDescriptor( IPanel panel, bool enabled, string name )
            {
                Panel   = panel;
                Enabled = enabled;
                Name    = name;
            }
        }

        public List<PanelDescriptor> Panels  { get; set; }
        public bool                  Enabled { get; set; }

        public PanelManager()
        {
            Panels  = new List<PanelDescriptor>();
            Enabled = false;
        }

        public void Update()
        {
        }

        public void Draw()
        {
        }

        public void AddSlidePanel( string imageName )
        {
        }

        public void AddZoomPanel( string imageName, int displayDelay )
        {
        }

        public void AddZoomPanel( string imageName, int displayDelay, int x, int y )
        {
        }

        public bool PanelExists( string name ) => false;

        public void SetPosition( string name, int x, int y )
        {
        }
    }
}