using System;


namespace RT
{
    public class Vector : Tuple
    {
        public Vector() : base(0.0, 0.0, 0.0, 0.0)
        {}

        public Vector(double x = 0.0, double y = 0.0, double z = 0.0) : base(x, y, z, 0.0)
        {}

        public Vector(Vector v) : base(v.x, v.y, v.z, v.w)
        {

        }

        public Vector(Point p) : base(p.x, p.y, p.z, 0.0)
        {
            
        }

        public static Vector operator +(Vector a, Vector b)
        {
            Vector temp = new Vector();
            temp.x = a.x + b.x;
            temp.y = a.y + b.y;
            temp.z = a.z + b.z;
            temp.w = a.w + b.w;
            return temp;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            Vector temp = new Vector();
            temp.x = a.x - b.x;
            temp.y = a.y - b.y;
            temp.z = a.z - b.z;
            temp.w = a.w - b.w;
            return temp;
        }

        public Vector Negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
            this.w = -this.w;
            return this;
        }

        public static Vector operator -(Vector a)
        {
            Vector temp = new Vector();
            temp.x = -a.x;
            temp.y = -a.y;
            temp.z = -a.z;
            temp.w = -a.w;
            return temp;
        }

        public static Vector operator *(Vector a, double b)
        {
            Vector temp = new Vector();
            temp.x = a.x * b;
            temp.y = a.y * b;
            temp.z = a.z * b;
            temp.w = a.w * b;
            return temp;
        }

        public static Vector operator *(double a, Vector b)
        {
            Vector temp = new Vector();
            temp.x = b.x * a;
            temp.y = b.y * a;
            temp.z = b.z * a;
            temp.w = b.w * a;
            return temp;
        }

        public static Vector operator *(Mat4 a, Vector b)
        {
            Vector temp = new Vector();

            temp.x = a[0, 0] * b.x + a[0, 1] * b.y + a[0, 2] * b.z + a[0, 3] * b.w;
            temp.y = a[1, 0] * b.x + a[1, 1] * b.y + a[1, 2] * b.z + a[1, 3] * b.w;
            temp.z = a[2, 0] * b.x + a[2, 1] * b.y + a[2, 2] * b.z + a[2, 3] * b.w;
            temp.w = a[3, 0] * b.x + a[3, 1] * b.y + a[3, 2] * b.z + a[3, 3] * b.w;

            return temp;
        }

        public Vector Scale(double s)
        {
            this.x *= s;
            this.y *= s;
            this.z *= s;
            this.w *= s;

            return this;
        }

        public Vector Normalize()
        {
            double mag = this.Magnitude();

            if(Utility.FE(0.0, mag))
            {
                //This vector is zero and can't be normalized. 
                //Return zero
                this.x = 0.0;
                this.y = 0.0;
                this.z = 0.0;
                this.w = 0.0;
                return this;
            }

            this.x = this.x / mag;
            this.y = this.y / mag;
            this.z = this.z / mag;
            this.w = this.w / mag;
            return this;
        }

        public Vector Normalized()
        {
            Vector temp = new Vector();
            double mag = this.Magnitude();

            if (Utility.FE(0.0, mag))
            {
                //This vector is zero and can't be normalized. 
                //Return zero
                return new Vector(0, 0, 0);
            }

            temp.x = this.x / mag;
            temp.y = this.y / mag;
            temp.z = this.z / mag;
            temp.w = this.w / mag; //Not necessary since zero?
            return temp;
        }

        public Vector Cross(Vector b)
        {
            Vector temp = new Vector();
            temp.x = this.y * b.z - this.z * b.y;
            temp.y = this.z * b.x - this.x * b.z;
            temp.z = this.x * b.y - this.y * b.x;
            return temp;
        }

        public static Vector Cross(Vector a, Vector b)
        {
            Vector temp = new Vector();
            temp.x = a.y * b.z - a.z * b.y;
            temp.y = a.z * b.x - a.x * b.z;
            temp.z = a.x * b.y - a.y * b.x;
            return temp;
        }

        public static Vector Reflect(Vector incoming, Vector normal)
        {
            return incoming - normal * 2.0 * Dot(incoming, normal);
        }

    }
}
