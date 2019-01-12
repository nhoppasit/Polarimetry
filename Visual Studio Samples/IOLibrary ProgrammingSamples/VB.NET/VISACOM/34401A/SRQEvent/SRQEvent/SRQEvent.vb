'''"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright (c) 2002-2004 Agilent Technologies Inc.  All rights reserved.
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
'
'*************************************************************
' The following example shows how you can use the multimeter's status
' registers to determine when a command sequence is completed. For
' more information see "The SCPI Status Model" in the Agilent 34401A
' User Guide
''##########################################################################
' NOTE: This Example uses the IEventHandler
'       This will only work with GPIB
'##########################################################################
'
' Sequence of Operation;
'   1. During the Set IO the IO address
'      and the IEventHandler is set to fire the Event 'HandleEvent'
'   2. The meter is cleared and set to give an SRQ when its
'      operation is complete
'   3. The meter is set for dc, and multiple readings. This will
'      take about 3 seconds for 10 readings
'   4. We start the reading with INIT. This will put the
'      data into memory.  When the meter is finished, it
'      will set SRQ.
'   5. When HandleEvent event fires, then get the reading from the
'      meter with the routine ReadData.
'
'*******************************************************************
Public Class SRQEvent
    Inherits System.Windows.Forms.Form
    Implements VisaComLib.IEventHandler
    Private ioDmm As VisaComLib.FormattedIO488
    Private m_iNumOfReadings As Integer

    '' Use this to enable the event handler
    Dim SRQ As VisaComLib.IEventManager

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents grouppBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents btnInitIO As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnStartReading As System.Windows.Forms.Button
    Friend WithEvents txtData As System.Windows.Forms.TextBox
    Friend WithEvents lblData As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SRQEvent))
        Me.grouppBox1 = New System.Windows.Forms.GroupBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.lblAddress = New System.Windows.Forms.Label
        Me.btnInitIO = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.btnStartReading = New System.Windows.Forms.Button
        Me.txtData = New System.Windows.Forms.TextBox
        Me.lblData = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grouppBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grouppBox1
        '
        Me.grouppBox1.Controls.Add(Me.txtAddress)
        Me.grouppBox1.Controls.Add(Me.lblAddress)
        Me.grouppBox1.Controls.Add(Me.btnInitIO)
        Me.grouppBox1.Controls.Add(Me.btnClose)
        Me.grouppBox1.Location = New System.Drawing.Point(3, 3)
        Me.grouppBox1.Name = "grouppBox1"
        Me.grouppBox1.Size = New System.Drawing.Size(408, 88)
        Me.grouppBox1.TabIndex = 23
        Me.grouppBox1.TabStop = False
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(72, 24)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(208, 20)
        Me.txtAddress.TabIndex = 0
        Me.txtAddress.Text = "GPIB::22"
        '
        'lblAddress
        '
        Me.lblAddress.Location = New System.Drawing.Point(16, 28)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 16)
        Me.lblAddress.TabIndex = 21
        Me.lblAddress.Text = "Address"
        Me.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnInitIO
        '
        Me.btnInitIO.AccessibleDescription = ""
        Me.btnInitIO.CausesValidation = False
        Me.btnInitIO.Location = New System.Drawing.Point(288, 24)
        Me.btnInitIO.Name = "btnInitIO"
        Me.btnInitIO.Size = New System.Drawing.Size(104, 23)
        Me.btnInitIO.TabIndex = 1
        Me.btnInitIO.Tag = ""
        Me.btnInitIO.Text = "Initialize IO"
        Me.ToolTip1.SetToolTip(Me.btnInitIO, "Click to initialize the IO enviornment")
        '
        'btnClose
        '
        Me.btnClose.Enabled = False
        Me.btnClose.Location = New System.Drawing.Point(288, 56)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close IO"
        Me.ToolTip1.SetToolTip(Me.btnClose, "Click to close the IO enviornment")
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.btnStartReading)
        Me.groupBox2.Controls.Add(Me.txtData)
        Me.groupBox2.Controls.Add(Me.lblData)
        Me.groupBox2.Enabled = False
        Me.groupBox2.Location = New System.Drawing.Point(3, 91)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(408, 112)
        Me.groupBox2.TabIndex = 24
        Me.groupBox2.TabStop = False
        '
        'btnStartReading
        '
        Me.btnStartReading.Location = New System.Drawing.Point(288, 48)
        Me.btnStartReading.Name = "btnStartReading"
        Me.btnStartReading.Size = New System.Drawing.Size(104, 23)
        Me.btnStartReading.TabIndex = 3
        Me.btnStartReading.Text = "Start Readings"
        Me.ToolTip1.SetToolTip(Me.btnStartReading, "Click the get the readings from the instrument")
        '
        'txtData
        '
        Me.txtData.Location = New System.Drawing.Point(72, 15)
        Me.txtData.Multiline = True
        Me.txtData.Name = "txtData"
        Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtData.Size = New System.Drawing.Size(208, 88)
        Me.txtData.TabIndex = 5
        Me.txtData.Text = ""
        '
        'lblData
        '
        Me.lblData.Location = New System.Drawing.Point(16, 48)
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New System.Drawing.Size(48, 16)
        Me.lblData.TabIndex = 0
        Me.lblData.Text = "Data"
        '
        'SRQEvent
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(415, 207)
        Me.Controls.Add(Me.grouppBox1)
        Me.Controls.Add(Me.groupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "SRQEvent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agilent 34401A SRQEvent"
        Me.grouppBox1.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnInitIO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInitIO.Click

        Dim rm As VisaComLib.ResourceManager
        On Error GoTo myHandler


        rm = New VisaComLib.ResourceManager
        ioDmm = New VisaComLib.FormattedIO488Class

        'Check the valid GPIB card and address, ensure that there is no errors while
        'opening the instrument. 
        'Open port and initialize Formatted IO
        ioDmm.IO = rm.Open(txtAddress.Text, VisaComLib.AccessMode.NO_LOCK, 100, "")


        '""""""""""""""""""""""""""""""""""""""""""""""""""
        ' set SRQ to the session and
        ' Enable the SRQ
        SRQ = ioDmm.IO
        SRQ.InstallHandler(VisaComLib.EventType.EVENT_SERVICE_REQ, Me)
        SRQ.EnableEvent(VisaComLib.EventType.EVENT_SERVICE_REQ, VisaComLib.EventMechanism.EVENT_HNDLR)

        EnableControls()
        Me.txtData.Text = ""

        Exit Sub

myHandler:
        DisableControls()
        MessageBox.Show("Open failed on " + txtAddress.Text + " ", "SRQEvent", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub

    Private Sub SRQEvent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_iNumOfReadings = 0
        DisableControls()
    End Sub
    Private Sub EnableControls()
        txtData.Text = ""
        groupBox2.Enabled = True
        Me.btnClose.Enabled = True
        Me.btnInitIO.Enabled = False
        Me.btnStartReading.Enabled = True
    End Sub

    Private Sub DisableControls()
        Me.txtData.Text = ""
        groupBox2.Enabled = False
        Me.btnClose.Enabled = False
        Me.btnInitIO.Enabled = True
   End Sub

   Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
      m_iNumOfReadings = 0
      ioDmm.IO.Close()
      DisableControls()
   End Sub

   Private Sub btnStartReading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartReading.Click

      btnStartReading.Enabled = False
      StartReading()

   End Sub
   Private Sub StartReading()

      On Error GoTo myHandler
      Dim strTemp As String

      ' Use this to enable the event handler
      Dim SRQ As VisaComLib.IEventManager

      Me.btnStartReading.Enabled = False

      'Clear out text box for the data so we can see when new data arrives
      txtData.Text = ""

      'Setup dmm to return an event when readings are complete 
      ioDmm.WriteString("*RST", True) '          // Reset dmm
      ioDmm.WriteString("*CLS", True) '          // Clear dmm status registers
      ioDmm.WriteString("*ESE 1", True) '        // Enable 'operation complete bit to

      'set 'standard event' bit in status byte
      ioDmm.WriteString("*SRE 32", True) '       // Enable 'standard event' bit in status

      ' byte to pull the IEEE-488 SRQ line
      ioDmm.WriteString("*OPC?", True) ';         // Assure syncronization
      strTemp = ioDmm.ReadString() ';

      ' Configure the meter to take readings
      ' and initiate the readings (source is set to immediate by default)
      m_iNumOfReadings = 10

      ioDmm.WriteString("Configure:Voltage:dc 10", True)  '  set dmm to 10 volt dc range
      ioDmm.WriteString("Voltage:DC:NPLC 10", True)       '  set integration time to 10 Power line cycles (PLC)"
      ioDmm.WriteString("Trigger:count " + m_iNumOfReadings.ToString(), True)     'set dmm to accept multiple triggers
      ioDmm.WriteString("Init", True)     'Place dmm in 'wait-for-trigger' state
      ioDmm.WriteString("*OPC", True)     'Set 'operation complete' bit in standard   
      'event registers when measurement is complete


      'give message that meter is initialized
      txtData.Text = "Meter configured and " + vbCrLf + "Initialized " + DateTime.Now

      Exit Sub
myHandler:
      Me.btnStartReading.Enabled = True
      MessageBox.Show("Error configuring the meter. " + txtAddress.Text + " " + Err.Source, "SRQEvent", MessageBoxButtons.OK, MessageBoxIcon.Error)

   End Sub
   Private Sub ReadData()

      '// Once the Event is detected, this routine will get the data from the meter
      Dim readings() As Object
      Dim sTemp As String
      On Error GoTo myHandler
      ' Once the SRQ is detected, this routine will
      ' get the data from the meter
      ' Called by: PollForSRQTimer_Timer
      Dim i As Long

      If (m_iNumOfReadings > 0) Then
         With ioDmm
            .WriteString("Fetch?")        ' Query for the data in memory
            readings = .ReadList           ' get the data and parse into the array
         End With

         ' Insert data into text box
         sTemp = ""
         sTemp = txtData.Text
         For i = 0 To readings.Length - 1
            sTemp = sTemp + readings(i).ToString() & "Vdc" & vbCrLf
         Next i
         txtData.Text = ""
         txtData.Text = sTemp
      Else
         txtData.Text = ""
      End If

      Exit Sub
myHandler:
      Me.btnStartReading.Enabled = True
      MsgBox("Error Reading Data.")
   End Sub

   ' Note; on version 1.0 of VISA COM change the argument name event to avoid using a key word
   ' In this example the argument 'event' was changed to 'SRQevent'

   Public Sub HandleEvent(ByVal vi As VisaComLib.IEventManager, ByVal [event] As VisaComLib.IEvent, ByVal userHandle As Integer) Implements VisaComLib.IEventHandler.HandleEvent
      Dim sTemp As String
      sTemp = txtData.Text
      txtData.Text = sTemp & vbCrLf & "SRQ fired, getting data " & System.DateTime.Now & vbCrLf
      ReadData()
      btnStartReading.Enabled = True
   End Sub
End Class
