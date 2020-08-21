using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    public class Chapter6Test
    {
        [Test, Order(1)]
        public void T01_NormalCalcs()
        {
            Sphere s = new Sphere();
            Vector n = s.GetNormal(new Point(1, 0, 0));
            Assert.AreEqual(new Vector(1, 0, 0), n);

            n = s.GetNormal(new Point(0, 1, 0));
            Assert.AreEqual(new Vector(0, 1, 0), n);

            n = s.GetNormal(new Point(0, 0, 1));
            Assert.AreEqual(new Vector(0, 0, 1), n);

            n = s.GetNormal(new Point((double)Math.Sqrt(3)/3f,
                                        (double)Math.Sqrt(3) / 3f,
                                        (double)Math.Sqrt(3) / 3f));
            Assert.AreEqual(new Vector((double)Math.Sqrt(3) / 3f,
                                        (double)Math.Sqrt(3) / 3f,
                                        (double)Math.Sqrt(3) / 3f),
                            n);

        }

        [Test, Order(2)]
        public void T02_Normalized()
        {
            Sphere s = new Sphere();
            Vector n = s.GetNormal(new Point((double)Math.Sqrt(3) / 3f,
                                        (double)Math.Sqrt(3) / 3f,
                                        (double)Math.Sqrt(3) / 3f));
            Assert.AreEqual(n, n.Normalize());
        }

        [Test, Order(3)]
        public void T03_TranslatedSphere()
        {
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.TranslateMatrix(0, 1, 0));
            Vector n = s.GetNormal(new Point(0, 1.70711f, -0.70711f));
            Assert.AreEqual(new Vector(0, 0.70711f, -0.70711f), n);

            s.SetMatrix(Mat4.ScaleMatrix(1, 0.5, 1) *
                            Mat4.RotateZMatrix(Constants.pi / 5));
            n = s.GetNormal(new Point(0, (double)Math.Sqrt(2)/2,
                                            (double)Math.Sqrt(2) / -2));
            Assert.AreEqual(new Vector(0.0, 0.97014, -0.24254),
                                n);
        }

        [Test, Order(4)]
        public void T04_ReflectingAVector()
        {
            Vector v = new Vector(1, -1, 0);
            Vector n = new Vector(0, 1, 0);
            Vector r = Vector.Reflect(v, n);
            Assert.AreEqual(new Vector(1, 1, 0), r);

            v = new Vector(0, -1, 0);
            n = new Vector((double)Math.Sqrt(2)/2f, (double)Math.Sqrt(2) / 2f,0);
            r = Vector.Reflect(v, n);
            Assert.AreEqual(new Vector(1, 0, 0), r);

        }

        [Test, Order(5)]
        public void T05_Lighting()
        {
            Color c = new Color(1, 1, 1);
            Point p = new Point(0, 0, 0);
            Light light = new Light(p, c);
            Assert.AreEqual(p, light.position);
            Assert.AreEqual(c, light.intensity);
        }

        [Test, Order(6)]
        public void T06_Material()
        {
            Material m = new Material();
            Assert.AreEqual(new Color(1, 1, 1), m.color);
            Assert.IsTrue(Utility.FE(0.1f, m.Ambient));
            Assert.IsTrue(Utility.FE(0.9f, m.Diffuse));
            Assert.IsTrue(Utility.FE(0.9f, m.Specular));
            Assert.IsTrue(Utility.FE(200f, m.Shinniness));
        }

        [Test, Order(7)]
        public void T07_SphereMaterial()
        {
            Sphere s = new Sphere();
            Assert.AreEqual(new Material(), s.material);

            Material m = new Material();
            m.Ambient = 1.0f;
            s.material = m;
            Assert.AreEqual(m, s.material);

        }

        [Test, Order(8)]
        public void T08_LightingResults()
        {
            Material m = new Material();
            Point p = new Point(0, 0, 0);

            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            RayObject test = new TestRayObject();
            Color result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(1.9f, 1.9f, 1.9f), result);

            eye = new Vector(0, (double)Math.Sqrt(2) / 2, (double)Math.Sqrt(2) / -2);
            normal = new Vector(0, 0, -1);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(1, 1, 1), result);

            eye = new Vector(0, 0, -1);
            normal = new Vector(0, 0, -1);
            light.position = new Point(0, 10, -10);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(0.7364f, 0.7364f, 0.7364f), result);

            eye = new Vector(0,
                            (double)Math.Sqrt(2) / -2f,
                            (double) Math.Sqrt(2) / -2f);
            normal = new Vector(0, 0, -1);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(1.6364f, 1.6364f, 1.6364f), result);

            eye = new Vector(0, 0, -1);
            normal = new Vector(0, 0, -1);
            light.position = new Point(0, 0, 10);
            result = test.Lighting(p, light, eye, normal, false);
            Assert.AreEqual(new Color(0.1f, 0.1f, 0.1f), result);
        }
    }
}
