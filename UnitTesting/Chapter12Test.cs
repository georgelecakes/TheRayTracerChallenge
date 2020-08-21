using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{

    [TestFixture]
    public class Chapter12Test
    {
        [Test, Order(1)]
        public void T01_RayIntersectsCube()
        {
            Cube cube = new Cube();
            Ray[] r = new Ray[7];

            r[0] = new Ray(new Point(5, 0.5, 0), new Vector(-1, 0, 0));
            r[1] = new Ray(new Point(-5, 0.5, 0), new Vector(1, 0, 0));
            r[2] = new Ray(new Point(0.5, 5, 0), new Vector(0, -1, 0));
            r[3] = new Ray(new Point(0.5, -5, 0), new Vector(0, 1, 0));
            r[4] = new Ray(new Point(0.5, 0, 5), new Vector(0, 0, -1));
            r[5] = new Ray(new Point(0.5, 0, -5), new Vector(0, 0, 1));
            r[6] = new Ray(new Point(0, 0.5, 0), new Vector(0, 0, 1));

            double[] t1 = new double[7];
            double[] t2 = new double[7];

            t1[0] = 4;
            t2[0] = 6;
            t1[1] = 4;
            t2[1] = 6;
            t1[2] = 4;
            t2[2] = 6;
            t1[3] = 4;
            t2[3] = 6;
            t1[4] = 4;
            t2[4] = 6;
            t1[5] = 4;
            t2[5] = 6;
            t1[6] = -1;
            t2[6] = 1;

            List<Intersection> xs = cube.Intersect(r[0]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[0], xs[0].t);
            Assert.AreEqual(t2[0], xs[1].t);

            xs = cube.Intersect(r[1]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[1], xs[0].t);
            Assert.AreEqual(t2[1], xs[1].t);

            xs = cube.Intersect(r[2]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[2], xs[0].t);
            Assert.AreEqual(t2[2], xs[1].t);

            xs = cube.Intersect(r[3]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[3], xs[0].t);
            Assert.AreEqual(t2[3], xs[1].t);

            xs = cube.Intersect(r[4]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[4], xs[0].t);
            Assert.AreEqual(t2[4], xs[1].t);

            xs = cube.Intersect(r[5]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[5], xs[0].t);
            Assert.AreEqual(t2[5], xs[1].t);

            xs = cube.Intersect(r[6]);
            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(t1[6], xs[0].t);
            Assert.AreEqual(t2[6], xs[1].t);
        }

        [Test, Order(2)]
        public void T02_RayMissesCube()
        {
            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            Cube cube = new Cube();
            Ray[] r = new Ray[6];
            r[0] = new Ray(new Point(-2, 0, 0), new Vector(0.2673, 0.5345, 0.8018));
            r[1] = new Ray(new Point(0, -2, 0), new Vector(0.8018, 0.2673, 0.5345));
            r[2] = new Ray(new Point(0, 0, -2), new Vector(0.5345, 0.8018, 0.2673));
            r[3] = new Ray(new Point(2, 0, 2), new Vector(0, 0, -1));
            r[4] = new Ray(new Point(0, 2, 2), new Vector(0, -1, 0));
            r[5] = new Ray(new Point(2, 2, 0), new Vector(-1, 0, 0));

            Assert.AreEqual(0, cube.Intersect(r[0]).Count);
            Assert.AreEqual(0, cube.Intersect(r[1]).Count);
            Assert.AreEqual(0, cube.Intersect(r[2]).Count);
            Assert.AreEqual(0, cube.Intersect(r[3]).Count);
            Assert.AreEqual(0, cube.Intersect(r[4]).Count);
            Assert.AreEqual(0, cube.Intersect(r[5]).Count);
        }

        [Test, Order(3)]
        public void T03_NormalOnACube()
        {
            Cube cube = new Cube();
            Point[] p = new Point[8];

            p[0] = new Point(1, 0.5, -0.8);
            p[1] = new Point(-1, -0.2, 0.9);
            p[2] = new Point(-0.4, 1, -0.1);
            p[3] = new Point(0.3, -1, -0.7);
            p[4] = new Point(-0.6, 0.3, 1);
            p[5] = new Point(0.4, 0.4, -1);
            p[6] = new Point(1, 1, 1);
            p[7] = new Point(-1, -1, -1);

            Vector[] expectedNormal = new Vector[9];
            expectedNormal[0] = new Vector(1, 0, 0);
            expectedNormal[1] = new Vector(-1, 0, 0);
            expectedNormal[2] = new Vector(0, 1, 0);
            expectedNormal[3] = new Vector(0, -1, 0);
            expectedNormal[4] = new Vector(0, 0, 1);
            expectedNormal[5] = new Vector(0, 0, -1);
            expectedNormal[6] = new Vector(1, 0, 0);
            expectedNormal[7] = new Vector(-1, 0, 0);

            Assert.AreEqual(expectedNormal[0], cube.GetNormal(p[0]));
            Assert.AreEqual(expectedNormal[1], cube.GetNormal(p[1]));
            Assert.AreEqual(expectedNormal[2], cube.GetNormal(p[2]));
            Assert.AreEqual(expectedNormal[3], cube.GetNormal(p[3]));
            Assert.AreEqual(expectedNormal[4], cube.GetNormal(p[4]));
            Assert.AreEqual(expectedNormal[5], cube.GetNormal(p[5]));
            Assert.AreEqual(expectedNormal[6], cube.GetNormal(p[6]));
            Assert.AreEqual(expectedNormal[7], cube.GetNormal(p[7]));
        }

        [Test, Order(4)]
        public void T04_PuttingItAllTogether()
        {

            if(Scene.current == null)
            {
                new Scene();
            }

            Scene.current.Clear();

            //Create a cube room
            Light light = new Light(new Point(-4, 4, -3), Color.white);

            //Floor
            
            Cube floor = new Cube();
            floor.material.pattern = new Patterns.CheckersPattern();
            floor.material.Shinniness = 200.0;
            floor.material.Reflective = 0.5;
            floor.material.pattern.matrix = Mat4.ScaleMatrix(0.2, 0.2, 0.2);
            floor.SetMatrix(Mat4.ScaleMatrix(10,0.1,10));
            
            
            //Ceiling
            Cube ceiling = new Cube();
            ceiling.material.color = new Color(0.9, 0.9, 0.95);
            ceiling.SetMatrix(Mat4.TranslateMatrix(0, 5, 0) * Mat4.ScaleMatrix(10, 0.1, 10));
            
            //Back  Wall
            
            Cube backWall = new Cube();
            backWall.material.pattern = new Patterns.SolidColorPattern(new Color(0.6, 0.1, 0.05));
            backWall.material.color = new Color(0.6, 0.1, 0.05);
            backWall.material.Reflective = 0.2;
            backWall.SetMatrix(Mat4.TranslateMatrix(0,0,4) *
                                Mat4.ScaleMatrix(10, 10, 0.1));

            Cube tableTop = new Cube();
            tableTop.SetMatrix(Mat4.TranslateMatrix(0,1,0) * Mat4.ScaleMatrix(2, 0.1, 1));

            Cube leg1 = new Cube();
            leg1.SetMatrix(Mat4.TranslateMatrix(-1.7, 0, -0.9) * Mat4.ScaleMatrix(0.1, 1, 0.1));

            Cube leg2 = new Cube();
            leg2.SetMatrix(Mat4.TranslateMatrix(1.7, 0, -0.9) * Mat4.ScaleMatrix(0.1, 1, 0.1));

            Cube leg3 = new Cube();
            leg3.SetMatrix(Mat4.TranslateMatrix(1.7, 0, 0.9) * Mat4.ScaleMatrix(0.1, 1, 0.1));

            Cube leg4 = new Cube();
            leg4.SetMatrix(Mat4.TranslateMatrix(-1.7, 0, 0.9) * Mat4.ScaleMatrix(0.1, 1, 0.1));

            Camera camera = new Camera(800, 600, Constants.pi / 3.0);
            camera.ViewTransform(new Point(0, 2, -5), new Point(0, 0, 4), new Vector(0, 1, 0));

            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter12_CubeRoom2");


        }

    }
}
