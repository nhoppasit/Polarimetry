VERSION 5.00
Begin VB.Form frmAMmod 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   240
      TabIndex        =   3
      Top             =   360
      Width           =   1575
   End
   Begin VB.CommandButton cmdRecallState 
      Caption         =   "Recall State"
      Height          =   495
      Left            =   2880
      TabIndex        =   2
      Top             =   1920
      Width           =   1575
   End
   Begin VB.CommandButton cmdSave 
      Caption         =   "Save state"
      Height          =   495
      Left            =   2880
      TabIndex        =   1
      Top             =   1320
      Width           =   1575
   End
   Begin VB.CommandButton cmdAM 
      Caption         =   "Set AM"
      Height          =   495
      Left            =   2880
      TabIndex        =   0
      Top             =   360
      Width           =   1575
   End
End
Attribute VB_Name = "frmAMmod"
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


' This program uses low-level SCPI commands to configure
' the function generator to output an AM waveform.
' This program also shows how to use "state storage" to
' store the instrument configuration in memory.

Dim Fgen As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::10"
Dim m_ioAddress As String

Private Sub cmdAM_Click()
    
    With Fgen
        .WriteString "*RST"                      ' reset instrument
        .WriteString "Output:load 50"           ' set output termination to 50 ohm
        .WriteString "Function:Shape Sinusoid" ' set carrier waveshape to sine
        .WriteString "Frequency 5000; Volt 5"   ' set carrier frequency to 5kHz, @ 5Vpp
        .WriteString "AM:Internal:Function Sinusoid" ' modulation to sine
        .WriteString "AM:Internal:Frequency 200" ' Modulation frequency to 200Hz
        .WriteString "AM:State On"              ' turn AM modulation on
    End With
End Sub

Private Sub cmdRecallState_Click()
    Fgen.WriteString "*RCL 1"
End Sub

Private Sub cmdSave_Click()
    Fgen.WriteString "*Sav 1"
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
