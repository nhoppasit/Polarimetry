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
        public Form1()
        {
            InitializeComponent();
            IsvData.Columns.Add("No.", 35);
            IsvData.Columns.Add("ΔT", 150);
            IsvData.Columns.Add("y", 150);
            IsvData.View = View.Details;
            IsvData.GridLines = true;
            IsvData.FullRowSelect = true;

            comboBox1.Items.Add("y = A((ωt)+B)");
            comboBox1.Items.Add("y = A(ω+t)");
            comboBox1.Items.Add("y = ωA+B");
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            ListViewItem Isv = new ListViewItem(textBox6.Text);
            Isv.SubItems.Add(textBox4.Text);
            Isv.SubItems.Add(textBox5.Text);
            IsvData.Items.Add(Isv);

            double z, x, c, v;
            z = double.Parse(textBox1.Text);
            x = double.Parse(textBox2.Text);
            c = double.Parse(textBox3.Text);
            v = double.Parse(textBox4.Text);
            textBox5.Text = calculate(z, x, c, v);
        }

        public string calculate(double A, double B,double ω,double t)
        {
            double y = 0;
            if (comboBox1.Text == "y = ωA+B")
            {
                y = (ω * A) + B;
            }
            else if(comboBox1.Text == "y = A(ω+t)")
            {
                y = A * (ω + t);
            }
            else if(comboBox1.Text== "y = A((ωt)+B)")
            {
                y = A * ((ω * t) + B);
            }
            return y.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
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
    }
}
