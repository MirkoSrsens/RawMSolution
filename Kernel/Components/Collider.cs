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

        protected static Dictionary<Collider, Rectangle> colliderPositions { get; set; }

        public Action<Collider> OnCollisionEnter { get; set; }

        static Collider()
        {
            colliderPositions = new Dictionary<Collider, Rectangle>();
        }

        public Collider(Layers layer = Layers.Layer1)
        {
            Layer = layer;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void UpdateReferences()
        {
        }

        public void CheckCollision(Rectangle rect)
        {
            foreach (var col in colliderPositions)
            {
                if (col.Key == this && col.Key.Layer == this.Layer)
                {
                    continue;
                }

                if (colliderPositions[this].Intersects(col.Value))
                {
                    if (OnCollisionEnter != null)
                    {
                        OnCollisionEnter(col.Key);
                    }
                    if (col.Key.OnCollisionEnter != null)
                    {
                        col.Key.OnCollisionEnter(this);
                    }
                }
            }
        }
    }
}
