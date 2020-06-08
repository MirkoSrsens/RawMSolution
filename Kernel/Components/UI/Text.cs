using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Kernel.Components.UI
{
    public class Text : Component
    {
        private SpriteFont font { get; set; }

        public string text { get; set; }

        public Text(SpriteFont font)
        {
            this.font = font;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text,
            this.GameObject.transform.Position, Color.DarkBlue, 0, Vector2.Zero,
            1, SpriteEffects.None, 1);

        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void UpdateReferences()
        {
        }

        public override void Destroy()
        {
            this.GameObject.Components.Remove(this);
        }
    }
}
