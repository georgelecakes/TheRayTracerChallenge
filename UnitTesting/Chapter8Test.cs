using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    class Chapter8Test
    {
        [Test, Order(1)]
        public void T01_LightingWithTheSurfaceInShadow()
        {
            Scene scene = new Scene();
            Vector eye = new Vector(0, 0, -1);
            Vector normal = new Vector(0, 0, -1);
            Light light = new Light(new Point(0,0,-10), new Color(1,1,1));
            bool inShadow = true;

            Sphere sphere = new Sphere();

            Color result = sphere.Lighting( sphere.GetPosition(),
                                            light,
                                            eye,
                                            normal,
                                            inShadow);
            Assert.AreEqual(new Color(0.1f, 0.1f, 0.1f), result);

        }

        [Test, Order(2)]
        public void T02_NoShadow()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();

            List<Light> lights = Scene.current.GetLights();

            Point point = new Point(0, 10, 0);
            Assert.IsFalse(Scene.current.IsShadowed(point, lights[0]));
        }

        [Test, Order(3)]
        public void T03_BetweenPointAndLight()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point point = new Point(10, -10, 10);
            Assert.IsTrue(Scene.current.IsShadowed(point, lights[0]));
        }

        [Test, Order(4)]
        public void T04_ObjectBehindLight()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point point = new Point(-20, 20, -20);
            Assert.IsFalse(Scene.current.IsShadowed(point, lights[0]));
        }

        [Test, Order(5)]
        public void T05_ObjectBehindPoint()
        {
            if (Scene.current == null)
                new Scene();
            Scene.current.Default();
            List<Light> lights = Scene.current.GetLights();
            Point p = new Point(-2, 2, -2);
            Assert.IsFalse(Scene.current.IsShadowed(p, lights[0]));
        }

        [Test, Order(6)]
        public void T06_ShadeHitIntersectionInShadow()
        {
            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Light light = new Light(new Point(0,0,-10), new Color(1,1,1));
            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();
            s2.SetMatrix(Mat4.TranslateMatrix(0, 0, 10));

            Ray ray = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));

            Intersection i = new Intersection(s2, 4);

            Computations c = Computations.Prepare(i, ray, null);

            Color color = Scene.current.ShadeHit(c);

            Assert.AreEqual(new Color(0.1f, 0.1f, 0.1f), color);
        }

        [Test, Order(7)]
        public void T07_OffsetHit()
        {
            Ray ray = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            if (Scene.current == null)
                new Scene();
            Scene.current.Clear();

            Sphere sphere = new Sphere();
            sphere.SetMatrix(Mat4.TranslateMatrix(0,0,1));
            Intersection i = new Intersection(sphere, 5);
            Computations c = Computations.Prepare(i, ray, null);
            Assert.IsTrue(c.overPoint.z < Constants.epsilon / -2.0f);
            Assert.IsTrue(c.point.z > c.overPoint.z);
        }

        [Test, Order(21)]

        public void T08_PuttingItAllTogether()
        {


            Scene scene = new Scene();
            scene.Default();
            scene.ClearRayObjects();

            RayObject floor = new Sphere();
            floor.SetMatrix(Mat4.ScaleMatrix(10f, 0.01f, 10f));
            floor.material = new Material();
            floor.material.color = new Color(1, 0.9f, 0.9f);
            floor.material.Specular = 0;

            RayObject leftWall = new Sphere();
            leftWall.SetMatrix(Mat4.TranslateMatrix(0, 0, 5) *
                                Mat4.RotateYMatrix(Constants.pi / -4.0f) *
                                Mat4.RotateXMatrix(Constants.pi / 2.0f) *
                                Mat4.ScaleMatrix(10, 0.01f, 10));

            leftWall.material = floor.material;

            RayObject rightWall = new Sphere();
            rightWall.SetMatrix(Mat4.TranslateMatrix(0, 0, 5) *
                                Mat4.RotateYMatrix(Constants.pi / 4.0f) *
                                Mat4.RotateXMatrix(Constants.pi / 2.0f) *
                                Mat4.ScaleMatrix(10, 0.01f, 10));

            RayObject middle = new Sphere();
            middle.SetMatrix(Mat4.TranslateMatrix(-0.5f, 1.0f, 0.5f));
            middle.material.color = new Color(0.1f, 1.0f, 0.5f);
            middle.material.Diffuse = 0.7f;
            middle.material.Specular = 0.3f;

            RayObject right = new Sphere();
            right.SetMatrix(Mat4.TranslateMatrix(1.5f, 0.5f, -0.5f) *
                            Mat4.ScaleMatrix(0.5f, 0.5f, 0.5f));
            right.material.color = new Color(0.5f, 1.0f, 0.1f);
            right.material.Diffuse = 0.7f;
            right.material.Specular = 0.3f;

            RayObject left = new Sphere();
            left.SetMatrix(Mat4.TranslateMatrix(-1.5f, 0.33f, -0.75f) *
                            Mat4.ScaleMatrix(0.33f, 0.33f, 0.33f));
            left.material.color = new Color(1, 0.8f, 0.1f);
            left.material.Diffuse = 0.7f;
            left.material.Specular = 0.3f;

            Light light = Scene.current.GetLights()[0];
            light.position = new Point(-5.0f, 5.0f, -5.0f);
            light.intensity = Color.white;

            Camera camera = new Camera(300, 229, Constants.pi / 3.0f);

            camera.ViewTransform(new Point(0, 1.5f, -5.0f),
                                    new Point(0, 1, 0),
                                    new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Ch8Challenge");

        }



    }
}
