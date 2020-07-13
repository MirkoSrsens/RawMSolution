using _3DSolution;
using _3DSolution.Scripts;
using Kernel.Components;
using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Solution.Objects
{
    public class GameStateManager : DrawableGameComponent
    {
        public const int MaximumTime = 10000;

        public int timeLimit { get; set; }

        public GraphicsDevice graphicsDevice { get; set; } 

        public CameraObject camera { get; set; }

        public GameStateManager(GraphicsDevice graphicsDevice, Game game)
            : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            camera = new CameraObject(graphicsDevice);

            new Enemy(graphicsDevice, camera.camera);
        }

        public void StartGame()
        {
        }

        public void GameOver()
        {
            GameLoop.DestroyAll();
        }

        public override void Update(GameTime gameTime)
        {
            GameLoop.UpdateObjects(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameLoop.Draw(camera.camera);
            base.Draw(gameTime);
        }
    }
}
