using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Kernel.Components
{
    public class SpriteRenderer : Component
    {
        private string TexturePath { get; set; }

        public Texture2D Texture { get; set; }

        public Point Size { get; set; }

        public int OrderInLayer { get; set; }

        public readonly bool AutoAnimation;

        public Action<SpriteBatch> DrawAction { get; set; } 

        public SpriteRenderer(string texturePath, Point size = default(Point), int orderInLayer = 0, bool autoAnimation = false, Action<SpriteBatch> drawAction = null)
        {
            this.TexturePath = texturePath;
            this.Size = size;
            this.AutoAnimation = autoAnimation;
            this.DrawAction = drawAction;
            this.OrderInLayer = orderInLayer;
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
            if(DrawAction != null)
            {
                DrawAction(spriteBatch);
            }
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

        public override void Destroy()
        {
            this.GameObject.Components.Remove(this);
        }
    }
}
