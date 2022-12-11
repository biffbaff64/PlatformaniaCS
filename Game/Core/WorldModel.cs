// ##################################################

using System.Collections.Generic;

using Box2DSharp.Dynamics;

using PlatformaniaCS.Game.Graphics;
using PlatformaniaCS.Game.Physics;

using Vector2 = System.Numerics.Vector2;

// ##################################################

namespace PlatformaniaCS.Game.Core
{
    public class WorldModel : IDisposable
    {
        public World                     Box2DWorld           { get; private set; }
        public Box2DWorldContactListener Box2DContactListener { get; private set; }
        public BodyBuilder               BodyBuilder          { get; private set; }
        public List<PhysicsBody>         BodiesList           { get; private set; }
        public bool                      WorldStepEnabled     { get; set; }
        public bool                      CanDrawBebug         { get; set; }

        public WorldModel()
        {
        }

        public void CreateWorld()
        {
            Box2DWorld = new World
                (
                 new Vector2
                     (
                      Gfx.WorldGravity.X * Gfx.PPM,
                      Gfx.WorldGravity.Y * Gfx.PPM
                     )
                );

            BodyBuilder          = new BodyBuilder();
            Box2DContactListener = new Box2DWorldContactListener();
            BodiesList           = new List<PhysicsBody>();

            Box2DWorld.SetContactListener( Box2DContactListener );

            WorldStepEnabled = true;
            CanDrawBebug     = false;
        }

        public void WorldStep()
        {
            if ( WorldStepEnabled && ( Box2DWorld != null ) && !LughSystem.Inst().GamePaused )
            {
                Box2DWorld.Step
                    (
                     B2DConstants.StepTime,
                     B2DConstants.VelocityIterations,
                     B2DConstants.PositionIterations
                    );
            }

            TidyDeletionList();
        }

        public void CreateB2DRenderer()
        {
        }

        public void DrawDebugMatrix()
        {
        }

        /// <summary>
        /// If any PhysicsBody#isAlive is FALSE then destroy the associated
        /// Box2D Body. The PhysicsBody should remain intact, allowing for
        /// a new body to be allocated if required.
        /// </summary>
        public void TidyDeletionList()
        {
        }

        public void Activate()
        {
            WorldStepEnabled = true;
        }

        public void DeActivate()
        {
            WorldStepEnabled = false;
        }

        public void Dispose()
        {
        }
    }
}