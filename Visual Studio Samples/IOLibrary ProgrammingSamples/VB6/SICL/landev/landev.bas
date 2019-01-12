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
' landev.bas
' This example program opens a GPIB device session via a
' LAN-to-GPIB gateway. The addresses in this example assume
' a machine with hostname 'instserv' is acting as a
' LAN-to-GPIB gateway.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Sub Main()
    Dim dvm As Integer, sw As Integer
    Dim nargs As Integer, I As Integer
    Dim actual As Long
    Dim res As String * 20

    ' Set up an error handler within this subroutine that
    ' will get called if a SICL error occurs.
    On Error GoTo ErrorHandler

    ' "gpib0" is a SICL interface name
    ' "3" and "14" are instrument gpib addresses
    ' Change these to appropriate values for your instrument
    ' Consult the Agilent Connectivity Expert for the interface names
    '    and instrument addresses on the remote PC.
    dvm = iopen("lan[intserv]:gpib0,9,3")
    sw = iopen("lan[intserv]:gpib0,9,14")

    Call itimeout(dvm, 10000)
    Call itimeout(sw, 10000)

    ' set up the trigger
    nargs = iwrite(sw, "TRIG:SOUR BUS" + Chr$(10) + Chr$(0), _
                        14, 1, actual)

    ' set up scan list
    nargs = iwrite(sw, "SCAN (@100:103)" + Chr$(10) + Chr$(0), _
                        15, 1, actual)
    nargs = iwrite(sw, "INIT" + Chr$(10) + Chr$(0), 5, 1, actual)

    For I = 1 To 4 Step 1
        ' Take a measurement
        nargs = iwrite(dvm, "MEAS:VOLT:DC?" + Chr$(10) + Chr$(0), _
                        14, 1, actual)

        ' Read the results
        nargs = iread(dvm, res, 20, 0&, actual)

        ' Print the results
        MsgBox "Channel " & I & " result:  " + res & vbCrLf

        ' Trigger switch
        nargs = iwrite(sw, "TRIG" + Chr$(10) + Chr$(0), _
                        5, 1, actual)
    Next I

    Call iclose(dvm)
    Call iclose(sw)

    Exit Sub

ErrorHandler:
    ' Display the error message in the txtResponse TextBox.
    MsgBox "*** Error : " + Error$

    ' Close the device session if iopen was successful.
    If dvm <> 0 Then
        Call iclose(dvm)
    End If

    If sw <> 0 Then
        Call iclose(sw)
    End If

End Sub

