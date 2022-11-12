namespace Lugh;

public interface IScene
{
    /// <summary>
    /// Called when the scene should initialise itself.
    /// </summary>
    void Initialise();

    /// <summary>
    /// Called when the scene should update itself.
    /// </summary>
    void Update();

    /// <summary>
    /// Called when the scene should render itself.
    /// </summary>
    /// <param name="delta">The time in seconds since the last render.</param>
    void Render( float delta );

    /// <summary>
    /// Called when the application is resized. This can happen at any
    /// point during a non-paused state but will never happen before a
    /// call to #create(). 
    /// </summary>
    /// <param name="width">the new width in pixels</param>
    /// <param name="height">the new height in pixels</param>
    void Resize( int width, int height );

    /// <summary>
    /// Called when the application is paused, usually when it's not
    /// active or visible on-screen. An Application is also paused
    /// before it is destroyed.
    /// </summary>
    void Pause();

    /// <summary>
    /// Called when the application is resumed from a paused state, usually
    /// when it regains focus.
    /// </summary>
    void Resume();

    /// <summary>
    /// Called when this scene becomes the current scene
    /// for a <see cref="GdxGame"/>
    /// </summary>
    void Show();

    /// <summary>
    /// Called when this scene is no longer the current scene for
    /// a <see cref="GdxGame"/>
    /// </summary>
    void Hide();

    /// <summary>
    /// Returns the name of this scene.
    /// </summary>
    string Name();

    /// <summary>
    /// Called when this scene should release all resources.
    /// </summary>
    void Dispose();
}