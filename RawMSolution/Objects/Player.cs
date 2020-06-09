using Kernel;
using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace RawMSolution.Objects
{
    public class Player : MonoBehaviour
    {
        public float speed = 6;

        public AudioSource defaultSound { get; set; }

        public Player(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputController.GetKeyDown(Keys.A))
            {
                gameObject.transform.Position.X -= speed;
            }
            if (InputController.GetKeyDown(Keys.D))
            {
                gameObject.transform.Position.X += speed;
            }
            if (InputController.GetKeyDown(Keys.W))
            {
                gameObject.transform.Position.Y -= speed;
            }
            if (InputController.GetKeyDown(Keys.S))
            {
                gameObject.transform.Position.Y += speed;
            }

            if (InputController.IsMouseActive())
            {
                gameObject.transform.Position = InputController.GetMousePosition();
            }

            var position = InputController.GetGamePadLeftStickPosition();
            if (InputController.GetGamePadKeyDown(Buttons.A))
            {
                gameObject.transform.Position.X += speed * 2 * position.X;
                gameObject.transform.Position.Y -= speed * 2 * position.Y;
                GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
            }
            else
            {
                gameObject.transform.Position.X += speed * position.X;
                gameObject.transform.Position.Y -= speed * position.Y;
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }
        }

        protected override void InitComponents()
        {
            gameObject.AddComponent(new BoxCollider2D(0, 10, 10));
            //gameObject.AddComponent(new AudioSource(@"Content/sound/bgmusic.wav"));
            gameObject.AddComponent(new SpriteAnimation(50, @"Content/images/threerings.png",new Point(0,0), new Point(75, 75), 1, true));
            gameObject.transform.Position = new Vector2(100, 100);

            //var audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.SoundInstance.IsLooped = true;
            //audioSource.Play();

            //gameObject.GetComponent<BoxCollider2D>().OnCollisionEnter += a => method;
        }
    }
}
