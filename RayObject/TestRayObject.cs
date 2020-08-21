using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class TestRayObject : RayObject
    {
        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            return new Vector(localPoint);
        }

        public override Bounds GetLocalBounds()
        {
            return new Bounds();
        }

        public override List<Intersection> Intersect(Ray ray)
        {
            return new List<Intersection>();
        }

    }
}
