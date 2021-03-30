using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class dlgDMMAddress : Form {
        public dlgDMMAddress() {
            InitializeComponent();
            txtDMMAddress.Text = Properties.Settings.Default.DMMAddress;
            txtMMC2Address.Text= Properties.Settings.Default.MMC2Address;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            Properties.Settings.Default.DMMAddress = txtDMMAddress.Text;
            Properties.Settings.Default.MMC2Address= txtMMC2Address.Text;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
