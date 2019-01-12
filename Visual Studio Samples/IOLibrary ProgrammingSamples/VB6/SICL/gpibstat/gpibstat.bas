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
'  gpibstat.bas
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Sub main()
   Dim id As Integer        ' session id
   Dim remen As Integer     ' remote enable
   Dim srq As Integer       ' service request
   Dim ndac As Integer      ' not data accepted
   Dim sysctlr As Integer   ' system controller
   Dim actctlr As Integer   ' active controller
   Dim talker As Integer    ' talker
   Dim listener As Integer  ' listener
   Dim addr As Integer      ' bus address
   Dim header As String     ' report header
   Dim values As String     ' report output

   ' Open GPIB interface session
    ' "gpib0" is a SICL interface name
    ' Change these to appropriate values for your configuration
    ' Consult the Agilent Connectivity Expert for the interface names
    '    on your PC.
   id = iopen("gpib0")
   Call itimeout(id, 10000)

   ' Retrieve GPIB bus status
   Call igpibbusstatus(id, I_GPIB_BUS_REM, remen)
   Call igpibbusstatus(id, I_GPIB_BUS_SRQ, srq)
   Call igpibbusstatus(id, I_GPIB_BUS_NDAC, ndac)
   Call igpibbusstatus(id, I_GPIB_BUS_SYSCTLR, sysctlr)
   Call igpibbusstatus(id, I_GPIB_BUS_ACTCTLR, actctlr)
   Call igpibbusstatus(id, I_GPIB_BUS_TALKER, talker)
   Call igpibbusstatus(id, I_GPIB_BUS_LISTENER, listener)
   Call igpibbusstatus(id, I_GPIB_BUS_ADDR, addr)

   ' Display form1 and print results
   Form1.Show
   Form1.Print ""
   Form1.Print "REM"; Tab(7); "SRQ"; Tab(14); "NDC"; Tab(21); "SYS"; Tab(28); "ACT"; Tab(35); "TLK"; Tab(42); "LTN"; Tab(49); "ADDR"
   Form1.Print remen; Tab(7); srq; Tab(14); ndac; Tab(21); sysctlr; Tab(28); actctlr; Tab(35); talker; Tab(42); listener; Tab(49); addr

   ' Tell SICL to cleanup for this task
   Call siclcleanup


End Sub
