using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

public class BaseDataControl
{
    #region Members

    public struct strucCurveData
    {
        public double[] X;
        public double[] Y;
        public double Xmin;
        public double Ymin;
        public double Xmax;
        public double Ymax;
        public double AngleOfRotation;
    }

    private string mSampleName;
    private double mSpecificRotation;
    public strucCurveData Reference;
    public strucCurveData[] Data;

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
            //SampleName = value;
        }
    }

    #endregion

    #region Functions/Method

    public void PatchReference(int PointID, double X, double Xmax, double Y, double Ymax)
    {
        double Xvalue = Reference.X[PointID];
        double Yvalue = Reference.Y[PointID];

        Reference.X[PointID] = X;
        Reference.Y[PointID] = Y;

        if (Yvalue > Reference.Ymax)
        {
            Reference.Xmax = Xvalue;
            Reference.Ymax = Yvalue;
        }

        if (Y < Reference.Ymin)
        {
            Reference.Ymin = Y;
            Reference.Xmin = X;
        }
        else
        {
            if (Y > Reference.Ymin)
            {
                if (Y > Reference.Ymax)
                {
                    Reference.Ymax = Y;
                    Reference.Xmax = X;
                }
                else
                {

                }
            }
        }
    }

    public void PatchData(int RepeatID, int PointID, double X, double Xmax, double Y, double Ymax)
    {
        //    !!!!!!!!
        //    'zero base, careful!!!

        if (Data == null)
        {
            strucCurveData Z = Data[RepeatID];
            Data[RepeatID].Ymin = 999999;
        }
        else
        {
            if (Data.Length - 1 < RepeatID)
            {
                strucCurveData Q = Data[RepeatID];
                Data[RepeatID].Ymin = 999999;
            }
        }

        double Xdata = Data[RepeatID].X[PointID];
        double Ydata = Data[RepeatID].Y[PointID];

        Data[RepeatID].X[PointID] = X;
        Data[RepeatID].Y[PointID] = Y;

        if (Ydata > Data[RepeatID].Ymax)
        {
            Data[RepeatID].Xmax = Xdata;
            Data[RepeatID].Ymax = Ydata;
        }

        if (Y < Data[RepeatID].Ymin)
        {
            Data[RepeatID].Ymin = Y;
            Data[RepeatID].Xmin = X;
            AnalyzeData(RepeatID);
        }
        else
        {
            if (Y > Data[RepeatID].Ymin)
            {
                if (Y > Data[RepeatID].Ymax)
                {
                    Data[RepeatID].Ymax = Y;
                    Data[RepeatID].Xmax = X;
                    AnalyzeData(RepeatID);
                }
                else
                {

                }
            }
        }
    }

    public void SaveFile()
    {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Filter = "Text File (*.txt)|*.txt|CSV File (*.csv)|*.csv|Polarimeter File (*.pom)|*.pom|All File (*.*)|*.*";
        DialogResult redlg = dlg.ShowDialog();
        if (redlg != System.Windows.Forms.DialogResult.OK)
        {
            return;
        }


        // ----------Save---------------------

        string path = dlg.FileName;

        // Delete the file if it exists.
        if (File.Exists(path))
            File.Delete(path);

        // Create the file.
        FileStream fs = File.Create(path);

        //LogFile.Log theSave = new LogFile.Log(dlg.Filter, path);
        // Start header
        AddText(fs, "Polarimeter Data File"); // Intro
        AddText(fs, "(Copy Right)2011, CIID, KMITL" + Environment.NewLine);
        AddText(fs, DateTime.Now.ToString() + Environment.NewLine); // Date-time

        // Start body
        AddText(fs, "[Sample Name]" + Environment.NewLine); // Sample name
        AddText(fs, mSampleName);
        AddText(fs, "[Specific Rotation]" + Environment.NewLine); // Specific rotation
        AddText(fs, "[Samples]" + Environment.NewLine); // Number of samples
                                                        //AddText(fs, Data.Length.ToString);
        AddText(fs, Data.ToString());

        // Reference
        AddText(fs, "[Reference]");
        for (int i = 0; i <= Reference.X.Length - 1; i++)
            AddText(fs, Reference.X[i].ToString() + "," + Reference.Y[i].ToString() + Environment.NewLine);
        // Data
        for (int k = 0; k <= Data.Length - 1; k++)
        {
            AddText(fs, "[Sample " + (k + 1).ToString() + "]");
            for (int i = 0; i <= Data[k].X.Length - 1; i++)
            {
                AddText(fs, Data[k].X[i].ToString() + "," + Data[k].Y[i].ToString() + Environment.NewLine);
            }
        }

        // Ending
        fs.Close();
    }

    private void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

    public void OpenFile()
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "Text File (*.txt)|*.txt";
        DialogResult redlg = dlg.ShowDialog();
        if (redlg != System.Windows.Forms.DialogResult.OK)
            return;
    }

    private void AnalyzeData(int RepeatID)
    {
        try
        {
            if (Reference.X == null)
            {
                return;
            }
            if (Data[RepeatID].X == null)
            {
                return;
            }
            Data[RepeatID].AngleOfRotation = Math.Abs(Data[RepeatID].Xmin - Reference.Xmin);
        }
        catch (Exception ex)
        {

        }
    }

    private void AnalyzeAllData()
    {
        int i = 0;
        foreach (strucCurveData d in Data)
        {
            if (d.X != null)
                AnalyzeData(i);
            i += 1;
        }
    }

    #endregion

    #region Constructor

    public BaseDataControl()
    {
        Reference.Ymin = 9999999;
    }

    #endregion
}
