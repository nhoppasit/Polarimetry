' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
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
'      store the instrument configuration in memory.
'

Public Class CAMmodulation
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
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents m_stagelocationComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents m_saveButton As System.Windows.Forms.Button
    Friend WithEvents m_recallButton As System.Windows.Forms.Button
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents m_idLabel As System.Windows.Forms.Label
    Friend WithEvents m_gpibAddrTextBox As System.Windows.Forms.TextBox
    Friend WithEvents m_setioButton As System.Windows.Forms.Button
    Friend WithEvents m_setamButton As System.Windows.Forms.Button
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents toolTipCtrl As System.Windows.Forms.ToolTip
    Friend WithEvents m_closeioButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CAMmodulation))
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.label1 = New System.Windows.Forms.Label
        Me.m_stagelocationComboBox = New System.Windows.Forms.ComboBox
        Me.m_saveButton = New System.Windows.Forms.Button
        Me.m_recallButton = New System.Windows.Forms.Button
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.m_closeioButton = New System.Windows.Forms.Button
        Me.m_idLabel = New System.Windows.Forms.Label
        Me.m_gpibAddrTextBox = New System.Windows.Forms.TextBox
        Me.m_setioButton = New System.Windows.Forms.Button
        Me.m_setamButton = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.toolTipCtrl = New System.Windows.Forms.ToolTip(Me.components)
        Me.groupBox2.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.label1)
        Me.groupBox2.Controls.Add(Me.m_stagelocationComboBox)
        Me.groupBox2.Controls.Add(Me.m_saveButton)
        Me.groupBox2.Controls.Add(Me.m_recallButton)
        Me.groupBox2.Enabled = False
        Me.groupBox2.Location = New System.Drawing.Point(239, 66)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(237, 183)
        Me.groupBox2.TabIndex = 17
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Save/Recall"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(10, 99)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(220, 72)
        Me.label1.TabIndex = 13
        Me.label1.Text = "This section explains how frequently used setting can be stored in the memory loc" & _
        "ation. Select the location from the list. Press 'Save' or 'Recall' to store or r" & _
        "ecall the panel setting."
        '
        'm_stagelocationComboBox
        '
        Me.m_stagelocationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.m_stagelocationComboBox.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.m_stagelocationComboBox.Location = New System.Drawing.Point(89, 28)
        Me.m_stagelocationComboBox.Name = "m_stagelocationComboBox"
        Me.m_stagelocationComboBox.Size = New System.Drawing.Size(58, 21)
        Me.m_stagelocationComboBox.TabIndex = 12
        Me.toolTipCtrl.SetToolTip(Me.m_stagelocationComboBox, "Select memory location where the settings are to be stored/recalled")
        '
        'm_saveButton
        '
        Me.m_saveButton.Location = New System.Drawing.Point(7, 19)
        Me.m_saveButton.Name = "m_saveButton"
        Me.m_saveButton.Size = New System.Drawing.Size(70, 39)
        Me.m_saveButton.TabIndex = 11
        Me.m_saveButton.Text = "Save State"
        Me.toolTipCtrl.SetToolTip(Me.m_saveButton, "Save state to the selected memory location")
        '
        'm_recallButton
        '
        Me.m_recallButton.Location = New System.Drawing.Point(159, 19)
        Me.m_recallButton.Name = "m_recallButton"
        Me.m_recallButton.Size = New System.Drawing.Size(70, 39)
        Me.m_recallButton.TabIndex = 10
        Me.m_recallButton.Text = "Recall State"
        Me.toolTipCtrl.SetToolTip(Me.m_recallButton, "Recalls the satee from the selected location")
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
        'm_setamButton
        '
        Me.m_setamButton.Enabled = False
        Me.m_setamButton.Location = New System.Drawing.Point(7, 85)
        Me.m_setamButton.Name = "m_setamButton"
        Me.m_setamButton.Size = New System.Drawing.Size(225, 27)
        Me.m_setamButton.TabIndex = 15
        Me.m_setamButton.Text = "Set AM"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(9, 126)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(220, 114)
        Me.label2.TabIndex = 18
        Me.label2.Text = "Press the 'Set AM' button to set the Function generator to give following output:" & _
        "                                            Amplitude modulated sine wave with c" & _
        "arrier as 5kHz @5Vpp and modulating signal 200Hz Sine at 50Ohm"
        '
        'CAMmodulation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(480, 251)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.m_setamButton)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.groupBox2)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CAMmodulation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agilent 33120A AM Mod"
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
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
        MessageBox.Show("Open failed on " + m_gpibAddrTextBox.Text + " ", "AM Modulation", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        m_setamButton.Enabled = True
        groupBox2.Enabled = True
        m_closeioButton.Enabled = True
    End Sub

    Private Sub DisableControls()
        m_setamButton.Enabled = False
        groupBox2.Enabled = False
    End Sub

    Private Sub CAMmodulation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ioArbFG = New Ivi.Visa.Interop.FormattedIO488Class
    End Sub

    Private Sub closeioButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_closeioButton.Click
        ioArbFG.IO.Close()
        Me.m_setioButton.Enabled = True
        Me.m_closeioButton.Enabled = False
        DisableControls()
        m_idLabel.Text = ""
    End Sub

    Private Sub m_saveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_saveButton.Click
        If (m_stagelocationComboBox.Text <> "") Then
            ioArbFG.WriteString("*SAV " + m_stagelocationComboBox.Text, True)
        Else
            MessageBox.Show("Please select a location from list")
        End If
    End Sub

    Private Sub m_recallButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_recallButton.Click
        If (m_stagelocationComboBox.Text <> "") Then
            ioArbFG.WriteString("*RCL " + m_stagelocationComboBox.Text, True)
        Else
            MessageBox.Show("Please select a location from list")
        End If
    End Sub

    Private Sub m_setamButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_setamButton.Click
        ioArbFG.WriteString("*RST", True) '										// reset instrument to bring to know state
        ioArbFG.WriteString("Output:load 50", True) '						// set output termination to 50 ohm
        ioArbFG.WriteString("Function:Shape Sinusoid", True) '		// set carrier waveshape to sine
        ioArbFG.WriteString("Frequency 5000; Volt 5", True) '			// set carrier frequency to 5kHz, @ 5Vpp
        ioArbFG.WriteString("AM:Internal:Function Sinusoid", True) '// modulation to sine
        ioArbFG.WriteString("AM:Internal:Frequency 200", True) '	// Modulation frequency to 200Hz
        ioArbFG.WriteString("AM:State On", True)
    End Sub
End Class
