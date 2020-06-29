using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Kernel.Components
{
    public class SpriteAnimation : SpriteRenderer
    {
        private SpriteRenderer spriteRenderer { get; set; }

        private int millisecondsPerFrame { get; set; }

        private int timeSinceLastFrame { get; set; }

        public Point SheetSize { get; set; }

        public Point CurrentFrame;

        public SpriteAnimation(
            int millisecondsPerFrame, 
            string texturePath,
            Point currentFrame = default(Point), 
            Point size = default(Point), 
            int orderInLayer = 0,
            bool autoAnimation = false, 
            Action<SpriteBatch> drawAction = null)
            : base(texturePath, size, orderInLayer, drawAction)
        {
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.timeSinceLastFrame = 0;
            this.CurrentFrame = currentFrame;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            base.LoadContent(graphicsDevice);
            this.SheetSize = new Point(Texture.Width / NumberOfSprites.X, Texture.Height / NumberOfSprites.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;

            this.timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

                if (this.timeSinceLastFrame > this.millisecondsPerFrame)
                {
                    this.timeSinceLastFrame -= this.millisecondsPerFrame;
                    this.CurrentFrame.X += 1;

                    if (this.CurrentFrame.X >= this.SheetSize.X)
                    {
                        this.CurrentFrame.X = 0;
                        ++this.CurrentFrame.Y;

                        if (this.CurrentFrame.Y >= this.SheetSize.Y)
                        {
                            this.CurrentFrame.Y = 0;
                        }
                    }
                }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Enabled || !Visiable) return;

            spriteBatch.Draw(spriteRenderer.Texture,
               this.GameObject.transform.Position2D,
               new Rectangle(this.CurrentFrame.X * this.spriteRenderer.NumberOfSprites.X, 
               this.CurrentFrame.Y * this.spriteRenderer.NumberOfSprites.Y, 
               this.spriteRenderer.NumberOfSprites.X, 
               this.spriteRenderer.NumberOfSprites.Y),
               Color.White,
               0,
               Vector2.Zero,
               1,
               SpriteEffects.None,
               spriteRenderer.OrderInLayer);
        }

        public override void UpdateReferences()
        {
            spriteRenderer = this.GameObject.GetComponent<SpriteRenderer>();
        }

        public override void Destroy()
        {
            base.Destroy();
            this.spriteRenderer = null;
        }
    }
}
