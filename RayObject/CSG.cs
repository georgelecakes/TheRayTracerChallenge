using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class CSG : RayObject
    {
        public enum Operation
        {
            Union,
            Intersection,
            Difference
        }

        public Operation operation;

        public RayObject left;
        public RayObject right;

        public CSG(Operation operation, RayObject s1,
                    RayObject s2) : base()
        {
            this.operation = operation;
            left = s1;
            right = s2;
            left.SetParent(this);
            right.SetParent(this);
        }

        public static bool IntersectionAllowed(Operation operation, 
                                        bool lhit,
                                        bool inl,
                                        bool inr)
        {
            if(operation == Operation.Union)
            {
                return (lhit && !inr) || (!lhit && !inl);
            }

            else if(operation == Operation.Intersection)
            {
                return (lhit && inr) || (!lhit && inl);
            }

            else if(operation == Operation.Difference)
            {
                return (lhit && !inr) || (!lhit && inl);
            }

            return false;
        }

        public List<Intersection> FilterIntersections(List<Intersection> xs)
        {
            bool inl = false;
            bool inr = false;

            List<Intersection> temp = new List<Intersection>();

            foreach (Intersection i in xs)
            {
                bool lhit = this.left.Includes(i.rayObject); // This is a search through child objects.
            
                if(IntersectionAllowed(this.operation, lhit, inl, inr))
                {
                    temp.Add(i);
                }
                
                if(lhit)
                {
                    inl = !inl;
                }

                else
                {
                    inr = !inr;
                }
            }
            return temp;

        }

        public override Vector CalculateLocalNormal(Point localPoint, Intersection i = null)
        {
            throw new NotImplementedException();
        }

        public override List<Intersection> Intersect(Ray ray)
        {

            Ray transRay = this.matrix.Inverse() * ray;
            
            List<Intersection> leftxs = this.left.Intersect(transRay);
            List<Intersection> rightxs = this.right.Intersect(transRay);

            leftxs.AddRange(rightxs);

            List<Intersection> sorted = Intersection.SortIntersections(leftxs);

            return FilterIntersections(sorted);
        }

        public override Bounds GetLocalBounds()
        {
            Bounds temp = new Bounds();
            return temp;
        }
    }
}
