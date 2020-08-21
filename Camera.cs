using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RT;

namespace RT
{
    public class Camera
    {

        public int hSize;
        public int vSize;
        public double fov;
        public Mat4 transform;
        protected double pixelSize;
        protected double halfWidth;
        protected double halfHeight;

        public double PixelSize
        {
            get { return pixelSize; }
            private set { }
        }

        public double HalfWidth
        {
            get { return halfWidth; }
            private set { }
        }

        public double HalfHeight
        {
            get { return halfHeight; }
            private set { }
        }

        public Camera(int hSize = 160, int vSize = 120, double fieldOfView = Constants.pi / 2.0)
        {
            this.hSize = hSize;
            this.vSize = vSize;
            fov = fieldOfView;
            transform = new Mat4();
            CalculatePixelSize();
        }

        public void CalculatePixelSize()
        {
            double halfView = (double)Math.Tan(this.fov / 2.0);
            double aspect = this.hSize / (double)this.vSize;

            if(aspect >=1)
            {
                this.halfWidth = halfView;
                this.halfHeight = halfView / aspect;
            }
            else
            {
                this.halfWidth = halfView * aspect;
                this.halfHeight = halfView;
            }
            this.pixelSize = (this.halfWidth * 2.0) / this.hSize;

        }

        public Mat4 ViewTransform(Point from, Point to, Vector up)
        {
            up = up.Normalize();
            Vector forward = (to - from).Normalize();
            //NaN check 
            if (double.IsNaN(forward.x))
                Console.WriteLine("Bad Forward Vector in Camera's View Transform.");

            Vector left = Vector.Cross(forward,up);
            if (double.IsNaN(left.x))
                Console.WriteLine("Bad Left Vector in Camera's View Transform.");

            Vector true_up = Vector.Cross(left, forward);
            if (double.IsNaN(true_up.x))
                Console.WriteLine("Bad Up Vector in Camera's View Transform.");

            Mat4 orientation = new Mat4(
                left.x, left.y, left.z, 0,
                true_up.x, true_up.y, true_up.z, 0,
                -forward.x, -forward.y, -forward.z, 0,
                0, 0, 0, 1);
            //Move to the from position
            transform = orientation * Mat4.TranslateMatrix(-from.x, -from.y, -from.z);
            return transform;
        }

    }
}
