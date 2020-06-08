using Kernel.Components.UI;
using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RawMSolution.Objects
{
    public class ScoreBox : MonoBehaviour
    {
        Text text;

        public ScoreBox(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            text.text = "Score" + Manager.TotalScore;
        }

        protected override void InitComponents()
        {
            SpriteFont font = Game1.singleton.Content.Load<SpriteFont>("font/defaultFont");
            text = new Text(font);
            this.gameObject.AddComponent(text);
            this.gameObject.transform.Position = new Vector2(10, 10);
        }
    }
}
