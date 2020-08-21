using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Patterns
{
    public abstract class Pattern
    {
        public Mat4 matrix;

        public Pattern()
        {
            matrix = new Mat4();
        }

        public abstract Color PatternAt(Point point);

        public Color PatternAtObject(RayObject obj, Point p)
        {
            Point transPoint = obj.WorldToObject(p);
            transPoint = matrix.Inverse() * transPoint;
            //Move this into each pattern?
            return this.PatternAt(transPoint);
        }
    }
}
