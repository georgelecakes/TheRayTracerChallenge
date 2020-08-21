using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT {
    public class Cylinder : RayObject
    {

        public double minimum = double.NegativeInfinity;
        public double maximum = double.PositiveInfinity;

        public bool isClosed = false;

        public Cylinder(double min = double.NegativeInfinity, double max = double.PositiveInfinity) : base()
        {
            minimum = min;
            maximum = max;
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {

            double distance = localPoint.x * localPoint.x + localPoint.z * localPoint.z;

            if(distance < 1 && localPoint.y >= this.maximum - Constants.epsilon)
            {
                return new Vector(0, 1, 0);
            }

            else if (distance < 1 && localPoint.y <= this.minimum + Constants.epsilon)
            {
                return new Vector(0, -1, 0);
            }
            else
            {
                return new Vector(localPoint.x, 0, localPoint.z);
            }
        }

        protected bool CheckCap(Ray ray, double t)
        {
            double x = ray.origin.x + t * ray.direction.x;
            double z = ray.origin.z + t * ray.direction.z;
            return (x * x + z * z) <= 1;
        }

        protected void IntersectCaps(Ray ray, ref List<Intersection> xs)
        {


            if(!isClosed || Utility.FE(ray.direction.y , 0))
            {
                return;
            }

            double t = (this.minimum - ray.origin.y) / ray.direction.y;
            if(CheckCap(ray, t))
            {
                xs.Add(new Intersection(this,t));
            }

            t = (this.maximum - ray.origin.y) / ray.direction.y;
            if(CheckCap(ray, t))
            {
                xs.Add(new Intersection(this, t));
            }
        }


        public override List<Intersection> Intersect(Ray ray)
        {

            Ray transRay = RayToObjectSpace(ray);

            List<Intersection> xs = new List<Intersection>();

            double a = transRay.direction.x * transRay.direction.x
                        + transRay.direction.z * transRay.direction.z;

            if(Utility.FE(0, a))
            {
                return xs;
            }

            double b = 2 * transRay.origin.x * transRay.direction.x +
                        2 * transRay.origin.z * transRay.direction.z;
            double c = transRay.origin.x * transRay.origin.x + transRay.origin.z * transRay.origin.z - 1;

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return xs;

            double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            if(t0 > t1)
            {
                double temp = t0;
                t0 = t1;
                t1 = temp;
            }

            double y0 = transRay.origin.y + t0 * transRay.direction.y;

            if(this.minimum < y0 && y0 < this.maximum)
            {
                xs.Add(new Intersection(this,t0));
            }

            double y1 = transRay.origin.y + t1 * transRay.direction.y;

            if (this.minimum < y1 && y1 < this.maximum)
            {
                xs.Add(new Intersection(this,t1));
            }

            IntersectCaps(ray, ref xs);

            return xs;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this

            b.min.y = minimum;
            b.max.y = maximum;

            b.min.x = -1;
            b.max.x = 1;

            b.min.z = -1;
            b.max.z = 1;

            return b;
        }


    }
}
