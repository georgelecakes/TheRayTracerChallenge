using System;
using RT;

namespace RT
{
    public class Point : Tuple
    {

        public Point Randomize(Point start, Point end)
        {
            this.x = Random.Instance.NextDouble(start.x, end.x);
            this.y = Random.Instance.NextDouble(start.y, end.y);
            this.z = Random.Instance.NextDouble(start.z, end.z);
            return this;
        }

        public Point(Point p) : base(p.x, p.y, p.z, p.w)
        {

        }

        public Point() : base(0.0, 0.0, 0.0, 1.0)
        {
        }

        public Point(double x = 0.0, double y = 0.0, double z = 0.0, double w = 1.0) : base(x, y, z, w)
        {
        }

        public static Point operator +(Point a, Point b)
        {
            Point temp = new Point();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            temp.w = a.w + b.w;
            return temp;

        }

        public static Point operator +(Vector a, Point b)
        {
            Point temp = new Point();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            temp.w = a.w + b.w;
            return temp;

        }

        public static Point operator +(Point a, Vector b)
        {
            Point temp = new Point();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            temp.w = a.w + b.w;
            return temp;
        }

        public static Vector operator -(Point a, Point b)
        {
            Vector temp = new Vector();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            temp.w = a.w - b.w;
            return temp;
        }

        public static Point operator -(Vector a, Point b)
        {
            Point temp = new Point();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            temp.w = a.w - b.w;
            return temp;

        }

        public static Point operator -(Point a, Vector b)
        {
            Point temp = new Point();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            temp.w = a.w - b.w;
            return temp;
        }
        public static Point operator -(Point a)
        {
            Point temp = new Point();
            temp.x = -a.x;
            temp.y = -a.y;
            temp.z = -a.z;
            temp.w = -a.w; //Not sure about negating w
            return temp;
        }

        public Point Negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
            this.w = -this.w;
            return this;
        }

        public static Point operator *(Point a, double b)
        {
            Point temp = new Point();
            temp.x = a.x * b;
            temp.y = a.y * b;
            temp.z = a.z * b;
            temp.w = a.w * b;
            return temp;
        }

        public static Point operator *(double a, Point b)
        {
            Point temp = new Point();
            temp.x = b.x * a;
            temp.y = b.y * a;
            temp.z = b.z * a;
            temp.w = b.w * a;
            return temp;
        }

        public static Point operator *(Mat4 a, Point b)
        {
            Point temp = new Point();

            temp.x = a[0, 0] * b.x + a[0, 1] * b.y + a[0, 2] * b.z + a[0, 3] * b.w;
            temp.y = a[1, 0] * b.x + a[1, 1] * b.y + a[1, 2] * b.z + a[1, 3] * b.w;
            temp.z = a[2, 0] * b.x + a[2, 1] * b.y + a[2, 2] * b.z + a[2, 3] * b.w;
            temp.w = a[3, 0] * b.x + a[3, 1] * b.y + a[3, 2] * b.z + a[3, 3] * b.w;

            return temp;
        }

        public Point Scale(double s)
        {
            this.x *= s;
            this.y *= s;
            this.z *= s;
            this.w *= s;

            return this;
        }

        public Point Normalize()
        {
            double mag = this.Magnitude();
            this.x = this.x / mag;
            this.y = this.y / mag;
            this.z = this.z / mag;
            this.w = this.w / mag;
            return this;
        }

        public Point Normalized()
        {
            Point temp = new Point();
            double mag = this.Magnitude();
            temp.x = this.x / mag;
            temp.y = this.y / mag;
            temp.z = this.z / mag;
            temp.w = this.w / mag;
            return temp;
        }


    }
}
