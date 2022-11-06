namespace PlatformaniaCS.Game.UI;

public interface IUIPage
{
    void Initialise();

    bool Update();

    void Show();

    void Hide();

    void Draw();

    void Dispose();
}