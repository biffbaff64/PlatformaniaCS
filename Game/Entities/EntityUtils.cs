// ##################################################

using Box2DSharp.Dynamics;
using PlatformaniaCS.Game.Core;
using PlatformaniaCS.Game.Entities.Objects;
using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.Physics;

// ##################################################

namespace PlatformaniaCS.Game.Entities
{
    public class EntityUtils
    {
        public EntityUtils()
        {
        }
        
        public TextureRegion GetKeyFrame( Animation animation, float elapsedTime, bool looping ) => animation.GetKeyFrame( elapsedTime, looping );

        public Body AddDynamicPhysicsBody( Rectangle rectangle, CollisionFilter filter ) =>
                App.WorldModel.BodyBuilder.CreateDynamicBox(
                    rectangle,
                    filter,
                    1.0f,
                    B2DConstants.FullFriction,
                    B2DConstants.LowRestitution
                );

        public Body AddDynamicCirclePhysicsBody( Rectangle rectangle, CollisionFilter filter )
        {
            CircleF circle = new CircleF();
            circle.Position = new Point2( rectangle.X, rectangle.Y );
            circle.Radius   = ( ( rectangle.Width / 2f ) / Gfx.PPM );

            return App.WorldModel.BodyBuilder.CreateDynamicCircle(
                                                                  circle,
                                                                  filter,
                                                                  0.8f,
                                                                  B2DConstants.LowFriction,
                                                                  0.5f //B2DConstants.LOW_RESTITUTION
                                                                 );
        }

        public Body AddBouncyDynamicPhysicsBody( Rectangle rectangle, CollisionFilter filter ) =>
                App.WorldModel.BodyBuilder.CreateDynamicBox(
                    rectangle,
                    filter,
                    0.8f,
                    B2DConstants.LowFriction,
                    B2DConstants.HighRestitution
                );

        public Body AddKinematicPhysicsBody( Rectangle rectangle, CollisionFilter filter ) =>
                App.WorldModel.BodyBuilder.CreateKinematicBody(
                    rectangle,
                    filter,
                    1.0f,
                    B2DConstants.DefaultFriction,
                    B2DConstants.LowRestitution
                );

        public Body AddStaticPhysicsBody( Rectangle rectangle, CollisionFilter filter ) =>
                App.WorldModel.BodyBuilder.CreateStaticBody(
                    rectangle,
                    filter
                );

        public bool isOnScreen( GameSprite sprite ) => App.MapData.ViewportBox.Intersects( sprite.BoundsBox );

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

                case GraphicID.G_MOVING_PLATFORM:
                case GraphicID.G_SMALL_BOULDER:
                case GraphicID.G_CRATE:
                {
                    zed = 8;
                    break;
                }

                case GraphicID.G_EYES:
                case GraphicID.G_SPIKES:
                case GraphicID.G_JAIL:
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
                case GraphicID._IC_HAT:
                case GraphicID._IC_POTION:
                case GraphicID._IC_EGG:
                case GraphicID._IC_HELMET:
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
                case GraphicID._IC_EMERALD:
                case GraphicID._IC_BELT:
                {
                    zed = 6;
                    break;
                }

                case GraphicID.G_QUESTION_MARK:
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
                {
                    zed = 3;
                    break;
                }

                case GraphicID.G_PLAYER:
                case GraphicID.G_PRISONER:
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

        public void ResetAllPositions()
        {
        }

        public void KillAllExcept( GraphicID gidToAvoid )
        {
        }
    }
}