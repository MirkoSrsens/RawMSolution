using _3DSolution;
using _3DSolution.Scripts;
using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Solution.Objects
{
    public class GameStateManager : DrawableGameComponent
    {
        public GraphicsDevice graphicsDevice { get; set; } 

        public CameraObject camera { get; set; }

        public EnemySpawner enemySpawner { get; set; }

        public GameStateManager(GraphicsDevice graphicsDevice, Game game)
            : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            camera = new CameraObject(graphicsDevice);
            enemySpawner = new EnemySpawner(camera.camera1, graphicsDevice);
            new Bullets(enemySpawner, camera, graphicsDevice);

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
            GameLoop.Draw(Game1.singleton.spriteBatch, camera.camera1);
            base.Draw(gameTime);
        }

    }
}
