Attribute VB_Name = "Module1"
Option Explicit
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999 - 2002 Agilent Technologies Inc.  All rights reserved.
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

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  vximdev.bas
'  This example program opens a communication session with a
'  VXI message-based device and measures the AC voltage. The
'  measurement results are then printed
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub Main()
    Dim id As Integer
    Dim strres As String * 80  'Fixed-length String
    Dim actual As Long

    '  Open the instrument session
    ' "vxi" is a SICL interface name
    ' "216" is the instrument logical address.
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.
    id = iopen("vxi,216")

    '  Set timeout to 10 seconds
    Call itimeout(id, 10000)

    '  Initialize dvm
    Call iwrite(id, "*RST" + Chr$(10), 6, 1, 0&)

    '  Take measurement
    Call iwrite(id, "MEAS:VOLT:DC? 1, 0.001" + _
                    Chr$(10), 23, 1, 0&)

    '  Read result
    Call iread(id, strres, 80, 0&, actual)

    '  Display the results
    MsgBox "Result is: " + strres, vbOKOnly, _
                    "DVM DCV Result"

    '  Close the instrument session
    Call iclose(id)

    '  Tell SICL to cleanup for this task
    Call siclcleanup

End Sub
