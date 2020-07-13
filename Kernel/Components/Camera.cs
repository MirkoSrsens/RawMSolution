using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Kernel.Components
{
    public class Camera : Component
    {
        public Matrix view { get; set; }

        public Matrix projection { get; set; }

        public Vector3 LookPoint { get; set; }

        public Vector3 Direction { get; set; }

        public MouseState prevMouseState { get; set; }

        public Vector3 cameraUp { get; set; }

        private readonly float totalYaw = MathHelper.PiOver4 / 2;
        private float currentYaw = 0;
        private readonly float totalPitch = MathHelper.PiOver4 / 2;
        private float currentPitch = 0;

        public Camera(int clipNear, int clipFar, float aspectRation)
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2,
                aspectRation,
                clipNear,
                clipFar);
        }

        public override void Destroy()
        {
        }

        public override void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            Direction = LookPoint - this.GameObject.transform.Position3D;
            Direction.Normalize();

            prevMouseState = Mouse.GetState();
            CreateLookAt();
        }

        public override void Update(GameTime gameTime)
        {
            // Yaw
            float yawAngle = (-MathHelper.PiOver4 / 1000) *
                (Mouse.GetState().X - prevMouseState.X);

            if(Math.Abs(currentYaw + yawAngle) < totalYaw)
            {
                Direction = Vector3.Transform(Direction,
                    Matrix.CreateFromAxisAngle(cameraUp, yawAngle));

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
                var directionNorm = Direction;
                directionNorm.Normalize();
                Direction = Vector3.Transform(Direction,
                    Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, directionNorm), pitchAngle));

                currentPitch += pitchAngle;
            }

            prevMouseState = Mouse.GetState();

            CreateLookAt();
        }

        public override void UpdateReferences()
        {
        }

        private void CreateLookAt()
        {
            var position = this.GameObject.transform.Position3D;
            view = Matrix.CreateLookAt(position, position + Direction, cameraUp);
        }
    }
}
