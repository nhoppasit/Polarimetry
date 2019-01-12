VERSION 5.00
Begin VB.Form frmScanner 
   Caption         =   "34970A Code Wizard Example"
   ClientHeight    =   5895
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6555
   LinkTopic       =   "Form1"
   ScaleHeight     =   5895
   ScaleWidth      =   6555
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   4680
      TabIndex        =   10
      Top             =   120
      Width           =   1455
   End
   Begin VB.Frame Frame2 
      Caption         =   "Scan Channels"
      Height          =   2775
      Left            =   120
      TabIndex        =   4
      Top             =   840
      Width           =   6255
      Begin VB.TextBox txtScanData 
         Height          =   1455
         Left            =   240
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   6
         Text            =   "frmScanner.frx":0000
         Top             =   1080
         Width           =   5775
      End
      Begin VB.CommandButton cmdStartScan 
         Caption         =   "Start Scan"
         Height          =   495
         Left            =   4560
         TabIndex        =   5
         Top             =   360
         Width           =   1455
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Monitor"
      Height          =   1935
      Left            =   120
      TabIndex        =   0
      Top             =   3840
      Width           =   6255
      Begin VB.ListBox lstChannel 
         Height          =   1035
         ItemData        =   "frmScanner.frx":0006
         Left            =   2760
         List            =   "frmScanner.frx":000D
         TabIndex        =   7
         Top             =   600
         Width           =   1455
      End
      Begin VB.CommandButton cmdMonitor 
         Caption         =   "Monitor"
         Enabled         =   0   'False
         Height          =   495
         Left            =   4560
         TabIndex        =   3
         Top             =   600
         Width           =   1455
      End
      Begin VB.CommandButton cmdRefreshChannels 
         Caption         =   "Refresh Channels"
         Height          =   495
         Left            =   4560
         TabIndex        =   2
         Top             =   1200
         Width           =   1455
      End
      Begin VB.TextBox txtMonitor 
         Height          =   1095
         Left            =   240
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   1
         Top             =   600
         Width           =   2415
      End
      Begin VB.Label Label2 
         Caption         =   "Channel   Reading"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   360
         Width           =   2055
      End
      Begin VB.Label Label1 
         Caption         =   "Select channel"
         Height          =   255
         Left            =   2760
         TabIndex        =   8
         Top             =   360
         Width           =   1575
      End
   End
End
Attribute VB_Name = "frmScanner"
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
' This example program calls the module DAQ_34970A to set a scan and
' then get the readings from the instrument. The module DAQ_34970.bas
' was created by the 34970A Code Wizard VB6.0 add in. The module was modified
' to call a routine that places data onto the form.

' This code requires a MUX module in slot 1
'
' It demonstrates how to monitor channels set up on the 34970A by calling the
' channel list, allowing the user to select one of the channels, and then getting
' a reading (Monitor) from that channel.
'
Dim Daq As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::9"
Dim m_ioAddress As String

Public Sub WriteDataToForm(Readings() As Double, _
                            Units() As String, _
                            ChanNumb() As Integer, _
                            Time() As Date, _
                            ByVal Sweep As Integer, _
                            ByVal numberChannels As Integer)
    ' A call to this routine was added to the code developed by the
    ' 34970A code Wizard
    ' This code will fill out the scan text box every time it is executed.

    ' If you do not use the Units, Channel annotation, or Time when making
    ' new code, you will have to modify this code.
    '
    Dim i As Long
    Dim lineUnits As String
    Dim lineChannels As String
    
    lineUnits = ""
    lineChannels = ""
    If Sweep = 1 Then
        For i = 1 To numberChannels
            ' We put a heading only for the first sweep
            lineUnits = lineUnits & Units(1, i) & vbTab
            lineChannels = lineChannels & Str$(ChanNumb(1, i)) & vbTab
        Next i
        txtScanData.SelText = lineUnits & vbCrLf & lineChannels & vbCrLf
    End If

    For i = 1 To numberChannels
        Me.txtScanData.SelText = Format$(Readings(Sweep, i), "0.0e+0") & vbTab
    Next i
    Me.txtScanData.SelText = vbCrLf


End Sub

Private Sub cmdMonitor_Click()
    Dim channel As String

    If isconnected Then
        channel = MonitorChannel
        Monitor Daq, txtMonitor, channel
        InstrumentError Daq
    End If

End Sub

Private Sub cmdRefreshChannels_Click()
    
    Me.MousePointer = vbHourglass

    If isconnected Then
        
        GetScanList Daq, lstChannel
        InstrumentError Daq
    End If
    Me.cmdMonitor.Enabled = True
    
        Me.MousePointer = vbDefault

End Sub


Private Sub cmdStartScan_Click()
    
    Me.MousePointer = vbHourglass
    
    If isconnected Then
        Me.txtScanData.Text = ""
        main_34970A Daq
        InstrumentError Daq
        cmdRefreshChannels_Click
    End If
    
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



Public Sub InstrumentError(Instrument As VisaComLib.FormattedIO488)
    Dim reply As String

    With Instrument
        .WriteString "Syst:Error?"
        reply = .ReadString
    End With

    If Val(reply) <> 0 Then
        MsgBox "Instrument Error: " & vbCrLf & reply, vbCritical
        InstrumentError Instrument
    End If

End Sub


Private Function isconnected() As Boolean
    ' determines if the instrument is connected, and
    ' gives a message if not
    Dim result() As Variant
    Dim oldTimeout As Long

    On Error GoTo connectError

    With Daq
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

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the Function Generator", "Set IO address", m_ioAddress)

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



