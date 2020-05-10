using Kernel.Components;
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
        ChasePlayer
    }

    public class TestObject : MonoBehaviour
    {
        private Random rnd { get; set; }

        private int direction { get; set; }

        private SpriteRenderer sprRen { get; set; }

        public TypeOfEnemyMovement TypeOfMovement { get; set; }

        public Player Player { get; set; }

        public TestObject(GraphicsDevice graphicsDevice, TypeOfEnemyMovement typeOfMovement = TypeOfEnemyMovement.Random) : base(graphicsDevice)
        {
            rnd = new Random();
            direction = rnd.Next(0, 4);
            this.TypeOfMovement = typeOfMovement;
        }

        public override void Update(GameTime gameTime)
        {
            if (TypeOfMovement == TypeOfEnemyMovement.Random)
            {
                switch (direction)
                {
                    case 0:
                        gameObject.transform.Position.X++;
                        break;
                    case 1:
                        gameObject.transform.Position.X--;
                        break;
                    case 2:
                        gameObject.transform.Position.Y++;
                        break;
                    case 3:
                        gameObject.transform.Position.Y--;
                        break;
                }
            }
            else if(TypeOfMovement == TypeOfEnemyMovement.ChasePlayer)
            {
                if(Player != null)
                {
                    if (Player.gameObject.transform.Position.X < gameObject.transform.Position.X)
                        gameObject.transform.Position.X--;
                    else if (Player.gameObject.transform.Position.X > gameObject.transform.Position.X)
                        gameObject.transform.Position.X++;
                    if (Player.gameObject.transform.Position.Y < gameObject.transform.Position.Y)
                        gameObject.transform.Position.Y--;
                    else if (Player.gameObject.transform.Position.Y > gameObject.transform.Position.Y)
                        gameObject.transform.Position.Y++;
                }
                else
                {
                    Player = GameManager.singleton.FindObjectByType<Player>();
                }
            }

            if(sprRen.IsOutOfBounds(Game1.singleton.Window.ClientBounds))
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
                }
            };
            sprRen = new SpriteRenderer("Content/images/skullball.png", new Point(75, 75), true);
            gameObject.AddComponent(sprRen);
            gameObject.transform.Position = new Vector2(250, 250);
        }
    }
}
