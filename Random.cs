using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    class Random
    {
        public static Random Instance = null;

        public System.Random random;

        public Random(int seed = 0)
        {
            if(Instance == null)
            {
                Instance = this;
                random = new System.Random(seed);
            }
        }

        public double NextDouble(double start, double end)
        {
            double value = this.random.NextDouble();
            double magnitude = Math.Abs(end - start);
            value = (magnitude * value) + start;
            return value;
        }

    }
}
