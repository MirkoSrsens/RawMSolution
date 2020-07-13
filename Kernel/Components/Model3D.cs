using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Components
{
    public class Model3D : Component
    {
        public Model model { get; protected set; }

        private Camera camera { get; set; }

        protected Matrix world = Matrix.Identity;

        public Matrix rotation = Matrix.Identity;

        private float yawAngle { get; set; }

        private float pitchAngle { get; set; }

        private float rollAngle { get; set; }

        private Vector3 direction { get; set; }

        public Model3D(Model model, Camera camera)
        {
            this.model = model;
            this.camera = camera;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            rotation *= Matrix.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);

            world *= Matrix.CreateTranslation(direction);
        }

        public override void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
            var transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach(var mesh in model.Meshes)
            {
                foreach(BasicEffect basicEffects in mesh.Effects)
                {
                    basicEffects.EnableDefaultLighting();
                    basicEffects.Projection = camera.projection;
                    basicEffects.View = camera.view;
                    basicEffects.World = GetWorld() * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        protected virtual Matrix GetWorld()
        {
            return world * rotation;
        }

        public override void UpdateReferences()
        {
        }

        public override void Destroy()
        {
        }
    }
}
