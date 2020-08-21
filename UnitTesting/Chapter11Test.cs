using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RT.Patterns;


namespace RT.UnitTesting
{
    [TestFixture]
    public class Chapter11Test
    {
        [Test, Order(1)]
        public void T01_ReflectiveMaterial()
        {
            Material m = new Material();

            Assert.AreEqual(0.0, m.Reflective);

        }

        [Test, Order(2)]
        public void T02_ReflectiveVector()
        {
            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Plane plane = new Plane();
            Ray ray = new Ray(new Point(0, 1, -1), new Vector(0, Math.Sqrt(2) / -2, Math.Sqrt(2) / 2));
            Intersection i = new Intersection(plane, Math.Sqrt(2));
            
            Computations c = Computations.Prepare(i, ray, null);
            Assert.AreEqual(new Vector(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2),
                            c.reflectVector);
        }

        [Test, Order(3)]
        public void T03_NonReflectiveSurface()
        {
            if (Scene.current == null)
                new Scene();

            Scene.current.Default();

            Ray ray = new Ray(new Point(0, 0, 0),
                                new Vector(0, 0, 1));
            RayObject object2 = Scene.current.GetRayObjects()[1];

            object2.material.Ambient = 1.0;

            Intersection i = new Intersection(object2, 1.0);

            Computations c = Computations.Prepare(i, ray, null);

            Color color = Scene.current.ReflectedColor(c);
            Assert.AreEqual(Color.black, color);
        }

        [Test, Order(4)]
        public void T04_ReflectiveSurface()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();

            Plane plane = new Plane();
            plane.material.Reflective = 0.5;
            plane.SetMatrix(Mat4.TranslateMatrix(0, -1, 0));

            Ray ray = new Ray(new Point(0, 0, -3),
                                new Vector(0, Math.Sqrt(2) / -2, Math.Sqrt(2) / 2));
            Intersection i = new Intersection(plane, Math.Sqrt(2));

            Computations c = Computations.Prepare(i, ray, null);
            Color color = Scene.current.ReflectedColor(c);
            Assert.AreEqual(new Color(0.19032, 0.2379, 0.14274), color);
        }

        [Test, Order(5)]
        public void T05_UpdateShadeHit()
        {
            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Default();

            Plane plane = new Plane();

            plane.material.Reflective = 0.5;
            plane.SetMatrix(Mat4.TranslateMatrix(0, -1, 0));

            Ray ray = new Ray(new Point(0, 0, -3), new Vector(0, Math.Sqrt(2) / -2, Math.Sqrt(2) / 2));
            Intersection i = new Intersection(plane, Math.Sqrt(2));
            Computations c = Computations.Prepare(i, ray, null);
            Color color = Scene.current.ShadeHit(c);
            Assert.AreEqual(new Color(0.87677, 0.92436, 0.82918), color);
        }

        [Test, Order(6)]
        public void T06_AvoidInfiniteRecursion()
        {
            if (Scene.current == null)
            {
                new Scene();
            }
            Scene.current.Clear();

            Light light = new Light(new Point(0,0,0), new Color(1,1,1));

            Plane plane = new Plane();
            plane.material.Reflective = 1;
            plane.SetMatrix(Mat4.TranslateMatrix(0, -1, 0));
            Plane upper = new Plane();
            upper.material.Reflective = 1;
            upper.SetMatrix(Mat4.TranslateMatrix(0, 1, 0));
            Ray ray = new Ray(new Point(0, 0, 0), new Vector(0, 1, 0));
            //Check that colorAt terminates successfully...
        }

        [Test, Order(7)]
        public void T07_LimitRecursion()
        {
            if (Scene.current == null)
            {
                new Scene();
            }
            Scene.current.Default();

            Plane plane = new Plane();
            plane.material.Reflective = 0.5;
            plane.SetMatrix(Mat4.TranslateMatrix(0, -1, 0));

            Ray ray = new Ray(new Point(0, 0, -3),
                    new Vector(0, Math.Sqrt(2) / -2, Math.Sqrt(2) / 2));
            Intersection i = new Intersection(plane, Math.Sqrt(2));

            Computations c = Computations.Prepare(i, ray, null);
            Color color = Scene.current.ReflectedColor(c, 0); //Number of recursions left
            Assert.AreEqual(Color.black, color);
        }

        [Test, Order(8)]
        public void T08_AddMaterialTransRefractive()
        {
            Material m = new Material();
            m.Transparency = 0.0;
            m.RefracIndex = 1.0;

            Assert.AreEqual(0.0, m.Transparency);
            Assert.AreEqual(1.0, m.RefracIndex);
        }

        [Test, Order(9)]
        public void T09_NIntersections()
        {
            Sphere a = new Sphere();
            a.material.Glassy();
            a.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            a.material.RefracIndex = 1.5;

            Sphere b = new Sphere();
            b.material.Glassy();
            b.SetMatrix(Mat4.TranslateMatrix(0, 0, -0.25));
            b.material.RefracIndex = 2.0;

            Sphere c = new Sphere();
            c.material.Glassy();
            c.SetMatrix(Mat4.TranslateMatrix(0, 0, 0.25));
            c.material.RefracIndex = 2.5;

            Ray ray = new Ray(new Point(0, 0, -4),
                                new Vector(0, 0, 1));

            List < Intersection > i = new List<Intersection>();
            i.Add(new Intersection(a, 2));
            i.Add(new Intersection(b, 2.75));
            i.Add(new Intersection(c, 3.25));
            i.Add(new Intersection(b, 4.75));
            i.Add(new Intersection(c, 5.25));
            i.Add(new Intersection(a, 6));

            List<double> n1 = new List<double>();
            n1.Add(1.0);
            n1.Add(1.5);
            n1.Add(2.0);
            n1.Add(2.5);
            n1.Add(2.5);
            n1.Add(1.5);

            List<double> n2 = new List<double>();
            n2.Add(1.5);
            n2.Add(2.0);
            n2.Add(2.5);
            n2.Add(2.5);
            n2.Add(1.5);
            n2.Add(1.0);

            Computations comp = Computations.Prepare(i[0], ray, i);

            Assert.IsTrue(Utility.FE(n1[0], comp.n1));
            Assert.IsTrue(Utility.FE(n2[0], comp.n2));

            comp = Computations.Prepare(i[1], ray, i);
            Assert.IsTrue(Utility.FE(n1[1], comp.n1));
            Assert.IsTrue(Utility.FE(n2[1], comp.n2));

            comp = Computations.Prepare(i[2], ray, i);
            Assert.IsTrue(Utility.FE(n1[2], comp.n1));
            Assert.IsTrue(Utility.FE(n2[2], comp.n2));

            comp = Computations.Prepare(i[3], ray, i);
            Assert.IsTrue(Utility.FE(n1[3], comp.n1));
            Assert.IsTrue(Utility.FE(n2[3], comp.n2));

            comp = Computations.Prepare(i[4], ray, i);
            Assert.IsTrue(Utility.FE(n1[4], comp.n1));
            Assert.IsTrue(Utility.FE(n2[4], comp.n2));

        }

        [Test, Order(10)]
        public void T10_UnderPoint()
        {

            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere sphere = new Sphere();
            sphere.material.Glassy();
            sphere.SetMatrix(Mat4.TranslateMatrix(0,0,1));
            Intersection i = new Intersection(sphere, 5);
            List<Intersection> xs = new List<Intersection>();
            xs.Add(i);

            Computations c = Computations.Prepare(i, ray, xs);
            Assert.IsTrue(c.underPoint.z > Constants.epsilon / 2);
            Assert.IsTrue(c.point.z < c.underPoint.z);

        }

        [Test, Order(11)]
        public void T11_RefractedColorOfOpaque()
        {
            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Scene.current.Default();

            RayObject ro1 = Scene.current.root.GetChildren()[0];

            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> xs = new List<Intersection>();
            xs.Add(new Intersection(ro1, 4));
            xs.Add(new Intersection(ro1, 6));

            Computations c = Computations.Prepare(xs[0], ray, xs);
            Color color = Scene.current.RefractedColor(c, 5);
            Assert.AreEqual(Color.black, color);
        }

        [Test, Order(12)]
        public void T12_RefractedColorAtMaxRecusionDepth()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Default();

            RayObject ro1 = Scene.current.root.GetChildren()[0];

            ro1.material.Transparency = 1;
            ro1.material.RefracIndex = 1.5;

            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));

            List<Intersection> xs = new List<Intersection>();
            xs.Add(new Intersection(ro1, 4));
            xs.Add(new Intersection(ro1, 6));

            Computations c = Computations.Prepare(xs[0], r, xs);

            Color color = Scene.current.RefractedColor(c, 0);
            Assert.AreEqual(Color.black, color);

        }

        [Test, Order(13)]
        public void T13_TotalInternalReflection()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Default();

            RayObject ro1 = Scene.current.root.GetChildren()[0];

            ro1.material.Transparency = 1.0;
            ro1.material.RefracIndex = 1.5;

            Ray r = new Ray(new Point(0, 0, Math.Sqrt(2) / 2),
                new Vector(0, 1, 0));
            List<Intersection> xs = new List<Intersection>();
            xs.Add(new Intersection(ro1, Math.Sqrt(2) / -2));
            xs.Add(new Intersection(ro1, Math.Sqrt(2) / 2));
            Computations c = Computations.Prepare(xs[1], r, xs);
            Color color = Scene.current.RefractedColor(c, 5);
            Assert.AreEqual(Color.black, color);
        }

        [Test, Order(14)]
        public void T14_FindRefractedColor()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Default();

            RayObject a = Scene.current.root.GetChildren()[0];

            a.material.Ambient = 1.0;
            a.material.pattern = new Patterns.TestPattern();

            RayObject b = Scene.current.root.GetChildren()[1];
            b.material.Transparency = 1.0;
            b.material.RefracIndex = 1.5;

            Ray ray = new Ray(new Point(0, 0, 0.1), new Vector(0, 1, 0));
            List<Intersection> xs = new List<Intersection>();

            xs.Add(new Intersection(a, -0.9899));
            xs.Add(new Intersection(b, -0.4899));
            xs.Add(new Intersection(b, 0.4899));
            xs.Add(new Intersection(a, 0.9899));

            Computations c = Computations.Prepare(xs[2], ray, xs);

            Color color = Scene.current.RefractedColor(c, 5);
            Assert.AreEqual(new Color(0, 0.99888, 0.04725), color);

        }

        [Test, Order(15)]
        public void T15_HandingRefractionInShadeHit()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Default();

            Plane plane = new Plane();
            plane.SetMatrix(Mat4.TranslateMatrix(0, -1, 0));
            plane.material.Transparency = 0.5;
            plane.material.RefracIndex = 1.5;

            Sphere ball = new Sphere();
            ball.material.color = new Color(1, 0, 0);
            ball.material.Ambient = 0.5;
            ball.SetMatrix(Mat4.TranslateMatrix(0, -3.5, -0.5));

            Ray r = new Ray(new Point(0, 0, -3), new Vector(0, Math.Sqrt(2) / -2,
                                                            Math.Sqrt(2) / 2));
            List<Intersection> xs = new List<Intersection>();
            xs.Add(new Intersection(plane, Math.Sqrt(2)));

            Computations c = Computations.Prepare(xs[0], r, xs);

            Color color = Scene.current.ShadeHit(c, 5);
            Assert.AreEqual(new Color(0.93642, 0.68642, 0.68642), color);

        }

        [Test, Order(16)]
        public void T16_ReflectanceUnderTotalInteralReflection()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Sphere sphere = new Sphere();
            sphere.material.Glassy();

            Ray ray = new Ray(new Point(0, 0, Math.Sqrt(2) / 2),
                        new Vector(0, 1, 0));

            List<Intersection> xs = new List<Intersection>();

            xs.Add(new Intersection(sphere, Math.Sqrt(2) / -2));
            xs.Add(new Intersection(sphere, Math.Sqrt(2) / 2));

            Computations c = Computations.Prepare(xs[1], ray, xs);

            double reflectance = Scene.current.Schlick(c);
            Assert.IsTrue(Utility.FE(1.0, reflectance));
        }

        //Currently not passing, but the values are close?
        [Test, Order(17)]
        public void T17_ReflectanceOfPerpRay()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Sphere sphere = new Sphere();
            sphere.material.Glassy();

            Ray ray = new Ray(new Point(0, 0, 0),
                        new Vector(0, 1, 0));

            List<Intersection> xs = new List<Intersection>();

            xs.Add(new Intersection(sphere, -1));
            xs.Add(new Intersection(sphere, 1));

            Computations c = Computations.Prepare(xs[1], ray, xs);

            double reflectance = Scene.current.Schlick(c);
            Assert.IsTrue(Utility.FE(0.04, reflectance));
        }

        //Currently not passing, but the values are close?
        [Test, Order(18)]
        public void T18_ReflectanceN2GreaterN1()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Sphere sphere = new Sphere();
            sphere.material.Glassy();

            Ray ray = new Ray(new Point(0, 0.99, -2),
                        new Vector(0, 0, 1));

            List<Intersection> xs = new List<Intersection>();

            xs.Add(new Intersection(sphere, 1.8589));

            Computations c = Computations.Prepare(xs[0], ray, xs);

            double reflectance = Scene.current.Schlick(c);
            Assert.IsTrue(Utility.FE(0.48873, reflectance));
        }

        [Test, Order(19)]
        public void T19_CombiningReflectionAndRefraction()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Default();

            Plane plane = new Plane();
            plane.SetMatrix(Mat4.TranslateMatrix(0, -1, 0));
            plane.material.Reflective = 0.5;
            plane.material.Transparency = 0.5;
            plane.material.RefracIndex = 1.5;

            Sphere ball = new Sphere();
            ball.material.color = new Color(1, 0, 0);
            ball.material.Ambient = 0.5;
            ball.SetMatrix(Mat4.TranslateMatrix(0, -3.5, -0.5));

            Ray r = new Ray(new Point(0, 0, -3), new Vector(0, Math.Sqrt(2) / -2,
                                                            Math.Sqrt(2) / 2));
            List<Intersection> xs = new List<Intersection>();
            xs.Add(new Intersection(plane, Math.Sqrt(2)));

            Computations c = Computations.Prepare(xs[0], r, xs);

            Color color = Scene.current.ShadeHit(c, 5);
            Assert.AreEqual(new Color(0.93391, 0.69643, 0.69243), color);

        }


        [Test, Order(20)]
        public void T20_TransparencyTest()
        {
            if (Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            //RadialGradientPattern radialPattern = new RadialGradientPattern();

            StripePattern s1 = new StripePattern(new SolidColorPattern(Color.white), new SolidColorPattern(Color.green));
            StripePattern s2 = new StripePattern(new SolidColorPattern(Color.white), new SolidColorPattern(Color.green));

            s1.matrix = Mat4.RotateYMatrix(Constants.pi / 2);

            BlendPattern blend = new BlendPattern(s1, s2);

            Plane floor = new Plane();
            floor.SetMatrix(Mat4.TranslateMatrix(0, -1.2, 0));
            floor.material.pattern = blend;

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2.0) *
                            Mat4.TranslateMatrix(0, 0, 4.1));
            wall.material.pattern = new Patterns.CheckersPattern();

            Light light = new Light(new Point(-5, 5, -5), Color.white);

            Sphere sphere1 = new Sphere();
            sphere1.material.Diffuse = 0.0;
            sphere1.material.Shinniness = 300;
            sphere1.material.Ambient = 0.0;
            sphere1.material.Reflective = 1.0;
            sphere1.material.RefracIndex = RefractiveIndex.Vacuum;
            sphere1.material.Transparency = 1.0;
            sphere1.canCastShadows = false;

            Camera camera = new Camera(150, 150, Constants.pi / 3.0);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 0, -3),
                                    new Point(0, 0, 4),
                                    new Vector(0, 1, 0));
            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter11_SphereTest");
        }

        [Test, Order(21)]
        public void T21_UnderwaterScene()
        {
            if (Scene.current == null)
                new Scene();


            Scene.current.Clear();

            Light light = new Light(new Point(-5, 5, -5), Color.white);
            
            Plane waterSurface = new Plane();

            waterSurface.material.color = new Color(0.3, 0.7, 0.99);
            waterSurface.material.Diffuse = 0.3;
            waterSurface.material.Reflective = 0.4;
            waterSurface.material.Transparency = 0.9;
            waterSurface.material.RefracIndex = RefractiveIndex.Water;
            waterSurface.canCastShadows = false;
            
            Plane waterBed = new Plane();

            waterBed.SetMatrix(Mat4.TranslateMatrix(0, -5, 0));
            waterBed.material.color = new Color(0.5, 0.25, 0.1);
            waterBed.material.Diffuse = 0.9;
            waterBed.material.Reflective = 0.0;
            waterBed.material.Transparency = 0.0;
            waterBed.material.Specular = 0.0;
            waterBed.material.RefracIndex = RefractiveIndex.Air;

            int numSpheres = 30;
            
            Point start = new Point(-10, -5, -10);
            Point end = new Point(10, -4, 10);

            Sphere current;
            for (int i = 0; i < numSpheres; i++)
            {
                current = new Sphere();
                current.material.color = new Color().Randomize();
                Console.WriteLine(current.material.color);
                current.SetMatrix(Mat4.TranslateMatrix(new Point().Randomize(start, end)));
                current.material.Diffuse = 0.9;
                current.material.Reflective = 0.5;
                current.material.Transparency = 0.65;
                current.material.Specular = 1.0;
                current.material.Shinniness = 300.0;
            }

            Camera camera = new Camera(500, 400, Constants.pi / 3.0);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 5, -20),
                                    new Point(0, -4, 10),
                                    new Vector(0, 1, 0));
            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter11_UnderwaterScene2");


        }


    }


}
