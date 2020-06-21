using Kernel;
using Kernel.Components.UI;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RawMSolution.Objects
{
    public class MainMenu : MonoBehaviour
    {
        private Text text { get; set; }

        public MainMenu(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                Game1.singleton.gameStateManager.StartGame();
            }
        }

        protected override void InitComponents()
        {
            SpriteFont font = Game1.singleton.Content.Load<SpriteFont>("font/defaultFont");
            text = new Text(font);
            text.text = "Collect all balls \n Press Any key to begin <> ";
            this.gameObject.AddComponent(text);
            this.gameObject.transform.Position2D = new Vector2(Game1.singleton.Window.ClientBounds.Width / 2, Game1.singleton.Window.ClientBounds.Height/2);
        }
    }
}
