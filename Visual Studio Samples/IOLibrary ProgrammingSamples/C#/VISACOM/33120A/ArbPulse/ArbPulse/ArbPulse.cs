/*' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2002 Agilent Technologies Inc. All rights
'    reserved.
'
' You have a royalty-free right to use, modify, reproduce and distribute
' the Sample Application Files (and/or any modified version) in any way
' you find useful, provided that you agree that Agilent has no
' warranty,  obligations or liability for any Sample Application Files.
'
' Agilent Technologies provides programming examples for illustration only,
' This sample program assumes that you are familiar with the programming
' language being demonstrated and the tools used to create and debug
' procedures. Agilent support engineers can help explain the
' functionality of Agilent software components and associated
' commands, but they will not modify these samples to provide added
' functionality or construct procedures to meet your specific needs.
' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

' This program uses the arbitrary waveform function to
' download and output a square wave pulse with calculated
' rise and fall times.  The waveform consists of 4000
' points downloaded to the function generator as ASCII data.
'
' See the ArbDampedSine example for downloading as binary data*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ivi.Visa.Interop;

namespace ArbPulse
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CArbPulse : System.Windows.Forms.Form
	{
		//Variable to hold the instance of Formatted IO; used in communication
		
		private Ivi.Visa.Interop.ResourceManager  rm;
		private  Ivi.Visa.Interop.FormattedIO488  ioArbFG;
		private  Ivi.Visa.Interop.IMessage  msg;

		private System.Windows.Forms.ToolTip toolTipCtrl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox m_gpibAddrTextBox;
		private System.Windows.Forms.Button m_setioButton;
		private System.Windows.Forms.Label m_idLabel;
		internal System.Windows.Forms.GroupBox mygroupBox;
		internal System.Windows.Forms.Button m_downloadButton;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.TextBox m_falltimeTextBox;
		internal System.Windows.Forms.TextBox m_pulsetopTextBox;
		internal System.Windows.Forms.TextBox m_risetimeTextBox;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		private System.Windows.Forms.Button m_closeioButton;
		private System.ComponentModel.IContainer components;

		public CArbPulse()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CArbPulse));
			this.toolTipCtrl = new System.Windows.Forms.ToolTip(this.components);
			this.m_gpibAddrTextBox = new System.Windows.Forms.TextBox();
			this.m_idLabel = new System.Windows.Forms.Label();
			this.m_setioButton = new System.Windows.Forms.Button();
			this.m_closeioButton = new System.Windows.Forms.Button();
			this.m_downloadButton = new System.Windows.Forms.Button();
			this.m_falltimeTextBox = new System.Windows.Forms.TextBox();
			this.m_pulsetopTextBox = new System.Windows.Forms.TextBox();
			this.m_risetimeTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mygroupBox = new System.Windows.Forms.GroupBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.mygroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_gpibAddrTextBox
			// 
			this.m_gpibAddrTextBox.Location = new System.Drawing.Point(6, 13);
			this.m_gpibAddrTextBox.Name = "m_gpibAddrTextBox";
			this.m_gpibAddrTextBox.Size = new System.Drawing.Size(211, 21);
			this.m_gpibAddrTextBox.TabIndex = 3;
			this.m_gpibAddrTextBox.Text = "GPIB0::10";
			this.toolTipCtrl.SetToolTip(this.m_gpibAddrTextBox, "Enter the GPIB board number and the GPIB address of the Instrument");
			// 
			// m_idLabel
			// 
			this.m_idLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_idLabel.Location = new System.Drawing.Point(9, 40);
			this.m_idLabel.Name = "m_idLabel";
			this.m_idLabel.Size = new System.Drawing.Size(457, 20);
			this.m_idLabel.TabIndex = 4;
			this.m_idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTipCtrl.SetToolTip(this.m_idLabel, "Displays ID string of the instrument");
			// 
			// m_setioButton
			// 
			this.m_setioButton.Location = new System.Drawing.Point(220, 12);
			this.m_setioButton.Name = "m_setioButton";
			this.m_setioButton.Size = new System.Drawing.Size(104, 24);
			this.m_setioButton.TabIndex = 2;
			this.m_setioButton.Text = "Set I/O";
			this.toolTipCtrl.SetToolTip(this.m_setioButton, "Click to set the IO enviornment and get the ID of the instrument");
			this.m_setioButton.Click += new System.EventHandler(this.m_setioButton_Click);
			// 
			// m_closeioButton
			// 
			this.m_closeioButton.Enabled = false;
			this.m_closeioButton.Location = new System.Drawing.Point(331, 12);
			this.m_closeioButton.Name = "m_closeioButton";
			this.m_closeioButton.Size = new System.Drawing.Size(104, 24);
			this.m_closeioButton.TabIndex = 5;
			this.m_closeioButton.Text = "Close I/O";
			this.toolTipCtrl.SetToolTip(this.m_closeioButton, "Click to set the IO enviornment and get the ID of the instrument");
			this.m_closeioButton.Click += new System.EventHandler(this.closeioButton_Click);
			// 
			// m_downloadButton
			// 
			this.m_downloadButton.Location = new System.Drawing.Point(9, 143);
			this.m_downloadButton.Name = "m_downloadButton";
			this.m_downloadButton.Size = new System.Drawing.Size(457, 34);
			this.m_downloadButton.TabIndex = 8;
			this.m_downloadButton.Text = "Download Pulse";
			this.toolTipCtrl.SetToolTip(this.m_downloadButton, "Click to download the points to instrument");
			this.m_downloadButton.Click += new System.EventHandler(this.m_downloadButton_Click);
			// 
			// m_falltimeTextBox
			// 
			this.m_falltimeTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_falltimeTextBox.Location = new System.Drawing.Point(339, 44);
			this.m_falltimeTextBox.Name = "m_falltimeTextBox";
			this.m_falltimeTextBox.Size = new System.Drawing.Size(128, 23);
			this.m_falltimeTextBox.TabIndex = 5;
			this.m_falltimeTextBox.Text = "15";
			this.toolTipCtrl.SetToolTip(this.m_falltimeTextBox, "Enter the points for fall time");
			this.m_falltimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_risetimeTextBox_KeyPress);
			// 
			// m_pulsetopTextBox
			// 
			this.m_pulsetopTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_pulsetopTextBox.Location = new System.Drawing.Point(174, 44);
			this.m_pulsetopTextBox.Name = "m_pulsetopTextBox";
			this.m_pulsetopTextBox.Size = new System.Drawing.Size(128, 23);
			this.m_pulsetopTextBox.TabIndex = 4;
			this.m_pulsetopTextBox.Text = "200";
			this.toolTipCtrl.SetToolTip(this.m_pulsetopTextBox, "Enter the points for width of pulse top");
			this.m_pulsetopTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_risetimeTextBox_KeyPress);
			// 
			// m_risetimeTextBox
			// 
			this.m_risetimeTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_risetimeTextBox.Location = new System.Drawing.Point(9, 44);
			this.m_risetimeTextBox.Name = "m_risetimeTextBox";
			this.m_risetimeTextBox.Size = new System.Drawing.Size(128, 23);
			this.m_risetimeTextBox.TabIndex = 3;
			this.m_risetimeTextBox.Text = "50";
			this.toolTipCtrl.SetToolTip(this.m_risetimeTextBox, "Enter the points for rise time");
			this.m_risetimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_risetimeTextBox_KeyPress);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_closeioButton);
			this.groupBox1.Controls.Add(this.m_idLabel);
			this.groupBox1.Controls.Add(this.m_gpibAddrTextBox);
			this.groupBox1.Controls.Add(this.m_setioButton);
			this.groupBox1.Location = new System.Drawing.Point(2, -2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(474, 65);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// mygroupBox
			// 
			this.mygroupBox.Controls.Add(this.m_downloadButton);
			this.mygroupBox.Controls.Add(this.Label5);
			this.mygroupBox.Controls.Add(this.Label4);
			this.mygroupBox.Controls.Add(this.m_falltimeTextBox);
			this.mygroupBox.Controls.Add(this.m_pulsetopTextBox);
			this.mygroupBox.Controls.Add(this.m_risetimeTextBox);
			this.mygroupBox.Controls.Add(this.Label3);
			this.mygroupBox.Controls.Add(this.Label2);
			this.mygroupBox.Controls.Add(this.Label1);
			this.mygroupBox.Enabled = false;
			this.mygroupBox.Location = new System.Drawing.Point(2, 68);
			this.mygroupBox.Name = "mygroupBox";
			this.mygroupBox.Size = new System.Drawing.Size(474, 182);
			this.mygroupBox.TabIndex = 18;
			this.mygroupBox.TabStop = false;
			this.mygroupBox.Text = "Arbitrary Pulse Generation";
			// 
			// Label5
			// 
			this.Label5.Location = new System.Drawing.Point(9, 113);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(457, 30);
			this.Label5.TabIndex = 7;
			this.Label5.Text = "Calculate the time per point as the period of the frequency divided by the number" +
				" of points (4000) in the array.";
			// 
			// Label4
			// 
			this.Label4.Location = new System.Drawing.Point(9, 78);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(457, 30);
			this.Label4.TabIndex = 6;
			this.Label4.Text = "The pulse parameters are expressed as number of points. Each point is 0.5usec @ 5" +
				"kHz, 4000pts in array.";
			// 
			// Label3
			// 
			this.Label3.Location = new System.Drawing.Point(339, 22);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(126, 18);
			this.Label3.TabIndex = 2;
			this.Label3.Text = "Fall time";
			// 
			// Label2
			// 
			this.Label2.Location = new System.Drawing.Point(174, 22);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(126, 18);
			this.Label2.TabIndex = 1;
			this.Label2.Text = "Pulse top width";
			// 
			// Label1
			// 
			this.Label1.Location = new System.Drawing.Point(9, 22);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(126, 18);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Rise Time";
			// 
			// CArbPulse
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(478, 252);
			this.Controls.Add(this.mygroupBox);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CArbPulse";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Agilent 33120A ArbPulse";
			this.Load += new System.EventHandler(this.CArbPulse_Load);
			this.groupBox1.ResumeLayout(false);
			this.mygroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new CArbPulse());
		}

		
		private void m_setioButton_Click(object sender, System.EventArgs e)
		{
			this.rm  =  new ResourceManager();
			try
			{
					//Open port
					this.msg  = (rm.Open(this.m_gpibAddrTextBox.Text, Ivi.Visa.Interop.AccessMode.NO_LOCK   , 2000, "")) as IMessage ;
					/*Initialize Formatted IO If this works fine then we are ready to go*/
					ioArbFG.IO = msg;
			}
			catch (SystemException ex)
			{
				MessageBox.Show("Open failed on " + this.m_gpibAddrTextBox.Text + " " + ex.Source + "  " + ex.Message, "ArbPulse", MessageBoxButtons.OK, MessageBoxIcon.Error); 
				DisableControls();
				ioArbFG.IO = null;
            return;
			}

			//Get the ID string of the instrument connected.
			m_idLabel.Text = GetInstrumentID();
		
		}

		
		/// <summary>
		/// Method to get the ID string of instrument
		/// </summary>
		
		/*Method to read ID string of the instrument */
		 private string GetInstrumentID()
		{
			string m_strReturn;
			ioArbFG.WriteString("*IDN?",true);
			m_strReturn = ioArbFG.ReadString();

			//Check if its not null
			if(m_strReturn != null)
			{
				EnableControls();
				return m_strReturn;
			}
			else
			{
				DisableControls();
				return "Error : Could not read the ID of instrument";
			}
			
		}
	
		private void CArbPulse_Load(object sender, System.EventArgs e)
		{
			DisableControls(); 
			ioArbFG = new FormattedIO488Class();
		}

		private void EnableControls()
		{
			this.mygroupBox.Enabled = true;
			this.m_closeioButton.Enabled = true;
		}

		private void DisableControls()
		{
			this.mygroupBox.Enabled = false;
			this.m_closeioButton.Enabled = false;
		}

		private void closeioButton_Click(object sender, System.EventArgs e)
		{
			ioArbFG.IO.Close ();
			DisableControls();
			this.m_setioButton.Enabled = true;
			m_idLabel.Text = "";
		}

		private void m_risetimeTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			short KeyAscii  = Convert.ToSByte(e.KeyChar);
			if(KeyAscii < 32 || (KeyAscii >=48 && KeyAscii <= 57))
			{

			}
			else
				KeyAscii = 0;

			if(KeyAscii == 0)
				e.Handled = true;
		}
		
		private string makePulse(long riseTime , long TopWidth , long fallTime )
		{
			string strData;
			string[] Waveform= new string [4000];
			long topStart;
			long topStop;
			long endPulse;
			long i;

  		    topStart = riseTime;
			topStop = topStart + TopWidth;
			endPulse = topStop + fallTime;

			//' Set rise time																																													  
			for(i = 0; i<= riseTime;i++)
					  Waveform[i] = (Convert.ToSingle(i-1)/riseTime).ToString();
			
			// Set pulse width
			for(i = riseTime + 1; i<= topStop;i++)
					 Waveform[i] = "1";
			
			// Set fall time
			for( i = topStop + 1 ;i <= endPulse;i++)
					Waveform[i] = (Convert.ToSingle(endPulse - i) / fallTime).ToString();
			

			// Set zero level for rest of points
			for( i = endPulse + 1;i <4000; i++)
					Waveform[i] = "0";
			
			strData = String.Join(",", Waveform );
			return strData;

			}

		private void m_downloadButton_Click(object sender, System.EventArgs e)
		{
			string strData;
	        m_downloadButton.Enabled = false;
			
			strData = makePulse(Convert.ToInt32(m_risetimeTextBox.Text), Convert.ToInt32(m_pulsetopTextBox.Text), Convert.ToInt32(m_falltimeTextBox.Text));

			// Reset instrument
			ioArbFG.WriteString("*RST",true);

			// Set timeout large enough to sent all data
			ioArbFG.IO.Timeout = 40000;

			// Download  data points to volatile memory from array
			ioArbFG.WriteString("Data Volatile, " + strData,true);

			ioArbFG.WriteString("Data:Copy Pulse, Volatile",true);  // ' Copy arb to non-volatile memory

			ioArbFG.WriteString("Function:User Pulse",true);         //' Select the active arb waveform
			ioArbFG.WriteString("Function:Shape User",true);         //' output selected arb waveform
			ioArbFG.WriteString("Output:Load 50",true);              //' Output termination is 50 ohms
			ioArbFG.WriteString("Frequency 5000; Voltage 5",true);   //' Ouput frequency is 5kHz @ 5 Vpp

			// The arb will require some time to set everything up at this point
			m_downloadButton.Enabled = true;
			MessageBox.Show("Download complete");
		}
	}
}
