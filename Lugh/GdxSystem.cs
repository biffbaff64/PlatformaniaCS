// ##################################################

using System.Collections.Generic;
using Trace = Lugh.Utils.Trace;

// ##################################################

namespace Lugh
{
    public class GdxSystem
    {
        public const int LogNone  = 0;
        public const int LogDebug = 1;
        public const int LogInfo  = 2;
        public const int LogError = 3;

        public bool                 QuitToMainMenu       { get; set; }
        public bool                 ForceQuitToMenu      { get; set; }
        public bool                 GamePaused           { get; set; }
        public bool                 CamerasReady         { get; set; }
        public bool                 ShutDownActive       { get; set; }
        public bool                 EntitiesExist        { get; set; }
        public bool                 ControllersFitted    { get; set; }
        public bool                 GameButtonsReady     { get; set; }
        public ScreenID             CurrentScreenID      { get; set; }
        public string               CurrentController    { get; set; }
        public ControllerPos        VirtualControllerPos { get; set; }
        public List<ControllerType> AvailableInputs      { get; set; }
        public GameButtonRegion     FullScreenButton     { get; set; }
        public Switch               SystemBackButton     { get; set; }
        public ImageButton          BackButton           { get; set; }
        public IGdxScene            CurrentScene         { get; set; }

        private static readonly GdxSystem instance = new();

        public static GdxSystem Inst() => instance;

        public int LogLevel { get; set; }

        public void Setup()
        {
            Trace.CheckPoint();

            QuitToMainMenu    = false;
            ForceQuitToMenu   = false;
            GamePaused        = false;
            CamerasReady      = false;
            ShutDownActive    = false;
            EntitiesExist     = false;
            ControllersFitted = false;
            GameButtonsReady  = false;
            CurrentController = "None";
            AvailableInputs   = new List<ControllerType>();
            FullScreenButton  = new GameButtonRegion();
            SystemBackButton  = new Switch();
            CurrentScreenID   = ScreenID._NO_ID;
        }

        public void Exit()
        {
        }
    }
}