using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT
{
    public static class Utility
    { 
        public static double DegToRad(double degrees)
        {
            return degrees * (Constants.pi / 180.0);
        }

        public static bool FE(double a, double b)
        {
            double temp = Math.Abs(a - b);

            if(temp < Constants.epsilon)
            {
                return true;
            }
            return false;
        }
    }
}
