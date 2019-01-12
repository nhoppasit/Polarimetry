VERSION 5.00
Begin VB.Form frmStatReg 
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
      ScrollBars      =   2  'Vertical
      TabIndex        =   4
      Top             =   1200
      Width           =   2175
   End
   Begin VB.Timer tmrPollForSRQ 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   3720
      Top             =   1920
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Text            =   "GPIB::22"
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
Attribute VB_Name = "frmStatReg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2002 Agilent Technologies Inc.  All rights reserved.
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
'
' NOTE: Polling will work with a GPIB interface. It does not
'       work for RS232
'
' Sequence of Operation;
'   1. The meter is cleared and set to give an SRQ when its
'      operation is complete
'   2. The meter is set for dc, and multiple readings. This will
'      take about 3 seconds for 10 readings
'   3. We start the reading with INIT. This will put the
'      data into memory.  When the meter is finished, it
'      will set SRQ.
'   4. Enable the timer that polls for SRQ every second.
'   5. When SRQ is detected, then get the reading from the
'      meter with the routine ReadData.
' Monitor the Immediate window to see every time a polling takes place.
'
'Created on:   11/19/02
'Module:       frmStatReg
'Project:      StatReg_SRQ
'*************************************************************
Dim statusValue As Byte
Dim numberReadings As Long

Dim DMM As VisaComLib.FormattedIO488


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

    Dim Average As Double
    Dim MinReading As Double
    Dim MaxReading As Double
    Dim strTemp  As String
    Dim Value As Integer
    Dim Mask As Integer
    Dim Task As Integer
    
    On Error GoTo StartReadingsError
    
    ' Clear out text box for the data so we can see
    ' when new data arrives
    txtData.Text = ""
    
    
    '""""""""""""""""""""""""""""""""""""""""""""""""""
    ' Setup dmm to return an SRQ event when readings are complete
    With DMM
        .WriteString "*RST"          ' Reset dmm
        .WriteString "*CLS"          ' Clear dmm status registers
        .WriteString "*ESE 1"        ' Enable 'operation complete bit to
                                '  set 'standard event' bit in status byte
        .WriteString "*SRE 32"       ' Enable 'standard event' bit in status
                                '  byte to pull the IEEE-488 SRQ line
        .WriteString "*OPC?"         ' Assure syncronization
        strTemp = .ReadString
    End With
    
    
    '"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
    ' Configure the meter to take readings
    ' and initiate the readings (source is set to immediate by default)
    numberReadings = 10
    With DMM
        .WriteString "Configure:Voltage:dc 10"   ' set dmm to 10 volt dc range"
        .WriteString "Voltage:DC:NPLC 10"        ' set integration time to 10 Power line cycles (PLC)"
        .WriteString "Trigger:count" & Str$(numberReadings) ' set dmm to accept multiple triggers
        .WriteString "Init"                      ' Place dmm in 'wait-for-trigger' state
        .WriteString "*OPC"                      ' Set 'operation complete' bit in standard
                                            ' event registers when measurement is complete
    End With
    
    '""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
    
    ' give message that meter is initialized
    ' give message that configuration is done
    txtData.SelText = "Meter configured and " & vbCrLf & _
                    "Initialized" & vbCrLf
    
    ' enable the timer to poll gpib once per second
    tmrPollForSRQ.Enabled = True
    

    Exit Sub
    
StartReadingsError:
    Debug.Print "Start Readings Error = "; Err.Description
End Sub

Private Sub tmrPollForSRQ_Timer()
    ' This timer when enabled will poll the GPIB
    ' and return the status byte to indicate if SRQ is set
    
    On Error GoTo pollerror
    
    ' read the status byte
    statusValue = 0
    statusValue = DMM.IO.ReadSTB
    
    ' Test for the SRQ bit
    If statusValue And 64 Then       ' SRQ from Operation complete
        ' Turn off the timer and stop polling.
        tmrPollForSRQ.Enabled = False
        Debug.Print "SRQ is set, getting data"
        ' Get the Data, the meter is ready
        txtData.SelText = "Getting Data" & vbCrLf
        ReadData
    Else
        Debug.Print "Test for SRQ, SRQ not fired"
    End If
    
    Exit Sub
    
pollerror:
     Debug.Print "No SRQ yet, Poll error = "; Err.Description

End Sub

Private Sub ReadData()
' Once the SRQ is detected, this routine will
' get the data from the meter
' Called by: PollForSRQTimer_Timer
'
    Dim readings() As Variant
    Dim i As Long

    On Error GoTo ReadDataError

    ' dimension the array for the number of readings
    ReDim readings(numberReadings - 1)

    With DMM
        .WriteString "Fetch?"                              ' Query for the data in memory
        readings = .ReadList                               ' get the data and parse into the array
    End With

    ' Insert data into text box
    For i = 0 To numberReadings - 1
        txtData.SelText = vbCrLf & Format$(readings(i)) & " Vdc"
    Next i

    Exit Sub

ReadDataError:
    Debug.Print "ReadData Error = "; Err.Description

End Sub



Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim sfc As VisaComLib.ISerial
    Dim ioAddress As Variant
    
    On Error GoTo ioError
    ioAddress = txtAddress.Text
    ioAddress = InputBox("Enter the IO address of the DMM", "Set IO address", ioAddress)
 
    txtAddress.Text = ioAddress
    Set mgr = New VisaComLib.ResourceManager
    Set DMM = New VisaComLib.FormattedIO488
    Set DMM.IO = mgr.Open(ioAddress)

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    cmdSetIO_Click

End Sub



