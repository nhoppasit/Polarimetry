Attribute VB_Name = "PtpSyncLogger"
Option Explicit

'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 2007 - 2007 Agilent Technologies Inc.  All rights reserved.
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

'' To develop PtpManagerC applications in Microsoft Visual Basic, you first need
'' to add the Visual Basic (VB) declaration file in your VB project as a
'' Module. This file contains the PtpManagerC function definitions and constant
'' declarations needed to make PtpManagerC calls from Visual Basic.
'' To add this module to your project in VB 6, from the menu, select
'' Project->Add Module, select the 'Existing' tab, and browse to the
'' directory containing the VB Declaration file, select PtpManagerC.bas, and
'' press 'Open'.
''
'' The location of the VB declaration file depends on where IO Libaries Suite is installed.
'' Assuming the default IO Libraries Suite install point this file can be found at:
''
''  C:\Program Files\Agilent\IO Libraries Suite\Include\PtpManagerC.bas
''

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  PtpSyncLogger.bas
'
' This program finds the active clocks, then logs the synchronization history
' for those clocks to the file 'PtpSyncLog.csv'.
'
' The format of the output is a comma-separated-value format suitable for consumption by Excel.
'
'  "SyncTime", <ClockUUID1>,, <ClockUUID2>,, ...
'  , "Offset", "Delay", "Offset", "Delay", ...
'  '<SyncSeconds>.<SyncNanoseconds>', <UUID1 Offset>, <UUID1 1WayDelay>, <UUID2 Offset>, <UUID2 1WayDelay>
'  ...
'
' Where:
'    <SyncSeconds>.<SyncNanoseconds> indicates the sync PTP time when the offsets and delays were valid
'    <UUIDx Offset> and <UUIDx 1WayDelay> are the Offset from master and One way delay as reals in seconds
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Dim m_lastSyncSeconds As PtpManagerC.BigSeconds
Dim m_lastSyncNanoSeconds As Long

Sub Main()
    Dim result As Long
    
    m_lastSyncSeconds.upperSeconds = 0
    m_lastSyncSeconds.lowerSeconds = 0
    
    Open "PtpSyncLog.csv" For Output As #1
    
    result = FindActiveClocks()
    
    LogSynchronizationHeaders
    
    result = PtpManagerC.ptpSetAttribute(PTP_ATTR_MANAGER_CAPTURE_HISTORY, 1)
    If (result <> 0) Then
        MsgBox "Error Setting Capture History Attribute"
        Exit Sub
    End If
    
    result = PtpManagerC.ptpInstallSyncMsgHandler(AddressOf MessageReceivedHandler)
    If (result <> 0) Then
        MsgBox "Error installing message received handler."
        Exit Sub
    End If
    
    MsgBox "Capturing sync history to 'PtpSyncLog.csv'. Click 'Ok' to stop capture."
    
    
    result = PtpManagerC.ptpSetAttribute(PTP_ATTR_MANAGER_CAPTURE_HISTORY, 0)
    result = PtpManagerC.ptpUninstallSyncMsgHandler(AddressOf MessageReceivedHandler)
    result = PtpManagerC.ptpShutdown
    Close #1
        
End Sub

Function MessageReceivedHandler(ByVal receiptSecondsUpper As Long, ByVal receiptSecondsLower As Long, ByVal receiptNanoseconds As Long, ByVal msgSource As Long) As Long
    Dim result As Long
    Dim msgType As Byte
    
    result = PtpManagerC.ptpGetMsgAttribute(PTP_MSG_MESSAGE_TYPE, msgType)
    If (result <> 0 Or msgType <> PTP_MSG_MESSAGE_TYPE_SYNC) Then
        MessageReceivedHandler = result
        Exit Function
    End If
    
    LogLastSynchronizationInfo
    
    ' Get the time stamp for this sync interval in preparation for logging next sync message
    result = PtpManagerC.ptpGetMsgAttribute(PTP_MSG_ORIGIN_TIMESTAMP_SECONDS, m_lastSyncSeconds)
    result = PtpManagerC.ptpGetMsgAttribute(PTP_MSG_ORIGIN_TIMESTAMP_NANOSECONDS, m_lastSyncNanoSeconds)
    
    MessageReceivedHandler = 0
End Function

Function FindActiveClocks() As Long
    Dim result As Long
    Dim i As Byte
    Dim clockCount As Long
    Dim clkInx As Long
    
    ' Set to search for clocks in all domains
    For i = 0 To 127
        result = PtpManagerC.ptpAddSearchDomain(i)
    Next i
    
    ' Get data for all clocks
    result = PtpManagerC.ptpUpdateClocks()
    
    FindActiveClocks = result
      
End Function

Sub LogSynchronizationHeaders()
    Dim header1, header2, clockSeparator, clockID As String
    Dim result As Long
    Dim clockCount, clkInx As Long
    Dim domain As Byte
    
    header1 = "SyncTime"
    header2 = ""
    clockSeparator = ", "
    
    result = PtpManagerC.ptpGetClockCount(clockCount)
        
    For clkInx = 0 To clockCount - 1
        Dim clock As Long
        result = PtpManagerC.ptpGetClock(clkInx, clock)
        If (result = 0) Then
            ' Get the clock ID
            result = PtpManagerC.ptpGetClockStringAttribute(clock, PTP_ATTR_DEFAULTDS_CLOCK_IDENTITY, clockID)
            result = PtpManagerC.ptpGetClockAttribute(clock, PTP_ATTR_DEFAULTDS_DOMAIN_NUMBER, domain)
            header1 = header1 & clockSeparator & clockID & "[" & domain & "]"
            header2 = header2 & ", Offset, Delay"
            clockSeparator = ",, "
            
        End If
    Next clkInx
    
    Print #1, header1
    Print #1, header2
End Sub

Sub LogLastSynchronizationInfo()
    Dim result As Long
    Dim syncLine, clockEntry As String
    
    ' Use a fixed string of reasonable length for the ptpTimeToString return
    Dim lastSyncTime As String * 256
    
    Dim clockCount, clkInx, histInx, histCount, clock As Long
    Dim histSec As PtpManagerC.BigSeconds
    Dim histNsec As Long
    Dim clkOffset, clkDelay As Double
    Dim histEntryFound As Boolean
    
    ' If no previous sync time recorded, no info to gather
    If (m_lastSyncSeconds.upperSeconds = 0 And m_lastSyncSeconds.lowerSeconds = 0) Then Exit Sub
    
    result = PtpManagerC.ptpGetClockCount(clockCount)
    
    ' Get the PTP time of the last sync as a string (note it is a fixed string that needs later trimming)
    result = ptpTimeToString(lastSyncTime, 256, m_lastSyncSeconds.upperSeconds, m_lastSyncSeconds.lowerSeconds, m_lastSyncNanoSeconds, 0)
    
    ' Quote the time so Excel won't try to convert it to a float, losing precision
    syncLine = "'" & PtpManagerC.TrimFixedString(lastSyncTime) & "'"
    
    result = PtpManagerC.ptpGetClockCount(clockCount)
    
    For clkInx = 0 To clockCount - 1
        histEntryFound = False
        result = PtpManagerC.ptpGetClock(clkInx, clock)
        If (result = 0) Then
            result = PtpManagerC.ptpGetClockHistoryCount(clock, histCount)
            For histInx = 0 To histCount - 1
                result = PtpManagerC.ptpGetClockHistoryAttribute(clock, histInx, PTP_ATTR_HISTORY_TIMESTAMP_SECONDS, histSec)
                result = PtpManagerC.ptpGetClockHistoryAttribute(clock, histInx, PTP_ATTR_HISTORY_TIMESTAMP_NANOSECONDS, histNsec)
                
                If (histSec.upperSeconds = m_lastSyncSeconds.upperSeconds And _
                    histSec.lowerSeconds = m_lastSyncSeconds.lowerSeconds And _
                    histNsec = m_lastSyncNanoSeconds) Then
                    result = PtpManagerC.ptpGetClockHistoryDoubleAttr(clock, histInx, PTP_ATTR_HISTORY_OFFSET_FROM_MASTER, clkOffset)
                    result = PtpManagerC.ptpGetClockHistoryDoubleAttr(clock, histInx, PTP_ATTR_HISTORY_PATH_DELAY, clkDelay)
                    clockEntry = ", " & clkOffset & ", " & clkDelay
                    syncLine = syncLine & clockEntry
                    histEntryFound = True
                End If
            Next histInx
            If (Not histEntryFound) Then syncLine = syncLine & ",,"
        End If
    Next clkInx
    
    Print #1, syncLine
        
End Sub
