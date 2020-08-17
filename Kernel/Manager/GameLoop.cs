using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Kernel.Manager
{
    public static class GameLoop
    {
        public static List<MonoBehaviour> MonoBehaviours { get; set; }

        public static Queue<MonoBehaviour> WaitingForAdding { get; set; }

        public static Queue<MonoBehaviour> WaitingForDestruction { get; set; }

        public static int DefaultMillisecondsPerFrame { get; set; }

        static GameLoop()
        {
            DefaultMillisecondsPerFrame = 50;
            MonoBehaviours = new List<MonoBehaviour>();
            WaitingForDestruction = new Queue<MonoBehaviour>();
            WaitingForAdding = new Queue<MonoBehaviour>();
        }

        public static void Destroy(MonoBehaviour monoBehaviour)
        {
            WaitingForDestruction.Enqueue(monoBehaviour);
        }

        public static void UpdateObjects(GameTime gameTime)
        {
            foreach (var mono in MonoBehaviours)
            {
                mono.Update(gameTime);
                mono.gameObject.Update(gameTime);
            }

            while (WaitingForAdding.Count > 0)
            {
                MonoBehaviours.Add(WaitingForAdding.Dequeue());
            }

            while (WaitingForDestruction.Count > 0)
            {
                MonoBehaviours.Remove(WaitingForDestruction.Dequeue());
            }
        }

        public static void DestroyAll()
        {
            foreach (var mono in MonoBehaviours)
            {
                WaitingForDestruction.Enqueue(mono);
            }
        }

        public static void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
            if(spriteBatch != null)
            {
                spriteBatch.Begin();
            }

            foreach (var mono in MonoBehaviours)
            {
                mono.gameObject.Draw(spriteBatch, camera);
            }


            if (spriteBatch != null)
            {
                spriteBatch.End();
            }
        }
    }
}
