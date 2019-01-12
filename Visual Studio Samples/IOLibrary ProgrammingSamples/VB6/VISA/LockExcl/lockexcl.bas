Attribute VB_Name = "lockexcl"
Option Explicit

'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999- 2001 Agilent Technologies Inc.  All rights reserved.
''
'' You have a royalty-free right to use, modify, reproduce and distribute
'' the Sample Application Files (and/or any modified version) in any way
'' you find useful, provided that you agree that Agilent Technologies has no
'' warranty, obligations or liability for any Sample Application Files.
''
'' Agilent Technologies provides programming examples for illustration only,
'' This sample program assumes that you are familiar with the programming
'' language being demonstrated and the tools used to create and debug
'' procedures. Agilent Technologies support engineers can help explain the
'' functionality of Agilent Technologies software components and associated
'' commands, but they will not modify these samples to provide added
'' functionality or construct procedures to meet your specific needs.
'' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

'' To develop VISA applications in Microsoft Visual Basic, you first need
'' to add the Visual Basic (VB) declaration file in your VB project as a
'' Module. This file contains the VISA function definitions and constant
'' declarations needed to make VISA calls from Visual Basic.
'' To add this module to your project in VB 6, from the menu, select
'' Project->Add Module, select the 'Existing' tab, and browse to the
'' directory containing the VB Declaration file, select visa32.bas, and
'' press 'Open'.
''
'' The name and location of the VB declaration file depends on which
'' operating system you are using.  Assuming the 'standard' VISA directory
'' of C:\Program Files\Visa or the 'standard' VXIpnp directory of
'' C:\VXIpnp, the visa32.bas file can be located in one of the following:
''
''   \winnt\agvisa\include\visa32.bas - Windows NT/2000
''   \winnt\include\visa32.bas        - Windows NT/2000
''   \win95\include\visa32.bas        - Windows 95/98/Me

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  lockexcl.bas
'  This example shows a session gaining an exclusive lock to perform
'  the viVPrintf and viVScanf VISA operations on a GPIB device.
'  It then releases the lock via the viUnlock function.
'  The program queries a GPIB device for an identification string
'  and prints the results.
'  Note that you may need to change the address.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Sub Main()

   Dim status As Long            'VISA function status return code
   Dim defrm As Long             'Session to Default Resource Manager
   Dim vi As Long                'Session to instrument
   Dim strRes As String * 256    'Fixed length string to hold results

   On Error GoTo ErrorHandler

   ' Open the default resource manager session
   status = viOpenDefaultRM(defrm)
   If (status < VI_SUCCESS) Then GoTo VisaErrorHandler

   '  Open the session to the GPIB device at address 22
   '  The "GPIB0" parameter is the VISA Interface name to
   '  an GPIB instrument as defined in:
   '    Start->Programs->Agilent IO Libraries->IO Config
   '  Change this name to whatever you have defined for your
   '  VISA Interface.
   '  "GPIB0::22::INSTR" is the address string for the device -
   '  this address will be the same as seen in:
   '    Start->Programs->Agilent IO Libraries->VISA Assistant
   '  after the VISA Interface Name is defined in IO Config

   status = viOpen(defrm, "GPIB0::22::INSTR", 0, 0, vi)
   If (status < VI_SUCCESS) Then GoTo VisaErrorHandler

   ' Initialize device
   status = viVPrintf(vi, "*RST" + Chr$(10), 0)
   If (status < VI_SUCCESS) Then GoTo VisaErrorHandler

   ' Make sure no other process or thread does anything to this
   ' resource between the viVPrintf() and the viVScanf() calls
    status = viLock(vi, VI_EXCLUSIVE_LOCK, 2000, VI_NULL, VI_NULL)

   ' Ask for the device's *IDN string.
   status = viVPrintf(vi, "*IDN?" + Chr$(10), 0)
   If (status < VI_SUCCESS) Then GoTo VisaErrorHandler

   ' Read the results as a string.
   status = viVScanf(vi, "%t", strRes)
   If (status < VI_SUCCESS) Then GoTo VisaErrorHandler

   '  Unlock this session so other processes and threads can use it
   status = viUnlock(vi)

   ' Display the results
   MsgBox "Result is: " + strRes, vbOKOnly, "*IDN? Result"

   ' Close the vi session and the resource manager session
   Call viClose(vi)
   Call viClose(defrm)

   Exit Sub

ErrorHandler:
   ' Display the error message
   MsgBox "*** Error : " + Error$, vbExclamation
   Exit Sub

VisaErrorHandler:
   Dim strVisaErr As String * 200
   Call viStatusDesc(defrm, status, strVisaErr)
   MsgBox "*** Error : " & strVisaErr, vbExclamation, "VISA Error Message"
   Exit Sub

End Sub

