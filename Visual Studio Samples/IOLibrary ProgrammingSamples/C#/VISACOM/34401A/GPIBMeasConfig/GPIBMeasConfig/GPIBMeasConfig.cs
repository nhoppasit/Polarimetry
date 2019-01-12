
/* """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2002,2004 Agilent Technologies Inc.  All rights reserved.
'''
''' You have a royalty-free right to use, modify, reproduce and distribute
''' the Sample Application Files (and/or any modified version) in any way
''' you find useful, provided that you agree that Agilent Technologies has no
''' warranty,  obligations or liability for any Sample Application Files.
'''
''' Agilent Technologies provides programming examples for illustration only,
''' This sample program assumes that you are familiar with the programming
''' language being demonstrated and the tools used to create and debug
''' procedures. Agilent Technologies support engineers can help explain the
''' functionality of Agilent Technologies software components and associated
''' commands, but they will not modify these samples to provide added
''' functionality or construct procedures to meet your specific needs.
''' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using Ivi.Visa.Interop;



namespace GPIB_Meas_Config
{
	/// <summary>
	/// Summary description for GPIBMeasConfig.
	/// </summary>
	public class GPIB_Meas_Config : System.Windows.Forms.Form
	{
		//formattedIO interface
		private Ivi.Visa.Interop.FormattedIO488 ioDmm;

		private System.Windows.Forms.GroupBox grouppBox1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnInitIO;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnMeasure;
		private System.Windows.Forms.Button btnConfigure;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		public GPIB_Meas_Config()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GPIB_Meas_Config));
			this.grouppBox1 = new System.Windows.Forms.GroupBox();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.btnInitIO = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnConfigure = new System.Windows.Forms.Button();
			this.btnMeasure = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.lblResult = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grouppBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// grouppBox1
			// 
			this.grouppBox1.Controls.Add(this.txtAddress);
			this.grouppBox1.Controls.Add(this.lblAddress);
			this.grouppBox1.Controls.Add(this.btnInitIO);
			this.grouppBox1.Controls.Add(this.btnClose);
			this.grouppBox1.Location = new System.Drawing.Point(8, 8);
			this.grouppBox1.Name = "grouppBox1";
			this.grouppBox1.Size = new System.Drawing.Size(408, 88);
			this.grouppBox1.TabIndex = 19;
			this.grouppBox1.TabStop = false;
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(72, 24);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(208, 20);
			this.txtAddress.TabIndex = 0;
			this.txtAddress.Text = "GPIB::22";
			// 
			// lblAddress
			// 
			this.lblAddress.Location = new System.Drawing.Point(16, 28);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(48, 16);
			this.lblAddress.TabIndex = 21;
			this.lblAddress.Text = "Address";
			this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnInitIO
			// 
			this.btnInitIO.AccessibleDescription = "";
			this.btnInitIO.CausesValidation = false;
			this.btnInitIO.Location = new System.Drawing.Point(288, 24);
			this.btnInitIO.Name = "btnInitIO";
			this.btnInitIO.Size = new System.Drawing.Size(104, 23);
			this.btnInitIO.TabIndex = 1;
			this.btnInitIO.Tag = "";
			this.btnInitIO.Text = "Initialize IO";
			this.toolTip1.SetToolTip(this.btnInitIO, "Click to initialize the IO enviornment");
			this.btnInitIO.Click += new System.EventHandler(this.btnInitIO_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(288, 56);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(104, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close IO";
			this.toolTip1.SetToolTip(this.btnClose, "Click to close the IO enviornment");
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnConfigure);
			this.groupBox2.Controls.Add(this.btnMeasure);
			this.groupBox2.Controls.Add(this.txtResult);
			this.groupBox2.Controls.Add(this.lblResult);
			this.groupBox2.Location = new System.Drawing.Point(8, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 112);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			// 
			// btnConfigure
			// 
			this.btnConfigure.Location = new System.Drawing.Point(288, 64);
			this.btnConfigure.Name = "btnConfigure";
			this.btnConfigure.Size = new System.Drawing.Size(104, 23);
			this.btnConfigure.TabIndex = 4;
			this.btnConfigure.Text = "Configure";
			this.toolTip1.SetToolTip(this.btnConfigure, "Click to use CONFigure command with the dBm math operation");
			this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
			// 
			// btnMeasure
			// 
			this.btnMeasure.Location = new System.Drawing.Point(288, 32);
			this.btnMeasure.Name = "btnMeasure";
			this.btnMeasure.Size = new System.Drawing.Size(104, 23);
			this.btnMeasure.TabIndex = 3;
			this.btnMeasure.Text = " Measure";
			this.toolTip1.SetToolTip(this.btnMeasure, "Clisk to use MEASure? command to make a single AC measurement");
			this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
			// 
			// txtResult
			// 
			this.txtResult.Location = new System.Drawing.Point(72, 15);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtResult.Size = new System.Drawing.Size(208, 88);
			this.txtResult.TabIndex = 5;
			this.txtResult.Text = "";
			// 
			// lblResult
			// 
			this.lblResult.Location = new System.Drawing.Point(16, 48);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(48, 16);
			this.lblResult.TabIndex = 0;
			this.lblResult.Text = "Result";
			// 
			// GPIB_Meas_Config
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 221);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.grouppBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "GPIB_Meas_Config";
			this.Text = "Agilent 34401A GPIBMeasConfig";
			this.Load += new System.EventHandler(this.GPIB_Meas_Config_Load);
			this.grouppBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new GPIB_Meas_Config());
		}

		private void GPIB_Meas_Config_Load(object sender, System.EventArgs e)
		{
			try
			{
				//create the formatted io object
				ioDmm = new FormattedIO488Class();	
			}
			catch(SystemException ex)
			{
				MessageBox.Show("FormattedIO488Class object creation failure. " + ex.Source + "  " + ex.Message, "GPIBMeasConfig", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
			SetAccessForClosed();
		}

		private void btnInitIO_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

				//create the resource manager and open a session with the instrument specified on txtAddress
				ResourceManager grm = new ResourceManager();
                ioDmm.IO = (IMessage)grm.Open(this.txtAddress.Text, AccessMode.NO_LOCK, 2000, "");
				ioDmm.IO.Timeout = 7000;

				//Enable UI
				SetAccessForOpened();

				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch (SystemException ex)
			{
				MessageBox.Show("Open failed on " + this.txtAddress.Text + " " + ex.Source + "  " + ex.Message, "GPIBMeasConfig", MessageBoxButtons.OK, MessageBoxIcon.Error); 
				ioDmm.IO = null;
			}
			
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			//close the instrument session
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			ioDmm.IO.Close();
			SetAccessForClosed();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		/// <summary>
		/// Enable the relevant UI once a session has been successfully established with the instrument
		/// </summary>
		private void SetAccessForOpened()
		{
			this.txtAddress.Enabled = false;
			this.btnInitIO.Enabled = false;
			this.btnClose.Enabled = true;
			this.btnConfigure.Enabled = true;
			this.btnMeasure.Enabled = true;
            
			this.txtResult.Enabled = true;
			this.txtResult.Text = "";
		}

		/// <summary>
		/// Disable UI if not connected to an instrument
		/// </summary>
		private void SetAccessForClosed()
		{
			this.txtAddress.Enabled = true;
			this.btnInitIO.Enabled = true;
			this.btnClose.Enabled = false;
			this.btnConfigure.Enabled = false;
			this.btnMeasure.Enabled = false;
			
			this.txtResult.Text = "";
			this.txtResult.Enabled = false;
		}

		/*The following example uses Measure? command to make a single
		 ac current measurement. This is the easiest way to program the
		 multimeter for measurements. However, MEASure? does not offer
		 much flexibility.
		 Be sure to set the instrument address and Initialize before
		 calling this method.
		 */
		private void btnMeasure_Click(object sender, System.EventArgs e)
		{
			double dbResult;
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				
				btnConfigure.Enabled = false;
				btnMeasure.Enabled = false;
				
				//Reset the dmm
				ioDmm.WriteString("*RST",true);
				//Clear the dmm registers
				ioDmm.WriteString("*CLS",true);

				// Set meter to 1 amp ac range
				ioDmm.WriteString("Measure:Current:AC? 1A,0.001MA",true);
				dbResult = (double)ioDmm.ReadNumber(IEEEASCIIType.ASCIIType_R4,true);
			
				txtResult.Text = dbResult + " amps AC";

				btnConfigure.Enabled = true;
				btnMeasure.Enabled = true;

				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch(SystemException ex)
			{
				btnConfigure.Enabled = true;
				btnMeasure.Enabled = true;
				MessageBox.Show("Measure command failed. " + ex.Source + "  " + ex.Message, "GPIB_Meas_Config", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
			

		}

		/* The following example uses CONFigure with the dBm math operation
		 The CONFigure command gives you a little more programming flexibility
		 than the MEASure? command. This allows you to 'incrementally'
		 change the multimeter's configuration.
		 Be sure to set the instrument address and Initialize before
		 caliing this method.
		*/							
		private void btnConfigure_Click(object sender, System.EventArgs e)
		{
			
		
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

				btnConfigure.Enabled = false;
				btnMeasure.Enabled = false;

				//Reset the dmm
				ioDmm.WriteString("*RST",true);
				//Clear the dmm registers
				ioDmm.WriteString("*CLS",true);

				//Set 50 ohm reference for dBm
				ioDmm.WriteString("CALC:DBM:REF 50",true);

				/*CONFigure command sets range and resolution for AC.
				 all other AC function parameters are defaulted but can be
				 set before a READ?
				*/

				// Set dmm to 1 amp ac range
				ioDmm.WriteString("Conf:Volt:AC 1, 0.001",true);     
				
				// Select the 200 Hz (fast) ac filter
				ioDmm.WriteString(":Det:Band 200",true);              
				
				//dmm will accept 5 triggers
				ioDmm.WriteString("Trig:Coun 5",true);               
				
				//Trigger source is IMMediate
				ioDmm.WriteString("Trig:Sour IMM",true);
              
				//Select dBm function
				ioDmm.WriteString("Calc:Func DBM",true);

				//Enable math and request operation complete
				ioDmm.WriteString("Calc:Stat ON",true);
         
				//Take readings; send to output buffer
				ioDmm.WriteString("Read?",true);                     
				
																	 
				
					
				// Get readings and parse into array of doubles
				// Enter will wait until all readings are completed
				//' print to Text box
				double[] Readings = new double[5];
				string sText = "";
				txtResult.Text = "";

				Readings = (double[])ioDmm.ReadList(IEEEASCIIType.ASCIIType_R8,",");
				for(int iIndex = 0; iIndex < Readings.Length;iIndex++)
				{
					sText = sText + Readings[iIndex].ToString() + " dBm" + "\r\n";
				}
				txtResult.Text = sText;
    
				btnConfigure.Enabled = true;
				btnMeasure.Enabled = true;

				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch(SystemException ex)
			{
				btnConfigure.Enabled = true;
				btnMeasure.Enabled = true;
				MessageBox.Show("Configure command failed. " + ex.Source + "  " + ex.Message, "GPIBMeasConfig", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
		}
	}
}
