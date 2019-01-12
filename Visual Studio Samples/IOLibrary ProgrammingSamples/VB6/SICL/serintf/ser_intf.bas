Attribute VB_Name = "Module1"
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

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  set_intf.bas
'  This program does the following:
'  1) gets the current configuration of the serial port
'  2) sets it to 9600 baud, no parity, 8 data bits, and
'     1 stop bit
'  3) prints the old configuration
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Sub Main()
   Dim intf As Integer
   Dim baudrate As Long
   Dim parity As Long
   Dim databits As Long
   Dim stopbits As Long
   Dim parity_str As String
   Dim msg_str As String

   ' open RS-232 interface session
    ' "COM1" is a SICL interface name
    ' Change this to an appropriate value for your instrument
   intf = iopen("COM1")
   Call itimeout(intf, 10000)

   ' get baud rate, parity, data bits, and stop bits
   Call iserialstat(intf, I_SERIAL_BAUD, baudrate)
   Call iserialstat(intf, I_SERIAL_PARITY, parity)
   Call iserialstat(intf, I_SERIAL_WIDTH, databits)
   Call iserialstat(intf, I_SERIAL_STOP, stopbits)

   ' determine string to display for parity
   Select Case parity
   Case I_SERIAL_PAR_NONE
      parity_str = "NONE"
   Case I_SERIAL_PAR_ODD
      parity_str = "ODD"
   Case I_SERIAL_PAR_EVEN
      parity_str = "EVEN"
   Case I_SERIAL_PAR_MARK
      parity_str = "MARK"
   Case Else
      parity_str = "SPACE"
   End Select

   ' set to 9600,NONE,8, 1
   Call iserialctrl(intf, I_SERIAL_BAUD, 9600)
   Call iserialctrl(intf, I_SERIAL_PARITY, I_SERIAL_PAR_NONE)
   Call iserialctrl(intf, I_SERIAL_WIDTH, I_SERIAL_CHAR_8)
   Call iserialctrl(intf, I_SERIAL_STOP, I_SERIAL_STOP_1)

   ' display previous settings
   msg_str = "Old settings: " & _
            Str$(baudrate) & "," & _
            parity_str & "," & _
            Str$(databits) & "," & _
            Str$(stopbits)

   MsgBox msg_str, vbExclamation

   ' close port
   Call iclose(intf)

'  Tell SICL to cleanup for this task
   Call siclcleanup

End Sub

