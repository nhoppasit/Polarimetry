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
'  ser_dev.bas
'  This example program takes a measurement from a DVM using a
'  SICL RS-232 device session.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Sub Main()
   Dim dvm As Integer
   Dim res As Double
   Dim argcount As Integer

    ' "COM1" is a SICL interface name
    ' Change this to an appropriate value for your instrument
   dvm = iopen("COM1,488")

   ' Set timeout to 10 sec
   Call itimeout(dvm, 10000)

   ' Prepare the multimeter for measurements
   argcount = ivprintf(dvm, "*RST" + Chr$(10), 0&)
   argcount = ivprintf(dvm, "SYST:REM" + Chr$(10), 0&)

   ' Take a measurement
   argcount = ivprintf(dvm, "MEAS:VOLT:DC?" + Chr$(10))

   ' Read the results
   argcount = ivscanf(dvm, "%lf", res)

   ' Print the results
   MsgBox "Result is " + Format(res), vbExclamation

   ' Close the multimeter session
   Call iclose(dvm)

'  Tell SICL to cleanup for this task
   Call siclcleanup

End Sub

