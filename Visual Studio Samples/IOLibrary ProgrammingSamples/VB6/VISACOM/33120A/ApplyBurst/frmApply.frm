VERSION 5.00
Begin VB.Form frmApply 
   Caption         =   "Burst Example"
   ClientHeight    =   5085
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8175
   LinkTopic       =   "Form1"
   ScaleHeight     =   5085
   ScaleWidth      =   8175
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set IO"
      Height          =   615
      Left            =   5400
      TabIndex        =   14
      Top             =   4200
      Width           =   2535
   End
   Begin VB.Frame Frame2 
      Caption         =   "Instrument Error"
      Height          =   1335
      Left            =   120
      TabIndex        =   11
      Top             =   2520
      Width           =   7935
      Begin VB.CommandButton cmdError 
         Caption         =   "Get Instrument Error"
         Height          =   495
         Left            =   5640
         TabIndex        =   13
         Top             =   480
         Width           =   1815
      End
      Begin VB.TextBox txtErrorList 
         Height          =   855
         Left            =   240
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   12
         Top             =   360
         Width           =   4815
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Configure Arb for burst"
      Height          =   2055
      Left            =   120
      TabIndex        =   1
      Top             =   240
      Width           =   4455
      Begin VB.ComboBox cboFunction 
         Height          =   315
         ItemData        =   "frmApply.frx":0000
         Left            =   2280
         List            =   "frmApply.frx":0010
         TabIndex        =   5
         Top             =   600
         Width           =   1695
      End
      Begin VB.CommandButton cmdSetUp 
         Caption         =   "Configure Arb"
         Height          =   495
         Left            =   2280
         TabIndex        =   4
         Top             =   1200
         Width           =   1695
      End
      Begin VB.ComboBox cboNumberBursts 
         Height          =   315
         ItemData        =   "frmApply.frx":0032
         Left            =   240
         List            =   "frmApply.frx":0048
         TabIndex        =   3
         Text            =   "Combo1"
         Top             =   1320
         Width           =   1455
      End
      Begin VB.TextBox txtFrequency 
         Height          =   375
         Left            =   240
         TabIndex        =   2
         Text            =   "5000"
         Top             =   480
         Width           =   1455
      End
      Begin VB.Label Label1 
         Caption         =   "Frequency"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   240
         Width           =   1455
      End
      Begin VB.Label Label2 
         Caption         =   "Function"
         Height          =   255
         Left            =   2280
         TabIndex        =   7
         Top             =   360
         Width           =   1695
      End
      Begin VB.Label Label3 
         Caption         =   "Number Bursts"
         Height          =   255
         Left            =   240
         TabIndex        =   6
         Top             =   1080
         Width           =   1455
      End
   End
   Begin VB.CommandButton cmdTrigger 
      Caption         =   "Initiate Burst"
      Enabled         =   0   'False
      Height          =   495
      Left            =   5280
      TabIndex        =   0
      Top             =   360
      Width           =   2655
   End
   Begin VB.Label Label6 
      Caption         =   "Set the scope to Normal mode to catch the single burst. Set the level to about 1V."
      Height          =   735
      Left            =   5280
      TabIndex        =   10
      Top             =   1800
      Width           =   2775
   End
   Begin VB.Label Label5 
      Caption         =   "First configure the Arb and then click on Initiate Burst to trigger one burst. "
      Height          =   615
      Left            =   5280
      TabIndex        =   9
      Top             =   1080
      Width           =   2655
   End
End
Attribute VB_Name = "frmApply"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit


' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2001, 2002 Agilent Technologies Inc. All rights
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

' This program sets up a burst waveform with a 270 degree
' starting phase. By adding an offset voltage to the burst,
' a "haversine" is created. This program also shows the use of
' a trigger received over the GPIB interface to  initiate a single
' trigger.
' To run the program, first make sure the GPIB address is set correctly
' and then click on Set Generator. To create on burst click on Trigger.


' To see this on a scope set the scope trigger to normal, DC, 1v
' and set the horizontal to 100usec/div, and the vertcal scale to
' 20V/div

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

Private Sub cmdSetUp_Click()
    Dim Frequency As String
    
    On Error Resume Next
    
    Frequency = txtFrequency.Text
    With Fgen
        .WriteString "*RST"
        .WriteString "Output:Load 50"
        Select Case UCase(cboFunction.Text)
            Case "SINE"
                .WriteString "Apply:Sin " & Frequency & ",5"
            Case "SQUARE"
                .WriteString "Apply:Square " & Frequency & ",5"
            Case "TRIANGLE"
                .WriteString "Apply:Triangle " & Frequency & ",5"
            Case "RAMP"
                .WriteString "Apply:Ramp " & Frequency & ",5"
        End Select
        .WriteString "BM:NCYC " & cboNumberBursts.Text
        .WriteString "BM:Phase 270"
        .WriteString "Volt:Offset .5"
        .WriteString "Trig:Sour Bus"
        .WriteString "BM:State On"
    End With
    
    Me.cmdTrigger.Enabled = True
    
End Sub

Private Sub cmdTrigger_Click()
    ' Trigger the function generator
    Fgen.WriteString "*TRG"
    
End Sub

Private Sub Form_Load()
    ' initialize combo box
    Me.cboFunction.ListIndex = 0
    Me.cboNumberBursts.ListIndex = 0
    
    ' initialize io
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click
End Sub

Private Sub cmdError_Click()
    ' Gets instrument error and displays them on the text box
    Dim reply As String
    
    Fgen.WriteString "syst:error?"
    reply = Fgen.ReadString
    
    txtErrorList.SelText = Left$(reply, Len(reply) - 1) & vbCrLf
    
End Sub


