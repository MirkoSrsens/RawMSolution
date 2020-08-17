using Kernel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Scripts
{
    public class Player : MonoBehaviour
    {
        public int Score { get; set; }

        public bool IsChasing { get; set; }

        public Rectangle clientBounds { get; set; }

        public Vector2 frameSize { get; set; }

        public Player(bool isChasing, GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            this.Score = 0;
            this.IsChasing = isChasing;
        }

        public override void Update(GameTime gameTime)
        {
            var direction = 1;

            this.gameObject.transform.Position2D += new Vector2(1, 1);

            var position = this.gameObject.transform.Position2D;

            if (position.X < 0)
                position.X = 0;

            if (position.Y < 0)
                position.Y = 0;

            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;

            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;

        }

        protected override void InitComponents()
        {
        }
    }
}
