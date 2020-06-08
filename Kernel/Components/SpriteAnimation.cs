using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel.Components
{
    public class SpriteAnimation : Component
    {
        private SpriteRenderer spriteRenderer { get; set; }

        private int millisecondsPerFrame { get; set; }

        private int timeSinceLastFrame { get; set; }

        public Point SheetSize { get; set; }

        public Point CurrentFrame;

        public SpriteAnimation(int millisecondsPerFrame, Point sheetSize, Point currentFrame = default(Point))
        {
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.timeSinceLastFrame = 0;
            this.SheetSize = sheetSize;
            this.CurrentFrame = currentFrame;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void UnloadContent()
        {
            this.spriteRenderer = null;
        }

        public override void Update(GameTime gameTime)
        {
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
            spriteBatch.Draw(spriteRenderer.Texture,
               this.GameObject.transform.Position,
               new Rectangle(this.CurrentFrame.X * this.spriteRenderer.Size.X, 
               this.CurrentFrame.Y * this.spriteRenderer.Size.Y, 
               this.spriteRenderer.Size.X, 
               this.spriteRenderer.Size.Y),
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
            this.GameObject.Components.Remove(this);
        }
    }
}
