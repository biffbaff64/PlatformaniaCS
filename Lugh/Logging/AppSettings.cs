namespace Lugh.Logging
{
    public class AppSettings : ApplicationSettingsBase
    {
        // ------------------------------------------------
        // Developer settings
        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        // Shows sprite AABB Boxes
        public bool GsSpriteBoxes
        {
            get => ( ( bool )this[ nameof(GsSpriteBoxes)] );
            set => this[ nameof(GsSpriteBoxes)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        // Shows game tile AABB Boxes
        public bool GsTileBoxes 
        {
            get => ( ( bool )this[ nameof(GsTileBoxes)] );
            set => this[ nameof(GsTileBoxes)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsScrollDemo
        {
            get => ( ( bool )this[ nameof(GsScrollDemo)] );
            set => this[ nameof(GsScrollDemo)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsButtonBoxes
        {
            get => ( ( bool )this[ nameof(GsButtonBoxes)] );
            set => this[ nameof(GsButtonBoxes)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsShowFPS
        {
            get => ( ( bool )this[ nameof(GsShowFPS)] );
            set => this[ nameof(GsShowFPS)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsShowDebug
        {
            get => ( ( bool )this[ nameof(GsShowDebug)] );
            set => this[ nameof(GsShowDebug)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsSpawnpoints
        {
            get => ( ( bool )this[ nameof(GsSpawnpoints)] );
            set => this[ nameof(GsSpawnpoints)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsCullSprites
        {
            get => ( ( bool )this[ nameof(GsCullSprites)] );
            set => this[ nameof(GsCullSprites)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsDisableEnemies
        {
            get => ( ( bool )this[ nameof(GsDisableEnemies)] );
            set => this[ nameof(GsDisableEnemies)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsDisablePlayer
        {
            get => ( ( bool )this[ nameof(GsDisablePlayer)] );
            set => this[ nameof(GsDisablePlayer)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsAutoplay
        {
            get => ( ( bool )this[ nameof(GsAutoplay)] );
            set => this[ nameof(GsAutoplay)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsMenuScene
        {
            get => ( ( bool )this[ nameof(GsMenuScene)] );
            set => this[ nameof(GsMenuScene)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsIntroPanel
        {
            get => ( ( bool )this[ nameof(GsIntroPanel)] );
            set => this[ nameof(GsIntroPanel)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsLevelSelect
        {
            get => ( ( bool )this[ nameof(GsLevelSelect)] );
            set => this[ nameof(GsLevelSelect)] = value;
        }

        // ------------------------------------------------
        // Configuration settings

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsBox2DPhysics
        {
            get => ( ( bool )this[ nameof(GsBox2DPhysics)] );
            set => this[ nameof(GsBox2DPhysics)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsUsingAshleyECS
        {
            get => ( ( bool )this[ nameof(GsUsingAshleyECS)] );
            set => this[ nameof(GsUsingAshleyECS)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsInstalled
        {
            get => ( ( bool )this[ nameof(GsInstalled)] );
            set => this[ nameof(GsInstalled)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsShaderProgram
        {
            get => ( ( bool )this[ nameof(GsShaderProgram)] );
            set => this[ nameof(GsShaderProgram)] = value;
        }

        // ------------------------------------------------
        // Game settings

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsShowHints
        {
            get => ( ( bool )this[ nameof(GsShowHints)] );
            set => this[ nameof(GsShowHints)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsJoystickLeft
        {
            get => ( ( bool )this[ nameof(GsJoystickLeft)] );
            set => this[ nameof(GsJoystickLeft)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsMusicEnabled
        {
            get => ( ( bool )this[ nameof(GsMusicEnabled)] );
            set => this[ nameof(GsMusicEnabled)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "true" )]
        public bool GsSoundsEnabled
        {
            get => ( ( bool )this[ nameof(GsSoundsEnabled)] );
            set => this[ nameof(GsSoundsEnabled)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "4" )]
        public int GsMusicVolume
        {
            get => ( ( int )this[ nameof(GsMusicVolume)] );
            set => this[ nameof(GsMusicVolume)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "6" )]
        public int GsFxVolume
        {
            get => ( ( int )this[ nameof(GsFxVolume)] );
            set => this[ nameof(GsFxVolume)] = value;
        }

        // ------------------------------------------------
        // Google Play Store - KEEP FOR NOW

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsPlayServices
        {
            get => ( ( bool )this[ nameof(GsPlayServices)] );
            set => this[ nameof(GsPlayServices)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsSignInStatus
        {
            get => ( ( bool )this[ nameof(GsSignInStatus)] );
            set => this[ nameof(GsSignInStatus)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsAchievements
        {
            get => ( ( bool )this[ nameof(GsAchievements)] );
            set => this[ nameof(GsAchievements)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsChallenges
        {
            get => ( ( bool )this[ nameof(GsChallenges)] );
            set => this[ nameof(GsChallenges)] = value;
        }

        [UserScopedSetting()]
        [DefaultSettingValue( "false" )]
        public bool GsEvents
        {
            get => ( ( bool )this[ nameof(GsEvents)] );
            set => this[ nameof(GsEvents)] = value;
        }

        public void BindAppSettings()
        {
        }
    }
}