using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class StripePattern : Pattern
    {
        //Make these patterns instead of colors to allow for patterns
        //to be embedded within patterns as part of the chapter challenge
        //later
        public Pattern a;
        public Pattern b;

        public StripePattern() : base()
        {
            a = new SolidColorPattern(Color.white);
            b = new SolidColorPattern(Color.black);
        }

       public StripePattern(Pattern a, Pattern b) : base()
        {
            this.a = a;
            this.b = b;
        }

        public override Color PatternAt(Point p)
        {

            Point tp = this.matrix.Inverse() * p;

            if(Utility.FE(Math.Floor(tp.x) % 2, 0.0))
            {
                return this.a.PatternAt(tp);
            }
            return this.b.PatternAt(tp);
        }


    }
}
