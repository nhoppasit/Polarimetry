VERSION 5.00
Begin VB.Form scope 
   Appearance      =   0  'Flat
   AutoRedraw      =   -1  'True
   BackColor       =   &H00FFFFFF&
   Caption         =   "Hewlett-Packard"
   ClientHeight    =   4695
   ClientLeft      =   585
   ClientTop       =   1875
   ClientWidth     =   8430
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   ForeColor       =   &H80000008&
   LinkMode        =   1  'Source
   LinkTopic       =   "Form1"
   PaletteMode     =   1  'UseZOrder
   ScaleHeight     =   4695
   ScaleWidth      =   8430
   Begin VB.TextBox txtSdiv 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   5880
      TabIndex        =   3
      Top             =   3840
      Width           =   1332
   End
   Begin VB.TextBox txtOffset 
      Appearance      =   0  'Flat
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   5880
      TabIndex        =   2
      Top             =   3360
      Width           =   1332
   End
   Begin VB.TextBox txtVdiv 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   5880
      TabIndex        =   1
      Top             =   2880
      Width           =   1332
   End
   Begin VB.TextBox txtStatus 
      Appearance      =   0  'Flat
      Height          =   375
      Left            =   5880
      TabIndex        =   10
      Top             =   2280
      Width           =   2172
   End
   Begin VB.CommandButton cmdExit 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Exit"
      Height          =   375
      Left            =   6480
      TabIndex        =   8
      Top             =   1080
      Width           =   1575
   End
   Begin VB.CommandButton cmdPrint 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Print Form"
      Height          =   375
      Left            =   6480
      TabIndex        =   9
      Top             =   600
      Width           =   1575
   End
   Begin VB.CommandButton cmdGetWaveform 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      Caption         =   "Waveform"
      Height          =   375
      Left            =   6480
      TabIndex        =   0
      Top             =   120
      Width           =   1575
   End
   Begin VB.Label Label5 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFF00&
      Caption         =   "Message"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   372
      Left            =   5880
      TabIndex        =   11
      Top             =   1800
      Width           =   1092
   End
   Begin VB.Label Label3 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFF00&
      Caption         =   "S/Div"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   372
      Left            =   7320
      TabIndex        =   6
      Top             =   3840
      Width           =   852
   End
   Begin VB.Label Label2 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFF00&
      Caption         =   "Offset"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   372
      Left            =   7320
      TabIndex        =   5
      Top             =   3360
      Width           =   852
   End
   Begin VB.Label Label1 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFF00&
      Caption         =   "V/Div"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   372
      Left            =   7320
      TabIndex        =   4
      Top             =   2880
      Width           =   852
   End
   Begin VB.Label Label4 
      Appearance      =   0  'Flat
      BackColor       =   &H00FFFF00&
      Caption         =   "Agilent 54601A OSCILLOSCOPE"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   372
      Left            =   240
      TabIndex        =   7
      Top             =   240
      Width           =   4092
   End
End
Attribute VB_Name = "scope"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999 - 2004 Agilent Technologies Inc.  All rights reserved.
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
''
'' This program is designed to illustrate several individual SICL
'' commands and is not meant to be an example of a robust Windows
'' application.
'' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

'' To develop SICL applications in Microsoft Visual Basic, you first need
'' to add the Visual Basic (VB) declaration file in your VB project as a
'' Module. This file contains the SICL function definitions and constant
'' declarations needed to make SICL calls from Visual Basic.
'' To add this module to your project in VB 6, from the menu, select
'' Project|Add Module, select the 'Existing' tab, and browse to the
'' directory containing the VB Declaration file, select sicl32.bas, and
'' click 'Open'.
''
'' Assuming the Agilent IO Libraries Suite is installed in the 'standard'
'' location:
''
''         C:\Program Files\Agilent\IO Libraries Suite
''
'' the sicl32.bas file can be located in:
''
''         C:\Program Files\Agilent\IO Libraries Suite\Include

'""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
' scope.frm
' This program sets up a scope and reads a digitized waveform into an
' integer array. The waveform is plotted on the form and can then be
' printed to the system default printer.
' Note that scope_address should be set to the SICL Name for your scope
' as configured in IO Config.
'""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

Dim waveform(1000) As Integer       ' Waveform (32-bit Integer)
Dim preamble(10) As Double          ' Preamble array
Dim waveformByte(2010) As Byte      ' Waveform bytes (10 byte header)

    ' "gpib0" is a SICL interface name
    ' "7" is the instrument gpib address
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.

Private Const scope_address = "gpib0,7"     ' Address of SCOPE

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' This routine terminates the application.  Note that we
' need to use Unload Me so that the form unload procedure
' is called and siclcleanup occurs.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub cmdExit_Click()
   Unload Me
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  This routine uses the Standard Instrument Control
'  Library (SICL) to get and plot waveform data from an
'  Agilent 54601A (or compatible) scope.
'
'  Note that any SICL errors that occur are displayed in
'  the txtStatus Text box.
'
'  This routine is called each time the cmdGetWaveform
'  Command button is clicked.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub cmdGetWaveform_Click()
   Dim scope_id As Integer      ' device session id for scope
   Dim intf_id As Integer       ' interface session id
   Dim xaxis As Integer         ' used to draw the waveform
   Dim numargs As Integer       ' # of args processed ivprintf/ivscanf
   Dim VpD As Double
   Dim Off As Double
   Dim SpD As Double
   Dim actual As Long
   Dim i As Integer

'  Set up Error Handler within this subroutine that will get
'  called if a SICL error occurs.
   On Error GoTo errorhandler:

'  Disable the button used to initiate I/O while I/O is being
'  performed.
   cmdGetWaveform.Enabled = False

'  Make sure text boxes are clear

   txtVdiv.Text = ""
   txtOffset.Text = ""
   txtSdiv.Text = ""

'  Open a device session using the device address specified by
'  the scope_address string.
   scope_id = iopen(scope_address)
   txtStatus.Text = "iopen - no error"

'  Open an interface session to the interface that the scope
'  is connected to.  Then call iclear to reset the interface.
   intf_id = igetintfsess(scope_id)
   Call iclear(intf_id)
   txtStatus.Text = "iclear - no error"

'  Set the I/O timeout for the scope's device session to 3 seconds
   Call itimeout(scope_id, 3000)
   txtStatus.Text = "itimeout - no error"

'  AUTOSCALE the scope - uncomment if manual setup desired
   'numargs = ivprintf(scope_id, ":AUTOSCALE" + Chr$(10))
   'txtStatus.Text = "ivprintf - no error"

'  digitize 1000 points (0-255) on channel 1, output WORD (16-bit)
   numargs = ivprintf(scope_id, ":WAVEFORM:POINTS 1000" + Chr$(10))
   txtStatus.Text = "ivprintf - no error"

   numargs = ivprintf(scope_id, ":WAVEFORM:FORMAT WORD" + Chr$(10))
   txtStatus.Text = "ivprintf - no error"

   numargs = ivprintf(scope_id, ":DIGITIZE CHANNEL1" + Chr$(10))
   txtStatus.Text = "ivprintf - no error"

'  Read the preamble
   numargs = ivprintf(scope_id, ":WAVEFORM:PREAMBLE?" + Chr$(10))
   txtStatus.Text = "ivprintf - no error"

   numargs = ivscanf(scope_id, "%,10lf" + Chr$(10), preamble())
   txtStatus.Text = "ivscanf - no error"

'  Read the waveform data
   numargs = ivprintf(scope_id, ":WAVEFORM:DATA?" + Chr$(10))
   txtStatus.Text = "ivprintf - no error"

'  unformatted read 2010 bytes ((1000 WORDs * 2) + 10-byte header)
   numargs = iread(scope_id, waveformByte(), 2010, vbNull, actual)
   txtStatus.Text = "ivscanf - no error"
   
'  calculate the WORD low-byte values after stripping 10-byte header
   For i = 0 To 999
      waveform(i) = waveformByte(2 * i + 11)
   Next i
   
'  Close device session for scope
   Call iclose(scope_id)
   txtStatus.Text = "iclose - no error"

'  Deal with the preamble
   VpD = (32 * preamble(7))
   Off = -1# * ((128 - preamble(9)) * preamble(7) + preamble(8))
   SpD = preamble(2) * preamble(4) / 10
   txtVdiv.Text = Str$(VpD)
   txtOffset.Text = Str$(Off)
   txtSdiv.Text = Str$(SpD)

   Cls

'  Set up the screen coordinate system
   ScaleLeft = 0
   ScaleTop = 330
   ScaleWidth = 6000
   ScaleHeight = -330

'  Draw the Grid

'  Main Border
   Line (100, 10)-(4100, 10), RGB(0, 128, 0)
   Line -(4100, 266), RGB(0, 128, 0)
   Line -(100, 266), RGB(0, 128, 0)
   Line -(100, 10), RGB(0, 128, 0)

'  Y-axis grid
   Line (500, 10)-(500, 266), RGB(0, 128, 0)
   Line (900, 10)-(900, 266), RGB(0, 128, 0)
   Line (1300, 10)-(1300, 266), RGB(0, 128, 0)
   Line (1700, 10)-(1700, 266), RGB(0, 128, 0)
   Line (2100, 10)-(2100, 266), RGB(255, 0, 0)
   Line (2500, 10)-(2500, 266), RGB(0, 128, 0)
   Line (2900, 10)-(2900, 266), RGB(0, 128, 0)
   Line (3300, 10)-(3300, 266), RGB(0, 128, 0)
   Line (3700, 10)-(3700, 266), RGB(0, 128, 0)

'  X-axis grid
   Line (100, 42)-(4100, 42), RGB(0, 128, 0)
   Line (100, 74)-(4100, 74), RGB(0, 128, 0)
   Line (100, 106)-(4100, 106), RGB(0, 128, 0)
   Line (100, 138)-(4100, 138), RGB(255, 0, 0)
   Line (100, 170)-(4100, 170), RGB(0, 128, 0)
   Line (100, 202)-(4100, 202), RGB(0, 128, 0)
   Line (100, 234)-(4100, 234), RGB(0, 128, 0)

'  Draw the waveform
   CurrentX = 100
   CurrentY = waveform(0) + 10
   For xaxis = 0 To 999
       Line -(xaxis * 4 + 100, waveform(xaxis) + 10)
   Next xaxis

'  Clear the status text box
   txtStatus.Text = ""

'  Enable the button used to initiate I/O
   cmdGetWaveform.Enabled = True

   Exit Sub

errorhandler:
'  Display the error message in the txtStatus TextBox.
   txtStatus.Text = Error$

'  Close the scope_id and intf_id sessions if iopen was successful
   If scope_id <> 0 Then
      iclose (scope_id)
   End If

'  Enable the button used to initiate I/O
   cmdGetWaveform.Enabled = True

   Exit Sub


End Sub

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' This routine prints the main form.
' The printer used by PrintForm is the default
' printer set up on the computer
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub cmdPrint_Click()
   scope.PrintForm
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  The following routine is called when the application's
'  Start Up form is unloaded.  It calls siclcleanup to
'  release resources allocated by SICL for this
'  application.
'
Private Sub Form_Unload(Cancel As Integer)
   Call siclcleanup     ' Tell SICL to clean up for this task
End Sub

