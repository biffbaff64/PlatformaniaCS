namespace PlatformaniaCS.Game.Graphics
{
    public class GameAssets : IDisposable
    {

        //
        // MainPlayer assets
        public const string PlayerIdleAsset   = "idle";
        public const string PlayerWalkAsset   = "run";
        public const string PlayerFightAsset  = "attack";
        public const string PlayerCrouchAsset = "crouch";
        public const string PlayerJumpAsset   = "jump";
        public const string PlayerHurtAsset   = "hurt";
        public const string PlayerSpawnAsset  = "spawn";

        //
        // Prisoner assets
        public const string PrisonerIdleAsset = "prisoner_stand_down";

        //
        // Pickups
        public const string BlueGemsAsset    = "gems_blue";
        public const string GreenGemsAsset   = "gems";
        public const string GoldGemsAsset    = "gems_gold";
        public const string RedGemsAsset     = "gems_red";
        public const string CoinAsset        = "coin";
        public const string HeartAsset       = "heart";
        public const string RunesAsset       = "runes";
        public const string GreyRunesAsset   = "grey_runes";
        public const string BooksAsset       = "books";
        public const string GreyBooksAsset   = "books_grey";
        public const string PotionsAsset     = "potions";
        public const string GreyPotionsAsset = "potions_grey";
        public const string KeyAsset         = "gold_key";
        public const string AppleAsset       = "apple";

        public const string IconsAsset        = "icons";
        public const string ICChestAsset      = "ic_icon1";
        public const string ICLetterAsset     = "ic_icon2";
        public const string ICMapAsset        = "ic_icon3";
        public const string ICBook1Asset      = "ic_icon4";
        public const string ICSilverCoinAsset = "ic_icon5";
        public const string ICScrollAsset     = "ic_icon6";
        public const string ICCandleAsset     = "ic_icon7";
        public const string ICRubyAsset       = "ic_icon8";
        public const string ICRuneAsset       = "ic_icon9";
        public const string ICPickaxeAsset    = "ic_icon10";
        public const string ICBook2Asset      = "ic_icon11";
        public const string ICGoldKeyAsset    = "ic_icon12";
        public const string ICLanternAsset    = "ic_icon13";
        public const string ICGoldCoinAsset   = "ic_icon14";
        public const string ICAxeAsset        = "ic_icon15";
        public const string ICHatAsset        = "ic_icon16";
        public const string ICPotionAsset     = "ic_icon17";
        public const string ICEggAsset        = "ic_icon18";
        public const string ICHelmetAsset     = "ic_icon19";
        public const string ICHammerAsset     = "ic_icon20";
        public const string ICBottleAsset     = "ic_icon21";
        public const string ICGearAsset       = "ic_icon22";
        public const string ICArmourAsset     = "ic_icon23";
        public const string ICKeyAsset        = "ic_icon24";
        public const string ICBeerAsset       = "ic_icon25";
        public const string ICBook3Asset      = "ic_icon26";
        public const string ICBronzeCoinAsset = "ic_icon27";
        public const string ICParchmentAsset  = "ic_icon28";
        public const string ICFeatherAsset    = "ic_icon29";
        public const string ICSilverKeyAsset  = "ic_icon30";
        public const string ICEmeraldAsset    = "ic_icon31";
        public const string ICBeltAsset       = "ic_icon32";


        //
        // Interactive Items
        public const string TreasureChestAsset        = "treasure_chest";
        public const string TreasureChestOpeningAsset = "treasure_chest_opening";
        public const string MovingPlatformAsset       = "moving_platform";
        public const string SmallMovingPlatformAsset  = "moving_platform_small";
        public const string SmallBoulderAsset         = "boulder32x32";
        public const string CrateAsset                = "crate";
        public const string LockedDoorAsset           = "locked_door";
        public const string FloorLeverAsset           = "floor_lever";

        //
        // Decorations
        public const string EyesAsset  = "eyes";
        public const string FlameAsset = "flames1";
        public const string BeesAsset  = "bees";

        //
        // Hazards
        public const string SpikesAsset    = "spike";
        public const string DropBlockAsset = "boulder64x64";
        public const string BatStoneAsset  = "small_boulder";

        //
        // Static Enemies
        public const string PlantIdleAsset   = "plant_idle";
        public const string PlantHurtAsset   = "plant_hurt";
        public const string PlantAttackAsset = "plant_attack";

        //
        // Mobile Enemies
        public const string BatFlyAsset        = "bat_fly";
        public const string BatAttackAsset     = "bat_attack";
        public const string BeastIdleAsset     = "beast_idle";
        public const string BeastWalkAsset     = "beast_walk";
        public const string BeastAttackAsset   = "beast_attack";
        public const string MushroomWalkAsset  = "mushroom_walk";
        public const string CrowManIdleAsset   = "enemy04_idle";
        public const string CrowManWalkAsset   = "enemy04_walk";
        public const string CrowManAttackAsset = "enemy04_attack";
        public const string WormManIdleAsset   = "enemy05_idle";
        public const string WormManWalkAsset   = "enemy05_walk";
        public const string WormManAttackAsset = "enemy05_attack";
        public const string SpikeyTurtleAsset  = "spikey_turtle_green";

        //
        // In-Game Messaging
        public const string HelpMeAsset            = "help_bubble";
        public const string ABXYAsset              = "abxy";
        public const string WelcomeMsgAsset        = "welcome_message";
        public const string KeyCollectedMsgAsset   = "key_collected";
        public const string GameoverMsgAsset       = "gameover";
        public const string KeyNeededMsgAsset      = "key_needed";
        public const string GetreadyMsgAsset       = "getready";
        public const string PressForTreasureAsset  = "press_for_treasure";
        public const string PressForPrisonerAsset  = "press_for_prisoner";
        public const string StormDemonWarningAsset = "storm_demon_warning";

        //
        // Weaponry and Explosions
        public const string Explosion64Asset = "explosion64";

        //
        // Fonts and HUD assets
        public const string CenturyFont    = "fonts/CENSCBK.ttf";
        public const string AcmeFont       = "fonts/Acme-Regular.ttf";
        public const string ProWindowsFont = "fonts/ProFontWindows.ttf";
        public const string HUDPanelFont   = "fonts/Acme-Regular.ttf";

        public const string HUDPanelAsset       = "hud_panel_candidate";
        public const string SplashScreenAsset   = "splash_screen";
        public const string CreditsPanelAsset   = "credits_panel";
        public const string OptionsPanelAsset   = "options_panel";
        public const string ControllerTestAsset = "controller_test_panel";
        public const string PausePanelAsset     = "pause_panel";
        public const string MessagePanelAsset   = "message_panel";
        public const string GameBackground      = "water_background";
        public const string BackgroundAsset     = "water_background";
        public const string UiskinAsset         = "ui/uiskin.json";
        public const string SmallMan            = "life";

        //
        // Frame counts for animations
        public const int SingleFrame = 1;

        public const int HelpMeFrames     = 3;
        public const int LockedDoorFrames = 1;

        public const int PlayerIdleFrames   = 9;
        public const int PlayerRunFrames    = 8;
        public const int PlayerFightFrames  = 7;
        public const int PlayerDyingFrames  = 6;
        public const int PlayerCrouchFrames = 1;
        public const int PlayerJumpFrames   = 1;
        public const int PlayerHurtFrames   = 1;
        public const int PlayerSpawnFrames  = 1;

        public const int PrisonerIdleFrames = 7;

        public const int VillagerIdleFrames = 8;

        public const int BatFlyFrames        = 7;
        public const int BatAttackFrames     = 10;
        public const int BeastIdleFrames     = 11;
        public const int BeastWalkFrames     = 8;
        public const int BeastAttackFrames   = 8;
        public const int PlantIdleFrames     = 8;
        public const int PlantHurtFrames     = 3;
        public const int PlantAttackFrames   = 16;
        public const int MushroomWalkFrames  = 10;
        public const int CrowManIdleFrames   = 7;
        public const int CrowManWalkFrames   = 6;
        public const int CrowManAttackFrames = 7;
        public const int WormManIdleFrames   = 9;
        public const int WormManWalkFrames   = 6;
        public const int WormManAttackFrames = 6;
        public const int DropBlockFrames     = 1;
        public const int SpikeyTurtleFrames  = 8;

        public const int SpikeFrames          = 9;
        public const int FlameFrames          = 8;
        public const int BeesFrames           = 10;
        public const int EyesFrames           = 3;
        public const int MovingPlatformFrames = 1;
        public const int SmallBoulderFrames   = 1;
        public const int CrateFrames          = 1;
        public const int BatStoneFrames       = 9;
        public const int FloorLeverFrames     = 7;

        public const int Explosion64Frames   = 12;
        public const int CoinFrames          = 4;
        public const int GemsFrames          = 6;
        public const int HeartFrames         = 6;
        public const int TreasureChestFrames = 5;
        public const int BooksFrames         = 8;
        public const int RunesFrames         = 8;
        public const int PotionsFrames       = 8;
        public const int IconsFrames         = ( 32 * 2 );
        public const int KeyFrames           = 6;
        public const int AppleFrames         = 6;

        public void Initialise()
        {
        }

        public void Dispose()
        {
        }
    }
}