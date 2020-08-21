using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Mat2
    {
        double[,] mat;
        int size = 2;

        public static Mat2 Translate(double x, double y)
        {
            Mat2 temp = new Mat2();
            temp[0, 2] = x; //x
            temp[1, 2] = y; //y
            return temp;
        }

        public static Mat2 Scale(double x, double y)
        {
            Mat2 temp = new Mat2();
            temp[0, 0] = x; //x
            temp[1, 1] = y; //y
            return temp;
        }

        public static Mat2 Rotate(double z)
        {
            Mat2 temp = new Mat2();
            temp[0, 0] = (double)System.Math.Cos(z);
            temp[0, 1] = (double)System.Math.Sin(z) * -1.0;
            temp[1, 0] = (double)System.Math.Sin(z);
            temp[1, 1] = (double)System.Math.Cos(z);
            return temp;
        }

        public int Size
        {
            get { return size; }
        }

        public Mat2(Mat2 a)
        {
            mat = new double[2, 2];
            mat[0, 0] = a[0, 0];
            mat[0, 1] = a[0, 1];
            mat[1, 0] = a[1, 0];
            mat[1, 1] = a[1, 1];
        }

        public Mat2(double m00 = 1.0, double m01 = 0.0,
                   double m10 = 0.0, double m11 = 1.0)
        {
        mat = new double[2, 2];
        mat[0,0] = m00;
        mat[0, 1] = m01;
        mat[1, 0] = m10;
        mat[1, 1] = m11;
        }
    
        public double this[int r, int c]
        {
            get { return mat[r, c]; }
            set { mat[r,c] = value; }
        }

        public static bool operator ==(Mat2 a, Mat2 b)
        {
            for (int x = 0; x < a.size; x++)
            {
                for (int y = 0; y < a.size; y++)
                {
                    if (!Utility.FE(a[x, y], b[x, y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Mat2 a, Mat2 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            
            return  String.Format("|{0,6:0.00}", mat[0, 0]) + ',' + 
                    String.Format("{0,6:0.00}|", mat[0, 1]) + "\n" + 
                    String.Format("|{0,6:0.00}", mat[1, 0]) + ',' + 
                    String.Format("{0,6:0.00}|", mat[1, 1]);
                    
            /*
            return  mat[0, 0].ToString("0.00") + ',' +
                    mat[0, 1].ToString("0.00") + '\n' +
                    mat[1, 0].ToString("0.00") + ',' +
                    mat[1, 1].ToString("0.00");
                    */
        }

        public static Mat2 operator *(Mat2 a, Mat2 b)
        {
            Mat2 temp = new Mat2();

            for(int r = 0; r < a.size; r++)
            {
                for(int c = 0; c < a.size; c++)
                {
                    temp[r, c] = a[r, 0] * b[0, c] +
                                a[r, 1] * b[1, c];
                }
            }
            return temp;
        }

        public static Mat2 operator*(Mat2 a, double b)
        {
            Mat2 temp = new Mat2(a);

            for (int r = 0; r < a.size; r++)
            {
                for (int c = 0; c < a.size; c++)
                {
                    temp[r, c] *= b;
                }
            }
            return temp;
        }

        public void Transpose()
        {
            Mat2 temp = new Mat2(this);
            {
                for(int r = 0; r < temp.Size; r++)
                {
                    for (int c = 0; c < temp.Size; c++)
                    {
                        this[r, c] = temp[c, r];
                    }
                }
            }
        }

        public double Det()
        {
            return mat[0, 0] * mat[1, 1] - mat[0, 1] * mat[1, 0];
        }

        public Mat2 Inverse()
        {
            Mat2 inverse = new Mat2(this);
            double det = this.Det();
            if (!Utility.FE(0.0f, det))
            { 
                inverse[0, 0] = this[1, 1];
                inverse[1, 1] = this[0, 0];
                inverse[0, 1] = inverse[0, 1] * -1.0;
                inverse[1, 0] = inverse[1, 0] * -1.0;
                inverse = inverse * (1.0f / det);
            }
            return inverse;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Mat2 other = obj as Mat2;

            if (    Utility.FE(this.mat[0, 0], other.mat[0, 0]) &&
                    Utility.FE(this.mat[0, 1], other.mat[0, 1]) &&
                    Utility.FE(this.mat[1, 0], other.mat[1, 0]) &&
                    Utility.FE(this.mat[1, 1], other.mat[1, 1]))
                return true;

            return false;
        }

    }
}
