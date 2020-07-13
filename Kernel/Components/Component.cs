using Kernel.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel
{
    public abstract class Component
    {
        public Component()
        {
            Enabled = true;
            Visiable = true;
        }

        public GameObject GameObject { get; set; }

        public bool Enabled { get; set; }

        public bool Visiable { get; set; }

        public abstract void LoadContent(GraphicsDevice graphicsDevice);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch = null, Camera camera = null);

        public abstract void UpdateReferences();

        public abstract void Destroy();
    }
}