using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RawMSolution.Objects.Enums;

namespace RawMSolution.Objects
{
    public class GameStateManager
    {
        public GameState state;

        public GameStateManager(GraphicsDevice graphicsDevice)
        {
            new ScoreBox(graphicsDevice);
            new Player(graphicsDevice);
            new EnemySpawner(graphicsDevice);
            new Background(graphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Start:
                    break;
                case GameState.InGame:
                    GameLoop.UpdateObjects(gameTime);
                    break;
                case GameState.GameOver:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case GameState.Start:
                    break;
                case GameState.InGame:
                    GameLoop.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    break;
            }
        }
    }
}
