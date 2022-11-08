namespace Lugh.Scene2D
{
    public class InputListener : IEventListener
    {
        /// <summary>
        /// Try to handle the given event, if it is an <see cref="InputEvent"/>.
        /// If the input event is of type <see cref="InputEvent.Type#TouchDown"/>
        /// and <see cref="InputEvent#GetTouchFocus"/> is TRUE and <see cref="TouchDown"/>
        /// returns TRUE, indicating the event was handled, then this listener is added to
        /// the Stages touch focus via <see cref="Stage#AddTouchFocus"/> so it will receive all
        /// touch dragged events until the next touch up event.
        /// </summary>
        public bool Handle( Event ev ) => false;    // TODO:

        /// <summary>
        /// Called when a mouse button or a finger touch goes down on the actor.
        /// </summary>
        public bool TouchDown( InputEvent ev, float x, float y, int pointer ) => false;

        /// <summary>
        /// Called when a mouse button or a finger touch goes up anywhere, but only
        /// if touchDown previously returned true for the mouse button or touch. 
        /// </summary>
        public void TouchUp( InputEvent ev, float x, float y, int pointer, int button )
        {
        }

        /// <summary>
        /// Called when a mouse button or a finger touch is moved anywhere, but
        /// only if touchDown previously returned true for the  mouse button or touch.
        /// </summary>
        public void TouchDragged( InputEvent ev, float x, float y, int pointer )
        {
        }

        /// <summary>
        /// Called any time the mouse is moved when a button is NOT down.
        /// </summary>
        public bool MouseMoved( InputEvent ev, float x, float y ) => false;

        /// <summary>
        /// Called any time the mouse cursor or a finger touch is moved over an actor.
        /// </summary>
        public void Enter( InputEvent ev, float x, float y, int pointer, Actor fromActor )
        {
        }

        /// <summary>
        /// Called any time the mouse cursor or a finger touch is moved out of an actor.
        /// </summary>
        public void Exit( InputEvent ev, float x, float y, int pointer, Actor fromActor )
        {
        }

        /// <summary>
        /// Called when the mouse wheel has been scrolled.
        /// </summary>
        public bool Scrolled( InputEvent ev, float x, float y, float amountX, float amountY ) => false;

        /// <summary>
        /// Called when a key is released.
        /// </summary>
        public bool KeyUp( InputEvent ev, int keycode ) => false;

        /// <summary>
        /// Called when a key is pressed.
        /// </summary>
        public bool KeyDown( InputEvent ev, int keycode ) => false;

        /// <summary>
        /// Called when a key is typed. 
        /// </summary>
        /// <param name="character">
        /// May be 0 for key typed events that don't map to a character (ctrl, shift, etc).
        /// </param>
        public bool KeyTyped( InputEvent ev, char character ) => false;
    }
}