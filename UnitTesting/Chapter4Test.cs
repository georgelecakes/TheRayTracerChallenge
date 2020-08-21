using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    public class Chapter4Test
    {
        [Test, Order(1)]
        public void T01_TranslationMatrix()
        {
            Mat4 trans = Mat4.TranslateMatrix(5, -3, 2);
            Point p = new Point(-3, 4, 5);
            Assert.AreEqual(new Point(2, 1, 7), trans * p);

            Mat4 inverse = trans.Inverse();
            p = new Point(-3, 4, 5);
            Assert.AreEqual(new Point(-8, 7, 3), inverse * p);

            Vector v = new Vector(-3, 4, 5);
            Assert.AreEqual(v, trans * v);
        }

        [Test, Order(2)]
        public void T02_ScalingMatrix()
        {
            Mat4 scaling = Mat4.ScaleMatrix(2, 3, 4);
            Vector v = new Vector(-4, 6, 8);
            Assert.AreEqual(new Vector(-8, 18, 32), scaling * v);

            Mat4 inverse = scaling.Inverse();
            Assert.AreEqual(new Vector(-2, 2, 2), inverse * v);

            scaling = Mat4.ScaleMatrix(-1, 1, 1);
            Point p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(-2, 3, 4), scaling * p);
        }

        [Test, Order(3)]
        public void T03_Rotation()
        {
            Point p = new Point(0, 1, 0);
            Mat4 halfQuarter = Mat4.RotateXMatrix(Constants.pi / 4.0f);
            Mat4 fullQuarter = Mat4.RotateXMatrix(Constants.pi / 2.0f);
            Assert.AreEqual(new Point(0, (double)Math.Sqrt(2.0) / 2.0f, (double)Math.Sqrt(2.0) / 2.0f), halfQuarter * p);
            Assert.AreEqual(new Point(0, 0, 1), fullQuarter * p);

            Mat4 inverse = halfQuarter.Inverse();
            Assert.AreEqual(new Point(0, (double)Math.Sqrt(2.0) / 2.0f, (double)Math.Sqrt(2.0) / -2.0f), inverse * p);

            p = new Point(0, 0, 1);
            halfQuarter = Mat4.RotateYMatrix(Constants.pi / 4.0f);
            fullQuarter = Mat4.RotateYMatrix(Constants.pi / 2.0f);
            Assert.AreEqual(new Point( (double)Math.Sqrt(2.0) / 2.0f, 0, (double)Math.Sqrt(2.0) / 2.0f), halfQuarter * p);
            Assert.AreEqual(new Point(1, 0, 0), fullQuarter * p);

            p = new Point(0, 1, 0);
            halfQuarter = Mat4.RotateZMatrix(Constants.pi / 4.0f);
            fullQuarter = Mat4.RotateZMatrix(Constants.pi / 2.0f);
            Assert.AreEqual(new Point((double)Math.Sqrt(2.0) / -2.0f, (double)Math.Sqrt(2.0) / 2.0f, 0.0f), halfQuarter * p);
            Assert.AreEqual(new Point(-1, 0, 0), fullQuarter * p);
        }

        [Test, Order(4)]
        public void T04_ShearingMatrix()
        {
            Mat4 shearing = Mat4.ShearMatrix(1, 0, 0, 0, 0, 0);
            Point p = new Point(2, 3, 4);
            Assert.AreEqual(new Point(5, 3, 4), shearing * p);

            shearing = Mat4.ShearMatrix(0, 1, 0, 0, 0, 0);
            Assert.AreEqual(new Point(6,3,4), shearing * p);

            shearing = Mat4.ShearMatrix(0, 0, 1, 0, 0, 0);
            Assert.AreEqual(new Point(2,5,4), shearing * p);

            shearing = Mat4.ShearMatrix(0, 0, 0, 1, 0, 0);
            Assert.AreEqual(new Point(2, 7, 4), shearing * p);

            shearing = Mat4.ShearMatrix(0, 0, 0, 0, 1, 0);
            Assert.AreEqual(new Point(2, 3, 6), shearing * p);

            shearing = Mat4.ShearMatrix(0, 0, 0, 0, 0, 1);
            Assert.AreEqual(new Point(2, 3, 7), shearing * p);
        }

        [Test, Order(5)]
        public void T05_ChainingTransforms()
        {
            Point p = new Point(1, 0, 1);
            Mat4 A = Mat4.RotateXMatrix(Constants.pi / 2.0f);
            Mat4 B = Mat4.ScaleMatrix(5, 5, 5);
            Mat4 C = Mat4.TranslateMatrix(10, 5, 7);

            Point p2 = A * p;
            Assert.AreEqual(new Point(1, -1, 0), p2);

            Point p3 = B * p2;
            Assert.AreEqual(new Point(5, -5, 0), p3);

            Point p4 = C * p3;
            Assert.AreEqual(new Point(15, 0, 7), p4);

            Mat4 trans = C * B * A;
            Assert.AreEqual(new Point(15, 0, 7), trans * p);



        }

    }
}
