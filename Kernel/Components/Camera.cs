using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Kernel.Components
{
    public class Camera : Component
    {
        public Matrix view { get; set; }

        public Viewport viewPort { get; set; }

        public Matrix projection { get; set; }

        public Vector3 LookPoint { get; set; }

        public Vector3 Direction { get; set; }

        public Vector3 cameraUp { get; set; }

        public Camera(int clipNear, int clipFar, Viewport viewport)
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2,
                (float)viewport.Width
                /(float)viewport.Height,
                clipNear,
                clipFar);

            this.viewPort = viewport;
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
            CreateLookAt();
        }

        public override void Update(GameTime gameTime)
        {
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
