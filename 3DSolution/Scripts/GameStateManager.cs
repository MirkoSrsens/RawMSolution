using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solution.Objects
{
    public class GameStateManager
    {
        public const int MaximumTime = 10000;

        public int timeLimit { get; set; }

        public GraphicsDevice graphicsDevice { get; set; } 

        public GameStateManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void StartGame()
        {
        }

        public void GameOver()
        {
            GameLoop.DestroyAll();
        }

        public void Update(GameTime gameTime)
        {
            timeLimit += gameTime.ElapsedGameTime.Milliseconds;

            if (MaximumTime - timeLimit < 0)
            {
                GameOver();
            }

            GameLoop.UpdateObjects(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            GameLoop.Draw(gameTime);
        }
    }
}
