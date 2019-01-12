Attribute VB_Name = "M34420ALearnString"
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
''' Module Name:    M34420ALearnString
'''
''' Description:    Module to query instrument for its settings and
'''                 build a 'learn' string that can be sent to the
'''                 instrument to recover these settings.
'''
'''                Copyright  ©  2000 Agilent Technologies, Inc.
'''
''' Date            Developer
'''                 Agilent Technologies
''' -------------------------------------------------------------------------
Public Property Get Get34420ALearnString(dmm As VisaComLib.FormattedIO488) _
                    As String
    Dim s As String, t As String, u As String, v As String, w As String
    Dim i As Integer

    s = ""

    'get the base configuration
    dmm.WriteString "conf?"
    t = dmm.ReadString
    cleanString t
    t = removeQuotes(t) 'strip quotes

    s = s + "conf:" + t

    'channel
    If _
        InStr(t, "VOLT") <> 0 _
    Then
        dmm.WriteString "rout:term?"
        u = dmm.ReadString
        cleanString u
        s = s + ";:rout:term " + u
    End If

    'auto ranging
    If _
        InStr(t, "VOLT") <> 0 Or _
        InStr(t, "RES") <> 0 _
    Then
        i = InStr(1, t, " ") 'find the function
        u = Left(t, i - 1)
        u = u + ":range:auto?" 'now ask if auto ranging on
        dmm.WriteString u
        v = dmm.ReadString
        cleanString v 'will hold 0 or 1 (auto=1)

        u = Left(u, Len(u) - 1) 'get the range auto command
        u = u + " " + v 'makes the range auto command
        s = s + ";:sense:" + u 'add it to the current lrn string

    End If
    
    s = s & vbLf
    
    'NPLC
    If _
        InStr(t, "VOLT") <> 0 Or _
        InStr(t, "RES") <> 0 Or _
        InStr(t, "TEMP") <> 0 _
    Then
        i = InStr(1, t, " ") 'find the function
        u = Left(t, i - 1)

        u = u + ":NPLC?" 'ask integration (power line cycles)
        dmm.WriteString u
        v = dmm.ReadString
        cleanString v

        u = Left(u, Len(u) - 1) 'rid of '?'
        u = u + " " + v 'builds the command
        s = s + ";:sense:" & u

    End If
    
    s = s & vbLf

    'added for 34420 - temperature stuff
    'temperature config
    'thermistor requires only conf? above
    'rtd requires rtd type(85 or 91) and 0 deg C res val (4.9 ohm to 2.1k ohm)
    'tc requires tc type, ref junction type, and ref temp
    '                                 if rjun type fixed (-1 to 55 deg C)
    'transducer type
    If InStr(t, "TEMP") <> 0 Then

        i = InStr(1, t, " ") 'find the function TEMP
        u = Left(t, i - 1)

        'prepend temp units before conf command
        w = "unit:" + u + "?"
        dmm.WriteString w
        v = dmm.ReadString
        cleanString v

        w = Left(w, Len(w) - 1) 'rid of '?'
        w = w + " " + v 'builds the command
        s = w + ";:" + s

        u = u + ":tran:"
        w = u + "type?" 'ask what type of transducer
        dmm.WriteString w
        v = dmm.ReadString
        cleanString v

        'rtd (resistance temp device)
        If InStr(v, "FRTD") <> 0 Then

            'rtd resistance
            u = u + v + ":res?" 'ask rtd resistance
            dmm.WriteString u
            v = dmm.ReadString
            cleanString v
            u = Left(u, Len(u) - 1) 'rid of '?'
            w = Left(w, Len(v) - 1)

            u = u + " " + v 'builds the command

            s = s + ";:sense:" + u
        s = s & vbLf
        'thermocouple
        ElseIf InStr(v, "TC") <> 0 Then
            'tc ref junc type
            u = u + "tc:rjun"
            w = u + ":type?" 'ask tc ref junc type
            dmm.WriteString w
            v = dmm.ReadString
            cleanString v

            w = Left(w, Len(w) - 1) 'rid of '?'
            w = w + " " + v 'builds the command
            s = s + ";:sense:" + w 'add it to the current lrn string

            'tc ref junc temp if type is FIXED (must be in range -1 C to 55 C)
            If InStr(v, "FIX") Then
                u = u + "?" 'now ask for ref junc temp

                dmm.WriteString u
                v = dmm.ReadString
                u = Left(u, Len(u) - 1) 'rid of '?'
                cleanString v
                u = u + " " + v       'builds the command

                s = s + ";:sense:" + u  'add it to the current lrn string"
            End If

        End If

    End If

    s = s & vbLf

    'calc func
    dmm.WriteString "calc:func?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:calc:func " + u

    'calc state
    dmm.WriteString "calc:state?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:calc:state " + u

    'data feed
    dmm.WriteString "data:feed? rdg_store"
    u = dmm.ReadString
    cleanString u
    s = s + ";:data:feed rdg_store," + u

    'trig source
    dmm.WriteString "trig:sour?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:trig:sour " + u
    
    s = s & vbLf

    'trig delay
    dmm.WriteString "trig:delay?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:trig:delay " + u

    'trig delay auto
    dmm.WriteString "trig:delay:auto?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:trig:delay:auto " + u

    'sample count
    dmm.WriteString "sample:count?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:sample:count " + u

    'trig count
    dmm.WriteString "trig:count?"
    u = dmm.ReadString
    cleanString u
    s = s + ";:trig:count " + u


    Get34420ALearnString = s

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


