using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Plane : RayObject
    {

        public Plane() : base()
        {

        }


        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            return this.GetMatrix() * new Vector(0, 1, 0);
        }

        public override List<Intersection> Intersect(Ray ray)
        {
            List<Intersection> intersections = new List<Intersection>();

            Ray localRay = RayToObjectSpace(ray);

            if (Math.Abs(localRay.direction.y) < Constants.epsilon)
            {
                return intersections;
            }

            intersections.Add(new Intersection(this, -localRay.origin.y / localRay.direction.y));

            return intersections;
        }


        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this

            b.min.y = 0;
            b.max.y = 0;

            b.min.x = double.NegativeInfinity;
            b.max.x = double.PositiveInfinity;

            b.min.z = double.NegativeInfinity;
            b.max.z = double.PositiveInfinity;

            return b;
        }

    }
}
