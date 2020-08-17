using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _3DSolution.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private Camera camera { get; set; }

        public Model3D model { get; set; }

        public ParticleExplosionSettings particleExplosionSettings = new ParticleExplosionSettings();
        public ParticleSettings particleSettings = new ParticleSettings();
        public Texture2D explosionTexture;
        public Texture2D explosionColorsTexture;
        public Effect explosionEffect;

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
            
            if (model.GetWorld().Translation.Z > camera.GameObject.transform.Position3D.Z + 100)
            {
               this.Destroy();
            }
        }

        protected override void InitComponents()
        {
            model = new Model3D(Game1.singleton.Content.Load<Model>(@"Models\lowpoly_max"), camera, this.gameObject.transform.Position3D);
            this.gameObject.AddComponent(model);

            var spriteRenderer = new SpriteRenderer(@"Content/images/crosshair.png");
            spriteRenderer.LoadContent(graphicsDevice);
            explosionTexture = spriteRenderer.Texture;
            explosionColorsTexture = spriteRenderer.Texture;
            explosionEffect = new BasicEffect(graphicsDevice);
        }
    }
}
