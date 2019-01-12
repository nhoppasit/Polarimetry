VERSION 5.00
Begin VB.Form frmScreenImage 
   Caption         =   "Form1"
   ClientHeight    =   5520
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7125
   LinkTopic       =   "Form1"
   ScaleHeight     =   368
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   475
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox Text1 
      Height          =   375
      Left            =   1080
      TabIndex        =   2
      Text            =   "GPIB0::7"
      Top             =   120
      Width           =   3015
   End
   Begin VB.PictureBox rasterPBox 
      BackColor       =   &H00FFFFFF&
      Height          =   4812
      Left            =   120
      ScaleHeight     =   317
      ScaleMode       =   3  'Pixel
      ScaleWidth      =   453
      TabIndex        =   1
      Top             =   600
      Width           =   6852
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Get Screen Image"
      Height          =   372
      Left            =   5160
      TabIndex        =   0
      Top             =   120
      Width           =   1575
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      Caption         =   "Address:"
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   120
      Width           =   735
   End
End
Attribute VB_Name = "frmScreenImage"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2001 Agilent Technologies Inc.  All rights reserved.
'''
''' You have a royalty-free right to use, modify, reproduce and distribute
''' this code module (and/or any modified version) in any way
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

' this code demonstrates how to read the screen image from a 546xx scope (that supports
' the PRINT command) and create an image in Visual Basic

Private Sub Command1_Click()
    Dim InstrAddress As String
    Dim pclData() As Byte
    Dim dataSize As Long
    Dim scope As VisaComLib.FormattedIO488
    Dim io_mgr As VisaComLib.ResourceManager
    
    InstrAddress = Me.Text1.Text


    Set io_mgr = New VisaComLib.ResourceManager
    Set scope = New VisaComLib.FormattedIO488
    Set scope.IO = io_mgr.Open(InstrAddress)
    
    dataSize = 100000
    ReDim pclData(dataSize)

    scope.WriteString ":PRINT?"
    scope.IO.Timeout = 50000
    
    pclData() = scope.IO.Read(dataSize)
    ReDim Preserve pclData(UBound(pclData))
    
    Dim myPCLConverter As clsPclToBMP
    Set myPCLConverter = New clsPclToBMP

    With myPCLConverter
            Set .PictureBox = rasterPBox
            .pclData = pclData()
            .PCLtoRaster
            .isPBoxScaled = False
            .fillPictureBox
    End With
    
    Set clsPclToBMP = Nothing
    Set Instrument = Nothing
    Set IO = Nothing

End Sub

