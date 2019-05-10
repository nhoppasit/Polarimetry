using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ivi.Visa.Interop;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;

namespace Polarimeter2019
{
    public partial class frmMain : Form
    {
        private Ivi.Visa.Interop.FormattedIO488 ioDmm;
        public BaseDataControl BDC;
        Random rand = new Random();
        int tick = 0;

        

        public frmMain()
        {
            InitializeComponent();
            comboAxis.Items.Add("X");
            comboAxis.Items.Add("Y");


            BDC = new BaseDataControl();

            DMM = new Ivi.Visa.Interop.FormattedIO488();
            MMC = new Ivi.Visa.Interop.FormattedIO488();
        }

        #region DECRALATION
        //Constants
        const double StepFactor = 0.013325; //Deg /Step 

        //Scaning & Data
        bool IsScanning = false;
        bool IsContinuing = false;
        int CurrentPointIndex = 0;
        double SpecificRotation;
        int NumberOfRepeatation;
        int SelectedIndex;

        //ColorTable
        public Color ReferenceColor = Color.Red;
        public Color[] ColorTable = new Color[20];
        #endregion

        #region Devices

        private Ivi.Visa.Interop.IFormattedIO488 DMM;
        private Ivi.Visa.Interop.IFormattedIO488 MMC;

        //Disconnect
        void MMCDisconnectDevices()
        {
            try
            {
                MMC.IO.Close();
                MMC.IO = null;
                lblMMC.Text = "Disconncected";
                lblMMC.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO Error" + ex.Message);
            }
        }

        void DMMDisconnectDevices()
        {
            try
            {
                DMM.IO.Close();
                DMM.IO = null;
                lblDMM.Text = "Disconnected";
                lblDMM.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO Error" + ex.Message);
            }
        }

        void DisconnectDevices()
        {
            try
            {
                DMMDisconnectDevices();
                MMCDisconnectDevices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO Error" + ex.Message);
            }
        }

        //Connect
        void DMMConnection()
        {
            try
            {
                //CONNECT DMM
                Ivi.Visa.Interop.ResourceManager mgr1;
                string DMMAddress;
                DMMAddress = txtDMMAddress.Text;
                mgr1 = new Ivi.Visa.Interop.ResourceManager();
                DMM.IO = (IMessage)mgr1.Open(DMMAddress);
                DMM.IO.Timeout = 7000;
                DMM.WriteString("*CLS");
                System.Threading.Thread.Sleep(100);
                DMM.WriteString("CONF:VOLT:DC " + txtVoltageRange.Text + ", " + txtVoltageResolution.Text);
                DMM.WriteString("TRIG:SOUR IMM");
                DMM.WriteString("TRIG:DEL 1.5E-3");
                DMM.WriteString("TRIG:COUNT 1");
                DMM.WriteString("SAMP:COUNT 1");
                // MsgBox("Connect devices are successful.")
                lblDMM.Text = "Connected";
                lblDMM.BackColor = Color.Lime;
            }
            catch (Exception ex)
            {                 //Interaction.MsgBox("InitIO Error:" + Constants.vbCrLf + ex.Message);
                MessageBox.Show("InitIO Error:" + MessageBoxButtons.RetryCancel + ex.Message);
                lblDMM.Text = "Disconnected";
                lblDMM.BackColor = Color.Red;
            }

        }

        void MMCConnection()
        {
            try
            { //CONNECT MMC
                Ivi.Visa.Interop.ResourceManager mgr2;
                string MMCAddress;
                MMCAddress = txtMMCAddress.Text;
                mgr2 = new Ivi.Visa.Interop.ResourceManager();
                MMC.IO = (IMessage)mgr2.Open(MMCAddress);
                MMC.IO.Timeout = 7000;
                lblMMC.Text = "Conncected";
                lblMMC.BackColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblMMC.Text = "Disconncected";
                lblMMC.BackColor = Color.Red;
            }

        }

        void ConnectedDevices()
        {
            try
            {
                DMMConnection();
                MMCConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Control Panel

        private void DoStart()
        {
            // Add curve

            double[] x = new double[1];
            double[] y = new double[1];

            //ResetDynaplot();

            // Plot curve from initial data
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
            gbMeasurement.Enabled = true;

            // ----------------------------------------
            // 2. start Test loop of reading light intensity
            // ----------------------------------------
            CurrentPointIndex = 0;
            IsScanning = true;
            lblMainStatus.Text = "Measuring...";
            DoScanLightIntensity();

            // end
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
                DoPasuseScanning();
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

        Random Rnd = new Random();
        public void DoScanLightIntensity()
        {
            PolarChart();
            // --------------------------------------------
            // validate selected index of repeats
            // --------------------------------------------
            if (lsvData.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select item in samples list view that you want to measure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //btnStart.Enabled = true;
                //btnStop.Enabled = false;
                //btnPause.Enabled = false;
                //btnPause.Text = "PAUSE";
                //btnNew.Enabled = true;
                //btnOpen.Enabled = true;
                gbSample.Enabled = false;
                gbScanCondition.Enabled = false;
                return;
            }

            if (!mnuOptionsDemomode.Checked)
            {
                ConnectedDevices();
            }

            try
            {
                timer1.Start();
                // --------------------------------------------
                // get read conditions
                // --------------------------------------------
                double ThetaA = System.Convert.ToDouble(txtStart.Text);
                double ThetaB = System.Convert.ToDouble(txtStop.Text);
                double Delta = System.Convert.ToDouble(txtResolution.Text);

                // --------------------------------------------
                // Evaluate size of array of X[], Y[]
                // --------------------------------------------
                double dNumberOfPoint = Math.Abs((ThetaA - ThetaB) / Delta);
                int NumberOfPoint = (int)Math.Floor(dNumberOfPoint);
                if (0 < (dNumberOfPoint % Math.Floor(dNumberOfPoint)))
                {
                    NumberOfPoint++;
                }
                NumberOfPoint++;
                ThetaB = (double)(NumberOfPoint - 1) * Delta + ThetaA;
                txtStop.Text = ThetaB.ToString();
                BDC.Reference.X = new double[NumberOfPoint];
                BDC.Reference.Y = new double[NumberOfPoint];

                for (int i = 0; i < BDC.Data.Length; i++)
                {
                    BDC.Data[i].X = new double[NumberOfPoint];
                    BDC.Data[i].Y = new double[NumberOfPoint];
                }


                // --------------------------------------------
                // initialize minimum finder
                // --------------------------------------------
                if (!IsContinuing)
                {
                    if (SelectedIndex == 0)
                        BDC.Reference.Ym = 99999999;
                    else if (BDC.Data != null)
                    {
                        if (SelectedIndex <= BDC.Data.Length)
                            BDC.Data[SelectedIndex - 1].Ym = 99999999;
                    }
                }

                // ----------------------------------------------------------------
                // REAL INTERFACE YES OR NOT (Theta,I)
                // ----------------------------------------------------------------
                double CurrentLightIntensity;
                int StepNumber;
                string MSG;
                double CurrentTheta = 0;
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
                    int nAvg = (int)numRepeatNumber.Value;
                    CurrentLightIntensity = 0;
                    for (int tt = 0; tt <= nAvg - 1; tt++)
                    {
                        DMM.WriteString("READ?");
                        CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber();
                    }
                    CurrentLightIntensity = CurrentLightIntensity / nAvg;

                    foreach (Series ptseries in chart1.Series)
                    {
                        double x = ((System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text)) + (tick * (System.Convert.ToDouble(1 * Convert.ToDouble(txtResolution.Text))))));
                        double y = CurrentLightIntensity;
                        System.Diagnostics.Trace.WriteLine(string.Format(">>> (X, Y) = ({0}, {1})", x, y));
                        ptseries.Points.AddXY(x, y);
                    }
                    chart1.Invalidate();
                }
                else
                {
                    // CAUTION! DEMO MODE HERE
                    CurrentLightIntensity = Rnd.NextDouble() * 0.1 + Math.Cos((CurrentTheta - Rnd.NextDouble() * 50) * Math.PI / 180) + 2;
                }
                // ----------------------------------------------------------------
                // STORE DATA AND PLOT
                // ----------------------------------------------------------------
                // Save to memory

                if (SelectedIndex == 0)
                {
                    BDC.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                }
                else
                {
                    BDC.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                }
                //
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
                        int nAvg = (int)numRepeatNumber.Value;
                        CurrentLightIntensity = 0;
                        for (int tt = 0; tt <= nAvg - 1; tt++)
                        {
                            DMM.WriteString("READ?");
                            CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber();
                        }
                        CurrentLightIntensity = CurrentLightIntensity / nAvg;

                        foreach (Series ptseries in chart1.Series)
                        {
                            double x = ((System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text)) + (tick * (System.Convert.ToDouble(1 * Convert.ToDouble(txtResolution.Text))))));
                            double y = CurrentLightIntensity;
                            System.Diagnostics.Trace.WriteLine(string.Format(">>> (X, Y) = ({0}, {1})", x, y));
                            ptseries.Points.AddXY(x, y);
                        }
                        chart1.Invalidate();
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
                        CurrentLightIntensity = Rnd.NextDouble() * 0.1 + Math.Cos((CurrentTheta - Rnd.NextDouble() * 50) * Math.PI / 180) + 2;

                    // Save to memory and update curve
                    try
                    {
                        if (SelectedIndex == 0)
                        {
                            BDC.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                        }
                    }
                    catch (Exception)
                    {
                        BDC.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                        DefineAngleOfRotation();
                        PlotReferenceCurve();
                        PlotTreatmentsCurve();
                        PlotSelectedTRTMarker();
                    }
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
                } // end of while
                // --------------------------------------------(^0^)

                // if stop update buttons to a new start
                if (btnPause.Text != "CONTINUE")
                {
                    if (!mnuOptionsDemomode.Checked)
                    {
                        MSG = "A:WP" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString() + "P" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString();
                        MMC.WriteString(MSG);
                        DisconnectDevices();
                    }
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                    btnPause.Text = "PAUSE";
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
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
                MessageBox.Show(ex.Message);

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
                btnOpen.Enabled = true;
                gbMeasurement.Enabled = true;
                gbStartMea.Enabled = true;
                gbSample.Enabled = true;
                gbScanCondition.Enabled = true;
            }
        }

        private void StopScanning()
        {
            IsScanning = false;
            IsContinuing = false;
            string MSG = "A:WP0P0";
            MMC.WriteString(MSG);
        }

        private void DoPasuseScanning()
        {
            IsScanning = false;
            IsContinuing = false;
        }

        private void DoContinueScanning()
        {
            DoStart();
            IsScanning = true;
            IsContinuing = true;
        }

        #endregion

        //ยังไม่เสร็จ
        #region Menu

        //ยังไม่เสร็จ
        #region File

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewMeasurement();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDC.OpenFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDC.SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)  //ยังไม่เสร็จ
        {
            //try
            //{
            //    if()
            //    {
            //        BDC.SaveFile();
            //    }
            //    else
            //    {
            //        MessageBox.Show("The data have been save")
            //    }
            //}
            //catch
            //{

            //}
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuExportToImageFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Bit map (*.bmp)|*.bmp|All File (*.*)|*.*";
                DialogResult redlg = dlg.ShowDialog();
                if (redlg != System.Windows.Forms.DialogResult.OK) ;
                {
                    return;
                }
                string path = dlg.FileName;

                //AxDynaPlot1.ToFile(path)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Measure

        private void mnuStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStart();
        }

        private void mnuStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStop();
        }

        private void mnuPauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPause();
        }

        private void mnuCoutinewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPause();
        }

        #endregion

        #region Devices

        private void ConnectToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            ConnectedDevices();
        }

        private void DisconnectToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            DisconnectDevices();
        }

        private void mnuDevicesClearDMM_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                ConnectedDevices();
                DMM.WriteString("*CLS");
                DisconnectDevices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuDevicesResetDMM_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                ConnectedDevices();
                DMM.WriteString("*RST");
                DisconnectDevices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Polarimeter2019 program. (c)2019, Physics, KMITL. Design by S. Saejia.");  //// แก้เป็นปี 2019
        }

        private void colorTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmColorTable f = new frmColorTable(this);
            DialogResult result = f.ShowDialog();
            ResetDynaplot();
            PlotReferenceCurve();
            PlotTreatmentsCurve();
            PlotSelectedTRTMarker();
        }

        #endregion

        #region Form Event

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadSetting();
            gbSample.Enabled = false;
            gbMeasurement.Enabled = false;
            gbScanCondition.Enabled = false;
        }

        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)

        {
            DialogResult result = MessageBox.Show("Do you want to quit program?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                IsScanning = false;
                SaveSetting();
            }
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region PLOT when select curve

        private void ResetDynaplot()
        {
            try
            {
                double[] x = new double[1];
                double[] y = new double[1];

                // Remove all curves

                for (int i = 0; i <= NumberOfRepeatation - 1; i++)
                {
                    // add some data to series
                    // nothing for now!
                }
            }
            catch (Exception ex)
            {
                //Information.Err.Clear();  //// กล่องข้อความ Error ?
            }
        }

        private bool PlotReferenceCurve()
        {
            bool e = false;
            try
            {
                if (lsvData.Items[0].Checked == true)
                {
                    if (BDC.Reference.X != null)
                    {
                        //ReferenceCurve.UpdateData(TheData.Reference.X, TheData.Reference.Y, TheData.Reference.X.Length)
                        // ReferenceCurve.Penstyle.Color = RGB(ReferenceColor.R, ReferenceColor.G, ReferenceColor.B)
                        // ReferenceMinMarker.PositionX = TheData.Reference.Xm
                        // ReferenceMinMarker.PositionY = TheData.Reference.Ym
                        lblNullPoint.Text = BDC.Reference.Xm.ToString("0.0000") + " deg";
                        e = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return e;
        }

        private bool PlotTreatmentsCurve()
        {
            //if (BDC == null)
            //    return false;
            //if (BDC.Data.ToString() == null)
            //    return false;
            for (int i = 0; i <= NumberOfRepeatation - 1; i++)
            {
                try
                {
                    if (lsvData.Items[i + 1].Checked)
                    {
                    }
                }
                catch (Exception ex)
                {
                    //Information.Err.Clear();  ////กล่องข้อความ Error ??
                }
            }
            return true;
        }

        private void PlotSelectedTRTMarker()
        {
            try
            {
                if (lsvData.Items[SelectedIndex].Checked)
                    // TreatmentMinMarker.PositionX = TheData.Data(SelectedIndex - 1).Xm
                    // TreatmentMinMarker.PositionY = TheData.Data(SelectedIndex - 1).Ym
                    lblNullPoint.Text = BDC.Data[SelectedIndex - 1].Xm.ToString("0.0000") + " deg";
            }
            catch (Exception ex)
            {
                //Information.Err.Clear(); //// กล่องข้อความ Error ??
            }
        }

        #endregion

        #region Sub-Routine

        private void NewMeasurement()
        {
            // verify user
            if (lsvData.Items.Count > 0)
            {
                DialogResult result = MessageBox.Show("Data will be deleted. Do you want to new measurement?", "New measurement", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {

                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            // load dialog
            frmNewMeasurement f = new frmNewMeasurement();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();

            // do update the job
            if (f.Verify() == true)
            {
                try
                {
                    // get information
                    txtSampleName.Text = f.SampleName;
                    numRepeatNumber.Value = f.RepeatNumber;
                    NumberOfRepeatation = (int)f.OfRepeatation;

                    // initialize the data object
                    BDC = new BaseDataControl();
                    BDC.SampleName = txtSampleName.Text;

                    // Add blank data
                    BDC.Data = new BaseDataControl.strucCurveData[NumberOfRepeatation];

                    // clear
                    lsvData.Items.Clear();
                    ListViewItem lvi;

                    // add ref.
                    lvi = new ListViewItem();
                    lvi.Text = "Reference";
                    lvi.SubItems.Add("-");
                    lvi.SubItems.Add("-");
                    lvi.SubItems.Add("-");
                    lvi.Checked = true;
                    lvi.BackColor = ReferenceColor;
                    lvi.UseItemStyleForSubItems = false;
                    lsvData.Items.Add(lvi);

                    // add repeats
                    for (int i = 1; i <= NumberOfRepeatation; i++)
                    {
                        lvi = new ListViewItem();
                        lvi.Text = "Sample " + i.ToString();
                        lvi.SubItems.Add("-");
                        lvi.SubItems.Add("-");
                        lvi.SubItems.Add("-");
                        lvi.Checked = true;
                        lvi.BackColor = ColorTable[(i - 1) % ColorTable.Length];
                        lvi.UseItemStyleForSubItems = false;
                        lsvData.Items.Add(lvi);
                    }

                    // clear treatment curve
                    // ReDim TreatmentCurve(0 To NumberOfRepeatation - 1)

                    gbMeasurement.Enabled = true;
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
                    gbSample.Enabled = true;
                    gbScanCondition.Enabled = true;

                    lsvData.Items[0].Selected = true;
                    lsvData.CheckBoxes = true;
                    lsvData.Focus();

                    PolarChart();
                }
                catch
                {

                }
            }
        }

        private void DefineAngleOfRotation()
        {
            ListViewItem lvi;
            lvi = lsvData.Items[SelectedIndex];
            if (SelectedIndex == 0)
            {
                lvi.SubItems[1].Text = "(" + BDC.Reference.Xm.ToString("0.00") + ", " + BDC.Reference.Ym.ToString("0.0000") + ")";
            }
            else
            {
                lvi = lsvData.Items[SelectedIndex];
                lvi.SubItems[1].Text = "(" + BDC.Data[SelectedIndex - 1].Xm.ToString("0.00") + ", " + BDC.Data[SelectedIndex - 1].Ym.ToString("0.0000") + ")";
                lvi.SubItems[2].Text = BDC.Data[SelectedIndex - 1].AngleOfRotation.ToString("0.00");
            }
        }

        private void LoadSetting()
        {
            txtVoltageRange.Text = Properties.Settings.Default.VoltageRange.ToString();
            txtVoltageResolution.Text = Properties.Settings.Default.VoltageResolution.ToString();
            mnuOptionsDemomode.Checked = Properties.Settings.Default.IsDemo;
            ReferenceColor = Properties.Settings.Default.ReferenceColor;
            ColorTable[0] = Properties.Settings.Default.color1;
            ColorTable[1] = Properties.Settings.Default.color2;
            ColorTable[2] = Properties.Settings.Default.color3;
            ColorTable[3] = Properties.Settings.Default.color4;
            ColorTable[4] = Properties.Settings.Default.color5;
            ColorTable[5] = Properties.Settings.Default.color6;
            ColorTable[6] = Properties.Settings.Default.color7;
            ColorTable[7] = Properties.Settings.Default.color8;
            ColorTable[8] = Properties.Settings.Default.color9;
            ColorTable[9] = Properties.Settings.Default.color10;
            ColorTable[10] = Properties.Settings.Default.color11;
            ColorTable[11] = Properties.Settings.Default.color12;
            ColorTable[12] = Properties.Settings.Default.color13;
            ColorTable[13] = Properties.Settings.Default.color14;
            ColorTable[14] = Properties.Settings.Default.color15;
            ColorTable[15] = Properties.Settings.Default.color16;
            ColorTable[16] = Properties.Settings.Default.color17;
            ColorTable[17] = Properties.Settings.Default.color18;
            ColorTable[18] = Properties.Settings.Default.color19;
            ColorTable[19] = Properties.Settings.Default.color20;
        }

        private void SaveSetting()
        {
            Properties.Settings.Default.IsDemo = mnuOptionsDemomode.Checked;
            Properties.Settings.Default.Save();
        }

        public void ApplyColorTableToSamples()
        {
            try
            {
                lsvData.Items[0].BackColor = ReferenceColor;
                for (int i = 1; i <= lsvData.Items.Count - 1; i++)
                    lsvData.Items[i].BackColor = ColorTable[(i - 1) % ColorTable.Length];
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Summary

        private void lvSummary_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (lsvData.SelectedIndices == null)
                return;
            if (lsvData.SelectedIndices.Count <= 0)
                return;
            SelectedIndex = lsvData.SelectedIndices[0];
            try
            {
                if (SelectedIndex == 0)
                {
                    lblSample.Text = "Reference";
                }
                else
                {
                    lblSample.Text = "Sample " + SelectedIndex.ToString();
                }
                ResetDynaplot();
                PlotReferenceCurve();
                PlotTreatmentsCurve();
                PlotSelectedTRTMarker();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Textbox

        private void txtStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtStop_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtResoluton_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        #region Axis

        public void MotorAxis()
        {
            switch (comboAxis.Text)
            {
                case "X":
                    AxisX();
                    break;
                case "Y":
                    AxisY();
                    break;
            }

            void AxisX()
            {

            }

            void AxisY()
            {

            }
        }

        #endregion

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                string MSG = "A:WP" + System.Convert.ToInt32(-1 * Convert.ToDouble(txtStart.Text) / StepFactor).ToString() + "P" + System.Convert.ToInt32(-1 * Convert.ToDouble(txtStart.Text) / StepFactor).ToString();
                MMC.WriteString(MSG);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // สามารถทำได้แต่ไม่คลอบคลุม

            //MessageBox.Show("You want to save this last file?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //DialogResult a;
            //a = MessageBox.Show("You want to save this last file?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //{
            //    switch (a)
            //    {
            //        case object _ when a == System.Windows.Forms.DialogResult.Yes:
            //            {
            //                BDC.SaveFile();
            //                break;
            //            }
            //        case object _ when a == System.Windows.Forms.DialogResult.Cancel:
            //            {
            //                break;
            //            }

            //    }
            //}
            //NewMeasurement();

            // แก้ไขแล้วใช้ได้
            {
                DialogResult result = MessageBox.Show("Do you want to save file before new measurement?", "New file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    BDC.SaveFile();
                    NewMeasurement();
                }
                else if (result == DialogResult.No)
                {
                    NewMeasurement();
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            BDC.OpenFile();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectedDevices();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectDevices();
        }

        private void txtAvageNumber_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void lsvData_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                SendKeys.Send("{TAB}");
        }

        private void lvSummary_ItemChecked(System.Object sender, System.Windows.Forms.ItemCheckedEventArgs e)
        {
            ResetDynaplot();
            PlotReferenceCurve();
            PlotTreatmentsCurve();
            PlotSelectedTRTMarker();
        }

        private void txtVoltageRange_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                Properties.Settings.Default.VoltageRange = Convert.ToDouble(txtVoltageRange.Text);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
            }
        }

        private void txtVoltageResolution_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                Properties.Settings.Default.VoltageResolution = Convert.ToDouble(txtVoltageResolution.Text);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
            }
        }

        private Bitmap GetFormImage(bool include_boders)
        {
            int wid = this.Width;
            int hgt = this.Height;
            Bitmap bm = new Bitmap(wid, hgt);

            this.DrawToBitmap(bm, new Rectangle(0, 0, wid, hgt));

            if (include_boders)
                return bm;

            wid = this.ClientSize.Width;
            hgt = this.ClientSize.Height;
            Bitmap bm2 = new Bitmap(wid, hgt);

            Point pt = new Point(0, 0);
            pt = PointToScreen(pt);
            int dx = pt.X - this.Left;
            int dy = pt.Y - this.Top;

            Graphics g = Graphics.FromImage(bm2);
            g.DrawImage(bm, 0, 0, new Rectangle(dx, dy, wid, hgt), GraphicsUnit.Pixel);
            return bm2;
        }

        private void PushXY()
        {
            foreach (Series ptseries in chart1.Series)
            {
                for (int i = 0; i < 1; i++)
                {
                    //double N = ((System.Convert.ToDouble(1 * Convert.ToDouble(txtStop.Text)) - System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text))) / System.Convert.ToDouble(1 * Convert.ToDouble(txtResolution.Text))) + i;
                    //double Xf = System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text) + N * (System.Convert.ToDouble(1 * Convert.ToDouble(txtResolution.Text))));
                    //double x = (System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text))+ System.Convert.ToDouble(1 * Convert.ToDouble(txtResolution.Text)))*i;
                    double x = ((System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text)) + (tick * (System.Convert.ToDouble(1 * Convert.ToDouble(txtResolution.Text))))));
                    double y = rand.Next(5, 20);
                    System.Diagnostics.Trace.WriteLine(string.Format(">>> (X, Y) = ({0}, {1})", x, y));

                    ptseries.Points.AddXY(x, y);
                }
                //chart1.ChartAreas[0].AxisX.Maximum = ptseries.Points[ptseries.Points.Count - 1].XValue;
                //chart1.ChartAreas[0].AxisX.Minimum = System.Convert.ToDouble(1 * Convert.ToDouble(txtStart.Text));
                //chart1.ChartAreas[0].AxisX.Maximum = System.Convert.ToDouble(1 * Convert.ToDouble(txtStop.Text));
                chart1.Invalidate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //PushXY();
            tick++;
        }

        private void lsvData_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //bool clicked = false;
            //CheckBoxState state;  
            //if (!clicked)
            //{
            //    clicked = true;
            //    state = CheckBoxState.CheckedPressed;

            //    foreach (ListViewItem item in lsvData.Items)
            //    {
            //        item.Checked = true;
            //    }

            //    Invalidate();
            //}
            //else
            //{
            //    clicked = false;
            //    state = CheckBoxState.UncheckedNormal;
            //    Invalidate();

            //    foreach (ListViewItem item in lsvData.Items)
            //    {
            //        item.Checked = false;
            //    }
            //}
        }

        public void PolarChart()
        {
           try
           { 
                chart1.Series.Clear();
                Series newSeries = new Series("Reference");
                newSeries.ChartType = SeriesChartType.Polar;
                newSeries.BorderWidth = 3;
                newSeries.XValueType = ChartValueType.DateTime;
                newSeries.YValueType = ChartValueType.DateTime;
                newSeries.Color = Properties.Settings.Default.ReferenceColor;
                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sample = new Series("Sample" + i.ToString());
                    sample.ChartType = SeriesChartType.Polar;
                    sample.BorderWidth = 3;
                    sample.XValueType = ChartValueType.Auto;
                    sample.YValueType = ChartValueType.Auto;
                    chart1.Series.Add(sample);
                    sample.Color = ColorTable[(i - 1) % ColorTable.Length];
                }
                chart1.Series.Add(newSeries);
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00} deg";
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0.00 Volt";
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
           }
                catch (Exception ex)
           {

           }
        }
    }
}
