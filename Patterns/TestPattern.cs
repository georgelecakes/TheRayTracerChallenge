using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class TestPattern : Pattern
    {
        public TestPattern() : base()
        {

        }

        public override Color PatternAt(Point point)
        {
            return new Color(point.x, point.y, point.z);
        }
    }
}
