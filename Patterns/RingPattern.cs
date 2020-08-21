using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class RingPattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public RingPattern() : base()
        {
            this.a = new SolidColorPattern(Color.white);
            this.b = new SolidColorPattern(Color.black);
        }

        public RingPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point tp = this.matrix.Inverse() * point;
            if (Utility.FE(Math.Floor(Math.Sqrt(tp.x * tp.x + tp.z * tp.z)) % 2, 0))
            {
                return this.a.PatternAt(tp);
            }
            return this.b.PatternAt(tp);
        }
    }
}
