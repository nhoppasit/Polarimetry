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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public static object ReferenceColor { get; internal set; }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewMeasurement mea = new frmNewMeasurement();
            DialogResult result = mea.ShowDialog();
        }

        internal static Color ColorTable(int v)
        {
            throw new NotImplementedException();
        }

        internal static void ApplyColorTableToSamples()
        {
            throw new NotImplementedException();
        }

        private void colorTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmColorTable f = new frmColorTable();
            DialogResult result = f.ShowDialog();
        }
    }
}
