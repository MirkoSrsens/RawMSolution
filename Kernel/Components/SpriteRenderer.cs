using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Kernel.Components
{
    public class SpriteRenderer : Component
    {
        protected string TexturePath { get; set; }

        public Texture2D Texture { get; set; }

        public Point Size { get; set; }

        public int OrderInLayer { get; set; }

        public readonly bool AutoAnimation;

        public Action<SpriteBatch> DrawAction { get; set; } 

        public SpriteRenderer(string texturePath, Point size = default(Point), int orderInLayer = 0, Action<SpriteBatch> drawAction = null)
        {
            this.TexturePath = texturePath;
            this.Size = size;
            this.DrawAction = drawAction;
            this.OrderInLayer = orderInLayer;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            using (var stream = new FileStream(TexturePath, FileMode.Open))
            {
                Texture = Texture2D.FromStream(graphicsDevice, stream);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Enabled || !Visiable) return;

            if (DrawAction != null)
            {
                DrawAction(spriteBatch);
            }
            spriteBatch.Draw(Texture,
               this.GameObject.transform.Position2D,
               new Rectangle(0,0,Size.X,Size.Y),
               Color.White,
               0,
               Vector2.Zero,
               1,
               SpriteEffects.None,
               OrderInLayer);
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

            if (this.GameObject.transform.Position2D.X < -Size.X ||
            this.GameObject.transform.Position2D.X > clientRect.Width ||
            this.GameObject.transform.Position2D.Y < -Size.Y ||
            this.GameObject.transform.Position2D.Y > clientRect.Height)
            {
                return true;
            }

            return false;
        }

        public override void Destroy()
        {
            this.Texture = null;
        }
    }
}
