namespace Enums;

public enum GraphicID
{
    // ----------------------------
    // The Player
    G_PLAYER,
    G_PLAYER_RUN,
    G_PLAYER_FIGHT,
    G_PLAYER_CAST,
    G_PLAYER_DYING,
    G_PLAYER_HURT,
    G_PLAYER_JUMP,
    G_PLAYER_CROUCH,
    G_PLAYER_SPAWNING,

    // ----------------------------
    // Other main, non-enemy, characters
    G_PRISONER,

    // ----------------------------
    // Pickup Items
    G_COIN,
    G_SPECIAL_COIN,
    G_HIDDEN_COIN,
    G_ARROW,
    G_SMALL_BULLET,
    G_GEM,
    G_SHIELD,
    G_HEART,
    G_KEY,
    G_HUD_KEY,
    G_APPLE,
    G_BOOK,
    G_CAKE,
    G_CHERRIES,
    G_GRAPES,
    G_SILVER_ARMOUR,
    G_GOLD_ARMOUR,
    G_LITTER,
    G_RUNE,
    G_POTION,
    G_TREASURE_CHEST,
    G_TREASURE_CHEST_OPENING,
    G_MYSTERY_BOX,

    // ----------------------------
    // Itembar Collectibles
    _IC_ARMOUR,
    _IC_AXE,
    _IC_BEER,
    _IC_BELT,
    _IC_BOOK1,
    _IC_BOOK2,
    _IC_BOOK3,
    _IC_BOOT,
    _IC_BOTTLE,
    _IC_BRONZE_COIN,
    _IC_CANDLE,
    _IC_CHEST,
    _IC_CRATE,
    _IC_EGG,
    _IC_EMERALD,
    _IC_FEATHER,
    _IC_GEAR,
    _IC_GOLD_COIN,
    _IC_HAMMER,
    _IC_HAT,
    _IC_HELMET,
    _IC_LANTERN,
    _IC_LETTER,
    _IC_MAP,
    _IC_PARCHMENT,
    _IC_PICKAXE,
    _IC_POTION,
    _IC_SCROLL,
    _IC_SILVER_COIN,
    _IC_STRING,
    _IC_RUBY,
    _IC_RUNE,

    // ----------------------------
    // Rune Types
    _LIGHTNING_RUNE,
    _FIRE_RUNE,
    _WIND_RUNE,
    _SUN_RUNE,
    _ICE_RUNE,
    _NATURE_RUNE,
    _WATER_RUNE,
    _EARTH_RUNE,

    // ----------------------------
    // Book Types
    _PURPLE_BOOK,
    _RED_BOOK,
    _BROWN_BOOK,
    _DARK_GREEN_BOOK,
    _YELLOW_BOOK,
    _GREEN_BOOK,
    _PINK_BOOK,
    _BLUE_BOOK,

    // ----------------------------
    // Potion Types
    _CYAN_POTION,
    _PURPLE_POTION,
    _GREEN_POTION,
    _YELLOW_POTION,
    _RED_POTION,
    _PINK_POTION,
    _BLUE_POTION,
    _RED_YELLOW_POTION,

    // ----------------------------
    // Decorations
    G_POT,
    G_CRATE,
    G_BARREL,
    G_GLOW_EYES,
    G_ALCOVE_TORCH,
    G_SACKS,
    G_PLANT_POT,
    G_WAGON_WHEEL,
    G_POT_STAND,
    G_ANVIL,
    G_HAMMERS,
    G_SKULL_BONES,
    G_EYES,
    G_JAIL,
    G_FLAMES,
    G_BEES,

    // ----------------------------
    // Interactive items
    G_DOOR,
    G_OPEN_DOOR,
    G_LOCKED_DOOR,
    G_EXIT_BLOCK,
    G_HIDDEN_EXIT_BLOCK,
    G_FLOOR_LEVER,
    G_LEVER_SWITCH,
    G_TELEPORTER,
    G_PRIZE_BALLOON,
    G_MESSAGE_BUBBLE,
    G_MESSAGE_PANEL,
    G_HELP_BUBBLE,
    G_DOCUMENT,
    G_QUESTION_MARK,
    G_EXCLAMATION_MARK,
    G_TALK_BOX,
    G_FLOATING_PLATFORM,
    G_MOVING_PLATFORM,
    G_SMALL_MOVING_PLATFORM,
    G_SELECTION_RING,
    G_CROSSHAIRS,
    G_ESCALATOR,
    G_ESCALATOR_LEFT,
    G_ESCALATOR_RIGHT,
    G_ESCALATOR_UP,
    G_ESCALATOR_DOWN,

    // ----------------------------
    G_EXPLOSION12,
    G_EXPLOSION32,
    G_EXPLOSION64,
    G_EXPLOSION128,
    G_EXPLOSION256,

    // ----------------------------
    // Enemies
    G_STORM_DEMON,
    G_BOUNCER,
    G_MINI_FIRE_BALL,
    G_SPIKE_BALL,
    G_SPIKE_BLOCK_HORIZONTAL,
    G_SPIKE_BLOCK_VERTICAL,
    G_DOUBLE_SPIKE_BLOCK,
    G_LOOP_BLOCK_HORIZONTAL,
    G_LOOP_BLOCK_VERTICAL,
    G_SPIKES,
    G_SCORPION,
    G_SCORPION_FIGHT,
    G_ENEMY_BULLET,
    G_ENEMY_FIREBALL,
    G_SOLDIER,
    G_SOLDIER_FIGHT,
    G_BIG_BLOCK_VERTICAL,
    G_BIG_BLOCK_HORIZONTAL,
    G_BAT,
    G_BAT_ATTACK,
    G_BAT_BOMB,
    G_BEAST,
    G_BEAST_WALK,
    G_BEAST_ATTACK,
    G_PLANT,
    G_PLANT_FIGHTING,
    G_PLANT_HURT,
    G_MUSHROOM,
    G_CROW_MAN,
    G_CROW_MAN_RUN,
    G_CROW_MAN_ATTACK,
    G_WORM_MAN,
    G_WORM_MAN_RUN,
    G_WORM_MAN_ATTACK,
    G_DROP_BLOCK,
    G_SPIKEY_TURTLE,
    G_BOULDER,
    G_SMALL_BOULDER,

    // ----------------------------
    G_LASER,
    G_LASER_BEAM,
    G_LASER_BEAM_VERTICAL,
    G_LASER_BEAM_HORIZONTAL,
    G_FLAME_THROWER,
    G_FLAME_THROWER_HORIZONTAL,
    G_FLAME_THROWER_VERTICAL,
    G_TURRET,

    // #########################################################
    // Generic IDs
    // ----------------------------
    _MONSTER,
    _BLOCKS,
    _GROUND,
    _CEILING,
    _WALL,
    _LETHAL_OBJECT,
    _HAZARD,
    _SIGN,
    _SPEECH,
    _HUD_PANEL,
    _EXIT_BOX,
    _BACKGROUND_ENTITY,
    _BRIDGE,
    _CRATER,

    // ----------------------------
    // Managers
    _BLOCKS_MANAGER,
    _DECORATIONS_MANAGER,
    _ENEMY_MANAGER,
    _INTERACTIVE_MANAGER,
    _PLAYER_MANAGER,
    _PICKUPS_MANAGER,
    _HAZARDS_MANAGER,

    // ----------------------------
    // Main Character type, i.e. Player
    _MAIN,

    // ----------------------------
    // Enemy Character type, but not stationary entities
    // like rocket launchers etc.
    _ENEMY,

    // ----------------------------
    // Encapsulating type, covering any collision IDs that can be stood on.
    // This will be checked against the collision object TYPE, not the NAME.
    _OBSTACLE,

    // ----------------------------
    // As above but for objects that can't be stood on and are not entities
    _DECORATION,

    // As above, but for entities
    _ENTITY,

    // ----------------------------
    // Interactive objects
    _PICKUP,
    _WEAPON,
    _INTERACTIVE,
    _PRISONER,
    _PLATFORM,
    _AUTO_FLOOR,
    _ENTITY_BARRIER,

    // ----------------------------
    // Messages
    _STORM_DEMON_WARNING,
    _PRESS_FOR_TREASURE,
    _PRESS_FOR_PRISONER,
    _PRESS_FOR_GUIDE,
    _KEY_NEEDED,

    // ----------------------------

    G_DUMMY,
    G_NO_ID
}