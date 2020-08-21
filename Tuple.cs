using System;

namespace RT
{
    public abstract class Tuple
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public Tuple(double x = 0.0, double y = 0.0, double z = 0.0, double w = 0.0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ',' +
                             y.ToString() + ',' +
                             z.ToString() + ',' +
                             w.ToString() + ")";
        }

        public double Magnitude()
        {
            double temp = Math.Sqrt(this.x  * this.x + this.y * this.y + this.z * this.z + this.w * this.w);
            return (double)temp;
        }

        public double SqrMagnitude()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w;
        }

        public double Dot(Tuple a)
        {
            return this.x * a.x + this.y * a.y + this.z * a.z + this.w * a.w;
        }

        public static double Dot(Tuple a, Tuple b)
        {
            return b.x * a.x + b.y * a.y + b.z * a.z + b.w * a.w;
        }

        public override bool Equals(object obj)
        {
            var tuple = obj as Tuple;
            return tuple != null &&
                   RT.Utility.FE(x , tuple.x) &&
                   RT.Utility.FE(y , tuple.y) &&
                   RT.Utility.FE(z , tuple.z) &&
                   RT.Utility.FE(w , tuple.w);
        }
  
        public static bool operator==(Tuple tuple1, Tuple tuple2)
        {
            //If null values passed in, are they both null?
            if(object.ReferenceEquals(tuple1, null))
            {
                return object.ReferenceEquals(tuple2, null);
            }

            return tuple1.Equals(tuple2);

        }

        public static bool operator!=(Tuple tuple1, Tuple tuple2)
        {
            return !(tuple1 == tuple2);
        }

    }
}
