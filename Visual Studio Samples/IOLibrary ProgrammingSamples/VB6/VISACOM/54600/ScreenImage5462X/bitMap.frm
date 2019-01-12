VERSION 5.00
Begin VB.Form frmBitMap 
   Caption         =   "Form1"
   ClientHeight    =   7485
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8565
   LinkTopic       =   "Form1"
   ScaleHeight     =   7485
   ScaleWidth      =   8565
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   615
      Left            =   4080
      TabIndex        =   4
      Top             =   120
      Width           =   1935
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Text            =   "GPIB0::7"
      Top             =   480
      Width           =   3495
   End
   Begin VB.PictureBox Picture1 
      Height          =   6015
      Left            =   120
      ScaleHeight     =   5955
      ScaleWidth      =   7995
      TabIndex        =   1
      Top             =   1320
      Width           =   8055
   End
   Begin VB.CommandButton cmdGetBitMap 
      Caption         =   "Get Bit Map"
      Height          =   615
      Left            =   6240
      TabIndex        =   0
      Top             =   120
      Width           =   1935
   End
   Begin VB.Label Label1 
      Caption         =   "Address"
      Height          =   255
      Left            =   120
      TabIndex        =   3
      Top             =   240
      Width           =   2535
   End
End
Attribute VB_Name = "frmBitMap"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright ©  2000,2001 Agilent Technologies Inc.  All rights reserved.
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
' this example will read a bitmap from the 54620 series of
' scopes
'

Dim Scope As VisaComLib.FormattedIO488

Private Sub cmdGetBitMap_Click()
    ' This code gets the bitmap from the Agilent 54620-series scopes
    Dim data As Variant
    Dim byteData() As Byte
    Dim dummy As String
    Dim IsRS232 As Boolean
    Dim strVariant As Variant

    
    On Error GoTo imageError
    
    Const filePath = "C:\~scp_tmp.bmp"
    
    Set Picture1 = LoadPicture("")
    
    With Scope
        .IO.Timeout = 15000
        .WriteString ":DISPlay:DATA? BMP, SCReen"
        byteData = .ReadIEEEBlock(BinaryType_UI1)
        .IO.Timeout = 10000
    End With
    
    saveAsFile filePath, byteData
    
    Picture1.AutoSize = True
    Set Picture1 = LoadPicture(filePath)
    Kill filePath
    
    Exit Sub
imageError:
    MsgBox "Error retrieving scope data" & vbCrLf & "Error: " & vbCrLf & Err.Description
    
End Sub


Private Sub saveAsFile(ByVal filePath As String, byteData() As Byte)
    Dim hFile As Long
    Dim isOpen As Boolean
   
    On Error GoTo saveAsFile

    hFile = FreeFile()
    Open filePath For Binary Access Write Shared As hFile
    isOpen = True
    
    Put #hFile, , byteData

    If isOpen Then Close #hFile
    
    Exit Sub

saveAsFile:
    If isOpen Then Close #hFile
End Sub

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim ioAddress As String
    On Error GoTo ioError

    ioAddress = txtAddress.Text
    ioAddress = InputBox("Enter the IO address of the scope", "Set IO address", ioAddress)

    If Len(ioAddress) > 6 Then
        Set mgr = New VisaComLib.ResourceManager
        Set Scope = New VisaComLib.FormattedIO488
        Set Scope.IO = mgr.Open(ioAddress)
        txtAddress.Text = ioAddress
    End If

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub

Private Sub Form_Load()
    ' load the address of the control into the text box
    cmdSetIO_Click
End Sub

