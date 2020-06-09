using Kernel.Enums;
using Microsoft.Xna.Framework;

namespace Kernel.Components
{
    public class BoxCollider2D : Collider
    {
        public int OffsetX { get; set; }

        public int OffsetY { get; set; }

        private SpriteRenderer spriteRenderer { get; set; }

        public BoxCollider2D(Layers layer = 0, int offsetX = 0, int offsetY= 0)
            : base(layer)
        {
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
            colliderPositions.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;

            var newRect = new Rectangle((int)this.GameObject.transform.Position.X + OffsetX,
                                 (int)this.GameObject.transform.Position.Y + OffsetY,
                                 spriteRenderer.Size.X - (OffsetX * 2),
                                 spriteRenderer.Size.Y - (OffsetY * 2));

            if(newRect != rect)
            {
                rect = newRect;
                CheckCollision(newRect);
            }
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
