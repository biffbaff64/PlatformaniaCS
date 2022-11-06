
using Microsoft.Xna.Framework.Graphics;

namespace Lugh.UI
{
    public interface IDefaultUIPanel
    {
        void Open();

        void Close();

        void Initialise( TextureRegion region, string nameID, params object[] args );

        void Set( SimpleVec2F xy, SimpleVec2F distance, Direction direction, SimpleVec2F speed );

        void Setup();

        void Draw( SpriteBatch spriteBatch );

        void PopulateTable();

        void SetPosition( float x, float y );

        void ForceZoomOut();

        void SetPauseTime( int time );

        bool Update();

        bool NameExists( string nameID );

        int GetWidth();

        int GetHeight();

        StateID GetState();

        void SetState( StateID state );
    }
}