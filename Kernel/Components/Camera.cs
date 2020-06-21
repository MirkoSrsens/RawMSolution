using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel.Components
{
    public class Camera : Component
    {
        public Matrix view { get; set; }

        public Matrix projection { get; set; }

        public Vector3 Target { get; set; }

        public float AspectRation { get; set; }

        public int ClipNear { get; set; }

        public int ClipFar { get; set; }

        public Vector3 LookVector { get; set; }

        public override void Destroy()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            view = Matrix.CreateLookAt(this.GameObject.transform.Position3D, Target, LookVector);

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2,
                AspectRation,
                ClipNear, 
                ClipFar);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void UpdateReferences()
        {
        }
    }
}
