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

''''''''''''''''''''''''''''''''''''''''''''''''
'  locking.bas
'  This example shows how device locking can be
'  used to gain exclusive access to a device
''''''''''''''''''''''''''''''''''''''''''''''''
Sub Main()
   Dim dvm As Integer
   Dim strres As String * 20  'Fixed length String
   Dim actual As Long

'  Install an error handler
   On Error GoTo ErrorHandler

    ' "gpib0" is a SICL interface name
    ' "23" is the instrument gpib address
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.
   dvm = iopen("gpib0,23")
   Call itimeout(dvm, 10000)

'  Lock the multimeter device to prevent access from
'  other applications
   Call ilock(dvm)

'  Take a measurement
   Call iwrite(dvm, "MEAS:VOLT:DC?" + Chr$(10), _
               14, 1, 0&)

'  Read the results
   Call iread(dvm, strres, 20, 0&, actual)

'  Release the multimeter device for use by others
   Call iunlock(dvm)

'  Display the results
   MsgBox "Result is " + Left$(strres, actual)

'  Close the multimeter session
   Call iclose(dvm)

'  Tell SICL to cleanup for this task
   Call siclcleanup

   Exit Sub

ErrorHandler:
   '  Display the error message.
      MsgBox "*** Error : " + Error
   '  Tell SICL to cleanup for this task
      Call siclcleanup

End Sub

