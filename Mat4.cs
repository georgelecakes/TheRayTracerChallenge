using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Mat4
    {
        double[,] mat;
        int size = 4;

        public int Size
        {
            get { return size; }
        }

        public Mat4(Mat4 a)
        {
            mat = new double[4, 4];
            mat[0, 0] = a[0, 0];
            mat[0, 1] = a[0, 1];
            mat[0, 2] = a[0, 2];
            mat[0, 3] = a[0, 3];
            mat[1, 0] = a[1, 0];
            mat[1, 1] = a[1, 1];
            mat[1, 2] = a[1, 2];
            mat[1, 3] = a[1, 3];
            mat[2, 0] = a[2, 0];
            mat[2, 1] = a[2, 1];
            mat[2, 2] = a[2, 2];
            mat[2, 3] = a[2, 3];
            mat[3, 0] = a[3, 0];
            mat[3, 1] = a[3, 1];
            mat[3, 2] = a[3, 2];
            mat[3, 3] = a[3, 3];
        }

        public Mat4(double m00 = 1.0, double m01 = 0.0, double m02 = 0.0, double m03 = 0.0,
                    double m10 = 0.0, double m11 = 1.0, double m12 = 0.0, double m13 = 0.0,
                    double m20 = 0.0, double m21 = 0.0, double m22 = 1.0, double m23 = 0.0,
                    double m30 = 0.0, double m31 = 0.0, double m32 = 0.0, double m33 = 1.0)
        {
            mat = new double[4, 4];
            mat[0, 0] = m00;
            mat[0, 1] = m01;
            mat[0, 2] = m02;
            mat[0, 3] = m03;

            mat[1, 0] = m10;
            mat[1, 1] = m11;
            mat[1, 2] = m12;
            mat[1, 3] = m13;

            mat[2, 0] = m20;
            mat[2, 1] = m21;
            mat[2, 2] = m22;
            mat[2, 3] = m23;

            mat[3, 0] = m30;
            mat[3, 1] = m31;
            mat[3, 2] = m32;
            mat[3, 3] = m33;
        }

        public Mat4 Identity()
        {
            mat[0, 0] = 1;
            mat[0, 1] = 0;
            mat[0, 2] = 0;
            mat[0, 3] = 0;

            mat[1, 0] = 0;
            mat[1, 1] = 1;
            mat[1, 2] = 0;
            mat[1, 3] = 0;

            mat[2, 0] = 0;
            mat[2, 1] = 0;
            mat[2, 2] = 1;
            mat[2, 3] = 0;

            mat[3, 0] = 0;
            mat[3, 1] = 0;
            mat[3, 2] = 0;
            mat[3, 3] = 1;

            return this;

        }

        public double this[int r, int c]
        {
            get { return mat[r, c]; }
            set { mat[r, c] = value; }
        }

        public static bool operator ==(Mat4 a, Mat4 b)
        {
            for(int x = 0; x < a.size; x++)
            {
                for(int y = 0; y < a.size; y++)
                {
                    if(!RT.Utility.FE(a[x,y], b[x,y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Mat4 a, Mat4 b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return  String.Format("|{0,6:0.00}", mat[0, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[0, 1]) + ',' +
                    String.Format("{0,6:0.00}", mat[0, 2]) + ',' +
                    String.Format("{0,6:0.00}|", mat[0, 3]) + "\n" +

                    String.Format("|{0,6:0.00}", mat[1, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[1, 1]) + ',' +
                    String.Format("{0,6:0.00}", mat[1, 2]) + ',' +
                    String.Format("{0,6:0.00}|", mat[1, 3]) + "\n" +

                    String.Format("|{0,6:0.00}", mat[2, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[2, 1]) + ',' +
                    String.Format("{0,6:0.00}", mat[2, 2]) + ',' +
                    String.Format("{0,6:0.00}|", mat[2, 3]) + "\n" +

                    String.Format("|{0,6:0.00}", mat[3, 0]) + ',' +
                    String.Format("{0,6:0.00}", mat[3, 1]) + ',' +
                    String.Format("{0,6:0.00}", mat[3, 2]) + ',' +
                    String.Format("{0,6:0.00}|", mat[3, 3]);
        }

        public static Mat4 operator *(Mat4 a, Mat4 b)
        {
            Mat4 temp = new Mat4();

            for (int r = 0; r < a.size; r++)
            {
                for (int c = 0; c < a.size; c++)
                {
                    temp[r, c] = a[r, 0] * b[0, c] +
                                 a[r, 1] * b[1, c] +
                                 a[r, 2] * b[2, c] +
                                 a[r, 3] * b[3, c];
                }
            }
            return temp;
        }

        public static Mat4 operator *(Mat4 a, double b)
        {
            Mat4 temp = new Mat4(a);

            for (int r = 0; r < a.size; r++)
            {
                for (int c = 0; c < a.size; c++)
                {
                    temp[r, c] = temp[r, c] * b;
                }
            }
            return temp;
        }




        public Mat4 Transpose()
        {
            Mat4 temp = new Mat4(this);
            {
                for (int r = 0; r < temp.Size; r++)
                {
                    for (int c = 0; c < temp.Size; c++)
                    {
                        this[r, c] = temp[c, r];
                    }
                }
            }
            return this;
        }

        public Mat3 Sub(int x, int y)
        {
            Mat3 sub = new Mat3();

            int i = 0;

            for (int r = 0; r < this.size; r++)
            {
                int j = 0;
                if (r == x)
                    continue;

                for (int c = 0; c < this.size; c++)
                {
                    if (c == y)
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

        public double Det()
        {
            double det = 0.0;

            det = this[0, 0] * this.Sub(0, 0).Det() -
                  this[1, 0] * this.Sub(1, 0).Det() +
                  this[2, 0] * this.Sub(2, 0).Det() -
                  this[3, 0] * this.Sub(3, 0).Det();

            return det;
        }

        public Mat4 Adjugate()
        {
            Mat4 adj = new Mat4();

            //Grab all 16 sub matrices of 3x3 and get the determinate of each, then determine if positive or negative valud
            for(int r = 0; r < size; r++)
            {
                for(int c = 0; c < size; c++)
                {
                    adj[r, c] = this.Sub(r, c).Det();
                    if (((r + c) % 2) != 0)
                    {
                        adj[r, c] *= -1.0;
                    }
                        
                }
            }
            return adj;
        }

        public Mat4 Inverse()
        {
            Mat4 inverse = new Mat4();


            double det = Det();

            if(!Utility.FE(det, 0.0))
            {
                inverse = this.Adjugate().Transpose() * (1.0/det);
            }

            return inverse;

        }

        public static Mat4 TranslateMatrix(double x, double y, double z)
        {
            Mat4 temp = new Mat4();
            temp[0, 3] = x; //x
            temp[1, 3] = y; //y
            temp[2, 3] = z; //z
            return temp;
        }

        public static Mat4 TranslateMatrix(Point p1)
        {
            Mat4 temp = new Mat4();
            temp[0, 3] = p1.x; //x
            temp[1, 3] = p1.y; //y
            temp[2, 3] = p1.z; //z
            return temp;
        }

        public static Mat4 TranslateMatrix(Vector v1)
        {
            Mat4 temp = new Mat4();
            temp[0, 3] = v1.x; //x
            temp[1, 3] = v1.y; //y
            temp[2, 3] = v1.z; //z
            return temp;
        }

        public Mat4 Translate(double x, double y, double z)
        {

            Mat4 temp = TranslateMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 ScaleMatrix(double x, double y, double z)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = x; //x
            temp[1, 1] = y; //y
            temp[2, 2] = z; //z
            return temp;
        }


        public static Mat4 ScaleMatrix(Point p1)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = p1.x; //x
            temp[1, 1] = p1.y; //y
            temp[2, 2] = p1.z; //z
            return temp;
        }

        public static Mat4 ScaleMatrix(Vector v1)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = v1.x; //x
            temp[1, 1] = v1.y; //y
            temp[2, 2] = v1.z; //z
            return temp;
        }

        public Mat4 Scale(double x, double y, double z)
        {
            Mat4 temp = ScaleMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        //Order here matters, so be careful!
        public static Mat4 RotateMatrix(double x, double y, double z)
        {
            Mat4 tempX = Mat4.RotateXMatrix(x);
            Mat4 tempY = Mat4.RotateYMatrix(y);
            Mat4 tempZ = Mat4.RotateZMatrix(z);
            return tempZ * tempY * tempX;
        }

        public Mat4 Rotate(double x, double y, double z)
        {
            Mat4 temp = RotateMatrix(x, y, z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 RotateXMatrix(double x)
        {
            Mat4 temp = new Mat4();
            temp[1, 1] = (double)System.Math.Cos(x);
            temp[1, 2] = (double)System.Math.Sin(x) * -1.0;
            temp[2, 1] = (double)System.Math.Sin(x);
            temp[2, 2] = (double)System.Math.Cos(x);
            return temp;
        }

        public Mat4 RotateX(double x)
        {
            Mat4 temp = RotateXMatrix(x);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 RotateYMatrix(double y)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = (double)System.Math.Cos(y);
            temp[0, 2] = (double)System.Math.Sin(y);
            temp[2, 0] = (double)System.Math.Sin(y) * -1.0;
            temp[2, 2] = (double)System.Math.Cos(y);
            return temp;
        }

        public Mat4 RotateY(double y)
        {
            Mat4 temp = RotateYMatrix(y);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 RotateZMatrix(double z)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = (double)System.Math.Cos(z);
            temp[0, 1] = (double)System.Math.Sin(z) * -1.0;
            temp[1, 0] = (double)System.Math.Sin(z);
            temp[1, 1] = (double)System.Math.Cos(z);
            return temp;
        }

        public Mat4 RotateZ(double z)
        {
            Mat4 temp = RotateZMatrix(z);
            this.mat = (this * temp).mat;
            return this;
        }

        public static Mat4 ShearMatrix(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat4 temp = new Mat4();
            temp[0, 0] = 1.0;
            temp[0, 1] = xy;
            temp[0, 2] = xz;
            temp[0, 3] = 0.0;
            temp[1, 0] = yx;
            temp[1, 1] = 1.0;
            temp[1, 2] = yz;
            temp[1, 3] = 0.0;
            temp[2, 0] = zx;
            temp[2, 1] = zy;
            temp[2, 2] = 1.0;
            temp[2, 3] = 0.0;
            temp[3, 0] = 0.0;
            temp[3, 1] = 0.0;
            temp[3, 2] = 0.0;
            temp[3, 3] = 1.0;
            return temp;
        }

        public Mat4 Shear(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            Mat4 temp = ShearMatrix(xy, xz, yx, yz, zx, zy);
            this.mat = (this * temp).mat;
            return this;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Mat4 other = obj as Mat4;

            if (    Utility.FE(this.mat[0, 0], other.mat[0, 0]) &&
                    Utility.FE(this.mat[0, 1], other.mat[0, 1]) &&
                    Utility.FE(this.mat[0, 2], other.mat[0, 2]) &&
                    Utility.FE(this.mat[0, 3], other.mat[0, 3]) &&
                    Utility.FE(this.mat[1, 0], other.mat[1, 0]) &&
                    Utility.FE(this.mat[1, 1], other.mat[1, 1]) &&
                    Utility.FE(this.mat[1, 2], other.mat[1, 2]) &&
                    Utility.FE(this.mat[1, 3], other.mat[1, 3]) &&
                    Utility.FE(this.mat[2, 0], other.mat[2, 0]) &&
                    Utility.FE(this.mat[2, 1], other.mat[2, 1]) &&
                    Utility.FE(this.mat[2, 2], other.mat[2, 2]) &&
                    Utility.FE(this.mat[2, 3], other.mat[2, 3]) &&
                    Utility.FE(this.mat[3, 0], other.mat[3, 0]) &&
                    Utility.FE(this.mat[3, 1], other.mat[3, 1]) &&
                    Utility.FE(this.mat[3, 2], other.mat[3, 2]) &&
                    Utility.FE(this.mat[3, 3], other.mat[3, 3]))
                return true;

            return false;
        }


    }
}
