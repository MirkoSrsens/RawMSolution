using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DSolution.Scripts
{
    class CameraObject : MonoBehaviour
    {
        public CameraObject(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected override void InitComponents()
        {
            var camera = new Camera()
            {
                Target = new Vector3(0, 0, 5),
                AspectRation = (float)Game1.singleton.Window.ClientBounds.Width / (float)Game1.singleton.Window.ClientBounds.Height,
                ClipNear = 1,
                ClipFar = 100,
                LookVector = Vector3.Up
            };

            this.gameObject.AddComponent(camera);
        }
    }
}
