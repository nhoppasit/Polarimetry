VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   1920
   ClientLeft      =   45
   ClientTop       =   270
   ClientWidth     =   4770
   LinkTopic       =   "Form1"
   ScaleHeight     =   1920
   ScaleWidth      =   4770
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox Text1 
      Height          =   1692
      Left            =   120
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   2
      Top             =   120
      Width           =   2892
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Take Readings"
      Height          =   372
      Left            =   3240
      TabIndex        =   1
      Top             =   120
      Width           =   1452
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Close"
      Height          =   372
      Left            =   3240
      TabIndex        =   0
      Top             =   1440
      Width           =   1452
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'global variables
Dim setNo As Integer
Dim dvm As Integer
Dim sw As Integer
Dim res As Double
Dim argcount As Integer

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
'' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

'' To develop SICL applications in Microsoft Visual Basic, you first need
'' to add the Visual Basic (VB) declaration file in your VB project as a
'' Module. This file contains the SICL function definitions and constant
'' declarations needed to make SICL calls from Visual Basic.
'' To add this module to your project in VB 6, from the menu, select
'' Project | Add Module, select the 'Existing' tab, and browse to the
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

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' gpibdv.frm
' This example opens two GPIB communications sessions with
' dvm and switch VXI devices (via a VXI Command Module).
' Every time readings are taken, a scan list is sent to the
' switch and measurements are taken by the multimeter every
' time a switch is closed.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Private Sub Form_Load()

    'set up form's controls
    'Text1.MultiLine = True   run-time read-only  - set in properties
    'Text1.ScrollBars = 2     run-time read-only  - set in properties
    Text1.Text = "Readings:" & vbCrLf

    setNo = 0

    ' "gpib0" is a SICL interface name
    ' "9" is the customary address for a VXI command module
    ' "3" and "14" are the instrument gpib addresses
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.
    dvm = iopen("gpib0,9,3")
    sw = iopen("gpib0,9,14")

    'set timeouts
    Call itimeout(dvm, 10000)
    Call itimeout(sw, 10000)

    ' Set up switch trigger
    argcount = ivprintf(sw, "TRIG:SOUR BUS" + Chr$(10))

End Sub

Private Sub Command1_Click()
    Dim i As Integer

    ' Set up scan list
    argcount = ivprintf(sw, "SCAN (@100:103)" + Chr$(10))
    argcount = ivprintf(sw, "INIT" + Chr$(10))

    setNo = setNo + 1
    Text1.Text = Text1.Text & "Set " & setNo & ": " & vbCrLf
    For i = 1 To 4
       ' Take a measurement
       argcount = ivprintf(dvm, "MEAS:VOLT:DC?" + Chr$(10))

       ' Read the results
       argcount = ivscanf(dvm, "%lf", res)

       ' Print the results
       Text1.Text = Text1.Text & "   Channel " & i & " result:  " + Format(res) & vbCrLf

       ' Trigger switch
       argcount = ivprintf(sw, "TRIG" + Chr$(10))
    Next i
    'Text1.Text = Text1.Text & vbCrLf
End Sub


Private Sub Command2_Click()
    Unload Me
End Sub


Private Sub Form_Unload(Cancel As Integer)
   ' Close the sessions
   Call iclose(dvm)
   Call iclose(sw)

   ' Tell SICL to cleanup for this task
   Call siclcleanup

End Sub
