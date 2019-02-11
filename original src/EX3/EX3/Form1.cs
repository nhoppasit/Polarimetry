using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace EX3
{
    public partial class Form1 : Form
    {
        private Thread addDataRunner;
        private AddDataDelegate addDataDel;
        private DateTime minValue;
        private DateTime maxValue;
        private object rand;

        public object Chart1 { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoop);
            addDataRunner = new Thread(addDataThreadStart);

            addDataDel = new AddDataDelegate(addDataDel);
        }

        private void AddDataThreadLoop()
        {
            try
            {
                while (true)
                {
                    chart1.Invoke(addDataDel);

                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
            }
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
   
            // Reset number of series in the chart.
            chart1.Series.Clear();

            // create a line chart series
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.DateTime;
            chart1.Series.Add(newSeries);

            // start worker threads.
            if (addDataRunner.IsAlive == true)
                addDataRunner.Resume();
            else
                addDataRunner.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (addDataRunner.IsAlive == true)
                addDataRunner.Suspend();

            // Enable all controls on the form
            btnStart.Enabled = true;
            // and only Disable the Stop button
            btnStop.Enabled = false;
        }
        // / Main loop for the thread that adds data to the chart.
        // / The main purpose of this function is to Invoke AddData
        // / function every 1000ms (1 second).

        public void AddData()
        {
            DateTime timeStamp = DateTime.Now;

            foreach (Series ptSeries in chart1.Series)
                AddNewPoint(timeStamp, ptSeries);
        } // AddData

        // / The AddNewPoint function is called for each series in the chart when
        // / new points need to be added.  The new point will be placed at specified
        // / X axis (Date/Time) position with a Y value in a range +/- 1 from the previous
        // / data point's Y value, and not smaller than zero.

        public void AddNewPoint(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            // Add new data point to its series.
            ptSeries.Points.AddXY(timeStamp.ToOADate(), rand.Next(10, 20));

            // remove all points from the source series older than 1.5 minutes.
            double removeBefore = timeStamp.AddSeconds((System.Convert.ToDouble(2) * -1)).ToOADate();
            // remove oldest values to maintain a constant number of data points
            while (ptSeries.Points(0).XValue < removeBefore)
                ptSeries.Points.RemoveAt(0);

            Chart1.ChartAreas(0).AxisX.Minimum = ptSeries.EmptyPointStyle(0).XValue;
            Chart1.ChartAreas(0).AxisX.Maximum = DateTime.FromOADate(ptSeries.EmptyPointStyle(0).XValue).AddSeconds(1.9).ToOADate();

            Chart1.Invalidate();
        } // AddNewPoint
    }

}
