using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter1Test
    {
        [Test, Order(1)]
        public void T01_Tuples()
        {
            Point a = new Point(4.3f, -4.2f, 3.1f);
            Assert.AreEqual(4.3f, a.x);
            Assert.AreEqual(-4.2f, a.y);
            Assert.AreEqual(3.1f, a.z);
            Assert.AreEqual(1.0f, a.w);
            Assert.IsInstanceOf(typeof(Point), a);
            Assert.IsNotInstanceOf(typeof(Vector), a);
        }

        [Test, Order(2)]
        public void T02_Vectors()
        {
            Vector a = new Vector(4.3f, -4.2f, 3.1f);
            Assert.AreEqual(4.3f, a.x);
            Assert.AreEqual(-4.2f, a.y);
            Assert.AreEqual(3.1f, a.z);
            Assert.AreEqual(0.0f, a.w);
            Assert.IsNotInstanceOf(typeof(Point), a);
            Assert.IsInstanceOf(typeof(Vector), a);
        }


        [Test, Order(3)]
        public void T03_Adding()
        {
            Point p = new Point(3, -2, 5);
            Vector v = new Vector(-2, 3, 1);

            Assert.AreEqual(new Point(1, 1, 6), p + v);

        }

        [Test, Order(4)]
        public void T04_Subtraction()
        {
            Point p1 = new Point(3, 2, 1);
            Point p2 = new Point(5, 6, 7);
            Assert.AreEqual(new Vector(-2, -4, -6), p1 - p2);

            //Vector from point
            p1 = new Point(3, 2, 1);
            Vector v1 = new Vector(5, 6, 7);
            Assert.AreEqual(new Point(-2, -4, -6), p1 - v1);

            //Two Vectors
            v1 = new Vector(3, 2, 1);
            Vector v2 = new Vector(5, 6, 7);
            Assert.AreEqual(new Vector(-2, -4, -6), v1 - v2);

        }

        [Test, Order(5)]
        public void T05_Negation()
        {
            Vector zero = new Vector(0, 0, 0);
            Vector v = new Vector(1, -2, 3);
            Assert.AreEqual(new Vector(-1, 2, -3), zero - v);

            Point p = new Point(1, -2, 3);
            p.w = -4;

            p = -p;

            Point expected = new Point(-1, 2, -3);
            expected.w = 4;

            Assert.AreEqual(expected, p);

        }

        [Test, Order(6)]
        public void T06_ScalarMultiplication()
        {
            Point p = new Point(1, -2, 3, -4);
            Assert.AreEqual(new Point(3.5f, -7, 10.5f, -14), p * 3.5f);

            Point a = new Point(1, -2, 3, -4);
            Assert.AreEqual(new Point(0.5f, -1.0f, 1.5f, -2.0f), a * 0.5f);

        }

        [Test, Order(7)]
        public void T07_Magnitude()
        {
            Vector v = new Vector(1, 0, 0);
            Assert.AreEqual(1, v.Magnitude());

            v = new Vector(0, 1, 0);
            Assert.AreEqual(1, v.Magnitude());

            v = new Vector(0, 0, 1);
            Assert.AreEqual(1, v.Magnitude());

            v = new Vector(1, 2, 3);
            Assert.IsTrue(Utility.FE((double)Math.Sqrt(14), v.Magnitude()));

            v = new Vector(-1, -2, -3);
            Assert.IsTrue(Utility.FE((double)Math.Sqrt(14), v.Magnitude()));


        }


        [Test, Order(8)]
        public void T08_Normalization()
        {
            Vector v = new Vector(4, 0, 0);
            Assert.AreEqual(new Vector(1, 0, 0), v.Normalize());

            v = new Vector(1, 2, 3);
            v.Normalize();
            Assert.IsTrue( v == new Vector(0.26726, 0.53452, 0.80178));

            v = new Vector(1, 2, 3);
            Vector norm = v.Normalize();
            Assert.IsTrue(Utility.FE(1.0, norm.Magnitude()));

        }

        [Test, Order(9)]
        public void T09_DotProduct()
        {
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(2, 3, 4);
            Assert.AreEqual(20, a.Dot(b));
        }

        [Test, Order(10)]
        public void T10_CrossProduct()
        {
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(2, 3, 4);
            Assert.AreEqual(new Vector(-1, 2, -1), a.Cross(b));
            Assert.AreEqual(new Vector(1, -2, 1), b.Cross(a));
        }

    }
}
