Option Explicit On 
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 2002-2004 Agilent Technologies Inc.  All rights reserved.
''
'' You have a royalty-free right to use, modify, reproduce and distribute
'' the Sample Application Files (and/or any modified version) in any way
'' you find useful, provided that you agree that Agilent Technologies has no
'' warranty,  obligations or liability for any Sample Application Files.
''
'' Agilent Technologies provides programming examples for illustration only,
'' This sample program assumes that you are familiar with the programming
'' language being demonstrated and the tools used to create and debug
'' procedures. Agilent Technologies support engineers can help explain the
'' functionality of Agilent Technologies software components and associated
'' commands, but they will not modify these samples to provide added
'' functionality or construct procedures to meet your specific needs.
'' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""


'************************************************************************
' Note;
'   To use RS232, you must first set the instrument to
'   remote with this command:
'   DMM.WriteString "Syst:Rem"
'************************************************************************

Public Class frmEZSample
    Inherits System.Windows.Forms.Form
    Private ioDmm As Ivi.Visa.Interop.FormattedIO488



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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTake3DC As System.Windows.Forms.Button
    Friend WithEvents btnTake1AC As System.Windows.Forms.Button
    Friend WithEvents lblReading3 As System.Windows.Forms.Label
    Friend WithEvents lblReading2 As System.Windows.Forms.Label
    Friend WithEvents lblReading1 As System.Windows.Forms.Label
    Friend WithEvents txtReading3 As System.Windows.Forms.TextBox
    Friend WithEvents txtReading2 As System.Windows.Forms.TextBox
    Friend WithEvents txtReading1 As System.Windows.Forms.TextBox
    Friend WithEvents btnClearDisplay As System.Windows.Forms.Button
    Friend WithEvents btnToDisplay As System.Windows.Forms.Button
    Friend WithEvents btnGetRev As System.Windows.Forms.Button
    Friend WithEvents btnGetID As System.Windows.Forms.Button
    Friend WithEvents btnInitIO As System.Windows.Forms.Button
    Friend WithEvents lblToDisplay As System.Windows.Forms.Label
    Friend WithEvents lblRevision As System.Windows.Forms.Label
    Friend WithEvents lblIDString As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtToDisplay As System.Windows.Forms.TextBox
    Friend WithEvents txtRevision As System.Windows.Forms.TextBox
    Friend WithEvents txtIDString As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmEZSample))
        Me.btnClose = New System.Windows.Forms.Button
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.btnTake3DC = New System.Windows.Forms.Button
        Me.btnTake1AC = New System.Windows.Forms.Button
        Me.lblReading3 = New System.Windows.Forms.Label
        Me.lblReading2 = New System.Windows.Forms.Label
        Me.lblReading1 = New System.Windows.Forms.Label
        Me.txtReading3 = New System.Windows.Forms.TextBox
        Me.txtReading2 = New System.Windows.Forms.TextBox
        Me.txtReading1 = New System.Windows.Forms.TextBox
        Me.btnClearDisplay = New System.Windows.Forms.Button
        Me.btnToDisplay = New System.Windows.Forms.Button
        Me.btnGetRev = New System.Windows.Forms.Button
        Me.btnGetID = New System.Windows.Forms.Button
        Me.btnInitIO = New System.Windows.Forms.Button
        Me.lblToDisplay = New System.Windows.Forms.Label
        Me.lblRevision = New System.Windows.Forms.Label
        Me.lblIDString = New System.Windows.Forms.Label
        Me.lblAddress = New System.Windows.Forms.Label
        Me.txtToDisplay = New System.Windows.Forms.TextBox
        Me.txtRevision = New System.Windows.Forms.TextBox
        Me.txtIDString = New System.Windows.Forms.TextBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(328, 40)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 20)
        Me.btnClose.TabIndex = 29
        Me.btnClose.Text = "Close IO"
        Me.ToolTip1.SetToolTip(Me.btnClose, "Click to close the IO enviornment")
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.btnTake3DC)
        Me.groupBox1.Controls.Add(Me.btnTake1AC)
        Me.groupBox1.Controls.Add(Me.lblReading3)
        Me.groupBox1.Controls.Add(Me.lblReading2)
        Me.groupBox1.Controls.Add(Me.lblReading1)
        Me.groupBox1.Controls.Add(Me.txtReading3)
        Me.groupBox1.Controls.Add(Me.txtReading2)
        Me.groupBox1.Controls.Add(Me.txtReading1)
        Me.groupBox1.Location = New System.Drawing.Point(16, 192)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(416, 120)
        Me.groupBox1.TabIndex = 28
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Measurements"
        '
        'btnTake3DC
        '
        Me.btnTake3DC.Location = New System.Drawing.Point(240, 56)
        Me.btnTake3DC.Name = "btnTake3DC"
        Me.btnTake3DC.Size = New System.Drawing.Size(160, 20)
        Me.btnTake3DC.TabIndex = 14
        Me.btnTake3DC.Text = "Take Three Readings (DC)"
        Me.ToolTip1.SetToolTip(Me.btnTake3DC, "Click to take three DC Voltage readings")
        '
        'btnTake1AC
        '
        Me.btnTake1AC.Location = New System.Drawing.Point(240, 24)
        Me.btnTake1AC.Name = "btnTake1AC"
        Me.btnTake1AC.Size = New System.Drawing.Size(160, 20)
        Me.btnTake1AC.TabIndex = 13
        Me.btnTake1AC.Text = "Take One Reading (AC)"
        Me.ToolTip1.SetToolTip(Me.btnTake1AC, "Click to take one AC Voltage reading")
        '
        'lblReading3
        '
        Me.lblReading3.Location = New System.Drawing.Point(16, 88)
        Me.lblReading3.Name = "lblReading3"
        Me.lblReading3.Size = New System.Drawing.Size(64, 16)
        Me.lblReading3.TabIndex = 12
        Me.lblReading3.Text = "Reading 3"
        Me.lblReading3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblReading2
        '
        Me.lblReading2.Location = New System.Drawing.Point(16, 56)
        Me.lblReading2.Name = "lblReading2"
        Me.lblReading2.Size = New System.Drawing.Size(64, 16)
        Me.lblReading2.TabIndex = 11
        Me.lblReading2.Text = "Reading 2"
        Me.lblReading2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblReading1
        '
        Me.lblReading1.Location = New System.Drawing.Point(16, 24)
        Me.lblReading1.Name = "lblReading1"
        Me.lblReading1.Size = New System.Drawing.Size(64, 16)
        Me.lblReading1.TabIndex = 10
        Me.lblReading1.Text = "Reading 1"
        Me.lblReading1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtReading3
        '
        Me.txtReading3.Location = New System.Drawing.Point(88, 88)
        Me.txtReading3.Name = "txtReading3"
        Me.txtReading3.Size = New System.Drawing.Size(128, 20)
        Me.txtReading3.TabIndex = 9
        Me.txtReading3.Text = ""
        '
        'txtReading2
        '
        Me.txtReading2.Location = New System.Drawing.Point(88, 56)
        Me.txtReading2.Name = "txtReading2"
        Me.txtReading2.Size = New System.Drawing.Size(128, 20)
        Me.txtReading2.TabIndex = 8
        Me.txtReading2.Text = ""
        '
        'txtReading1
        '
        Me.txtReading1.Location = New System.Drawing.Point(88, 24)
        Me.txtReading1.Name = "txtReading1"
        Me.txtReading1.Size = New System.Drawing.Size(128, 20)
        Me.txtReading1.TabIndex = 7
        Me.txtReading1.Text = ""
        '
        'btnClearDisplay
        '
        Me.btnClearDisplay.Location = New System.Drawing.Point(328, 160)
        Me.btnClearDisplay.Name = "btnClearDisplay"
        Me.btnClearDisplay.Size = New System.Drawing.Size(104, 20)
        Me.btnClearDisplay.TabIndex = 27
        Me.btnClearDisplay.Text = "Clear Display"
        Me.ToolTip1.SetToolTip(Me.btnClearDisplay, "Click to clear the instrument display")
        '
        'btnToDisplay
        '
        Me.btnToDisplay.Location = New System.Drawing.Point(328, 136)
        Me.btnToDisplay.Name = "btnToDisplay"
        Me.btnToDisplay.Size = New System.Drawing.Size(104, 20)
        Me.btnToDisplay.TabIndex = 26
        Me.btnToDisplay.Text = "Send to Display"
        Me.ToolTip1.SetToolTip(Me.btnToDisplay, "Click to send the display string to the instrument")
        '
        'btnGetRev
        '
        Me.btnGetRev.Location = New System.Drawing.Point(328, 104)
        Me.btnGetRev.Name = "btnGetRev"
        Me.btnGetRev.Size = New System.Drawing.Size(104, 20)
        Me.btnGetRev.TabIndex = 25
        Me.btnGetRev.Text = "Get Revision"
        Me.ToolTip1.SetToolTip(Me.btnGetRev, "Click to get the SCPI version for which the instrument complies")
        '
        'btnGetID
        '
        Me.btnGetID.Location = New System.Drawing.Point(328, 72)
        Me.btnGetID.Name = "btnGetID"
        Me.btnGetID.Size = New System.Drawing.Size(104, 20)
        Me.btnGetID.TabIndex = 24
        Me.btnGetID.Text = "Get ID String"
        Me.ToolTip1.SetToolTip(Me.btnGetID, "Click to get the ID of the instrument")
        '
        'btnInitIO
        '
        Me.btnInitIO.Location = New System.Drawing.Point(328, 16)
        Me.btnInitIO.Name = "btnInitIO"
        Me.btnInitIO.Size = New System.Drawing.Size(104, 20)
        Me.btnInitIO.TabIndex = 23
        Me.btnInitIO.Text = "Initialize IO"
        Me.ToolTip1.SetToolTip(Me.btnInitIO, "Click to initialize the IO enviornment")
        '
        'lblToDisplay
        '
        Me.lblToDisplay.Location = New System.Drawing.Point(8, 136)
        Me.lblToDisplay.Name = "lblToDisplay"
        Me.lblToDisplay.Size = New System.Drawing.Size(88, 16)
        Me.lblToDisplay.TabIndex = 22
        Me.lblToDisplay.Text = "Display String"
        Me.lblToDisplay.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRevision
        '
        Me.lblRevision.Location = New System.Drawing.Point(8, 104)
        Me.lblRevision.Name = "lblRevision"
        Me.lblRevision.Size = New System.Drawing.Size(88, 16)
        Me.lblRevision.TabIndex = 21
        Me.lblRevision.Text = "Revision"
        Me.lblRevision.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblIDString
        '
        Me.lblIDString.Location = New System.Drawing.Point(8, 72)
        Me.lblIDString.Name = "lblIDString"
        Me.lblIDString.Size = New System.Drawing.Size(88, 16)
        Me.lblIDString.TabIndex = 20
        Me.lblIDString.Text = "ID String"
        Me.lblIDString.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddress
        '
        Me.lblAddress.Location = New System.Drawing.Point(8, 24)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(88, 16)
        Me.lblAddress.TabIndex = 19
        Me.lblAddress.Text = "Address"
        Me.lblAddress.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtToDisplay
        '
        Me.txtToDisplay.Location = New System.Drawing.Point(104, 136)
        Me.txtToDisplay.Name = "txtToDisplay"
        Me.txtToDisplay.Size = New System.Drawing.Size(208, 20)
        Me.txtToDisplay.TabIndex = 18
        Me.txtToDisplay.Text = "34401A EZ Sample"
        '
        'txtRevision
        '
        Me.txtRevision.Location = New System.Drawing.Point(104, 104)
        Me.txtRevision.Name = "txtRevision"
        Me.txtRevision.ReadOnly = True
        Me.txtRevision.Size = New System.Drawing.Size(208, 20)
        Me.txtRevision.TabIndex = 17
        Me.txtRevision.Text = ""
        '
        'txtIDString
        '
        Me.txtIDString.Location = New System.Drawing.Point(104, 72)
        Me.txtIDString.Name = "txtIDString"
        Me.txtIDString.ReadOnly = True
        Me.txtIDString.Size = New System.Drawing.Size(208, 20)
        Me.txtIDString.TabIndex = 16
        Me.txtIDString.Text = ""
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(104, 16)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(208, 20)
        Me.txtAddress.TabIndex = 15
        Me.txtAddress.Text = "GPIB::22"
        '
        'frmEZSample
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(448, 325)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.btnClearDisplay)
        Me.Controls.Add(Me.btnToDisplay)
        Me.Controls.Add(Me.btnGetRev)
        Me.Controls.Add(Me.btnGetID)
        Me.Controls.Add(Me.btnInitIO)
        Me.Controls.Add(Me.lblToDisplay)
        Me.Controls.Add(Me.lblRevision)
        Me.Controls.Add(Me.lblIDString)
        Me.Controls.Add(Me.lblAddress)
        Me.Controls.Add(Me.txtToDisplay)
        Me.Controls.Add(Me.txtRevision)
        Me.Controls.Add(Me.txtIDString)
        Me.Controls.Add(Me.txtAddress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEZSample"
        Me.Text = "Agilent 34401A EZSample"
        Me.groupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnInitIO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInitIO.Click

        ' set the I/O address to the text box in case the user changed it.
        ' bring up the input dialog and save any changes to the text box
        Dim mgr As Ivi.Visa.Interop.ResourceManager
        Dim ioAddress As String
        On Error GoTo ioError

        ioAddress = txtAddress.Text

        txtAddress.Text = ioAddress
        mgr = New Ivi.Visa.Interop.ResourceManager
        ioDmm = New Ivi.Visa.Interop.FormattedIO488
        ioDmm.IO() = mgr.Open(ioAddress)
        EnableControl(True)

        Exit Sub
ioError:
        MsgBox("InitIO Error:" & vbCrLf & Err.Description)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ioDmm.IO.Close()
        EnableControl(False)
    End Sub

    Private Sub btnGetID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetID.Click

        Dim result As String
        On Error GoTo ioError

        ioDmm.WriteString("*idn?")
        result = ioDmm.ReadString

        txtIDString.Text = result

        Exit Sub
ioError:
        MsgBox("GetID Error:" & vbCrLf & Err.Description)
    End Sub
    Private Sub btnGetRev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetRev.Click

        ' Gets the hardware revision from the instrument
        Dim result As String

        On Error GoTo ioError

        ioDmm.WriteString(":Syst:Vers?")
        result = ioDmm.ReadString

        txtRevision.Text = result

        Exit Sub
ioError:
        MsgBox("GetRevision Error:" & vbCrLf & Err.Description)
    End Sub

    Private Sub btnToDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToDisplay.Click
        ' Send a message to the multimeters display,
        ' and generate a beep

        On Error GoTo ioError

        ioDmm.WriteString(":syst:beep;:disp:text " & "'" & txtToDisplay.Text & "'")

        Exit Sub
ioError:
        MsgBox("Display Error:" & vbCrLf & Err.Description)
    End Sub

    Private Sub btnClearDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearDisplay.Click
        On Error GoTo ioError

        ' Clear the display
        ioDmm.WriteString("Display:text:Clear")

        Exit Sub
ioError:
        MsgBox("Clear Display Error:" & vbCrLf & Err.Description)
    End Sub

    Private Sub btnTake1AC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTake1AC.Click

        ' Set the multimeter for ac voltage reading,
        ' Use default values
        ' Get the reading and put it in first text box
        Dim Reading As Double

        On Error GoTo ioError


        '""""""""""""""""""""""""""""""""""""
        '   Include this line for RS232 only
        '   ioDmm.WriteString("Syst:Rem")


        ioDmm.WriteString("Measure:Voltage:AC?")
        Reading = ioDmm.ReadNumber

        txtReading1.Text = Reading
        txtReading2.Text = ""
        txtReading3.Text = ""
        
        Exit Sub
ioError:
        MsgBox("Error Getting Reading:" & vbCrLf & Err.Description)
    End Sub

    Private Sub btnTake3DC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTake3DC.Click

        ' Configure the multimeter for dc voltage readings,
        ' 10V range, 0.1V resolution, and 3 readings
        Dim Readings()
        Dim strtemp As String

        On Error GoTo ioError
        

        '""""""""""""""""""""""""""""""""""""
        '   Include this line for RS232 only
        '   ioDmm.WriteString "Syst:Rem"

        With ioDmm
            .WriteString(":CONF:VOLT:DC 10, 0.1")
            .WriteString("SAMP:COUN 3")
            ' for RS232 only, a delay may be needed before the Read
            'delay(200)
            .WriteString("Read?")
        End With
        Readings = ioDmm.ReadList

        txtReading1.Text = Readings(0)
        txtReading2.Text = Readings(1)
        txtReading3.Text = Readings(2)

        Exit Sub
ioError:
        MsgBox("Error Getting Readings:" & vbCrLf & Err.Description)
    End Sub
    Private Sub EnableControl(ByVal bStatus As Boolean)


        txtIDString.Text = ""
        txtRevision.Text = ""
        txtReading1.Text = ""
        txtReading2.Text = ""
        txtReading3.Text = ""


        btnInitIO.Enabled = Not bStatus
        txtAddress.Enabled = Not bStatus
        txtIDString.Enabled = bStatus
        txtRevision.Enabled = bStatus
        txtReading1.Enabled = bStatus
        txtReading2.Enabled = bStatus
        txtReading3.Enabled = bStatus
        txtToDisplay.Enabled = bStatus
        btnClearDisplay.Enabled = bStatus
        btnClose.Enabled = bStatus
        btnGetID.Enabled = bStatus
        btnGetRev.Enabled = bStatus
        btnTake1AC.Enabled = bStatus
        btnTake3DC.Enabled = bStatus
        btnToDisplay.Enabled = bStatus
    End Sub

    Private Sub frmEZSample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnableControl(False)
    End Sub
End Class
