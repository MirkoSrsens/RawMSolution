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
    public class EnemySpawnerData
    {
        public int minSpawnTime { get; set; }
        public int maxSpawnTime { get; set; }

        public int numberEnemies { get; set; }
        public int minSpeed { get; set; }
        public int maxSpeed { get; set; }
        // Misses
        public int missesAllowed { get; set; }

        public EnemySpawnerData(int minSpawnTime, int maxSpawnTime, int numberEnemies, int minSpeed, int maxSpeed, int missesAllowed)
        {
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            this.numberEnemies = numberEnemies;
            this.minSpeed = minSpeed;
            this.maxSpeed = maxSpeed;
            this.missesAllowed = missesAllowed;
        }
    }

    public class EnemySpawner : MonoBehaviour
    {
        public Random rnd { get; set; }

        private readonly Vector3 maxSpawnLocation = new Vector3(500, 100, -3000);

        private int nextSpawnTime = 0;

        private int timeSinceLastSpawn = 0;

        private float maxRollAngle = MathHelper.Pi / 40;

        private int enemiesThisLevel = 0;

        private int missedThisLevel = 0;

        private int currentLevel = 0;

        public List<EnemySpawnerData> enemySpawnersData { get; set; }

        public List<Enemy> enemies { get; set; }

        private Camera camera { get; set; }

        public List<ParticleExplosion> particleExplosionsOfDeadEnemies { get; set; }

        public EnemySpawner(Camera camera, GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            this.camera = camera;
            this.enemies = new List<Enemy>();
            this.particleExplosionsOfDeadEnemies = new List<ParticleExplosion>();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < particleExplosionsOfDeadEnemies.Count; ++i)
            {
                // If explosion is finished, remove it
                if (particleExplosionsOfDeadEnemies[i].IsDead)
                {
                    this.gameObject.RemoveComponent(particleExplosionsOfDeadEnemies[i]);
                    particleExplosionsOfDeadEnemies.RemoveAt(i);
                    --i;
                }
            }

            if (enemiesThisLevel < enemySpawnersData[currentLevel].numberEnemies)
            {
                timeSinceLastSpawn += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastSpawn > nextSpawnTime)
                {
                    SpawnEnemy();
                }
            }
        }

        private void SetNextSpawnTime()
        {
            nextSpawnTime = rnd.Next(enemySpawnersData[currentLevel].minSpawnTime,
                enemySpawnersData[currentLevel].maxSpawnTime);
            timeSinceLastSpawn = 0;
        }

        private void SpawnEnemy()
        {
            var position = new Vector3(rnd.Next(
                -(int)maxSpawnLocation.X, (int)maxSpawnLocation.X),
                rnd.Next(-(int)maxSpawnLocation.Y, (int)maxSpawnLocation.Y), maxSpawnLocation.Z);

            var enemy = new Enemy(graphicsDevice, camera);
            enemy.model.SetPosition(position);

            enemy.model.direction = new Vector3(
                rnd.Next(-(int)maxSpawnLocation.X, (int)maxSpawnLocation.X),
                rnd.Next(-(int)maxSpawnLocation.Y, (int)maxSpawnLocation.Y),
                30000);
            enemy.model.direction = new Vector3(0, 0, rnd.Next(enemySpawnersData[currentLevel].minSpeed, enemySpawnersData[currentLevel].maxSpeed));
            // enemy.model.rollAngle = (float)rnd.NextDouble() * maxRollAngle - (maxRollAngle / 2);

            enemies.Add(enemy);

            enemiesThisLevel++;
            SetNextSpawnTime();
        }

        protected override void InitComponents()
        {
            rnd = new Random();
            enemySpawnersData = new List<EnemySpawnerData>();
            enemySpawnersData.Add(new EnemySpawnerData(1000, 3000, 20, 2, 6, 10));
            enemySpawnersData.Add(new EnemySpawnerData(900, 2800, 22, 2, 6, 9));
            enemySpawnersData.Add(new EnemySpawnerData(800, 2600, 24, 2, 6, 8));
            SetNextSpawnTime();
        }
    }
}
