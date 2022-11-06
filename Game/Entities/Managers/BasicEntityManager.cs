
namespace PlatformaniaCS.Game.Entities.Managers
{
    public class BasicEntityManager : IEntityManagerComponent
    {
        private readonly GraphicID _managerID;
        private readonly GraphicID _graphicID;
        private          int       _activeCount;
        private          int       _maxCount;

        protected BasicEntityManager()
        {
            this._graphicID = GraphicID.G_NO_ID;
            this._managerID = GraphicID.G_NO_ID;
        }

        public BasicEntityManager( GraphicID graphicID )
        {
            this._graphicID = graphicID;
            this._managerID = graphicID;
        }

        public void Init()
        {
            Reset();
        }

        public void Update()
        {
        }

        public void Create()
        {
        }

        public void Free()
        {
            _activeCount = Math.Max( 0, _activeCount - 1 );
        }

        public void Reset()
        {
            _activeCount = 0;
            _maxCount    = 1;
        }

        public int GetActiveCount()
        {
            return _activeCount;
        }

        public void SetActiveCount( int numActive )
        {
            _activeCount = numActive;
        }

        public void AddMaxCount( int add )
        {
            _maxCount += add;
        }

        public void SetMaxCount( int max )
        {
            _maxCount = max;
        }

        public GraphicID GetGID()
        {
            return _graphicID;
        }

        public string GetName()
        {
            return nameof( _graphicID );
        }
    }
}