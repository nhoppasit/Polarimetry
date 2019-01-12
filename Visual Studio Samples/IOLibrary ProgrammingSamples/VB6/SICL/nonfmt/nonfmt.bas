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
'  nonfmt.bas
'  The following subroutine measures AC voltage on a
'  multimeter and prints out the results.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub Main()
   Dim dvm As Integer
   Dim strres As String * 20  'Fixed-length String
   Dim actual As Long

    ' "gpib0" is a SICL interface name
    ' "23" is the instrument gpib address
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.
   dvm = iopen("gpib0,23")
   Call itimeout(dvm, 5000)

'  Initialize dvm
   Call iwrite(dvm, "*RST" + Chr$(10), 5, 1, 0&)

'  Set up multimeter and take measurements
   Call iwrite(dvm, "CALC:DBM:REF 50" + Chr$(10), 16, 1, 0&)
   Call iwrite(dvm, "MEAS:VOLT:AC? 1, 0.001" + Chr$(10), 23, 1, 0&)

'  Read measurements
   Call iread(dvm, strres, 20, 0&, actual)

'  Print the results
   Form1.Show
   Form1.Print ""
   Form1.Print Tab(5); "Result Is " & Left$(strres, actual)

'  Close the multimeter session
   Call iclose(dvm)

'  Tell SICL to cleanup for this task
   Call siclcleanup

   Exit Sub

End Sub

