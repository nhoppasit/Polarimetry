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
        #region DECRALATION
        //Constants
        const var StepFactor = 0.013325; //Deg /Step 
        ////var คือ value??

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
        public Color[] ColorTable; ////บรรทัดนี้ทำให้ ColorTable บรรทัด110ผิด
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        #region Devices
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
                    string message = "IO Error";
                    string caption = "Error";
                    //Interaction.MsgBox("IO Error: " + ex.Message, MsgBoxStyle.Critical); //// กล่องข้อความ??
                    MessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning); ////ขึ้นข้อความเตือน
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
                    DMM.WriteString("CONF:VOLT:DC " + txtVoltageRange.Text + ", " + txtVoltageResolution.Text); //// กล่องข้อความ
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
                    string message = "InitIO Error";
                    string caption = "Error";
                    //Interaction.MsgBox("InitIO Error:" + Constants.vbCrLf + ex.Message); //// กล่องข้อความ
                    MessageBox.Show(message, caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning); ////ขึ้นข้อความเตือน
                    lblDMM.Text = "Disconnected";
                    lblDMM.BackColor = Color.Red;
                    lblMMC.Text = "Disconncected";
                    lblMMC.BackColor = Color.Red;
                }
            }
        }
        #endregion

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
        
        #region Control Panel

        private void DoStart()
        {
            // Add curve
            double[] x = new double[1];
            double[] y = new double[1];

            ResetDynaplot();

            PlotReferenceCurve();

            // ----------------------------------------
            // 1. Update buttons
            // ----------------------------------------
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;

            // ----------------------------------------
            // disable box
            // ----------------------------------------
            gbStartMea.Enabled = false;
            gbSample.Enabled = false;
            gbScanCondition.Enabled = false;

            // ----------------------------------------
            // 2. start Test loop of reading light intensity
            // ----------------------------------------
            CurrentPointIndex = 0;
            IsScanning = true;
            lblMainStatus.Text = "Measuring...";

            DoScanLightIntensity();

            lblMainStatus.Text = "Ready";
        }

        private void DoStop()
        {
            //----------------------------------------
            //1. stop Test loop of reading light intensity
            //----------------------------------------
            StopScanning();

            //----------------------------------------
            //2. Update buttons
            //----------------------------------------
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";
        }

        private void DoPause()
        {
            // ----------------------------------------
            // 1. pause/continue Test loop of reading light intensity
            // ----------------------------------------
            if (btnPause.Text == "PAUSE")
            {
                DoPauseScanning();
            }

            else
            {   
                DoContinueScanning();
            }
            // ----------------------------------------
            // 2. Update buttons
            // ----------------------------------------
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            if (btnPause.Text == "PAUSE")
                btnPause.Text = "CONTINUE";
            else
            {
                btnPause.Text = "PAUSE";
                DoScanLightIntensity();
            }
        }

        private void btnStart_Click(System.Object sender, System.EventArgs e)
        {
            DoStart();
        }

        private void btnStop_Click(System.Object sender, System.EventArgs e)
        {
            DoStop();
        }

        private void btnPause_Click(System.Object sender, System.EventArgs e)
        {
            DoPause();
        }
        #endregion

        #region Scanning Procedure

        private void DoScanLightIntensity()
        {
            // --------------------------------------------
            // validate selected index of repeats
            // --------------------------------------------
            if (lsvData.SelectedItems.Count <= 0)
            {
                Interaction.MsgBox("Please select item in samples list view that you want to measure!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly);
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnPause.Enabled = false;
                btnPause.Text = "PAUSE";
                btnNew.Enabled = true;
                gbSample.Enabled = true;
                gbScanCondition.Enabled = true;
                return;
            }

            // --------------------------------------------
            // Confirmation
            // --------------------------------------------
            // If Not IsContinuing Then
            // Dim trt As String
            // If SelectedIndex = 0 Then
            // trt = "Reference data"
            // Else
            // trt = "Sample " & SelectedIndex
            // End If
            // If MsgBox("Are you sure to measure " & trt & "?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            // btnStart.Enabled = True
            // btnStop.Enabled = False
            // btnPause.Enabled = False
            // btnPause.Text = "PAUSE"
            // btnNewMeas.Enabled = True
            // gbSample.Enabled = True
            // gbScanCondition.Enabled = True
            // Exit Sub
            // End If
            // End If

            if (!mnuOptionsDemomode.Checked)
                ConnectDevices();

            try
            {
                // --------------------------------------------
                // get read conditions
                // --------------------------------------------
                double ThetaA = System.Convert.ToDouble(txtStart.Text);
                double ThetaB = System.Convert.ToDouble(txtStop.Text);
                double Delta = System.Convert.ToDouble(txtResolution.Text);

                // --------------------------------------------
                // initialize minimum finder
                // --------------------------------------------
                if (!IsContinuing)
                {
                    if (SelectedIndex == 0)
                        TheData.Reference.Ym = 99999999;
                    else if (TheData.Data != null)
                    {
                        if (SelectedIndex <= TheData.Data.Length)
                            TheData.Data(SelectedIndex - 1).Ym = 99999999;
                    }
                }

                // ----------------------------------------------------------------
                // REAL INTERFACE YES OR NOT (Theta,I)
                // ----------------------------------------------------------------
                double CurrentLightIntensity;
                int StepNumber;
                string MSG;
                double CurrentTheta;
                if (ThetaA < ThetaB)
                {
                    CurrentTheta = ThetaA + CurrentPointIndex * Delta;
                }
                else if (ThetaA > ThetaB)
                {
                    CurrentTheta = ThetaA - CurrentPointIndex * Delta;
                }
                // check demo mode
                if (mnuOptionsDemomode.Checked == false)
                {
                    // 0.4 GOTO Theta A
                    StepNumber = -1 * System.Convert.ToInt32(CurrentTheta / StepFactor); // step
                    MSG = "A:WP" + StepNumber.ToString() + "P" + StepNumber.ToString();
                    MMC.WriteString(MSG);

                    // 0.5 Read first
                    int nAvg = numRepeatNumber.Value;
                    CurrentLightIntensity = 0;
                    for (int tt = 0; tt <= nAvg - 1; tt++)
                    {
                        DMM.WriteString("READ?");
                        CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber;
                    }
                    CurrentLightIntensity = CurrentLightIntensity / nAvg;
                }
                else
                    CurrentLightIntensity = VBMath.Rnd() * 0.1 + Math.Cos((CurrentTheta - VBMath.Rnd() * 50) * Math.PI / 180) + 2;

                // ----------------------------------------------------------------
                // STORE DATA AND PLOT
                // ----------------------------------------------------------------
                // Save to memory
                if (SelectedIndex == 0)
                    TheData.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                else
                    TheData.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                DefineAngleOfRotation();
                PlotReferenceCurve();
                PlotTreatmentsCurve();
                PlotSelectedTRTMarker();

                // auto scale
                // AxDynaPlot1.Axes.Autoscale()

                // --------------------------------------------
                // MAIN READING LOOP (^0^)
                // --------------------------------------------
                while (IsScanning)
                {
                    Application.DoEvents();

                    // Update current THETA
                    if (ThetaA < ThetaB)
                        CurrentTheta = ThetaA + CurrentPointIndex * Delta;
                    else if (ThetaA > ThetaB)
                        CurrentTheta = ThetaA - CurrentPointIndex * Delta;

                    // --------------------------------------------
                    // CHECK DEMO MODE
                    // --------------------------------------------
                    if (mnuOptionsDemomode.Checked == false)
                    {

                        // --------------------------------------------
                        // REAL INTERFACING
                        // --------------------------------------------
                        // 1. Move polarizer 
                        StepNumber = -1 * System.Convert.ToInt32(CurrentTheta / StepFactor); // step
                        MSG = "A:WP" + StepNumber.ToString() + "P" + StepNumber.ToString();
                        MMC.WriteString(MSG);

                        // 3. Read light intensity
                        int nAvg = numRepeatNumber.Value;
                        CurrentLightIntensity = 0;
                        for (int tt = 0; tt <= nAvg - 1; tt++)
                        {
                            DMM.WriteString("READ?");
                            CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber;
                        }
                        CurrentLightIntensity = CurrentLightIntensity / nAvg;
                    }
                    else

                        // --------------------------------------------
                        // DEMO MODE
                        // --------------------------------------------
                        // Delay.
                        // Dim sw As New Stopwatch
                        // sw.Start()
                        // Do
                        // 'do nothing
                        // Loop Until sw.ElapsedMilliseconds > 50 'ms
                        // 'Simulation
                        CurrentLightIntensity = VBMath.Rnd() * 0.1 + Math.Cos((CurrentTheta - VBMath.Rnd() * 50) * Math.PI / 180) + 2;

                    // Save to memory and update curve
                    if (SelectedIndex == 0)
                        TheData.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                    else
                        TheData.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                    DefineAngleOfRotation();
                    PlotReferenceCurve();
                    PlotTreatmentsCurve();
                    PlotSelectedTRTMarker();

                    // auto scale
                    // AxDynaPlot1.Axes.Autoscale()

                    // check stop condition!!!
                    if (ThetaA < ThetaB)
                    {
                        if (ThetaB < CurrentTheta)
                            IsScanning = false;
                    }
                    else if (ThetaA > ThetaB)
                    {
                        if (CurrentTheta < ThetaB)
                            IsScanning = false;
                    }
                    // --------------------------------------------

                    // next point
                    CurrentPointIndex += 1;
                }
                // --------------------------------------------(^0^)

                // if stop update buttons to a new start
                if (btnPause.Text != "CONTINUE")
                {
                    if (!mnuOptionsDemomode.Checked)
                    {
                        MSG = "A:WP" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString + "P" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString;
                        MMC.WriteString(MSG);
                        DisconnectDevices();
                    }
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                    btnPause.Text = "PAUSE";
                    btnNew.Enabled = true;
                    gbSample.Enabled = true;
                    gbScanCondition.Enabled = true;
                }
                else
                {
                    if (!mnuOptionsDemomode.Checked)
                        DisconnectDevices();
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    btnPause.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);

                // ----------------------------------------
                // 1. stop Test loop of reading light intensity
                // ----------------------------------------
                StopScanning();

                // ----------------------------------------
                // 2. Update buttons
                // ----------------------------------------
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnPause.Enabled = false;
                btnPause.Text = "PAUSE";
                btnNew.Enabled = true;
                gbSample.Enabled = true;
                gbScanCondition.Enabled = true;
            }
        }

        private void StopScanning()
        {
            IsScanning = false;
            IsContinuing = false;
        }

        private void DoPasuseScanning()
        {
            IsScanning = false;
            IsContinuing = false;
        }

        private void DoContinueScanning()
        {
            IsScanning = true;
            IsContinuing = true;
        }

        #endregion

        #region Menu
        
        #region File

        private void ConnectToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            ConnectDevices();
        }

        private void DisconnectToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            DisconnectDevices();
        }

        #endregion
        #endregion

        #region Form Event

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadSetting();  //// loadSetting มาจากไหน??
        }

        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //if (Interaction.MsgBox("Do you want to quit program?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Quit") == MsgBoxResult.Yes) 
            //{
            //    IsScanning = false;
            //    SaveSetting();  //// SaveSetting มาจากไหน??
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
            string message = "Do you want to quit program?";
            string caption = "Polarimeter2019";
            if (MessageBox.Show(message, caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question)) 
            {
                IsScanning = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}
