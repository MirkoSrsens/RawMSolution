using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel.Design
{
    public abstract class MonoBehaviour
    {
        public GameObject gameObject;

        protected GraphicsDevice graphicsDevice;

        protected MonoBehaviour(GraphicsDevice graphicsDevice)
        {
            this.gameObject = new GameObject(this.GetType().Name);
            this.graphicsDevice = graphicsDevice;
            GameManager.singleton.gameObjects.Add(this);
            this.LoadContent();
        }

        protected abstract void InitComponents();

        private void LoadContent()
        {
            this.InitComponents();
            GameLoop.WaitingForAdding.Enqueue(this);
            this.gameObject.LoadContent(graphicsDevice);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Destroy()
        {
            GameLoop.Destroy(this);
            GameManager.singleton.gameObjects.Remove(this);
            this.gameObject.Destroy();
        }
    }
}
