/* Simple GPIB Communication in Visual C# .NET with Measurement Studio 6.0 Active X Controls

With this example, you can use the Measurement Studio GPIB (CWGPIB) control 
to write to and read from a GPIB instrument.  

Be default, it configures an instrument at primary address 2 and secondary
address 0 on GPIB board number 0.  

The user must first configure the system for the specified board and instrument address.  
Then the user may write to and read from the specified GPIB instrument.

Software 
Group: Measurement Studio
	Version: 6.0
Language: C#
	Version: 7.0
Required Add on Kit: None

Hardware
Group: GPIB
Driver info: NI-488.2
	Version: 1.7
Required Hardware: GPIB card

*/


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Simple_GPIB
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

		// These are the declarations for the controls on the form.
		private System.Windows.Forms.Button readButton;
		private System.Windows.Forms.TextBox receivedString;
		private System.Windows.Forms.Label label2;
		private AxCWInstrumentControlLib.AxCWGPIB CWGPIB1;
		private AxCWUIControlsLib.AxCWNumEdit gpibAddr;
		private AxCWUIControlsLib.AxCWNumEdit instrAddr;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private AxCWUIControlsLib.AxCWNumEdit instrSecAddr;
		private System.Windows.Forms.Button configureButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button writeButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox sendString;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.CWGPIB1 = new AxCWInstrumentControlLib.AxCWGPIB();
			this.configureButton = new System.Windows.Forms.Button();
			this.readButton = new System.Windows.Forms.Button();
			this.receivedString = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gpibAddr = new AxCWUIControlsLib.AxCWNumEdit();
			this.instrAddr = new AxCWUIControlsLib.AxCWNumEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.instrSecAddr = new AxCWUIControlsLib.AxCWNumEdit();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.writeButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.sendString = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.CWGPIB1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gpibAddr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.instrAddr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.instrSecAddr)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// CWGPIB1
			// 
			this.CWGPIB1.Enabled = true;
			this.CWGPIB1.Location = new System.Drawing.Point(328, 8);
			this.CWGPIB1.Name = "CWGPIB1";
			this.CWGPIB1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CWGPIB1.OcxState")));
			this.CWGPIB1.Size = new System.Drawing.Size(32, 32);
			this.CWGPIB1.TabIndex = 0;
			// 
			// configureButton
			// 
			this.configureButton.Location = new System.Drawing.Point(256, 72);
			this.configureButton.Name = "configureButton";
			this.configureButton.Size = new System.Drawing.Size(72, 32);
			this.configureButton.TabIndex = 3;
			this.configureButton.Text = "Configure";
			this.configureButton.Click += new System.EventHandler(this.configureButton_Click);
			// 
			// readButton
			// 
			this.readButton.Enabled = false;
			this.readButton.Location = new System.Drawing.Point(11, 290);
			this.readButton.Name = "readButton";
			this.readButton.Size = new System.Drawing.Size(72, 32);
			this.readButton.TabIndex = 5;
			this.readButton.Text = "Read";
			this.readButton.Click += new System.EventHandler(this.readButton_Click);
			// 
			// receivedString
			// 
			this.receivedString.Location = new System.Drawing.Point(128, 313);
			this.receivedString.Multiline = true;
			this.receivedString.Name = "receivedString";
			this.receivedString.Size = new System.Drawing.Size(224, 71);
			this.receivedString.TabIndex = 6;
			this.receivedString.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(128, 290);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "String Received";
			// 
			// gpibAddr
			// 
			this.gpibAddr.ContainingControl = this;
			this.gpibAddr.Location = new System.Drawing.Point(112, 24);
			this.gpibAddr.Name = "gpibAddr";
			this.gpibAddr.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("gpibAddr.OcxState")));
			this.gpibAddr.Size = new System.Drawing.Size(80, 24);
			this.gpibAddr.TabIndex = 8;
			// 
			// instrAddr
			// 
			this.instrAddr.ContainingControl = this;
			this.instrAddr.Location = new System.Drawing.Point(112, 51);
			this.instrAddr.Name = "instrAddr";
			this.instrAddr.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("instrAddr.OcxState")));
			this.instrAddr.Size = new System.Drawing.Size(80, 24);
			this.instrAddr.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 10;
			this.label3.Text = "Board";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Instrument PAD";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 84);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 12;
			this.label5.Text = "Instrument SAD";
			// 
			// instrSecAddr
			// 
			this.instrSecAddr.ContainingControl = this;
			this.instrSecAddr.Location = new System.Drawing.Point(112, 80);
			this.instrSecAddr.Name = "instrSecAddr";
			this.instrSecAddr.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("instrSecAddr.OcxState")));
			this.instrSecAddr.Size = new System.Drawing.Size(80, 24);
			this.instrSecAddr.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(296, 56);
			this.label6.TabIndex = 14;
			this.label6.Text = "Select the Board, Primary Address (PAD), and Secondary Address (PAD) of the GPIB " +
				"device and click the Configure button.";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label3,
																					this.instrAddr,
																					this.gpibAddr,
																					this.label4,
																					this.label5,
																					this.instrSecAddr,
																					this.configureButton});
			this.groupBox1.Location = new System.Drawing.Point(8, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 112);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
			// 
			// writeButton
			// 
			this.writeButton.Enabled = false;
			this.writeButton.Location = new System.Drawing.Point(256, 24);
			this.writeButton.Name = "writeButton";
			this.writeButton.Size = new System.Drawing.Size(72, 32);
			this.writeButton.TabIndex = 4;
			this.writeButton.Text = "Write";
			this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Send String";
			// 
			// sendString
			// 
			this.sendString.Location = new System.Drawing.Point(112, 30);
			this.sendString.Name = "sendString";
			this.sendString.Size = new System.Drawing.Size(120, 20);
			this.sendString.TabIndex = 1;
			this.sendString.Text = "*IDN?";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.writeButton,
																					this.label1,
																					this.sendString});
			this.groupBox2.Location = new System.Drawing.Point(8, 200);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 72);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Writing";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 397);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.groupBox1,
																		  this.label6,
																		  this.receivedString,
																		  this.readButton,
																		  this.CWGPIB1,
																		  this.label2});
			this.Name = "Form1";
			this.Text = "Simple GPIB Communication";
			((System.ComponentModel.ISupportInitialize)(this.CWGPIB1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gpibAddr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.instrAddr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.instrSecAddr)).EndInit();
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
			Application.Run(new Form1());
		}


		// Callback function for the Configure button.
		// This will configure communication to the specified instrument.
		private void configureButton_Click(object sender, System.EventArgs e)
		{
			
			// Set the board number, the primary address, and the secondary address.
			// Notice you must convert the data to the correct data type for the CWGPIB control.
			CWGPIB1.Reset();
			CWGPIB1.BoardNumber = System.Convert.ToInt16(gpibAddr.Value);
			CWGPIB1.PrimaryAddress = System.Convert.ToInt16(instrAddr.Value);
			CWGPIB1.SecondaryAddress = System.Convert.ToInt16(instrSecAddr.Value);
			// Now configure the GPIB system.
			CWGPIB1.Configure();

			// Now that it's enabled, allow the user to write and read.
			writeButton.Enabled = true;
			readButton.Enabled = true;

		}

		// Callback function for the Write button.
		private void writeButton_Click(object sender, System.EventArgs e)
		{
			CWGPIB1.Write(sendString.Text);
		}

		// Callback function for the Read button.
		private void readButton_Click(object sender, System.EventArgs e)
		{
			// When read back, you must convert the data to a string value.
			receivedString.Text = System.Convert.ToString(CWGPIB1.Read(100));
		}

		
	}
}
