using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class RadialGradientPattern : Pattern
    {

        public Pattern a;
        public Pattern b;

        public RadialGradientPattern() : base()
        {
            a = new SolidColorPattern(Color.white);
            b = new SolidColorPattern(Color.black);
        }

        public RadialGradientPattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point point)
        {
            //Ring pattern
            Point tp = this.matrix.Inverse() * point;
            double distance = Math.Sqrt(tp.x * tp.x + tp.z * tp.z);
            double fraction = distance - Math.Floor(distance);
            return this.a.PatternAt(tp) +
                    (this.b.PatternAt(tp) - this.a.PatternAt(tp))*
                    fraction;
        }
    }
}
