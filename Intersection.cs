using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Intersection
    {
        public RayObject rayObject;
        public double t;

        //Used with triangles, disregard with anything else.
        public double u;
        public double v;

        public Intersection(RayObject obj, double t, double u = 0.0, double v = 0.0)
        {
            rayObject = obj;
            this.t = t;
            this.u = u;
            this.v = v;
        }

        public override string ToString()
        {
            return "Intersection: " + rayObject.Id + " " + this.t.ToString();
        }

        public static List<RT.Intersection> SortIntersections(List<RT.Intersection> intersections)
        {
            if (intersections.Count == 0)
                return intersections;

            List<RT.Intersection> sortedIntersections = new List<RT.Intersection>();

            sortedIntersections.Add(intersections[0]);

            int currentIntersection = 1;
            bool valueInserted = false;

            while (currentIntersection < intersections.Count)
            {
                valueInserted = false;
                for (int i = 0; i < sortedIntersections.Count; i++)
                {
                    if (intersections[currentIntersection].t < sortedIntersections[i].t)
                    {
                        sortedIntersections.Insert(i, intersections[currentIntersection]);
                        currentIntersection++;
                        valueInserted = true;
                        break;
                    }
                }
                if (!valueInserted)
                {
                    sortedIntersections.Add(intersections[currentIntersection]);
                    currentIntersection++;
                    valueInserted = false;
                }
            }
            return sortedIntersections;
        }

        public override bool Equals(object obj)
        {
            var intersection = obj as Intersection;
            return intersection != null &&
                   EqualityComparer<RayObject>.Default.Equals(rayObject, intersection.rayObject) &&
                   t == intersection.t &&
                   u == intersection.u &&
                   v == intersection.v;
        }
    }
}
