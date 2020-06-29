using _3DSolution.Scripts;
using Kernel.Components;
using Kernel.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Solution.Objects
{
    public class GameStateManager
    {
        public const int MaximumTime = 10000;

        public int timeLimit { get; set; }

        public GraphicsDevice graphicsDevice { get; set; } 

        public CameraObject camera { get; set; }

        VertexPositionTexture[] verts { get; set; }

        VertexBuffer vertexBuffer { get; set; }

        BasicEffect effect { get; set; }

        Matrix world { get; set; }

        Matrix worldTranslation = Matrix.Identity;

        Matrix worldRotation = Matrix.Identity;
        SpriteRenderer texture { get; set; }

        public GameStateManager(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            verts = new VertexPositionTexture[4];
            verts[0] = new VertexPositionTexture(
            new Vector3(-1, 1, 0), new Vector2(0, 0));
            verts[1] = new VertexPositionTexture(
             new Vector3(1, 1, 0), new Vector2(1, 0));
            verts[2] = new VertexPositionTexture(
             new Vector3(-1, -1, 0), new Vector2(0, 1));
            verts[3] = new VertexPositionTexture(
             new Vector3(1, -1, 0), new Vector2(1, 1));

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
            vertexBuffer.SetData(verts);

            effect = new BasicEffect(graphicsDevice);

            camera = new CameraObject(graphicsDevice);

            world = Matrix.Identity;

            RasterizerState rs = new RasterizerState();

            texture = new SpriteRenderer(@"Content/images/threerings.png");
            texture.LoadContent(graphicsDevice);

            rs.CullMode = CullMode.None;
            graphicsDevice.RasterizerState = rs;
        }

        public void StartGame()
        {
        }

        public void GameOver()
        {
            GameLoop.DestroyAll();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                worldTranslation *= Matrix.CreateTranslation(-0.01f, 0, 0);
            if (keyboardState.IsKeyDown(Keys.Right))
                worldTranslation *= Matrix.CreateTranslation(.01f, 0, 0);
            worldRotation *= Matrix.CreateFromYawPitchRoll(
             MathHelper.PiOver4 / 60,
             0f,
             0.01f);
            GameLoop.UpdateObjects(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            graphicsDevice.SetVertexBuffer(vertexBuffer);

            effect.World = worldRotation * worldTranslation * worldRotation;
            effect.View = camera.camera.view;
            effect.Projection = camera.camera.projection;
            effect.TextureEnabled = true;
            effect.Texture = texture.Texture;

            foreach(var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, verts, 0, 2);
            }
        }
    }
}
