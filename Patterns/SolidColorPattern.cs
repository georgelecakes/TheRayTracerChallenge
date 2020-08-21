using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public class SolidColorPattern : Pattern
    {
        Color color;

        public SolidColorPattern() : base()
        {
            color = Color.black;
        }

        public SolidColorPattern(Color color) : base()
        {
            this.color = color;
        }

        public override Color PatternAt(Point point)
        {
            return this.color;
        }
    }
}
