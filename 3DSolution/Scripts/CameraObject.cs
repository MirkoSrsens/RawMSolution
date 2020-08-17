using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _3DSolution.Scripts
{
    public class CameraObject : MonoBehaviour
    {
        public Camera camera1 { get; set; }
        public Camera camera2 { get; set; }

        ////public float speed = 0.1f;



        public MouseState prevMouseState { get; set; }
        private readonly float totalYaw = MathHelper.PiOver4 / 2;
        private float currentYaw = 0;
        private readonly float totalPitch = MathHelper.PiOver4 / 2;
        private float currentPitch = 0;

        private SpriteRenderer spriteRenderer { get; set; }

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

            // Yaw
            float yawAngle = (-MathHelper.PiOver4 / 1000) *
                (Mouse.GetState().X - prevMouseState.X);

            if (Math.Abs(currentYaw + yawAngle) < totalYaw)
            {
                camera1.Direction = Vector3.Transform(camera1.Direction,
                    Matrix.CreateFromAxisAngle(camera1.cameraUp, yawAngle));

                currentYaw += yawAngle;
            }

            ///ROLL
            //cameraUp = Vector3.Transform(cameraUp,
            //    Matrix.CreateFromAxisAngle(LookDirection, (MathHelper.PiOver4 / 45)));

            /// Pitch

            float pitchAngle = (MathHelper.PiOver4 / 1500) *
                (Mouse.GetState().Y - prevMouseState.Y);

            if (Math.Abs(currentPitch + pitchAngle) < totalPitch)
            {
                var directionNorm = camera1.Direction;
                directionNorm.Normalize();
                camera1.Direction = Vector3.Transform(camera1.Direction,
                    Matrix.CreateFromAxisAngle(Vector3.Cross(camera1.cameraUp, directionNorm), pitchAngle));

                currentPitch += pitchAngle;
            }

            prevMouseState = Mouse.GetState();

        }

        //public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
        protected override void InitComponents()
        {
            var vp1 = graphicsDevice.Viewport;

            vp1.Height = (graphicsDevice.Viewport.Height / 2);

            camera1 = new Camera(1, 3000, vp1)
            {
                LookPoint = new Vector3(30, 100, 5),
                cameraUp = Vector3.Up,
            };

            this.gameObject.AddComponent(camera1);
            camera1.GameObject.transform.Position3D = new Vector3(30, 100, 20);

            prevMouseState = Mouse.GetState();
            Mouse.SetPosition(Game1.singleton.Window.ClientBounds.Width / 2,
            Game1.singleton.Window.ClientBounds.Height / 2);

            spriteRenderer = new SpriteRenderer(@"Content/images/crosshair.png");

            spriteRenderer.DrawAction = new Action<SpriteBatch>((a) =>
            {
                a.Draw(spriteRenderer.Texture,
                     new Vector2((Game1.singleton.Window.ClientBounds.Width / 2)
                     - (spriteRenderer.Texture.Width / 2),
                     (Game1.singleton.Window.ClientBounds.Height / 2)
                     - (spriteRenderer.Texture.Height / 2)),
                     Color.White);
            });

            this.gameObject.AddComponent(spriteRenderer);

        }
    }
}
