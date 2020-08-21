using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Mat3
    {
        double[,] mat;
        int size = 3;

        public static Mat3 Translate(double x, double y, double z)
        {
            Mat3 temp = new Mat3();
            temp[0, 2] = x; //x
            temp[1, 2] = y; //y
            temp[2, 2] = z; //z
            return temp;
        }

        public static Mat3 Scale(double x, double y, double z)
        {
            Mat3 temp = new Mat3();
            temp[0, 0] = x; //x
            temp[1, 1] = y; //y
            temp[2, 2] = z; //z
            return temp;
        }

        //Order here matters, so be careful!
        public static Mat3 Rotate(double x, double y, double z)
        {
            Mat3 tempX = Mat3.RotateX(x);
            Mat3 tempY = Mat3.RotateY(y);
            Mat3 tempZ = Mat3.RotateZ(z);
            return tempZ * tempY * tempX;
        }

        public static Mat3 RotateX(double x)
        {
            Mat3 temp = new Mat3();
            temp[1, 1] = (double)System.Math.Cos(x);
            temp[1, 2] = (double)System.Math.Sin(x) * -1.0;
            temp[2, 1] = (double)System.Math.Sin(x);
            temp[2, 2] = (double)System.Math.Cos(x);
            return temp;
        }

        public static Mat3 RotateY(double y)
        {
            Mat3 temp = new Mat3();
            temp[0, 0] = (double)System.Math.Cos(y);
            temp[0, 2] = (double)System.Math.Sin(y);
            temp[2, 0] = (double)System.Math.Sin(y) * -1.0;
            temp[2, 2] = (double)System.Math.Cos(y);
            return temp;
        }

        public static Mat3 RotateZ(double z)
        {
            Mat3 temp = new Mat3();
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

        public Mat3(Mat3 a)
        {
            mat = new double[3, 3];
            mat[0, 0] = a[0, 0];
            mat[0, 1] = a[0, 1];
            mat[0, 2] = a[0, 2];
            mat[1, 0] = a[1, 0];
            mat[1, 1] = a[1, 1];
            mat[1, 2] = a[1, 2];
            mat[2, 0] = a[2, 0];
            mat[2, 1] = a[2, 1];
            mat[2, 2] = a[2, 2];
        }

        public Mat3(double m00 = 1.0, double m01 = 0.0, double m02 = 0.0,
                    double m10 = 0.0, double m11 = 1.0, double m12 = 0.0,
                    double m20 = 0.0, double m21 = 0.0, double m22 = 1.0)
        {
            mat = new double[3, 3];
            mat[0, 0] = m00;
            mat[0, 1] = m01;
            mat[0, 2] = m02;

            mat[1, 0] = m10;
            mat[1, 1] = m11;
            mat[1, 2] = m12;

            mat[2, 0] = m20;
            mat[2, 1] = m21;
            mat[2, 2] = m22;
        }

        public double this[int r, int c]
        {
            get { return mat[r, c]; }
            set { mat[r, c] = value; }
        }

        public static bool operator ==(Mat3 a, Mat3 b)
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

        public static bool operator !=(Mat3 a, Mat3 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return String.Format("|{0,6:0.00}", mat[0, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[0, 1]) + ',' +
                    String.Format("{0,6:0.00}|", mat[0, 2]) + "\n" +

                    String.Format("|{0,6:0.00}", mat[1, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[1, 1]) + ',' +
                    String.Format("{0,6:0.00}|", mat[1, 2]) + "\n" +

                    String.Format("|{0,6:0.00}", mat[2, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[2, 1]) + ',' +
                    String.Format("{0,6:0.00}|", mat[2, 2]);

        }

        public static Mat3 operator *(Mat3 a, Mat3 b)
        {
            Mat3 temp = new Mat3();

            for (int r = 0; r < a.size; r++)
            {
                for (int c = 0; c < a.size; c++)
                {
                    temp[r, c] = a[r, 0] * b[0, c] +
                                 a[r, 1] * b[1, c] +
                                 a[r, 2] * b[2, c];
                }
            }
            return temp;
        }

        public static Mat3 operator *(Mat3 a, double b)
        {
            Mat3 temp = new Mat3(a);

            for (int r = 0; r < a.size; r++)
            {
                for (int c = 0; c < a.size; c++)
                {
                    temp[r, c] = temp[r, c] * b;
                }
            }
            return temp;
        }

        public void Transpose()
        {
            Mat3 temp = new Mat3(this);
            {
                for (int r = 0; r < temp.Size; r++)
                {
                    for (int c = 0; c < temp.Size; c++)
                    {
                        this[r, c] = temp[c, r];
                    }
                }
            }
        }

        public Mat2 Sub(int x, int y)
        {
            Mat2 sub = new Mat2();

            int i = 0;
            
            for(int r = 0; r < this.size; r++)
            {
                int j = 0;
                if (r == x)
                    continue;
                
                for(int c = 0; c < this.size; c++)
                {
                    if(c == y)
                    {
                        continue;
                    }

                    sub[i, j] = this.mat[r, c];
                    j++;
                }
                i++;
            }

            return sub;
        }

        public double Minor(int r, int c)
        {
            Mat2 sub = this.Sub(r, c);
            return sub.Det();
        }

        public double Cofactor(int r, int c)
        {
            double temp = this.Minor(r, c);
            
            if(((r+c) % 2) != 0)
            {
                temp = temp * -1.0;
            }

            return temp;
        }

        public double Det()
        {
            return this[0, 0] * this.Cofactor(0, 0) +
                    this[0, 1] * this.Cofactor(0, 1) +
                    this[0, 2] * this.Cofactor(0, 2);
        }

        public Mat3 Inverse()
        {

            Mat3 inverse = new Mat3();

            double det = Det();
            Console.WriteLine(det);

            if (!Utility.FE(det, 0.0f))
            {
                for (int r = 0; r < size; r++)
                {
                    for (int c = 0; c < size; c++)
                    {
                        inverse[r, c] = Cofactor(r, c);
                    }
                }
                inverse.Transpose();
                inverse = inverse * (1.0f / det);
            }
            return inverse;

        }


        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Mat3 other = obj as Mat3;

            if (    Utility.FE(this.mat[0, 0], other.mat[0, 0]) &&
                    Utility.FE(this.mat[0, 1], other.mat[0, 1]) &&
                    Utility.FE(this.mat[0, 2], other.mat[0, 2]) &&
                    Utility.FE(this.mat[1, 0], other.mat[1, 0]) &&
                    Utility.FE(this.mat[1, 1], other.mat[1, 1]) &&
                    Utility.FE(this.mat[1, 2], other.mat[1, 2]) &&
                    Utility.FE(this.mat[2, 0], other.mat[2, 0]) &&
                    Utility.FE(this.mat[2, 1], other.mat[2, 1]) &&
                    Utility.FE(this.mat[2, 2], other.mat[2, 2]))
                return true;

            return false;
        }

    }
}
