using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public class Canvas
    {
        int width;
        int height;
        Color[,] canvas;
        
        public Canvas(int width = 256, int height = 256)
        {
            this.width = width;
            this.height = height;
            CreateCanvas();
        }

        void CreateCanvas()
        {
            canvas = new Color[width, height];
            FillCanvas(Color.black);
        }

        public void DrawCircle(int cx, int cy, int radius, Color color)
        {
            //Create a block to iterate over
            int xStart = cx - radius;
            int xEnd = cx + radius;
            int yStart = cy - radius;
            int yEnd = cy + radius;

            //Iterate over every element of the block and test if it is within the radius

            for(int x = xStart; x < xEnd; x++)
            {
                for(int y = yStart; y < yEnd; y++)
                {
                    //Calculate distance to center, use square as it is faster that root
                    int squareRadius = radius * radius;
                    int distance = (x - cx) * (x - cx) + (y - cy) * (y - cy);
                    if(distance <= squareRadius)
                    {
                        //Draw to location, which tests to see if it is even possible.
                        SetPixel(x, y, color);
                    }
                }
            }

        }

        public void FillCanvas(Color color)
        {
            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    canvas[x, y] = new Color(color);
                }
            }
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                canvas[x, y] = color;
            }
        }

        public Color GetPixel(int x, int y)
        {
            Color temp = new Color();

            if(x >= 0 && y >= 0 && x < width && y < height)
            {
                temp = canvas[x, y];
            }
            return temp;
        }

        public override string ToString()
        {
            return "Width: " + this.width.ToString() + ", Height: " + this.height.ToString();
        }

    }
}
