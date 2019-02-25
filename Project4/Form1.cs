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

namespace Project4
{
    public partial class Form1 : Form
    {
        public int num = 0;
        int tickIdx = 0;
        double delta = 0.1;
        Random rand = new Random();
        private object lsvdata;
        private int i;

        public Form1()
        {
            InitializeComponent();

            LsvData.Columns.Add("n", 35);
            LsvData.Columns.Add("t(n)", 150);
            LsvData.Columns.Add("y(n)", 150);
            LsvData.View = View.Details;
            LsvData.GridLines = true;
            LsvData.FullRowSelect = true;

            comboBox1.Items.Add("y = A sin(wt + P) + B");
            comboBox1.Items.Add("y = A cos(wt + P) + B");

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0.00";

            timer1.Stop();
            timer1.Enabled = false;
            timer1.Interval = 1000;
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            Calculate();
            StartNew();
        }

        public void Calculate()
        {
            switch (comboBox1.Text)
            {
                case "y = A sin(wt + P) + B":
                    CalSin();
                    break;

                case "y = A cos(wt + P) + B":
                    CalCos();
                    break;
            }
        }

        void CalSin()
        {
            try
            {
                int N = Convert.ToInt32(txtBox6.Text);

                if (N <= i)
                {
                    timer1.Stop();
                    return;
                }

                double A = Convert.ToDouble(txtBox1.Text);
                double B = Convert.ToDouble(txtBox2.Text);
                double w = Convert.ToDouble(txtBox3.Text);
                double dT = Convert.ToDouble(txtBox4.Text);
                double P = Convert.ToDouble(txtBox5.Text);
                int Number = Convert.ToInt32(txtBox6.Text);

                double[] tn = new double[N];
                double[] yn = new double[N];
                for (int i = 0; i < N; i++)
                {
                    tn[i] = i * dT;
                    yn[i] = A * Math.Sin(w * (tn[i]) + P);

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(tn[i].ToString());
                    chart1.ChartAreas[0].AxisX.Minimum = tn[i];
                    chart1.ChartAreas[0].AxisX.Maximum = tn[i] + 1;
                    lvi.SubItems.Add(yn[i].ToString());
                    chart1.ChartAreas[0].AxisX.Minimum = yn[i];
                    chart1.ChartAreas[0].AxisX.Maximum = yn[i] + 1;
                    LsvData.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        void CalCos()
        {
            try
            {
                int N = Convert.ToInt32(txtBox6.Text);

                if (N <= i)
                {
                    timer1.Stop();
                    return;
                }

                double A = Convert.ToDouble(txtBox1.Text);
                double B = Convert.ToDouble(txtBox2.Text);
                double w = Convert.ToDouble(txtBox3.Text);
                double dT = Convert.ToDouble(txtBox4.Text);
                double P = Convert.ToDouble(txtBox5.Text);
                int Number = Convert.ToInt32(txtBox6.Text);

                double[] tn = new double[N];
                double[] yn = new double[N];
                for (int i = 1; i < N; i++)
                {
                    tn[i] = i * dT;
                    yn[i] = A * Math.Cos(w * (tn[i]) + P);

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(tn[i].ToString());
                    lvi.SubItems.Add(yn[i].ToString());
                    LsvData.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "y = A sin(wt + P) + B":
                    //CalSin();
                    //PushXY();
                    tickIdx++;
                    break;

                case "y = A cos(wt + P) + B":
                    CalCos();
                    //PushXY();
                    //tickIdx++;
                    break;
                    i++;
            }

            PushXY();
            tickIdx++;
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            timer1.Stop();
           LsvData.Items.Clear();
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void LsvData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
