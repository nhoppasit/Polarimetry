/*' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
' Copyright © 2001 Agilent Technologies Inc. All rights
' reserved.
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

' This program sets up a burst waveform with a 270 degree
' starting phase. By adding an offset voltage to the burst,
' a "haversine" is created. This program also shows the use of
' a trigger received over the GPIB interface to  initiate a single
' trigger.
' To run the program, first make sure the GPIB address is set correctly
' and then click on Set Generator. To create on burst click on Trigger.


' To see this on a scope set the scope trigger to normal, DC, 1v
' and set the horizontal to 100usec/div, and the vertcal scale to
' 20V/div*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ivi.Visa.Interop;

namespace ApplyBurst
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CApplyBurst : System.Windows.Forms.Form
	{
		private Ivi.Visa.Interop.ResourceManager  rm;
		private  Ivi.Visa.Interop.FormattedIO488  ioArbFG;
		private  Ivi.Visa.Interop.IMessage  msg;
		private System.Windows.Forms.ToolTip toolTipCtrl;
		private System.Windows.Forms.GroupBox gb2;
		private System.Windows.Forms.GroupBox gb3;
		private System.Windows.Forms.Label burstnoLabel;
		private System.Windows.Forms.Label functionLabel;
		private System.Windows.Forms.Label frequencyLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label m_idLabel;
		private System.Windows.Forms.TextBox m_gpibAddrTextBox;
		private System.Windows.Forms.Button m_setioButton;
		private System.Windows.Forms.Button m_closeioButton;
		private System.Windows.Forms.Button m_configureButton;
		private System.Windows.Forms.ComboBox m_burstnoComboBox;
		private System.Windows.Forms.ComboBox m_functionComboBox;
		private System.Windows.Forms.TextBox m_frequencyTextBox;
		private System.Windows.Forms.Button m_initiateButton;
		private System.Windows.Forms.TextBox m_errorListBox;
		private System.Windows.Forms.Button m_geterrorButton;
		private System.ComponentModel.IContainer components;

		public CApplyBurst()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CApplyBurst));
			this.toolTipCtrl = new System.Windows.Forms.ToolTip(this.components);
			this.m_closeioButton = new System.Windows.Forms.Button();
			this.m_idLabel = new System.Windows.Forms.Label();
			this.m_gpibAddrTextBox = new System.Windows.Forms.TextBox();
			this.m_setioButton = new System.Windows.Forms.Button();
			this.m_configureButton = new System.Windows.Forms.Button();
			this.m_burstnoComboBox = new System.Windows.Forms.ComboBox();
			this.m_functionComboBox = new System.Windows.Forms.ComboBox();
			this.m_frequencyTextBox = new System.Windows.Forms.TextBox();
			this.m_initiateButton = new System.Windows.Forms.Button();
			this.m_errorListBox = new System.Windows.Forms.TextBox();
			this.m_geterrorButton = new System.Windows.Forms.Button();
			this.gb2 = new System.Windows.Forms.GroupBox();
			this.burstnoLabel = new System.Windows.Forms.Label();
			this.functionLabel = new System.Windows.Forms.Label();
			this.frequencyLabel = new System.Windows.Forms.Label();
			this.gb3 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.gb2.SuspendLayout();
			this.gb3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_closeioButton
			// 
			this.m_closeioButton.Enabled = false;
			this.m_closeioButton.Location = new System.Drawing.Point(331, 12);
			this.m_closeioButton.Name = "m_closeioButton";
			this.m_closeioButton.Size = new System.Drawing.Size(104, 24);
			this.m_closeioButton.TabIndex = 2;
			this.m_closeioButton.Text = "Close I/O";
			this.toolTipCtrl.SetToolTip(this.m_closeioButton, "Click to close the IO enviornment.");
			this.m_closeioButton.Click += new System.EventHandler(this.closeioButton_Click);
			// 
			// m_idLabel
			// 
			this.m_idLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_idLabel.Location = new System.Drawing.Point(9, 40);
			this.m_idLabel.Name = "m_idLabel";
			this.m_idLabel.Size = new System.Drawing.Size(457, 20);
			this.m_idLabel.TabIndex = 3;
			this.m_idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTipCtrl.SetToolTip(this.m_idLabel, "Displays ID string of the instrument");
			// 
			// m_gpibAddrTextBox
			// 
			this.m_gpibAddrTextBox.Location = new System.Drawing.Point(6, 13);
			this.m_gpibAddrTextBox.Name = "m_gpibAddrTextBox";
			this.m_gpibAddrTextBox.Size = new System.Drawing.Size(211, 21);
			this.m_gpibAddrTextBox.TabIndex = 0;
			this.m_gpibAddrTextBox.Text = "GPIB0::10";
			this.toolTipCtrl.SetToolTip(this.m_gpibAddrTextBox, "Enter the GPIB board number and the GPIB address of the Instrument");
			// 
			// m_setioButton
			// 
			this.m_setioButton.Location = new System.Drawing.Point(220, 12);
			this.m_setioButton.Name = "m_setioButton";
			this.m_setioButton.Size = new System.Drawing.Size(104, 24);
			this.m_setioButton.TabIndex = 1;
			this.m_setioButton.Text = "Set I/O";
			this.toolTipCtrl.SetToolTip(this.m_setioButton, "Click to set the IO enviornment and get the ID of the instrument");
			this.m_setioButton.Click += new System.EventHandler(this.m_setioButton_Click);
			// 
			// m_configureButton
			// 
			this.m_configureButton.Location = new System.Drawing.Point(393, 41);
			this.m_configureButton.Name = "m_configureButton";
			this.m_configureButton.Size = new System.Drawing.Size(72, 20);
			this.m_configureButton.TabIndex = 6;
			this.m_configureButton.Text = "Configure";
			this.toolTipCtrl.SetToolTip(this.m_configureButton, "Press to configure the instrument with selected settings");
			this.m_configureButton.Click += new System.EventHandler(this.configureButton_Click);
			// 
			// m_burstnoComboBox
			// 
			this.m_burstnoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_burstnoComboBox.Items.AddRange(new object[] {
																   "1",
																   "2",
																   "3",
																   "4",
																   "5",
																   "10"});
			this.m_burstnoComboBox.Location = new System.Drawing.Point(254, 39);
			this.m_burstnoComboBox.Name = "m_burstnoComboBox";
			this.m_burstnoComboBox.Size = new System.Drawing.Size(134, 21);
			this.m_burstnoComboBox.TabIndex = 5;
			this.toolTipCtrl.SetToolTip(this.m_burstnoComboBox, "Select the no. of bursts");
			// 
			// m_functionComboBox
			// 
			this.m_functionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_functionComboBox.Items.AddRange(new object[] {
																	"Sine",
																	"Square",
																	"Triangle",
																	"Ramp"});
			this.m_functionComboBox.Location = new System.Drawing.Point(151, 39);
			this.m_functionComboBox.Name = "m_functionComboBox";
			this.m_functionComboBox.Size = new System.Drawing.Size(99, 21);
			this.m_functionComboBox.TabIndex = 4;
			this.toolTipCtrl.SetToolTip(this.m_functionComboBox, "Select the function type");
			// 
			// m_frequencyTextBox
			// 
			this.m_frequencyTextBox.Location = new System.Drawing.Point(9, 39);
			this.m_frequencyTextBox.Name = "m_frequencyTextBox";
			this.m_frequencyTextBox.Size = new System.Drawing.Size(134, 21);
			this.m_frequencyTextBox.TabIndex = 3;
			this.m_frequencyTextBox.Text = "5000";
			this.toolTipCtrl.SetToolTip(this.m_frequencyTextBox, "Enter the value of frequency and press \'Enter Key\'");
			this.m_frequencyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frequencyTextBox_KeyPress);
			this.m_frequencyTextBox.Leave += new System.EventHandler(this.frequencyTextBox_Leave);
			// 
			// m_initiateButton
			// 
			this.m_initiateButton.Location = new System.Drawing.Point(4, 145);
			this.m_initiateButton.Name = "m_initiateButton";
			this.m_initiateButton.Size = new System.Drawing.Size(471, 40);
			this.m_initiateButton.TabIndex = 2;
			this.m_initiateButton.Text = "2. Initiate Burst";
			this.toolTipCtrl.SetToolTip(this.m_initiateButton, "Initiate burst signal by triggering the instrument");
			this.m_initiateButton.Click += new System.EventHandler(this.initiateButton_Click);
			// 
			// m_errorListBox
			// 
			this.m_errorListBox.AcceptsTab = true;
			this.m_errorListBox.Location = new System.Drawing.Point(8, 17);
			this.m_errorListBox.Multiline = true;
			this.m_errorListBox.Name = "m_errorListBox";
			this.m_errorListBox.ReadOnly = true;
			this.m_errorListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.m_errorListBox.Size = new System.Drawing.Size(341, 108);
			this.m_errorListBox.TabIndex = 1;
			this.m_errorListBox.Text = "";
			this.toolTipCtrl.SetToolTip(this.m_errorListBox, "Shows the history of errors");
			// 
			// m_geterrorButton
			// 
			this.m_geterrorButton.Location = new System.Drawing.Point(354, 51);
			this.m_geterrorButton.Name = "m_geterrorButton";
			this.m_geterrorButton.Size = new System.Drawing.Size(112, 37);
			this.m_geterrorButton.TabIndex = 0;
			this.m_geterrorButton.Text = "Get Instrument Error";
			this.toolTipCtrl.SetToolTip(this.m_geterrorButton, "Click to get the errors if any");
			this.m_geterrorButton.Click += new System.EventHandler(this.geterrorButton_Click);
			// 
			// gb2
			// 
			this.gb2.Controls.Add(this.m_configureButton);
			this.gb2.Controls.Add(this.burstnoLabel);
			this.gb2.Controls.Add(this.functionLabel);
			this.gb2.Controls.Add(this.frequencyLabel);
			this.gb2.Controls.Add(this.m_burstnoComboBox);
			this.gb2.Controls.Add(this.m_functionComboBox);
			this.gb2.Controls.Add(this.m_frequencyTextBox);
			this.gb2.Location = new System.Drawing.Point(3, 66);
			this.gb2.Name = "gb2";
			this.gb2.Size = new System.Drawing.Size(473, 72);
			this.gb2.TabIndex = 1;
			this.gb2.TabStop = false;
			this.gb2.Text = "1. Configure ArbFG for Burst";
			// 
			// burstnoLabel
			// 
			this.burstnoLabel.Location = new System.Drawing.Point(254, 19);
			this.burstnoLabel.Name = "burstnoLabel";
			this.burstnoLabel.Size = new System.Drawing.Size(123, 18);
			this.burstnoLabel.TabIndex = 2;
			this.burstnoLabel.Text = "Select no of Bursts:";
			// 
			// functionLabel
			// 
			this.functionLabel.Location = new System.Drawing.Point(151, 19);
			this.functionLabel.Name = "functionLabel";
			this.functionLabel.Size = new System.Drawing.Size(99, 18);
			this.functionLabel.TabIndex = 1;
			this.functionLabel.Text = "Select Function:";
			// 
			// frequencyLabel
			// 
			this.frequencyLabel.Location = new System.Drawing.Point(9, 19);
			this.frequencyLabel.Name = "frequencyLabel";
			this.frequencyLabel.Size = new System.Drawing.Size(120, 18);
			this.frequencyLabel.TabIndex = 0;
			this.frequencyLabel.Text = "Set Frequency(Hz):";
			// 
			// gb3
			// 
			this.gb3.Controls.Add(this.m_errorListBox);
			this.gb3.Controls.Add(this.m_geterrorButton);
			this.gb3.Location = new System.Drawing.Point(4, 191);
			this.gb3.Name = "gb3";
			this.gb3.Size = new System.Drawing.Size(471, 130);
			this.gb3.TabIndex = 3;
			this.gb3.TabStop = false;
			this.gb3.Text = "Check Instrument Errors";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_closeioButton);
			this.groupBox1.Controls.Add(this.m_idLabel);
			this.groupBox1.Controls.Add(this.m_gpibAddrTextBox);
			this.groupBox1.Controls.Add(this.m_setioButton);
			this.groupBox1.Location = new System.Drawing.Point(2, -4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(474, 65);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// CApplyBurst
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(478, 325);
			this.Controls.Add(this.gb3);
			this.Controls.Add(this.m_initiateButton);
			this.Controls.Add(this.gb2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CApplyBurst";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Agilent 33120A Burst Example";
			this.Load += new System.EventHandler(this.CApplyBurst_Load);
			this.gb2.ResumeLayout(false);
			this.gb3.ResumeLayout(false);
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
			Application.Run(new CApplyBurst());
		}

		
		private void m_setioButton_Click(object sender, System.EventArgs e)
		{
			this.rm  =  new ResourceManager();
			try
			{
					//Open port
					this.msg  = (rm.Open(this.m_gpibAddrTextBox.Text, Ivi.Visa.Interop.AccessMode.NO_LOCK   , 2000, "")) as Ivi.Visa.Interop.IMessage ;
				    //Initialize Formatted IO	
					ioArbFG.IO = msg;
			}
			catch (SystemException ex)
			{
				MessageBox.Show("Open failed on " + this.m_gpibAddrTextBox.Text + " " + ex.Source + "  " + ex.Message, "ApplyBurst", MessageBoxButtons.OK, MessageBoxIcon.Error); 
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
		
		 private string GetInstrumentID()
		{
			string m_strReturn;
			ioArbFG.WriteString("*RST",true);
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
				return "Error : Could not read ID of instrument";
			}
		}
		
		private void CApplyBurst_Load(object sender, System.EventArgs e)
		{
			DisableControls(); 
			ioArbFG = new FormattedIO488Class();
			this.m_functionComboBox.SelectedIndex = 0;
			this.m_burstnoComboBox.SelectedIndex = 0;
		}

		private void EnableControls()
		{
			this.gb2.Enabled = true;
			this.gb3.Enabled = true;
			this.m_closeioButton.Enabled =true;
		}

		private void DisableControls()
		{
			this.gb2.Enabled = false;
			this.gb3.Enabled = false;
			this.m_initiateButton.Enabled = false;
		}

		private void geterrorButton_Click(object sender, System.EventArgs e)
		{
			string reply;
			ioArbFG.WriteString("syst:error?",true);
			reply = ioArbFG.ReadString();
			this.m_errorListBox.Text = this.m_errorListBox.Text + reply.Substring(0,reply.Length -1) + "\r\n";
		}

		private void configureButton_Click(object sender, System.EventArgs e)
		{
    		string strCmd="";
   		    ioArbFG.WriteString("*RST",true);
			ioArbFG.WriteString("Output:Load 50",true);
			switch(m_functionComboBox.Text.ToUpper())
			{
				case "SINE":
					  strCmd = "Apply:Sin " + m_frequencyTextBox .Text + ",5";
					  break;
				case "SQUARE":
                      strCmd = "Apply:Square " + m_frequencyTextBox .Text + ",5";
					  break;
				case  "TRIANGLE":
					  strCmd="Apply:Triangle " + m_frequencyTextBox .Text + ",5";
					  break;
				case "RAMP":
					  strCmd="Apply:Ramp " + m_frequencyTextBox .Text + ",5";
					  break;
			}
			ioArbFG.WriteString(strCmd, true);
			ioArbFG.WriteString("BM:NCYC " + this.m_burstnoComboBox.Text,true);
			ioArbFG.WriteString("BM:Phase 270",true);
			ioArbFG.WriteString("Volt:Offset .5",true);
			ioArbFG.WriteString("Trig:Sour Bus",true);
			ioArbFG.WriteString("BM:State On",true);
			this.m_initiateButton.Enabled = true;
  		}

		private void closeioButton_Click(object sender, System.EventArgs e)
		{
			ioArbFG.IO.Close ();
			this.m_closeioButton.Enabled = false;
			this.m_idLabel.Text = "";
			this.m_setioButton.Enabled = true;
			DisableControls();
		}

		private void initiateButton_Click(object sender, System.EventArgs e)
		{
			ioArbFG.WriteString("*TRG",true);
		}

		private float ConvertStringDouble(string strVal)
		{
			float floatVal = 0;
    
			try 
			{
				floatVal = (float)System.Convert.ToDouble (strVal);
			} 
			catch (System.OverflowException)
			{
				MessageBox.Show("The conversion from string-to-float overflowed.");
			}
			catch (System.FormatException) 
			{
				MessageBox.Show("The string is not formatted as a float.");
			}
			catch (System.ArgumentNullException) 
			{
				MessageBox.Show("The string is null.");
			}
			return floatVal;
		}

		private void frequencyTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r') //Enter key is pressed
			{
				//Ensure a valid entry for frequency
			 	float frq;
				frq = ConvertStringDouble(this.m_frequencyTextBox.Text);
				if( frq >= 1.0E-6 && frq <= 20E6)
				{
					//Valid frequency
				}
				else
				{
					MessageBox.Show("Valid Frequency Range : 0.000001 to 20000000");
					this.m_frequencyTextBox.Text = "5000";
				}
			}
		}

		private void frequencyTextBox_Leave(object sender, System.EventArgs e)
		{
			//Ensure a valid entry for frequency
			float frq;
			frq = ConvertStringDouble(this.m_frequencyTextBox.Text);
			if( frq >= 1.0E-6 && frq <= 20E6)
			{
				//Valid frequency
			}
			else
			{
				MessageBox.Show("Valid Frequency Range : 0.000001 to 20000000");
				this.m_frequencyTextBox.Text = "5000";
			}
		}
	}
}
