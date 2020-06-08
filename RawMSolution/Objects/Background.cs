using Kernel.Components;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawMSolution.Objects
{
    public class Background : MonoBehaviour
    {
        public Background(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected override void InitComponents()
        {
            var spriteRen = new SpriteRenderer(@"Content/images/background.png");
            spriteRen.DrawAction = new Action<SpriteBatch>(a => {
                a.Draw(spriteRen.Texture,
                new Rectangle(0, 0, Game1.singleton.Window.ClientBounds.Width,
                Game1.singleton.Window.ClientBounds.Height), null,
                Color.White, 0, Vector2.Zero,
                SpriteEffects.None, 0);
            });

            this.gameObject.AddComponent(spriteRen);
        }
    }
}
