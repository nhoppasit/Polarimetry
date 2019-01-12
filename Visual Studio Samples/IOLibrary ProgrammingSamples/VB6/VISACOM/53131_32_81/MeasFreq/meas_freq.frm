VERSION 5.00
Begin VB.Form frmMeas_freq 
   Caption         =   "Agilent Counter Sample"
   ClientHeight    =   5715
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6285
   LinkTopic       =   "Form1"
   ScaleHeight     =   5715
   ScaleWidth      =   6285
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Measurements"
      Height          =   2655
      Left            =   240
      TabIndex        =   9
      Top             =   2880
      Width           =   5895
      Begin VB.ListBox List1 
         Height          =   2010
         Left            =   360
         TabIndex        =   11
         Top             =   360
         Width           =   3135
      End
      Begin VB.CommandButton cmdGetReadings 
         Caption         =   "Get Readings"
         Height          =   375
         Left            =   3840
         TabIndex        =   3
         Top             =   600
         Width           =   1815
      End
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   240
      Locked          =   -1  'True
      TabIndex        =   8
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
      TabIndex        =   6
      TabStop         =   0   'False
      Top             =   1920
      Width           =   3855
   End
   Begin VB.TextBox txtID 
      Height          =   375
      Left            =   240
      Locked          =   -1  'True
      TabIndex        =   4
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
      TabIndex        =   10
      Top             =   120
      Width           =   2415
   End
   Begin VB.Label Label2 
      Caption         =   "Revision"
      Height          =   255
      Left            =   240
      TabIndex        =   7
      Top             =   1680
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "ID string"
      Height          =   255
      Left            =   240
      TabIndex        =   5
      Top             =   960
      Width           =   1935
   End
End
Attribute VB_Name = "frmMeas_freq"
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

Dim Cntr As VisaComLib.FormattedIO488

Private Sub cmdGetReadings_Click()
    'This program sets up the counter to make 10 frequency measurements
    'on channel 1 using a 0.1 second gate time.
    'ASCII format is used to preserve resolution
    Dim Reading(10) As Double
    Dim strtemp As String
    Dim i As Integer    'i is used for loops
    Dim samples As Integer
    'Reading ASCII formatted data gives results to the correct
    'resolution. Must be read into a string. The maximum number
    'of characters that can ever be sent is 20 per measurement.
    Dim Frequency(10) As String    'String to be read
    
    cmdGetReadings.Enabled = False

    samples = 10    'Number of measurements

    With Cntr
        .WriteString "*RST"    'Reset counter
        .WriteString "*CLS"    'Clear event registers and error queue
        .WriteString "*SRE 0"    'Clear service request enable register
        .WriteString "*ESE 0"    'Clear event status enable register
        .WriteString ":STAT:PRES"    'Preset enable registers and transition
        'filters for operation and questionable
        'status structures
        .WriteString ":Func 'Frequency 1'"    'Measure frequency
        .WriteString ":FREQ:ARM:STAR:SOUR IMM"    'These 3 lines enable using
        .WriteString ":FREQ:ARM:STOP:SOUR TIM"    'time arming with a 0.1 second
        .WriteString ":FREQ:ARM:STOP:TIM .1"    'gate time

        List1.Clear

        ' now get the 10 readings
        For i = 0 To samples - 1
            .WriteString "Read:Freq?"
            Frequency(i) = .ReadString
            List1.AddItem Frequency(i)
            List1.Refresh
        Next i
    End With
    
    cmdGetReadings.Enabled = True
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
    result = Cntr.ReadString

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

