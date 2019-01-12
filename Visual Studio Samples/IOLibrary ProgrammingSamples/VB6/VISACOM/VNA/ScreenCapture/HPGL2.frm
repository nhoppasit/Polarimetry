VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Begin VB.Form frmHPGL2 
   Caption         =   "Form1"
   ClientHeight    =   6585
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7725
   LinkTopic       =   "Form1"
   ScaleHeight     =   6585
   ScaleWidth      =   7725
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   240
      TabIndex        =   8
      Top             =   960
      Width           =   1455
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   2520
      Top             =   1200
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton cmdPrint 
      Caption         =   "Print"
      Height          =   375
      Left            =   5760
      TabIndex        =   7
      Top             =   1080
      Width           =   1575
   End
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   240
      TabIndex        =   5
      Text            =   "GPIB0::26"
      Top             =   480
      Width           =   2775
   End
   Begin VB.ListBox lstModelNumber 
      Height          =   840
      Left            =   3240
      TabIndex        =   3
      Top             =   480
      Width           =   1815
   End
   Begin VB.CommandButton cmdRefresh 
      Caption         =   "Refresh"
      Height          =   375
      Left            =   5760
      TabIndex        =   2
      Top             =   600
      Width           =   1575
   End
   Begin VB.CommandButton cmdGetHPGL2 
      Caption         =   "Get Screen"
      Height          =   375
      Left            =   5760
      TabIndex        =   1
      Top             =   120
      Width           =   1575
   End
   Begin VB.PictureBox picScreen 
      Height          =   4695
      Left            =   0
      ScaleHeight     =   4635
      ScaleWidth      =   7635
      TabIndex        =   0
      Top             =   1680
      Width           =   7695
   End
   Begin VB.Label Label2 
      Caption         =   "Address"
      Height          =   255
      Left            =   240
      TabIndex        =   6
      Top             =   240
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "Model number:"
      Height          =   255
      Left            =   3240
      TabIndex        =   4
      Top             =   240
      Width           =   1695
   End
End
Attribute VB_Name = "frmHPGL2"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2000 Agilent Technologies Inc.  All rights reserved.
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


Dim plotter As clsHPGL2ToPicture

Private Sub cmdGetHPGL2_Click()
    InstrumentGetPlotterImage
End Sub

Private Sub cmdRefresh_Click()
    plotter.Refresh
End Sub

Private Sub Form_Load()

    LoadModelNumberList
    
    picScreen.BorderStyle = 0
    picScreen.AutoRedraw = True

    Set plotter = New clsHPGL2ToPicture

    With plotter
        ' set default values
        .Backcolor = vbWhite
        .IncludeTimeStamp = False
        .Zoom = 95                  ' in per cent
        .isMonochrome = False
        .PlotterFontName = "Arial"
        .isDefaultPenColors = True
        .LineThickness = 1          'width in pixels
    End With
    
    SetIO Me.txtAddress

End Sub
Private Sub InstrumentGetPlotterImage()
    Dim frmType As String
    Dim gpib_address As String
    Dim data() As Byte
    Dim modelNumber As String
    Dim timeStamp As Boolean

    On Error Resume Next


    Me.MousePointer = vbHourglass

    On Error GoTo GetIOError

    modelNumber = lstModelNumber.Text

    ' this will route to the needed routine by model number
    GetPlotterData modelNumber, data
    
    Set plotter.PictureBox = Me.picScreen
    
    With plotter
        .HPGL2Data = data
        .Plot
        .Refresh
    End With

    Me.MousePointer = vbDefault

    Exit Sub

GetIOError:
    Me.MousePointer = vbDefault
    Debug.Print Err.Description
    MsgBox "Error; Can't find instrument; " & Err.Description

End Sub
Sub LoadModelNumberList()
    ' loads the list of supported model numbers
    With lstModelNumber
        .Clear
        .AddItem "HP3577A"
        .AddItem "8510C"
        .AddItem "8714ET"
        .AddItem "8720ES"
        .AddItem "8753E"
        .ListIndex = 4
    End With

End Sub


Private Sub Form_Resize()
    Me.picScreen.Width = Me.ScaleWidth
    Me.picScreen.Height = Me.ScaleHeight - picScreen.Top
End Sub

Private Sub cmdPrint_Click()
    ' Prints the contents of the Picture box and

    Dim msg As String  ' Declare variable
    Dim numberLines As Long
    
    On Error GoTo ErrorHandler ' Set up error handler.
    
    ' set the flags of the common dialog and show it
    With CommonDialog1
        .CancelError = True
        .Flags = cdlPDReturnDC + cdlPDHidePrintToFile _
                + cdlPDUseDevModeCopies + cdlPDNoPageNums
        .Flags = .Flags + cdlPDAllPages
        .ShowPrinter
    End With
    
    ' set the simulator object for printer
    Set plotter.PictureBox = Printer
    
    ' print graphics to printer object
    plotter.PrintHPGL2
    Printer.EndDoc
    
    Set plotter.PictureBox = Me.picScreen

    Exit Sub

ErrorHandler:
    If Err <> cdlCancel Then
        msg = "The Plotter can't be printed." & vbCrLf & Err.Description
        MsgBox msg  ' Display message.
    End If
End Sub


Private Sub cmdSetIO_Click()
    SetIO Me.txtAddress
End Sub

