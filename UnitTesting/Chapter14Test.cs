using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    public class Chapter14Test
    {
        [Test, Order(1)]
        public void T01_CreatingNewGroup()
        {
            Group group = new Group();
            Assert.AreEqual(0, group.GetChildren().Count);
        }

        [Test, Order(2)]
        public void T02_ShapeHasParent()
        {
            TestRayObject r = new TestRayObject();

            Assert.AreEqual(null, r.GetParent());
        }

        [Test, Order(3)]
        public void T03_AddingAChildToAGroup()
        {
            Group g = new Group();
            TestRayObject o = new TestRayObject();
            o.SetParent(g);
            Assert.Contains(o, g.GetChildren());
            Assert.AreEqual(g, o.GetParent());

        }

        [Test, Order(4)]
        public void T04_IntersectRayWithEmptyGroup()
        {
            Group g = new Group();
            Ray ray = new Ray(new Point(0,0,0), new Vector(0,0,1));
            List<Intersection> xs = g.Intersect(ray);
            Assert.IsEmpty(xs);

        }

        [Test, Order(5)]
        public void T05_IntersectRayWithNonEmptyGroup()
        {
            Group g = new Group();
            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();

            s2.SetMatrix(Mat4.TranslateMatrix(0, 0, -3));

            Sphere s3 = new Sphere();

            s3.SetMatrix(Mat4.TranslateMatrix(5, 0, 0));

            s1.SetParent(g);
            s2.SetParent(g);
            s3.SetParent(g);

            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            List<Intersection> xs = g.Intersect(r);

            Assert.AreEqual(4, xs.Count);

            Assert.AreEqual(s2, xs[0].rayObject);
            Assert.AreEqual(s2, xs[1].rayObject);
            Assert.AreEqual(s1, xs[2].rayObject);
            Assert.AreEqual(s1, xs[3].rayObject);
        }


        [Test, Order(6)]
        public void T06_IntersectingATransformedGroup()
        {
            Group g = new Group();
            g.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.TranslateMatrix(5, 0, 0));
            s.SetParent(g);
            Ray r = new Ray(new Point(10, 0, -10), new Vector(0, 0, 1));
            List<Intersection> xs = g.Intersect(r);
            Assert.AreEqual(2, xs.Count);
        }

        [Test, Order(7)]
        public void T07_PointFromWorldToObject()
        {
            Group g1 = new Group();
            g1.SetMatrix(Mat4.RotateYMatrix(Constants.pi / 2.0));
            Group g2 = new Group();
            g2.SetMatrix(Mat4.ScaleMatrix(2, 2, 2));
            g2.SetParent(g1);
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.TranslateMatrix(5, 0, 0));
            s.SetParent(g2);
            Point p = s.WorldToObject(new Point(-2, 0, -10));
            Assert.AreEqual(new Point(0, 0, -1), p);
        }

        [Test, Order(8)]
        public void T08_ConvertNormalVectorFromObjectToWorld()
        {
            Group g1 = new Group();
            g1.SetMatrix(Mat4.RotateYMatrix(Constants.pi / 2));
            Group g2 = new Group();
            g2.SetMatrix(Mat4.ScaleMatrix(1, 2, 3));
            g2.SetParent(g1);
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.TranslateMatrix(5, 0, 0));
            s.SetParent(g2);
            Vector n = s.NormalToWorld(new Vector(Math.Sqrt(3)/3,
                                                    Math.Sqrt(3) / 3,
                                                    Math.Sqrt(3) / 3));
            Assert.AreEqual(new Vector(0.2857, 0.4286, -0.8571), n);
        }

        [Test, Order(9)]
        public void T09_FindNormalOnChildObject()
        {
            Group g1 = new Group();
            g1.SetMatrix(Mat4.RotateYMatrix(Constants.pi / 2));
            Group g2 = new Group();
            g2.SetMatrix(Mat4.ScaleMatrix(1, 2, 3));
            g2.SetParent(g1);
            Sphere s = new Sphere();
            s.SetMatrix(Mat4.TranslateMatrix(5, 0, 0));
            s.SetParent(g2);
            Vector n = s.GetNormal(new Point(1.7321, 1.1547, -5.5774));
            Assert.AreEqual(new Vector(0.2857, 0.4286, -0.8571), n);
        }

        [Test, Order(10)]
        public void T10_AABBTest()
        {
            Group g1 = new Group();
            Sphere s1 = new Sphere();
            s1.SetParent(g1);

            Ray r1 = new Ray(new Point(0, 0, -2), new Vector(0, 0, 1));
            Ray r2 = new Ray(new Point(0.9, 0.9, -2), new Vector(0, 0, 1));
            Ray r3 = new Ray(new Point(2, 2, -2), new Vector(0, 0, 1));

            //Bounds should hit and we get intersections
            Assert.IsTrue(g1.GetBounds().Intersect(r1));
            List<Intersection> xs = g1.Intersect(r1);
            Assert.AreEqual(2, xs.Count);

            //Bounds should hit but we get no intersection
            Assert.IsTrue(g1.GetBounds().Intersect(r2));
            xs = g1.Intersect(r2);
            Assert.AreEqual(0, xs.Count);

            //Bounds miss and we get no intersection
            Assert.IsFalse(g1.GetBounds().Intersect(r3));
            xs = g1.Intersect(r3);
            Assert.AreEqual(0, xs.Count);
        }

        [Test, Order(10)]
        public void T10_AABBTestMatOperations()
        {
            Group g1 = new Group();
            Sphere s1 = new Sphere();
            s1.SetParent(g1);
            s1.SetMatrix(Mat4.TranslateMatrix(3, 3, 0));

            Ray r1 = new Ray(new Point(3, 3, -2), new Vector(0, 0, 1));
            Ray r2 = new Ray(new Point(3.9, 3.9, -2), new Vector(0, 0, 1));
            Ray r3 = new Ray(new Point(5, 5, -2), new Vector(0, 0, 1));

            //Bounds should hit and we get intersections
            Assert.IsTrue(g1.GetBounds().Intersect(r1));
            List<Intersection> xs = g1.Intersect(r1);
            Assert.AreEqual(2, xs.Count);

            //Bounds should hit but we get no intersection
            Assert.IsTrue(g1.GetBounds().Intersect(r2));
            xs = g1.Intersect(r2);
            Assert.AreEqual(0, xs.Count);

            //Bounds miss and we get no intersection
            Assert.IsFalse(g1.GetBounds().Intersect(r3));
            xs = g1.Intersect(r3);
            Assert.AreEqual(0, xs.Count);
        }

        [Test, Order(11)]
        public void T11_AABBTestMatOperationWithGroup()
        {
            Group g1 = new Group();
            g1.SetMatrix(Mat4.TranslateMatrix(1, 1, 0));
            Sphere s1 = new Sphere();
            s1.SetParent(g1);
            s1.SetMatrix(Mat4.TranslateMatrix(3, 3, 0));

            Ray r1 = new Ray(new Point(4, 4, -2), new Vector(0, 0, 1));
            Ray r2 = new Ray(new Point(4.9, 4.9, -2), new Vector(0, 0, 1));
            Ray r3 = new Ray(new Point(6, 6, -2), new Vector(0, 0, 1));

            //Bounds should hit and we get intersections
            Assert.IsTrue(g1.GetBounds().Intersect(r1));
            List<Intersection> xs = g1.Intersect(r1);
            Assert.AreEqual(2, xs.Count);

            //Bounds should hit but we get no intersection
            Assert.IsTrue(g1.GetBounds().Intersect(r2));
            xs = g1.Intersect(r2);
            Assert.AreEqual(0, xs.Count);

            //Bounds miss and we get no intersection
            Assert.IsFalse(g1.GetBounds().Intersect(r3));
            xs = g1.Intersect(r3);
            Assert.AreEqual(0, xs.Count);
        }

        [Test, Order(12)]
        public void T12_RenderingLotsOfObjects()
        {
            if (Scene.current == null)
                new Scene();

            Light light = new Light(new Point(0, 50, -25), Color.white);

            int numberOfSpheres = 5;

            int offset = 10;

            Point min = new Point(0, 0, 0);
            Point max = new Point(10, 10, 10);
            
            Camera camera = new Camera(200, 300, Constants.pi / 3.0);
            camera.ViewTransform(new Point(25, 20, -30), new Point(25, 20, 40), new Vector(0, 1, 0));

            //Create copies of each group at intervals
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                        //Create a group of severel spheres at random locations
                        Group g = new Group();
                        g.SetMatrix(Mat4.TranslateMatrix(x * offset, y * offset, 0 * offset));
                        for (int i = 0; i < numberOfSpheres; i++)
                        {
                            Sphere s = new Sphere();
                            s.SetMatrix(Mat4.TranslateMatrix(new Point().Randomize(min, max)));
                            s.SetParent(g);
                            s.material.color = new Color().Randomize();
                    }
                }
            }

            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter14_FieldOfSpheres");

        }


        public RayObject HexagonCorner()
        {
            RayObject corner = new Sphere();
            corner.SetMatrix(Mat4.TranslateMatrix(0, 0, -1) * Mat4.ScaleMatrix(0.25, 0.25, 0.25));
            return corner;
        }

        public RayObject HexagonEdge()
        {
            Cylinder edge = new Cylinder();
            edge.minimum = 0;
            edge.maximum = 1;
            edge.SetMatrix(Mat4.TranslateMatrix(0, 0, -1) *
                            Mat4.RotateYMatrix(Constants.pi / -6) *
                            Mat4.RotateZMatrix(Constants.pi / -2) *
                            Mat4.ScaleMatrix(0.25, 1, 0.25));
            return edge;
        }
        
        public RayObject HexagonSide()
        {
            Group side = new Group();
            RayObject hc = HexagonCorner();
            RayObject he = HexagonEdge();

            hc.SetParent(side);
            he.SetParent(side);

            return side;

        }

        public RayObject Hexagon()
        {
            //Group hexagon = new Group();
            //hexagon.performAABBIntersectionTest = false;

            for(int i = 0; i < 6; i++)
            {
                RayObject side = HexagonSide();
                side.SetMatrix(Mat4.RotateYMatrix(i * Constants.pi / 3));
                //side.SetParent(hexagon);
            }
            return null;
        }

        [Test, Order(13)]
        public void T13_PuttingItAllTogether()
        {
            if (Scene.current == null)
                new Scene();

            Light light = new Light(new Point(0, 50, -25), Color.white);


            Camera camera = new Camera(600, 480, Constants.pi / 3.0);
            camera.ViewTransform(new Point(0, 2, -2), new Point(0, 0, 0), new Vector(0, 1, 0));

            RayObject hexagon = Hexagon();

            //Canvas canvas = Scene.current.Render(camera);
            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter14_Hexagon");
        }

        [Test, Order(14)]
        public void T14_CheckingAABBValues()
        {

            Group g = new Group();
            g.SetMatrix(Mat4.TranslateMatrix(2,0,0));

            Cylinder c = new Cylinder();
            c.SetParent(g);
            c.minimum = 0;
            c.maximum = 1;
            c.isClosed = true;
            
            c.SetMatrix( Mat4.TranslateMatrix(2,0,0) * 
                Mat4.RotateZMatrix(Constants.pi / -4));
            
            Bounds b = g.GetBounds();


        }


    }
}
