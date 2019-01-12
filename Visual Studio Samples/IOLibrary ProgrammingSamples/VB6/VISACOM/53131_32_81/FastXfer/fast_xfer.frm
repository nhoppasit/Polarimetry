VERSION 5.00
Begin VB.Form frmFast_xfer 
   Caption         =   "Agilent Counter Sample"
   ClientHeight    =   4725
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6285
   LinkTopic       =   "Form1"
   ScaleHeight     =   4725
   ScaleWidth      =   6285
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Measurements"
      Height          =   2655
      Left            =   240
      TabIndex        =   3
      Top             =   1080
      Width           =   5895
      Begin VB.ListBox List1 
         Height          =   2010
         Left            =   360
         TabIndex        =   5
         Top             =   360
         Width           =   3135
      End
      Begin VB.CommandButton cmdGetReadings 
         Caption         =   "Get Readings"
         Height          =   375
         Left            =   3840
         TabIndex        =   1
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
      TabIndex        =   2
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
   Begin VB.Label Label6 
      Caption         =   "Address"
      Height          =   375
      Left            =   240
      TabIndex        =   4
      Top             =   120
      Width           =   2415
   End
End
Attribute VB_Name = "frmFast_xfer"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999- 2001 Agilent Technologies Inc.  All rights reserved.
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
' This program shows how to set up the counter to transfer data
' at the fastest possible rate.
' Note that the arming is set to AUTO. This allows measurements to be taken
' quickly, but at the least resolution the counter can provide.
' See the program comments for details.
' The data is sent in ASCII format to preserve resolution.
'
Dim Cntr As VisaComLib.FormattedIO488

Private Sub cmdGetReadings_Click()
    'ASCII format is used to preserve resolution
    Dim Reading(200) As String
    Dim strtemp As String
    Dim i As Integer    'i is used for loops
    'Reading ASCII formatted data gives results to the correct
    'resolution. Must be read into a string. The maximum number
    'of characters that can ever be sent is 20 per measurement.

    With Cntr
        .WriteString "*RST"               'Reset counter
        .WriteString "*CLS"              'Clear event registers and error queue
        .WriteString "*SRE 0"            'Clear service request enable register
        .WriteString "*ESE 0"            'Clear event status enable register
        .WriteString ":STAT:PRES"        'Preset enable registers and transition
                                    'filters for operation and questionable
                                    'status structures
                                    
        'The following commands will provide the fastest measurement throughput,
        'independent of the state of the counter prior to these commands.
        .WriteString ":FORMAT ASCII"         ' ASCII format give fastest throughput
        .WriteString ":FUNC ""FREQ 1"""      ' Select frequency
        'The function must be a quoted string. The actual string sent to the
        'counter is "FREQ 1"
        
        .WriteString ":EVENT1:LEVEL 0"           'Set trigger level on channel 1 0 Volts
        .WriteString ":FREQ:ARM:STAR:SOUR IMM"   'These 2 lines enable the auto arming mode
        .WriteString ":FREQ:ARM:STOP:SOUR IMM"
        
        ' Use internal oscillator. If you want to use an external timebase,
        ' you must select it and turn off the automatic detection using:
        ' :ROSC:EXT:CHECK OFF
        .WriteString ":ROSC:SOUR INT"
        
        ' Disable automatic interpolator calibration. The most recent calibration
        ' factors will be used in the calculation for frequency
        .WriteString ":DIAG:CAL:INT:AUTO OFF"
        
        'Turn off the display. This greatly increases measurement throughput
        .WriteString ":DISP:ENABLE OFF"
        
        .WriteString ":CALC:MATH:STATE OFF"      'Make sure all post-processing
        .WriteString ":CALC2:LIM:STATE OFF"      'is turned off.
        .WriteString ":CALC3:AVER:STATE OFF"
        
        .WriteString ":HCOPY:CONT OFF"           ' Disable any printing operation
        
        ' Define the Trigger command. This means the command FETC? does
        ' not need to be sent for every measurement, decreasing the number
        ' of bytes transferred over the bus.
        .WriteString "*DDT #15FETC?"             'Define trigger as fetc?
        
        .WriteString ":INIT:CONT ON"             ' Put counter in run mode
        .WriteString "FETCH:FREQ?"               ' Fetch the frequency to be used
        strtemp = .ReadString
        
        ' Tell the counter what frequency to expect on Ch 1. This number must be
        ' within 10% of the input frequency. Using this greatly increases throughput.
        ' When high throughput is not needed, the expected value is not required.
        .WriteString ":FREQ:EXP1 " & strtemp
        
        
        List1.Clear
        List1.AddItem "Making measurements"
        List1.Refresh
        
        For i = 1 To 200
            .IO.AssertTrigger
            Reading(i) = .ReadString
        Next i
    End With
    
    
        List1.Clear
        ' Display the first 10 readings
        For i = 1 To 10
            List1.AddItem Reading(i)
        Next i

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

