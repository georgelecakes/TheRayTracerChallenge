using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Group : RayObject
    {

        public bool performAABBIntersectionTest = true;

        public Group() : base()
        {

        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            Console.WriteLine("Exception - Attempted to get a normal from a group object.");
            throw new NotImplementedException();
        }

        public override List<Intersection> Intersect(Ray ray)
        {
            //For right now we'll only have groups do the axis-aligned bounding box test,
            //however with the way it has been programmed it should easily extend to every object
            //which may be usedful when we get to triangular meshed in the next chapter?

            Bounds groupBounds = GetBounds(); //This will be a bound of 0 to 0

            List<Intersection> xs = new List<Intersection>();

            if (groupBounds.Intersect(ray))
            {
                //Iterate through all child objects and aggregate intersections

                Ray transRay = GetMatrix().Inverse() * ray;

                foreach (RayObject obj in GetChildren())
                {
                    xs.AddRange(obj.Intersect(transRay));
                }

                //Need to sort resulting intersections...
                return Intersection.SortIntersections(xs);
            }
            //No intersections detected
            return xs;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            return b;
        }

    }
}
