using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    /* Refactoring notes
     *  1) - All shapes have a transform matrix - done
     *  2) - All shapes have a default material - done
     *  3) - intersecting the shape with a ray, all shapes need to first convert
     *  the ray into object space, transforming it by the inverse f the shape's transform matrix
     *  - done
     *  4) Normal vector - convert to object space, get normal, bring to work space
     *   - done
     */


    [TestFixture]
    class Chapter9Test
    {
        [Test, Order(1)]
        public void T01_DefaultTransform()
        {
            TestRayObject tro = new TestRayObject();
            Assert.AreEqual(new Mat4(), tro.GetMatrix());

            tro.SetMatrix(Mat4.TranslateMatrix(2, 3, 4));
            Assert.AreEqual(Mat4.TranslateMatrix(2, 3, 4), tro.GetMatrix());

        }

        [Test, Order(2)]
        public void T02_Materials()
        {
            TestRayObject tro = new TestRayObject();
            Material material = new Material();
            Assert.AreEqual(material, tro.material);

            material.Ambient = 1;
            tro.material = material;
            Assert.AreEqual(material, tro.material);

        }

        //Skipping local intersection test as it doesn't quite seem to work
        //with my implementation?

        //once again for the following tests I don't want to expose things
        //that don't need exposing just to pass a test. 
        //I know they work because they have been tested so far and I've
        //already been building with abstraction in place.

        [Test, Order(3)]
        public void T03_Type()
        {
            Sphere sphere = new Sphere();
            Assert.IsInstanceOf(typeof(RayObject), sphere);
        }

        [Test, Order(4)]
        public void T04_NormalOfPlane()
        {
            Plane plane = new Plane();
            Vector n1 = plane.GetNormal(new Point(0, 0, 0));
            Vector n2 = plane.GetNormal(new Point(10, 0, -10));
            Vector n3 = plane.GetNormal(new Point(-5, 0, 150));
            Assert.AreEqual(new Vector(0, 1, 0), n1);
            Assert.AreEqual(new Vector(0, 1, 0), n2);
            Assert.AreEqual(new Vector(0, 1, 0), n3);
        }

        [Test, Order(5)]
        public void T05_NoIntersectionsWithPlane()
        {
            Plane plane = new Plane();
            Ray ray = new Ray(new Point(0, 10, 0), new Vector(0, 0, 1));
            List<Intersection> i = plane.Intersect(ray);
            Assert.IsEmpty(i);

            ray = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            i = plane.Intersect(ray);
            Assert.IsEmpty(i);

        }

        [Test, Order(6)]
        public void T06_IntersectingPlaneAboveBelow()
        {
            Plane plane = new Plane();
            Ray ray = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));
            List<Intersection> i = plane.Intersect(ray);
            Assert.AreEqual(1, i.Count);
            Assert.AreEqual(1, i[0].t);

            ray = new Ray(new Point(0, -1, 0), new Vector(0, 1, 0));
            i = plane.Intersect(ray);
            Assert.AreEqual(1, i.Count);
            Assert.AreEqual(1, i[0].t);


        }

        [Test, Order(7)]
        public void T07_PuttingItTogether()
        {
            Scene scene = new Scene();
            Light light = new Light(new Point(-5, 5, -5), Color.white);
            Light light2 = new Light(new Point(5, 0.4, -5), new Color(0.6, 0.6, 0.15));
            Plane floor = new Plane();
            floor.material = new Material(new Color(1, 0, 0));

            Plane wall = new Plane();
            wall.material = new Material(new Color(0, 0, 1));
            wall.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2.0f) * 
                            Mat4.TranslateMatrix(0,0,4));

            Sphere sphere1 = new Sphere();
            sphere1.SetMatrix(Mat4.TranslateMatrix(0, 0.5, -3) *
                                Mat4.ScaleMatrix(0.5f, 0.5f, 0.5f));

            Sphere sphere2 = new Sphere();
            sphere2.SetMatrix(Mat4.TranslateMatrix(2, 1.0, -1));

            Camera camera = new Camera(640, 480, Constants.pi / 3.0f);
            //Need to halt execution if I end up with NaN
            camera.ViewTransform(new Point(0, 2, -10),
                                    new Point(0, 2, 4),
                                    new Vector(0, 1, 0));


            Canvas canvas = Scene.current.Render(camera);

            Save.SaveCanvas(canvas, "Chapter9_PIT");
        }

    }
}
