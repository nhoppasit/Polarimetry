using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Windows.Forms;

namespace EX3_ver._2
{
    public partial class Form1 : Form
    {
        private Thread addDataRunner;
        private Random rand = new Random();
        public delegate void AddDataDelegate();
        public AddDataDelegate addDataDel;

        private DateTime minValue, maxValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoop);
            addDataRunner = new Thread(addDataThreadStart);

            addDataDel = new AddDataDelegate(AddData);

            // this.btnStart_Click(null, EventArgs.Empty);
        }

        void AddData()
        {
            DateTime timeStamp = DateTime.Now;

            //For Each ptSeries As Series In Chart1.Series
            //    AddNewPoint(timeStamp, ptSeries)
            //Next ptSeries

            foreach (Series ptSeries in chart1.Series)
            {
                AddNewPoint(timeStamp, ptSeries);
            }
        }

        void btnStop_Click(object p, EventArgs empty)
        {
            throw new NotImplementedException();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Disable all controls on the form
            btnStart.Enabled = false;
            // and only Enable the Stop button
            btnStop.Enabled = true;

            // Predefine the viewing area of the chart
            minValue = DateTime.Now;
            maxValue = minValue.AddSeconds(30);

            chart1.ChartAreas[0].AxisX.Minimum = minValue.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = maxValue.ToOADate();

            // Reset number of series in the chart.
            chart1.Series.Clear();

            // create a line chart series
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.XValueType = ChartValueType.DateTime;
            chart1.Series.Add(newSeries);

            // start worker threads.
            if (addDataRunner.IsAlive == true)
            {
                addDataRunner.Resume();
            }
            else
            {
                addDataRunner.Start();
            }
        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            if (addDataRunner.IsAlive == true)
            {
                addDataRunner.Suspend();
            }
            // Enable all controls on the form
            btnStart.Enabled = true;
            // and only Disable the Stop button
            btnStop.Enabled = false;
        }
        private void AddDataThreadLoop()
        {
            try
            {
                while (true)
                {
                    chart1.Invoke(addDataDel);

                    Thread.Sleep(1000);
                }
            }
            catch (Exception)
            {

            }
        }

        int idx = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            idx++;
            AddData();
            //aasdasdsdsad
        }

        public void AddNewPoint(DateTime timeStamp, Series ptSeries)
        {
            // Add new data point to its series.
            //ptSeries.Points.AddXY(timeStamp.ToOADate(), rand.Next(10,20));
            ptSeries.Points.AddXY(idx, rand.Next(10, 20));

            System.Diagnostics.Trace.WriteLine(idx.ToString());

            // remove all points from the source series older than 1.5 minutes.

            //double removeBefore = timeStamp.AddSeconds((System.Convert.ToDouble(2) * -1)).ToOADate();
            //// remove oldest values to maintain a constant number of data points
            //while (ptSeries.Points[0].XValue < removeBefore)
            //    ptSeries.Points.RemoveAt(0);

            //chart1.ChartAreas[0].AxisX.Minimum = ptSeries.Points[0].XValue;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(ptSeries.Points[0].XValue).AddSeconds(1.9).ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = ptSeries.Points[ptSeries.Points.Count - 1].XValue + 1;

            chart1.Invalidate();
        } // AddNewPoint
    }
}