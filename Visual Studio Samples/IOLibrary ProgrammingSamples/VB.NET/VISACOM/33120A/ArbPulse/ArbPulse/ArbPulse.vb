' ' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
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
Public Class CArbPulse
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mygroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents m_closeioButton As System.Windows.Forms.Button
    Friend WithEvents m_downloadButton As System.Windows.Forms.Button
    Friend WithEvents m_falltimeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents m_pulsetopTextBox As System.Windows.Forms.TextBox
    Friend WithEvents m_risetimeTextBox As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CArbPulse))
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.m_closeioButton = New System.Windows.Forms.Button
        Me.m_idLabel = New System.Windows.Forms.Label
        Me.m_gpibAddrTextBox = New System.Windows.Forms.TextBox
        Me.m_setioButton = New System.Windows.Forms.Button
        Me.toolTipCtrl = New System.Windows.Forms.ToolTip(Me.components)
        Me.mygroupBox = New System.Windows.Forms.GroupBox
        Me.m_downloadButton = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.m_falltimeTextBox = New System.Windows.Forms.TextBox
        Me.m_pulsetopTextBox = New System.Windows.Forms.TextBox
        Me.m_risetimeTextBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.groupBox1.SuspendLayout()
        Me.mygroupBox.SuspendLayout()
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
        'mygroupBox
        '
        Me.mygroupBox.Controls.Add(Me.m_downloadButton)
        Me.mygroupBox.Controls.Add(Me.Label5)
        Me.mygroupBox.Controls.Add(Me.Label4)
        Me.mygroupBox.Controls.Add(Me.m_falltimeTextBox)
        Me.mygroupBox.Controls.Add(Me.m_pulsetopTextBox)
        Me.mygroupBox.Controls.Add(Me.m_risetimeTextBox)
        Me.mygroupBox.Controls.Add(Me.Label3)
        Me.mygroupBox.Controls.Add(Me.Label2)
        Me.mygroupBox.Controls.Add(Me.Label1)
        Me.mygroupBox.Enabled = False
        Me.mygroupBox.Location = New System.Drawing.Point(2, 66)
        Me.mygroupBox.Name = "mygroupBox"
        Me.mygroupBox.Size = New System.Drawing.Size(474, 182)
        Me.mygroupBox.TabIndex = 17
        Me.mygroupBox.TabStop = False
        Me.mygroupBox.Text = "Arbitrary Pulse Generation"
        '
        'm_downloadButton
        '
        Me.m_downloadButton.Location = New System.Drawing.Point(10, 143)
        Me.m_downloadButton.Name = "m_downloadButton"
        Me.m_downloadButton.Size = New System.Drawing.Size(457, 34)
        Me.m_downloadButton.TabIndex = 8
        Me.m_downloadButton.Text = "Download Pulse"
        Me.toolTipCtrl.SetToolTip(Me.m_downloadButton, "Click to download the points to instrument")
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(9, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(457, 30)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Calculate the time per point as the period of the frequency divided by the number" & _
        " of points (4000) in the array."
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(9, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(457, 30)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "The pulse parameters are expressed as number of points. Each point is 0.5usec @ 5" & _
        "kHz, 4000pts in array."
        '
        'm_falltimeTextBox
        '
        Me.m_falltimeTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_falltimeTextBox.Location = New System.Drawing.Point(339, 44)
        Me.m_falltimeTextBox.Name = "m_falltimeTextBox"
        Me.m_falltimeTextBox.Size = New System.Drawing.Size(128, 23)
        Me.m_falltimeTextBox.TabIndex = 5
        Me.m_falltimeTextBox.Text = "15"
        Me.toolTipCtrl.SetToolTip(Me.m_falltimeTextBox, "Enter the points for fall time")
        '
        'm_pulsetopTextBox
        '
        Me.m_pulsetopTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_pulsetopTextBox.Location = New System.Drawing.Point(174, 44)
        Me.m_pulsetopTextBox.Name = "m_pulsetopTextBox"
        Me.m_pulsetopTextBox.Size = New System.Drawing.Size(128, 23)
        Me.m_pulsetopTextBox.TabIndex = 4
        Me.m_pulsetopTextBox.Text = "200"
        Me.toolTipCtrl.SetToolTip(Me.m_pulsetopTextBox, "Enter the points for width of pulse top")
        '
        'm_risetimeTextBox
        '
        Me.m_risetimeTextBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_risetimeTextBox.Location = New System.Drawing.Point(9, 44)
        Me.m_risetimeTextBox.Name = "m_risetimeTextBox"
        Me.m_risetimeTextBox.Size = New System.Drawing.Size(128, 23)
        Me.m_risetimeTextBox.TabIndex = 3
        Me.m_risetimeTextBox.Text = "50"
        Me.toolTipCtrl.SetToolTip(Me.m_risetimeTextBox, "Enter the points for rise time")
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(339, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Fall time"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(174, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Pulse top width"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(9, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Rise Time"
        '
        'CArbPulse
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(479, 251)
        Me.Controls.Add(Me.mygroupBox)
        Me.Controls.Add(Me.groupBox1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CArbPulse"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agilent 33120A ArbPulse"
        Me.groupBox1.ResumeLayout(False)
        Me.mygroupBox.ResumeLayout(False)
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

        '//Get the ID string of the instrument connected.
        m_idLabel.Text = GetInstrumentID()

        EnableControls()
        Exit Sub

myHandler:
        MessageBox.Show("Open failed on " + m_gpibAddrTextBox.Text + " ", _
                      "ArbPulse ", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        mygroupBox.Enabled = True
        m_closeioButton.Enabled = True
    End Sub

    Private Sub DisableControls()
        mygroupBox.Enabled = False
        m_closeioButton.Enabled = False
    End Sub

    Private Sub CArbPulse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ioArbFG = New Ivi.Visa.Interop.FormattedIO488Class
    End Sub

    Private Sub closeioButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_closeioButton.Click
        ioArbFG.IO.Close()
        m_setioButton.Enabled = True
        m_closeioButton.Enabled = False
        DisableControls()
        m_idLabel.Text = ""
    End Sub

    Private Function makePulse(ByVal riseTime As Long, ByVal TopWidth As Long, ByVal fallTime As Long) As String

        Dim Waveform(4000) As String
        Dim topStart As Long
        Dim topStop As Long
        Dim endPulse As Long
        Dim i As Long

        topStart = riseTime
        topStop = topStart + TopWidth
        endPulse = topStop + fallTime

        ' Set rise time
        For i = 1 To riseTime
            Waveform(i) = Format(((i - 1) / riseTime), "#0.00")
        Next i

        ' Set pulse width
        For i = riseTime + 1 To topStop
            Waveform(i) = "1"
        Next i

        ' Set fall time
        For i = topStop + 1 To endPulse
            Waveform(i) = Format(((endPulse - i) / fallTime), "#0.00")
        Next i

        ' Set zero level for rest of points
        For i = endPulse + 1 To 4000
            Waveform(i) = "0"
        Next i

        makePulse = Join(Waveform, ",")
        makePulse = makePulse.Remove(0, 1)
    End Function

    Private Sub downloadButton_Click(ByVal sender As System.Object, _
                                                           ByVal e As System.EventArgs) Handles m_downloadButton.Click

        Dim strData As String
        On Error GoTo myErrHandler

        m_downloadButton.Enabled = False
        strData = makePulse(CLng(m_risetimeTextBox.Text), CLng(m_pulsetopTextBox.Text), CLng(m_falltimeTextBox.Text))

        ' Reset instrument
        ioArbFG.WriteString("*RST")

        ' Set timeout large enough to sent all data
        ioArbFG.IO.Timeout = 40000

        ' Download  data points to volatile memory from array
        ioArbFG.WriteString("Data Volatile, " & strData)
        ioArbFG.WriteString("Data:Copy Pulse, Volatile")   ' Copy arb to non-volatile memory
        ioArbFG.WriteString("Function:User Pulse")         ' Select the active arb waveform
        ioArbFG.WriteString("Function:Shape User")         ' output selected arb waveform
        ioArbFG.WriteString("Output:Load 50")              ' Output termination is 50 ohms
        ioArbFG.WriteString("Frequency 5000; Voltage 5")   ' Ouput frequency is 5kHz @ 5 Vpp

        ' The arb will require some time to set everything up at this point
        m_downloadButton.Enabled = True
        MessageBox.Show("Download complete")
        Exit Sub

myErrHandler:
        MessageBox.Show("Error: Download could not be completed.")
        m_downloadButton.Enabled = True
    End Sub

    Private Sub m_risetimeTextBox_KeyPress(ByVal sender As Object, _
                                                                   ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                                                                   Handles m_risetimeTextBox.KeyPress, _
                                                                   m_pulsetopTextBox.KeyPress, m_falltimeTextBox.KeyPress

        Dim KeyAscii As Short = Asc(e.KeyChar)
        Select Case KeyAscii
            Case Is < 32
            Case 48 To 57
            Case Else
                KeyAscii = 0
        End Select
        If KeyAscii = 0 Then
            e.Handled = True
        End If

    End Sub

End Class
