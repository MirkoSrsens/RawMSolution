using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel.Components
{
    public class Transform : Component
    {
        public Vector2 Position2D;

        public Vector3 Position3D;

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

        public override void Destroy()
        {
            this.GameObject.Components.Remove(this);
        }
    }
}
