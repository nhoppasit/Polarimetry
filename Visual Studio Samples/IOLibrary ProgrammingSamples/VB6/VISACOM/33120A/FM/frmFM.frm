VERSION 5.00
Begin VB.Form frmFM 
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
   Begin VB.CommandButton cmdFM 
      Caption         =   "Set FM"
      Height          =   495
      Left            =   2880
      TabIndex        =   0
      Top             =   360
      Width           =   1575
   End
End
Attribute VB_Name = "frmFM"
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

' This program demonstrates how to program the instrument
' output for an FM waveform.
' Also demonstrates how to store and recall an instrument setup

Private Sub cmdFM_Click()
    Dim SRQMask As Integer
    Dim Value As Integer
    
    SRQMask = 2                             ' bit 1 sets SRQ
    
    With Fgen
        .WriteString "*RST"                      ' reset instrument
                                            
        ' Set up function generator to output FM waveform
        .WriteString "Output:load 50"            ' set output termination to 50 ohm
        .WriteString "Apply:Sinusoid 500,5"      ' set carrier waveshape to sine, 5kHz, 5Vpp
        .WriteString "FM:Internal:Function Sin"  ' modulation to sine
        .WriteString "FM:Internal:Frequency 200" ' Modulation frequency to 200Hz
        .WriteString "FM:Deviation 250"          ' Frequency deviation 250Hz
        .WriteString "FM:State On"               ' turn FM modulation on
        
        .WriteString "*OPC?"                     ' Send a "1" to output buffer when complete
        Value = .ReadString                      ' When this returns, it means the instrument is done
                                                 ' with the setup
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

