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

        public Point NumberOfSprites { get; set; }

        public int OrderInLayer { get; set; }

        public readonly bool AutoAnimation;

        public Action<SpriteBatch> DrawAction { get; set; }

        public SpriteRenderer(string texturePath, Point numberOfSprites = default(Point), int orderInLayer = 0, Action<SpriteBatch> drawAction = null)
        {
            this.TexturePath = texturePath;

            if (numberOfSprites == default(Point))
            {
                this.NumberOfSprites = new Point(1, 1);
            }
            else
            {
                this.NumberOfSprites = numberOfSprites;
            }
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

        public override void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
            if (!Enabled || !Visiable) return;

            if (DrawAction != null)
            {
                DrawAction(spriteBatch);
            }
            spriteBatch.Draw(Texture,
               this.GameObject.transform.Position2D,
               new Rectangle(0,0,NumberOfSprites.X,NumberOfSprites.Y),
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

            if (this.GameObject.transform.Position2D.X < -NumberOfSprites.X ||
            this.GameObject.transform.Position2D.X > clientRect.Width ||
            this.GameObject.transform.Position2D.Y < -NumberOfSprites.Y ||
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
