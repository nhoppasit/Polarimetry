using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exam_1
{
    public partial class dlgAdd : Form
    {
        public string ID { get { return textBox1.Text; }  }
        public string FirstName { get { return textBox2.Text; }  }
        public string LastName { get { return textBox4.Text; }  }
        public string NickName { get { return textBox3.Text; }  }
        public decimal Age { get { return numericUpDown1.Value; } }
        public bool Gender { get { return radioButton1.Checked; } }
        public string Province { get { return comboBox1.Text; } }
        
       

        public dlgAdd()
        {
            InitializeComponent();

            comboBox1.Items.Add("Bangkok");
            comboBox1.Items.Add("Nonthaburi");
            comboBox1.Items.Add("Pathumthani");

            


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox4.Text = string.Empty;
            numericUpDown1.Value = 0;
            radioButton1.Checked = true;
            radioButton2.Checked = true;
            comboBox1.ValueMember = String.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { this.Text = comboBox1.Text; }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                System.Diagnostics.Trace.WriteLine(ex.StackTrace);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
