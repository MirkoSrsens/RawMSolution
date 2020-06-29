using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DSolution.Scripts
{
    public class CameraObject : MonoBehaviour
    {

        public Camera camera { get; set; }

        public CameraObject(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected override void InitComponents()
        {
            camera = new Camera()
            {
                Target = Vector3.Zero,
                AspectRation = (float)Game1.singleton.Window.ClientBounds.Width / (float)Game1.singleton.Window.ClientBounds.Height,
                ClipNear = 1,
                ClipFar = 3000,
                LookVector = Vector3.Up,
            };
            this.gameObject.AddComponent(camera);
            camera.GameObject.transform.Position3D = new Vector3(0, 0, 5);

        }
    }
}
