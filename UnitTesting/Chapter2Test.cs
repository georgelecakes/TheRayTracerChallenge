using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RT.UnitTesting
{

    [TestFixture]
    public class Chapter2Test
    {

        [Test, Order(1)]
        public void T01_ColorBasics()
        {
            //Create
            Color color = new Color(-0.5f, 0.4f, 1.7f);
            Assert.AreEqual(-0.5f, color.r);
            Assert.AreEqual(0.4f, color.g);
            Assert.AreEqual(1.7f, color.b);
            //Add
            Color c1 = new Color(0.9f, 0.6f, 0.75f);
            Color c2 = new Color(0.7f, 0.1f, 0.25f);
            Assert.AreEqual(new Color(1.6f, 0.7f, 1.0f), c1 + c2);
            //Substract
            Assert.AreEqual(new Color(0.2f, 0.5f, 0.5f), c1 - c2);
            //Multiply Scalar
            c1 = new Color(0.2f, 0.3f, 0.4f);
            Assert.AreEqual(new Color(0.4f, 0.6f, 0.8f), c1 * 2);
            //Multiply
            c1 = new Color(1, 0.2f, 0.4f);
            c2 = new Color(0.9f, 1, 0.1f);
            Assert.AreEqual(new Color(0.9f, 0.2f, 0.04f), c1 * c2);

        }

        [Test, Order(2)]
        public void T02_Canvas()
        {
            Canvas canvas = new Canvas(10, 20);
            Assert.AreEqual(10, canvas.GetWidth());
            Assert.AreEqual(20, canvas.GetHeight());

            for(int y = 0; y < canvas.GetHeight(); y++)
            {
                for(int x = 0; x < canvas.GetWidth(); x++)
                {
                    Assert.AreEqual(Color.black, canvas.GetPixel(x, y));
                }
            }

            canvas.SetPixel(2, 3, Color.red);
            Assert.AreEqual(Color.red, canvas.GetPixel(2, 3));

        }



    }
}
