using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Components
{
    public class ParticleSettings
    {
        public int maxSize = 2;
    }

    public class ParticleExplosionSettings
    {
        // Life of particles
        public int minLife = 1000;
        public int maxLife = 2000;
        // Particles per round
        public int minParticlesPerRound = 100;
        public int maxParticlesPerRound = 600;
        // Round time
        public int minRoundTime = 16;
        public int maxRoundTime = 50;
        // Number of particles
        public int minParticles = 2000;
        public int maxParticles = 3000;
    }
}
