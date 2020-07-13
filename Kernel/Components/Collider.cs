using Kernel.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Kernel.Components
{
    public abstract class Collider : Component
    {
        public Layers Layer { get; set; }

        protected static List<Collider> colliderPositions { get; set; }

        public Rectangle rect { get; set; }

        public Action<Collider> OnCollisionEnter { get; set; }

        static Collider()
        {
            colliderPositions = new List<Collider>();
        }

        public Collider(Layers layer = Layers.Layer1)
        {
            Layer = layer;
        }

        public override void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
            if (!Enabled || !Visiable) return;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;
        }

        public override void UpdateReferences()
        {
        }

        public void CheckCollision(Rectangle rect)
        {

            for(int i = colliderPositions.Count-1; i>= 0; i--)
            {
                var col = colliderPositions[i];

                if (col == this && col.Layer == this.Layer)
                {
                    continue;
                }

                if (this.rect.Intersects(col.rect))
                {
                    if (OnCollisionEnter != null)
                    {
                        OnCollisionEnter(col);
                    }
                    if (col.OnCollisionEnter != null)
                    {
                        col.OnCollisionEnter(this);
                    }
                }
            }
        }

        public override void Destroy()
        {
            colliderPositions.Remove(this);
        }
    }
}
