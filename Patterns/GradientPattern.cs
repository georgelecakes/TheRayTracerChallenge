using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class GradientPattern : Pattern
    {
        public Pattern a;
        public Pattern b;

        public GradientPattern() : base()
        {
            this.a = new SolidColorPattern(Color.white);
            this.b = new SolidColorPattern(Color.black);
        }

        public GradientPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            Point tp = this.matrix.Inverse() * point;
            return  this.a.PatternAt(tp) +
                    (this.b.PatternAt(tp) - this.a.PatternAt(tp)) *
                    (tp.x - Math.Floor(tp.x));
        }
    }
}
