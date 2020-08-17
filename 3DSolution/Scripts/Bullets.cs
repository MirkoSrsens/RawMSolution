using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSolution.Scripts
{
    public class Bullets : MonoBehaviour
    {
        public List<Model3D> bullets { get; set; }
        float shotMinZ = -3000;

        private CameraObject cameraObject { get; set; }

        private EnemySpawner enemySpawner { get; set; }

        private float shotSpeed = 10;

        private float shotDelay = 300;

        private float shootCountdown = 0;

        public Bullets(EnemySpawner enemySpawner, CameraObject cameraObject, GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            this.cameraObject = cameraObject;
            bullets = new List<Model3D>();
            this.enemySpawner = enemySpawner;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < bullets.Count; ++i)
            {
                if (bullets[i].GetWorld().Translation.Z < shotMinZ)
                {
                    var destroyBullet = bullets[i];
                    bullets.RemoveAt(i);
                    this.gameObject.RemoveComponent(destroyBullet);
                    i--;
                }
                else
                {
                    for(int j =0; j< enemySpawner.enemies.Count(); j++)
                    {
                        var enemy = enemySpawner.enemies[j];
                        if (bullets[i].IsColiding(enemy.model.model, enemy.model.GetWorld()))
                        {
                            var particle = new ParticleExplosion(graphicsDevice, enemy.model.GetWorld().Translation,
                                enemySpawner.rnd.Next(enemy.particleExplosionSettings.minLife, enemy.particleExplosionSettings.maxLife),
                                enemySpawner.rnd.Next(enemy.particleExplosionSettings.minRoundTime, enemy.particleExplosionSettings.maxRoundTime),
                                enemySpawner.rnd.Next(enemy.particleExplosionSettings.minParticlesPerRound, enemy.particleExplosionSettings.maxParticlesPerRound),
                                enemySpawner.rnd.Next(enemy.particleExplosionSettings.minParticles, enemy.particleExplosionSettings.maxParticles),
                                enemy.explosionColorsTexture, enemy.particleSettings, enemy.explosionEffect);

                            this.enemySpawner.gameObject.AddComponent(particle);

                            enemySpawner.enemies[j].Destroy();
                            var destroyBullet = bullets[i];
                            bullets.RemoveAt(i);
                            this.gameObject.RemoveComponent(destroyBullet);
                            i--;

                            
                            break;
                        }
                    }
                }
            }

            if (shootCountdown <= 0)
            {
                Shoot();
            }
            else
            {
                shootCountdown -= gameTime.ElapsedGameTime.Milliseconds;
            }


        }

        protected override void InitComponents()
        {
        }

        private void Shoot()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                var bullet = new Model3D(Game1.singleton.Content.Load<Model>(@"Models\lowpoly_max"), null, this.gameObject.transform.Position3D);
                this.gameObject.AddComponent(bullet);

                bullet.SetPosition(cameraObject.camera1.GameObject.transform.Position3D + new Vector3(0, -5, 0));
                bullet.direction = cameraObject.camera1.Direction * shotSpeed;

                bullets.Add(bullet);
                shootCountdown = shotDelay;
            }
        }
    }
}
