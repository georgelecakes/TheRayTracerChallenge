using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    /// <summary>
    /// NOTE
    /// WE ARE NOT ALLOWED TO TRANSFORM A BOUNDS OBJECT DIRECTLY THOUGH MATRIX MULTIPLICATION
    /// AS Non-AXIS ALIGNED ORIENTATIONS WILL MESS THINGS UP.
    /// For instance a cylinder when rotated 45 degrees will get the sides chopped off
    /// Therefore, bounds need to be taken from local objects, then converted to corners, then transformed!
    /// 
    /// </summary>


    public class Bounds
    {

        public Point min;
        public Point max;

        public Bounds()
        {
            min = new Point();
            max = new Point();
        }
        

        //Gets the AABB for an object in another space
        //by taking the local corners, converting them using the matrix
        //and then creating a new bound that wraps around them and is axis aligned.
        public Bounds GetAABB(Mat4 transformMatrix)
        {
            List<Point> corners = GetTransformedCorners(transformMatrix);
            Bounds aabb = GetAABB(corners);
            return aabb;
        }

        /**
         * 
         * Returns an axis aligned box around the provided points
         */
        public Bounds GetAABB(List<Point> points)
        {
            Bounds bounds = new Bounds();
            
           if(points.Count > 0)
            {
                bounds.min =  new Point(points[0]);
                bounds.max = new Point(points[0]);

                //Iterate over points and log the biggest and smallest dimensions

                for(int i = 1; i < points.Count; i++)
                {
                    if(points[i].x < bounds.min.x)
                    {
                        bounds.min.x = points[i].x;
                    }
                    if (points[i].y < bounds.min.y)
                    {
                        bounds.min.y = points[i].y;
                    }
                    if (points[i].z < bounds.min.z)
                    {
                        bounds.min.z = points[i].z;
                    }

                    if (points[i].x > bounds.max.x)
                    {
                        bounds.max.x = points[i].x;
                    }
                    if (points[i].y > bounds.max.y)
                    {
                        bounds.max.y = points[i].y;
                    }
                    if (points[i].z > bounds.max.z)
                    {
                        bounds.max.z = points[i].z;
                    }

                }

            }

            return bounds;
        }

        public List<Point> GetTransformedCorners(Mat4 mat)
        {
            List<Point> corners = GetCorners();

            //Is this taking into consideration all the levels between?

            for(int i = 0; i < corners.Count; i++)
            {
                corners[i] = mat * corners[i];
            }
            return corners;
        }

        //Calculates and returns the 8 corners of the Bounds
        public List<Point> GetCorners()
        {
            /*
            *Looking down, winding order CW
            *  p2---p3
            *  |    |
            *  p1---p0 
            *
            */

            List<Point> corners = new List<Point>();

            //Bottom of cube
            corners.Add( new Point( max.x, min.y, min.z));
            corners.Add( new Point( min.x, min.y, min.z));
            corners.Add( new Point( min.x, min.y, max.z));
            corners.Add( new Point( max.x, min.y, max.z));

            /*
            *Looking down, winding order CW
            *  p6---p7
            *  |    |
            *  p5---p4 
            *
            */

            //Top of cube
            corners.Add(new Point(max.x, max.y, min.z));
            corners.Add(new Point(min.x, max.y, min.z));
            corners.Add(new Point(min.x, max.y, max.z));
            corners.Add(new Point(max.x, max.y, max.z));

            return corners;
        }

        //Used to see whether the ray even hits the volume of objects
        //Meant to speed up intersection tests by first testing the bound and if it hits
        //we can test the individual objects

        public bool Intersect(Ray ray)
        {

            double[] xt = CheckAxis(RayObject.Axis.X, ray.origin.x, ray.direction.x);
            double[] yt = CheckAxis(RayObject.Axis.Y, ray.origin.y, ray.direction.y);
            double[] zt = CheckAxis(RayObject.Axis.Z, ray.origin.z, ray.direction.z);

            double tMin = Math.Max(Math.Max(xt[0], yt[0]), zt[0]);
            double tMax = Math.Min(Math.Min(xt[1], yt[1]), zt[1]);

            List<Intersection> xs = new List<Intersection>();

            //Box not hit
            if (tMin > tMax)
            {
                return false;
            }

            //Box hit
            return true;
        }

        public double[] CheckAxis(RayObject.Axis axis, double origin, double direction)
        {
            double[] t = new double[2];

            double tMinNumerator = 0.0;
            double tMaxNumerator = 0.0;

            switch (axis)
            {
                case RayObject.Axis.X:
                    tMinNumerator = (min.x - origin);
                    tMaxNumerator = (max.x - origin);
                    break;
                case RayObject.Axis.Y:
                    tMinNumerator = (min.y - origin);
                    tMaxNumerator = (max.y - origin);
                    break;
                case RayObject.Axis.Z:
                    tMinNumerator = (min.z - origin);
                    tMaxNumerator = (max.z - origin);
                    break;
            }

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

            if (t[0] > t[1])
            {
                double temp = t[0];
                t[0] = t[1];
                t[1] = temp;
            }

            return t;
        }



    }
}
