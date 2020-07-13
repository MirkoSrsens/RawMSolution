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
            this.text = string.Empty;
        }

        public override void Draw(SpriteBatch spriteBatch = null, Camera camera = null)
        {
            spriteBatch.DrawString(font, text,
            this.GameObject.transform.Position2D, Color.DarkBlue, 0, Vector2.Zero,
            1, SpriteEffects.None, 1);

        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
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
