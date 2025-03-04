﻿using Kernel.Components;
using Kernel.Design;
using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RawMSolution.Objects
{
    public enum TypeOfEnemyMovement
    {
        Random,
        ChasePlayer,
        Evade
    }

    public class TestObject : MonoBehaviour
    {
        private Random rnd { get; set; }

        private int direction { get; set; }

        private SpriteAnimation sprAnim { get; set; }

        public TypeOfEnemyMovement TypeOfMovement { get; set; }

        public Player Player { get; set; }

        public int ScoreValue { get; set; }

        public TestObject(GraphicsDevice graphicsDevice, int scoreValue, TypeOfEnemyMovement typeOfMovement = TypeOfEnemyMovement.Random) : base(graphicsDevice)
        {
            rnd = new Random();
            direction = rnd.Next(0, 4);
            this.TypeOfMovement = typeOfMovement;
            this.ScoreValue = scoreValue;
        }

        public override void Update(GameTime gameTime)
        {
            if (TypeOfMovement == TypeOfEnemyMovement.Random)
            {
                switch (direction)
                {
                    case 0:
                        gameObject.transform.Position2D.X++;
                        break;
                    case 1:
                        gameObject.transform.Position2D.X--;
                        break;
                    case 2:
                        gameObject.transform.Position2D.Y++;
                        break;
                    case 3:
                        gameObject.transform.Position2D.Y--;
                        break;
                }
            }
            else if(TypeOfMovement == TypeOfEnemyMovement.ChasePlayer)
            {
                if(Player != null)
                {
                    if (Player.gameObject.transform.Position2D.X < gameObject.transform.Position2D.X)
                        gameObject.transform.Position2D.X--;
                    else if (Player.gameObject.transform.Position2D.X > gameObject.transform.Position2D.X)
                        gameObject.transform.Position2D.X++;
                    if (Player.gameObject.transform.Position2D.Y < gameObject.transform.Position2D.Y)
                        gameObject.transform.Position2D.Y--;
                    else if (Player.gameObject.transform.Position2D.Y > gameObject.transform.Position2D.Y)
                        gameObject.transform.Position2D.Y++;
                }
                else
                {
                    Player = GameManager.singleton.FindObjectByType<Player>();
                }
            }
            else if (TypeOfMovement == TypeOfEnemyMovement.Evade)
            {
                if (Player != null)
                {
                    if (Player.gameObject.transform.Position2D.X < gameObject.transform.Position2D.X)
                        gameObject.transform.Position2D.X++;
                    else if (Player.gameObject.transform.Position2D.X > gameObject.transform.Position2D.X)
                        gameObject.transform.Position2D.X--;
                    if (Player.gameObject.transform.Position2D.Y < gameObject.transform.Position2D.Y)
                        gameObject.transform.Position2D.Y++;
                    else if (Player.gameObject.transform.Position2D.Y > gameObject.transform.Position2D.Y)
                        gameObject.transform.Position2D.Y--;
                }
                else
                {
                    Player = GameManager.singleton.FindObjectByType<Player>();
                }
            }

            if (sprAnim.IsOutOfBounds(Game1.singleton.Window.ClientBounds))
            {
                Destroy();
            }
        }

        protected override void InitComponents()
        {
            var audioSource = new AudioSource(@"Content/sound/collected.wav");
            audioSource.SoundInstance.IsLooped = false;
            gameObject.AddComponent(audioSource);
            gameObject.AddComponent(new BoxCollider2D(0, 10, 10));
            gameObject.GetComponent<BoxCollider2D>().OnCollisionEnter = a =>
            {
                if (a.GameObject.Name == nameof(Player))
                {
                    audioSource.Play();
                    Destroy();
                    Manager.TotalScore += 100;
                }
            };
            sprAnim = new SpriteAnimation(50, "Content/images/skullball.png", new Point(0, 0), new Point(75, 75), 1, true);
            gameObject.AddComponent(sprAnim);
            gameObject.transform.Position2D = new Vector2(250, 250);
        }
    }
}
