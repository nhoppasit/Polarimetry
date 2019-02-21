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
                //public strucCurveData Reference;
                //public Collection DataCollection = new Collection();
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

        #endregion

        #region New

        #endregion
    }
}