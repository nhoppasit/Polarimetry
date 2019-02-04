using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX1
{
    public partial class dlgAdd : Form
    {
        public string ID { get { return textBox1.Text; } }
        public string FirstName { get { return textBox2.Text; } }
        public string LastName { get { return textBox3.Text; } }
        public decimal Age { get { return numericUpDown1.Value; } }
        public bool Gender { get { return radioButton1.Checked; } }
        public string Address { get { return comboBox1.Text; } }

        public dlgAdd()
        {
            InitializeComponent();
        }

        private void dlgAdd_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.Items.Add("กรุงเทพมหานคร");
            comboBox1.Items.Add("นครนายก");
            comboBox1.Items.Add("สมุทรสาคร");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
