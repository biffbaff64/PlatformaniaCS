// ##################################################

// ##################################################

using Lugh.Audio;

namespace PlatformaniaCS.Game.Audio;

public abstract class AudioData
{
    public const int Silent = 0;

    public const int MinVolume          = 0;
    public const int MaxVolume          = 10;
    public const int VolumeIncrement    = 1;
    public const int VolumeMultiplier   = 10;
    public const int DefaultMusicVolume = 4;
    public const int DefaultFxVolume    = 6;

    public const int SfxLaser         = 0;
    public const int SfxPlazma        = 1;
    public const int SfxExplosion1    = 2;
    public const int SfxExplosion2    = 3;
    public const int SfxExplosion3    = 4;
    public const int SfxExplosion4    = 5;
    public const int SfxExplosion5    = 6;
    public const int SfxThrust        = 7;
    public const int SfxPickup        = 8;
    public const int SfxTeleport      = 9;
    public const int SfxExtraLife     = 10;
    public const int SfxLaunchWarning = 11;
    public const int SfxTestSound     = 12;
    public const int SfxBeep          = 13;
    public const int SfxLost          = 14;
    public const int MaxSound         = 15;

    public const int MusTitle   = 0;
    public const int MusHiscore = 1;
    public const int MusGame    = 2;
    public const int MaxTunes   = 3;

    public static Music[]       Sounds { get; set; }
    public static SoundEffect[] Music  { get; set; }

    // ----------------------------------------------

    public static void Tidy()
    {
        Sounds = null;
        Music  = null;
    }
}
