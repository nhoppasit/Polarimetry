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
        public frmMain()
        {
            InitializeComponent();

            BDC = new BaseDataControl();

            DMM = new Ivi.Visa.Interop.FormattedIO488();
            MMC = new Ivi.Visa.Interop.FormattedIO488();

        }

        #region DECRALATION
        private Ivi.Visa.Interop.FormattedIO488 ioDmm;
        public BaseDataControl BDC;
        Random rand = new Random();

        //Constants
        const double StepFactor = 0.013325; //Deg /Step 

        //Scaning & Data
        bool IsScanning = false;
        bool IsContinuing = false;
        int CurrentPointIndex = 0;
        double SpecificRotation;
        int NumberOfRepeatation;
        int SelectedIndex;
        double CurrentLightIntensity;
        double CurrentTheta = 0;
        int StepNumber;
        string MSG;

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

            // Plot curve from initial data
            PlotReferenceCurve();

            // ----------------------------------------
            // 1. Update buttons
            // ----------------------------------------
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;

            // ----------------------------------------
            // disable box
            // ----------------------------------------
            gbStartMea.Enabled = false;
            gbSample.Enabled = false;
            gbScanCondition.Enabled = false;
            gbDevices.Enabled = false;
            gbMeasurement.Enabled = true;

            // ----------------------------------------
            // 2. start Test loop of reading light intensity
            // ----------------------------------------
            CurrentPointIndex = 0;
            IsScanning = true;
            lblMainStatus.Text = "Measuring...";
            PolarChart();
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
            btnPause.Enabled = false;
            gbSample.Enabled = true;
            gbDevices.Enabled = true;
            gbMeasurement.Enabled = true;
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
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            if (btnPause.Text == "PAUSE")
            {
                btnPause.Text = "CONTINUE";
            }
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

        private void btnPause_Click(System.Object sender, System.EventArgs e)
        {
            DoPause();
        }

        private void btnStop_Click(System.Object sender, System.EventArgs e)
        {
            DoStop();
        }

        #endregion

        #region Scanning Procedure    

        Random Rnd = new Random();
        public void DoScanLightIntensity()
        {
            // --------------------------------------------
            // validate selected index of repeats
            // --------------------------------------------
            if (lsvData.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Please select item in samples list view that you want to measure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnStart.Enabled = true;
                btnStop.Enabled = false;  
                btnPause.Enabled = false;
                btnPause.Text = "PAUSE";
                btnNew.Enabled = true;
                btnOpen.Enabled = true;
                gbStartMea.Enabled = true;
                gbSample.Enabled = true;
                gbScanCondition.Enabled = true;
                return;
            }

            if (!mnuOptionsDemomode.Checked)
            {
                ConnectedDevices();
            }

            // --------------------------------------------
            // get read conditions
            // --------------------------------------------
            double ThetaA = System.Convert.ToDouble(txtStart.Text);
            double ThetaB = System.Convert.ToDouble(txtStop.Text);
            double Delta = System.Convert.ToDouble(txtResolution.Text);

            try
            {
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
                BDC.Reference.X = new double[1+NumberOfPoint];
                BDC.Reference.Y = new double[1+NumberOfPoint];

                for (int i = 0; i < BDC.Data.Length; i++)
                {
                    BDC.Data[i].X = new double[1+NumberOfPoint];
                    BDC.Data[i].Y = new double[1+NumberOfPoint];
                }


                // --------------------------------------------
                // initialize minimum finder
                // --------------------------------------------
                if (!IsContinuing)
                {
                    if (SelectedIndex == 0)
                    {
                        BDC.Reference.Ym = 99999999;
                    }
                    else if (BDC.Data != null)
                    {
                        if (SelectedIndex < BDC.Data.Length)
                        {
                            BDC.Data[SelectedIndex].Ym = 99999999;
                        }
                    }
                }

                // --------------------------------------------
                // MAIN READING LOOP (^0^)
                // --------------------------------------------
                while (IsScanning)
                {
                    Application.DoEvents();

                    // Update current THETA
                    if (ThetaA < ThetaB)
                    {
                        CurrentTheta = ThetaA + CurrentPointIndex * Delta;
                    }
                    else if (ThetaA > ThetaB)
                    {
                        CurrentTheta = ThetaA - CurrentPointIndex * Delta;
                    }
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

                        // 2. Read light intensity
                        int nAvg = (int)numRepeatNumber.Value;
                        CurrentLightIntensity = 0;
                        for (int tt = 0; tt <= nAvg - 1; tt++)
                        {
                            DMM.WriteString("READ?");
                            CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber();
                        }
                        CurrentLightIntensity = CurrentLightIntensity / nAvg;
                    }
                    else
                    {
                        // --------------------------------------------
                        // DEMO MODE
                        // --------------------------------------------
                        CurrentLightIntensity = Rnd.NextDouble() * 0.1 + Math.Cos((CurrentTheta - Rnd.NextDouble() * 50) * Math.PI / 180) + 2;
                    }

                    // Save to memory and update curve
                    if (SelectedIndex == 0) // Curve / เส้นกราฟ ของ Reference ที่แต่ละกราฟ
                    {
                        BDC.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                    }
                    else // ที่ไม่ใช่ Ref ตั้งแต่ SelectedIndex = 1 เป็นต้นไป
                    {
                        BDC.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity);
                    }
                    //<-------------PLOT HERE!  ptseries.Points.AddXY(x, y);
                    // OK GO GO GO 
                    chart4.Series[SelectedIndex].Points.AddXY(CurrentTheta, CurrentLightIntensity);
                    chart4.Invalidate();
                    chart3.Series[SelectedIndex].Points.AddXY(CurrentTheta, CurrentLightIntensity);
                    chart3.Invalidate();
                    chart2.Series[SelectedIndex].Points.AddXY(CurrentTheta, CurrentLightIntensity);
                    // ---------
                    if (chart2.Series[SelectedIndex + lsvData.Items.Count].Points.Count <= 0) // จำนวนจุดของเส้นกราฟ นี้ น้อยกว่าหรือเท่ากับ ศูนย์
                    {
                        if(SelectedIndex==0)
                            chart2.Series[SelectedIndex + lsvData.Items.Count].Points.AddXY(BDC.Reference.Xm, BDC.Reference.Ym);
                        else
                            chart2.Series[SelectedIndex + lsvData.Items.Count].Points.AddXY(BDC.Data[CurrentPointIndex].Xm, BDC.Data[CurrentPointIndex].Ym);
                    }
                    else
                    {
                        if (SelectedIndex == 0)
                        {
                            chart2.Series[SelectedIndex + lsvData.Items.Count].Points[0].XValue = BDC.Reference.Xm;
                            chart2.Series[SelectedIndex + lsvData.Items.Count].Points[0].YValues[0] = BDC.Reference.Ym;
                        }
                        else
                        {
                            chart2.Series[SelectedIndex + lsvData.Items.Count].Points[0].XValue = BDC.Data[CurrentPointIndex].Xm;
                            chart2.Series[SelectedIndex + lsvData.Items.Count].Points[0].YValues[0] = BDC.Data[CurrentPointIndex].Ym;
                        }
                    }
                    chart2.Series[SelectedIndex + lsvData.Items.Count].MarkerStyle = MarkerStyle.Square;
                    chart2.Series[SelectedIndex + lsvData.Items.Count].MarkerSize = 20;
                    chart2.Series[SelectedIndex + lsvData.Items.Count].IsValueShownAsLabel = true;
                    // ---------
                    chart2.Invalidate();
                    chart1.Series[SelectedIndex].Points.AddXY(CurrentTheta, CurrentLightIntensity);
                    //====....
                    chart1.Invalidate();
                    //
                    // เหนียวเส้น ไม่สวยเลย chart1.Series[0].Points[CurrentPointIndex]
                    //
                    DefineAngleOfRotation();
                    PlotReferenceCurve();
                    PlotTreatmentsCurve();
                    PlotSelectedTRTMarker();
                    // auto scale

                    // check stop condition!!!
                    if (ThetaA < ThetaB)
                    {
                        if (ThetaB < CurrentTheta)
                        {
                            IsScanning = false;
                        }
                    }
                    else if (ThetaA > ThetaB)
                    {
                        if (CurrentTheta < ThetaB)
                        {
                            IsScanning = false;
                        }
                    }
                    // --------------------------------------------

                    // next point
                    CurrentPointIndex += 1;
                } // end of while
                // --------------------------------------------(^0^)

                // if stop update buttons to a new start
                if (btnPause.Text != "CONTINUE" )
                {
                    if (!mnuOptionsDemomode.Checked)
                    {
                        MSG = "A:WP" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString() + "P" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString();
                        MMC.WriteString(MSG);
                        DisconnectDevices();
                    }
                    btnStart.Enabled = true;
                    btnPause.Enabled = false;
                    btnStop.Enabled = false;
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
                    gbSample.Enabled = true;
                    gbScanCondition.Enabled = true;
                    gbStartMea.Enabled = true;
                    gbDevices.Enabled = true;
                    gbMeasurement.Enabled = true;
                }
                else
                {
                    if (!mnuOptionsDemomode.Checked)
                    {
                        DisconnectDevices();
                    }
                    btnStart.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;
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
                btnPause.Enabled = false;
                btnStop.Enabled = false;
                btnNew.Enabled = true;
                btnOpen.Enabled = true;
                gbMeasurement.Enabled = true;
                gbStartMea.Enabled = true;
                gbSample.Enabled = true;
                gbDevices.Enabled = true;
                gbScanCondition.Enabled = true;

                // ----------------------------------------
                // 3. Return Motor
                // ----------------------------------------
                //MSG = "A:WP" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString() + "P" + System.Convert.ToInt32(-1 * ThetaA / StepFactor).ToString();
                //MMC.WriteString(MSG);
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
            //*****************************************************************************
            // ***ต้องเช็คก่อนว่ามีไฟล์หรือไม่ ถ้ามีให้ลบ
            if (lsvData.Items.Count > 0)
            {
                DialogResult result = MessageBox.Show("Data will be deleted. Do you want to save file?", "Save file", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {

                }
                else if (result == DialogResult.Cancel)
                {

                }
            }

            //เขียนต่อเลยนะครับ
            LogFile.Log theSave = new LogFile.Log(@"C:\Users\Pee\Desktop\test save", @"test save");  

            //Header
            LogFile.PolarimeterHeader header = new LogFile.PolarimeterHeader()
            {
                DMMPort = txtDMMAddress.Text,
                MMCPort = txtMMCAddress.Text,
                SampleName = "Hello1",
                SampleNumber = 3,
            };

            //Append to file
            theSave.AppendText(header.ToString());

            //Listview loop
            foreach (ListViewItem lvi in lsvData.Items)
            {
                theSave.AppendText($"{lvi.Index + 1},{lvi.Text},{lvi.SubItems[1].Text},{lvi.SubItems[2].Text}");
            }

            //Curve data loop
            int NumberOfPoints = BDC.Reference.X.Length;
            theSave.AppendText(NumberOfPoints.ToString());
            //            
            for(int idx = 0; idx < NumberOfPoints; idx++)//.. Points loop
            {
                //theSave.AppendText($"{lvi.Index + 1},{lvi.Text},{lvi.SubItems[1].Text},{lvi.SubItems[2].Text}");
                string left = $"{idx + 1},{BDC.Reference.X[idx]},{BDC.Reference.Y[idx]}";
                string right = "";
                for (int di = 0; di < BDC.Data.Length; di++) //..Data loop
                {
                    right += $",{BDC.Data[di].Y[idx]}";
                }//end data loop
                //
                theSave.AppendText($"{left}{right}");
            }// end points loop
            //*****************************************************************************

            //BDC.SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)  
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
            if (result == DialogResult.OK)
            {
                ResetDynaplot();
                PlotReferenceCurve();
                PlotTreatmentsCurve();
                PlotSelectedTRTMarker();
            }
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
            if (BDC == null)
            {
                return false;
            }
            if (BDC.Data.ToString() == null)
            {
                return false;
            }
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
                    
                }
            }
            return true;
        }

        private void PlotSelectedTRTMarker()
        {
            try
            {
                if (lsvData.Items[SelectedIndex].Checked)
                {
                    lblNullPoint.Text = BDC.Data[SelectedIndex].Xm.ToString("0.0000") + " deg";
                }
            }
            catch (Exception ex)
            {
                
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
                    MEA();
                }
                else if (result == DialogResult.Cancel)
                {

                }
            }
            else
            {
                MEA();
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
                lvi.SubItems[1].Text = "(" + BDC.Data[SelectedIndex].Xm.ToString("0.00") + ", " + BDC.Data[SelectedIndex].Ym.ToString("0.0000") + ")";
                lvi.SubItems[2].Text = BDC.Data[SelectedIndex].AngleOfRotation.ToString("0.00");
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
                {
                    lsvData.Items[i].BackColor = ColorTable[(i - 1) % ColorTable.Length];
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Summary

        private void lsvData_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            if (lsvData.SelectedIndices == null)
            {
                return;
            }
            if (lsvData.SelectedIndices.Count <= 0)
            {
                return;
            }
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

        private void PolarChart()
        {
            try
            {
                #region CHART 1

                //chart1
                Series newSeries = new Series("Reference");
                newSeries.ChartType = SeriesChartType.Polar;
                newSeries.BorderWidth = 4;
                newSeries.Color = Properties.Settings.Default.ReferenceColor;
                chart1.Series.Add(newSeries);

                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sample = new Series("Sample" + i.ToString());
                    sample.ChartType = SeriesChartType.Polar;
                    sample.BorderWidth = 4;
                    sample.XValueType = ChartValueType.Auto;
                    sample.YValueType = ChartValueType.Auto;
                    chart1.Series.Add(sample);
                    sample.Color = ColorTable[(i - 1) % ColorTable.Length];
                }

                Series newSeriesRefMarker = new Series("Reference_Marker");
                newSeriesRefMarker.ChartType = SeriesChartType.Polar;
                newSeriesRefMarker.Color = Properties.Settings.Default.ReferenceColor;
                chart1.Series.Add(newSeriesRefMarker);
                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sampleMarker = new Series("sample_Marker" + i.ToString());
                    sampleMarker.ChartType = SeriesChartType.Polar;
                    sampleMarker.BorderWidth = 4;
                    sampleMarker.XValueType = ChartValueType.Auto;
                    sampleMarker.YValueType = ChartValueType.Auto;
                    chart1.Series.Add(sampleMarker);
                    sampleMarker.Color = ColorTable[(i - 1) % ColorTable.Length];
                }


                //chart2.ChartAreas[0].Axes[0].Minimum = Convert.ToDouble(txtStart.Text);

                //------X major 
                chart1.ChartAreas[0].Axes[0].LabelStyle.Format = "{0:0.00} deg";
                chart1.ChartAreas[0].Axes[0].MajorGrid.LineColor = Color.DarkGray;
                chart1.ChartAreas[0].Axes[0].MajorGrid.Enabled = true;
                chart1.ChartAreas[0].Axes[0].MajorGrid.Interval = 10;
                chart1.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true;
                //chart1.ChartAreas[0].Axes[0].MajorTickMark.LineColor = Color.Black;
                chart1.ChartAreas[0].Axes[0].MajorTickMark.Interval = 10;
                chart1.ChartAreas[0].Axes[0].MajorTickMark.LineWidth = 2;
                chart1.ChartAreas[0].Axes[0].MajorTickMark.Size = 1;
                chart1.ChartAreas[0].Axes[0].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------X minor
                chart1.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].Axes[0].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart1.ChartAreas[0].Axes[0].MinorGrid.Enabled = true;
                chart1.ChartAreas[0].Axes[0].MinorGrid.Interval = 2;
                chart1.ChartAreas[0].Axes[0].MinorTickMark.Enabled = true;
                chart1.ChartAreas[0].Axes[0].MinorTickMark.LineColor = Color.Gray;
                chart1.ChartAreas[0].Axes[0].MinorTickMark.Interval = 2;
                chart1.ChartAreas[0].Axes[0].MinorTickMark.LineWidth = 1;
                chart1.ChartAreas[0].Axes[0].MinorTickMark.Size = 1;
                chart1.ChartAreas[0].Axes[0].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------Y major
                chart1.ChartAreas[0].Axes[1].LabelStyle.Format = "0.00 Volt";
                chart1.ChartAreas[0].Axes[1].MajorGrid.LineColor = Color.DarkGray;
                chart1.ChartAreas[0].Axes[1].MajorGrid.Enabled = true;
                chart1.ChartAreas[0].Axes[1].MajorGrid.Interval = 0.5;
                chart1.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true;
                //chart1.ChartAreas[0].Axes[1].MajorTickMark.LineColor = Color.Black;
                chart1.ChartAreas[0].Axes[1].MajorTickMark.Interval = 0.5;
                chart1.ChartAreas[0].Axes[1].MajorTickMark.LineWidth = 2;
                chart1.ChartAreas[0].Axes[1].MajorTickMark.Size = 1;
                chart1.ChartAreas[0].Axes[1].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //-------Y minor
                chart1.ChartAreas[0].Axes[1].MinorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas[0].Axes[1].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart1.ChartAreas[0].Axes[1].MinorGrid.Enabled = true;
                chart1.ChartAreas[0].Axes[1].MinorGrid.Interval = 0.25;
                chart1.ChartAreas[0].Axes[1].MinorTickMark.Enabled = true;
                chart1.ChartAreas[0].Axes[1].MinorTickMark.LineColor = Color.Gray;
                chart1.ChartAreas[0].Axes[1].MinorTickMark.Interval = 0.1;
                chart1.ChartAreas[0].Axes[1].MinorTickMark.LineWidth = 1;
                chart1.ChartAreas[0].Axes[1].MinorTickMark.Size = 1;
                chart1.ChartAreas[0].Axes[1].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;

                #endregion

                #region CHART 2

                //chart2
                Series newSeries2 = new Series("Reference");
                newSeries2.ChartType = SeriesChartType.Line;
                newSeries2.BorderWidth = 4;
                newSeries2.Color = Properties.Settings.Default.ReferenceColor;
                chart2.Series.Add(newSeries2);
                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sample = new Series("Sample" + i.ToString());
                    sample.ChartType = SeriesChartType.Line;
                    sample.BorderWidth = 4;
                    sample.XValueType = ChartValueType.Auto;
                    sample.YValueType = ChartValueType.Auto;
                    chart2.Series.Add(sample);
                    sample.Color = ColorTable[(i - 1) % ColorTable.Length];
                }

                Series newSeries2Marker = new Series("Reference_Marker");
                newSeries2Marker.ChartType = SeriesChartType.Line;
                newSeries2Marker.Color = Properties.Settings.Default.ReferenceColor;
                chart2.Series.Add(newSeries2Marker);

                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sampleMarker = new Series("sample_Marker" + i.ToString());
                    sampleMarker.ChartType = SeriesChartType.Line;
                    sampleMarker.XValueType = ChartValueType.Auto;
                    sampleMarker.YValueType = ChartValueType.Auto;
                    chart2.Series.Add(sampleMarker);
                    sampleMarker.Color = ColorTable[(i - 1) % ColorTable.Length];
                }


                chart2.ChartAreas[0].Axes[0].Minimum = Convert.ToDouble(txtStart.Text);

                //------X major 
                chart2.ChartAreas[0].Axes[0].LabelStyle.Format = "{0:0.00} deg";
                chart2.ChartAreas[0].Axes[0].MajorGrid.LineColor = Color.DarkGray;
                chart2.ChartAreas[0].Axes[0].MajorGrid.Enabled = true;
                chart2.ChartAreas[0].Axes[0].MajorGrid.Interval = 10;
                chart2.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true;
                //chart2.ChartAreas[0].Axes[0].MajorTickMark.LineColor = Color.Black;
                chart2.ChartAreas[0].Axes[0].MajorTickMark.Interval = 10;
                chart2.ChartAreas[0].Axes[0].MajorTickMark.LineWidth = 2;
                chart2.ChartAreas[0].Axes[0].MajorTickMark.Size = 1;
                chart2.ChartAreas[0].Axes[0].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------X minor
                chart2.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.LightGray;
                chart2.ChartAreas[0].Axes[0].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart2.ChartAreas[0].Axes[0].MinorGrid.Enabled = true;
                chart2.ChartAreas[0].Axes[0].MinorGrid.Interval = 2;
                chart2.ChartAreas[0].Axes[0].MinorTickMark.Enabled = true;
                chart2.ChartAreas[0].Axes[0].MinorTickMark.LineColor = Color.Gray;
                chart2.ChartAreas[0].Axes[0].MinorTickMark.Interval = 2;
                chart2.ChartAreas[0].Axes[0].MinorTickMark.LineWidth = 1;
                chart2.ChartAreas[0].Axes[0].MinorTickMark.Size = 1;
                chart2.ChartAreas[0].Axes[0].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------Y major
                chart2.ChartAreas[0].Axes[1].LabelStyle.Format = "0.00 Volt";
                chart2.ChartAreas[0].Axes[1].MajorGrid.LineColor = Color.DarkGray;
                chart2.ChartAreas[0].Axes[1].MajorGrid.Enabled = true;
                chart2.ChartAreas[0].Axes[1].MajorGrid.Interval = 0.5;
                chart2.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true;
                //chart2.ChartAreas[0].Axes[1].MajorTickMark.LineColor = Color.Black;
                chart2.ChartAreas[0].Axes[1].MajorTickMark.Interval = 0.5;
                chart2.ChartAreas[0].Axes[1].MajorTickMark.LineWidth = 2;
                chart2.ChartAreas[0].Axes[1].MajorTickMark.Size = 1;
                chart2.ChartAreas[0].Axes[1].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------Y minor
                chart2.ChartAreas[0].Axes[1].MinorGrid.LineColor = Color.LightGray;
                chart2.ChartAreas[0].Axes[1].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart2.ChartAreas[0].Axes[1].MinorGrid.Enabled = true;
                chart2.ChartAreas[0].Axes[1].MinorGrid.Interval = 0.25;
                chart2.ChartAreas[0].Axes[1].MinorTickMark.Enabled = true;
                chart2.ChartAreas[0].Axes[1].MinorTickMark.LineColor = Color.Gray;
                chart2.ChartAreas[0].Axes[1].MinorTickMark.Interval = 0.1;
                chart2.ChartAreas[0].Axes[1].MinorTickMark.LineWidth = 1;
                chart2.ChartAreas[0].Axes[1].MinorTickMark.Size = 1;
                chart2.ChartAreas[0].Axes[1].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;

                #endregion

                //chart3
                Series newSeries3 = new Series("Reference");
                newSeries3.ChartType = SeriesChartType.Line;
                newSeries3.BorderWidth = 4;
                newSeries3.Color = Properties.Settings.Default.ReferenceColor;
                chart3.Series.Add(newSeries3);
                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sample = new Series("Sample" + i.ToString());
                    sample.ChartType = SeriesChartType.Line;
                    sample.BorderWidth = 4;
                    sample.XValueType = ChartValueType.Auto;
                    sample.YValueType = ChartValueType.Auto;
                    chart3.Series.Add(sample);
                    sample.Color = ColorTable[(i - 1) % ColorTable.Length];
                }
                chart3.ChartAreas[0].Axes[0].Minimum = Convert.ToDouble(txtStart.Text);

                //------X major 
                chart3.ChartAreas[0].Axes[0].LabelStyle.Format = "{0:0.00} deg";
                chart3.ChartAreas[0].Axes[0].MajorGrid.LineColor = Color.DarkGray;
                chart3.ChartAreas[0].Axes[0].MajorGrid.Enabled = true;
                chart3.ChartAreas[0].Axes[0].MajorGrid.Interval = 10;
                chart3.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true;
                //chart3.ChartAreas[0].Axes[0].MajorTickMark.LineColor = Color.Black;
                chart3.ChartAreas[0].Axes[0].MajorTickMark.Interval = 10;
                chart3.ChartAreas[0].Axes[0].MajorTickMark.LineWidth = 2;
                chart3.ChartAreas[0].Axes[0].MajorTickMark.Size = 1;
                chart3.ChartAreas[0].Axes[0].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------X minor
                chart3.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.LightGray;
                chart3.ChartAreas[0].Axes[0].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart3.ChartAreas[0].Axes[0].MinorGrid.Enabled = true;
                chart3.ChartAreas[0].Axes[0].MinorGrid.Interval = 2;
                chart3.ChartAreas[0].Axes[0].MinorTickMark.Enabled = true;
                chart3.ChartAreas[0].Axes[0].MinorTickMark.LineColor = Color.Gray;
                chart3.ChartAreas[0].Axes[0].MinorTickMark.Interval = 2;
                chart3.ChartAreas[0].Axes[0].MinorTickMark.LineWidth = 1;
                chart3.ChartAreas[0].Axes[0].MinorTickMark.Size = 1;
                chart3.ChartAreas[0].Axes[0].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------Y major
                chart3.ChartAreas[0].Axes[1].MajorGrid.LineColor = Color.DarkGray;
                chart3.ChartAreas[0].Axes[1].LabelStyle.Format = "0.00 Volt";
                chart3.ChartAreas[0].Axes[1].MajorGrid.Enabled = true;
                chart3.ChartAreas[0].Axes[1].MajorGrid.Interval = 0.5;
                chart3.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true;
                //chart3.ChartAreas[0].Axes[1].MajorTickMark.LineColor = Color.Black;
                chart3.ChartAreas[0].Axes[1].MajorTickMark.Interval = 0.5;
                chart3.ChartAreas[0].Axes[1].MajorTickMark.LineWidth = 2;
                chart3.ChartAreas[0].Axes[1].MajorTickMark.Size = 1;
                chart3.ChartAreas[0].Axes[1].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------Y minor
                chart3.ChartAreas[0].Axes[1].MinorGrid.LineColor = Color.LightGray;
                chart3.ChartAreas[0].Axes[1].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart3.ChartAreas[0].Axes[1].MinorGrid.Enabled = true;
                chart3.ChartAreas[0].Axes[1].MinorGrid.Interval = 0.1;
                chart3.ChartAreas[0].Axes[1].MinorTickMark.Enabled = true;
                chart3.ChartAreas[0].Axes[1].MinorTickMark.LineColor = Color.Gray;
                chart3.ChartAreas[0].Axes[1].MinorTickMark.Interval = 0.1;
                chart3.ChartAreas[0].Axes[1].MinorTickMark.LineWidth = 1;
                chart3.ChartAreas[0].Axes[1].MinorTickMark.Size = 1;
                chart3.ChartAreas[0].Axes[1].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;

                //chart4
                Series newSeries4 = new Series("Reference");
                newSeries4.ChartType = SeriesChartType.Polar;
                newSeries4.BorderWidth = 4;
                newSeries4.Color = Properties.Settings.Default.ReferenceColor;
                chart4.Series.Add(newSeries4);
                for (int i = 1; i <= NumberOfRepeatation; i++)
                {
                    Series sample = new Series("Sample" + i.ToString());
                    sample.ChartType = SeriesChartType.Polar;
                    sample.BorderWidth = 4;
                    sample.XValueType = ChartValueType.Auto;
                    sample.YValueType = ChartValueType.Auto;
                    chart4.Series.Add(sample);
                    sample.Color = ColorTable[(i - 1) % ColorTable.Length];
                }
                //chart2.ChartAreas[0].Axes[0].Minimum = Convert.ToDouble(txtStart.Text);

                //------X major 
                chart4.ChartAreas[0].Axes[0].LabelStyle.Format = "{0:0.00} deg";
                chart4.ChartAreas[0].Axes[0].MajorGrid.LineColor = Color.DarkGray;
                chart4.ChartAreas[0].Axes[0].MajorGrid.Enabled = true;
                chart4.ChartAreas[0].Axes[0].MajorGrid.Interval = 10;
                chart4.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true;
                //chart4.ChartAreas[0].Axes[0].MajorTickMark.LineColor = Color.Black;
                chart4.ChartAreas[0].Axes[0].MajorTickMark.Interval = 10;
                chart4.ChartAreas[0].Axes[0].MajorTickMark.LineWidth = 2;
                chart4.ChartAreas[0].Axes[0].MajorTickMark.Size = 1;
                chart4.ChartAreas[0].Axes[0].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------X minor
                chart4.ChartAreas[0].Axes[0].MinorGrid.LineColor = Color.LightGray;
                chart4.ChartAreas[0].Axes[0].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart4.ChartAreas[0].Axes[0].MinorGrid.Enabled = true;
                chart4.ChartAreas[0].Axes[0].MinorGrid.Interval = 2;
                chart4.ChartAreas[0].Axes[0].MinorTickMark.Enabled = true;
                chart4.ChartAreas[0].Axes[0].MinorTickMark.LineColor = Color.Gray;
                chart4.ChartAreas[0].Axes[0].MinorTickMark.Interval = 2;
                chart4.ChartAreas[0].Axes[0].MinorTickMark.LineWidth = 1;
                chart4.ChartAreas[0].Axes[0].MinorTickMark.Size = 1;
                chart4.ChartAreas[0].Axes[0].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //------Y major
                chart4.ChartAreas[0].Axes[1].LabelStyle.Format = "0.00 Volt";
                chart4.ChartAreas[0].Axes[1].MajorGrid.LineColor = Color.DarkGray;
                chart4.ChartAreas[0].Axes[1].MajorGrid.Enabled = true;
                chart4.ChartAreas[0].Axes[1].MajorGrid.Interval = 0.5;
                chart4.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true;
                //chart4.ChartAreas[0].Axes[1].MajorTickMark.LineColor = Color.Black;
                chart4.ChartAreas[0].Axes[1].MajorTickMark.Interval = 0.5;
                chart4.ChartAreas[0].Axes[1].MajorTickMark.LineWidth = 2;
                chart4.ChartAreas[0].Axes[1].MajorTickMark.Size = 1;
                chart4.ChartAreas[0].Axes[1].MajorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;
                //-------Y minor
                chart4.ChartAreas[0].Axes[1].MinorGrid.LineColor = Color.LightGray;
                chart4.ChartAreas[0].Axes[1].MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart4.ChartAreas[0].Axes[1].MinorGrid.Enabled = true;
                chart4.ChartAreas[0].Axes[1].MinorGrid.Interval = 0.1;
                chart4.ChartAreas[0].Axes[1].MinorTickMark.Enabled = true;
                chart4.ChartAreas[0].Axes[1].MinorTickMark.LineColor = Color.Gray;
                chart4.ChartAreas[0].Axes[1].MinorTickMark.Interval = 0.1;
                chart4.ChartAreas[0].Axes[1].MinorTickMark.LineWidth = 1;
                chart4.ChartAreas[0].Axes[1].MinorTickMark.Size = 1;
                chart4.ChartAreas[0].Axes[1].MinorTickMark.TickMarkStyle = TickMarkStyle.InsideArea;


                //-------------- Marker ----------------//
                //Series Markerseries = new Series("ReferenceMarker");
                //Markerseries.ChartType = SeriesChartType.Polar;
                //Markerseries.BorderWidth = 4;
                //Markerseries.Color = Properties.Settings.Default.ReferenceColor;
                //chart1.Series.Add(Markerseries);
                //double[] X = { 0 };
                ////if (X < BDC.Reference.Xm)
                //{

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} {ex.StackTrace}");
            }
        }

        private void MEA()
        {
            // load dialog
            frmNewMeasurement f = new frmNewMeasurement();
            f.StartPosition = FormStartPosition.CenterScreen;
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                // clear chart
                chart4.Series.Clear();
                chart3.Series.Clear();
                chart2.Series.Clear();
                chart1.Series.Clear();
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
                        BDC.Data = new BaseDataControl.strucCurveData[NumberOfRepeatation+1];

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

                        gbMeasurement.Enabled = true;
                        btnNew.Enabled = true;
                        btnOpen.Enabled = true;
                        gbSample.Enabled = true;
                        gbScanCondition.Enabled = true;

                        lsvData.Items[0].Selected = true;
                        lsvData.CheckBoxes = true;
                        lsvData.GridLines = true;
                        lsvData.FullRowSelect = true;
                        lsvData.AllowColumnReorder = true;
                        lsvData.Focus();

                        // chart
                        PolarChart();
                    }
                    catch
                    {

                    }
                }
            }
            else
            {

            }
        }

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
            if (lsvData.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to save file before new measurement?", "New file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                {
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
            else
            {
                NewMeasurement();
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
            {
                SendKeys.Send("{TAB}");
            }
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

        private void lsvData_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ResetDynaplot();
            PlotReferenceCurve();
            PlotTreatmentsCurve();
            PlotSelectedTRTMarker();
        }

        //bool LVCS = false;
        //    if (LVCS==false)
        //    {
        //        chart1.Series[SelectedIndex].Enabled = false;
        //        chart2.Series[SelectedIndex].Enabled = false;
        //        chart3.Series[SelectedIndex].Enabled = false;
        //        chart4.Series[SelectedIndex].Enabled = false;
        //        LVCS = true;
        //    }
        //    else
        //    {
        //        chart1.Series[SelectedIndex].Enabled = true;
        //        chart2.Series[SelectedIndex].Enabled = true;
        //        chart3.Series[SelectedIndex].Enabled = true;
        //        chart4.Series[SelectedIndex].Enabled = true;
        //        LVCS = false;
        //    }
    }
}
