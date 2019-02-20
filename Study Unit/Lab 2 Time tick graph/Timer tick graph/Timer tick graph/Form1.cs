using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Timer_tick_graph
{
    public partial class Form1 : Form
    {
        int tickIdx = 0;
        double delta = 0.1;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0.00";

            timer1.Stop();
            timer1.Enabled = false;
            timer1.Interval = 1000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PushXY();
            tickIdx++;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartNew();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        void StartNew()
        {
            timer1.Stop();

            tickIdx = 0;

            // Reset number of series in the chart.
            chart1.Series.Clear();

            // create a line chart series
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.BorderWidth = 2;
            newSeries.XValueType = ChartValueType.DateTime;
            chart1.Series.Add(newSeries);

            chart1.ChartAreas[0].AxisX.Minimum = tickIdx * delta;
            chart1.ChartAreas[0].AxisX.Maximum = (tickIdx + 1) * delta;

            timer1.Start();
        }

        void PushXY()
        {
            foreach (Series ptSeries in chart1.Series)
            {
                double x = tickIdx * delta;
                double y = rand.Next(10, 20);
                System.Diagnostics.Trace.WriteLine(string.Format(">>> (X, Y) = ({0}, {1})", x, y));

                ptSeries.Points.AddXY(x, y);
                chart1.ChartAreas[0].AxisX.Maximum = ptSeries.Points[ptSeries.Points.Count - 1].XValue;
                chart1.Invalidate();
            }
        }
    }
}
