
namespace PlatformaniaCS.Game.UI;

public class ItemBar
{
    public const int NumItemPanels = 4;
    public const int ItemsPerPanel = 8;

    private const int X              = 0;
    private const int Y              = 1;
    private const int Width          = 2;
    private const int Height         = 3;
    private const int CollectPanel   = 0;
    private const int ItemsIndex     = 1;
    private const int HighlightIndex = 9;

        //@formatter:off
        public static GraphicID[ , ] ItemIDs =
        {
            {
                GraphicID._IC_CHEST, GraphicID._IC_LETTER, GraphicID._IC_MAP, GraphicID._IC_BOOK1,
                GraphicID._IC_SILVER_COIN, GraphicID._IC_SCROLL, GraphicID._IC_CANDLE, GraphicID._IC_RUBY,
            },
            {
                GraphicID._IC_RUNE, GraphicID._IC_PICKAXE, GraphicID._IC_BOOK2, GraphicID._IC_CRATE,
                GraphicID._IC_LANTERN, GraphicID._IC_GOLD_COIN, GraphicID._IC_AXE, GraphicID._IC_HAT,
            },
            {
                GraphicID._IC_POTION, GraphicID._IC_EGG, GraphicID._IC_HELMET, GraphicID._IC_HAMMER,
                GraphicID._IC_BOTTLE, GraphicID._IC_GEAR, GraphicID._IC_ARMOUR, GraphicID._IC_STRING,
            },
            {
                GraphicID._IC_BEER, GraphicID._IC_BOOK3, GraphicID._IC_BRONZE_COIN, GraphicID._IC_PARCHMENT,
                GraphicID._IC_FEATHER, GraphicID._IC_BOOT, GraphicID._IC_EMERALD, GraphicID._IC_BELT,
            },
        };

        private static string[ , ] _itemNames =
        {
            {
                "chest_text_box", "letter_text_box", "map_text_box", "book_text_box",
                "silver_coin_text_box", "scroll_text_box", "candle_text_box", "ruby_text_box",
            },
            {
                "rune_text_box", "pickaxe_text_box", "book_text_box", "crate_text_box",
                "lantern_text_box", "gold_coin_text_box", "axe_text_box", "hat_text_box",
            },
            {
                "potion_text_box", "egg_text_box", "helmet_text_box", "hammer_text_box",
                "bottle_text_box", "gear_text_box", "armour_text_box", "string_text_box",
            },
            {
                "beer_text_box", "book_text_box", "bronze_coin_text_box", "parchment_text_box",
                "feather_text_box", "boot_text_box", "emerald_text_box", "belt_text_box",
            },
        };

        private static int[ , ] _displayPos =
        {
            //
            {414, 12, 452, 82, 0, 0, 0, 0}, // Collection Panel

            //
            {448, 36, 32, 32, 441, 29, 48, 48},
            {498, 36, 32, 32, 491, 29, 48, 48},
            {548, 36, 32, 32, 541, 29, 48, 48},
            {598, 36, 32, 32, 591, 29, 48, 48},
            {648, 36, 32, 32, 641, 29, 48, 48},
            {698, 36, 32, 32, 691, 29, 48, 48},
            {748, 36, 32, 32, 741, 29, 48, 48},
            {798, 36, 32, 32, 791, 29, 48, 48},
        };

    //@formatter:on
}