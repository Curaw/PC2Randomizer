using Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC2Randomizer
{
    internal class CityRandomizer
    {
        private Mem mrmy;
        private readonly Random rng;

        public CityRandomizer(Mem mrmy, Random rng)
        {
            this.mrmy = mrmy;
            this.rng = rng;
        }
    }
}
