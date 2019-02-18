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
        //Constants
        const var StepFactor = 0.013325; //Deg /Step 
        //var คือ value??

        //Scaning & Data
        BaseDataControl TheData; 
        bool IsScanning = false;
        bool IsContinuing = false;
        int CurrentPointIndex = 0;
        double SpecificRotation;
        int NumberOfRepeatation;
        int SelectedIndex;

        //ColorTable
        public Color ReferenceColor = Color.Red;
        public Color[] ColorTable; //บรรทัดนี้ทำให้ ColorTable บรรทัด110ผิด
        
        public frmMain()
        {
            InitializeComponent();
        }

        class SurroundingClass
        {
            private Ivi.Visa.Interop.IFormattedIO488 DMM;
            private Ivi.Visa.Interop.IFormattedIO488 MMC;

            private void DisconnectDevices()
            {
                try
                {
                    DMM.IO.Close();
                    DMM.IO = null;
                    MMC.IO.Close();
                    MMC.IO = null;
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("IO Error: " + ex.Message, MsgBoxStyle.Critical);
                    lblDMM.Text = "Disconnected";
                    lblDMM.BackColor = Color.Red;
                    lblMMC.Text = "Disconncected";
                    lblMMC.BackColor = Color.Red;
                    
                }
            }
            private void ConnectedDevices()
            {
                try
                {
                    //CONNECT DMM
                    Ivi.Visa.Interop.ResourceManager mgr1;
                    string DMMAddress;
                    DMMAddress = txtDMMAddress.Text;
                    mgr1 = new Ivi.Visa.Interop.ResourceManager();
                    DMM = new Ivi.Visa.Interop.FormattedIO488();
                    DMM.IO() = mgr1.Open(DMMAddress);
                    DMM.IO.Timeout = 7000;
                    DMM.WriteString("CONF:VOLT:DC " + txtVoltageRange.Text + ", " + txtVoltageResolution.Text);
                    DMM.WriteString("TRIG:SOUR TMM");
                    DMM.WriteString("TRIG:DEL 1.5E-3");
                    DMM.WriteString("TRIG:COUNT 1");
                    DMM.WriteString("SAMP:COUNT 1");

                    //CONNECT MMC
                    Ivi.Visa.Interop.ResourceManager mgr2;
                    string MMCAddress;
                    MMCAddress = txtMMCAddress.Text;
                    mgr2 = new Ivi.Visa.Interop.ResourceManager();
                    MMC = new Ivi.Visa.Interop.FormattedIO488();
                    MMC.IO() = mgr2.Open(MMCAddress);
                    MMC.IO.Timeout = 7000;

                    // MsgBox("Connect devices are successful.")
                    lblDMM.Text = "Connected";
                    lblDMM.BackColor = Color.Lime;
                    lblMMC.Text = "Conncected";
                    lblMMC.BackColor = Color.Lime;
                }
                catch(Exception ex)
                {
                    Interaction.MsgBox("InitIO Error:" + Constants.vbCrLf + ex.Message);
                    lblDMM.Text = "Disconnected";
                    lblDMM.BackColor = Color.Red;
                    lblMMC.Text = "Disconncected";
                    lblMMC.BackColor = Color.Red;
                }
            }
        }

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
