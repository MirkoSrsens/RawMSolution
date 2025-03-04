﻿using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RawMSolution.Objects.Enums;

namespace RawMSolution.Objects
{
    public class GameStateManager
    {
        public GameState state { get; set; }

        public MainMenu mainMenu { get; set; }

        public TimerDisplay gameOverScreen { get; set; }

        public const int MaximumTime = 10000;

        public int timeLimit { get; set; }

        public GraphicsDevice graphicsDevice { get; set; } 

        public GameStateManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            mainMenu = new MainMenu(graphicsDevice);
        }

        public void StartGame()
        {
            mainMenu.Destroy();
            state = GameState.InGame;
            new ScoreBox(graphicsDevice);
            new Player(graphicsDevice);
            new EnemySpawner(graphicsDevice);
            new Background(graphicsDevice);
            new TimerDisplay(graphicsDevice);
        }

        public void GameOver()
        {
            GameLoop.DestroyAll();
            new GameOverScreen(graphicsDevice);
            state = GameState.GameOver;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            GameLoop.Draw(spriteBatch);
        }
    }
}
