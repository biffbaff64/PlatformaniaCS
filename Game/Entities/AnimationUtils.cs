using PlatformaniaCS.Game.Entities.Objects;

namespace PlatformaniaCS.Game.Entities;

public class AnimationUtils
{
    /// <summary>
    /// Gets an animation frame based on the supplied animation time.
    /// </summary>
    public TextureRegion GetKeyFrame( Animation animation, float elapsedTime, bool looping )
    {
        return animation.GetKeyFrame( elapsedTime, looping );
    }

    public Animation CreateAnimation
        ( String filename, TextureRegion[] destinationFrames, int frameCount, PlayMode playmode )
    {
        Animation animation;

        try
        {
            var textureRegion = App.Assets.GetAnimationRegion( filename );

            var tmpFrames = textureRegion.Split
                (
                 ( textureRegion.RegionWidth / frameCount ),
                 textureRegion.RegionHeight
                );

            Array.Copy( tmpFrames, 0, destinationFrames, 0, frameCount );

            animation = new Animation( 0.75f / 6f, tmpFrames )
            {
                PlayMode = playmode
            };
        }
        catch ( NullReferenceException npe )
        {
            Trace.Dbg( message: "Creating animation from " + filename + " failed!" );

            animation = null;
        }

        return animation;
    }

    public static void RandomiseAnimTime( GameSprite sprite )
    {
        var rand    = new Random();
        var counter = rand.Next( 5 );

        for ( int i = 0; i < counter; i++ )
        {
            sprite.ElapsedAnimTime += 1.0f; // TODO:
        }
    }
}
