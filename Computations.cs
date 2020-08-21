using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Computations
    {
        public double t;
        public RayObject rayObject;
        public Point point;
        public Vector eye;
        public Vector normal;
        public bool inside;
        public Point overPoint;
        public Point underPoint;
        public Vector reflectVector;
        public double n1;
        public double n2;

        public static Computations Prepare(Intersection i, Ray ray, List<Intersection> xs)
        {

            if (i == null)
                return null;

            Point temp = ray.Position(i.t);

            Computations c = new Computations();
            c.rayObject = i.rayObject;
            c.t = i.t;
            c.point = temp;
            c.eye = -ray.direction.Normalize();
            c.normal = i.rayObject.GetNormal(temp, i).Normalize();
            c.n1 = RefractiveIndex.Vacuum;
            c.n2 = RefractiveIndex.Vacuum;

            if(c.normal.Dot(c.eye) < 0)
            {
                c.inside = true;
                c.normal = -c.normal;
            }
            else
            {
                c.inside = false;
            }

            //Transparency Intersections algorithm
            if (xs != null)
            {
                List<RayObject> container = new List<RayObject>();
                for (int x = 0; x < xs.Count; x++)
                {
                    if (i == xs[x])
                    {
                        if(container.Count == 0)
                        {
                            c.n1 = RefractiveIndex.Vacuum;
                        }
                        else
                        {
                            c.n1 = container.Last<RayObject>().material.RefracIndex;
                        }
                    }

                    if(container.Contains(xs[x].rayObject))
                    {
                        container.Remove(xs[x].rayObject);
                    }
                    else
                    {
                        container.Add(xs[x].rayObject);
                    }

                    if(i == xs[x])
                    {
                        if(container.Count == 0)
                        {
                            c.n2 = RefractiveIndex.Vacuum;
                        }
                        else
                        {
                            c.n2 = container.Last<RayObject>().material.RefracIndex;
                        }
                        break;
                    }


            }
            }

            c.reflectVector = Vector.Reflect(ray.direction, c.normal);

            c.overPoint = c.point + (c.normal * Constants.epsilon);
            c.underPoint = c.point - (c.normal * Constants.epsilon);
            return c;
        }

        public Computations()
        {

        }

        public Computations(RayObject rayObject, double t,
                            Point point, Vector eye, Vector normal)
        {
            this.t = t;
            this.rayObject = rayObject;
            this.point = point;
            this.eye = eye;
            this.normal = normal;
        }

    }
}
