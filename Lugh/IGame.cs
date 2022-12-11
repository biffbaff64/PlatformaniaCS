namespace Lugh
{
    public interface IGame
    {
        void Initialise();

        void LoadContent();

        void Update( float gameTime );

        void Draw();

        void SetScene( IScene scene );
    }
}