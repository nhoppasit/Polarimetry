VERSION 5.00
Begin VB.Form frmLimit_Test_SRQEvent 
   Caption         =   "Form1"
   ClientHeight    =   3570
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3570
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   495
      Left            =   2760
      TabIndex        =   6
      Top             =   2880
      Width           =   1695
   End
   Begin VB.TextBox txtData 
      Height          =   2295
      Left            =   120
      MultiLine       =   -1  'True
      TabIndex        =   4
      Top             =   1200
      Width           =   2175
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Text            =   "GPIB::4"
      Top             =   360
      Width           =   2175
   End
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   2760
      TabIndex        =   1
      Top             =   360
      Width           =   1695
   End
   Begin VB.CommandButton cmdStartReading 
      Caption         =   "Start Readings"
      Height          =   495
      Left            =   2760
      TabIndex        =   0
      Top             =   1200
      Width           =   1695
   End
   Begin VB.Label Label2 
      Caption         =   "Data"
      Height          =   255
      Left            =   120
      TabIndex        =   5
      Top             =   960
      Width           =   2175
   End
   Begin VB.Label Label1 
      Caption         =   "Address"
      Height          =   255
      Left            =   120
      TabIndex        =   3
      Top             =   120
      Width           =   2295
   End
End
Attribute VB_Name = "frmLimit_Test_SRQEvent"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Implements VisaComLib.IEventHandler
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2000-2001 Agilent Technologies Inc.  All rights reserved.
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


'*************************************************************
' The following example shows how you can use the counter's status
' registers to determine when a command sequence is completed.
'
'*************************************************************
'This program sets up the counter to make period measurements
'indefinitely until an out of limits measurement occurs. The upper
'limit is set to 1.05 ms and the lower limit is set to .95 ms.
'If a measurement falls outside of these limits, the counter will
'stop measuring and send the out of limits period to the computer.
'The out of limit period is sent in ASCII format to preserve resolution.
'*************************************************************

'##########################################################################
' NOTE: This Example uses 'Implements VisaComLib.IEventHandler'
'##########################################################################
'
' Sequence of Operation;
'   1. The counter is cleared and set to give an SRQ when its
'      operation is complete
'   2. Enable the GPIB port to look for an SRQ so we can
'      get an SRQ event. This is done during initializing the
'      IO in the subroutine cmdSetIO_Click
'   3. We start the reading with INIT. This will put the
'      data into memory.  When the meter is finished, it
'      will set SRQ.
'   4. When 'IEventHandler_HandleEvent' event fires, then get the reading from the
'      counter with the routine ReadData.
'
'' Use this to enable the event handler
Dim SRQ As VisaComLib.IEventManager

' This represents the 34401A
Dim Cntr As VisaComLib.FormattedIO488


Private Sub cmdClose_Click()
    Unload Me
End Sub

Private Sub cmdStartReading_Click()
    ' Call the routine that sets up the meter

    cmdStartReading.Enabled = False

    startReadings


    cmdStartReading.Enabled = True
End Sub

Private Sub startReadings()

    Dim Upper As String
    Dim Lower As String
    
    On Error GoTo StartReadingsError
    
    ' Clear out text box for the data so we can see
    ' when new data arrives
    txtData.Text = ""
    txtData.Refresh

    resetCounter
    
    Upper = ".00105"
    Lower = ".00095"

    '"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
    ' Configure the counter to take readings
    ' and initiate the readings (source is set to immediate by default)
    With Cntr
        .WriteString ":FUNC ""PER 1"""    ' Measure Period on channel 1
        ' Note that the function selected must be a quoted string
        ' The actual string sent to the counter is "PER 1"

        .WriteString ":Freq:Arm:Star:Sour IMM"    ' these two lines enable Automatic arming
        .WriteString ":Freq:Arm:Stop:Sour IMM"

        .WriteString ":CALC2:Lim:Stat ON"    ' Enable limit testing
        .WriteString ":CALC2:Lim:Disp GRAP"    ' Show the analog limit graph
        .WriteString ":CALC2:Lim:Lower " & Lower    ' Set the lower limit
        .WriteString ":CALC2:Lim:Upper " & Upper    ' Set the Upper Limit
        .WriteString ":INIT:Auto 1"    ' Stop when out of Limit

        .WriteString ":Stat:Ques:Enab 1024"    ' 1024 is out of limit bit Enable SRQ on
        ' questionable data register event
        .WriteString "*SRE 8"    ' Enable SRQ on questionable data register event
        ' event registers when measurement is complete
    End With
    '""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

    ' give message that meter is initialized
    ' give message that configuration is done
    txtData.Refresh
    txtData.Text = "Meter configured and " & vbCrLf & _
            "Initialized"
    
    Cntr.WriteString ":INIT:CONT ON"

    Exit Sub

StartReadingsError:
    Debug.Print "Start Readings Error = "; Err.Description
End Sub


Private Sub ReadData()
    ' Once the SRQ is detected, this routine will
    ' get the data from the meter
    ' Called by: PollForSRQTimer_Timer
    '
    Dim reading As String
    Dim i As Long

    On Error GoTo ReadDataError


    With Cntr
        .WriteString "Fetch:Period?"    ' Query for the data in memory
        reading = .ReadString    ' get the data as a string
    End With

    ' Insert data into text box
    txtData.Text = reading

    Exit Sub

ReadDataError:
    Debug.Print "ReadData Error = "; Err.Description

End Sub

Private Sub resetCounter()
    '
    With Cntr
        .WriteString "*RST"     'Reset counter
        .WriteString "*CLS"    'Clear event registers and error queue
        .WriteString "*SRE 0"    'Clear service request enable register
        .WriteString "*ESE 0"    'Clear event status enable register
        .WriteString ":STAT:PRES"    'Preset enable registers and transition
    End With

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
        
    '""""""""""""""""""""""""""""""""""""""""""""""""""
    ' set SRQ to the session and
    ' Enable the SRQ
    Set SRQ = Cntr.IO
    SRQ.InstallHandler EVENT_SERVICE_REQ, Me
    SRQ.EnableEvent EVENT_SERVICE_REQ, EVENT_HNDLR

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub

Private Sub Form_Load()
    cmdSetIO_Click
End Sub

' Note; on version 1.0 of VISA COM change the argument name event to avoid using a key word
' In this example the argument 'event' was changed to 'SRQevent'
Private Sub IEventHandler_HandleEvent(ByVal vi As VisaComLib.IEventManager, ByVal SRQevent As VisaComLib.IEvent, ByVal userHandle As Long)
    Debug.Print "SRQ event fired, getting data"
    txtData.SelText = vbCrLf & "SRQ fired, getting data " & Time() & vbCrLf
    ReadData

End Sub

