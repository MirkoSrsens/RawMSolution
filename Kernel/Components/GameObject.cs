using Kernel.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Kernel
{
    public class GameObject : Component
    {
        public Transform transform { get; set; }

        public string Name { get; set; }

        private List<Component> Components { get; set; }

        public GameObject()
        {
            this.Components = new List<Component>();
            transform = new Transform();
            this.AddComponent(transform);
        }

        public GameObject(string name)
            :this()
        {
            this.Name = name;
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            for(int i = 0; i< Components.Count; i++)
            {
                Components[i].LoadContent(graphicsDevice);
            }
        }

        public override void UnloadContent()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].UnloadContent();
            }
            Components.Clear();
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            for(int i = 0; i< this.Components.Count; i++)
            {
                if(this.Components[i] is TComponent)
                {
                    return (TComponent)this.Components[i];
                }
            }

            return null;
        }

        public void AddComponent(Component component)
        {
            this.Components.Add(component);
            component.GameObject = this;

            for (int i = 0; i < Components.Count; i++)
            {
                this.Components[i].UpdateReferences();
            }
        }

        public override void Update(GameTime gameTime)
        {
            for(int i =0; i< Components.Count; i++)
            {
                Components[i].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Draw(spriteBatch);
            }
        }

        public override void UpdateReferences()
        {
        }
    }
}
