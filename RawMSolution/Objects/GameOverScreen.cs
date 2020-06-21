using Kernel.Components.UI;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RawMSolution.Objects
{
    public class GameOverScreen : MonoBehaviour
    {
        private Text text { get; set; }

        public GameOverScreen(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected override void InitComponents()
        {
            SpriteFont font = Game1.singleton.Content.Load<SpriteFont>("font/defaultFont");
            text = new Text(font);
            text.text = string.Format("GAME OVER \n YOUR SCORE IS: {0}", Manager.TotalScore);
            this.gameObject.AddComponent(text);
            this.gameObject.transform.Position2D = new Vector2(Game1.singleton.Window.ClientBounds.Width/2, Game1.singleton.Window.ClientBounds.Height / 2);
        }
    }
}
