using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel
{
    public abstract class Component
    {
        public GameObject GameObject { get; set; }

        public abstract void LoadContent(GraphicsDevice graphicsDevice);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void UpdateReferences();
    }
}