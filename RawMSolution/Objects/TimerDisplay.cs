using Kernel.Components.UI;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RawMSolution.Objects
{
    public class TimerDisplay : MonoBehaviour
    {
        private Text text { get; set; }

        public TimerDisplay(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            text.text = "Time left:" + (GameStateManager.MaximumTime - Game1.singleton.gameStateManager.timeLimit) / 1000;
        }

        protected override void InitComponents()
        {
            SpriteFont font = Game1.singleton.Content.Load<SpriteFont>("font/defaultFont");
            text = new Text(font);
            text.text = "Time left:";
            this.gameObject.AddComponent(text);
            this.gameObject.transform.Position2D = new Vector2(Game1.singleton.Window.ClientBounds.Width - 120, 10);
        }
    }
}
