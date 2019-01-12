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
'' Assuming the Agilent IO Libraries are installed in the 'standard' location:
''
''         C:\Program Files\Agilent\IO Libraries Suite
''
'' the sicl32.bas file can be located in:
''
''         C:\Program Files\Agilent\IO Libraries Suite\Include

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  idn.bas
'  The following subroutine queries *IDN? on a GPIB instrument
'  and prints out the result.  No SICL error handling is set up
'  in this example, but should be for as programming practice
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub Main()
    Dim id As Integer
    Dim strres As String * 80  'Fixed-length String
    Dim actual As Long

    '  Open the instrument session
    ' "gpib0" is a SICL interface name
    ' "22" is the instrument gpib address
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on your PC.
    id = iopen("gpib0,22")
    Call itimeout(id, 5000)

    '  Query device's *IDN? string
    Call iwrite(id, "*IDN?" + Chr$(10), 6, 1, 0&)

    '  Read result
    Call iread(id, strres, 80, 0&, actual)

    '  Display the results
    MsgBox "Result is: " + strres, vbOKOnly, "*IDN? Result"

    '  Close the instrument session
    Call iclose(id)

    '  Tell SICL to cleanup for this task
    Call siclcleanup

End Sub

