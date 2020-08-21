using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{
    [TestFixture]
    public class Chapter16Test
    {
        [Test, Order(1)]
        public void T01_CSGCreation()
        {
            Sphere s1 = new Sphere();
            Cube s2 = new Cube();

            CSG c = new CSG(CSG.Operation.Union, s1, s2);
            Assert.AreEqual(CSG.Operation.Union, c.operation);
            Assert.AreEqual(s1, c.left);
            Assert.AreEqual(s2, c.right);
            Assert.AreEqual(c, s1.GetParent());
            Assert.AreEqual(c, s2.GetParent());
        }

        [Test, Order(2)]
        public void T02_EvaluatingRuleForACSGOperation()
        {
            List<bool> lHit = new List<bool>();
            List<bool> inl = new List<bool>();
            List<bool> inr = new List<bool>();
            List<bool> result = new List<bool>();

            lHit.Add(true); lHit.Add(true);
            lHit.Add(true); lHit.Add(true);
            lHit.Add(false); lHit.Add(false);
            lHit.Add(false); lHit.Add(false);

            inl.Add(true); inl.Add(true);
            inl.Add(false); inl.Add(false);
            inl.Add(true); inl.Add(true);
            inl.Add(false); inl.Add(false);

            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);

            result.Add(false); result.Add(true);
            result.Add(false); result.Add(true);
            result.Add(false); result.Add(false);
            result.Add(true); result.Add(true);

            Assert.AreEqual( result[0], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[0],
                                                                inl[0], inr[0]));

            Assert.AreEqual(result[1], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[1],
                                                    inl[1], inr[1]));

            Assert.AreEqual(result[2], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[2],
                                                    inl[2], inr[2]));

            Assert.AreEqual(result[3], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[3],
                                                    inl[3], inr[3]));

            Assert.AreEqual(result[4], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[4],
                                                    inl[4], inr[4]));

            Assert.AreEqual(result[5], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[5],
                                                    inl[5], inr[5]));

            Assert.AreEqual(result[6], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[6],
                                                    inl[6], inr[6]));

            Assert.AreEqual(result[7], CSG.IntersectionAllowed(CSG.Operation.Union, lHit[7],
                                                    inl[7], inr[7]));


        }

        [Test, Order(3)]
        public void T03_EvaluatingRuleForACSGIntersectOperation()
        {
            List<bool> lHit = new List<bool>();
            List<bool> inl = new List<bool>();
            List<bool> inr = new List<bool>();
            List<bool> result = new List<bool>();

            lHit.Add(true); lHit.Add(true);
            lHit.Add(true); lHit.Add(true);
            lHit.Add(false); lHit.Add(false);
            lHit.Add(false); lHit.Add(false);

            inl.Add(true); inl.Add(true);
            inl.Add(false); inl.Add(false);
            inl.Add(true); inl.Add(true);
            inl.Add(false); inl.Add(false);

            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);

            result.Add(true); result.Add(false);
            result.Add(true); result.Add(false);
            result.Add(true); result.Add(true);
            result.Add(false); result.Add(false);

            Assert.AreEqual(result[0], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[0],
                                                                inl[0], inr[0]));

            Assert.AreEqual(result[1], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[1],
                                                    inl[1], inr[1]));

            Assert.AreEqual(result[2], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[2],
                                                    inl[2], inr[2]));

            Assert.AreEqual(result[3], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[3],
                                                    inl[3], inr[3]));

            Assert.AreEqual(result[4], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[4],
                                                    inl[4], inr[4]));

            Assert.AreEqual(result[5], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[5],
                                                    inl[5], inr[5]));

            Assert.AreEqual(result[6], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[6],
                                                    inl[6], inr[6]));

            Assert.AreEqual(result[7], CSG.IntersectionAllowed(CSG.Operation.Intersection, lHit[7],
                                                    inl[7], inr[7]));

        }

        [Test, Order(4)]
        public void T04_EvaluatingRuleForACSGDifferenceOperation()
        {
            List<bool> lHit = new List<bool>();
            List<bool> inl = new List<bool>();
            List<bool> inr = new List<bool>();
            List<bool> result = new List<bool>();

            lHit.Add(true); lHit.Add(true);
            lHit.Add(true); lHit.Add(true);
            lHit.Add(false); lHit.Add(false);
            lHit.Add(false); lHit.Add(false);

            inl.Add(true); inl.Add(true);
            inl.Add(false); inl.Add(false);
            inl.Add(true); inl.Add(true);
            inl.Add(false); inl.Add(false);

            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);
            inr.Add(true); inr.Add(false);

            result.Add(false); result.Add(true);
            result.Add(false); result.Add(true);
            result.Add(true); result.Add(true);
            result.Add(false); result.Add(false);

            Assert.AreEqual(result[0], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[0],
                                                                inl[0], inr[0]));

            Assert.AreEqual(result[1], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[1],
                                                    inl[1], inr[1]));

            Assert.AreEqual(result[2], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[2],
                                                    inl[2], inr[2]));

            Assert.AreEqual(result[3], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[3],
                                                    inl[3], inr[3]));

            Assert.AreEqual(result[4], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[4],
                                                    inl[4], inr[4]));

            Assert.AreEqual(result[5], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[5],
                                                    inl[5], inr[5]));

            Assert.AreEqual(result[6], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[6],
                                                    inl[6], inr[6]));

            Assert.AreEqual(result[7], CSG.IntersectionAllowed(CSG.Operation.Difference, lHit[7],
                                                    inl[7], inr[7]));
        }

        [Test, Order(5)]
        public void T05_FilteringAListOfIntersections()
        {
            Sphere s1 = new Sphere();
            Cube s2 = new Cube();

            CSG c1 = new CSG(CSG.Operation.Union, s1, s2);
            CSG c2 = new CSG(CSG.Operation.Intersection, s1, s2);
            CSG c3 = new CSG(CSG.Operation.Difference, s1, s2);

            List<Intersection> xs = new List<Intersection>();
            xs.Add(new Intersection(s1, 1));
            xs.Add(new Intersection(s2, 2));
            xs.Add(new Intersection(s1, 3));
            xs.Add(new Intersection(s2, 4));

            List<CSG.Operation> operation = new List<CSG.Operation>();
            List<int> x0 = new List<int>();
            List<int> x1 = new List<int>();

            operation.Add(CSG.Operation.Union);
            operation.Add(CSG.Operation.Intersection);
            operation.Add(CSG.Operation.Difference);

            x0.Add(0); x0.Add(1); x0.Add(0);
            x1.Add(3); x1.Add(2); x1.Add(1);

            //Union
            List<Intersection> result = c1.FilterIntersections(xs);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(xs[x0[0]], result[0]);
            Assert.AreEqual(xs[x1[0]], result[1]);

            //Intersection
            result = c2.FilterIntersections(xs);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(xs[x0[1]], result[0]);
            Assert.AreEqual(xs[x1[1]], result[1]);

            //Difference
            result = c3.FilterIntersections(xs);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(xs[x0[2]], result[0]);
            Assert.AreEqual(xs[x1[2]], result[1]);
        }

        [Test, Order(6)]
        public void T06_IntersectingARayWithACSGObjectMiss()
        {
            CSG c = new CSG(CSG.Operation.Union, new Sphere(), new Cube());
            Ray r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
            List<Intersection> xs = c.Intersect(r);
            Assert.IsEmpty(xs);
        }

        [Test, Order(7)]
        public void T07_IntersectingARayWithACSGObjectHit()
        {
            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();
            s2.SetMatrix(Mat4.TranslateMatrix(0, 0, 0.5));
            CSG c = new CSG(CSG.Operation.Union, s1, s2);
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            List<Intersection> xs = c.Intersect(r);
            Assert.AreEqual(2, xs.Count);
            Assert.IsTrue(Utility.FE(4, xs[0].t));
            Assert.AreEqual(s1, xs[0].rayObject);
            Assert.IsTrue(Utility.FE(6.5, xs[1].t));
            Assert.AreEqual(s2, xs[1].rayObject);
        }

        public CSG CreateCSGDice()
        {
            //Create a dice
            Cube c1 = new Cube();
            c1.material.color = Color.red;

            Group g1 = new Group();
            Sphere d1 = new Sphere();
            d1.SetMatrix(Mat4.TranslateMatrix(0, 0, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Center
            d1.SetParent(g1);

            d1.material.color = Color.white;

            CSG csg1 = new CSG(CSG.Operation.Difference, c1, g1);

            Group g2 = new Group();
            Sphere d2a = new Sphere();
            Sphere d2b = new Sphere();
            d2a.SetMatrix(Mat4.TranslateMatrix(-0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Left
            d2b.SetMatrix(Mat4.TranslateMatrix(0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Right
            d2a.SetParent(g2);
            d2b.SetParent(g2);
            g2.SetMatrix(Mat4.RotateYMatrix(Constants.pi / 2));

            d2a.material.color = Color.white;
            d2b.material.color = Color.white;

            CSG csg2 = new CSG(CSG.Operation.Difference, csg1, g2);

            Group g3 = new Group();
            Sphere d3a = new Sphere();
            Sphere d3b = new Sphere();
            Sphere d3c = new Sphere();
            d3a.SetMatrix(Mat4.TranslateMatrix(-0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Left
            d3b.SetMatrix(Mat4.TranslateMatrix(0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Right
            d3c.SetMatrix(Mat4.TranslateMatrix(0.0, 0.0, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Center
            d3a.SetParent(g3);
            d3b.SetParent(g3);
            d3c.SetParent(g3);
            g3.SetMatrix(Mat4.RotateYMatrix(Constants.pi));

            d3a.material.color = Color.white;
            d3b.material.color = Color.white;
            d3c.material.color = Color.white;

            CSG csg3 = new CSG(CSG.Operation.Difference, csg2, g3);

            Group g4 = new Group();
            Sphere d4a = new Sphere();
            Sphere d4b = new Sphere();
            Sphere d4c = new Sphere();
            Sphere d4d = new Sphere();
            d4a.SetMatrix(Mat4.TranslateMatrix(-0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Left
            d4b.SetMatrix(Mat4.TranslateMatrix(0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Right
            d4c.SetMatrix(Mat4.TranslateMatrix(-0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Left
            d4d.SetMatrix(Mat4.TranslateMatrix(0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Right
            d4a.SetParent(g4);
            d4b.SetParent(g4);
            d4c.SetParent(g4);
            d4d.SetParent(g4);
            g4.SetMatrix(Mat4.RotateYMatrix(-Constants.pi / 2));

            d4a.material.color = Color.white;
            d4b.material.color = Color.white;
            d4c.material.color = Color.white;
            d4d.material.color = Color.white;

            CSG csg4 = new CSG(CSG.Operation.Difference, csg3, g4);

            Group g5 = new Group();
            Sphere d5a = new Sphere();
            Sphere d5b = new Sphere();
            Sphere d5c = new Sphere();
            Sphere d5d = new Sphere();
            Sphere d5e = new Sphere();
            d5a.SetMatrix(Mat4.TranslateMatrix(-0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Left
            d5b.SetMatrix(Mat4.TranslateMatrix(0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Right
            d5c.SetMatrix(Mat4.TranslateMatrix(-0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Left
            d5d.SetMatrix(Mat4.TranslateMatrix(0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Right
            d5e.SetMatrix(Mat4.TranslateMatrix(0.0, 0.0, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Center
            d5a.SetParent(g5);
            d5b.SetParent(g5);
            d5c.SetParent(g5);
            d5d.SetParent(g5);
            d5e.SetParent(g5);
            g5.SetMatrix(Mat4.RotateXMatrix(-Constants.pi / 2));

            d5a.material.color = Color.white;
            d5b.material.color = Color.white;
            d5c.material.color = Color.white;
            d5d.material.color = Color.white;
            d5e.material.color = Color.white;

            CSG csg5 = new CSG(CSG.Operation.Difference, csg4, g5);

            Group g6 = new Group();
            Sphere d6a = new Sphere();
            Sphere d6b = new Sphere();
            Sphere d6c = new Sphere();
            Sphere d6d = new Sphere();
            Sphere d6e = new Sphere();
            Sphere d6f = new Sphere();
            d6a.SetMatrix(Mat4.TranslateMatrix(-0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Left
            d6b.SetMatrix(Mat4.TranslateMatrix(0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Right
            d6c.SetMatrix(Mat4.TranslateMatrix(-0.5, -0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Lower Left
            d6d.SetMatrix(Mat4.TranslateMatrix(0.5, 0.5, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Upper Right
            d6e.SetMatrix(Mat4.TranslateMatrix(-0.5, 0.0, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Center Left
            d6f.SetMatrix(Mat4.TranslateMatrix(0.5, 0.0, -1) * Mat4.ScaleMatrix(0.2, 0.2, 0.2)); //Center Right
            d6a.SetParent(g6);
            d6b.SetParent(g6);
            d6c.SetParent(g6);
            d6d.SetParent(g6);
            d6e.SetParent(g6);
            d6f.SetParent(g6);
            g6.SetMatrix(Mat4.RotateXMatrix(Constants.pi / 2));

            d6a.material.color = Color.white;
            d6b.material.color = Color.white;
            d6c.material.color = Color.white;
            d6d.material.color = Color.white;
            d6e.material.color = Color.white;
            d6f.material.color = Color.white;

            CSG csg6 = new CSG(CSG.Operation.Difference, csg5, g6);
            return csg6;
        }

        [Test, Order(8)]
        public void T08_PuttingItTogether()
        {

            if (Scene.current == null)
                new Scene();

            Light light = new Light(new Point(-8, 8, -8), Color.white);
            
            Light light2 = new Light(new Point(8, -8, -8), new Color(0.2, 0.5, 0.9));
            
            Camera camera = new Camera(600, 400, Constants.pi / 3.0);
            camera.ViewTransform(new Point(0, 0.0, -8), new Point(0, 0, 10), new Vector(0, 1, 0));

            //Create a Lens
            /*
            Sphere s1 = new Sphere();
            Sphere s2 = new Sphere();

            s1.material.Glassy();
            s2.material.Glassy();

            s1.SetMatrix(Mat4.TranslateMatrix(-0.65, 0, 0));
            s2.SetMatrix(Mat4.TranslateMatrix(0.65, 0, 0));
            CSG lens = new CSG(CSG.Operation.Union, s1, s2);
            */
            CSG csgDice1 = CreateCSGDice();
            csgDice1.SetMatrix( Mat4.TranslateMatrix(2, 0, 0) * 
                Mat4.RotateXMatrix(Constants.pi / -4) *
                Mat4.RotateYMatrix(Constants.pi / -4));

            CSG csgDice2 = CreateCSGDice();
            csgDice2.SetMatrix(Mat4.TranslateMatrix(-2, 0, 0) * 
                Mat4.RotateXMatrix(Constants.pi / 4) *
                Mat4.RotateYMatrix(Constants.pi / 4));

            Canvas canvas = Scene.current.Render(camera);
            Save.SaveCanvas(canvas, "Chapter16_CSGObjects");

        }

    }
}
