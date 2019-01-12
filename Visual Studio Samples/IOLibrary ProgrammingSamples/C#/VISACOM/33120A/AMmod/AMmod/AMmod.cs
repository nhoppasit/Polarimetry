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

' This example explains follwing
' 1. Using VISA-COM library and formatted IO to communicate with the instrument
' 2. This program uses low-level SCPI commands to configure' the function 
'     generator to output an AM waveform.
' 3. This program also shows how to use "state storage" to
'      store the instrument configuration in memory.*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ivi.Visa.Interop;

namespace AMmod
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CAMmodulation : System.Windows.Forms.Form
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
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox m_stagelocationComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button m_setamButton;
		private System.Windows.Forms.Button m_saveButton;
		private System.Windows.Forms.Button m_recallButton;
		private System.Windows.Forms.Button m_closeioButton;
		private System.ComponentModel.IContainer components;

		public CAMmodulation()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CAMmodulation));
			this.toolTipCtrl = new System.Windows.Forms.ToolTip(this.components);
			this.m_gpibAddrTextBox = new System.Windows.Forms.TextBox();
			this.m_idLabel = new System.Windows.Forms.Label();
			this.m_setioButton = new System.Windows.Forms.Button();
			this.m_stagelocationComboBox = new System.Windows.Forms.ComboBox();
			this.m_saveButton = new System.Windows.Forms.Button();
			this.m_recallButton = new System.Windows.Forms.Button();
			this.m_closeioButton = new System.Windows.Forms.Button();
			this.m_setamButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			// m_stagelocationComboBox
			// 
			this.m_stagelocationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_stagelocationComboBox.Items.AddRange(new object[] {
																		 "1",
																		 "2",
																		 "3",
																		 "4",
																		 "5"});
			this.m_stagelocationComboBox.Location = new System.Drawing.Point(89, 27);
			this.m_stagelocationComboBox.Name = "m_stagelocationComboBox";
			this.m_stagelocationComboBox.Size = new System.Drawing.Size(58, 21);
			this.m_stagelocationComboBox.TabIndex = 12;
			this.toolTipCtrl.SetToolTip(this.m_stagelocationComboBox, "Select memory location where the settings are to be stored/recalled");
			// 
			// m_saveButton
			// 
			this.m_saveButton.Location = new System.Drawing.Point(7, 18);
			this.m_saveButton.Name = "m_saveButton";
			this.m_saveButton.Size = new System.Drawing.Size(70, 39);
			this.m_saveButton.TabIndex = 11;
			this.m_saveButton.Text = "Save State";
			this.toolTipCtrl.SetToolTip(this.m_saveButton, "Save state to the selected memory location");
			this.m_saveButton.Click += new System.EventHandler(this.m_saveButton_Click);
			// 
			// m_recallButton
			// 
			this.m_recallButton.Location = new System.Drawing.Point(159, 18);
			this.m_recallButton.Name = "m_recallButton";
			this.m_recallButton.Size = new System.Drawing.Size(70, 39);
			this.m_recallButton.TabIndex = 10;
			this.m_recallButton.Text = "Recall State";
			this.toolTipCtrl.SetToolTip(this.m_recallButton, "Recalls the satee from the selected location");
			this.m_recallButton.Click += new System.EventHandler(this.m_recallButton_Click);
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
			// m_setamButton
			// 
			this.m_setamButton.Location = new System.Drawing.Point(6, 86);
			this.m_setamButton.Name = "m_setamButton";
			this.m_setamButton.Size = new System.Drawing.Size(225, 27);
			this.m_setamButton.TabIndex = 2;
			this.m_setamButton.Text = "Set AM";
			this.m_setamButton.Click += new System.EventHandler(this.m_setamButton_Click);
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.m_stagelocationComboBox);
			this.groupBox2.Controls.Add(this.m_saveButton);
			this.groupBox2.Controls.Add(this.m_recallButton);
			this.groupBox2.Location = new System.Drawing.Point(237, 66);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(237, 183);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Save/Recall";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 98);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(220, 72);
			this.label1.TabIndex = 13;
			this.label1.Text = "This section explains how frequently used setting can be stored in the memory loc" +
				"ation. Select the location from the list. Press \'Save\' or \'Recall\' to store or r" +
				"ecall the panel setting.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(220, 114);
			this.label2.TabIndex = 14;
			this.label2.Text = "Press the \'Set AM\' button to set the Function generator to give following output:" +
				"                                            Amplitude modulated sine wave with c" +
				"arrier as 5kHz @5Vpp and modulating signal 200Hz Sine at 50Ohm";
			// 
			// CAMmodulation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(478, 254);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.m_setamButton);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CAMmodulation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Agilent 33120A AM Mod";
			this.Load += new System.EventHandler(this.CAMmodulation_Load);
			this.groupBox1.ResumeLayout(false);
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
			Application.Run(new CAMmodulation());
		}

		private void m_setioButton_Click(object sender, System.EventArgs e)
		{
			this.rm  =  new ResourceManager();
			try
			{
					this.msg  = (rm.Open(this.m_gpibAddrTextBox.Text, Ivi.Visa.Interop.AccessMode.NO_LOCK   , 2000, "")) as IMessage ;
					ioArbFG.IO = msg;
			}
			catch (SystemException ex)
			{
				MessageBox.Show("Open failed on " + this.m_gpibAddrTextBox.Text + " " + ex.Source + "  " + ex.Message, "AM Modulation", MessageBoxButtons.OK, MessageBoxIcon.Error); 
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
			ioArbFG.WriteString("*IDN?",true);
			m_strReturn = ioArbFG.ReadString();

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
		
		private void CAMmodulation_Load(object sender, System.EventArgs e)
		{
			DisableControls(); 
			ioArbFG = new FormattedIO488Class();
			this.m_stagelocationComboBox.SelectedIndex =0;
		}

		private void m_setamButton_Click(object sender, System.EventArgs e)
		{
			ioArbFG.WriteString("*RST",true);										// reset instrument to bring to know state
			ioArbFG.WriteString( "Output:load 50",true);						// set output termination to 50 ohm
			ioArbFG.WriteString( "Function:Shape Sinusoid",true);		// set carrier waveshape to sine
			ioArbFG.WriteString( "Frequency 5000; Volt 5",true);			// set carrier frequency to 5kHz, @ 5Vpp
			ioArbFG.WriteString( "AM:Internal:Function Sinusoid",true);// modulation to sine
			ioArbFG.WriteString( "AM:Internal:Frequency 200",true);	// Modulation frequency to 200Hz
			ioArbFG.WriteString( "AM:State On",true);							// Output on
		}

		private void m_saveButton_Click(object sender, System.EventArgs e)
		{
			if(this.m_stagelocationComboBox.Text != "")
					ioArbFG.WriteString("*SAV " + this.m_stagelocationComboBox.Text,true); 
			else
					MessageBox.Show ("Please select a location from list");
		}

		private void m_recallButton_Click(object sender, System.EventArgs e)
		{
			if(this.m_stagelocationComboBox.Text != "")
				ioArbFG.WriteString("*RCL " + this.m_stagelocationComboBox.Text,true); 
			else
				MessageBox.Show ("Please select a location from list");
		}

		private void EnableControls()
		{
			this.m_setamButton.Enabled = true;
			this.groupBox2.Enabled = true;
			this.m_closeioButton.Enabled = true;
		}

		private void DisableControls()
		{
			this.m_setamButton.Enabled = false;
			this.groupBox2.Enabled = false;
			this.m_closeioButton.Enabled = false;
		}

		private void closeioButton_Click(object sender, System.EventArgs e)
		{
			ioArbFG.IO.Close ();
			DisableControls();
			this.m_setioButton.Enabled = true;
			m_idLabel.Text = "";
		}

	}
}
