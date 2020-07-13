using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DSolution.Scripts
{
    public class CameraObject : MonoBehaviour
    {
        public Camera camera { get; set; }

        ////public float speed = 0.1f;


        public CameraObject(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            ////if (Keyboard.GetState().IsKeyDown(Keys.W))
            ////    gameObject.transform.Position3D += camera.LookDirection * speed;
            ////if (Keyboard.GetState().IsKeyDown(Keys.S))
            ////    gameObject.transform.Position3D -= camera.LookDirection * speed;
            ////if (Keyboard.GetState().IsKeyDown(Keys.A))
            ////    gameObject.transform.Position3D += Vector3.Cross(Vector3.Up ,camera.LookDirection) * speed;
            ////if (Keyboard.GetState().IsKeyDown(Keys.D))
            ////    gameObject.transform.Position3D -= Vector3.Cross(Vector3.Up, camera.LookDirection) * speed;


        }

        //public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
        protected override void InitComponents()
        {
            camera = new Camera(1, 3000, (float)Game1.singleton.Window.ClientBounds.Width / (float)Game1.singleton.Window.ClientBounds.Height)
            {
                LookPoint = new Vector3(10, 100, 5),
                cameraUp = Vector3.Up,
            };
            this.gameObject.AddComponent(camera);
            camera.GameObject.transform.Position3D = new Vector3(30, 100, 20);

            Mouse.SetPosition(Game1.singleton.Window.ClientBounds.Width / 2,
             Game1.singleton.Window.ClientBounds.Height / 2);

        }
    }
}
