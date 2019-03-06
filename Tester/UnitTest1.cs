using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Cal_Point_Count()
        {
            string s_res = "0.2";
            string s_min = "0";
            string s_max = "360";

            Polarimeter_Control.BaseDataControl bdc;

            bdc = new Polarimeter_Control.BaseDataControl();

            // Validation Text to float
            if (double.TryParse(s_res, out double resolution))
            {
                if (0.02 <= resolution && resolution <= 10) //deg
                {
                    // Change BDC
                    double min = double.Parse(s_min);
                    double max = double.Parse(s_max);
                    int PointCount = (int)((max - min) / resolution + 1);

                    //bdc.Reference.X = new double[PointCount]; 
                    //bdc.Reference.Y = new double[PointCount]; // destroy
                }
            }
        }
    }
}
