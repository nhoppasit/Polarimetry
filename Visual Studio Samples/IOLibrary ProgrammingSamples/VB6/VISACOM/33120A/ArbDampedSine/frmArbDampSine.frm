VERSION 5.00
Begin VB.Form frmArbSine 
   Caption         =   "Damped sine wave/binary download"
   ClientHeight    =   3345
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5745
   LinkTopic       =   "Form1"
   ScaleHeight     =   3345
   ScaleWidth      =   5745
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   3720
      TabIndex        =   6
      Top             =   240
      Width           =   1935
   End
   Begin VB.TextBox txtNumberCycles 
      Alignment       =   2  'Center
      Height          =   375
      Left            =   360
      TabIndex        =   3
      Text            =   "10"
      Top             =   1200
      Width           =   1095
   End
   Begin VB.TextBox txtFactor 
      Alignment       =   2  'Center
      Height          =   375
      Left            =   360
      TabIndex        =   1
      Text            =   "-5"
      Top             =   360
      Width           =   1095
   End
   Begin VB.CommandButton cmdLoadWaveform 
      Caption         =   "Download Waveform"
      Height          =   495
      Left            =   3720
      TabIndex        =   0
      Top             =   840
      Width           =   1935
   End
   Begin VB.Label Label3 
      Caption         =   "Be sure you have an instrument connection before downloading data.   Default I/O is set to COM1"
      Height          =   615
      Left            =   1680
      TabIndex        =   7
      Top             =   1560
      Width           =   3855
   End
   Begin VB.Label lblMsg 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   615
      Left            =   240
      TabIndex        =   5
      Top             =   2520
      Width           =   5295
   End
   Begin VB.Label Label2 
      Caption         =   "Number of cycles"
      Height          =   255
      Left            =   360
      TabIndex        =   4
      Top             =   960
      Width           =   1935
   End
   Begin VB.Label Label1 
      Caption         =   "Damping factor (must be negative)"
      Height          =   255
      Left            =   360
      TabIndex        =   2
      Top             =   120
      Width           =   2655
   End
End
Attribute VB_Name = "frmArbSine"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2001 Agilent Technologies Inc. All rights
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
Dim Fgen As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::10"
Dim m_ioAddress As String


' This program uses the arbitrary waveform function to
' download and output a calculated damped sine with
' The waveform consists of 16000
' points downloaded to the function generator as binary data.

' This program will work with the 33120A and 33250A

' RS232
' This program will work with RS232. To make RS232 work
 ' Notice the delay added before the query *OPC?

' To prevent timeout, set the timeout large enough
' to allow sending all the data.
' At a baud rate of 9600, it takes about 1.1 ms per character

Private Sub cmdLoadWaveform_Click()
    Dim WaveformData As Variant
    Dim reply As String
    Dim oldTimeout As Long
    Dim Addr As String

    Me.cmdLoadWaveform.Enabled = False

    MousePointer = vbHourglass

    lblMsg.Caption = "Making damped sine wave."
    lblMsg.Refresh

    makeDampedSine Me.txtFactor, txtNumberCycles, WaveformData

    ' reset instrument
    With Fgen
        lblMsg.Caption = "Initializing I/O and Instrument."
        lblMsg.Refresh

        .WriteString "*RST"

        Addr = .IO.ResourceName
        
        ' be sure to send this for RS232 only
        If InStr(1, Addr, "ASRL", vbTextCompare) Then
            .WriteString "syst:rem"
        End If

        .WriteString "Output:Load 50"                      ' Output termination is 50 ohms

        lblMsg.Caption = "Downloading data."
        lblMsg.Refresh

        ' download  data points to volatile memory from array
        If InStr(1, Addr, "ASRL", vbTextCompare) Then
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' RS232 --- Do it this way for RS232
            Dim strArray() As String
            Dim DataString As String
            Dim i As Long
            ReDim strArray(1 To UBound(WaveformData))

            ' convert the integer array to strings
            For i = 1 To UBound(strArray)
                strArray(i) = WaveformData(i)
            Next i
            DataString = Join(strArray, ",")
            '  It takes about 1.1 ms per character to load the data at 9600 baud
            ' calculate a time out and add 10 sec margin
            .IO.Timeout = 1.1 * (Len(DataString) + 20) + 10000
            .WriteString "Data:DAC Volatile, " & DataString
            .IO.Timeout = 20000
            delay 1000

        Else
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' GPIB --- Do it this way for GPIB
            ' To send an IEEE Block (fastest way)
            .WriteIEEEBlock "Data:Dac Volatile,", WaveformData
            .WriteString "*OPC?"
            reply = .ReadString
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        End If

        .WriteString "*OPC?"                               ' request completion bit
        reply = .ReadString

        lblMsg.Caption = "Copying to Non-Volatile memory."
        lblMsg.Refresh

        .WriteString "Data:Copy DampSine, Volatile"        ' Copy arb to non-volatile memory

        .WriteString "Function:User DampSine"              ' Select the active arb waveform
        .WriteString "Function:Shape User"                 ' output selected arb waveform

        ' Be sure the timeout is set high enough to allow completion by instrument
        delay 1000
        .WriteString "*OPC?"                               ' request completion bit
        reply = .ReadString

        ' The voltage value is correct with a 50 ohm load
        .WriteString "Frequency 5000; Voltage 5"           ' Ouput frequency is 5kHz @ 5 Vpp

        ' when it gets here the instrument is done

    End With

    lblMsg.Caption = "Done"
    MousePointer = vbDefault
    Me.cmdLoadWaveform.Enabled = True

End Sub

Sub makeDampedSine(ByVal DampFactor As Long, ByVal NumberCycles As Long, _
                     WaveformData As Variant)
    Dim TempNumber As Single
    Dim tempInteger As Integer
    Dim lastPoint As Long
    Dim TwoPiF As Double
    Dim Waveform() As Integer
    Dim i As Long
    
    Const pi = 3.1416
    Const MaxIndex = 16000
    
    ' size the array for the data
    ReDim Waveform(1 To MaxIndex)
    
    lastPoint = UBound(Waveform) * 0.875
    TwoPiF = 2 * pi * NumberCycles / lastPoint
    
    ' Set rise time
    For i = 1 To lastPoint
        TempNumber = Sin(TwoPiF * i) * 2047
        Waveform(i) = CInt(TempNumber * Exp(DampFactor * i / lastPoint))
    Next i
    
    For i = i To UBound(Waveform)
        Waveform(i) = CInt(0)
    Next i
    
    WaveformData = Waveform
    
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
    Dim sfc As VisaComLib.ISerial
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the Function Generator", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set Fgen = New VisaComLib.FormattedIO488
    Set Fgen.IO = mgr.Open(m_ioAddress)
    ' Set RS232 parameters
    If InStr(1, Fgen.IO.ResourceName, "ASRL", vbTextCompare) Then
        Set sfc = Fgen.IO
        sfc.BaudRate = 9600
        sfc.FlowControl = ASRL_FLOW_DTR_DSR
    End If   
    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

End Sub
