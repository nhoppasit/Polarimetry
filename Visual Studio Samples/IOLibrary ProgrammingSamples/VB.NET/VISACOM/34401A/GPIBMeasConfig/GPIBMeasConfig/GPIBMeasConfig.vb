''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
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
Public Class frmGPIBMeasConfig
    Inherits System.Windows.Forms.Form
    Private iodmm As Ivi.Visa.Interop.IFormattedIO488

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
    Friend WithEvents btnConfigure As System.Windows.Forms.Button
    Friend WithEvents btnMeasure As System.Windows.Forms.Button
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmGPIBMeasConfig))
        Me.grouppBox1 = New System.Windows.Forms.GroupBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.lblAddress = New System.Windows.Forms.Label
        Me.btnInitIO = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.btnConfigure = New System.Windows.Forms.Button
        Me.btnMeasure = New System.Windows.Forms.Button
        Me.txtResult = New System.Windows.Forms.TextBox
        Me.lblResult = New System.Windows.Forms.Label
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
        Me.grouppBox1.Location = New System.Drawing.Point(4, 0)
        Me.grouppBox1.Name = "grouppBox1"
        Me.grouppBox1.Size = New System.Drawing.Size(408, 88)
        Me.grouppBox1.TabIndex = 21
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
        Me.btnClose.Location = New System.Drawing.Point(288, 56)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close IO"
        Me.ToolTip1.SetToolTip(Me.btnClose, "Click to close the IO enviornment")
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.btnConfigure)
        Me.groupBox2.Controls.Add(Me.btnMeasure)
        Me.groupBox2.Controls.Add(Me.txtResult)
        Me.groupBox2.Controls.Add(Me.lblResult)
        Me.groupBox2.Location = New System.Drawing.Point(4, 88)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(408, 112)
        Me.groupBox2.TabIndex = 22
        Me.groupBox2.TabStop = False
        '
        'btnConfigure
        '
        Me.btnConfigure.Location = New System.Drawing.Point(288, 64)
        Me.btnConfigure.Name = "btnConfigure"
        Me.btnConfigure.Size = New System.Drawing.Size(104, 23)
        Me.btnConfigure.TabIndex = 4
        Me.btnConfigure.Text = "Configure"
        Me.ToolTip1.SetToolTip(Me.btnConfigure, "Click to use CONFigure command with the dBm math operation")
        '
        'btnMeasure
        '
        Me.btnMeasure.Location = New System.Drawing.Point(288, 32)
        Me.btnMeasure.Name = "btnMeasure"
        Me.btnMeasure.Size = New System.Drawing.Size(104, 23)
        Me.btnMeasure.TabIndex = 3
        Me.btnMeasure.Text = " Measure"
        Me.ToolTip1.SetToolTip(Me.btnMeasure, "Clisk to use MEASure? command to make a single AC measurement")
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(72, 15)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtResult.Size = New System.Drawing.Size(208, 88)
        Me.txtResult.TabIndex = 5
        Me.txtResult.Text = ""
        '
        'lblResult
        '
        Me.lblResult.Location = New System.Drawing.Point(16, 48)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(48, 16)
        Me.lblResult.TabIndex = 0
        Me.lblResult.Text = "Result"
        '
        'frmGPIBMeasConfig
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 205)
        Me.Controls.Add(Me.grouppBox1)
        Me.Controls.Add(Me.groupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmGPIBMeasConfig"
        Me.Text = "Agilent 34401A GPIBMeasConfig"
        Me.grouppBox1.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmGPIBMeasConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnableControl(False)
    End Sub
    Private Sub EnableControl(ByVal bStatus As Boolean)

        txtResult.Text = ""

        btnInitIO.Enabled = Not bStatus
        txtAddress.Enabled = Not bStatus

        txtResult.Enabled = bStatus
        btnClose.Enabled = bStatus
        btnConfigure.Enabled = bStatus
        btnMeasure.Enabled = bStatus

    End Sub

    Private Sub btnInitIO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInitIO.Click
        ' set the I/O address to the text box in case the user changed it.
        ' bring up the input dialog and save any changes to the text box
        Dim mgr As Ivi.Visa.Interop.ResourceManager
        Dim ioAddress As String
        On Error GoTo ioError

        ioAddress = txtAddress.Text

        txtAddress.Text = ioAddress
        mgr = New Ivi.Visa.Interop.ResourceManager
        iodmm = New Ivi.Visa.Interop.FormattedIO488
        iodmm.IO() = mgr.Open(ioAddress)
        iodmm.IO.Timeout = 7000
        EnableControl(True)

        Exit Sub
ioError:
        MsgBox("InitIO Error:" & vbCrLf & Err.Description)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        iodmm.IO.Close()
        EnableControl(False)
    End Sub

    Private Sub btnMeasure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMeasure.Click

        ' The following example uses Measure? command to make a single
        ' ac current measurement. This is the easiest way to program the
        ' multimeter for measurements. However, MEASure? does not offer
        ' much flexibility.
        '
        ' Be sure to set the instrument address in the Form.Load routine
        ' to match the instrument.
        Dim Result As Double

        On Error GoTo ioError


        btnConfigure.Enabled = False
        btnMeasure.Enabled = False

        ' EXAMPLE for using the Measure command
        iodmm.WriteString("*RST")
        iodmm.WriteString("*CLS")

        ' Set meter to 1 amp ac range
        iodmm.WriteString("Measure:Current:AC? 1A,0.001MA")
        Result = iodmm.ReadNumber

        txtResult.Text = Result & " amps AC"

        btnConfigure.Enabled = True
        btnMeasure.Enabled = True

        Exit Sub
ioError:
        btnConfigure.Enabled = True
        btnMeasure.Enabled = True
        MsgBox("Measurement Error: " & vbCrLf & Err.Description)
    End Sub

    Private Sub btnConfigure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfigure.Click

        ' The following example uses CONFigure with the dBm math operation
        ' The CONFigure command gives you a little more programming flexibility
        ' than the MEASure? command. This allows you to 'incrementally'
        ' change the multimeter's configuration.
        '
        ' Be sure to set the instrument address
        ' to match the instrument
        '
        Dim Readings() As Object
        Dim i As Long
        Dim status As Long
        Dim sTemp As String


        On Error GoTo ioError
        

        btnConfigure.Enabled = False
        btnMeasure.Enabled = False


        ' EXAMPLE for using the CONFigure command
        iodmm.WriteString("*RST")                     ' Reset the dmm
        iodmm.WriteString("*CLS")                     ' Clear dmm status registers
        iodmm.WriteString("CALC:DBM:REF 50")          ' set 50 ohm reference for dBm
        ' the CONFigure command sets range and resolution for AC
        ' all other AC function parameters are defaulted but can be
        ' set before a READ?
        iodmm.WriteString("Conf:Volt:AC 1, 0.001")     ' set dmm to 1 amp ac range"
        iodmm.WriteString(":Det:Band 200")             ' Select the 200 Hz (fast) ac filter
        iodmm.WriteString("Trig:Coun 5")              ' dmm will accept 5 triggers
        iodmm.WriteString("Trig:Sour IMM")            ' Trigger source is IMMediate
        iodmm.WriteString("Calc:Func DBM")            ' Select dBm function
        iodmm.WriteString("Calc:Stat ON")       ' Enable math and request operation complete
        iodmm.WriteString("Read?")                    ' Take readings; send to output buffer
        Readings = iodmm.ReadList                     ' Get readings and parse into array of doubles

        ' Enter will wait until all readings are completed
        ' print to Text box
        txtResult.Text = ""
        sTemp = ""
        For i = 0 To UBound(Readings)
            sTemp = sTemp & Readings(i) & " dBm" & vbCrLf
        Next i
        txtResult.Text = sTemp

        btnConfigure.Enabled = True
        btnMeasure.Enabled = True

        Exit Sub
ioError:
        btnConfigure.Enabled = True
        btnMeasure.Enabled = True
        MsgBox("Error configuring the instrument: " & vbCrLf & Err.Description)
    End Sub
End Class
