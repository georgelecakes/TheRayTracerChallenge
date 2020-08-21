using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Projectile
    {
        public RT.Point position;
        public RT.Vector velocity;

        public Projectile(RT.Point position, RT.Vector velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        public override string ToString()
        {
            return "Position: " + this.position.ToString() + ' ' +
                    "Velocity: " + this.velocity.ToString();
        }

    }
}
