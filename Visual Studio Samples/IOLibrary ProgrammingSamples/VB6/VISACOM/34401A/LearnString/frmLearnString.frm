VERSION 5.00
Begin VB.Form frmLearnString 
   Caption         =   "Form1"
   ClientHeight    =   2730
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5220
   LinkTopic       =   "Form1"
   ScaleHeight     =   2730
   ScaleWidth      =   5220
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtModel 
      Height          =   375
      Left            =   240
      TabIndex        =   5
      Text            =   "Text1"
      Top             =   1680
      Width           =   2535
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   240
      TabIndex        =   4
      Top             =   600
      Width           =   2655
   End
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   3360
      TabIndex        =   3
      Top             =   480
      Width           =   1575
   End
   Begin VB.CommandButton cmdSendSettings 
      Caption         =   "Send Settings"
      Height          =   495
      Left            =   3360
      TabIndex        =   1
      Top             =   2040
      Width           =   1575
   End
   Begin VB.CommandButton cmdGetSettings 
      Caption         =   "Get Settings"
      Height          =   495
      Left            =   3360
      TabIndex        =   0
      Top             =   1440
      Width           =   1575
   End
   Begin VB.Label label2 
      Caption         =   "Instrument"
      Height          =   375
      Left            =   240
      TabIndex        =   6
      Top             =   1320
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "Address"
      Height          =   255
      Left            =   240
      TabIndex        =   2
      Top             =   240
      Width           =   2055
   End
End
Attribute VB_Name = "frmLearnString"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2000 Agilent Technologies Inc. All rights
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
''' -------------------------------------------------------------------------
''' Project Name: learnString
'''
''' Description:   Demonstrates how to query the instrument for its
'''                settings. From the instrument responses this program
'''                will build a command string that can be sent to
'''                the instrument to set the instrument to the previous
'''                settings.
'''
'''                Copyright  ©  2000 Agilent Technologies, Inc.
'''
''' Date            Developer
''' May 12, 2000    Agilent Technologies
''' -------------------------------------------------------------------------

Dim learnString As String
Dim dmm As VisaComLib.FormattedIO488
Dim ioAddress As String

Private Sub cmdGetSettings_Click()
    ' retrieve the 34401A learn string from instrument
    ' store in string and write to debug window.
    Dim id() As Variant
    
    On Error GoTo GetSettingsError
    
    EnableButtons False
    
    
    ' Gets the instrument model number
    dmm.WriteString "*idn?"
    id = dmm.ReadList
    txtModel.Text = id(1)
    txtModel.Refresh
    
    If InStr(1, id(1), "34420") Then
        learnString = Get34420ALearnString(dmm)
    Else
        learnString = Get34401ALearnString(dmm)
    End If
    
    EnableButtons True
    Exit Sub
    
GetSettingsError:
    MsgBox "GetSettingsError, " & Err.Description
    EnableButtons True
End Sub

Private Sub cmdSendSettings_Click()
    ' if the learnString is not empty, the string will
    ' be send to the instrument.
    
    EnableButtons False
    
    If Len(learnString) > 10 Then
        ' allow 30 seconds for RS232, because it is slow
        dmm.IO.Timeout = 10000
        dmm.WriteString learnString
        dmm.IO.Timeout = 10000
    Else
        MsgBox "Learn string empty"
        Debug.Print learnString
    End If

    EnableButtons True
End Sub

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim ioAddr As String

    On Error GoTo ioError

    ioAddr = InputBox("Enter the IO address of the DMM", "Set IO address", txtAddress.Text)
    If Len(ioAddr) > 0 Then
        txtAddress.Text = ioAddr
        Set mgr = New VisaComLib.ResourceManager
        Set dmm = New VisaComLib.FormattedIO488
        Set dmm.IO = mgr.Open(txtAddress.Text)
    End If

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    ' load the address of the control into the text box
    txtAddress.Text = "GPIB::22"
    
    cmdSetIO_Click
End Sub

Sub EnableButtons(ByVal enable As Boolean)

    If enable Then
        cmdGetSettings.Enabled = True
        cmdSendSettings.Enabled = True
        cmdSetIO.Enabled = True
    Else
        cmdGetSettings.Enabled = False
        cmdSendSettings.Enabled = False
        cmdSetIO.Enabled = False
    End If
End Sub
