using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Kernel.Components
{
    public class SpriteRenderer : Component
    {
        public string TexturePath { get; set; }

        public Texture2D Texture { get; set; }

        public Point Size { get; set; }

        public readonly bool AutoAnimation;

        public SpriteRenderer(string texturePath, Point size, bool autoAnimation = false)
        {
            this.TexturePath = texturePath;
            this.Size = size;
            this.AutoAnimation = autoAnimation;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            using (var stream = new FileStream(TexturePath, FileMode.Open))
            {
                Texture = Texture2D.FromStream(graphicsDevice, stream);
            }

            if (AutoAnimation)
            {
                var x = Texture.Width / Size.X;
                var y = Texture.Height / Size.Y;
                this.GameObject.AddComponent(new SpriteAnimation(GameLoop.DefaultMillisecondsPerFrame, new Point(x, y)));
            }
        }

        public override void UnloadContent()
        {
            this.Texture = null;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void UpdateReferences()
        {
        }

        public bool IsOutOfBounds(Rectangle clientRect)
        {
            if(this.GameObject == null)
            {
                return true;
            }

            if (this.GameObject.transform.Position.X < -Size.X ||
            this.GameObject.transform.Position.X > clientRect.Width ||
            this.GameObject.transform.Position.Y < -Size.Y ||
            this.GameObject.transform.Position.Y > clientRect.Height)
            {
                return true;
            }

            return false;
        }
    }
}
