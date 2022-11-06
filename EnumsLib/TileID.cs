namespace Enums;

public enum TileID
{
    _DEFAULT_TILE = 0,

    // Row 1
    _PLAYER_TILE,
    _COIN_TILE,
    _GEM_TILE,
    _CHEST_TILE,
    _HEART_TILE,
    _KEY_TILE,
    _APPLE_TILE,
    _U8_TILE,
    _U9_TILE,
    _U10_TILE,

    // Row 2
    _IC_CHEST_TILE,
    _IC_LETTER_TILE,
    _IC_MAP_TILE,
    _IC_BOOK1_TILE,
    _IC_SILVER_COIN_TILE,
    _IC_SCROLL_TILE,
    _IC_CANDLE_TILE,
    _IC_RUBY_TILE,
    _U19_TILE,
    _U20_TILE,

    // Row 3
    _IC_RUNE_TILE,
    _IC_PICKAXE_TILE,
    _IC_BOOK2_TILE,
    _IC_GOLD_KEY_TILE,
    _IC_LANTERN_TILE,
    _IC_GOLD_COIN_TILE,
    _IC_AXE_TILE,
    _IC_HAT_TILE,
    _U29_TILE,
    _U30_TILE,

    // Row 4
    _IC_POTION_TILE,
    _IC_EGG_TILE,
    _IC_HELMET_TILE,
    _IC_HAMMER_TILE,
    _IC_BOTTLE_TILE,
    _IC_GEAR_TILE,
    _IC_ARMOUR_TILE,
    _IC_KEY_TILE,
    _U39_TILE,
    _U40_TILE,

    // Row 5
    _IC_BEER_TILE,
    _IC_BOOK3_TILE,
    _IC_BRONZE_COIN_TILE,
    _IC_PARCHMENT_TILE,
    _IC_FEATHER_TILE,
    _IC_SILVER_KEY_TILE,
    _IC_EMERALD_TILE,
    _IC_BELT_TILE,
    _U49_TILE,
    _U50_TILE,

    // Row 6
    _MOVING_PLATFORM_TILE,
    _SPIKES_TILE,
    _SMALL_MOVING_PLATFORM_TILE,
    _SMALL_BOULDER_TILE,
    _CRATE_TILE,
    _FLAME_TILE,
    _BEES_TILE,
    _U58_TILE,
    _U59_TILE,
    _U60_TILE,

    // Row 7
    _BAT_TILE,
    _BEAST_TILE,
    _PLANT_TILE,
    _MUSHROOM_TILE,
    _CROW_MAN_TILE,
    _WORM_MAN_TILE,
    _DROP_BLOCK_TILE,
    _SPIKEY_TURTLE_TILE,
    _U69_TILE,
    _U70_TILE,

    // Row 8
    _EYES_TILE,
    _JAIL_TILE,
    _PRISONER_TILE,
    _LOCKED_DOOR_TILE,
    _FLOOR_LEVER_TILE,
    _U76_TILE,
    _U77_TILE,
    _U78_TILE,
    _U79_TILE,
    _U80_TILE,

    // Row 9
    _U81_TILE,
    _U82_TILE,
    _U83_TILE,
    _U84_TILE,
    _U85_TILE,
    _U86_TILE,
    _U87_TILE,
    _U88_TILE,
    _U89_TILE,
    _U90_TILE,

    // Row 10
    _U91_TILE,
    _U92_TILE,
    _U93_TILE,
    _U94_TILE,
    _U95_TILE,
    _U96_TILE,
    _U97_TILE,
    _U98_TILE,
    _U99_TILE,
    _NO_ACTION_TILE,

    // Tile IDs that are used in path finding
    _GROUND,
    _HOLE,
    _WATER,
    _GRASS,
    _WALL_EDGE,
    _WALL_TILE,

    // Tile IDs that aren't indexes into the tileset
    _EXPLOSION_TILE,
    _BOT_STONE_TILE,
    _SPEECH_BUBBLE_TILE,
        
    _UNKNOWN
}