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
        private int mAverageNumber;
        private int mNumberOfRepeatation;

        public string SampleName { get { return txtSampleName.Text; } }
        public int RepeatNumber
        {
            get
            {
                return mAverageNumber;
            }
        }

        public int NumberOfRepeatation
        {
            get
            {
                return mNumberOfRepeatation;
            }
        }

        public frmNewMeasurement()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            mAverageNumber = numAverageNumber.Value;
            mNumberOfRepeatation = numRepeatation.Value;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Verify()
        {
            if (txtSampleName.Text == "")
                return false;
            else
                return true;
        }
    }
}
