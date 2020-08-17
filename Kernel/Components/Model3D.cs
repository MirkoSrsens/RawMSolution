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

        public Matrix world = Matrix.Identity;

        public Matrix rotation = Matrix.Identity;

        public float yawAngle { get; set; }

        public float pitchAngle { get; set; }

        public float rollAngle { get; set; }

        public Vector3 direction { get; set; }

        public Model3D(Model model, Camera camera,Vector3 position = default(Vector3))
        {
            this.model = model;
            this.world = Matrix.CreateTranslation(position);
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;

            rotation *= Matrix.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);

            world *= Matrix.CreateTranslation(direction);
        }

        public override void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
            if (!Enabled || !Visiable) return;

            var transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (var mesh in model.Meshes)
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

        public virtual Matrix GetWorld()
        {
            return rotation * world;
        }

        public override void UpdateReferences()
        {
        }

        public override void Destroy()
        {
            Enabled = false;
            Visiable = false;
        }

        public void SetPosition(Vector3 position)
        {
            this.GameObject.transform.Position3D = position;
            world = Matrix.CreateTranslation(position);
        }

        public bool IsColiding(Model otherModel, Matrix otherWorld)
        {
            foreach(var thisModel in model.Meshes)
            {
                foreach(var otherMesh in otherModel.Meshes)
                {
                    if (thisModel.BoundingSphere.Transform(GetWorld()).
                        Intersects(otherMesh.BoundingSphere.Transform(otherWorld)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
