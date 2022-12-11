namespace PlatformaniaCS.Game.Scenes
{
    public abstract class BaseScene : IScene, IDisposable
    {
        protected BaseScene()
        {
        }

        public abstract void Initialise();

        public abstract void Update( GameTime gameTime );

        public abstract void Render( GameTime gameTime );

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

        void IDisposable.Dispose()
        {
        }
    }
}