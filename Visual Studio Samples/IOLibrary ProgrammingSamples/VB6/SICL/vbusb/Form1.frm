VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Test"
      Height          =   375
      Left            =   1560
      TabIndex        =   0
      Top             =   2040
      Width           =   1695
   End
End
Attribute VB_Name = "Form1"
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

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  usbdev.bas
'  This example program opens a communication session with a
'  USB device.  It then does a *IDN? query of the device and
'  also gets and displays some device information.  The
'  usbinfo and usbcapinfo structures are defined in sicl32.bas
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


Private Sub Command1_Click()

    Dim id As Integer
    Dim status As Integer
    Dim res As Integer
    Dim buf As String * 10
    Dim result As Long
    Dim setting As Long
    Dim strres As String * 80
    Dim actual As Long
    Dim datalen As Long
    Dim retcnt As Integer
    Dim siclAddr As String
    Dim info As usbinfo
    Dim capinfo As usbcapinfo
    
    ' Change the value of siclAddr to refer to the USB device of
    ' your choice.  Note that for simplicity, we are using an
    ' alias name rather than the much longer standard open string.
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.
    siclAddr = "usbDevice1"
    
    buf = "*IDN?" + Chr$(10)
    datalen = Len("*IDN?" + Chr$(10))
    id = iopen(siclAddr)
    status = iwrite(id, buf, datalen, 1, 0&)
    status = iread(id, strres, 80, 0&, actual)
    MsgBox "Result is: " + strres, vbOKOnly, "*IDN? Result"
 
    status = iusbgetcapabilities(id, capinfo)
    MsgBox "Capabilities: dev = " + Str$(capinfo.dev488Capabilities) + _
           ", intf = " + Str$(capinfo.intf488Capabilities), vbOKOnly, _
           "iusbgetcapabilities Result"
           
    status = iusbgetinfo(id, info)
    MsgBox "S/N = " + info.serialNumberStr, vbOKOnly, "iusbgetinfo Result"
    MsgBox "Manuf = " + info.manufNameStr, vbOKOnly, "iusbgetinfo Result"
    MsgBox "Prod = " + info.productNameStr, vbOKOnly, "iusbgetinfo Result"
   
    status = iclose(id)
End Sub


