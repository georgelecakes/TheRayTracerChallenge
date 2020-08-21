using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class CheckersPattern : Pattern
    {

        public Pattern a;
        public Pattern b;

        public CheckersPattern() : base()
        {
            a = new SolidColorPattern(Color.white);
            b = new SolidColorPattern(Color.black);
        }

        public CheckersPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point tp = this.matrix.Inverse() * point;
            if (Utility.FE((Math.Floor(tp.x + Constants.epsilon) + Math.Floor(tp.y + Constants.epsilon) + Math.Floor(tp.z + Constants.epsilon)) % 2, 0.0))
                return this.a.PatternAt(tp);
            else
            {
                return this.b.PatternAt(tp);
            }
        }
    }
}
