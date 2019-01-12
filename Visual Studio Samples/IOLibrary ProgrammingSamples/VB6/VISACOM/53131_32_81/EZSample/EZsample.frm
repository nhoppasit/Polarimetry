VERSION 5.00
Begin VB.Form frmEZsample 
   Caption         =   "Agilent 5313xA Sample"
   ClientHeight    =   6735
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6255
   LinkTopic       =   "Form1"
   ScaleHeight     =   6735
   ScaleWidth      =   6255
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Measurements"
      Height          =   3495
      Left            =   120
      TabIndex        =   10
      Top             =   3000
      Width           =   6015
      Begin VB.CommandButton cmdFetch 
         Caption         =   "Using Fetch?"
         Height          =   375
         Left            =   3960
         TabIndex        =   14
         Top             =   1320
         Width           =   1815
      End
      Begin VB.TextBox txtReading 
         Height          =   375
         Left            =   240
         TabIndex        =   11
         TabStop         =   0   'False
         Top             =   480
         Width           =   3015
      End
      Begin VB.CommandButton cmdRead 
         Caption         =   "Using Read?"
         Height          =   375
         Left            =   3960
         TabIndex        =   4
         Top             =   840
         Width           =   1815
      End
      Begin VB.CommandButton cmdMeas 
         Caption         =   "Using Measure?"
         Height          =   375
         Left            =   3960
         TabIndex        =   3
         Top             =   360
         Width           =   1815
      End
      Begin VB.Label Label5 
         Alignment       =   2  'Center
         Caption         =   "Make sure there are signals at both counter inputs."
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   615
         Left            =   120
         TabIndex        =   16
         Top             =   2760
         Width           =   5775
      End
      Begin VB.Label Label4 
         Alignment       =   2  'Center
         Height          =   735
         Left            =   240
         TabIndex        =   15
         Top             =   1800
         Width           =   5535
      End
      Begin VB.Label Label3 
         Caption         =   "Reading 1"
         Height          =   375
         Left            =   240
         TabIndex        =   12
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   240
      Locked          =   -1  'True
      TabIndex        =   9
      Text            =   "GPIB::4"
      Top             =   480
      Width           =   3855
   End
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   375
      Left            =   4320
      TabIndex        =   0
      Top             =   480
      Width           =   1815
   End
   Begin VB.CommandButton cmdRevision 
      Caption         =   "Revision"
      Height          =   375
      Left            =   4320
      TabIndex        =   2
      Top             =   1920
      Width           =   1815
   End
   Begin VB.TextBox txtVersion 
      Height          =   375
      Left            =   240
      Locked          =   -1  'True
      TabIndex        =   7
      TabStop         =   0   'False
      Top             =   1920
      Width           =   3855
   End
   Begin VB.TextBox txtID 
      Height          =   375
      Left            =   240
      Locked          =   -1  'True
      TabIndex        =   5
      TabStop         =   0   'False
      Top             =   1200
      Width           =   3855
   End
   Begin VB.CommandButton CmdID 
      Caption         =   "Get ID string"
      Height          =   375
      Left            =   4320
      TabIndex        =   1
      Top             =   1200
      Width           =   1815
   End
   Begin VB.Label Label6 
      Caption         =   "Address"
      Height          =   375
      Left            =   240
      TabIndex        =   13
      Top             =   120
      Width           =   2415
   End
   Begin VB.Label Label2 
      Caption         =   "Revision"
      Height          =   255
      Left            =   240
      TabIndex        =   8
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "ID string"
      Height          =   255
      Left            =   240
      TabIndex        =   6
      Top             =   960
      Width           =   1935
   End
End
Attribute VB_Name = "frmEZsample"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999-2002 Agilent Technologies Inc.  All rights reserved.
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
'
' This program shows how to use the MEASure group of instructions to
' quickly and easily make any of the counter's measurements.
' In this program, time interval, frequency and period will be measured.
' However, the MEASure group can make measurements using any of the other
' counter functions.
' The program is composed of four subroutines. The first uses only the
' MEAS:TINT? (@1),(@2) command to make a time interval measurement.
' The second subroutine uses CONF:FREQ and READ? to make the measurement.
' The third uses CONF:FREQ, INIT and FETCH? to make the measurement.
' The comments at the start of each subroutine explain the benefits of each
' method.
' The fourth subroutine simply resets the counter

Dim Cntr As VisaComLib.FormattedIO488

Private Sub cmdMeas_Click()
    ' Use  the Meas:Tint? (@1),(@2) command
    ' The MEAS:TINT? query initiates a complete measurement
    ' sequence. It configures the counter for a 2 channel time interval
    ' measurement, starts the measurement and asks for the data.  The MEAS
    ' command is the simplest (and least flexible) way to make a measurement
    ' and collect data.
    '
    ' Make sure there are signals at both counter inputs.

    Dim TimeInterval As String

    resetCounter

    Label4.Caption = "Time interval from 1 to 2 measured using MEAS:TINT? (@1),(@2)"
    Label4.Refresh

    Cntr.WriteString "MEAS:TINT? (@1),(@2)"
    TimeInterval = Cntr.ReadString

    Me.txtReading.Text = TimeInterval

End Sub

Private Sub cmdRead_Click()
    ' Use the CONF and READ? commands
    ' The following commands will measure the frequency on channel 1.
    ' The Meas? query can be broken down into CONF and READ? commands.
    ' The CONF and READ? allow more flexibility than the MEAS? query.
    ' CONF can be used to configure a measurement. Additional commands
    ' can then be used to fine tune the measurement setup.  The READ?
    ' command than reads the result. In the following example, a frequency
    ' measurement is configured, then, asa an example for changing the setup
    ' created by the CONF command, the counter is programmed for a trigger
    ' level of 50 mv. (The CONF command tells the counter to use the AUTO
    ' Trigger level mode) Finally, the data is read using the READ? command.
    Dim Frequency As Double

    resetCounter

    Label4.Caption = "Frequency measured using CONF:FREQ (@1) and READ?"
    Label4.Refresh
    
    Cntr.WriteString "CONF:FREQ (@1)"    ' Configure for frequency measurement
    Cntr.WriteString ":EVENT1:LEVEL .05"    ' Set trigger level to 50 mV
    Cntr.WriteString "READ?"    ' Ask for the data
    Frequency = Cntr.ReadNumber

    txtReading.Text = Format$(Frequency, "General Number")

End Sub

Private Sub cmdFetch_Click()
    ' Use INIT and FETCH to read frequency and period.

    ' The READ? command can be broken down into INIT and FETCH?, providing
    ' even more measurement flexibility. By using FETCH?, you can retrieve
    ' results based on already acquired data. For example, period can be
    ' derived from a frequency measurement, without a new acquisition.
    ' The following example uses CONF to set up a frequency measurement.
    ' The trigger level is then changed to - 50 mV and an INIT is performed,
    ' starting the measurement process.  The data is read using
    ' the FETCH:Frequency? command. The period can be read by sending
    ' FETCH:Period?, this time asking for the period.
    Dim Frequency As Double
    Dim Period As Double

    resetCounter

    Label4.Caption = "Frequency and Period measured using CONF:FREQ (@1), INIT and FETCH?"
    Label4.Refresh

    Cntr.WriteString "CONF:FREQ (@1)"    ' Configure for frequency measurement
    Cntr.WriteString ":EVENT1:LEVEL -.05"    ' Set trigger level to 50 mV
    Cntr.WriteString "INIT"    ' Start a measurement
    Cntr.WriteString "Fetch:Frequency?"    ' Ask for the frequency result
    Frequency = Cntr.ReadNumber

    Cntr.WriteString "Fetch:Period?"    ' Ask for period result derived from
    ' frequency measurement. Note that
    ' another measurement was not made
    Period = Cntr.ReadNumber

    txtReading.Text = "Freq. = " & Format$(Frequency, "General Number") & ",  Period = " & Format$(Period, "General Number")

End Sub


Private Sub resetCounter()
    '
    With Cntr
        .WriteString "*RST"    'Reset counter
        .WriteString "*CLS"    'Clear event registers and error queue
        .WriteString "*SRE 0"    'Clear service request enable register
        .WriteString "*ESE 0"    'Clear event status enable register
        .WriteString ":STAT:PRES"    'Preset enable registers and transition
    End With

End Sub

Private Sub CmdID_Click()
    Dim result As String
    Dim IO_type As String

    Cntr.WriteString "*idn?"
    result = Cntr.ReadString

    txtID.Text = result

End Sub
Private Sub cmdRevision_Click()
    ' Gets the hardware revision from the instrument
    Dim result As String

    Cntr.WriteString ":Syst:Vers?"
    result = Cntr.ReadNumber

    txtVersion.Text = result
End Sub

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim ioAddress As String
    On Error GoTo ioError

    ioAddress = txtAddress.Text
    ioAddress = InputBox("Enter the IO address of the Counter", "Set IO address", ioAddress)

    txtAddress.Text = ioAddress
    Set mgr = New VisaComLib.ResourceManager
    Set Cntr = New VisaComLib.FormattedIO488
    Set Cntr.IO = mgr.Open(ioAddress)
    txtAddress.Text = ioAddress

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub

Private Sub Form_Load()
    cmdSetIO_Click
End Sub
Private Sub cmdDisplay_Click()
    ' Send a message to the multimeters display,
    ' and generate a beep
    Cntr.WriteString ":syst:beep;:disp:text " & "'" & txtDisplay.Text & "'"

End Sub
Private Sub cmdClearDisplay_Click()
    ' Clear the display
    Cntr.WriteString "Display:text:Clear"

End Sub


