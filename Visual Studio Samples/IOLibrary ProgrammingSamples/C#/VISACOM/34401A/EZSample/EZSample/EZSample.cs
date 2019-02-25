using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ivi.Visa.Interop;


namespace WindowsApplication1
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class EZSample : System.Windows.Forms.Form
    {
        private Ivi.Visa.Interop.FormattedIO488 ioDmm;


        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtIDString;
        private System.Windows.Forms.TextBox txtRevision;
        private System.Windows.Forms.TextBox txtToDisplay;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblIDString;
        private System.Windows.Forms.Label lblRevision;
        private System.Windows.Forms.Label lblToDisplay;
        private System.Windows.Forms.Button btnGetID;
        private System.Windows.Forms.Button btnGetRev;
        private System.Windows.Forms.Button btnToDisplay;
        private System.Windows.Forms.Button btnClearDisplay;
        private System.Windows.Forms.Button btnTake3DC;
        private System.Windows.Forms.Button btnTake1AC;
        private System.Windows.Forms.Label lblReading3;
        private System.Windows.Forms.Label lblReading2;
        private System.Windows.Forms.Label lblReading1;
        private System.Windows.Forms.TextBox txtReading3;
        private System.Windows.Forms.TextBox txtReading2;
        private System.Windows.Forms.TextBox txtReading1;
        private System.Windows.Forms.Button btnInitIO;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.IContainer components;

        public EZSample()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EZSample));
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtIDString = new System.Windows.Forms.TextBox();
            this.txtRevision = new System.Windows.Forms.TextBox();
            this.txtToDisplay = new System.Windows.Forms.TextBox();
            this.txtReading1 = new System.Windows.Forms.TextBox();
            this.txtReading2 = new System.Windows.Forms.TextBox();
            this.txtReading3 = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblIDString = new System.Windows.Forms.Label();
            this.lblRevision = new System.Windows.Forms.Label();
            this.lblToDisplay = new System.Windows.Forms.Label();
            this.lblReading1 = new System.Windows.Forms.Label();
            this.lblReading2 = new System.Windows.Forms.Label();
            this.lblReading3 = new System.Windows.Forms.Label();
            this.btnInitIO = new System.Windows.Forms.Button();
            this.btnGetID = new System.Windows.Forms.Button();
            this.btnGetRev = new System.Windows.Forms.Button();
            this.btnToDisplay = new System.Windows.Forms.Button();
            this.btnClearDisplay = new System.Windows.Forms.Button();
            this.btnTake1AC = new System.Windows.Forms.Button();
            this.btnTake3DC = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(104, 16);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(208, 20);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.Text = "GPIB::22";
            // 
            // txtIDString
            // 
            this.txtIDString.Location = new System.Drawing.Point(104, 72);
            this.txtIDString.Name = "txtIDString";
            this.txtIDString.ReadOnly = true;
            this.txtIDString.Size = new System.Drawing.Size(208, 20);
            this.txtIDString.TabIndex = 1;
            this.txtIDString.Text = "";
            // 
            // txtRevision
            // 
            this.txtRevision.Location = new System.Drawing.Point(104, 104);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.ReadOnly = true;
            this.txtRevision.Size = new System.Drawing.Size(208, 20);
            this.txtRevision.TabIndex = 2;
            this.txtRevision.Text = "";
            // 
            // txtToDisplay
            // 
            this.txtToDisplay.Location = new System.Drawing.Point(104, 136);
            this.txtToDisplay.Name = "txtToDisplay";
            this.txtToDisplay.Size = new System.Drawing.Size(208, 20);
            this.txtToDisplay.TabIndex = 3;
            this.txtToDisplay.Text = "34401A EZ Sample";
            // 
            // txtReading1
            // 
            this.txtReading1.Location = new System.Drawing.Point(88, 24);
            this.txtReading1.Name = "txtReading1";
            this.txtReading1.Size = new System.Drawing.Size(128, 20);
            this.txtReading1.TabIndex = 7;
            this.txtReading1.Text = "";
            // 
            // txtReading2
            // 
            this.txtReading2.Location = new System.Drawing.Point(88, 56);
            this.txtReading2.Name = "txtReading2";
            this.txtReading2.Size = new System.Drawing.Size(128, 20);
            this.txtReading2.TabIndex = 8;
            this.txtReading2.Text = "";
            // 
            // txtReading3
            // 
            this.txtReading3.Location = new System.Drawing.Point(88, 88);
            this.txtReading3.Name = "txtReading3";
            this.txtReading3.Size = new System.Drawing.Size(128, 20);
            this.txtReading3.TabIndex = 9;
            this.txtReading3.Text = "";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(8, 20);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(88, 16);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Address";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblIDString
            // 
            this.lblIDString.Location = new System.Drawing.Point(8, 72);
            this.lblIDString.Name = "lblIDString";
            this.lblIDString.Size = new System.Drawing.Size(88, 16);
            this.lblIDString.TabIndex = 5;
            this.lblIDString.Text = "ID String";
            this.lblIDString.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRevision
            // 
            this.lblRevision.Location = new System.Drawing.Point(8, 104);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(88, 16);
            this.lblRevision.TabIndex = 6;
            this.lblRevision.Text = "Revision";
            this.lblRevision.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblToDisplay
            // 
            this.lblToDisplay.Location = new System.Drawing.Point(8, 136);
            this.lblToDisplay.Name = "lblToDisplay";
            this.lblToDisplay.Size = new System.Drawing.Size(88, 16);
            this.lblToDisplay.TabIndex = 7;
            this.lblToDisplay.Text = "Display String";
            this.lblToDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblReading1
            // 
            this.lblReading1.Location = new System.Drawing.Point(16, 24);
            this.lblReading1.Name = "lblReading1";
            this.lblReading1.Size = new System.Drawing.Size(64, 16);
            this.lblReading1.TabIndex = 10;
            this.lblReading1.Text = "Reading 1";
            this.lblReading1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblReading2
            // 
            this.lblReading2.Location = new System.Drawing.Point(16, 56);
            this.lblReading2.Name = "lblReading2";
            this.lblReading2.Size = new System.Drawing.Size(64, 16);
            this.lblReading2.TabIndex = 11;
            this.lblReading2.Text = "Reading 2";
            this.lblReading2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblReading3
            // 
            this.lblReading3.Location = new System.Drawing.Point(16, 88);
            this.lblReading3.Name = "lblReading3";
            this.lblReading3.Size = new System.Drawing.Size(64, 16);
            this.lblReading3.TabIndex = 12;
            this.lblReading3.Text = "Reading 3";
            this.lblReading3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnInitIO
            // 
            this.btnInitIO.Location = new System.Drawing.Point(328, 16);
            this.btnInitIO.Name = "btnInitIO";
            this.btnInitIO.Size = new System.Drawing.Size(104, 20);
            this.btnInitIO.TabIndex = 8;
            this.btnInitIO.Text = "Initialize IO";
            this.toolTip1.SetToolTip(this.btnInitIO, "Click to initialize the IO enviornment");
            this.btnInitIO.Click += new System.EventHandler(this.btnInitIO_Click);
            // 
            // btnGetID
            // 
            this.btnGetID.Location = new System.Drawing.Point(328, 72);
            this.btnGetID.Name = "btnGetID";
            this.btnGetID.Size = new System.Drawing.Size(104, 20);
            this.btnGetID.TabIndex = 9;
            this.btnGetID.Text = "Get ID String";
            this.toolTip1.SetToolTip(this.btnGetID, "Click to get the ID of the instrument");
            this.btnGetID.Click += new System.EventHandler(this.btnGetID_Click);
            // 
            // btnGetRev
            // 
            this.btnGetRev.Location = new System.Drawing.Point(328, 104);
            this.btnGetRev.Name = "btnGetRev";
            this.btnGetRev.Size = new System.Drawing.Size(104, 20);
            this.btnGetRev.TabIndex = 10;
            this.btnGetRev.Text = "Get Revision";
            this.toolTip1.SetToolTip(this.btnGetRev, "Click to get the SCPI version for which the instrument complies");
            this.btnGetRev.Click += new System.EventHandler(this.btnGetRev_Click);
            // 
            // btnToDisplay
            // 
            this.btnToDisplay.Location = new System.Drawing.Point(328, 136);
            this.btnToDisplay.Name = "btnToDisplay";
            this.btnToDisplay.Size = new System.Drawing.Size(104, 20);
            this.btnToDisplay.TabIndex = 11;
            this.btnToDisplay.Text = "Send to Display";
            this.toolTip1.SetToolTip(this.btnToDisplay, "Click to send the display string to the instrument");
            this.btnToDisplay.Click += new System.EventHandler(this.btnToDisplay_Click);
            // 
            // btnClearDisplay
            // 
            this.btnClearDisplay.Location = new System.Drawing.Point(328, 160);
            this.btnClearDisplay.Name = "btnClearDisplay";
            this.btnClearDisplay.Size = new System.Drawing.Size(104, 20);
            this.btnClearDisplay.TabIndex = 12;
            this.btnClearDisplay.Text = "Clear Display";
            this.toolTip1.SetToolTip(this.btnClearDisplay, "Click to clear the instrument display");
            this.btnClearDisplay.Click += new System.EventHandler(this.btnClearDisplay_Click);
            // 
            // btnTake1AC
            // 
            this.btnTake1AC.Location = new System.Drawing.Point(240, 24);
            this.btnTake1AC.Name = "btnTake1AC";
            this.btnTake1AC.Size = new System.Drawing.Size(160, 20);
            this.btnTake1AC.TabIndex = 13;
            this.btnTake1AC.Text = "Take One Reading (AC)";
            this.toolTip1.SetToolTip(this.btnTake1AC, "Click to take one AC Voltage reading");
            this.btnTake1AC.Click += new System.EventHandler(this.btnTake1AC_Click);
            // 
            // btnTake3DC
            // 
            this.btnTake3DC.Location = new System.Drawing.Point(240, 56);
            this.btnTake3DC.Name = "btnTake3DC";
            this.btnTake3DC.Size = new System.Drawing.Size(160, 20);
            this.btnTake3DC.TabIndex = 14;
            this.btnTake3DC.Text = "Take Three Readings (DC)";
            this.toolTip1.SetToolTip(this.btnTake3DC, "Click to take three DC Voltage readings");
            this.btnTake3DC.Click += new System.EventHandler(this.btnTake3DC_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTake3DC);
            this.groupBox1.Controls.Add(this.btnTake1AC);
            this.groupBox1.Controls.Add(this.lblReading3);
            this.groupBox1.Controls.Add(this.lblReading2);
            this.groupBox1.Controls.Add(this.lblReading1);
            this.groupBox1.Controls.Add(this.txtReading3);
            this.groupBox1.Controls.Add(this.txtReading2);
            this.groupBox1.Controls.Add(this.txtReading1);
            this.groupBox1.Location = new System.Drawing.Point(16, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 120);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Measurements";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(328, 40);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 20);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close IO";
            this.toolTip1.SetToolTip(this.btnClose, "Click to close the IO enviornment");
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EZSample
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(448, 322);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClearDisplay);
            this.Controls.Add(this.btnToDisplay);
            this.Controls.Add(this.btnGetRev);
            this.Controls.Add(this.btnGetID);
            this.Controls.Add(this.btnInitIO);
            this.Controls.Add(this.lblToDisplay);
            this.Controls.Add(this.lblRevision);
            this.Controls.Add(this.lblIDString);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtToDisplay);
            this.Controls.Add(this.txtRevision);
            this.Controls.Add(this.txtIDString);
            this.Controls.Add(this.txtAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EZSample";
            this.Text = "Agilent 34401A EZSample";
            this.Load += new System.EventHandler(this.EZSample_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new EZSample());
        }

        private void EZSample_Load(object sender, System.EventArgs e)
        {
            ioDmm = new FormattedIO488Class();
            SetAccessForClosed();
        }

        private void btnInitIO_Click(object sender, System.EventArgs e)
        {

            try
            {
                ResourceManager grm = new ResourceManager();
                ioDmm.IO = (IMessage)grm.Open(this.txtAddress.Text, AccessMode.NO_LOCK, 2000, "");
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Open failed on " + this.txtAddress.Text + " " + ex.Source + "  " + ex.Message, "EZSample", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ioDmm.IO = null;
                SetAccessForClosed();
                return;
            }
            SetAccessForOpened();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            ioDmm.IO.Close();
            SetAccessForClosed();
        }

        private void btnGetID_Click(object sender, System.EventArgs e)
        {
            ioDmm.WriteString("*IDN?", true);
            this.txtIDString.Text = ioDmm.ReadString();
        }

        private void btnGetRev_Click(object sender, System.EventArgs e)
        {
            ioDmm.WriteString("SYST:VERS?", true);
            this.txtRevision.Text = ioDmm.ReadString();
        }

        private void btnToDisplay_Click(object sender, System.EventArgs e)
        {
            ioDmm.WriteString("SYST:BEEP;:DISP:TEXT '" + this.txtToDisplay.Text + "'", true);
        }

        private void btnClearDisplay_Click(object sender, System.EventArgs e)
        {
            ioDmm.WriteString("SYST:BEEP;:DISP:TEXT:CLEAR", true);
        }

        private void btnTake1AC_Click(object sender, System.EventArgs e)
        {
            ioDmm.WriteString("MEAS:VOLT:AC?", true);
            double dNum = (double)ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_R8, true);
            this.txtReading1.Text = dNum.ToString();
            this.txtReading2.Text = "---";
            this.txtReading3.Text = "---";
        }

        private void btnTake3DC_Click(object sender, System.EventArgs e)
        {
            double[] dNum = new double[3];
            ioDmm.WriteString("CONF:VOLT:DC 10, 0.1;:SAMP:COUN 3", true);
            ioDmm.WriteString("READ?", true);
            dNum = (double[])ioDmm.ReadList(IEEEASCIIType.ASCIIType_R8, ",");
            this.txtReading1.Text = dNum[0].ToString();
            this.txtReading2.Text = dNum[1].ToString();
            this.txtReading3.Text = dNum[2].ToString();
        }

        private void SetAccessForOpened()
        {
            this.btnInitIO.Enabled = false;
            this.txtAddress.Enabled = false;
            this.txtIDString.Enabled = true;
            this.txtRevision.Enabled = true;
            this.txtReading1.Enabled = true;
            this.txtReading2.Enabled = true;
            this.txtReading3.Enabled = true;
            this.txtToDisplay.Enabled = true;
            this.btnClearDisplay.Enabled = true;
            this.btnClose.Enabled = true;
            this.btnGetID.Enabled = true;
            this.btnGetRev.Enabled = true;
            this.btnTake1AC.Enabled = true;
            this.btnTake3DC.Enabled = true;
            this.btnToDisplay.Enabled = true;
        }

        private void SetAccessForClosed()
        {
            this.btnInitIO.Enabled = true;
            this.txtAddress.Enabled = true;
            this.txtIDString.Enabled = false;
            this.txtRevision.Enabled = false;
            this.txtReading1.Enabled = false;
            this.txtReading2.Enabled = false;
            this.txtReading3.Enabled = false;
            this.txtToDisplay.Enabled = false;
            this.btnClearDisplay.Enabled = false;
            this.btnClose.Enabled = false;
            this.btnGetID.Enabled = false;
            this.btnGetRev.Enabled = false;
            this.btnTake1AC.Enabled = false;
            this.btnTake3DC.Enabled = false;
            this.btnToDisplay.Enabled = false;
        }

    }
}