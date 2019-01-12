VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Scope Settings Sample"
   ClientHeight    =   3465
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4605
   Icon            =   "frmsettings.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   3465
   ScaleWidth      =   4605
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtFilePath 
      Height          =   405
      Left            =   120
      TabIndex        =   5
      Text            =   "c:\scopeSettings.scp"
      Top             =   1200
      Width           =   3375
   End
   Begin VB.TextBox txtAddress 
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Text            =   "GPIB0::7"
      Top             =   360
      Width           =   3375
   End
   Begin VB.CommandButton cmdGetSetup 
      Caption         =   "Save setup to file"
      Height          =   495
      Left            =   960
      TabIndex        =   1
      Top             =   2160
      Width           =   2655
   End
   Begin VB.CommandButton cmdSendSetup 
      Caption         =   "Send file to Scope"
      Height          =   495
      Left            =   960
      TabIndex        =   0
      Top             =   2760
      Width           =   2655
   End
   Begin VB.Label Label2 
      Caption         =   "File path"
      Height          =   255
      Left            =   120
      TabIndex        =   4
      Top             =   960
      Width           =   2535
   End
   Begin VB.Line Line1 
      X1              =   120
      X2              =   4440
      Y1              =   1800
      Y2              =   1800
   End
   Begin VB.Label Label1 
      Caption         =   "Address:"
      Height          =   255
      Left            =   120
      TabIndex        =   3
      Top             =   120
      Width           =   1455
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 2002 Agilent Technologies Inc.  All rights reserved.
''
'' You have a royalty-free right to use, modify, reproduce and distribute
'' the Sample Application Files (and/or any modified version) in any way
'' you find useful, provided that you agree that Agilent Technologies has no
'' warranty,  obligations or liability for any Sample Application Files.
''
'' Agilent Technologies provides programming examples for illustration only,
'' This sample program assumes that you are familiar with the programming
'' language being demonstrated and the tools used to create and debug
'' procedures. Agilent Technologies support engineers can help explain the
'' functionality of Agilent Technologies software components and associated
'' commands, but they will not modify these samples to provide added
'' functionality or construct procedures to meet your specific needs.
'' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'
' This example program demonstrates saving the scope settings (learn String) to a std
' file (same as the IntuiLink file structure) and then sending the settings
' back to the scope


Const VALID_INSTRUMENT_MASK = "546"

Private Sub cmdSendSetup_Click()
    ' Sends the file containing scope settings to the scope
    Dim SetupName As String

    On Error GoTo SendSetUpError

    cmdSendSetup.Enabled = False
    Me.MousePointer = vbHourglass
    
    getLearnStringFromFile Me.txtFilePath.Text
    
    Me.MousePointer = vbDefault
    cmdSendSetup.Enabled = True

    Exit Sub

SendSetUpError:
    Debug.Print "Error during Send Scope Setup: " & vbCrLf & Err.Description
End Sub

Private Sub cmdGetSetup_Click()
    ' Saves the scope setup to a file

    On Error GoTo saveSetUpError

    cmdGetSetup.Enabled = False
    Me.MousePointer = vbHourglass

    SaveLearnStringToFile Me.txtFilePath

    Me.MousePointer = vbDefault
    cmdGetSetup.Enabled = True
    Exit Sub

saveSetUpError:
    MsgBox "Error during Save Scope Setup: " & vbCrLf & Err.Description

End Sub
Public Sub SaveLearnStringToFile(filePath As String)
    ' Get and array of bytes for the learn string
    Dim strTemp As String
    Dim reply() As String
    Dim LearnString() As Byte
    Dim index As Long
    Dim S As CSetupFile
    Dim i As Long
    Dim scope As VisaComLib.FormattedIO488
    Dim io_mgr As VisaComLib.ResourceManager


    Set io_mgr = New VisaComLib.ResourceManager
    Set scope = New VisaComLib.FormattedIO488
    Set scope.IO = io_mgr.Open(Me.txtAddress.Text)

    Set S = New CSetupFile

   ' ReDim LearnString(index)


    With scope
        .WriteString "*idn?"
        strTemp = .ReadString
        reply = Split(strTemp, ",")
        
        index = 1000
        .IO.Timeout = 500
        .WriteString "*lrn?"
        LearnString = .IO.Read(index)

        ReDim Preserve LearnString(UBound(LearnString) - 1)

        S.LearnString = LearnString
    End With

    With S
        .Model = reply(1)
        .Manufacturer = reply(0)
        .Version = reply(3)
        .UserComments = Now()
        .WriteFile filePath
    End With
    Set S = Nothing


    Set S = Nothing

End Sub
Public Sub getLearnStringFromFile(filePath As String)
    ' Get and array of bytes for the learn string
    Dim LearnString() As Byte
    Dim index As Long
    Dim i As Long
    Dim S As CSetupFile
    Dim scope As VisaComLib.FormattedIO488
    Dim io_mgr As VisaComLib.ResourceManager


    Set io_mgr = New VisaComLib.ResourceManager
    Set scope = New VisaComLib.FormattedIO488
    Set scope.IO = io_mgr.Open(Me.txtAddress.Text)

    Set S = New CSetupFile

    Dim SendString() As Byte

    If S.IsACompatibleFile(filePath) Then

        With S
            .ReadFile filePath
            LearnString = .LearnString
        End With

        If InStr(1, S.Model, VALID_INSTRUMENT_MASK, vbTextCompare) = 0 Then
            GoTo notValidFile
        End If

        If (scope Is Nothing) Then
            MsgBox "Error: Instrument not connected"
        Else
            index = UBound(LearnString)
            ReDim SendString(index + 9)

            SendString(0) = Asc("S")
            SendString(1) = Asc("y")
            SendString(2) = Asc("s")
            SendString(3) = Asc("t")
            SendString(4) = Asc(":")
            SendString(5) = Asc("S")
            SendString(6) = Asc("e")
            SendString(7) = Asc("t")
            SendString(8) = Asc(" ")

            For i = 0 To index
                SendString(i + 9) = LearnString(i)
            Next i

            scope.IO.Write SendString, UBound(SendString) + 1

        End If

    Else
        GoTo notValidFile

    End If

    Set S = Nothing
    Set scope = Nothing

    Exit Sub

notValidFile:
    MsgBox "Not a valid file"
    Set S = Nothing
End Sub

