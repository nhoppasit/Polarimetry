VERSION 5.00
Begin VB.Form frmArb 
   Caption         =   "Rise"
   ClientHeight    =   3930
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5550
   LinkTopic       =   "Form1"
   ScaleHeight     =   3930
   ScaleWidth      =   5550
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set IO"
      Height          =   615
      Left            =   3720
      TabIndex        =   9
      Top             =   240
      Width           =   1695
   End
   Begin VB.TextBox txtPulse 
      Alignment       =   2  'Center
      Height          =   375
      Index           =   2
      Left            =   360
      TabIndex        =   5
      Text            =   "15"
      Top             =   2040
      Width           =   1095
   End
   Begin VB.TextBox txtPulse 
      Alignment       =   2  'Center
      Height          =   375
      Index           =   1
      Left            =   360
      TabIndex        =   3
      Text            =   "200"
      Top             =   1200
      Width           =   1095
   End
   Begin VB.TextBox txtPulse 
      Alignment       =   2  'Center
      Height          =   375
      Index           =   0
      Left            =   360
      TabIndex        =   1
      Text            =   "50"
      Top             =   360
      Width           =   1095
   End
   Begin VB.CommandButton cmdLoadWaveform 
      Caption         =   "Download Pulse"
      Height          =   615
      Left            =   1800
      TabIndex        =   0
      Top             =   240
      Width           =   1695
   End
   Begin VB.Label Label5 
      Caption         =   "Calculate the time per point as the period of the frequency divided by the number of points (4000) in the array"
      Height          =   735
      Left            =   1800
      TabIndex        =   8
      Top             =   1920
      Width           =   3495
   End
   Begin VB.Label Label4 
      Caption         =   "The pulse parameters are expressed as number of points. Each point is 0.5usec @ 5kHz, 4000pts in array."
      Height          =   735
      Left            =   1800
      TabIndex        =   7
      Top             =   1080
      Width           =   3495
   End
   Begin VB.Label Label3 
      Caption         =   "Fall time"
      Height          =   255
      Left            =   360
      TabIndex        =   6
      Top             =   1800
      Width           =   1095
   End
   Begin VB.Label Label2 
      Caption         =   "Pulse top width"
      Height          =   255
      Left            =   360
      TabIndex        =   4
      Top             =   960
      Width           =   1095
   End
   Begin VB.Label Label1 
      Caption         =   "Rise Time"
      Height          =   255
      Left            =   360
      TabIndex        =   2
      Top             =   120
      Width           =   1095
   End
End
Attribute VB_Name = "frmArb"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
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
' See the ArbDampedSine example for downloading as binary data
Dim Fgen As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::10"
Dim m_ioAddress As String

Private Sub cmdLoadWaveform_Click()
    Dim strData As String
    
    Me.cmdLoadWaveform.Enabled = False
    
    MousePointer = vbHourglass
    
    makePulse txtPulse(0), txtPulse(1), txtPulse(2), strData
    ' reset instrument
    With Fgen
        .WriteString "*RST"
        
        ' set timeout large enough to sent all data
        .IO.Timeout = 40000
        ' download  data points to volatile memory from array
        .WriteString "Data Volatile, " & strData
        
        .WriteString "Data:Copy Pulse, Volatile"    ' Copy arb to non-volatile memory
        
        .WriteString "Function:User Pulse"          ' Select the active arb waveform
        .WriteString "Function:Shape User"          ' output selected arb waveform
        .WriteString "Output:Load 50"               ' Output termination is 50 ohms
        .WriteString "Frequency 5000; Voltage 5"    ' Ouput frequency is 5kHz @ 5 Vpp
    End With
    
    ' the arb will require some time to set everything up at this point
    
    MousePointer = vbDefault
    Me.cmdLoadWaveform.Enabled = True
    
End Sub

Sub makePulse(ByVal riseTime As Long, ByVal TopWidth As Long, ByVal fallTime As Long, Data As String)
    Dim Waveform(1 To 4000) As String
    Dim topStart As Long
    Dim topStop As Long
    Dim endPulse As Long
    Dim i As Long
    
    topStart = riseTime
    topStop = topStart + TopWidth
    endPulse = topStop + fallTime
    
    ' Set rise time
    For i = 1 To riseTime
        Waveform(i) = Str$((i - 1) / riseTime)
    Next i
    
    ' set pulse width
    For i = riseTime + 1 To topStop
        Waveform(i) = "1"
    Next i
    
    ' set fall time
    For i = topStop + 1 To endPulse
        Waveform(i) = Str$((endPulse - i) / fallTime)
    Next i
    
    ' set zero level for rest of points
    For i = endPulse + 1 To 4000
        Waveform(i) = "0"
    Next i
    
    Data = Join(Waveform, ",")

End Sub

Private Sub txtPulse_KeyPress(Index As Integer, KeyAscii As Integer)
    Select Case KeyAscii
        Case Is < 32
        Case 48 To 57
        Case Else
            KeyAscii = 0
    End Select
End Sub
Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the Arb", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set Fgen = New VisaComLib.FormattedIO488
    Set Fgen.IO = mgr.Open(m_ioAddress)
    
    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

End Sub

