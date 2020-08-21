using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Environment
    {

        public RT.Vector gravity;
        public RT.Vector wind;

        public Environment(RT.Vector gravity, RT.Vector wind)
        {
            this.gravity = gravity;
            this.wind = wind;
        }

        public override string ToString()
        {
            return "Gravity: " + this.gravity.ToString() + ' ' + 
                    "Wind: " + this.wind.ToString();
        }

    }

}
