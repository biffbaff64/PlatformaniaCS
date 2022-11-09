using System.Collections.Generic;

namespace Lugh.Entities;

public class Animation
{
    public enum PlayMode
    {
        _NORMAL,
        _REVERSED,
        _LOOP,
        _LOOP_REVERSED,
        _LOOP_PINGPONG,
        _LOOP_RANDOM,
    }

    public PlayMode Mode { get; set; }

    private List<TextureRegion> _keyFrames;
    private float               _frameDuration;
    private float               _animationDuration;
    private int                 _lastFrameNumber;
    private float               _lastStateTime;

    public Animation( float frameDuration, List<TextureRegion> keyFrames )
    {
        this._frameDuration = frameDuration;

        SetKeyFrames( keyFrames.ToArray() );
    }

    public Animation( float frameDuration, List<TextureRegion> keyFrames, PlayMode mode )
        : this( frameDuration, keyFrames )
    {
        Mode = mode;
    }

    public Animation( float frameDuration, params TextureRegion[] args )
    {
        this._frameDuration = frameDuration;

        SetKeyFrames( args );
    }

    /// <summary>
    /// Returns a frame based on the so called state time. This is the
    /// amount of seconds an object has spent in the state this Animation
    /// instance represents, e.g. running, jumping and so on. The mode
    /// specifies whether the animation is looping or not.
    /// </summary>
    /// <param name="stateTime">the time spent in the state represented by this animation.</param>
    /// <param name="looping">whether the animation is looping or not.</param>
    /// <returns>the frame of animation for the given state time.</returns>
    public TextureRegion GetKeyFrame( float stateTime, bool looping )
    {
        var oldPlayMode = Mode;

        if ( looping & ( Mode is PlayMode._NORMAL or PlayMode._REVERSED ) )
        {
            Mode = Mode == PlayMode._NORMAL ? PlayMode._LOOP : PlayMode._LOOP_REVERSED;
        }
        else
        {
            if ( !looping && Mode is not (PlayMode._NORMAL or PlayMode._REVERSED) )
            {
                Mode = Mode == PlayMode._LOOP_REVERSED ? PlayMode._REVERSED : PlayMode._LOOP;
            }
        }

        var frame = GetKeyFrame( stateTime );

        Mode = oldPlayMode;

        return frame;
    }

    /// <summary>
    /// Returns a frame based on the so called state time. This is the amount
    /// of seconds an object has spent in the state this Animation instance
    /// represents, e.g. running, jumping and so on using the mode specified
    /// by 'Mode'.
    /// </summary>
    /// <param name="stateTime">the time spent in the state represented by this animation.</param>
    /// <returns>the frame of animation for the given state time.</returns>
    public TextureRegion GetKeyFrame( float stateTime )
    {
        var frameNumber = GetKeyFrameIndex( stateTime );

        return _keyFrames[ frameNumber ];
    }

    /// <summary>
    /// Returns the current frame number.
    /// </summary>
    public int GetKeyFrameIndex( float stateTime )
    {
        int frameNumber;

        if ( _keyFrames.Count == 1 )
        {
            frameNumber = 0;
        }
        else
        {
            frameNumber = (int) ( stateTime / _frameDuration );

            switch ( Mode )
            {
                case PlayMode._NORMAL:
                    frameNumber = Math.Min( _keyFrames.Count - 1, frameNumber );
                    break;

                case PlayMode._LOOP:
                    frameNumber %= _keyFrames.Count;
                    break;

                case PlayMode._LOOP_PINGPONG:
                    frameNumber %= ( ( _keyFrames.Count * 2 ) - 2 );
                    break;

                case PlayMode._LOOP_RANDOM:
                    var lastFrameNumber = (int) ( ( _lastStateTime ) / _frameDuration );

                    if ( lastFrameNumber != frameNumber )
                    {
                        var random = new Random();

                        frameNumber = random.Next( _keyFrames.Count );
                    }
                    else
                    {
                        frameNumber = this._lastFrameNumber;
                    }

                    break;

                case PlayMode._REVERSED:
                    frameNumber = Math.Max( _keyFrames.Count - frameNumber - 1, 0 );
                    break;

                case PlayMode._LOOP_REVERSED:
                    frameNumber %= _keyFrames.Count;
                    frameNumber =  _keyFrames.Count - frameNumber - 1;
                    break;

                default:
                    break;
            }
        }

        _lastFrameNumber = frameNumber;
        _lastStateTime   = stateTime;

        return frameNumber;
    }

    public void SetKeyFrames( params TextureRegion[] args )
    {
        this._keyFrames = new List<TextureRegion>();

        foreach ( var t in args )
        {
            this._keyFrames.Add( t );
        }

        this._animationDuration = ( this._keyFrames.Count * this._frameDuration );
    }

    /// <summary>
    /// Whether the animation would be finished if played without
    /// looping (PlayMode#NORMAL), given the state time.
    /// </summary>
    /// <param name="stateTime"></param>
    /// <returns>Whether or not the animation is finished.</returns>
    public bool IsAnimationFinished( float stateTime )
    {
        var frameNumber = (int) ( stateTime / _frameDuration );

        return ( ( _keyFrames.Count - 1 ) < frameNumber );
    }

    /// <summary>
    /// The duration of a frame in seconds.
    /// </summary>
    public float FrameDuration
    {
        get => _frameDuration;
        set
        {
            _frameDuration     = value;
            _animationDuration = ( _keyFrames.Count * _frameDuration );
        }
    }

    /// <summary>
    /// The duration of the entire animation, number of frames
    /// times frame duration, in seconds.
    /// </summary>
    public float AnimationDuration
    {
        get => _animationDuration;
        set => _animationDuration = value;
    }
}