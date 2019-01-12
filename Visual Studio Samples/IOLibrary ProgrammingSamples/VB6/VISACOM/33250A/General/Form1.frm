VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4590
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4590
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set IO"
      Height          =   495
      Left            =   3240
      TabIndex        =   3
      Top             =   120
      Width           =   1215
   End
   Begin VB.ListBox lstErrors 
      Height          =   1620
      Left            =   120
      TabIndex        =   1
      Top             =   1440
      Width           =   4335
   End
   Begin VB.CommandButton cmdStart 
      Caption         =   "Start"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   120
      Width           =   1215
   End
   Begin VB.Label label1 
      Caption         =   "Errors"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   13.5
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   960
      Width           =   1335
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""'
' Copyright (c) 2000-2001 Agilent Technologies.  All Rights Reserved.           '
'                                                                          '
' Agilent Technologies provides programming samples for illustration       '
' purposes only.  This sample program assumes that you are familiar        '
' with the programming language being demonstrated and the tools used      '
' to create and debug procedures.  Agilent support engineers can help      '
' answer questions relating to the functionality of the software           '
' components provided by Agilent, but they will not modify these samples   '
' to provide added functionality or construct procedures to meet your      '
' specific needs.                                                          '
' You have a royalty-free right to use, modify, reproduce, and distribute  '
' this sample program (and/or any modified version) in any way you find    '
' useful, provided that you agree that Agilent has no warranty,            '
' obligations, or liability for any sample programs.                       '
' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""'
Dim Arb As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::10"
Dim m_ioAddress As String

'  Agilent 33250A 80 MHz Function/Arbitrary Waveform Generator Examples
'
'  Examples include Modulation, Pulse, Sweeping, and Burst.
'  Examples illustrate various uses of short/long form SCPI.
'  Examples also illustrate enabling/disabling output BNCs.
'  To view results on Scope, set scope to:
'     Channel 1: Output BNC, 50ohms, 50us/div, 200mV/div
'     Channel 2: Sync BNC, 50us/div, 500mV/div, trigger on Channel 2
'
'  Microsoft Visual Basic 6.0 Programming Examples

'
Private Sub cmdStart_Click()

    Dim i As Integer                        ' Used as general purpose counter
    Dim reply As String
    
    cmdStart.Enabled = False                ' Disable Start button
'
' Return the 33250A to turn-ON conditions
'
    Arb.IO.WriteString "*RST"                   ' Default state of instrument
    Arb.IO.WriteString "*CLS"                   ' Clear errors and status
'
'  AM Modulation
'
    Arb.WriteString "OUTPut:LOAD INFinity"      ' Configure for Hi Z load
    Arb.WriteString "APPLy:SINusoid 1e6,1,0"    ' 1MHz Sine, 1Vpp, 0Vdc offset
    Arb.WriteString "AM:INTernal:FUNCtion RAMP" ' Modulating signal: Ramp
    Arb.WriteString "AM:INTernal:FREQuency 10e3" ' Modulating frequency: 10kHz
    Arb.WriteString "AM:DEPTh 80"               ' Modulating depth: 80%
    Arb.WriteString "AM:STATe ON"               ' Turn ON AM modulation
    Check_Errors                            ' Routine checks for errors
    MsgBox "AM Modulation", vbOKOnly, "33250A Example"
    Arb.WriteString "AM:STATe OFF"              ' Turn OFF AM modulation
'
'   FM Modulation
'
    Arb.WriteString "outp:load 50"              ' Configure for 50 ohm load
    Arb.WriteString "appl:sin 20e3,1,0"         ' 20kHz Sine, 1Vpp, 0Vdc Offset
    Arb.WriteString "fm:dev 20e3"               ' FM deviation: 20kHz
    Arb.WriteString "fm:int:freq 1000"          ' FM Modulating Freq: 1kHz
    Arb.WriteString "fm:stat on"                ' Turn ON FM modulation
    Check_Errors                            ' Routine checks for errors
    MsgBox "FM Modulation", vbOKOnly, "33250A Example"
    Arb.WriteString "fm:stat off"               ' Turn OFF FM modulation
'
'   Linear Sweep
'
    Arb.WriteString "sweep:time 1"              ' 1 second sweep time
    Arb.WriteString "freq:start 100"            ' Start frequency: 100Hz
    Arb.WriteString "freq:stop 20000"           ' Stop frequency: 20kHz
    Arb.WriteString "sweep:stat on"             ' Turn ON sweeping
    Check_Errors                            ' Routine checks for errors
    MsgBox "Linear Sweep", vbOKOnly, "33250A Example"
    Arb.WriteString "sweep:stat off"            ' Turn OFF sweeping
'
'   Pulse Waveform with variable Edge Times
'
    Arb.WriteString "output:state off"          ' Disable Output BNC
    Arb.WriteString "volt:low 0;:volt:high 0.75" ' Low = 0V, High = 0.75V
    Arb.WriteString "pulse:period 1e-3"         ' 1ms intervals
    Arb.WriteString "pulse:width 100e-6"        ' 100us pulse width
    Arb.WriteString "pulse:tran 10e-6"          ' Edge time 10us
    Arb.WriteString "func pulse"                ' Select Function Pulse
    Arb.WriteString "output:state on"           ' Enable Output BNC
    For i = 1 To 20                         ' Vary edge by 1usec steps
        Arb.WriteString "puls:tran " & Str$((0.00001 + i * 0.000001))
        Sleep 300                           ' Wait 300msec
    Next i
    Check_Errors                            ' Routine checks for errors
    MsgBox "Pulse Waveform with variable Edge Times", vbOKOnly, "33250A Example"
'
'   Triggered Burst
'
    Arb.WriteString "output:state off"          ' Turn OFF Output BNC
    Arb.WriteString "output:sync off"           ' Disable Sync BNC
    Arb.WriteString "func square"               ' Select Function square
    Arb.WriteString "frequency 20e3"            ' 20kHz
    Arb.WriteString "volt 1;:volt:offset 0"     ' 1Vpp and 0V offset
    Arb.WriteString "func:square:dcycle 20"     ' 20% duty cycle
    Arb.WriteString "trig:sour bus"             ' Bus triggering
    Arb.WriteString "burst:ncycles 3"           ' Burst of 3 cycles per trigger
    Arb.WriteString "burst:state on"            ' Enable Burst
    Arb.WriteString "output:state on"           ' Turn ON Output BNC
    Arb.WriteString "output:sync on"            ' Enable Sync BNC
    Check_Errors                            ' Routine checks for errors
    
    For i = 1 To 5
        Arb.WriteString "*trg"                  ' Send BUS trigger
        Sleep 100                           ' Wait 100msec
    Next i
    MsgBox "Triggered Burst", vbOKOnly, "33250A Example"
'
'   Download a 20 point Arbitrary waveform using ASCII.
'
    Dim Arb_20(0 To 19) As String           ' Allocate array of 20 strings
    Dim strData As String
    Fill_array Arb_20                       ' Call routine to fill array
    strData = Join(Arb_20, ",")
    
    With Arb
        .WriteString "Data:DAC Volatile, " & strData
        
        .WriteString "Data:Copy Pulse, Volatile"    ' Copy arb to non-volatile memory
        .WriteString "*OPC?"
        reply = .ReadString
        .WriteString "func:user volatile"        ' Select downloaded waveform
        .WriteString "Frequency 5000; Voltage 5"           ' Ouput frequency is 5kHz @ 5 Vpp
    End With
    
    Check_Errors                            ' Routine checks for errors
    MsgBox "Download a 20 point Arbitrary waveform using ASCII.", vbOKOnly, "33250A Example"
'
'   Download a 6 point Arbitrary waveform using Binary.
'   This example for GPIB only
'
    Dim Arb_6(5) As Integer                     ' Create array
    Arb_6(0) = -2047
    Arb_6(1) = 2047
    Arb_6(2) = 2047
    Arb_6(3) = -2047
    Arb_6(4) = -2047
    Arb_6(5) = -2047
    With Arb
        .WriteIEEEBlock "Data:Dac Volatile,", Arb_6
        .WriteString "*OPC?"
        reply = .ReadString
        .WriteString "apply:user 5000,1,0"       ' Output waveform: 5kHz, 1Vpp
    End With ' Arb
    Check_Errors
    MsgBox "Download a 6 point Arbitrary waveform using Binary.", vbOKOnly, "33250A Example"
'

    MsgBox "Done", vbOKOnly, "33250A "
    cmdStart.Enabled = True
End Sub


Sub Check_Errors()
    Dim ErrVal As String
    
    With Arb
        .WriteString "syst:err?"                ' Query any errors data
        ErrVal = .ReadString                     ' Read: Errnum,"Error String"
        While Val(ErrVal) <> 0                ' End if find: 0,"No Error"
            lstErrors.AddItem ErrVal        ' Display errors
            lstErrors.Refresh               ' Update the box
            .WriteString "SYST:ERR?"            ' Request error message
            ErrVal = .ReadString                  ' Read error message
        Wend
    End With
End Sub

Sub WaitForOPC()
    Dim Stats As Byte

    With Arb
        Stats = .IO.Query("*STB?")          ' Read Status Byte
    
        Do While (Stats And 64) = 0         ' Test for Master Summary Bit
            Sleep 100                       ' Pause for 100msec
            Stats = .IO.Query("*STB?")      ' Read Status Byte
        Loop
    End With
End Sub

Sub Fill_array(ByRef data_array() As String)
   
'
' Routine can be used to fill array passed from Main Program.  Fills entire array
' with sequence of +/- 1.0
'
    data_array(0) = "-1"
    data_array(1) = "1"
    data_array(2) = "-1"
    data_array(3) = "-1"
    data_array(4) = "1"
    data_array(5) = "1"
    data_array(6) = "-1"
    data_array(7) = "-1"
    data_array(8) = "-1"
    data_array(9) = "1"
    data_array(10) = "1"
    data_array(11) = "1"
    data_array(12) = "-1"
    data_array(13) = "-1"
    data_array(14) = "-1"
    data_array(15) = "-1"
    data_array(16) = "1"
    data_array(17) = "1"
    data_array(18) = "1"
    data_array(19) = "1"

End Sub
Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the Function Generator", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set Arb = New VisaComLib.FormattedIO488
    Set Arb.IO = mgr.Open(m_ioAddress)
    
    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

End Sub

