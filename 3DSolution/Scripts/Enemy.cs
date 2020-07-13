using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSolution.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private Camera camera { get; set; }

        public Enemy(GraphicsDevice graphicsDevice, Camera camera)
            :this(graphicsDevice)
        {
            this.camera = camera;
        }

        public Enemy(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected override void InitComponents()
        {
            var model = new Model3D(Game1.singleton.Content.Load<Model>(@"Models\lowpoly_max"), camera);
            this.gameObject.AddComponent(model);
        }
    }
}
