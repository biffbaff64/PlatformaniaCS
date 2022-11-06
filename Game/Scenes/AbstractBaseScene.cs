using Lugh;
using PlatformaniaCS.Game.Core;

namespace PlatformaniaCS.Game.Scenes;

// TO BE REMOVED
public abstract class AbstractBaseScene : IGdxScene, IDisposable
{
    protected AbstractBaseScene()
    {
    }

    public void Resize( int width, int height )
    {
        App.BaseRenderer.ResizeCameras( width, height );
    }
 
    public void Pause()
    {
    }

    public void Resume()
    {
    }

    public abstract void Initialise();

    public abstract void Update();

    public abstract void Render( float delta );

    public abstract void Show();

    public abstract void Hide();

    public abstract void LoadImages();

    public abstract string Name();

    public abstract void Dispose();
}