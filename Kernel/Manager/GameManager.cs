using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kernel.Manager
{
    public class GameManager
    {
        private static GameManager _singleton;

        public static GameManager singleton { get { return Init(); } }

        public HashSet<MonoBehaviour> gameObjects { get; set; }

        public GameManager()
        {
            this.gameObjects = new HashSet<MonoBehaviour>();
        }

        private static GameManager Init()
        {
            if(_singleton == null)
            {
                _singleton = new GameManager();
            }

            return _singleton;
        }

        public MonoBehaviour FindObjectByName(string name)
        {
            foreach(var item in gameObjects)
            {
                if(item.gameObject.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        public Type FindObjectByType<Type>()
            where Type : MonoBehaviour
        {
            foreach (var item in gameObjects)
            {
                if (item is Type)
                {
                    return (Type)item;
                }
            }

            return null;
        }
    }

}
