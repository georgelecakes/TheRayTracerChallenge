using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Cube : RayObject
    {

        public Cube() : base()
        {

        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {


            double maxFace = Math.Max(
                                Math.Max(
                                    Math.Abs(localPoint.x), Math.Abs(localPoint.y)),
                                    Math.Abs(localPoint.z));

            if (Utility.FE(maxFace, Math.Abs(localPoint.x)))
            {
                return new Vector(localPoint.x, 0, 0);
            }
            else if(Utility.FE(maxFace, Math.Abs(localPoint.y)))
            {
                return new Vector(0, localPoint.y, 0);
            }
            return new Vector(0, 0, localPoint.z);
        }

        public override List<Intersection> Intersect(Ray ray)
        {

            Ray transRay = RayToObjectSpace(ray);

            double[] xt = CheckAxis(transRay.origin.x, transRay.direction.x);
            double[] yt = CheckAxis(transRay.origin.y, transRay.direction.y);
            double[] zt = CheckAxis(transRay.origin.z, transRay.direction.z);

            double tMin = Math.Max(Math.Max(xt[0], yt[0]), zt[0]);
            double tMax = Math.Min(Math.Min(xt[1], yt[1]), zt[1]);

            List<Intersection> xs = new List<Intersection>();

            if (tMin > tMax)
            {
                return xs;
            }
            
            xs.Add(new Intersection(this, tMin));
            xs.Add(new Intersection(this, tMax));
            return xs;
        }

        public double[] CheckAxis(double origin, double direction)
        {

            double[] t = new double[2];

            double tMinNumerator = (-1 - origin);
            double tMaxNumerator = (1 - origin);

            //Infinities might pop here due to division by zero
            if (Math.Abs(direction) >= Constants.epsilon)
            {
                t[0] = tMinNumerator / direction;
                t[1] = tMaxNumerator / direction;
            }
            else
            {
                t[0] = tMinNumerator * Constants.Infinity;
                t[1] = tMaxNumerator * Constants.Infinity;
            }

            if(t[0] > t[1])
            {
                double temp = t[0];
                t[0] = t[1];
                t[1] = temp;
            }

            return t;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this

            b.min.y = -1;
            b.max.y = 1;

            b.min.x = -1;
            b.max.x = 1;

            b.min.z = -1;
            b.max.z = 1;

            return b;
        }
    }
}
