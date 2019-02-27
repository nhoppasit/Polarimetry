using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polarimeter2019
{
    public partial class frmNewMeasurement : Form
    {
        private string mSampleName;
        int mAverageNumber;
        int mNumberOfRepeatation;
    
        public string SampleName { get { return txtSampleNamee.Text; } }
        public decimal RepeatNumber { get { return numAverageNumber.Value; } }
        public decimal OfRepeatation { get { return numRepeatation.Value; } }

        public frmNewMeasurement()
        {
            InitializeComponent();
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

        public bool Verify()
        {
            if (txtSampleNamee.Text == "")
                return false;
            else
                return true;
        }
    }
}
