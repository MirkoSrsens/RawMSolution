using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Kernel.Components
{
    public class AudioSource : Component
    {
        public SoundEffect SoundSource { get; set; }

        public SoundEffectInstance SoundInstance { get; set; }

        public AudioSource(string sourcePath)
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open))
            {
                this.SoundSource = SoundEffect.FromStream(stream);
            }

            this.SoundInstance = this.SoundSource.CreateInstance();
        }

        public void Play()
        {
            if(SoundInstance != null)
            {
                SoundInstance.Play();
            }
        }

        // Not needed on sound
        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!Enabled) return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Enabled || !Visiable) return;
        }

        public override void UpdateReferences()
        {
        }

        public override void Destroy()
        {
            this.SoundInstance = null;
            this.SoundSource = null;
        }
    }
}
