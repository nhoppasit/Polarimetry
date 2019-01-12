'"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
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
' 20V/div

Public Class CApplyBurst
    Inherits System.Windows.Forms.Form
    Private rm As Ivi.Visa.Interop.ResourceManager
    Private ioArbFG As Ivi.Visa.Interop.FormattedIO488
    Private msg As Ivi.Visa.Interop.IMessage

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
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents m_idLabel As System.Windows.Forms.Label
    Friend WithEvents m_gpibAddrTextBox As System.Windows.Forms.TextBox
    Friend WithEvents m_setioButton As System.Windows.Forms.Button
    Friend WithEvents toolTipCtrl As System.Windows.Forms.ToolTip
    Friend WithEvents gb3 As System.Windows.Forms.GroupBox
    Friend WithEvents gb2 As System.Windows.Forms.GroupBox
    Friend WithEvents burstnoLabel As System.Windows.Forms.Label
    Friend WithEvents functionLabel As System.Windows.Forms.Label
    Friend WithEvents frequencyLabel As System.Windows.Forms.Label
    Friend WithEvents m_closeioButton As System.Windows.Forms.Button
    Friend WithEvents m_errorListBox As System.Windows.Forms.TextBox
    Friend WithEvents m_geterrorButton As System.Windows.Forms.Button
    Friend WithEvents m_initiateButton As System.Windows.Forms.Button
    Friend WithEvents m_configureButton As System.Windows.Forms.Button
    Friend WithEvents m_burstnoComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents m_functionComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents m_frequencyTextBox As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CApplyBurst))
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.m_closeioButton = New System.Windows.Forms.Button
        Me.m_idLabel = New System.Windows.Forms.Label
        Me.m_gpibAddrTextBox = New System.Windows.Forms.TextBox
        Me.m_setioButton = New System.Windows.Forms.Button
        Me.toolTipCtrl = New System.Windows.Forms.ToolTip(Me.components)
        Me.m_errorListBox = New System.Windows.Forms.TextBox
        Me.m_geterrorButton = New System.Windows.Forms.Button
        Me.m_initiateButton = New System.Windows.Forms.Button
        Me.m_configureButton = New System.Windows.Forms.Button
        Me.m_burstnoComboBox = New System.Windows.Forms.ComboBox
        Me.m_functionComboBox = New System.Windows.Forms.ComboBox
        Me.m_frequencyTextBox = New System.Windows.Forms.TextBox
        Me.gb3 = New System.Windows.Forms.GroupBox
        Me.gb2 = New System.Windows.Forms.GroupBox
        Me.burstnoLabel = New System.Windows.Forms.Label
        Me.functionLabel = New System.Windows.Forms.Label
        Me.frequencyLabel = New System.Windows.Forms.Label
        Me.groupBox1.SuspendLayout()
        Me.gb3.SuspendLayout()
        Me.gb2.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.m_closeioButton)
        Me.groupBox1.Controls.Add(Me.m_idLabel)
        Me.groupBox1.Controls.Add(Me.m_gpibAddrTextBox)
        Me.groupBox1.Controls.Add(Me.m_setioButton)
        Me.groupBox1.Location = New System.Drawing.Point(2, -1)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(474, 65)
        Me.groupBox1.TabIndex = 16
        Me.groupBox1.TabStop = False
        '
        'm_closeioButton
        '
        Me.m_closeioButton.Enabled = False
        Me.m_closeioButton.Location = New System.Drawing.Point(329, 12)
        Me.m_closeioButton.Name = "m_closeioButton"
        Me.m_closeioButton.Size = New System.Drawing.Size(104, 24)
        Me.m_closeioButton.TabIndex = 5
        Me.m_closeioButton.Text = "Close I/O"
        Me.toolTipCtrl.SetToolTip(Me.m_closeioButton, "Click to Close the IO enviornment")
        '
        'm_idLabel
        '
        Me.m_idLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.m_idLabel.Location = New System.Drawing.Point(9, 40)
        Me.m_idLabel.Name = "m_idLabel"
        Me.m_idLabel.Size = New System.Drawing.Size(457, 20)
        Me.m_idLabel.TabIndex = 4
        Me.m_idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.toolTipCtrl.SetToolTip(Me.m_idLabel, "Displays ID string of the instrument")
        '
        'm_gpibAddrTextBox
        '
        Me.m_gpibAddrTextBox.Location = New System.Drawing.Point(6, 13)
        Me.m_gpibAddrTextBox.Name = "m_gpibAddrTextBox"
        Me.m_gpibAddrTextBox.Size = New System.Drawing.Size(211, 21)
        Me.m_gpibAddrTextBox.TabIndex = 3
        Me.m_gpibAddrTextBox.Text = "GPIB0::10"
        Me.toolTipCtrl.SetToolTip(Me.m_gpibAddrTextBox, "Enter the GPIB board number and the GPIB address of the Instrument")
        '
        'm_setioButton
        '
        Me.m_setioButton.Location = New System.Drawing.Point(220, 12)
        Me.m_setioButton.Name = "m_setioButton"
        Me.m_setioButton.Size = New System.Drawing.Size(104, 24)
        Me.m_setioButton.TabIndex = 2
        Me.m_setioButton.Text = "Set I/O"
        Me.toolTipCtrl.SetToolTip(Me.m_setioButton, "Click to set the IO enviornment and get the ID of the instrument")
        '
        'm_errorListBox
        '
        Me.m_errorListBox.AcceptsTab = True
        Me.m_errorListBox.Location = New System.Drawing.Point(8, 17)
        Me.m_errorListBox.Multiline = True
        Me.m_errorListBox.Name = "m_errorListBox"
        Me.m_errorListBox.ReadOnly = True
        Me.m_errorListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.m_errorListBox.Size = New System.Drawing.Size(341, 108)
        Me.m_errorListBox.TabIndex = 1
        Me.m_errorListBox.Text = ""
        Me.toolTipCtrl.SetToolTip(Me.m_errorListBox, "Shows history of Errors")
        '
        'm_geterrorButton
        '
        Me.m_geterrorButton.Location = New System.Drawing.Point(354, 51)
        Me.m_geterrorButton.Name = "m_geterrorButton"
        Me.m_geterrorButton.Size = New System.Drawing.Size(112, 37)
        Me.m_geterrorButton.TabIndex = 0
        Me.m_geterrorButton.Text = "Get Instrument Error"
        Me.toolTipCtrl.SetToolTip(Me.m_geterrorButton, "Read errors generated")
        '
        'm_initiateButton
        '
        Me.m_initiateButton.Enabled = False
        Me.m_initiateButton.Location = New System.Drawing.Point(5, 150)
        Me.m_initiateButton.Name = "m_initiateButton"
        Me.m_initiateButton.Size = New System.Drawing.Size(471, 40)
        Me.m_initiateButton.TabIndex = 18
        Me.m_initiateButton.Text = "2. Initiate Burst"
        Me.toolTipCtrl.SetToolTip(Me.m_initiateButton, "Click to start the burst by triggering the instrument")
        '
        'm_configureButton
        '
        Me.m_configureButton.Location = New System.Drawing.Point(393, 41)
        Me.m_configureButton.Name = "m_configureButton"
        Me.m_configureButton.Size = New System.Drawing.Size(72, 20)
        Me.m_configureButton.TabIndex = 6
        Me.m_configureButton.Text = "Configure"
        Me.toolTipCtrl.SetToolTip(Me.m_configureButton, "Click to configure the instrument with the selected settings")
        '
        'm_burstnoComboBox
        '
        Me.m_burstnoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.m_burstnoComboBox.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "10"})
        Me.m_burstnoComboBox.Location = New System.Drawing.Point(254, 39)
        Me.m_burstnoComboBox.Name = "m_burstnoComboBox"
        Me.m_burstnoComboBox.Size = New System.Drawing.Size(134, 21)
        Me.m_burstnoComboBox.TabIndex = 5
        Me.toolTipCtrl.SetToolTip(Me.m_burstnoComboBox, "Select the number of burst")
        '
        'm_functionComboBox
        '
        Me.m_functionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.m_functionComboBox.Items.AddRange(New Object() {"Sine", "Square", "Triangle", "Ramp"})
        Me.m_functionComboBox.Location = New System.Drawing.Point(151, 39)
        Me.m_functionComboBox.Name = "m_functionComboBox"
        Me.m_functionComboBox.Size = New System.Drawing.Size(99, 21)
        Me.m_functionComboBox.TabIndex = 4
        Me.toolTipCtrl.SetToolTip(Me.m_functionComboBox, "Select the function")
        '
        'm_frequencyTextBox
        '
        Me.m_frequencyTextBox.Location = New System.Drawing.Point(9, 39)
        Me.m_frequencyTextBox.Name = "m_frequencyTextBox"
        Me.m_frequencyTextBox.Size = New System.Drawing.Size(134, 21)
        Me.m_frequencyTextBox.TabIndex = 3
        Me.m_frequencyTextBox.Text = "5000"
        Me.toolTipCtrl.SetToolTip(Me.m_frequencyTextBox, "Enter the frequency value and press 'Enter' key")
        '
        'gb3
        '
        Me.gb3.Controls.Add(Me.m_errorListBox)
        Me.gb3.Controls.Add(Me.m_geterrorButton)
        Me.gb3.Enabled = False
        Me.gb3.Location = New System.Drawing.Point(5, 196)
        Me.gb3.Name = "gb3"
        Me.gb3.Size = New System.Drawing.Size(471, 130)
        Me.gb3.TabIndex = 19
        Me.gb3.TabStop = False
        Me.gb3.Text = "Check Instrument Errors"
        '
        'gb2
        '
        Me.gb2.Controls.Add(Me.m_configureButton)
        Me.gb2.Controls.Add(Me.burstnoLabel)
        Me.gb2.Controls.Add(Me.functionLabel)
        Me.gb2.Controls.Add(Me.frequencyLabel)
        Me.gb2.Controls.Add(Me.m_burstnoComboBox)
        Me.gb2.Controls.Add(Me.m_functionComboBox)
        Me.gb2.Controls.Add(Me.m_frequencyTextBox)
        Me.gb2.Enabled = False
        Me.gb2.Location = New System.Drawing.Point(4, 71)
        Me.gb2.Name = "gb2"
        Me.gb2.Size = New System.Drawing.Size(473, 72)
        Me.gb2.TabIndex = 17
        Me.gb2.TabStop = False
        Me.gb2.Text = "1. Configure ArbFG for Burst"
        '
        'burstnoLabel
        '
        Me.burstnoLabel.Location = New System.Drawing.Point(254, 19)
        Me.burstnoLabel.Name = "burstnoLabel"
        Me.burstnoLabel.Size = New System.Drawing.Size(123, 18)
        Me.burstnoLabel.TabIndex = 2
        Me.burstnoLabel.Text = "Select no of Bursts:"
        '
        'functionLabel
        '
        Me.functionLabel.Location = New System.Drawing.Point(151, 19)
        Me.functionLabel.Name = "functionLabel"
        Me.functionLabel.Size = New System.Drawing.Size(99, 18)
        Me.functionLabel.TabIndex = 1
        Me.functionLabel.Text = "Select Function:"
        '
        'frequencyLabel
        '
        Me.frequencyLabel.Location = New System.Drawing.Point(9, 19)
        Me.frequencyLabel.Name = "frequencyLabel"
        Me.frequencyLabel.Size = New System.Drawing.Size(120, 18)
        Me.frequencyLabel.TabIndex = 0
        Me.frequencyLabel.Text = "Set Frequency(Hz):"
        '
        'CApplyBurst
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(480, 329)
        Me.Controls.Add(Me.gb3)
        Me.Controls.Add(Me.m_initiateButton)
        Me.Controls.Add(Me.gb2)
        Me.Controls.Add(Me.groupBox1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CApplyBurst"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agilent 33120A ApplyBurst"
        Me.groupBox1.ResumeLayout(False)
        Me.gb3.ResumeLayout(False)
        Me.gb2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub m_setioButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_setioButton.Click

        rm = New Ivi.Visa.Interop.ResourceManager
        On Error GoTo myHandler

        'Open port
        msg = rm.Open(m_gpibAddrTextBox.Text, Ivi.Visa.Interop.AccessMode.NO_LOCK, 2000, "")
        'Initialize Formatted IO
        ioArbFG.IO = msg
        'Get the ID string of the instrument connected.
        m_idLabel.Text = GetInstrumentID()
        EnableControls()
        Exit Sub

myHandler:
        MessageBox.Show("Open failed on " + m_gpibAddrTextBox.Text + " ", "ApplyBurst", MessageBoxButtons.OK, MessageBoxIcon.Error)
        DisableControls()

    End Sub
    Private Function GetInstrumentID() As String
        Dim m_strReturn As String
        ioArbFG.WriteString("*IDN?")
        m_strReturn = ioArbFG.ReadString()

        'Check if its not null
        If (m_strReturn <> "") Then
            GetInstrumentID = m_strReturn
            EnableControls()
        Else
            GetInstrumentID = "Error : Could not read ID of instrument"
            DisableControls()
        End If
    End Function

    Private Sub EnableControls()
        gb2.Enabled = True
        gb3.Enabled = True
        m_closeioButton.Enabled = True
    End Sub

    Private Sub DisableControls()
        gb2.Enabled = False
        gb3.Enabled = False
        m_initiateButton.Enabled = False
    End Sub

    Private Sub CApplyBurst_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.m_functionComboBox.SelectedIndex = 0
        Me.m_burstnoComboBox.SelectedIndex = 0
        ioArbFG = New Ivi.Visa.Interop.FormattedIO488Class
    End Sub

    Private Sub closeioButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_closeioButton.Click
        ioArbFG.IO.Close()
        Me.m_setioButton.Enabled = True
        Me.m_closeioButton.Enabled = False
        DisableControls()
        m_idLabel.Text = ""
    End Sub

    Private Sub frequencyTextBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles m_frequencyTextBox.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then    'Enter key is pressed
            'Ensure a valid entry for frequency
            Dim frq As Single
            frq = ConvertStringDouble(m_frequencyTextBox.Text)
            If (frq >= 0.000001 And frq <= 20000000.0) Then

            Else
                MessageBox.Show("Valid Frequency Range : 0.000001 to 20000000")
                m_frequencyTextBox.Text = "5000"
            End If
        End If
    End Sub
    Private Function ConvertStringDouble(ByVal strVal As String) As Single
        Dim floatVal As Single
        On Error GoTo myErrorHandler
        floatVal = System.Convert.ToDouble(strVal)
        ConvertStringDouble = floatVal
        Exit Function
myErrorHandler:
        MessageBox.Show("Error : Could not convert to single value")
    End Function

    Private Sub configureButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_configureButton.Click
        Dim Frequency As String
        Frequency = m_frequencyTextBox.Text
        ioArbFG.WriteString("*RST")
        ioArbFG.WriteString("Output:Load 50")

        Select Case m_functionComboBox.Text.ToUpper()
            Case "SINE"
                ioArbFG.WriteString("Apply:Sin " + Frequency + ",5")
            Case "SQUARE"
                ioArbFG.WriteString("Apply:Square " + Frequency + ",5")
            Case "TRIANGLE"
                ioArbFG.WriteString("Apply:Triangle " + Frequency + ",5")
            Case "RAMP"
                ioArbFG.WriteString("Apply:Ramp " + Frequency + ",5")
        End Select

        ioArbFG.WriteString("BM:NCYC " + m_burstnoComboBox.Text)
        ioArbFG.WriteString("BM:Phase 270")
        ioArbFG.WriteString("Volt:Offset .5")
        ioArbFG.WriteString("Trig:Sour Bus")
        ioArbFG.WriteString("BM:State On")
        m_initiateButton.Enabled = True
    End Sub

    Private Sub initiateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_initiateButton.Click
        ioArbFG.WriteString("*TRG")
    End Sub

    Private Sub geterrorButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_geterrorButton.Click
        Dim reply As String
        ioArbFG.WriteString("syst:error?")
        reply = ioArbFG.ReadString()
        m_errorListBox.Text = m_errorListBox.Text & reply.Substring(0, reply.Length - 1) & vbCrLf
    End Sub

    Private Sub frequencyTextBox_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_frequencyTextBox.LostFocus
        'Ensure a valid entry for frequency
        Dim frq As Single
        frq = ConvertStringDouble(m_frequencyTextBox.Text)
        If (frq >= 0.000001 And frq <= 20000000.0) Then

        Else
            MessageBox.Show("Valid Frequency Range : 0.000001 to 20000000")
            m_frequencyTextBox.Text = "5000"
        End If

    End Sub
End Class
