using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX2
{
    public partial class Form1 : Form
    {
        public int num = 0;
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
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            calculate();
        }

        public void calculate()
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

        void tickCal()
        {
            btnCal.Enabled = false;
            timer1.Stop();
            timer1.Interval = 200;
            timer1.Start();
        }

        void CalSin()
        {
            try
            {
                double A = Convert.ToDouble(txtA.Text);
                double B = Convert.ToDouble(txtB.Text);
                double w = Convert.ToDouble(txtOmega.Text);
                double dT = Convert.ToDouble(txtDt.Text);
                double P = Convert.ToDouble(txtInitPhase.Text);
                int N = Convert.ToInt32(txtNumber.Text);

                double[] tn = new double[N];
                double[] yn = new double[N];
                for (int i = 0; i < N; i++)
                {
                    tn[i] = i * dT;
                    yn[i] = A * Math.Sin(w * (tn[i]) + P);

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


        void CalSin2()
        {
            try
            {
                int N = Convert.ToInt32(txtNumber.Text);

                if(N<=i)
                {
                    timer1.Stop();
                    return;
                }

                double A = Convert.ToDouble(txtA.Text);
                double B = Convert.ToDouble(txtB.Text);
                double w = Convert.ToDouble(txtOmega.Text);
                double dT = Convert.ToDouble(txtDt.Text);
                double P = Convert.ToDouble(txtInitPhase.Text);

                double tn;
                double yn;

                tn = i * dT;
                yn = A * Math.Sin(w * (tn) + P);
                i++;

                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                lvi.SubItems.Add(tn.ToString());
                lvi.SubItems.Add(yn.ToString());
                LsvData.Items.Add(lvi);
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
                double A = Convert.ToDouble(txtA.Text);
                double B = Convert.ToDouble(txtB.Text);
                double w = Convert.ToDouble(txtOmega.Text);
                double dT = Convert.ToDouble(txtDt.Text);
                double P = Convert.ToDouble(txtInitPhase.Text);
                int N = Convert.ToInt32(txtNumber.Text);

                double[] tn = new double[N];
                double[] yn = new double[N];
                for (int i = 0; i < N; i++)
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtA.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "y = A sin(wt + P) + B":
                    CalSin2();
                    break;

                case "y = A cos(wt + P) + B":
                    CalCos();
                    break;
            }
        }

        private void btnCalulator2_Click(object sender, EventArgs e)
        {
            LsvData.Items.Clear();
            i = 0;
            tickCal();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnCal.Enabled = true;
            timer1.Stop();
        }
    }
}
