/*''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2002-2004 Agilent Technologies Inc.  All rights reserved.
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
''' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'*************************************************************
' The following example shows how you can use the multimeter's status
' registers to determine when a command sequence is completed. For
' more information see "The SCPI Status Model" in the Agilent 34401A
' User Guide
'
' NOTE: Polling will work with a GPIB interface. It does not
'       work for RS232
'
' Sequence of Operation;
'   1. The meter is cleared and set to give an SRQ when its
'      operation is complete
'   2. The meter is set for dc, and multiple readings. This will
'      take about 3 seconds for 10 readings
'   3. We start the reading with INIT. This will put the
'      data into memory.  When the meter is finished, it
'      will set SRQ.
'   4. Enable the timer that polls for SRQ every second.
'   5. When SRQ is detected, then get the reading from the
'      meter with the routine ReadData.
' Monitor the Immediate window to see every time a polling takes place.
'**************************************************************/


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ivi.Visa.Interop;

namespace StatRegSRQ
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StatRegSRQ : System.Windows.Forms.Form
	{
		// Formatted class IO object
		private FormattedIO488 ioDmm;
		private System.Windows.Forms.GroupBox grouppBox1;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.Button btnInitIO;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnStartReading;
		private System.Windows.Forms.TextBox txtData;
		private System.Windows.Forms.Label lblData;

		private int m_iNumOfReadings;
		private System.Windows.Forms.Timer tmrPollForSRQ;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		public StatRegSRQ()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatRegSRQ));
			this.grouppBox1 = new System.Windows.Forms.GroupBox();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.btnInitIO = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnStartReading = new System.Windows.Forms.Button();
			this.txtData = new System.Windows.Forms.TextBox();
			this.lblData = new System.Windows.Forms.Label();
			this.tmrPollForSRQ = new System.Windows.Forms.Timer(this.components);
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
			this.grouppBox1.Location = new System.Drawing.Point(8, 0);
			this.grouppBox1.Name = "grouppBox1";
			this.grouppBox1.Size = new System.Drawing.Size(408, 88);
			this.grouppBox1.TabIndex = 23;
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
			this.groupBox2.Controls.Add(this.btnStartReading);
			this.groupBox2.Controls.Add(this.txtData);
			this.groupBox2.Controls.Add(this.lblData);
			this.groupBox2.Location = new System.Drawing.Point(8, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 112);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			
			// 
			// btnStartReading
			// 
			this.btnStartReading.Location = new System.Drawing.Point(288, 48);
			this.btnStartReading.Name = "btnStartReading";
			this.btnStartReading.Size = new System.Drawing.Size(104, 23);
			this.btnStartReading.TabIndex = 3;
			this.btnStartReading.Text = "Start Readings";
			this.toolTip1.SetToolTip(this.btnStartReading, "Click to get the readings from the instrument");
			this.btnStartReading.Click += new System.EventHandler(this.btnStartReading_Click);
			// 
			// txtData
			// 
			this.txtData.Location = new System.Drawing.Point(72, 15);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtData.Size = new System.Drawing.Size(208, 88);
			this.txtData.TabIndex = 5;
			this.txtData.Text = "";
			
			// 
			// lblData
			// 
			this.lblData.Location = new System.Drawing.Point(16, 48);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(48, 16);
			this.lblData.TabIndex = 0;
			this.lblData.Text = "Data";
			
			// 
			// tmrPollForSRQ
			// 
			this.tmrPollForSRQ.Interval = 1000;
			this.tmrPollForSRQ.Tick += new System.EventHandler(this.tmrPollForSRQ_Tick);
			// 
			// StatRegSRQ
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 205);
			this.Controls.Add(this.grouppBox1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "StatRegSRQ";
			this.Text = "Agilent 34401A StatRegSRQ";
			this.Load += new System.EventHandler(this.StatRegSRQ_Load);
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
			Application.Run(new StatRegSRQ());
		}

		private void StatRegSRQ_Load(object sender, System.EventArgs e)
		{
			try
			{
				//create the formatted io object
				ioDmm = new FormattedIO488Class();	
			}
			catch(SystemException ex)
			{
				MessageBox.Show("FormattedIO488Class object creation failure. " + ex.Source + "  " + ex.Message, "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
			SetAccessForClosed();
		
		}

		/// <summary>
		/// Enable the relevant UI once a session has been successfully established with the instrument
		/// </summary>
		private void SetAccessForOpened()
		{
			this.txtAddress.Enabled = false;
			this.btnInitIO.Enabled = false;
			this.btnClose.Enabled = true;
			this.btnStartReading.Enabled = true;
            
			this.txtData.Enabled = true;
			this.txtData.Text = "";
		
		}

		/// <summary>
		/// Disable UI if not connected to an instrument
		/// </summary>
		private void SetAccessForClosed()
		{
			this.txtAddress.Enabled = true;
			this.btnInitIO.Enabled = true;
			this.btnClose.Enabled = false;
			this.btnStartReading.Enabled = false;
            
			this.txtData.Text = "";
			this.txtData.Enabled = false;
		}

		private void btnInitIO_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				
				//create the resource manager and open a session with the instrument specified on txtAddress
				ResourceManager grm = new ResourceManager();
				ioDmm.IO = (IMessage)grm.Open(this.txtAddress.Text, AccessMode.NO_LOCK, 2000, "");

										
				//Enable UI
				SetAccessForOpened();
		

				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			catch (SystemException ex)
			{
				MessageBox.Show("Open failed on " + this.txtAddress.Text + " " + ex.Source + "  " + ex.Message, "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
				ioDmm.IO = null;
			}
			txtData.Text = "";
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			//close the session
			System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
			ioDmm.IO.Close();
			SetAccessForClosed();
			System.Windows.Forms.Cursor.Current = Cursors.Default;
		}

		private void btnStartReading_Click(object sender, System.EventArgs e)
		{
			//disable the button
			btnStartReading.Enabled = false;

			StartReading();
		}

		/// <summary>
		/// Configure the instrument to start the reading
		/// </summary>
		private void StartReading()
		{
			try
			{
				string strTemp;
				
				// Clear out text box for the data so we can see when new data arrives
				txtData.Text = "";
    
    
				// Setup dmm to return an event when readings are complete 
				ioDmm.WriteString("*RST",true);          // Reset dmm
				ioDmm.WriteString("*CLS",true);          // Clear dmm status registers
				ioDmm.WriteString("*ESE 1",true);        // Enable 'operation complete bit to
				// set 'standard event' bit in status byte
				ioDmm.WriteString("*SRE 32",true);       // Enable 'standard event' bit in status
				// byte to pull the IEEE-488 SRQ line
				ioDmm.WriteString("*OPC?",true);         // Assure syncronization
				strTemp = ioDmm.ReadString();
    
    
				
				// Configure the meter to take readings
				// and initiate the readings (source is set to immediate by default)
				m_iNumOfReadings = 10;
				
					
				ioDmm.WriteString("Configure:Voltage:dc 10",true);   // set dmm to 10 volt dc range
				ioDmm.WriteString("Voltage:DC:NPLC 10",true);        // set integration time to 10 Power line cycles (PLC)"
				ioDmm.WriteString("Trigger:count " + m_iNumOfReadings.ToString(),true);	// set dmm to accept multiple triggers
				ioDmm.WriteString("Init",true);		//Place dmm in 'wait-for-trigger' state
				ioDmm.WriteString("*OPC",true);		//Set 'operation complete' bit in standard
				//event registers when measurement is complete
    		
				
				//give message that meter is initialized
				txtData.Text = "Meter configured and \r\n" + "Initialized " + DateTime.Now;

				//enable the timer
				tmrPollForSRQ.Enabled = true;
			}
			catch(SystemException ex)
			{
				MessageBox.Show("Error configuring the meter. " + this.txtAddress.Text + " " + ex.Source + "  " + ex.Message, "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
		}

		/// <summary>
		/// Read the result from the instrument
		/// </summary>
		private void ReadData()
		{
			// Once the Event is detected, this routine will get the data from the meter
    		double[] readings;
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				
				if(m_iNumOfReadings > 0)
				{
					readings = new double[m_iNumOfReadings];

					ioDmm.WriteString("Fetch?",true);	// Query for the data in memory
					readings = (double[])ioDmm.ReadList(IEEEASCIIType.ASCIIType_R8,",");	//get the data and parse into the array
    
					//Insert data into text box
					string sTemp = "";
					sTemp = txtData.Text;
					for(int iIndex = 0; iIndex < readings.Length; iIndex++)
					{
						sTemp = sTemp + readings[iIndex].ToString() + " Vdc \r\n";
					}
					txtData.Text = "";
					txtData.Text = sTemp;
				}
				else
				{
					txtData.Text = "";
				}

				System.Windows.Forms.Cursor.Current = Cursors.Default;

				//enable the button
				btnStartReading.Enabled = true;
			}
			catch(SystemException ex)
			{
				MessageBox.Show("Error reading data from the meter. " + this.txtAddress.Text + " " + ex.Source + "  " + ex.Message, "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
		}

		private void tmrPollForSRQ_Tick(object sender, System.EventArgs e)
		{
			// This timer when enabled will poll the GPIB and return the status byte to indicate if SRQ is set
    
			try
			{
				// read the status byte
				int statusValue = 0;
				statusValue = ioDmm.IO.ReadSTB();

				// Test for the SRQ bit
				if((statusValue & 64) == 64)		// SRQ from Operation complete
				{
					 // Turn off the timer and stop polling.
					tmrPollForSRQ.Enabled = false;
					
					// Get the Data, the meter is ready
					txtData.Text= "Getting Data \r\n";
					ReadData();
				}

			}
			catch(SystemException ex)
			{
				MessageBox.Show("No SRQ yet, Poll error = " + this.txtAddress.Text + " " + ex.Source + "  " + ex.Message, "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
    	}
	}
}
