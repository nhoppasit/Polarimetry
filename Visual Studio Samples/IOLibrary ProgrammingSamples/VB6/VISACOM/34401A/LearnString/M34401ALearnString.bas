Attribute VB_Name = "M34401ALearnString"
Option Explicit
' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2000 Agilent Technologies Inc. All rights
'    reserved.
'
' You have a royalty-free right to use, modify, reproduce and distribute
' the Sample Application Files (and/or any modified version) in any way
' you find useful, provided that you agree that Agilent has no
' warranty,  obligations or liability for any Sample Application Files.
'
' Agilent Technologies provides programming examples for illustration only,
' This sample program assumes that you are familiar with the programming
' language being demonstrated and the tools used to create and debug
' procedures. Agilent support engineers can help explain the
' functionality of Agilent software components and associated
' commands, but they will not modify these samples to provide added
' functionality or construct procedures to meet your specific needs.
' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

''' -------------------------------------------------------------------------
''' Module Name:    M34401ALearnString
'''
''' Description:    Module to query instrument for its settings and
'''                 build a 'learn' string that can be sent to the
'''                 instrument to recover these settings.
'''
'''                Copyright  ©  2000 Agilent Technologies, Inc.
'''
''' Date            Developer
''' May 12, 2000    Agilent Technologies
''' -------------------------------------------------------------------------

Public Property Get Get34401ALearnString(dmm As VisaComLib.FormattedIO488) As String
    ' Query the instrument for the various settings and build up a
    ' string of commands that can be send to the instrument.
    Dim learnString  As String
    Dim strConfig As String
    Dim strFunction As String
    Dim strRange As String
    Dim strReply As String
    Dim strNPLC As String
    
    Dim Position As Integer

    learnString = "" 'build learn string into s

    'get the configuration
    dmm.WriteString "conf?"
    strConfig = dmm.ReadString
    
    cleanString strConfig
    strConfig = removeQuotes(strConfig)

    learnString = learnString + "conf:" & strConfig

    'auto ranging
    If _
        InStr(strConfig, "VOLT") <> 0 Or _
        InStr(strConfig, "CURR") <> 0 Or _
        InStr(strConfig, "RES") <> 0 Or _
        InStr(strConfig, "FREQ") <> 0 Or _
        InStr(strConfig, "PER") <> 0 _
    Then
        If InStr(strConfig, "VOLT:RAT") <> 1 Then
            Position = InStr(1, strConfig, " ") 'find the function
            If Position Then strFunction = Left(strConfig, Position - 1)
            If InStr(strFunction, "FREQ") Or InStr(strFunction, "PER") Then
                strFunction = strFunction + ":VOLT"
            End If
            strFunction = strFunction & ":range:auto?"
            
            ' now ask if auto ranging on
            dmm.WriteString strFunction
            strReply = dmm.ReadString 'will hold 0 or 1
            cleanString strReply
    
            strFunction = Left(strFunction, Len(strFunction) - 1) 'get the range auto command
            strFunction = strFunction + " " + strReply 'makes the range auto command
            learnString = learnString + ";:sense:" + strFunction 'add it to the current lrn string
        End If
    End If

    'NPLC
    If _
        InStr(strConfig, "VOLT:DC") <> 0 Or _
        (InStr(strConfig, "CURR") <> 0 And InStr(strConfig, "AC") = 0) Or _
        InStr(strConfig, "RES") <> 0 _
    Then
        Position = InStr(1, strConfig, " ") 'find the function
        strFunction = Left(strConfig, Position - 1)

        strFunction = strFunction + ":NPLC?"
        dmm.WriteString strFunction
        strReply = dmm.ReadString
        cleanString strReply
        
        ' replaced strReply with StrFunction
        strFunction = Left(strFunction, Len(strFunction) - 1) 'rid of '?'
        
        strFunction = strFunction + " " + strReply 'builds the command
        learnString = learnString + ";:sense:" & strFunction

    End If
    
    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf

    'aperture
    If _
        InStr(strConfig, "FREQ") <> 0 Or _
        InStr(strConfig, "PER") <> 0 _
    Then
        Position = InStr(1, strConfig, " ") 'find the function
        strFunction = Left(strConfig, Position - 1)

        strFunction = strFunction + ":aper?"
        dmm.WriteString strFunction
        strReply = dmm.ReadString
        cleanString strReply

        strFunction = Left(strFunction, Len(strFunction) - 1) 'rid of '?'
        strFunction = strFunction + " " + strReply 'builds the command
        learnString = learnString + ";:sense:" + strFunction
    End If



    'det:bandwidth
     dmm.WriteString "det:band?"
     strReply = dmm.ReadString
     cleanString strReply

     learnString = learnString + ";:sense:det:band " + strReply

    'auto zero
    dmm.WriteString "zero:auto?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:sense:zero:auto " + strReply


    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf
    
    'impedence
    dmm.WriteString "inp:imp:auto?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:inp:imp:auto " + strReply



    'calc func
    dmm.WriteString "calc:func?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:func " + strReply



    'calc state
    dmm.WriteString "calc:state?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:state " + strReply
    
    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf

    'calc offset
    dmm.WriteString "calc:null:offset?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:null:offset " + strReply

    
    'db ref
    dmm.WriteString "calc:db:ref?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:db:ref " + strReply

    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf

    'dbm ref
    dmm.WriteString "calc:dbm:ref?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:dbm:ref " + strReply



    'limit lower
    dmm.WriteString "calc:limit:lower?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:limit:lower " + strReply

    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf

    'lim upper
    dmm.WriteString "calc:lim:upper?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:calc:lim:upper " + strReply



    'data feed
    dmm.WriteString "data:feed?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:data:feed rdg_store," + strReply

    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf

    'trig source
    dmm.WriteString "trig:sour?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:trig:sour " + strReply

    
    'trig delay
    dmm.WriteString "trig:delay?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:trig:delay " + strReply
    
    ' add a LF to break up the string and make RS232 more reliable
    learnString = learnString & vbLf
    
    'trig delay auto
    dmm.WriteString "trig:delay:auto?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:trig:delay:auto " + strReply



    'sample count
    'trig delay auto
    dmm.WriteString "sample:count?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:sample:count " + strReply



    'trig count
    dmm.WriteString "trig:count?"
    strReply = dmm.ReadString
    cleanString strReply
    learnString = learnString + ";:trig:count " + strReply



    Get34401ALearnString = learnString

End Property


Private Sub cleanString(DirtyString As String)
    ' removes any CR and LF at end of string
    
    Trim DirtyString
    
    DirtyString = removeEndingCharacter(DirtyString, vbLf)
    DirtyString = removeEndingCharacter(DirtyString, vbCr)
    DirtyString = removeEndingCharacter(DirtyString, vbLf)
    
    
End Sub

Private Function removeEndingCharacter(stringIn As String, _
                            character As String) As String
    ' removes ending character of type specified
    Dim Position As Long
    
    Position = InStr(1, stringIn, character, vbTextCompare)
    If Position Then
        removeEndingCharacter = Left$(stringIn, Position - 1)
    Else
        removeEndingCharacter = stringIn
    End If

End Function

Private Function removeQuotes(stringIn As String) As String
' Removes quotes from either end of the string
    Dim strTemp As String
    
    
    strTemp = stringIn
    
    If Asc(Right$(strTemp, 1)) = 34 Then strTemp = Left$(strTemp, Len(strTemp) - 1)
    If Asc(Left$(strTemp, 1)) = 34 Then strTemp = Right$(strTemp, Len(strTemp) - 1)
    
    removeQuotes = strTemp
End Function

