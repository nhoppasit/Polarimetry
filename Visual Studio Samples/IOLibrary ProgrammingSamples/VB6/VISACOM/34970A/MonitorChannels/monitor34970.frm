VERSION 5.00
Begin VB.Form frmMonitor 
   Caption         =   "Form1"
   ClientHeight    =   2925
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6450
   LinkTopic       =   "Form1"
   ScaleHeight     =   2925
   ScaleWidth      =   6450
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set IO"
      Height          =   495
      Left            =   4680
      TabIndex        =   7
      Top             =   120
      Width           =   1575
   End
   Begin VB.Frame Frame1 
      Caption         =   "Monitor"
      Height          =   1935
      Left            =   120
      TabIndex        =   0
      Top             =   840
      Width           =   6255
      Begin VB.TextBox txtMonitor 
         Height          =   1095
         Left            =   240
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   4
         Top             =   600
         Width           =   2415
      End
      Begin VB.CommandButton cmdRefreshChannels 
         Caption         =   "Refresh Channels"
         Height          =   495
         Left            =   4560
         TabIndex        =   3
         Top             =   1200
         Width           =   1455
      End
      Begin VB.CommandButton cmdMonitor 
         Caption         =   "Monitor"
         Enabled         =   0   'False
         Height          =   495
         Left            =   4560
         TabIndex        =   2
         Top             =   600
         Width           =   1455
      End
      Begin VB.ListBox lstChannel 
         Height          =   1035
         ItemData        =   "monitor34970.frx":0000
         Left            =   2760
         List            =   "monitor34970.frx":0007
         TabIndex        =   1
         Top             =   600
         Width           =   1455
      End
      Begin VB.Label Label1 
         Caption         =   "Select channel"
         Height          =   255
         Left            =   2760
         TabIndex        =   6
         Top             =   360
         Width           =   1575
      End
      Begin VB.Label Label2 
         Caption         =   "Channel   Reading"
         Height          =   255
         Left            =   240
         TabIndex        =   5
         Top             =   360
         Width           =   2055
      End
   End
End
Attribute VB_Name = "frmMonitor"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2001 Agilent Technologies Inc.  All rights reserved.
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
'' You must have a scan list set in the instrument for this example to work
''
'' This example demonstrates how to read the scan list, and how to monitor
'' a channel that is on the scan list.
'' The example also demonstrates how to query the instrument for instrument
'' errors.

Dim Daq As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::9"
Dim m_ioAddress As String


Private Sub cmdMonitor_Click()
    Dim channel As String

    If isconnected Then
        channel = MonitorChannel
        Monitor Daq, txtMonitor, channel
        InstrumentError Daq
    End If

End Sub


Private Sub cmdRefreshChannels_Click()
    ' gets the scan list from instrument
    ' and checks instrument for errors

    Me.MousePointer = vbHourglass

    If isconnected Then

        GetScanList Daq, lstChannel
        InstrumentError Daq
    End If
    
    Me.cmdMonitor.Enabled = True

    Me.MousePointer = vbDefault

End Sub
Private Function MonitorChannel() As String
    ' returns the user selected channel to monitor
    Dim index As Long

    With lstChannel
        index = .ListIndex
        If index > 0 Then
            MonitorChannel = Right$(.List(index), 3)
        Else
            MonitorChannel = "Front Panel"
            Exit Function
        End If
    End With

End Function



'"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
Private Function isconnected() As Boolean
    ' determines if the instrument is connected, and
    ' gives a message if not
    ' Checks to make sure the instrument is a 34970a
    Dim result() As Variant
    Dim oldTimeout As Long

    On Error GoTo connectError

    With Daq
        ' use a very short timeout so we don't have to wait long
        ' if there is an error
        oldTimeout = .io.Timeout
        .io.Timeout = 1000
        .WriteString "*IDN?"
        result = .ReadList
        .io.Timeout = oldTimeout
    End With

    If InStr(1, result(1), "34970A", vbTextCompare) = 0 Then
        GoTo WrongInstrumentError
    End If

    isconnected = True

    Exit Function

connectError:
    Daq.io.Timeout = 10000
    MsgBox "Instrument not connected. Please check connections and use the Set I/O toolbar button to set the instrument connection."
    isconnected = False
    Exit Function
WrongInstrumentError:
    MsgBox "Incorrect instrument: " & vbCrLf & _
            "Expected 34970A " & vbCrLf & _
            "Instrument discovered: " & result(1)
    isconnected = False
End Function

Public Sub InstrumentError(io As VisaComLib.FormattedIO488)
    ' retrieves the instrument error and displays a messageBox
    ' if there is an error.
    Dim reply As String

    With io
        .WriteString "Syst:Error?"
        reply = .ReadString
    End With

    If Val(reply) <> 0 Then
        MsgBox "Instrument Error: " & vbCrLf & reply, vbCritical
        InstrumentError io
    End If

End Sub
Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the 34970A", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set Daq = New VisaComLib.FormattedIO488
    Set Daq.io = mgr.Open(m_ioAddress)
    
    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

End Sub



