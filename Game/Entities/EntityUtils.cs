// ##################################################

using System.Numerics;

using PlatformaniaCS.Game.Entities.Objects;
using PlatformaniaCS.Game.Graphics;

// ##################################################

namespace PlatformaniaCS.Game.Entities
{
    public class EntityUtils
    {
        public EntityUtils()
        {
        }

        /**
     * Resets all members of entityMap to their initXY positions
     */
        public void ResetAllPositions()
        {
            if ( App.EntityData.EntityMap != null )
            {
                for ( var i = 0; i < App.EntityData.EntityMap.Count; i++ )
                {
                    if ( App.EntityData.GetEntity( i ).GetEntityType() == GraphicID._MAIN )
                    {
                        var entity = ( GameSprite )App.EntityData.GetEntity( i );

                        entity.GetPhysicsBody().Body.SetTransform
                            (
                             new Vector2
                                 (
                                  entity.InitXYZ.X,
                                  entity.InitXYZ.Y
                                 ),
                             entity.GetPhysicsBody().Body.GetAngle()
                            );
                    }
                }
            }
        }

        /**
     * Fetch an initial Z position for the specified ID.
     *
     * @param graphicID The GraphicID.
     * @return Z position range is between 0 and Gfx._MAXIMUM_Z_DEPTH.
     */
        public int GetInitialZPosition( GraphicID graphicID )
        {
            int zed;

            switch ( graphicID )
            {
                case GraphicID.G_SELECTION_RING:
                {
                    zed = Gfx.MaximumZDepth;
                    break;
                }

                case GraphicID.G_EYES:
                case GraphicID.G_FLAMES:
                case GraphicID.G_BEES:
                case GraphicID.G_FISH:
                case GraphicID.G_LOCKED_DOOR:
                {
                    zed = 9;
                    break;
                }

                case GraphicID.G_SPIKES:
                case GraphicID.G_SMALL_SPIKES:
                case GraphicID.G_JAIL:
                {
                    zed = 8;
                    break;
                }

                case GraphicID.G_MOVING_PLATFORM:
                case GraphicID.G_BIG_BLOCK:
                case GraphicID.G_PUSHABLE_BOULDER:
                case GraphicID.G_PUSHABLE_CRATE:
                case GraphicID.G_FLOOR_LEVER:
                case GraphicID.G_FLAG:
                case GraphicID.G_TREASURE_CHEST:
                {
                    zed = 7;
                    break;
                }

                case GraphicID.G_GEM:
                case GraphicID.G_COIN:
                case GraphicID.G_HEART:
                case GraphicID.G_KEY:
                case GraphicID.G_APPLE:
                case GraphicID._IC_CHEST:
                case GraphicID._IC_LETTER:
                case GraphicID._IC_MAP:
                case GraphicID._IC_BOOK1:
                case GraphicID._IC_SILVER_COIN:
                case GraphicID._IC_SCROLL:
                case GraphicID._IC_CANDLE:
                case GraphicID._IC_RUBY:
                case GraphicID._IC_RUNE:
                case GraphicID._IC_PICKAXE:
                case GraphicID._IC_BOOK2:
                case GraphicID._IC_CRATE:
                case GraphicID._IC_LANTERN:
                case GraphicID._IC_GOLD_COIN:
                case GraphicID._IC_AXE:
                case GraphicID._IC_WIZARDS_HAT:
                case GraphicID._IC_GREEN_POTION:
                case GraphicID._IC_EGG:
                case GraphicID._IC_METAL_HELMET:
                case GraphicID._IC_HAMMER:
                case GraphicID._IC_BOTTLE:
                case GraphicID._IC_GEAR:
                case GraphicID._IC_ARMOUR:
                case GraphicID._IC_STRING:
                case GraphicID._IC_BEER:
                case GraphicID._IC_BOOK3:
                case GraphicID._IC_BRONZE_COIN:
                case GraphicID._IC_PARCHMENT:
                case GraphicID._IC_FEATHER:
                case GraphicID._IC_BOOT:
                case GraphicID._IC_RUCKSACK:
                case GraphicID._IC_BELT:
                case GraphicID._IC_BREAD:
                case GraphicID._IC_EYE:
                case GraphicID._IC_ARROW:
                case GraphicID._IC_RED_WAND:
                case GraphicID._IC_GOLD_BAR:
                case GraphicID._IC_LONGBOW:
                case GraphicID._IC_BLUE_WAND:
                case GraphicID._IC_LEATHER_CHESTPLATE:
                case GraphicID._IC_GREEN_WAND:
                case GraphicID._IC_MUSHROOM:
                case GraphicID._IC_BLUE_POTION:
                case GraphicID._IC_ROPE:
                case GraphicID._IC_LEATHER_HELMET:
                case GraphicID._IC_PEARL:
                case GraphicID._IC_SWORD:
                case GraphicID._IC_RED_POTION:
                {
                    zed = 6;
                    break;
                }

                case GraphicID.G_QUESTION_MARK:
                case GraphicID.G_EXCLAMATION_MARK:
                {
                    zed = 5;
                    break;
                }

                case GraphicID.G_ENEMY_BULLET:
                {
                    zed = 4;
                    break;
                }

                case GraphicID.G_BAT:
                case GraphicID.G_BEAST:
                case GraphicID.G_PLANT:
                case GraphicID.G_MUSHROOM:
                case GraphicID.G_CROW_MAN:
                case GraphicID.G_WORM_MAN:
                case GraphicID.G_DROP_BLOCK:
                case GraphicID.G_SPIKEY_TURTLE:
                case GraphicID.G_FROG:
                case GraphicID.G_RABBIT:
                case GraphicID.G_GOBLIN:
                case GraphicID.G_MUSHROOM_MONSTER:
                case GraphicID.G_SPAWNER:
                {
                    zed = 3;
                    break;
                }

                case GraphicID.G_PLAYER:
                case GraphicID.G_PRISONER:
                case GraphicID.G_PLAYER_KNIFE:
                case GraphicID.G_PLAYER_ROCK:
                {
                    zed = 2;
                    break;
                }

                case GraphicID.G_LASER_BEAM:
                case GraphicID.G_LASER_BEAM_VERTICAL:
                case GraphicID.G_LASER_BEAM_HORIZONTAL:
                {
                    zed = 1;
                    break;
                }

                case GraphicID.G_PRIZE_BALLOON:
                case GraphicID.G_MESSAGE_BUBBLE:
                case GraphicID.G_HELP_BUBBLE:
                case GraphicID.G_EXPLOSION12:
                case GraphicID.G_EXPLOSION32:
                case GraphicID.G_EXPLOSION64:
                case GraphicID.G_EXPLOSION128:
                case GraphicID.G_EXPLOSION256:
                {
                    zed = 0;
                    break;
                }

                default:
                {
                    zed = Gfx.MaximumZDepth + 1;
                    break;
                }
            }

            return zed;
        }

        public bool IsOnScreen( GameSprite spriteObject )
        {
            return App.MapData.ViewportBox.Overlaps( spriteObject.Sprite.BoundingRectangle );
        }

        public void Tidy()
        {
            for ( var i = 0; i < App.EntityData.EntityMap.Count; i++ )
            {
                if ( App.EntityData.GetEntity( i ).GetActionState() == ActionStates._DEAD )
                {
                    App.EntityData.RemoveEntityAt( i );
                }
            }
        }

        public void KillAllExcept( GraphicID gidToLeave )
        {
            for ( var i = 0; i < App.EntityData.EntityMap.Count; i++ )
            {
                if ( App.EntityData.GetEntity( i ).GetGID() != gidToLeave )
                {
                    App.EntityData.GetEntity( i ).SetActionState( ActionStates._DEAD );
                    App.EntityData.GetEntity( i ).GetPhysicsBody().IsAlive = false;
                    App.WorldModel.BodiesList.Add( App.EntityData.GetEntity( i ).GetPhysicsBody() );
                }
            }

            Tidy();
        }

        /**
     * Gets a random sprite from the entity map, making
     * sure to not return the specified sprite.
     */
        public GameSprite GetRandomSprite( GameSprite oneToAvoid )
        {
            GameSprite randomSprite;
            var        rand = new Random();

            do
            {
                randomSprite = ( GameSprite )App.EntityData.GetEntity
                    ( rand.Next( App.EntityData.EntityMap.Count - 1 ) );
            }
            while ( ( randomSprite.GID == oneToAvoid.GID )
                    || ( randomSprite.Sprite == null )
                    || ( randomSprite.SpriteNumber == oneToAvoid.SpriteNumber ) );

            return randomSprite;
        }

        /**
     * Finds the nearest sprite of type gid to the player.
     */
        public GameSprite FindNearest( GraphicID gid )
        {
            var distantSprite = FindFirstOf( gid );

            if ( distantSprite != null )
            {
                var playerPos  = new Vector2( App.GetPlayer().Sprite.X, App.GetPlayer().Sprite.Y );
                var distantPos = new Vector2( distantSprite.Sprite.X,   distantSprite.Sprite.Y );
                var spritePos  = new Vector2();

                var distance = Vector2.Distance( playerPos, distantPos );

                // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
                foreach ( var entity in App.EntityData.EntityMap )
                {
                    if ( entity.GetGID() == gid )
                    {
                        var gdxSprite = ( GameSprite )entity;

                        spritePos.X = gdxSprite.Sprite.X;
                        spritePos.Y = gdxSprite.Sprite.Y;

                        var tempDistance = Vector2.Distance( playerPos, spritePos );

                        if ( Math.Abs( tempDistance ) < Math.Abs( distance ) )
                        {
                            distance      = tempDistance;
                            distantSprite = gdxSprite;
                        }
                    }
                }
            }

            return distantSprite;
        }

        /**
     * Finds the furthest sprite of type gid to the player.
     */
        public GameSprite GetDistantSprite( GraphicID targetGID )
        {
            GameSprite distantSprite = App.GetPlayer();

            var playerPos = new Vector2( App.GetPlayer().Sprite.X, App.GetPlayer().Sprite.Y );
            var spritePos = new Vector2();

            float distance = 0;

            foreach ( IEntityComponent entity in App.EntityData.EntityMap )
            {
                GameSprite gdxSprite = ( GameSprite )entity;

                spritePos.X = gdxSprite.Sprite.X;
                spritePos.Y = gdxSprite.Sprite.Y;

                var tempDistance = Vector2.Distance( playerPos, spritePos );

                if ( Math.Abs( tempDistance ) > Math.Abs( distance ) )
                {
                    distance      = tempDistance;
                    distantSprite = gdxSprite;
                }
            }

            return distantSprite;
        }

        public GameSprite FindFirstOf( GraphicID gid )
        {
            GameSprite gdxSprite = null;

            foreach ( IEntityComponent entity in App.EntityData.EntityMap )
            {
                if ( entity.GetGID() == gid )
                {
                    gdxSprite = ( GameSprite )entity;
                    break;
                }
            }

            return gdxSprite;
        }

        public GameSprite FindLastOf( GraphicID gid )
        {
            GameSprite gdxSprite = null;

            foreach ( IEntityComponent entity in App.EntityData.EntityMap )
            {
                if ( entity.GetGID() == gid )
                {
                    gdxSprite = ( GameSprite )entity;
                }
            }

            return gdxSprite;
        }

        public int FindNumberOfGid( GraphicID gid )
        {
            int count = 0;

            foreach ( IEntityComponent entity in App.EntityData.EntityMap )
            {
                if ( entity.GetGID() == gid )
                {
                    count++;
                }
            }

            return count;
        }

        public int FindNumberOfType( GraphicID type )
        {
            var count = 0;

            foreach ( IEntityComponent entity in App.EntityData.EntityMap )
            {
                if ( entity.GetEntityType() == type )
                {
                    count++;
                }
            }

            return count;
        }

        public bool CanRandomlyTurn( IEntityComponent entity )
        {
            return ( MathUtils.RndInt( 100 ) == 5 )
                   && ( entity.GetPhysicsBody().ContactCount > 1 );
        }

        public int GetHittingSameCount( GraphicID gid )
        {
            var count = 0;

            foreach ( var entity in App.EntityData.EntityMap )
            {
                if ( ( entity.GetGID() == gid ) && entity.IsHittingSame() )
                {
                    count++;
                }
            }

            return count;
        }
    }
}
