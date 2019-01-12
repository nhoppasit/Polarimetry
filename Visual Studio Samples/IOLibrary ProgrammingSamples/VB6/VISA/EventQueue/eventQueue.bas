Attribute VB_Name = "eventQueue"
Option Explicit

'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999 - 2001 Agilent Technologies Inc.  All rights reserved.
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

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' eventQueue.bas
' Example: Trigger Event Queuing.
' Since VISA cannot callback to a VB function, you can only
' use the VI_QUEUE mechanism in viEnableEvent(). There is no
' way to install an VISA event handler in VB
'
' This program illustrates enabling a trigger event in a queuing
' mode. When the viWaitOnEvent function is called, the program
' will suspend operation until the trigger line is fired or the
' timeout period is reached. Since the trigger lines were already
' fired and the events were put into a queue, the function will
' return and print the trigger line that fired. Note that you
' may need to change the device address.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Sub main()

    Dim defaultRM As Long
    Dim vi As Long
    Dim eventType As Long
    Dim eventVi As Long
    Dim err As Long
    Dim trigId As Long
    
    ' open sessions to Default Resource Manager and VXI device
    Call viOpenDefaultRM(defaultRM)
    Call viOpen(defaultRM, "VXI0::24::INSTR", VI_NULL, VI_NULL, vi)
    
    ' select trigger line TTL0 */
    Call viSetAttribute(vi, VI_ATTR_TRIG_ID, VI_TRIG_TTL0)
    
    ' enable the event
    Call viEnableEvent(vi, VI_EVENT_TRIG, VI_QUEUE, VI_NULL)
    
    ' fire trigger line twice
    Call viAssertTrigger(vi, VI_TRIG_PROT_SYNC)
    Call viAssertTrigger(vi, VI_TRIG_PROT_SYNC)
    
    ' Wait 10 sec for the event to occur
    err = viWaitOnEvent(vi, VI_EVENT_TRIG, 10000, eventType, eventVi)
    
    If (err = VI_ERROR_TMO) Then
       MsgBox "Timeout Occurred! Event not received", vbExclamation, _
                "viWaitOnEvent Error"
                
        Exit Sub
    End If
   
    ' print the event information
    MsgBox "Trigger Event Occurred!" & Chr$(10) & _
       "...Original Device Session = " & Str$(vi), , "Event Info"
    
    ' get trigger that fired
    Call viGetAttribute(eventVi, VI_ATTR_RECV_TRIG_ID, trigId)
    
    Select Case trigId
        Case VI_TRIG_TTL0:
            MsgBox "Trigger that fired: TTL0", vbOKOnly
        Case Else
            MsgBox "TrigID:  &H" & Hex(trigId)
    End Select
    
    ' close the context before continuing
    Call viClose(eventVi)
    ' get second event
    err = viWaitOnEvent(vi, VI_EVENT_TRIG, 10000, eventType, eventVi)
    
    If (err = VI_ERROR_TMO) Then
       MsgBox "Timeout Occurred! Event not received", vbExclamation, _
                "viWaitOnEvent Error"
        Exit Sub
    End If
    
    ' print the event information
    MsgBox "Got second event", vbOKOnly, "Event Info"
    
    ' close the context before continuing
    Call viClose(eventVi)
    
    ' disable event
    Call viDisableEvent(vi, VI_EVENT_TRIG, VI_QUEUE)
    
    ' close the sessions
    Call viClose(vi)
    Call viClose(defaultRM)

End Sub
