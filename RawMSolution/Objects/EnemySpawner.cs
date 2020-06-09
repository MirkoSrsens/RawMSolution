using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RawMSolution.Objects
{
    public class EnemySpawner : MonoBehaviour
    {
        private Random rand { get; set; }

        private int enemySpawnMinMilliseconds = 300;

        private int enemySpawnMaxMilliseconds = 1500;

        private int nextSpawnTime = 0;

        int enemyMinSpeed = 2;
        int enemyMaxSpeed = 6;

        int numberOfSpawns = 100;

        public EnemySpawner(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            this.rand = new Random();
        }

        public override void Update(GameTime gameTime)
        {
            if (numberOfSpawns > 0)
            {
                nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                if (nextSpawnTime < 0)
                {
                    var test = new TestObject(graphicsDevice,100, TypeOfEnemyMovement.Evade);
                    nextSpawnTime = rand.Next(enemySpawnMinMilliseconds, enemySpawnMaxMilliseconds);

                    var x = rand.Next(0, 1000);
                    var y = rand.Next(0, 1000);
                    test.gameObject.transform.Position = new Vector2(x, y);
                    numberOfSpawns--;
                }
            }
        }

        protected override void InitComponents()
        {
        }
    }
}
