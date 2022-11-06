
namespace PlatformaniaCS.Game.Entities.Managers
{
    public interface IEntityManagerComponent
    {
        void Init();

        void Update();

        void Create();

        void Free();

        void Reset();

        int GetActiveCount();

        void SetActiveCount( int numActive );

        void AddMaxCount( int add );

        void SetMaxCount( int max );

        GraphicID GetGID();

        string GetName();
    }
}