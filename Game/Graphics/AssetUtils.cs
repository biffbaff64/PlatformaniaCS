using Microsoft.Xna.Framework.Graphics;

using PlatformaniaCS.Game.Core;

namespace PlatformaniaCS.Game.Graphics;

public class AssetUtils : IDisposable
{
    public void Initialise()
    {
    }

    public TextureRegion GetAnimationRegion( string descriptorAsset )
    {
        return null;
    }

    public static T LoadAsset<T>( string name )
    {
        return App.MainGame.Content.Load<T>( name );
    }

    public static void UnloadAsset( string name )
    {
        App.GetContent().UnloadAsset( name );
    }
    
    public void Dispose()
    {
    }
}