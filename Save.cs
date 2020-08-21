using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    class Save
    {
        public static void SaveCanvas(Canvas canvas, string filename = "temp")
        {
            CreatePPM(canvas, filename);
        }

        static void CreatePPM(Canvas canvas, string filename)
        {
            //Create Header
            //(Type) P3
            //Width Height
            //(Max Value) 255

            using (System.IO.StreamWriter sw = System.IO.File.CreateText(filename + ".ppm"))
            {
                int maxValue = 255;

                string header = "P3\n" +
                                canvas.GetWidth().ToString() + " " + canvas.GetHeight().ToString() + '\n' +
                                maxValue.ToString();

                sw.WriteLine(header);

                WritePPMBody(canvas, maxValue, sw);

                string footer = "\n"; //For ImageMagick loading, which requires a new line at the end.

                //sw.WriteLine(footer);
                sw.Close();
            }
        }

        static void WritePPMBody(Canvas canvas, int maxValue, System.IO.StreamWriter sw)
        {
            string currentLine = "";

            //Iterate over all pixels

            for(int y = 0; y < canvas.GetHeight(); y++)
            {
                for (int x = 0; x < canvas.GetWidth(); x++)
                {
                    //canvas.GetHeight() -
                       Color color = canvas.GetPixel(x, y);

                    string r = Clamp(color.r * maxValue, maxValue).ToString();
                    string g = Clamp(color.g * maxValue, maxValue).ToString();
                    string b = Clamp(color.b * maxValue, maxValue).ToString();

                    currentLine = ' ' + r + ' ' + g + ' ' + b + ' ';

                    sw.WriteLine(currentLine);
                }
            }
        }

         static int Clamp(double channelColor, int maxValue, int minValue = 0)
        {
            int temp = (int)(channelColor);
            if (temp > maxValue)
            {
                temp = maxValue;
                return temp;
            }
            if(temp < minValue)
            {
                temp = minValue;
            }
            return temp;
        }


    }
}
