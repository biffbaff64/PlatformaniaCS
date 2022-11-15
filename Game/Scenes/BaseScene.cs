namespace PlatformaniaCS.Game.Scenes;

public abstract class BaseScene : IScene, IDisposable
{
    public BaseScene()
    {
    }

    public abstract void Initialise();

    public abstract void Update();

    public abstract void Render( float delta );

    public abstract void Show();

    public abstract void Hide();

    public abstract string Name();

    public void Resize( int width, int height )
    {
    }

    public void Pause()
    {
    }

    public void Resume()
    {
    }

    void IScene.Dispose()
    {
    }

    void IDisposable.Dispose()
    {
    }
}