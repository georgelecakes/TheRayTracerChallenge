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
    public class Chapter10Test
    {
        [Test, Order(1)]
        public void T01_StripePattern()
        {
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white), 
                                                      new SolidColorPattern(Color.black));
            Assert.AreEqual(Color.white, pattern.a.PatternAt(new Point()));
            Assert.AreEqual(Color.black, pattern.b.PatternAt(new Point()));
        }

        [Test, Order(2)]
        public void T02_StripePatternResults()
        {
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            Color c = pattern.PatternAt(new Point(0,0,0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 1, 0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 2, 0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 0, 0));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 0, 1));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 0, 2));
            Assert.AreEqual(Color.white, c);

            c = pattern.PatternAt(new Point(0, 0, 0));
            Assert.AreEqual(Color.white, c);
            c = pattern.PatternAt(new Point(0.9, 0, 0));
            Assert.AreEqual(Color.white, c);
            c = pattern.PatternAt(new Point(1.0, 0, 0));
            Assert.AreEqual(Color.black, c);
            c = pattern.PatternAt(new Point(-0.1, 0, 0));
            Assert.AreEqual(Color.black, c);
            c = pattern.PatternAt(new Point(-1.0, 0, 0));
            Assert.AreEqual(Color.black, c);
            c = pattern.PatternAt(new Point(-1.1, 0, 0));
            Assert.AreEqual(Color.white, c);
        }

        [Test, Order(3)]
        public void T03_LightingPattern()
        {
            if (Scene.current != null)
                Scene.current.Clear();

            Sphere sphere = new Sphere();
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            Material material = new Material();
            material.pattern = pattern;
            material.Ambient = 1.0;
            material.Diffuse = 0.0;
            material.Specular = 0.0;
            sphere.material = material;
            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            Color c1 = sphere.Lighting(new Point(0.9, 0, 0), light, eye, normal, false);
            Color c2 = sphere.Lighting(new Point(1.1, 0, 0), light, eye, normal, false);
            Assert.AreEqual(Color.white, c1);
            Assert.AreEqual(Color.black, c2);
        }

        [Test, Order(4)]
        public void T04_StripeTransformation()
        {
            if(Scene.current != null)
            {
                Scene.current.Clear();
            }

            Sphere sphere = new Sphere();
            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            StripePattern pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            Color color = pattern.PatternAtObject(sphere, new Point(1.5, 0, 0));
            Assert.AreEqual(Color.white, color);

            sphere.SetMatrix(new Mat4());
            pattern.matrix = Mat4.ScaleMatrix(2, 2, 2);
            color = pattern.PatternAtObject(sphere, new Point(1.5, 0, 0));
            Assert.AreEqual(Color.white, color);

            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            pattern.matrix = Mat4.TranslateMatrix(0.5, 0, 0);
            color = pattern.PatternAtObject(sphere, new Point(2.5, 0, 0));
            Assert.AreEqual(Color.white, color);

        }

        [Test, Order(5)]
        public void T05_TestPattern()
        {

            if(Scene.current != null)
            {
                Scene.current.Clear();
            }

            Pattern pattern = new TestPattern();

            Assert.AreEqual(new Mat4(), pattern.matrix);

            pattern.matrix = Mat4.TranslateMatrix(1, 2, 3);
            Assert.AreEqual(Mat4.TranslateMatrix(1, 2, 3), pattern.matrix);

            Sphere sphere = new Sphere();
            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            sphere.material.pattern = new TestPattern();
            Color c = sphere.material.pattern.PatternAtObject(sphere, new Point(2, 3, 4));
            Assert.AreEqual(new Color(1, 1.5, 2), c);

            sphere.SetMatrix(new Mat4());
            sphere.material.pattern.matrix = Mat4.ScaleMatrix(2,2,2);
            c = sphere.material.pattern.PatternAtObject(sphere, new Point(2, 3, 4));
            Assert.AreEqual(new Color(1, 1.5, 2), c);

            sphere.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            sphere.material.pattern.matrix = Mat4.TranslateMatrix(0.5, 1, 1.5);
            c = sphere.material.pattern.PatternAtObject(sphere, new Point(2.5, 3, 3.5));
            Assert.AreEqual(new Color(0.75, 0.5, 0.25), c);

        }

        [Test, Order(6)]
        public void T06_Gradient()
        {
            Pattern pattern = new GradientPattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(new Color(0.75, 0.75, 0.75), pattern.PatternAt(new Point(0.25, 0, 0)));
            Assert.AreEqual(new Color(0.5, 0.5, 0.5), pattern.PatternAt(new Point(0.5, 0, 0)));
            Assert.AreEqual(new Color(0.25, 0.25, 0.25), pattern.PatternAt(new Point(0.75, 0, 0)));
        }


        [Test, Order(7)]
        public void T07_Ring()
        {
            Pattern pattern = new RingPattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(1, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0, 0, 1)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0.708, 0, 0.708)));
        }

        [Test, Order(8)]
        public void T08_3DChecker()
        {
            Pattern pattern = new CheckersPattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0.99, 0, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(1.01, 0, 0)));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0.99, 0)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0, 1.01, 0)));

            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0)));
            Assert.AreEqual(Color.white, pattern.PatternAt(new Point(0, 0, 0.99)));
            Assert.AreEqual(Color.black, pattern.PatternAt(new Point(0, 0, 1.01)));
        }

        [Test, Order(9)]
        public void T09_BasicScene()
        {
            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 5, -5), Color.white);
            Light light2 = new Light(new Point(5, 0.4, -5), new Color(0.6, 0.6, 0.15));
            Plane floor = new Plane();
            floor.material = new Material(new Color(1, 0, 0));
            floor.material.pattern = new RingPattern(new SolidColorPattern(Color.red),
                                                      new SolidColorPattern(Color.green));

            Plane wall = new Plane();
            wall.material = new Material(new Color(0, 0, 1));
            wall.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2.0) *
                            Mat4.TranslateMatrix(0, 0, 4));
            wall.material.pattern = new StripePattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                                Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = new CheckersPattern(new SolidColorPattern(Color.yellow),
                                                            new SolidColorPattern(Color.blue));

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1.0, -1));
            sphere2.material.pattern = new GradientPattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.green));

            Camera camera = new Camera(640, 480, Constants.pi / 3.0);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 2, -10),
                                    new Point(0, 2, 4),
                                    new Vector(0, 1, 0));


            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter10_BasicScene");
        }

        [Test, Order(10)]
        public void T10_RadialGradientPattern()
        {
            Pattern pattern = new RadialGradientPattern();
            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 5, -5), Color.white);
            Light light2 = new Light(new Point(5, 0.4, -5), new Color(0.6, 0.6, 0.15));
            Plane floor = new Plane();
            floor.material = new Material(new Color(1, 0, 0));
            floor.material.pattern = new RingPattern(new SolidColorPattern(Color.white),
                                                      new SolidColorPattern(Color.black));

            Plane wall = new Plane();
            wall.material = new Material(new Color(0, 0, 1));
            wall.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2.0) *
                            Mat4.TranslateMatrix(0, 0, 4));
            wall.material.pattern = pattern;

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                                Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = pattern;

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1.0, -1));
            sphere2.material.pattern = pattern;

            Camera camera = new Camera(640, 480, Constants.pi / 3.0);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 2, -10),
                                    new Point(0, 2, 4),
                                    new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter10_RadialGradient");

        }

        [Test, Order(11)]
        public void T11_NestedPatterns()
        {
            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            //RadialGradientPattern radialPattern = new RadialGradientPattern();
            RingPattern checkerboard = new RingPattern();
            checkerboard.a = new CheckersPattern(new SolidColorPattern(Color.white), new SolidColorPattern(Color.black));
            checkerboard.b = new CheckersPattern(new SolidColorPattern(Color.red), new SolidColorPattern(Color.green));

            Light light = new Light(new Point(-5, 5, -5), Color.white);
            Plane floor = new Plane();
            floor.material.pattern = checkerboard;

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2.0) *
                            Mat4.TranslateMatrix(0, 0, 4.1));
            wall.material.pattern = checkerboard;
            List<RayObject> temp = Scene.current.GetRayObjects();
            
            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                                Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = checkerboard;

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1.0, -1));
            sphere2.material.pattern = checkerboard;
            
            Camera camera = new Camera(640, 480, Constants.pi / 3.0);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 2, -10),
                                    new Point(0, 2, 4),
                                    new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter10_NestedPatterns");



        }

        [Test, Order(12)]

        public void BlendTPatterns()
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

            Light light = new Light(new Point(-5, 5, -5), Color.white);
            Plane floor = new Plane();
            floor.material.pattern = blend;

            Plane wall = new Plane();
            wall.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2.0) *
                            Mat4.TranslateMatrix(0, 0, 4.1));
            wall.material.pattern = blend;

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                                Mat4.ScaleMatrix(0.5, 0.5, 0.5));
            sphere1.material.pattern = blend;

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1.0, -1));
            sphere2.material.pattern = blend;

            Camera camera = new Camera(640, 480, Constants.pi / 3.0);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 2, -10),
                                    new Point(0, 2, 4),
                                    new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter10_BlendPatterns");


        }


    }
}
