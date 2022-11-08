namespace Lugh.Scene2D
{
    public class InputEvent : Event
    {
        public enum InputType
        {
            TouchDown,    // A new touch for a pointer on the stage was detected.
            TouchUp,      // A pointer has stopped touching the stage.
            TouchDragged, // A pointer that is touching the stage has moved.
            MouseMoved,   // The mouse pointer has moved (without a mouse button being active).
            Enter,        // The mouse pointer or an active touch have entered an actor.
            Exit,         // The mouse pointer or an active touch have exited an actor.
            Scrolled,     // The mouse scroll wheel has changed.
            KeyDown,      // A keyboard key has been pressed.
            KeyUp,        // A keyboard key has been released.
            KeyTyped      // A keyboard key has been pressed and released.
        }

        public float     StageX        { get; set; }
        public float     StageY        { get; set; }
        public InputType Type          { get; set; }
        public float     ScrollAmountX { get; set; }
        public float     ScrollAmountY { get; set; }
        public int       Pointer       { get; set; }
        public int       Button        { get; set; }
        public int       KeyCode       { get; set; }
        public char      Character     { get; set; }
        public Actor     RelatedActor  { get; set; }
        public bool      TouchFocus    { get; set; } = true;

        public void Reset()
        {
            base.Reset();

            RelatedActor = null;
            Button       = -1;
        }

        /// <summary>
        /// Sets actorCoords to this event's coordinates relative to the specified actor.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="actorCoords">Output for resulting coordinates.</param>
        public Vector2 ToCoordinates( Actor actor, Vector2 actorCoords )
        {
            actorCoords.SetX( StageX );
            actorCoords.SetY( StageY );

            actor.StageToLocalCoordinates( actorCoords );

            return actorCoords;
        }
        
        /// <summary>
        /// Returns true if this event is a touchUp triggered by <see cref="Stage#CancelTouchFocus"/>
        /// </summary>
        public bool IsTouchFocusCancel()
        {
            // ReSharper disable once ArrangeMethodOrOperatorBody
            return ( ( int ) StageX == Int32.MinValue ) || ( ( int ) StageY == Int32.MinValue );
        }
    }
}