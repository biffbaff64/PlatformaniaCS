namespace PlatformaniaCS.Game.Audio
{
    public class GameAudio
    {
        public int  CurrentTune     { get; set; }
        public int  MusicVolumeSave { get; set; }
        public int  FxVolumeSave    { get; set; }
        public bool MusicLoaded     { get; set; }
        public bool SoundsLoaded    { get; set; }
        public bool IsTunePaused    { get; set; }

        public GameAudio()
        {
        }

        public void Setup()
        {
            SoundsLoaded = false;
            MusicLoaded  = false;
            IsTunePaused = false;

            LoadSounds();

            MusicVolumeSave = Math.Max( 0, AudioData.DefaultMusicVolume );
            FxVolumeSave    = Math.Max( 0, AudioData.DefaultFxVolume );
        }

        public void Update()
        {
        }

        public void PlayTune( bool play )
        {
        }

        public void StartTune( int musicNumber, int volume, bool looping )
        {
        }

        public void StartSound( int soundNumber )
        {
        }

        public void StopTune()
        {
        }

        public void SetMusicVolume( int volume )
        {
        }

        public void SetFxVolume( int volume )
        {
        }

        private void LoadSounds()
        {
        }
    }
}