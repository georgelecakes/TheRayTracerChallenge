using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    public class Chapter13Test
    {
        [Test, Order(1)]
        public void T01_RayMissesCylinder()
        {
            Cylinder c = new Cylinder();
            Vector direction0 = new Vector(0, 1, 0).Normalize();
            Vector direction1 = new Vector(0, 1, 0).Normalize();
            Vector direction2 = new Vector(1, 1, 1).Normalize();
            Ray r0 = new Ray(new Point(1, 0, 0), direction0);
            Ray r1 = new Ray(new Point(0, 0, 0), direction1);
            Ray r2 = new Ray(new Point(0, 0, -5), direction2);

            List<Intersection> xs = c.Intersect(r0);
            Assert.AreEqual(0, xs.Count);
            xs = c.Intersect(r1);
            Assert.AreEqual(0, xs.Count);
            xs = c.Intersect(r2);
            Assert.AreEqual(0, xs.Count);
        }

        [Test, Order(2)]
        public void T02_RayHitsCylinder()
        {
            Cylinder c = new Cylinder();
            Vector d1 = new Vector(0, 0, 1);
            Vector d2 = new Vector(0, 0, 1);
            Vector d3 = new Vector(0.1, 1, 1);
            d3.Normalize();

            Ray r1 = new Ray(new Point(1, 0, -5), d1);
            Ray r2 = new Ray(new Point(0, 0, -5), d2);
            Ray r3 = new Ray(new Point(0.5, 0, -5), d3);

            List<Intersection> xs = c.Intersect(r1);
            Assert.IsTrue(Utility.FE(5, xs[0].t));
            Assert.IsTrue(Utility.FE(5, xs[1].t));
            xs = c.Intersect(r2);
            Assert.IsTrue(Utility.FE(4, xs[0].t));
            Assert.IsTrue(Utility.FE(6, xs[1].t));
            xs = c.Intersect(r3);
            Assert.IsTrue(Utility.FE(6.80798, xs[0].t));
            Assert.IsTrue(Utility.FE(7.08872, xs[1].t));
        }

        [Test, Order(3)]
        public void T03_CylinderNormal()
        {
            Cylinder c = new Cylinder();

            Point[] p = new Point[4];
            p[0] = new Point(1, 0, 0);
            p[1] = new Point(0, 5, -1);
            p[2] = new Point(0, -2, 1);
            p[3] = new Point(-1, 1, 0);

            Vector[] v = new Vector[4];
            v[0] = new Vector(1, 0, 0);
            v[1] = new Vector(0, 0, -1);
            v[2] = new Vector(0, 0, 1);
            v[3] = new Vector(-1, 0, 0);

            Assert.AreEqual(v[0], c.GetNormal(p[0]));
            Assert.AreEqual(v[1], c.GetNormal(p[1]));
            Assert.AreEqual(v[2], c.GetNormal(p[2]));
            Assert.AreEqual(v[3], c.GetNormal(p[3]));
        }

        [Test, Order(4)]
        public void T04_MinMaxBounds()
        {
            Cylinder c = new Cylinder();
            Assert.AreEqual(double.NegativeInfinity, c.minimum);
            Assert.AreEqual(double.PositiveInfinity, c.maximum);
        }

        [Test, Order(5)]
        public void T05_TrucatedCylinder()
        {
            Cylinder c = new Cylinder();
            c.minimum = 1;
            c.maximum = 2;
            Vector[] d = new Vector[6];
            d[0] = new Vector(0.1, 1, 0).Normalize();
            d[1] = new Vector(0, 0, 1).Normalize();
            d[2] = new Vector(0, 0, 1).Normalize();
            d[3] = new Vector(0, 0, 1).Normalize();
            d[4] = new Vector(0, 0, 1).Normalize();
            d[5] = new Vector(0, 0, 1).Normalize();

            Point[] p = new Point[6];
            p[0] = new Point(0, 1.5, 0);
            p[1] = new Point(0, 3, -5);
            p[2] = new Point(0, 0, -5);
            p[3] = new Point(0, 2, -5);
            p[4] = new Point(0, 1, -5);
            p[5] = new Point(0, 1.5, -2);

            Ray[] r = new Ray[6];
            r[0] = new Ray(p[0], d[0]);
            r[1] = new Ray(p[1], d[1]);
            r[2] = new Ray(p[2], d[2]);
            r[3] = new Ray(p[3], d[3]);
            r[4] = new Ray(p[4], d[4]);
            r[5] = new Ray(p[5], d[5]);

            Assert.AreEqual(0, c.Intersect(r[0]).Count);
            Assert.AreEqual(0, c.Intersect(r[1]).Count);
            Assert.AreEqual(0, c.Intersect(r[2]).Count);
            Assert.AreEqual(0, c.Intersect(r[3]).Count);
            Assert.AreEqual(0, c.Intersect(r[4]).Count);
            Assert.AreEqual(2, c.Intersect(r[5]).Count);
        }

        [Test, Order(6)]
        public void T06_ClosedCylinders()
        {
            Cylinder c = new Cylinder();
            Assert.IsFalse(c.isClosed);
        }

        [Test, Order(7)]
        public void T07_EndCaps()
        {
            Cylinder c = new Cylinder();
            c.minimum = 1;
            c.maximum = 2;
            c.isClosed = true;

            Vector[] d = new Vector[5];
            d[0] = new Vector(0, -1, 0);
            d[1] = new Vector(0, -1, 2);
            d[2] = new Vector(0, -1, 1);
            d[3] = new Vector(0, 1, 2);
            d[4] = new Vector(0, 1, 1);

            Point[] p = new Point[5];
            p[0] = new Point(0, 3, 0);
            p[1] = new Point(0, 3, -2);
            p[2] = new Point(0, 4, -2);
            p[3] = new Point(0, 0, -2);
            p[4] = new Point(0, -1, -2);

            //Assert.AreEqual(2, c.Intersect(new Ray(p[0], d[0])));
            Assert.AreEqual(2, c.Intersect(new Ray(p[1], d[1])).Count);
            Assert.AreEqual(2, c.Intersect(new Ray(p[2], d[2])).Count);
            Assert.AreEqual(2, c.Intersect(new Ray(p[3], d[3])).Count);
            Assert.AreEqual(2, c.Intersect(new Ray(p[4], d[4])).Count);
        }

        [Test, Order(8)]
        public void T08_EndCapNormal()
        {
            Cylinder c = new Cylinder();
            c.minimum = 1;
            c.maximum = 2;
            c.isClosed = true;

            Vector[] n = new Vector[6];
            n[0] = new Vector(0, -1, 0);
            n[1] = new Vector(0, -1, 0);
            n[2] = new Vector(0, -1, 0);
            n[3] = new Vector(0, 1, 0);
            n[4] = new Vector(0, 1, 0);
            n[5] = new Vector(0, 1, 0);

            Point[] p = new Point[6];
            p[0] = new Point(0, 1, 0);
            p[1] = new Point(0.5, 1, 0);
            p[2] = new Point(0, 1, 0.5);
            p[3] = new Point(0, 2, 0);
            p[4] = new Point(0.5, 2, 0);
            p[5] = new Point(0, 2, 0.5);

            Assert.AreEqual(n[0], c.GetNormal(p[0]));
            Assert.AreEqual(n[1], c.GetNormal(p[1]));
            Assert.AreEqual(n[2], c.GetNormal(p[2]));
            Assert.AreEqual(n[3], c.GetNormal(p[3]));
            Assert.AreEqual(n[4], c.GetNormal(p[4]));
            Assert.AreEqual(n[5], c.GetNormal(p[5]));

        }

        [Test, Order(9)]
        public void T09_IntersectConeWithRay()
        {
            Cone cone = new Cone();
            Vector[] dir = new Vector[3];
            dir[0] = new Vector(0, 0, 1).Normalize();
            dir[1] = new Vector(1, 1, 1).Normalize();
            dir[2] = new Vector(-0.5, -1, 1).Normalize();

            Ray[] r = new Ray[3];
            r[0] = new Ray(new Point(0, 0, -5), dir[0]);
            r[1] = new Ray(new Point(0, 0, -5), dir[1]);
            r[2] = new Ray(new Point(1, 1, -5), dir[2]);

            List<Intersection> xs = cone.Intersect(r[0]);
            Assert.IsTrue(Utility.FE(5, xs[0].t));
            Assert.IsTrue(Utility.FE(5, xs[1].t));

            xs = cone.Intersect(r[1]);
            Assert.IsTrue(Utility.FE(8.66025, xs[0].t));
            Assert.IsTrue(Utility.FE(8.66025, xs[1].t));

            xs = cone.Intersect(r[2]);
            Assert.IsTrue(Utility.FE(4.55006, xs[0].t));
            Assert.IsTrue(Utility.FE(49.44994, xs[1].t));

            Ray ray = new Ray(
                    new Point(0, 0, -1), new Vector(0, 1, 1).Normalize());

            xs = cone.Intersect(ray);

            Assert.AreEqual(1, xs.Count);

            Assert.IsTrue(Utility.FE(0.35355, xs[0].t));
        }

        [Test, Order(10)]
        public void T10_ConeCaps()
        {
            Cone cone = new Cone();
            cone.minimum = -0.5;
            cone.maximum = 0.5;

            cone.isClosed = true;

            Vector[] dir = new Vector[3];
            dir[0] = new Vector(0, 1, 0);
            dir[1] = new Vector(0, 1, 1);
            dir[2] = new Vector(0, 1, 0);

            Point[] p = new Point[3];
            p[0] = new Point(0, 0, -5);
            p[1] = new Point(0, 0, -0.25);
            p[2] = new Point(0, 0, -0.25);

            List<Intersection> xs = cone.Intersect(new Ray(p[0], dir[0]));
            Assert.AreEqual(0,xs.Count);
            xs = cone.Intersect(new Ray(p[1], dir[1]));
            Assert.AreEqual(2, xs.Count);
            xs = cone.Intersect(new Ray(p[2], dir[2]));
            Assert.AreEqual(4, xs.Count);
        }

        [Test, Order(11)]
        public void T11_ConeNormal()
        {
            Cone cone = new Cone();
            Point[] p = new Point[3];
            p[0] = new Point(0, 0, 0);
            p[1] = new Point(1, 1, 1);
            p[2] = new Point(-1, -1, 0);

            Vector[] n = new Vector[3];
            n[0] = new Vector(0, 0, 0);
            n[1] = new Vector(1, -Math.Sqrt(2), 1);
            n[2] = new Vector(-1, 1, 0);
            n[0].Normalize();
            n[1].Normalize();
            n[2].Normalize();
            //Stupid vectors in  the book aren't normalized ;-p

            Assert.AreEqual(n[0], cone.GetNormal(p[0]));
            Assert.AreEqual(n[1], cone.GetNormal(p[1]));
            Assert.AreEqual(n[2], cone.GetNormal(p[2]));

        }

        [Test, Order(12)]
        public void T12_IceCreamCone()
        {

            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Cone c = new Cone();
            c.minimum = 0;
            c.maximum = 1.0;
            c.material.pattern = new Patterns.CheckersPattern(new Patterns.SolidColorPattern(new Color(0.957, 0.80, 0.604)),
                                                                new Patterns.SolidColorPattern(new Color(0.925, 0.663, 0.333)));
            c.material.pattern.matrix = Mat4.ScaleMatrix(0.25, 0.25, 0.25);
            //how do I just use the top of the cone?
            c.SetMatrix(Mat4.TranslateMatrix(0,-3.0,0) * 
                            Mat4.ScaleMatrix(0.9, 3, 0.9));

            Light light = new Light(new Point(-3, 6, -2), new Color(0.9, 0.9, 0.9));
            Light light2 = new Light(new Point(3, -3, -2), new Color(0.4, 0.4, 0.4));


            //Stack of icecream ontop of cone
            Sphere icecream1 = new Sphere();
            icecream1.material.color = new Color(0.957, 0.604, 0.761); // Pink
            icecream1.SetMatrix(Mat4.TranslateMatrix(0.0, 0.5, 0.00));
            Sphere icecream2 = new Sphere();
            icecream2.material.color = new Color(0.604, 0.957, 0.624);
            icecream2.SetMatrix(Mat4.TranslateMatrix(-0.1, 1.5, -0.05) *
                                Mat4.ScaleMatrix(0.9, 0.9, 0.9));
            Sphere icecream3 = new Sphere();
            icecream3.material.color = new Color(0.604, 0.937, 0.957);
            icecream3.SetMatrix(Mat4.TranslateMatrix(0.05, 2.7, 0.025) * 
                                Mat4.ScaleMatrix(0.8,0.8,0.8));

            Camera camera = new Camera(400, 800, Constants.pi / 3.0);
            camera.ViewTransform(new Point(0, 2, -8), new Point(0, 0, 4), new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter13_IceCreamCone");


        }

    }
}
