Attribute VB_Name = "modMonitor"
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2001 Agilent Technologies Inc.  All rights reserved.
'''
''' You have a royalty-free right to use, modify, reproduce and distribute
''' the Sample Application Files (and/or any modified version) in any way
''' you find useful, provided that you agree that Agilent Technologies has no
''' warranty,  obligations or liability for any Sample Application Files.
'''
''' Agilent Technologies provides programming examples for illustration only,
''' This sample program assumes that you are familiar with the programming
''' language being demonstrated and the tools used to create and debug
''' procedures. Agilent Technologies support engineers can help explain the
''' functionality of Agilent Technologies software components and associated
''' commands, but they will not modify these samples to provide added
''' functionality or construct procedures to meet your specific needs.
''' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'

Public Sub GetScanList(io As VisaComLib.FormattedIO488, lst As ListBox)
    ' get the scan list and fill in the combo box on the toolbar
    Dim strChannelList() As String
    Dim cancel As Boolean
    Dim i As Integer

    On Error Resume Next

    GetScanListArray io, strChannelList, cancel

    If (cancel = False) Then
        With lst
            .Clear
            .AddItem "Front Panel"
            For i = 0 To UBound(strChannelList)
                lst.AddItem "Channel " & strChannelList(i)
            Next i
            lst.ListIndex = 1
        End With                                           ' monitorCB
    End If
End Sub

Private Sub GetScanListArray(io As VisaComLib.FormattedIO488, strChannelList() As String, cancel As Boolean)
    ' Returns the scan list as an array of strings
    ' returns cancel as true if there is an error
    Dim strTemp As String
    Dim position As Long
    Dim i As Long

    On Error GoTo getScanListError

    cancel = False

    io.WriteString "route:scan?"
    ' return the channel list as a string
    strTemp = io.ReadString
    position = InStr(1, strTemp, "@", vbBinaryCompare)
    If position Then
    ' get rid of the first two characters '(@'
        strTemp = Mid$(strTemp, position + 1, Len(strTemp) - position - 2)
        strChannelList = Split(strTemp, ",")
    End If
    Exit Sub

getScanListError:
    cancel = True
End Sub
Public Function Monitor(io As VisaComLib.FormattedIO488, txt As TextBox, ByVal channel As String)
    ' calls the instrument and gets the reading for channel or if
    ' channel is not a channel number uses the channel set on front panel
    Dim reading As Double
    Dim cmd As String
    Dim strTemp As String

    cmd = "Route:Monitor (@" & channel & ")"

    With io
        If IsNumeric(channel) Then
            .WriteString cmd
            strTemp = "ch " & Str$(channel) & ",  "
        Else
            strTemp = "Front Panel, "
        End If
        .WriteString "Route:Monitor:State ON"
        .WriteString "Route:Monitor:Data?"
        reading = .ReadNumber
        txt.SelText = strTemp & Str$(reading) & vbCrLf
    End With


End Function
