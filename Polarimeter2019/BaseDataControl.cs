using System;
using System.IO;
using System.Text;

namespace Polarimeter2019
{
    internal class BaseDataControl
    {
        public BaseDataControl()
        {

        }

        #region Members
        struct strucCurveData
        {
            public double[] X;
            public double[] Y;
            public double Xm;
            public double Ym;
            public double AngleOfRotation;

            private string mSampleName;
            private double mSpecificRotation;
            public strucCurveData[] Reference;
            public Collection DataCollection = new Collection();
            public strucCurveData[] Data;
        }
        #endregion
       
        #region Properties

        public string SampleName
        {
            get
            {
                return SampleName;
            }
            set
            {
                SampleName = value;
            }
        }

        #endregion

        #region Functions/Method

        public void PatchReference(int PointID, double X, double Y)
        {
            Input: Reference.X(0 To PointID);
            Input: Reference.Y(0 To PointID);
            Reference.X(PointID) = X;
            Reference.Y(PointID) = Y;
            if (Y < Reference.Ym)
            {
                Reference.Ym = Y;
                Reference.Xm = X;
            }
        }

        public void PatchData(int RepeatID, int PointID, double X, double Y)
        {
            if (Data == null)
            {
                System.Data(RepeatID).Ym = 999999;
                //System.Data(RepeatID).Ym = 999999;
            }
            else if (System.Data.Length - 1 < RepeatID)
            {
                Input: Data(0 To RepeatID);
                    
                System.Data(RepeatID).Ym = 999999;
            }
            Input: Data(RepeatID).X(0 To PointID);
            Input: Data(RepeatID).Y(0 To PointID);
            System.Data(RepeatID).X(PointID) = X;
            System.Data(RepeatID).Y(PointID) = Y;

            if (Y < System.Data(RepeatID).Ym)
            {
                System.Data(RepeatID).Ym = Y;
                System.Data(RepeatID).Xm = X;
                AnalyzeData(RepeatID);
            }
        }


        #endregion

        #region New

        #endregion
    }
}