using System.Collections.ObjectModel;

using PlatformaniaCS.Game.Entities.Objects;
using PlatformaniaCS.Game.Entities.Actors.Hero;
using PlatformaniaCS.Game.Entities.Managers;
using PlatformaniaCS.Game.Graphics;

namespace PlatformaniaCS.Game.Entities;

public class EntityData : IDisposable
{
    // ==========================================================
    // Table of SpriteDescriptors describing the basic properties
    // of entities. Used to create placement tiles.
    // ==========================================================
    private static readonly ReadOnlyCollection< SpriteDescriptor > entityList =
        new ReadOnlyCollection< SpriteDescriptor >
            (
             new[]
             {
                 // -----------------------------------------------------
                 // Player
                 new SpriteDescriptor()
                 {
                     Name     = "Player",
                     GID      = GraphicID.G_PLAYER, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerIdleAsset,
                     Frames   = GameAssets.PlayerIdleFrames,
                     Size     = new SimpleVec2( 64, 36 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player Spawn",
                     GID      = GraphicID.G_PLAYER_SPAWNING, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerSpawnAsset,
                     Frames   = GameAssets.PlayerSpawnFrames,
                     Size     = new SimpleVec2( 64, 36 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player Crouch",
                     GID      = GraphicID.G_PLAYER_CROUCH, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerCrouchAsset,
                     Frames   = GameAssets.PlayerCrouchFrames,
                     Size     = new SimpleVec2( 64, 36 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player Run",
                     GID      = GraphicID.G_PLAYER_RUN, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerWalkAsset,
                     Frames   = GameAssets.PlayerRunFrames,
                     Size     = new SimpleVec2( 64, 36 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player Fight",
                     GID      = GraphicID.G_PLAYER_FIGHT, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerFightAsset,
                     Frames   = GameAssets.PlayerFightFrames,
                     Size     = new SimpleVec2( 64, 36 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player Jump",
                     GID      = GraphicID.G_PLAYER_JUMP, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerJumpAsset,
                     Frames   = GameAssets.PlayerJumpFrames,
                     Size     = new SimpleVec2( 64, 38 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player Hurt",
                     GID      = GraphicID.G_PLAYER_HURT, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerHurtAsset,
                     Frames   = GameAssets.PlayerHurtFrames,
                     Size     = new SimpleVec2( 64, 36 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._PLAYER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Player death",
                     GID      = GraphicID.G_PLAYER_DYING, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PlayerIdleAsset,
                     Frames   = GameAssets.PlayerIdleFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._PLAYER_TILE
                 },

                 // -----------------------------------------------------
                 // Other Main Characters
                 new SpriteDescriptor()
                 {
                     Name     = "Prisoner",
                     GID      = GraphicID.G_PRISONER, Type = GraphicID._MAIN,
                     Asset    = GameAssets.PrisonerIdleAsset,
                     Frames   = GameAssets.PrisonerIdleFrames,
                     Size     = new SimpleVec2( 80, 80 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._PRISONER_TILE
                 },

                 // -----------------------------------------------------
                 // Mobile Enemies
                 new SpriteDescriptor()
                 {
                     Name     = "Bat",
                     GID      = GraphicID.G_BAT, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.BatFlyAsset,
                     Frames   = GameAssets.BatFlyFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._BAT_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Bat Attack",
                     GID      = GraphicID.G_BAT_ATTACK, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.BatAttackAsset,
                     Frames   = GameAssets.BatAttackFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._BAT_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Beast",
                     GID      = GraphicID.G_BEAST, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.BeastIdleAsset,
                     Frames   = GameAssets.BeastIdleFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._BEAST_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Beast Walk",
                     GID      = GraphicID.G_BEAST_WALK, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.BeastWalkAsset,
                     Frames   = GameAssets.BeastWalkFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._BEAST_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Beast Attack",
                     GID      = GraphicID.G_BEAST_ATTACK, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.BeastAttackAsset,
                     Frames   = GameAssets.BeastAttackFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._BEAST_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Mushroom",
                     GID      = GraphicID.G_MUSHROOM, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.MushroomWalkAsset,
                     Frames   = GameAssets.MushroomWalkFrames,
                     Size     = new SimpleVec2( 41, 30 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._MUSHROOM_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Crow Man",
                     GID      = GraphicID.G_CROW_MAN, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.CrowManIdleAsset,
                     Frames   = GameAssets.CrowManIdleFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._CROW_MAN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Crow Man Run",
                     GID      = GraphicID.G_CROW_MAN_RUN, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.CrowManWalkAsset,
                     Frames   = GameAssets.CrowManWalkFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._CROW_MAN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Crow Man Attack",
                     GID      = GraphicID.G_CROW_MAN_ATTACK, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.CrowManAttackAsset,
                     Frames   = GameAssets.CrowManAttackFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._CROW_MAN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Worm Man",
                     GID      = GraphicID.G_WORM_MAN, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.WormManIdleAsset,
                     Frames   = GameAssets.WormManIdleFrames,
                     Size     = new SimpleVec2( 74, 64 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._WORM_MAN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Worm Man Run",
                     GID      = GraphicID.G_WORM_MAN_RUN, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.WormManWalkAsset,
                     Frames   = GameAssets.WormManWalkFrames,
                     Size     = new SimpleVec2( 74, 64 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._WORM_MAN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Worm Man Attack",
                     GID      = GraphicID.G_WORM_MAN_ATTACK, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.WormManAttackAsset,
                     Frames   = GameAssets.WormManAttackFrames,
                     Size     = new SimpleVec2( 74, 64 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._WORM_MAN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Turtle",
                     GID      = GraphicID.G_SPIKEY_TURTLE, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.SpikeyTurtleAsset,
                     Frames   = GameAssets.SpikeyTurtleFrames,
                     Size     = new SimpleVec2( 48, 34 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._SPIKEY_TURTLE_TILE
                 },

                 // -----------------------------------------------------
                 // Stationary Enemies / Hazards
                 new SpriteDescriptor()
                 {
                     Name     = "Plant",
                     GID      = GraphicID.G_PLANT, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.PlantIdleAsset,
                     Frames   = GameAssets.PlantIdleFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     AnimRate = 0.4f,
                     Tile     = TileID._PLANT_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Plant Fight",
                     GID      = GraphicID.G_PLANT_FIGHTING, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.PlantAttackAsset,
                     Frames   = GameAssets.PlantAttackFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._PLANT_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Plant Hurt",
                     GID      = GraphicID.G_PLANT_HURT, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.PlantHurtAsset,
                     Frames   = GameAssets.PlantHurtFrames,
                     Size     = new SimpleVec2( 64, 64 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._PLANT_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Drop Block",
                     GID      = GraphicID.G_DROP_BLOCK, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.DropBlockAsset,
                     Frames   = GameAssets.DropBlockFrames,
                     Size     = new SimpleVec2( 48, 48 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._DROP_BLOCK_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Bat Stone",
                     GID      = GraphicID.G_BAT_BOMB, Type = GraphicID._ENEMY,
                     Asset    = GameAssets.BatStoneAsset,
                     Frames   = GameAssets.BatStoneFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     AnimRate = 0.4f,
                     Tile     = TileID._SMALL_BOULDER_TILE
                 },

                 // -----------------------------------------------------
                 // Pickups
                 new SpriteDescriptor()
                 {
                     Name     = "Coin",
                     GID      = GraphicID.G_COIN, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.CoinAsset,
                     Frames   = GameAssets.CoinFrames,
                     Size     = new SimpleVec2( 24, 24 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._COIN_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Gem",
                     GID      = GraphicID.G_GEM, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.BlueGemsAsset,
                     Frames   = GameAssets.GemsFrames,
                     Size     = new SimpleVec2( 28, 24 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._GEM_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Key",
                     GID      = GraphicID.G_KEY, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.KeyAsset,
                     Frames   = GameAssets.KeyFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._KEY_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Apple",
                     GID      = GraphicID.G_APPLE, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.AppleAsset,
                     Frames   = GameAssets.AppleFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._APPLE_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Heart",
                     GID      = GraphicID.G_HEART, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.HeartAsset,
                     Frames   = GameAssets.HeartFrames,
                     Size     = new SimpleVec2( 24, 24 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._HEART_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Treasure Chest",
                     GID      = GraphicID.G_TREASURE_CHEST, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.TreasureChestAsset,
                     Frames   = GameAssets.TreasureChestFrames,
                     Size     = new SimpleVec2( 48, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._CHEST_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Treasure Chest Opening",
                     GID      = GraphicID.G_TREASURE_CHEST_OPENING, Type = GraphicID._PICKUP,
                     Asset    = GameAssets.TreasureChestOpeningAsset,
                     Frames   = GameAssets.TreasureChestFrames,
                     Size     = new SimpleVec2( 48, 32 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._CHEST_TILE
                 },

                 // -----------------------------------------------------
                 // Item-bar Pickups

                 // -----------------------------------------------------
                 // Interactive
                 new SpriteDescriptor()
                 {
                     Name     = "Moving Platform",
                     GID      = GraphicID.G_MOVING_PLATFORM, Type = GraphicID._INTERACTIVE,
                     Asset    = GameAssets.MovingPlatformAsset,
                     Frames   = GameAssets.MovingPlatformFrames,
                     Size     = new SimpleVec2( 80, 16 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._MOVING_PLATFORM_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Small Platform",
                     GID      = GraphicID.G_MOVING_PLATFORM, Type = GraphicID._INTERACTIVE,
                     Asset    = GameAssets.SmallMovingPlatformAsset,
                     Frames   = GameAssets.MovingPlatformFrames,
                     Size     = new SimpleVec2( 40, 16 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._SMALL_MOVING_PLATFORM_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Small Boulder",
                     GID      = GraphicID.G_SMALL_BOULDER, Type = GraphicID._INTERACTIVE,
                     Asset    = GameAssets.SmallBoulderAsset,
                     Frames   = GameAssets.SmallBoulderFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._SMALL_BOULDER_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Crate",
                     GID      = GraphicID.G_PUSHABLE_CRATE, Type = GraphicID._INTERACTIVE,
                     Asset    = GameAssets.CrateAsset,
                     Frames   = GameAssets.CrateFrames,
                     Size     = new SimpleVec2( 48, 48 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._CRATE_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Locked Door",
                     GID      = GraphicID.G_LOCKED_DOOR, Type = GraphicID._INTERACTIVE,
                     Asset    = GameAssets.LockedDoorAsset,
                     Frames   = GameAssets.LockedDoorFrames,
                     Size     = new SimpleVec2( 64, 96 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._LOCKED_DOOR_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Floor Lever",
                     GID      = GraphicID.G_FLOOR_LEVER, Type = GraphicID._INTERACTIVE,
                     Asset    = GameAssets.FloorLeverAsset,
                     Frames   = GameAssets.FloorLeverFrames,
                     Size     = new SimpleVec2( 46, 29 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._FLOOR_LEVER_TILE
                 },

                 // -----------------------------------------------------
                 // Decorations
                 new SpriteDescriptor()
                 {
                     Name     = "Eyes",
                     GID      = GraphicID.G_EYES, Type = GraphicID._DECORATION,
                     Asset    = GameAssets.EyesAsset,
                     Frames   = GameAssets.EyesFrames,
                     Size     = new SimpleVec2( 46, 32 ),
                     PlayMode = Animation.PlayMode._LOOP_PINGPONG,
                     Tile     = TileID._EYES_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Flames",
                     GID      = GraphicID.G_FLAMES, Type = GraphicID._DECORATION,
                     Asset    = GameAssets.FlameAsset,
                     Frames   = GameAssets.FlameFrames,
                     Size     = new SimpleVec2( 29, 38 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._FLAME_TILE
                 },
                 new SpriteDescriptor()
                 {
                     Name     = "Bees",
                     GID      = GraphicID.G_BEES, Type = GraphicID._DECORATION,
                     Asset    = GameAssets.BeesAsset,
                     Frames   = GameAssets.BeesFrames,
                     Size     = new SimpleVec2( 32, 32 ),
                     PlayMode = Animation.PlayMode._LOOP,
                     Tile     = TileID._BEES_TILE
                 },

                 // -----------------------------------------------------
                 // Obstacles
                 new SpriteDescriptor()
                 {
                     Name     = "Wall",
                     GID      = GraphicID._WALL, Type = GraphicID._OBSTACLE,
                     Asset    = null,
                     Frames   = 0,
                     Size     = new SimpleVec2(),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._WALL_TILE
                 },

                 // -----------------------------------------------------
                 // Miscellaneous
                 new SpriteDescriptor()
                 {
                     Name     = "Speech Bubble",
                     GID      = GraphicID.G_HELP_BUBBLE, Type = GraphicID._SPEECH,
                     Asset    = GameAssets.HelpMeAsset,
                     Frames   = GameAssets.HelpMeFrames,
                     Size     = new SimpleVec2( 72, 52 ),
                     PlayMode = Animation.PlayMode._NORMAL,
                     Tile     = TileID._SPEECH_BUBBLE_TILE
                 },
             }
            );

    public MainPlayer                      MainPlayer  { get; set; }
    public List< IEntityComponent >        EntityMap   { get; set; }
    public List< IEntityManagerComponent > ManagerList { get; set; }

    // -----------------------------------------------------------------
    //        CODE
    // -----------------------------------------------------------------

    public EntityData()
    {
        EntityMap   = new List< IEntityComponent >();
        ManagerList = new List< IEntityManagerComponent >();
    }

    /// <summary>
    /// Adds an IEntityComponent to the entityMap.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if NULL is passed.</exception>
    public void AddEntity( IEntityComponent entity )
    {
        if ( entity != null )
        {
            EntityMap.Add( entity );
        }
        else
        {
            throw new ArgumentNullException
                (
                 "***** Attempt to add NULL Object, EntityMap current size: "
                 + EntityMap.Count
                );
        }
    }

    /// <summary>
    /// Gets the entity from entityMap at the supplied index.
    /// </summary>
    public IEntityComponent GetEntity( int index ) => EntityMap[ index ];

    /// <summary>
    /// Remove the entity at the supplied index from entityMap.
    /// </summary>
    public void RemoveEntityAt( int index )
    {
        EntityMap.RemoveAt( index );
    }

    /// <summary>
    /// Add an IEntityManagerComponent to the manager list.
    /// </summary>
    public void AddManager( IEntityManagerComponent manager )
    {
        if ( manager != null )
        {
            ManagerList.Add( manager );
        }
        else
        {
            throw new ArgumentNullException
                (
                 "***** Attempt to add NULL Object, ManagerList current size: "
                 + ManagerList.Count
                );
        }
    }

    /// <summary>
    /// Gets the entity manager at the specified array index.
    /// </summary>
    public IEntityManagerComponent GetManager( int index ) => ManagerList[ index ];

    /// <summary>
    /// Removes the specified manager from the manager array.
    /// </summary>
    /// <param name="manager">The manager to remove.</param>
    public void RemoveManager( IEntityManagerComponent manager )
    {
        if ( !ManagerList.Remove( manager ) )
        {
            Trace.Err( message: "Failed to remove ", args: manager.GetName() );
        }
    }

    public void Dispose()
    {
    }
}
