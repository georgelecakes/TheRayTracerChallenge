using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Cone : RayObject
    {
        public double minimum = double.NegativeInfinity;
        public double maximum = double.PositiveInfinity;
        public bool isClosed = false;

        public Cone(double min = double.NegativeInfinity,
                            double max = double.PositiveInfinity,
                            bool isClosed = false) : base()
        {
            minimum = min;
            maximum = max;
            this.isClosed = isClosed;
        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            //End caps are the same as the cylinder except for:

            double distance = localPoint.x * localPoint.x + localPoint.z * localPoint.z;

            if (distance < 1 && localPoint.y >= this.maximum - Constants.epsilon)
            {
                return new Vector(0, 1, 0);
            }

            else if (distance < 1 && localPoint.y <= this.minimum + Constants.epsilon)
            {
                return new Vector(0, -1, 0);
            }
            else
            {
                double y = Math.Sqrt(localPoint.x * localPoint.x + localPoint.z * localPoint.z);
                if(localPoint.y > 0 )
                {
                    y = -y;
                }
                return new Vector(localPoint.x, y, localPoint.z);
            }
        }

        protected bool CheckCap(Ray ray, double t)
        {
            double x = ray.origin.x + t * ray.direction.x;
            double z = ray.origin.z + t * ray.direction.z;
            return (x * x + z * z) <= Math.Abs(ray.origin.y + t * ray.direction.y); //Radius of 1 was for a cylinder, this needs to be changed
            //to be the absolute value of the y coordinate, because a cone fans out with distance.
        }

        protected void IntersectCaps(Ray ray, ref List<Intersection> xs)
        {
            if (!isClosed || Utility.FE(ray.direction.y, 0))
            {
                return;
            }

            double t = (this.minimum - ray.origin.y) / ray.direction.y;
            if (CheckCap(ray, t))
            {
                xs.Add(new Intersection(this, t));
            }

            t = (this.maximum - ray.origin.y) / ray.direction.y;
            if (CheckCap(ray, t))
            {
                xs.Add(new Intersection(this, t));
            }
        }


        public override List<Intersection> Intersect(Ray ray)
        {
            /*
            double a = transRay.direction.x * transRay.direction.x - transRay.direction.y * transRay.direction.y +
                        transRay.direction.z * transRay.direction.z;

            double b = 2.0 * transRay.origin.x * transRay.direction.x - 2.0 * transRay.origin.y * transRay.direction.y +
                        2.0 * transRay.origin.z * transRay.direction.z;

            double c = transRay.origin.x * transRay.origin.x - transRay.origin.y * transRay.origin.y + transRay.origin.z * transRay.origin.z;
            double t;
            */

            Ray transRay = RayToObjectSpace(ray);

            List<Intersection> xs = new List<Intersection>();

            double a = transRay.direction.x * transRay.direction.x - transRay.direction.y * transRay.direction.y +
                        transRay.direction.z * transRay.direction.z;

            double b = 2.0 * transRay.origin.x * transRay.direction.x - 2.0 * transRay.origin.y * transRay.direction.y +
            2.0 * transRay.origin.z * transRay.direction.z;
            double c = transRay.origin.x * transRay.origin.x - transRay.origin.y * transRay.origin.y + transRay.origin.z * transRay.origin.z;


            if (Utility.FE(0, a))
            {
                if (Utility.FE(0, b))
                {
                    //All misses
                    return xs;
                }
                //b is not zero, have a single point of intersection
                xs.Add(new Intersection(this, -c / (2 * b)));
            }

            //Both A and B are not zero at this point. 
            //Use the cylinder algorithm but with the new A B and C...

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return xs;

            double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            if (t0 > t1)
            {
                double temp = t0;
                t0 = t1;
                t1 = temp;
            }

            double y0 = transRay.origin.y + t0 * transRay.direction.y;

            if (this.minimum < y0 && y0 < this.maximum)
            {
                xs.Add(new Intersection(this, t0));
            }

            double y1 = transRay.origin.y + t1 * transRay.direction.y;

            if (this.minimum < y1 && y1 < this.maximum)
            {
                xs.Add(new Intersection(this, t1));
            }

            IntersectCaps(ray, ref xs);

            return xs;
        }

        public override Bounds GetLocalBounds()
        {
            //Bounds for a cone are the bottom, top and sides
            Bounds b = new Bounds();

            //These can potentially be infinite.
            b.min.y = this.minimum;
            b.max.y = this.maximum;

            //I believe the max size is 1 unit from the center, will have to 
            //re-check the books chapters on this
            b.min.x = -1;
            b.max.x = 1;

            b.min.z = -1;
            b.max.z = 1;

            return b;
        }
    }
}
