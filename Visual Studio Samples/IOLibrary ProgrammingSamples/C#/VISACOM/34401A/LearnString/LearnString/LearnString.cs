/* """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2002-2004 Agilent Technologies Inc. All rights
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
''' -------------------------------------------------------------------------
''' Project Name: learnString
'''
''' Description:   Demonstrates how to query the instrument for its
'''                settings. From the instrument responses this program
'''                will build a command string that can be sent to
'''                the instrument to set the instrument to the previous
'''                settings.
'''
'''                Copyright  ©  2000 Agilent Technologies, Inc.
'''
''' Date            Developer
''' June 23, 2004   Agilent Technologies
''' -------------------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ivi.Visa.Interop;

namespace LearnString
{
	/// <summary>
	/// Summary description for LearnString.
	/// </summary>
	public class LearnString : System.Windows.Forms.Form
	{
		//formatted IO object
		private FormattedIO488 ioDmm;
		private System.Windows.Forms.GroupBox grouppBox1;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.Button btnInitIO;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtData;
		private System.Windows.Forms.Label lblData;
		private System.Windows.Forms.Label lblInstrument;
		private System.Windows.Forms.TextBox txtInstrument;
		private System.Windows.Forms.Button btnGetReading;
		private System.Windows.Forms.Button btnSendReading;

		private string m_sLearnString;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		public LearnString()
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new LearnString());
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LearnString));
			this.grouppBox1 = new System.Windows.Forms.GroupBox();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.btnInitIO = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnSendReading = new System.Windows.Forms.Button();
			this.lblInstrument = new System.Windows.Forms.Label();
			this.btnGetReading = new System.Windows.Forms.Button();
			this.txtData = new System.Windows.Forms.TextBox();
			this.lblData = new System.Windows.Forms.Label();
			this.txtInstrument = new System.Windows.Forms.TextBox();
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
			this.grouppBox1.TabIndex = 25;
			this.grouppBox1.TabStop = false;
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(88, 24);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(184, 20);
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
			this.groupBox2.Controls.Add(this.btnSendReading);
			this.groupBox2.Controls.Add(this.lblInstrument);
			this.groupBox2.Controls.Add(this.btnGetReading);
			this.groupBox2.Controls.Add(this.txtData);
			this.groupBox2.Controls.Add(this.lblData);
			this.groupBox2.Controls.Add(this.txtInstrument);
			this.groupBox2.Location = new System.Drawing.Point(8, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 152);
			this.groupBox2.TabIndex = 26;
			this.groupBox2.TabStop = false;
			// 
			// btnSendReading
			// 
			this.btnSendReading.Location = new System.Drawing.Point(288, 96);
			this.btnSendReading.Name = "btnSendReading";
			this.btnSendReading.Size = new System.Drawing.Size(104, 23);
			this.btnSendReading.TabIndex = 23;
			this.btnSendReading.Text = "Send Readings";
			this.toolTip1.SetToolTip(this.btnSendReading, "Click to send the setting to the instrument");
			this.btnSendReading.Click += new System.EventHandler(this.btnSendReading_Click);
			// 
			// lblInstrument
			// 
			this.lblInstrument.Location = new System.Drawing.Point(16, 24);
			this.lblInstrument.Name = "lblInstrument";
			this.lblInstrument.Size = new System.Drawing.Size(64, 23);
			this.lblInstrument.TabIndex = 6;
			this.lblInstrument.Text = "Instrument";
			this.lblInstrument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnGetReading
			// 
			this.btnGetReading.Location = new System.Drawing.Point(288, 40);
			this.btnGetReading.Name = "btnGetReading";
			this.btnGetReading.Size = new System.Drawing.Size(104, 23);
			this.btnGetReading.TabIndex = 3;
			this.btnGetReading.Text = "Get Readings";
			this.toolTip1.SetToolTip(this.btnGetReading, "Click to to query the instrument for its setting");
			this.btnGetReading.Click += new System.EventHandler(this.btnGetReading_Click);
			// 
			// txtData
			// 
			this.txtData.Location = new System.Drawing.Point(88, 56);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtData.Size = new System.Drawing.Size(184, 88);
			this.txtData.TabIndex = 5;
			this.txtData.Text = "";
			// 
			// lblData
			// 
			this.lblData.Location = new System.Drawing.Point(16, 88);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(48, 16);
			this.lblData.TabIndex = 0;
			this.lblData.Text = "Data";
			// 
			// txtInstrument
			// 
			this.txtInstrument.Location = new System.Drawing.Point(88, 24);
			this.txtInstrument.Name = "txtInstrument";
			this.txtInstrument.Size = new System.Drawing.Size(184, 20);
			this.txtInstrument.TabIndex = 22;
			this.txtInstrument.Text = "GPIB::22";
			// 
			// LearnString
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 253);
			this.Controls.Add(this.grouppBox1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "LearnString";
			this.Text = "Agilent 34401A LearnString";
			this.Load += new System.EventHandler(this.LearnString_Load);
			this.grouppBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void LearnString_Load(object sender, System.EventArgs e)
		{
			try
			{
				//create the formatted io object
				ioDmm = new FormattedIO488Class();	
				m_sLearnString = "";
			}
			catch(SystemException ex)
			{
				MessageBox.Show("FormattedIO488Class object creation failure. " + ex.Source + "  " + ex.Message, "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
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

		/// <summary>
		/// Enable the relevant UI once a session has been successfully established with the instrument
		/// </summary>
		private void SetAccessForOpened()
		{
			this.txtAddress.Enabled = false;
			this.btnInitIO.Enabled = false;
			
			this.btnClose.Enabled = true;
			this.btnSendReading.Enabled = false;
			this.btnGetReading.Enabled = true;

			this.txtInstrument.Enabled = true;
			this.txtInstrument.Text = "";
            
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
			this.btnSendReading.Enabled = false;
			this.btnGetReading.Enabled = false;

			this.txtInstrument.Text = "";
			this.txtInstrument.Enabled = false;
			            
			this.txtData.Text = "";
			this.txtData.Enabled = false;
			
		}

		private void btnGetReading_Click(object sender, System.EventArgs e)
		{
			// Retrieve the learn string from instrument
			string sId;
			string[] sTempId;
			
			try
			{
				//disable close and send buttons
				btnClose.Enabled = false;
				btnSendReading.Enabled = false;
			   
				//Gets the instrument model number
				ioDmm.WriteString("*idn?",true);
				sId = ioDmm.ReadString();
    
				sTempId = sId.Split(',');

				txtInstrument.Text = sTempId[1];
				txtInstrument.Refresh();
				
				if( sTempId[1].Equals("34420A"))
				{
					Get34420ALearnString();
					btnClose.Enabled = true;
					btnSendReading.Enabled = true;
				}
				else if(sTempId[1].Equals("34401A"))
				{
					Get34401ALearnString();
					btnClose.Enabled = true;
					btnSendReading.Enabled = true;
				}
				else
				{
					MessageBox.Show("Please connect to a DMM(34401A or 34420A).", "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
					SetAccessForClosed();
				}

				txtData.Text = m_sLearnString;
			
			}
			catch(SystemException ex)
			{
				MessageBox.Show("Failed to retrieve default settings. ", "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
      		
		}

		private void btnSendReading_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (m_sLearnString.Length > 10)
				{
					
					//allow 30 seconds for RS232, because it is slow
					ioDmm.IO.Timeout = 10000;
					ioDmm.WriteString(m_sLearnString,true);
					ioDmm.IO.Timeout = 10000;
				}
				else
				{
					MessageBox.Show("Failed to retrieve default settings.", "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
				}
			}
			catch(SystemException ex)
			{
				MessageBox.Show("Error sending default settings. ", "StatRegSRQ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
    
		}

		/// <summary>
		/// Get the settings for 34420A
		/// to query instrument for its settings and  build a 'learn' string that can be sent to the
		/// instrument to recover these settings.
		/// </summary>
		private string Get34420ALearnString()
		{
			string sTString;
			string sUString;
			string sVString;
			string sWString = "";

			int iPosition;

			m_sLearnString = "";

			//get the configuration
			ioDmm.WriteString("conf?",true);
			sTString = ioDmm.ReadString();

			//clean the string
			sTString = sTString.Trim();
			sTString = sTString.Replace("\"","");

			m_sLearnString = m_sLearnString + "conf:" + sTString;

			//channel
			if(sTString.IndexOf("VOLT") != -1)
			{
				ioDmm.WriteString("rout:term?",true);
				sUString= ioDmm.ReadString();
				sUString.Trim();
				m_sLearnString = m_sLearnString + ";:rout:term " + sUString;
			}

			//auto ranging
			if(sTString.IndexOf("VOLT") != -1 || sTString.IndexOf("RES")!= -1)
			{
				iPosition = sTString.IndexOf(" "); //find the function
				sUString= sTString.Substring(0,iPosition);
        
				sUString= sUString + ":range:auto?"; //now ask if auto ranging on
				ioDmm.WriteString(sUString,true);
				sVString = ioDmm.ReadString();
				sVString = sVString.Trim(); //will hold 0 or 1 (auto=1)

				sUString= sUString.Substring(0,sWString.Length - 1); //get the range auto command
				sUString= sUString + " " + sVString; //makes the range auto command
				m_sLearnString = m_sLearnString + ";:sense:" + sUString; //add it to the current lrn string

			}
			
  
		    m_sLearnString = m_sLearnString +  "\r";
    

			//NPLC
			if( sTString.IndexOf("VOLT") != -1 ||  
				sTString.IndexOf("RES") != -1 ||			
				sTString.IndexOf("TEMP") != -1) 
			{
    
				iPosition = sTString.IndexOf(" "); //find the function
				sUString= sTString.Substring(0,iPosition);

				sUString= sUString + ":NPLC?"; //ask integration (power line cycles)
				ioDmm.WriteString(sUString,true);
				sVString= ioDmm.ReadString();
				sVString = sVString.Trim();

				sUString= sUString.Substring(0,sWString.Length - 1); //get the range auto command
				sUString= sUString + " " + sVString; //makes the range auto command
				sUString= sUString+ " " + sVString; //builds the command
				m_sLearnString = m_sLearnString + ";:sense:" + sUString; //add it to the current lrn string
			}
   
			m_sLearnString = m_sLearnString +  "\r";


			/*added for 34420 - temperature stuff
			temperature config
			thermistor requires only conf? above
			rtd requires rtd type(85 or 91) and 0 deg C res val (4.9 ohm to 2.1k ohm)
			tc requires tc type, ref junction type, and ref temp
			if rjun type fixed (-1 to 55 deg C)transducer type*/
			if(sTString.IndexOf("TEMP") != -1)
			{

				iPosition = sTString.IndexOf(" "); //find the function TEMP
				sUString= sTString.Substring(0,iPosition);

				//prepend temp units before conf command
				sWString= "unit:" + sUString + "?";
				ioDmm.WriteString(sWString,true); 
				sVString = ioDmm.ReadString();
				sVString = sVString.Trim();

				sWString= sWString.Substring(0,sWString.Length - 1); //rid of '?'
				sWString= sWString + " " + sVString; //builds the command
				m_sLearnString = sWString+ ";:" + m_sLearnString;

				sUString= sUString+ ":tran:";
				sWString= sUString + "type?"; //ask what type of transducer
				ioDmm.WriteString(sWString,true); 
				sVString = ioDmm.ReadString();
				sVString = sVString.Trim();
        
				//rtd (resistance temp device)
				if( sVString.IndexOf("FRTD") != -1)
				{
					//rtd resistance
					sUString= sUString+ sVString+ ":res?"; //ask rtd resistance
					ioDmm.WriteString(sUString,true);
					sVString = ioDmm.ReadString();
					sVString = sVString.Trim();

					
					sUString= sUString.Substring(0,sUString.Length - 1); //rid of '?'
					sWString= sWString.Substring(0,sVString.Length - 1);

					sUString= sUString + " " + sVString; //builds the command

					m_sLearnString = m_sLearnString + ";:sense:" + m_sLearnString;
					m_sLearnString = m_sLearnString + "\r";
				}
				else if(sVString.IndexOf("TC") != -1)
				{
					//thermocouple
        
					//tc ref junc type
					sUString= sUString + "tc:rjun";
					sWString= sUString + ":type?" ; //ask tc ref junc type
					ioDmm.WriteString(sWString,true); 
					sVString = ioDmm.ReadString();
					sVString = sVString.Trim();
					

					sWString = sWString.Substring(0,sWString.Length - 1); //rid of '?'
					sWString = sWString + " " + sVString; //builds the command
					m_sLearnString = m_sLearnString + ";:sense:" + sWString; //add it to the current lrn string

					//tc ref junc temp if type is FIXED (must be in range -1 C to 55 C)
					if( sVString.IndexOf("FIX") != -1)
					{
						sUString= sUString + "?"; //now ask for ref junc temp

						ioDmm.WriteString(sUString,true);
						sVString = ioDmm.ReadString();
						sVString = sVString.Trim();
						
						sUString= sUString.Substring(0,sUString.Length - 1); //rid of '?'
						sUString= sUString + " " + sVString; //builds the command

						m_sLearnString = m_sLearnString + ";:sense:" + sUString; //add it to the current lrn string"
					}
				}
			}

        
			m_sLearnString = m_sLearnString + "\r";

			//calc func
			ioDmm.WriteString("calc:func?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:calc:func " + sUString;

			//calc state
			ioDmm.WriteString("calc:state?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:calc:state " + sUString;
    

			//data feed
			ioDmm.WriteString("data:feed? rdg_store",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:data:feed rdg_store," + sUString;
    

			//trig source
			ioDmm.WriteString("trig:sour?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:trig:sour " + sUString;
    
    
    
			m_sLearnString = m_sLearnString + "\r";

			//trig delay
			ioDmm.WriteString("trig:delay?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:trig:delay " + sUString;
    

			//trig delay auto
			ioDmm.WriteString("trig:delay:auto?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:trig:delay:auto " + sUString;
    

			//sample count
			ioDmm.WriteString("sample:count?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:sample:count " + sUString;
    

			//trig count
			ioDmm.WriteString("trig:count?",true);
			sUString= ioDmm.ReadString();
			sUString = sUString.Trim();
			m_sLearnString = m_sLearnString + ";:trig:count " + sUString;
    
			return "";
		}

		/// <summary>
		/// Get settings for 34401A
		/// Description:    Module to query instrument for its settings and
		///                 build a 'learn' string that can be sent to the
		///                 instrument to recover these settings.
		/// </summary>
		private string Get34401ALearnString()
		{
			
			string sConfig = ""; 
			string sFunction = "";
			string sReply = "";
			
    
			int iPosition;

			m_sLearnString = "";

			//get the configuration
			ioDmm.WriteString("conf?",true);
			sConfig = ioDmm.ReadString();

			//clean the string
			sConfig = sConfig.Trim();
			sConfig = sConfig.Replace("\"","");

			m_sLearnString = m_sLearnString + "conf:" + sConfig;


			
			//auto ranging
			sFunction = "";
			sReply = "";
			if( sConfig.IndexOf("VOLT") != -1 || sConfig.IndexOf("CURR") != -1 || 
                sConfig.IndexOf("RES") != -1 || 	sConfig.IndexOf("FREQ") != -1 || 
				sConfig.IndexOf("PER") != -1)
				{
				if(sConfig.IndexOf("VOLT:RAT") == -1)
				{
					iPosition = sConfig.IndexOf(" ");
					if(iPosition > -1)
					{
						sFunction = sConfig.Substring(0,iPosition);
					}
					if(sFunction.IndexOf("FREQ") != -1 ||  sFunction.IndexOf("PER") != -1)
					{
						sFunction = sFunction + ":VOLT"; 
					}
					sFunction = sFunction + ":range:auto?";

					//query if auto ranging is on
					ioDmm.WriteString(sFunction,true);
					sReply = ioDmm.ReadString();	//will hold 0 or 1
					sReply = sReply.Trim();
    
					sFunction = sFunction.Substring(0,sFunction.Length - 1); //get the range auto command
					sFunction = sFunction + " " + sReply;	//makes the range auto command
					m_sLearnString = m_sLearnString + ";:sense:" + sFunction;	//add it to the current lrn string
      
				}

			  }

			//NLPC
			sFunction = "";
			sReply = "";
			if( sConfig.IndexOf("VOLT:DC") != -1 || 
				(sConfig.IndexOf("CURR") != -1 && sConfig.IndexOf("AC") == -1) ||
				sConfig.IndexOf("RES") != -1)
			{
				
					iPosition = sConfig.IndexOf(" ");
					if(iPosition > 0)
					{
						sFunction = sConfig.Substring(0,iPosition);
					}
				
					//query for NPLC
					sFunction = sFunction + ":NPLC?";
					ioDmm.WriteString(sFunction,true);
					sReply = ioDmm.ReadString();
					sReply = sReply.Trim();
        
					//create the learn string					
					sFunction = sFunction.Substring(0,sFunction.Length - 1); //rid of '?' & get the command
					sFunction = sFunction + " " + sReply;	//makes the range auto command
					m_sLearnString = m_sLearnString + ";:sense:" + sFunction;	//add it to the current lrn string
      
				
			}

			// add a LF to break up the string and make RS232 more reliable
			m_sLearnString = m_sLearnString + "\r";
		
			//aperture
			sFunction = "";
			sReply = "";
			if( sConfig.IndexOf("FREQ") != -1 || sConfig.IndexOf("RES") != -1)
			{
				
				iPosition = sConfig.IndexOf(" ");
				if(iPosition > 0)
				{
					sFunction = sConfig.Substring(0,iPosition);
				}
				
				//query for aper
				sFunction = sFunction + ":aper?";
				ioDmm.WriteString(sFunction,true);
				sReply = ioDmm.ReadString();
				sReply = sReply.Trim();
        
				//create the learn string					
				sFunction = sFunction.Substring(0,sFunction.Length - 1); //rid of '?' & get the command
				sFunction = sFunction + " " + sReply;	//makes the range auto command
				m_sLearnString = m_sLearnString + ";:sense:" + sFunction;	//add it to the current lrn string
      
				
			}
    
			//det:bandwidth
			ioDmm.WriteString("det:band?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:sense:det:band " + sReply;	//add it to the current lrn string
     

		
			//auto zero
			ioDmm.WriteString("zero:auto?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:sense:zero:auto " + sReply;	//add it to the current lrn string
			


			// add a LF to break up the string and make RS232 more reliable
			m_sLearnString = m_sLearnString + "\r";
    
			//impedence
			ioDmm.WriteString("inp:imp:auto?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:inp:imp:auto " + sReply;	//add it to the current lrn string
			

			//calc func
			ioDmm.WriteString("calc:func?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:func " + sReply;	//add it to the current lrn string
			

			//calc state
			ioDmm.WriteString("calc:state?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:state " + sReply;	//add it to the current lrn string
			
		    
			//add a LF to break up the string and make RS232 more reliable
		    m_sLearnString = m_sLearnString +  "\r";

			//calc offset
			ioDmm.WriteString("calc:null:offset?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:null:offset " + sReply;	//add it to the current lrn string
			

		    //db ref
			ioDmm.WriteString("calc:db:ref?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:db:ref " + sReply;	//add it to the current lrn string
			

			// add a LF to break up the string and make RS232 more reliable
			m_sLearnString   = m_sLearnString + "\r";


			//dbm ref
			ioDmm.WriteString("calc:dbm:ref?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:dbm:ref " + sReply;	//add it to the current lrn string
		

			//limit lower
			ioDmm.WriteString("calc:limit:lower?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:limit:lower " + sReply;	//add it to the current lrn string
    

			// add a LF to break up the string and make RS232 more reliable
			m_sLearnString = m_sLearnString + "\r";

			//lim upper
			ioDmm.WriteString("calc:lim:upper?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:calc:lim:upper " + sReply;	//add it to the current lrn string
    
			//data feed
			ioDmm.WriteString("data:feed?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:data:feed rdg_store," + sReply;	//add it to the current lrn string
			

			//add a LF to break up the string and make RS232 more reliable
			m_sLearnString = m_sLearnString + "\r";


			//trig source
			ioDmm.WriteString("trig:sour?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:trig:sour " + sReply;	//add it to the current lrn string
    

    
			//trig delay
			ioDmm.WriteString("trig:delay?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:trig:delay " + sReply;	//add it to the current lrn string
    
    
			// add a LF to break up the string and make RS232 more reliable
			m_sLearnString = m_sLearnString +  "\r";

			//trig delay auto
			ioDmm.WriteString("trig:delay:auto?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:trig:delay:auto " + sReply;	//add it to the current lrn string
    

			//sample count
			ioDmm.WriteString("sample:count?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:sample:count " + sReply;	//add it to the current lrn string
		



			//trig count
			ioDmm.WriteString("trig:count?",true);
			sReply = ioDmm.ReadString();
			sReply = sReply.Trim();

			m_sLearnString = m_sLearnString + ";:trig:count " + sReply;	//add it to the current lrn string
		
			return "";
		}
	
	}
}
