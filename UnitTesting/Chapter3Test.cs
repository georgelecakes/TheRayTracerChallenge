using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter3Test
    {
        [Test, Order(1)]
        public void T01_MatrixConstructor()
        {
            Mat4 mat = new Mat4(1, 2, 3, 4,
                                5.5f, 6.5f, 7.5f, 8.5f,
                                9, 10, 11, 12,
                                13.5f, 14.5f, 15.5f, 16.5f);

            Assert.AreEqual(1,      mat[0, 0]);
            Assert.AreEqual(4,      mat[0, 3]);
            Assert.AreEqual(5.5f,   mat[1, 0]);
            Assert.AreEqual(7.5f,   mat[1, 2]);
            Assert.AreEqual(11,     mat[2, 2]);
            Assert.AreEqual(13.5f,  mat[3, 0]);
            Assert.AreEqual(15.5f,  mat[3, 2]);


            Mat2 mat2 = new Mat2(-3, 5, 1, -2);
            Assert.AreEqual(-3, mat2[0, 0]);
            Assert.AreEqual(5, mat2[0, 1]);
            Assert.AreEqual(1, mat2[1, 0]);
            Assert.AreEqual(-2, mat2[1, 1]);

            Mat3 mat3 = new Mat3(-3, 5, 0,
                                 1, -2, -7,
                                 0, 1, 1);
            Assert.AreEqual(-3, mat3[0,0]);
            Assert.AreEqual(-2, mat3[1, 1]);
            Assert.AreEqual(1, mat3[2, 2]);
        }

        [Test, Order(2)]
        public void T02_MatrixEquality()
        {
            Mat4 a = new Mat4(1,2,3,4,5,6,7,8,9,8,7,6,5,4,3,2);
            Mat4 b = new Mat4(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Assert.AreEqual(a, b);

            a = new Mat4(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            b = new Mat4(2,3,4,5,6,7,8,9,8,7,6,5,4,3,2,1);
            Assert.AreNotEqual(a, b);

        }

        [Test, Order(3)]
        public void T03_MatrixMultiplication()
        {
            Mat4 a = new Mat4(1,2,3,4,5,6,7,8,9,8,7,6,5,4,3,2);
            Mat4 b = new Mat4(-2, 1, 2, 3, 3, 2, 1, -1, 4, 3, 6, 5, 1, 2, 7, 8);
            Assert.AreEqual(new Mat4(20, 22, 50, 48, 44, 54, 114, 108, 40, 58, 110, 102, 16, 26, 46, 42),
                            a * b);
        }

        [Test, Order(4)]
        public void T04_MatrixTimesPointVector()
        {
            Mat4 mat = new Mat4(1, 2, 3, 4, 2, 4, 4, 2, 8, 6, 4, 1, 0, 0, 0, 1);
            Vector v = new Vector(1,2,3);
            Point p = new Point(1, 2, 3, 1);

            //1 * 1 + 2 * 2 + 3 * 3 + 4 * 0 = 14
            //2 * 1 + 4 * 2 + 4 * 3 + 2 * 0 = 22
            //1 * 8 + 2 * 6 + 3 * 4 + 1 * 0 = 32
            // 0 

            Assert.AreEqual(new Point(18, 24, 33, 1), mat * p);
            Assert.AreEqual(new Vector(14, 22, 32), mat * v);
        }

        [Test, Order(5)]
        public void T05_IdentityMatrix()
        {
            Mat4 a = new Mat4(0, 1, 2, 4, 1, 2, 4, 8, 2, 4, 8, 16, 4, 8, 16, 32);
            Assert.AreEqual(a, a * new Mat4());
            Assert.AreEqual(a, a * new Mat4().Identity());
        }

        [Test, Order(6)]
        public void T06_Transpose()
        {
            Mat4 a = new Mat4(0, 9, 3, 0, 9, 8, 0, 8, 1, 8, 5, 3, 0, 0, 5, 8);
            Assert.AreEqual(new Mat4(0, 9, 1, 0, 9, 8, 8, 0, 3, 0, 5, 5, 0, 8, 3, 8),
                a.Transpose());
        }

        [Test, Order(7)]
        public void T07_Det2x2()
        {
            Mat2 a = new Mat2(1, 5, -3, 2);
            Assert.AreEqual(17, a.Det());
        }

        [Test, Order(8)]
        public void T08_SubMatrix()
        {
            Mat3 a = new Mat3(1, 5, 0, -3, 2, 7, 0, 6, -3);
            Assert.AreEqual(new Mat2(-3, 2, 0, 6), a.Sub(0, 2));

            Mat4 mat4 = new Mat4(-6, 1, 1, 6,
                                -8, 5, 8, 6,
                                -1, 0, 8, 2,
                                -7, 1, -1, 1);
            Assert.AreEqual(new Mat3(-6, 1, 6, -8, 8, 6, -7, -1, 1), mat4.Sub(2, 1));
        }

        [Test, Order(9)]
        public void T09_Minors()
        {
            Mat3 mat3 = new Mat3(3, 5, 0, 2, -1, -7, 6, -1, 5);
            Mat2 b = mat3.Sub(1, 0);
            Assert.AreEqual(25, b.Det());
            Assert.AreEqual(25, mat3.Minor(1, 0));
        }

        [Test, Order(10)]
        public void T10_CoFactors()
        {
            Mat3 mat3 = new Mat3(3, 5, 0, 2, -1, -7, 6, -1, 5);
            Assert.AreEqual(-12, mat3.Minor(0,0));
            Assert.AreEqual(-12, mat3.Cofactor(0, 0));
            Assert.AreEqual(25, mat3.Minor(1, 0));
            Assert.AreEqual(-25, mat3.Cofactor(1, 0));
        }

        [Test, Order(11)]
        public void T11_Det()
        {
            Mat3 a = new Mat3(1, 2, 6, -5, 8, -4, 2, 6, 4);
            Assert.AreEqual(56, a.Cofactor(0, 0));
            Assert.AreEqual(12, a.Cofactor(0, 1));
            Assert.AreEqual(-46, a.Cofactor(0, 2));
            Assert.AreEqual(-196, a.Det());

            Mat4 b = new Mat4(-2,-8,3,5,-3,1,7,3,1,2,-9,6,-6,7,7,-9);
            Assert.AreEqual(-4071, b.Det());
        }

        [Test, Order(12)]
        public void T12_Invertability()
        {
            Mat4 mat4 = new Mat4(6, 4, 4, 4, 5, 5, 7, 6, 4, -9, 3, -7, 9, 1, 7, -6);
            Assert.AreEqual(-2120, mat4.Det());

            mat4 = new Mat4(-4, 2, -2, -3, 9, 6, 2, 6, 0, -5, 1, -5, 0, 0, 0, 0);
            Assert.AreEqual(0, mat4.Det());


        }

        [Test, Order(13)]
        public void T13_Inversion()
        {
            Mat4 a = new Mat4(-5, 2, 6, -8, 1, -5, 1, 8, 7, 7, -6, -7, 1, -3, 7, 4);
            Mat4 b = a.Inverse();
            Assert.AreEqual(532, a.Det());
            Assert.IsTrue(Utility.FE(-160/532f, b[3, 2]));
            Assert.IsTrue(Utility.FE(105f / 532, b[2, 3]));
            Assert.AreEqual(new Mat4(0.21805f, 0.45113f, 0.24060f, -0.04511f,
                                    -0.80827f, -1.45677f, -0.44361f, 0.52068f,
                                    -0.07895f, -0.22368f, -0.05263f, 0.19737f,
                                    -0.52256f, -0.81391f, -0.30075f, 0.30639f), b);
        }

        [Test, Order(14)]
        public void T14_MoreInversions()
        {
            Mat4 a = new Mat4(8, -5, 9, 2,
                                7, 5, 6, 1,
                                -6, 0, 9, 6,
                                -3, 0, -9, -4);
            Mat4 b = new Mat4(-0.15385f, -0.15385f, -0.28205f, -0.53846f,
                                -0.07692f, 0.12308f, 0.02564f, 0.03077f,
                                0.35897f, 0.35897f, 0.43590f, 0.92308f,
                                -0.69231f, -0.69231f, -0.76923f, -1.92308f);
            Assert.AreEqual(b, a.Inverse());

            a = new Mat4(9, 3, 0, 9,
                        -5, -2, -6, -3,
                        -4, 9, 6, 4,
                        -7, 6, 6, 2);
            b = new Mat4(-0.04074f, -0.07778f, 0.14444f, -0.22222f,
                        -0.07778f, 0.03333f, 0.36667f, -0.33333f,
                        -0.02901f, -0.14630f, -0.10926f, 0.12963f,
                        0.17778f, 0.06667f, -0.26667f, 0.33333f);
            Assert.AreEqual(b, a.Inverse());


        }

        [Test, Order(15)]
        public void T15_ProductOfInverse()
        {
            Mat4 a = new Mat4(3, -9, 7, 3, 3, -8, 2, -9, -4, 4, 4, 1, -6, 5, -1, 1);
            Mat4 b = new Mat4(8, 2, 2, 2, 3, -1, 7, 0, 7, 0, 5, 4, 6, -2, 0, 5);
            Mat4 c = a * b;
            Assert.AreEqual(a, c * b.Inverse());
        }


    }
}
