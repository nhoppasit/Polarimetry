VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsHPGL2ToPicture"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'Attribute VB_Description = "Takes a binary HPGL/2 file and a picture box and draws the grphics in the picture box."
'Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
'Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
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

' defines the Character Plot (CP) cell size and location
' see pg 160 of The 'HP-GL/2 Reference' guide PN 5959-9733
Private Type CPcell
    LF As Long
    PointSize As Long
    CapHeight As Single
    charWidth As Single
    charPlotCellWidth As Single
    charOrigX As Single
    charOrigY As Single
End Type
Dim CPcell As CPcell

' determines the spacing and angle of labels
Private Type RelativeDirection
    run As Single
    rise As Single
End Type
Dim CharRD As RelativeDirection

Private Type ScalingPoints
    P1x As Single
    P1y As Single
    P2x As Single
    P2y As Single
End Type
Dim p1p2 As ScalingPoints

' this parameter is updated with AA, Ar, At, DF, DI,
' DR, DV,IN,LO, PA, PE, PR, RO, RT
Private Type CarriageReturnPoint
    Xaxis As Single
    Yaxis As Single
End Type
Dim CRpoint As CarriageReturnPoint

' Holds the  pen colors
Dim UserPenColors(7) As Long
Dim defPenColors(7) As Long
' Line type such as solid, dashed
Dim UserLineType(1 To 7) As Integer
Dim defLineType(1 To 7) As Integer

' Saves penup/down position
Dim m_PenUp As Boolean

' the aspect ratio (y:x)from the scaling points
Private m_AspectRatio As Double


'local variable(s) to hold property value(s)
Private mvarisDefaultPenColors As Boolean 'local copy
Private mvarisDefaultAspectRatio As Boolean 'local copy

' Picture box object
Private mvarPictureBox As Object 'local copy
'Private mvarPictureBox As PictureBox 'local copy    ' ### for development
'Private mvarPictureBox As Printer 'local copy       ' ### for development

Private mvarHPGL2Data As Variant 'local copy
Private mvarhpgl2cmds As Variant 'local copy
Private mvarBackcolor As Long 'local copy
Private mvarZoom As Double 'local copy
Private mvarIncludeTimeStamp As Boolean
Private mvarIsMonochrome As Boolean
Private mvarLineThickness As Long 'local copy
Private m_CommandList() As String
Private m_NumberOfCmds As Long          ' the max index for the number of commandList array
Private m_MarkerSymbol As String

' default values
Const def_mvarBackcolor = vbWhite
Const def_mvarZoom = 95
Const def_mvarIncludeTimeStamp = False
Const def_mvarIsMonochrome = False
Const def_mvarFontName = "Courier New"
Const def_mvarLineThickness = 1
Const def_m_AspectRatio = 0.7727


Public Property Let LineThickness(ByVal vData As Long)
Attribute LineThickness.VB_Description = "Sets the thickness of all lines drawn"
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.LineThickness = 5
    mvarLineThickness = vData
End Property


Public Property Get LineThickness() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.LineThickness
    LineThickness = mvarLineThickness
End Property

Public Property Let Zoom(ByVal vData As Double)
Attribute Zoom.VB_Description = "Sets and returns the size in per cent of the graphics relative to the picture box. Value is in per cent generally from  50 to 150."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.Zoom = 5
    mvarZoom = vData
End Property


Public Property Get Zoom() As Double
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.Zoom
    Zoom = mvarZoom
End Property

Public Property Let Backcolor(ByVal vData As Long)
Attribute Backcolor.VB_Description = "Sets and returns the color setting of the background or canvas color."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.Backcolor = 5
    mvarBackcolor = vData
End Property


Public Property Get Backcolor() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.Backcolor
    Backcolor = mvarBackcolor
End Property



Public Function PenColor(ByVal Pen As Long, Optional ByVal color As Long = -1) As Long
Attribute PenColor.VB_Description = "Assigns or returns the user defined pen colors. You can use the VB color constants."
    ' sets pen color when the color is given
    ' returns color for specified pen
    ' Uses the user colors when defaultcolor is set to true
    If color <> -1 Then       ' save pen color
        UserPenColors(Pen) = color
    End If
    
    PenColor = UserPenColors(Pen)
        
End Function

Public Function PenLineType(ByVal Pen As Long, Optional ByVal lineType As Long = -1) As Long
Attribute PenLineType.VB_Description = "Assigns returns pen line type"
    ' sets pen line type when the type is given
    ' returns line type for specified pen
    If lineType <> -1 Then       ' save pen color
        UserLineType(Pen) = lineType
    End If
    
    PenLineType = UserLineType(Pen)

End Function

Private Sub Class_Initialize()
    ' Set the default values, and set the user values
    ' to default
    Dim i As Long
    
    On Error Resume Next
    
    '"""""""""""""""""""""""""""""""""""""""""""""
    ' COLORS
    ' Set default pen colors
    defPenColors(0) = def_mvarBackcolor
    defPenColors(1) = vbBlack
    defPenColors(2) = vbRed
    defPenColors(3) = vbGreen
    defPenColors(4) = vbYellow
    defPenColors(5) = vbBlue
    defPenColors(6) = vbMagenta
    defPenColors(7) = vbCyan
    
    ' Line type
    ' line type is solid
    defLineType(1) = vbSolid
    defLineType(2) = vbSolid
    defLineType(3) = vbSolid
    defLineType(4) = vbSolid
    defLineType(5) = vbSolid
    defLineType(6) = vbSolid
    
    
    ' load the user colors with default values
    For i = 1 To 7
        UserPenColors(i) = defPenColors(i)
    Next i
    
    ' load the user line type with default values
    For i = 1 To 7
        UserLineType(i) = defLineType(i)
    Next i
    
    mvarBackcolor = def_mvarBackcolor
    mvarIncludeTimeStamp = def_mvarIncludeTimeStamp
    mvarIsMonochrome = def_mvarIsMonochrome
    
    mvarPictureBox.Font.Name = def_mvarFontName
    mvarPictureBox.FontBold = False
    mvarLineThickness = def_mvarLineThickness
   
    '"""""""""""""""""""""""""""""""""""""""""""""
    ' Miscellaneous
    mvarZoom = def_mvarZoom
    m_AspectRatio = def_m_AspectRatio
    m_MarkerSymbol = ""
    
    ' set the list of commands
    ' all commands must be capital letters for test to work
    Dim strTemp As String
    ' string of commands is seperated by a space
    ' add any new commands here
    strTemp = "VS PA DF LB PU PD SP SR DR IP SM CP SC LT VSI"
    m_CommandList = Split(strTemp)
    
    m_NumberOfCmds = UBound(m_CommandList)
    
End Sub

Private Function isValidCmd(ByVal cmd As String) As Boolean
Attribute isValidCmd.VB_Description = "Returns true if command is a supported HPGL2 command."
'*****************************************************
' Purpose: Tests to see if the command is a valid command
'       for a termination character
'
' Inputs: cmd; a two letter (upper case) string that
'               may be a command
'
' Returns: true if cmd is a string in the command List
'*****************************************************
    Dim i As Long
    
    On Error GoTo ValidError
    
    ' Test to see if both are capital letters
    ' if not don't bother to test
    For i = 1 To 2
        Select Case Mid$(cmd, i, 1)
            Case "A" To "Z"
                ' valid letters
            Case Else
                isValidCmd = False
                '*** EXIT FUNCTION
                Exit Function
        End Select
    Next i

    
    ' If it gets here both characters are Uppercase
    ' Test if it is a command
    For i = 0 To m_NumberOfCmds
        If m_CommandList(i) = cmd Then
            isValidCmd = True
                '*** EXIT FUNCTION
            Exit Function
        End If
    Next i
    
    ' If it gets here it failed the test
   
ValidError:
   isValidCmd = False
    
End Function

Public Property Let PlotterFontName(ByVal vData As String)
Attribute PlotterFontName.VB_Description = "Sets the font name of the labels. Use MS Sans Serif, Times New Roman, or Arial when the text spacing and length is critical to readability. Other fonts will be spaced according to  the font requirements."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.FontName = 5
    
    On Error Resume Next
    
    mvarPictureBox.Font.Name = def_mvarFontName
    mvarPictureBox.Font = vData
    mvarPictureBox.Font.Bold = False
End Property


Public Property Get PlotterFontName() As String
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.FontName
    PlotterFontName = mvarPictureBox.Font.Name

End Property



Public Property Let HPGL2cmds(ByVal vData As Variant)
Attribute HPGL2cmds.VB_Description = "Sets or returns each command of  the the HPGL2 data as a single element of a string array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.HPGL2cmds = 5
    mvarhpgl2cmds = vData
End Property


Public Property Set HPGL2cmds(ByVal vData As Variant)
'used when assigning an Object to the property, on the left side of a Set statement.
'Syntax: Set x.HPGL2cmds = Form1
    Set mvarhpgl2cmds = vData
End Property


Public Property Get HPGL2cmds() As Variant
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.HPGL2cmds
    If IsObject(mvarhpgl2cmds) Then
        Set HPGL2cmds = mvarhpgl2cmds
    Else
        HPGL2cmds = mvarhpgl2cmds
    End If
End Property



Public Sub Plot()
Attribute Plot.VB_Description = "Plots the HPGL2 graphics and plots it in the picture box. Uses the binary HPGL2 data. Must be performed prior to a refresh command."
    ' takes the binary data, converts it to string commands
    ' and then calls the routine that will plot the data
    ' to the picture box

   On Error GoTo PlotError

    ConvertBinaryToStringCmds
    
    Refresh
   
   Exit Sub
   
    
PlotError:
   Debug.Print "Plot, error = "; Err.Description
End Sub

Public Property Set PictureBox(ByVal vData As Object)
Attribute PictureBox.VB_Description = "Sets or returns the picture box"
'used when assigning an Object to the property, on the left side of a Set statement.
'Syntax: Set x.PictureBox = Form1
    Set mvarPictureBox = vData
    
    DFinst ""

End Property

Public Sub ConvertBinaryToStringCmds()
Attribute ConvertBinaryToStringCmds.VB_Description = "Converts a binary file from instrument into an array of HPGL2 string commands"
    ' Takes the binary data (from instrument) parses
    ' by command and puts it in the string command variable
    '
    ' NOTE; The binary data is always the original data
    '   from the instrument
    '   The String commands may contain a time stamp and other initialization
    Dim binCount As Long
    Dim count As Long
    Dim i As Long
    Dim ch As String
    Dim chNext As String
    Dim strTemp As String
    Dim cmd As String
    Dim charTerminator As Long
    Dim chBin As Byte
    Dim chBinNext As Byte
    Dim SCcmdIndex As Long
    
   ReDim mvarhpgl2cmds(20000)
   
   
   binCount = UBound(mvarHPGL2Data)
   ' if the first character is a '#' then replace them
   ' with ';'
   If mvarHPGL2Data(0) = Asc("#") Then
        mvarHPGL2Data(0) = Asc(";")
        mvarHPGL2Data(1) = Asc(";")
    End If
   
   
   mvarhpgl2cmds(0) = " "
   mvarhpgl2cmds(1) = "DF"
   count = 2
   charTerminator = Asc(";")
'####
StartTimer

    For i = 0 To binCount - 1
        'save this and the next byte
        chBin = mvarHPGL2Data(i)
        chBinNext = mvarHPGL2Data(i + 1)
        
        ' If there are two succesive elements with zero exit
        If chBin = 0 Then
            ' probably done, but check the next element also
            If chBinNext = 0 Then Exit For
        End If
        
'        ' Create characters from byte
        ch = UCase(Chr(chBin))
        chNext = UCase(Chr(chBinNext))
        cmd = ch & chNext
        
       
        ' Save the command and the associated data as one string
        If (Asc(ch) = charTerminator) Or (Asc(ch) = 59) Then
            ' add test here for CRLF and remove as leading characters for
            ' the HPGL2 command
            If Left$(strTemp, 2) = vbCrLf Then strTemp = Mid$(strTemp, 3)
            If Left$(strTemp, 1) = vbLf Then strTemp = Mid$(strTemp, 2)
            
            ' don't save spaces, extra semicolons and the like
            ' this not required, but makes array smaller
            strTemp = Trim$(strTemp)
            If Len(strTemp) >= 2 Then
                 mvarhpgl2cmds(count) = Trim$(strTemp)
                 ' increment count to start the next command
                 count = count + 1
            End If
            
            ' We have the next character, so put it in the string
            strTemp = Chr(chBinNext)
            ' reset to normal termination character if it's not a label
            cmd = chNext & UCase(Chr(mvarHPGL2Data(i + 2)))
            If "LB" = cmd Then          ' this is a Label
                charTerminator = 3
            Else
                charTerminator = Asc(";")
            End If
            
            ' Test for SC command to get the P1 point
            If cmd = "SC" Then
                ' This is the SC cmd, used to place time stamp
                SCcmdIndex = count
            End If
        
            ' increment the for Next loop to avoid reading the
            ' command
            ' *** INCREMENT FOR NEXT
            i = i + 1

        ' check for a valid cmd, but only if we are not processing a label
        ElseIf (charTerminator <> 3) And isValidCmd(cmd) Then
            '#### add test here for CRLF and remove as leading characters
            If Left$(strTemp, 2) = vbCrLf Then strTemp = Mid$(strTemp, 3)
            If Right$(strTemp, 2) = vbCrLf Then strTemp = Left$(strTemp, Len(strTemp) - 2)
            If Left$(strTemp, 1) = vbCr Then strTemp = Mid$(strTemp, 2)
            If Left$(strTemp, 1) = vbLf Then strTemp = Mid$(strTemp, 1)
            
            ' don't save spaces, extra semicolons and the like
            ' ### not required, but makes .hpg files smaller
            strTemp = Trim$(strTemp)
            If Len(strTemp) >= 2 Then
                 mvarhpgl2cmds(count) = Trim$(strTemp)
                 ' increment count to start the next command
                 count = count + 1
            End If
            strTemp = Chr(chBin)        ' first character of string and of command
            
            ' if this is a label then cmd then set termination character
            If "LB" = cmd Then          ' this is a Label
                charTerminator = 3
            Else
                charTerminator = Asc(";")
            End If
            If cmd = "SC" Then
                ' This is the SC cmd, used to place time stamp
                SCcmdIndex = count
            End If
        Else
            strTemp = strTemp & Chr(chBin)
        End If
    Next i
'###
Debug.Print "convert binary, time = "; EndTimer, ",    binary count = "; i

    ' The binary to string commands is done for received data
    ' now add additional commands for time stamp
    '""""""""""""""""""""""""""""""""""""""""""""""""""""""""
    On Error GoTo TimeStampError
    '
    ' create the x, y points for P1
    '
    Dim x As Long
    Dim y As Long
    Dim strP1P2() As String
    
    ' setting the timestamp will only work if there is
    ' real data.  SC command index must be greater than 0
    ' The x y points will place the time stamp in lower left
    If SCcmdIndex > 0 Then
        strTemp = mvarhpgl2cmds(SCcmdIndex)
        strTemp = Right$(strTemp, Len(strTemp) - 2)
    
        strP1P2 = Split(strTemp, ",")
    
        x = strP1P2(0) + 1
        y = strP1P2(2) + 5
    
        ' Add the time stamp if requested
        If mvarIncludeTimeStamp Then
            mvarhpgl2cmds(count) = "PU"
            mvarhpgl2cmds(count + 1) = "DR1,0"
            mvarhpgl2cmds(count + 2) = "SP1"
            mvarhpgl2cmds(count + 3) = "SR.6,1.2"
            mvarhpgl2cmds(count + 4) = "PA" & Format$(x) & "," & Format$(y)
            mvarhpgl2cmds(count + 5) = "LB" & Format(Now, "General Date")
            
            ReDim Preserve mvarhpgl2cmds(count + 5)
        Else
            ReDim Preserve mvarhpgl2cmds(count - 1)
        End If
    End If
    
    
    Exit Sub
    
TimeStampError:
    'Debug.Print "time stamp error "; Err.Description
    ' on error redim the commands array to make it small
    ' and it will not take lots of time with no data
    ReDim Preserve mvarhpgl2cmds(count + 5)

End Sub
Public Property Get PictureBox() As Object
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.PictureBox
    Set PictureBox = mvarPictureBox
End Property

' The ratio of height to width of the bitmap as intended by the instrument.

Property Get aspectRatio() As Double
Attribute aspectRatio.VB_Description = "The ratio of height to width of the bitmap as intended by the instrument."
    aspectRatio = m_AspectRatio
End Property

Property Let aspectRatio(ByVal newValue As Double)
    m_AspectRatio = newValue
End Property


Public Property Let isDefaultPenColors(ByVal vData As Boolean)
Attribute isDefaultPenColors.VB_Description = "When true uses the default pen colors. Set to false to use user defined pen colors."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.isDefaultPenColors = 5
    mvarisDefaultPenColors = vData
End Property


Public Property Get isDefaultPenColors() As Boolean
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.isDefaultPenColors
    isDefaultPenColors = mvarisDefaultPenColors
End Property



Public Sub Refresh()
Attribute Refresh.VB_Description = "Draws the graphics to the picture box. This uses the parsed HPGL2 data and is only slightly faster than the Plot command."
    ' use this to refresh picture box
    ' If you want to pass a printer object in place of picture box,
    '   then use the printHPGL2. It does not inlcude
    ' Picture box unique commands
    '
    ' You must either execute Plot or convertBinaryToStringCommands
    ' before calling this routine with binary data from instrument
    
    On Error Resume Next
    
    Screen.MousePointer = vbHourglass
    
    DFinst ""

    With mvarPictureBox
        .Backcolor = mvarBackcolor
        .Cls
        .Picture = LoadPicture("")
        .Picture = Nothing
        .DrawMode = vbCopyPen
    End With
    
    ' Send the picture to picture box
    RefreshPictureBox mvarhpgl2cmds
    
    mvarPictureBox.Refresh
    
    Screen.MousePointer = vbDefault
    
End Sub

Public Sub PrintHPGL2()
Attribute PrintHPGL2.VB_Description = "Use this in place of PictureBox when passing printer object , it does not have properties unique to picture box.  You must either execute Plot or convertBinaryToString Commands before calling this routine."
    ' use this when passing printer object in place of
    ' picture box, it does not have
    ' properties unique to picture box
    ' You must either execute Plot or convertBinaryToStringCommands
    ' before calling this routine
    
    RefreshPictureBox mvarhpgl2cmds
    
End Sub



Public Property Let HPGL2Data(ByVal vData As Variant)
Attribute HPGL2Data.VB_Description = "An array of bytes that contains the HPGL2 plotter code from the instrument."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.HPGL2Data = 5
    mvarHPGL2Data = vData
End Property


Public Property Set HPGL2Data(ByVal vData As Variant)
'used when assigning an Object to the property, on the left side of a Set statement.
'Syntax: Set x.HPGL2Data = Form1
    Set mvarHPGL2Data = vData
End Property


Public Property Get HPGL2Data() As Variant
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.HPGL2Data
    If IsObject(mvarHPGL2Data) Then
        Set HPGL2Data = mvarHPGL2Data
    Else
        HPGL2Data = mvarHPGL2Data
    End If
End Property

Private Sub RefreshPictureBox(cmdArray As Variant)
    ' Takes the HPGL2 string array contained in a variant and
    ' converts each command to a graphical command of the
    ' printer or pictureBox
    Dim i As Long
    Dim count As Long
    Dim cmd As String
    Dim cmdarg As String
    Dim strTemp As String
    
    On Error GoTo PlotError
    
    count = UBound(cmdArray)
    
    With mvarPictureBox
        .ForeColor = vbBlack
    End With
    
    mvarPictureBox.DrawWidth = mvarLineThickness
   
    m_PenUp = True
    For i = 0 To (count)
        strTemp = cmdArray(i)
        If Len(strTemp) >= 2 Then
            cmd = UCase(Left$(strTemp, 2))
            cmdarg = Right$(strTemp, Len(strTemp) - 2)
        Else
            cmd = ""
            cmdarg = "no command"
        End If
        routeHPGL_Cmd cmd, cmdarg
'Debug.Print "Command line "; cmd; ", "; cmdarg; ", color = "; mvarPictureBox.ForeColor ', , "  x, y "; mvarPictureBox.CurrentX; ", "; mvarPictureBox.CurrentY
'       mvarPictureBox.Refresh
    Next i
    
    Exit Sub
    
PlotError:
    Debug.Print "refreshPicturebox error, i = "; i; ", cmd ="; cmd; ", "; Err.Description
    mvarPictureBox.Refresh
    Resume Next
End Sub
Private Sub routeHPGL_Cmd(ByVal cmd As String, ByVal cmdarg As String)
    ' routes the HPGL command to the HPGL2 instruction

        Select Case cmd
                
            Case "PA"       ' plot absolute
                PAinst cmdarg
            Case "DF"       ' Default Values
                DFinst ""
            Case "DR"       ' establishes label direction and CR_Point
                DRinst cmdarg
            Case "IP"       ' Input relative scale
               'typemvarpicturebox
                IPinst cmdarg
            Case "LB"       ' Print label
                LBinst cmdarg
            Case "LT"       ' Line type
                ' user selected by pen
                 LTinst cmdarg
            Case "SM"        '  Sets the Symbol to character
                SMinst cmdarg
            Case "CP"        '  Moves specified number of spaces/lines
                CPinst cmdarg
            'Case "PE"       ' encrypted polyline code
            ' not used for scope and semi analyzer
            '    PEinst
            Case "PU"       ' Pen up
                m_PenUp = True
            Case "PD"       ' Pen down
                m_PenUp = False
                PDinst cmdarg
            Case "SC"       ' Sets the scale for picture box
                SCinst cmdarg
            Case "SP"        ' Select Pen
                SPinst cmdarg
            Case "SR"        ' Relative Character size
                SRinst cmdarg
            Case "SI"        ' set Character size in cm
                SIinst cmdarg
            Case "VS"        ' relative pen speed, not implemented
               '
            ''''''''''''''''''''''''''''''''''''''''''
            ' These commands are not HPGL2 commands or not implemented
            Case Else        ' commands not implemented
                If Len(cmd) = 2 Then Debug.Print "Not Implemented cmd = "; , cmd; ", arg. = "; cmdarg
        End Select

End Sub

Private Sub DFinst(cmdarg As String)
Attribute DFinst.VB_Description = "Sets the default values for the character size and direction."
    ' sets the default values
    ' only sets the character size and direction
    
    ' set default page size
    IPinst ""
    
    SCinst "0,6000,0,5000"
    ' set default character size
    SRinst ""
End Sub
Private Sub DRinst(cmdarg As String)
Attribute DRinst.VB_Description = "Sets the relative direction (rise and run) for a character space;  Save the location of the carriage return point. This is the reference point for labels (left margin)\r\n        ' affected by changes in p1p2 and DV"
        ' Relative Direction
        ' Save the location of the carriage return point
        ' this is the reference point for labels (left margin)
        ' affected by changes in p1p2 and DV
        Dim strDR() As String
        
        On Error GoTo DRerror
        
        CRpoint.Xaxis = mvarPictureBox.CurrentX
        CRpoint.Yaxis = mvarPictureBox.CurrentY
        
        strDR = Split(cmdarg, ",")
        
        If Len(cmdarg) > 2 Then ' extract the run and rise
            CharRD.rise = Val(strDR(1))
            CharRD.run = CPcell.charPlotCellWidth * Val(strDR(0))
        Else
            CharRD.rise = 0
            CharRD.run = CPcell.charPlotCellWidth
        End If
        
        Exit Sub
DRerror:
Debug.Print "DRinst error "; Err.Description
Resume Next
End Sub

Private Sub IPinst(cmdarg As String)
Attribute IPinst.VB_Description = "Sets new default locations for scaling points"
    ' set new default locations for scaling points
    Dim strP1P2() As String
    Dim delim As String
    
    On Error GoTo iperror
    
    If Len(cmdarg) > 0 Then
        delim = ","
        
        strP1P2 = Split(cmdarg, delim)
        
        With p1p2
            .P1x = strP1P2(0)
            .P1y = strP1P2(1)
            .P2x = strP1P2(2)
            .P2y = strP1P2(3)
            
            ' aspect ratio height to width
            m_AspectRatio = Abs((.P2y - .P1y) / (.P1x - .P2x))
        End With 'p1p2
        
    Else                        ' default setting
            
        m_AspectRatio = def_m_AspectRatio
    End If
'Debug.Print "IPinst, aspect ratio = "; m_AspectRatio
    Exit Sub
    
    
iperror:
     m_AspectRatio = def_m_AspectRatio

    
End Sub

Private Sub LBinst(cmdarg As String)
Attribute LBinst.VB_Description = "Prints text to the picture."
    ' Labels

    Dim nelem  As Long
    Dim strlen As Long
    Dim char As String
    Dim count As Long
    Dim x As Long
    Dim y As Long
    Dim charX As Single
    Dim fontHeight As Single
    Dim fontWidth As Single
    
    On Error GoTo LBerror
    
    CRpoint.Xaxis = mvarPictureBox.CurrentX
    CRpoint.Yaxis = mvarPictureBox.CurrentY

    fontHeight = -mvarPictureBox.TextHeight("B") * 0.75
    fontWidth = mvarPictureBox.TextWidth("B")
    
    count = 1
    nelem = 0
    strlen = Len(cmdarg)
    x = CRpoint.Xaxis
    y = CRpoint.Yaxis
    
    mvarPictureBox.CurrentX = x
    mvarPictureBox.CurrentY = y
    
    Do While (count <= strlen)
        char = Mid(cmdarg, count, 1)
        count = count + 1
        If char = vbCr Then
            mvarPictureBox.CurrentX = x
        ElseIf char = vbLf Then
            y = y - CPcell.LF
            mvarPictureBox.CurrentY = y
        ElseIf char = vbVerticalTab Then
            y = y + CPcell.LF
            mvarPictureBox.CurrentY = y
        Else
            ' print one character
            mvarPictureBox.CurrentY = mvarPictureBox.CurrentY + fontHeight
            If (mvarPictureBox.FontName = "MS Sans Serif") Or _
               (mvarPictureBox.FontName = "Times New Roman") Or _
               (mvarPictureBox.FontName = "Arial") Then   ' position for each character
                mvarPictureBox.Print char;
            Else                                            ' Let object set position
                charX = mvarPictureBox.CurrentX
                mvarPictureBox.Print char;
                mvarPictureBox.CurrentX = charX + CharRD.run
            End If ' proportional font
            mvarPictureBox.CurrentY = mvarPictureBox.CurrentY - fontHeight
        End If
    Loop
    
    Exit Sub
LBerror:
    Debug.Print "LB error, cmd ="; cmdarg
    Resume Next
    
End Sub

Private Sub CPinst(cmdarg As String)
Attribute CPinst.VB_Description = "Moves the pen the specified number of spaces and lines by  the character plot cells from the current pen location"
    ' moves the pen the specified number of spaces and lines by
    ' the character plot cells from the current pen location
    Dim spaces As Double
    Dim lines As Double
    Dim cmd() As String
    Dim current_X As Double
    Dim current_Y As Double
    
    
    cmd = Split(cmdarg, ",")
    
    If Len(cmdarg) > 2 Then                ' there are cmd parameters
        If IsNumeric(cmd(0)) Then
            spaces = Val(cmd(0)) * 2
        End If
        
        If IsNumeric(cmd(1)) Then
            lines = Val(cmd(1)) * 2
        End If
        current_X = mvarPictureBox.CurrentX
        current_Y = mvarPictureBox.CurrentY
        
        mvarPictureBox.CurrentX = current_X + CPcell.charWidth * spaces
        mvarPictureBox.CurrentY = current_Y - CPcell.LF * lines
        CRpoint.Yaxis = mvarPictureBox.CurrentY
    Else                                ' No parameters
        mvarPictureBox.CurrentX = CRpoint.Xaxis
        mvarPictureBox.CurrentY = CRpoint.Yaxis - CPcell.LF
        CRpoint.Yaxis = mvarPictureBox.CurrentY
    End If


    Exit Sub
CP_Error:

End Sub


Private Sub LTinst(cmdarg As String)
Attribute LTinst.VB_Description = "Selects the line type (DrawStyle) for graphics"
    ' LINE TYPE
    Dim lineType As Long
    
    lineType = Val(cmdarg) Mod 6
    mvarPictureBox.DrawStyle = lineType

End Sub
Private Sub PrintSymbol(cmdarg As String)
    Dim position As Long
    Dim xpt As Single
    Dim ypt As Single
    Dim fontHeight As Single
    Dim fontWidth As Single
    
    If Len(m_MarkerSymbol) = 0 Then
        ' no symbol, exit
        ' *** EXIT SUB
        Exit Sub
    End If
    
    
    position = InStr(1, cmdarg, ",")
    
    fontHeight = -mvarPictureBox.TextHeight(m_MarkerSymbol)
    fontWidth = mvarPictureBox.TextWidth(m_MarkerSymbol)

    
    If position Then
        xpt = (Val(Left$(cmdarg, position))) - 0.5 * fontWidth
        ypt = Val(Mid$(cmdarg, position + 1)) - 0.25 * fontHeight
    End If
    
    mvarPictureBox.CurrentX = xpt
    mvarPictureBox.CurrentY = ypt
    
    LBinst m_MarkerSymbol

End Sub
Private Sub PAinst(cmdarg As String)
Attribute PAinst.VB_Description = "Plot absolute. Moves pent to new point.  Paints a line from existing point if pen is down."
    ' Plot absolute
    Dim i As Long
    Dim cmdIndex As Long
    Dim xpt As String
    Dim ypt As String
    Dim cmd() As String
    
    
    cmd = Split(cmdarg, ",")
    cmdIndex = UBound(cmd)
    
    ' if the symbol mode is set then print the symbol
    If Len(m_MarkerSymbol) > 0 Then
        ' no symbol, exit
        PrintSymbol cmdarg
    End If
    
    If cmdIndex >= 0 Then
        For i = 0 To cmdIndex Step 2
            xpt = cmd(i)
            ypt = cmd(i + 1)
            SinglePlotAbsolute xpt, ypt
        Next i
    End If

    Exit Sub
PA_Error:

End Sub

Private Sub SinglePlotAbsolute(strXpt As String, strYpt As String)
    Dim xpt As Single
    Dim ypt As Single
    
    xpt = Val(strXpt)
    ypt = Val(strYpt)
    

    If m_PenUp Then
        mvarPictureBox.CurrentX = xpt
        mvarPictureBox.CurrentY = ypt
    Else
        mvarPictureBox.Line -(xpt, ypt), mvarPictureBox.ForeColor
    End If
    
End Sub

Private Sub PDinst(cmdarg As String)
Attribute PDinst.VB_Description = "Pen Down"
    ' Pen Down
    ' does not handle multiple x,y pairs
    Dim x As Single
    Dim y As Single
    
    x = mvarPictureBox.CurrentX
    y = mvarPictureBox.CurrentY
    mvarPictureBox.PSet (x, y)

End Sub

Private Sub SCinst(cmdarg As String)
Attribute SCinst.VB_Description = "Scale; Establishes a user-unit coordinate system for the picture box"
    ' Scale
    ' Establishes a user-unit coordinat system for the picture box
    ' if aspec ratio is to be maintained, size the picturebox to
    ' the correct aspect ratio and use maximum space available
    
    Dim strP1P2() As String
    Dim delim As String
    Dim P1x As Single
    Dim P1y As Single
    Dim P2x As Single
    Dim P2y As Single
    Dim sizeAdder As Double

    delim = ","

     strP1P2 = Split(cmdarg, delim)
    
    ' Set the scaling x,y points
    P1x = strP1P2(0)
    P2x = strP1P2(1)
    P1y = strP1P2(2)
    P2y = strP1P2(3)
    
    ' after this, the scale and plotter units are the same
    p1p2.P1x = P1x
    p1p2.P1y = P1y
    p1p2.P2x = P2x
    p1p2.P2y = P2y
    
    On Error Resume Next
    
    Dim WidthRatio As Double
    Dim HeightRatio As Double
    
    '###
    DoEvents
    
    ' we now know what the aspect ratio must be. Before we
    ' set the user scale we need to size the picture box to max
    ' size on form while still maintaining the aspect ratio
    ' Do ONLY FOR PICTURE BOX
    If TypeName(mvarPictureBox) = "PictureBox" Then
        With mvarPictureBox
            WidthRatio = 1 / mvarPictureBox.Parent.ScaleWidth
            HeightRatio = m_AspectRatio / (mvarPictureBox.Parent.ScaleHeight - mvarPictureBox.Top)
        
            If WidthRatio > HeightRatio Then ' Portrait
               mvarPictureBox.Width = mvarPictureBox.Parent.ScaleWidth
                mvarPictureBox.Height = mvarPictureBox.Width * m_AspectRatio
                'Debug.Print "w > h fix w"
            Else
                mvarPictureBox.Height = mvarPictureBox.Parent.ScaleHeight - mvarPictureBox.Top
                mvarPictureBox.Width = mvarPictureBox.Height / m_AspectRatio
            'Debug.Print "w < h fix h"
        End If
'    Debug.Print "SCinst, aspect ratio = "; m_AspectRatio; ", widhtR "; WidthRatio; ", heightR; "; HeightRatio; ""
'    Debug.Print "SCinst, pic box size "; mvarPictureBox.Width; ", "; mvarPictureBox.Height
'    mvarPictureBox.Picture = Nothing
    mvarPictureBox.Picture = mvarPictureBox.Image
'    mvarPictureBox.Refresh
    
    End With
    End If ' picture box
    
    '""""""""""""""""""""""""""""""""""""""
    ' Set the user scale for the picture box
    ' make drawing area larger/smaller for zoom (from option dialog)
    '
    sizeAdder = (1 - mvarZoom / 100) / 2
    
    P1x = P1x - (p1p2.P2x - p1p2.P1x) * sizeAdder
    P1y = P1y - (p1p2.P2y - p1p2.P1y) * sizeAdder
    P2x = P2x + (p1p2.P2x - p1p2.P1x) * sizeAdder
    P2y = P2y + (p1p2.P2y - p1p2.P1y) * sizeAdder
    
    
    With mvarPictureBox
        .ScaleMode = 0
        .ScaleTop = P2y
        .ScaleLeft = P1x
        .ScaleHeight = P1y - P2y
        .ScaleWidth = P2x - P1x
    End With
End Sub
Private Sub SIinst(cmdarg As String)
Attribute SIinst.VB_Description = "Set the absolute character size"
    ' Set the absolute character size
    ' the character size is given in cm, we convert to inches
    ' compare to the normal plotter page and then use the
    ' relative character size to set the character size
    Dim ypt As Single
    Dim xpt As Single
    Dim position As Long
    
    On Error GoTo SIerror
    
    
    position = InStr(1, cmdarg, ",")
    If position Then
        ypt = Val(Mid$(cmdarg, position + 1))
        xpt = Val(Left$(cmdarg, position))
    
        ' convert the cm to inches
        ypt = ypt * 0.394
        xpt = xpt * 0.394
        
        ' take the ratio to a page with .5 in border
        ypt = 100 * ypt / 10
        xpt = 100 * xpt / 7.5
    
        SRinst (Str$(xpt) & "," & Str$(ypt))
    Else
        ' set default char size
        SRinst ""
    End If
    
    
SIerror:
    SRinst ""
End Sub
Private Sub SMinst(cmdarg As String)
Attribute SMinst.VB_Description = "Sets the Symbol to character"
    ' Sets the Symbol to character
    If Len(cmdarg) > 0 Then
        m_MarkerSymbol = Left$(cmdarg, 1)
    Else
        m_MarkerSymbol = ""
    End If
    
End Sub

Private Sub SPinst(cmdarg As String)
Attribute SPinst.VB_Description = "Select Pen"
    ' Select Pen
    Dim penNumber As Long
    
    On Error Resume Next
    
    penNumber = Val(cmdarg)
    
    ' 0 is the backgroung color
    If penNumber = 0 Then
        mvarPictureBox.ForeColor = mvarBackcolor
        Exit Sub
    End If
    
    ' There are only 7 pen settings, so allow for more
    ' by recycling higher numbered pens
    Do Until penNumber <= 7
        penNumber = penNumber - 7
    Loop
    
    
    ' if monochrome is selected, make forecolor black
    If mvarIsMonochrome Then
        mvarPictureBox.ForeColor = vbBlack
        ' Select user line type
        mvarPictureBox.DrawStyle = UserLineType(penNumber)
    ElseIf mvarisDefaultPenColors Then
        mvarPictureBox.ForeColor = defPenColors(penNumber)
        mvarPictureBox.DrawStyle = defLineType(penNumber)
    Else
        mvarPictureBox.ForeColor = UserPenColors(penNumber)
        mvarPictureBox.DrawStyle = UserLineType(penNumber)
    End If
    
End Sub

Private Sub SRinst(cmdarg As String)
Attribute SRinst.VB_Description = "Set the relative character size"
    ' Set the relative character size
    Dim ypt As Single
    Dim xpt As Single
    Dim position As Long
    Dim points As Single
    
    
    position = InStr(1, cmdarg, ",")
    If position Then
        ypt = Val(Mid$(cmdarg, position + 1))
        xpt = Val(Left$(cmdarg, position))
    Else
        ypt = 1.5
        xpt = 0.75
    End If
    
    ' save the dimensions of the character Plot cell
    With CPcell
        .CapHeight = (ypt / 100) * (p1p2.P2y - p1p2.P1y)
        .charWidth = (xpt / 100) * (p1p2.P2x - p1p2.P1x)
        .LF = .CapHeight * (64 / 32)
        .charPlotCellWidth = .charWidth * (48 / 32)
        .PointSize = .LF * 48 / 64
    End With ' CPcell
    
    ' update the relative direction
    DRinst ""
    
    ' select an approximate font that is no larger than needed
    For points = 24 To 5 Step -0.5
        mvarPictureBox.FontSize = points
'        If -mvarPictureBox.TextHeight("BJ") <= CPcell.PointSize Then   ' acceptable font
        If mvarPictureBox.TextWidth("BJMO") <= (CPcell.charWidth * 4) Then   ' acceptable font
'        If mvarPictureBox.TextWidth("BJMN") <= (CPcell.charPlotCellWidth * 4) Then    ' acceptable font
            Exit For
        End If
    Next points

End Sub

Public Property Get IncludeTimeStamp() As Boolean
Attribute IncludeTimeStamp.VB_Description = "When true creates a time stamp as part of the HPGL2 binary to string convertion."
    IncludeTimeStamp = mvarIncludeTimeStamp
End Property

Public Property Let IncludeTimeStamp(ByVal vNewValue As Boolean)
    mvarIncludeTimeStamp = vNewValue
End Property

Public Property Get isMonochrome() As Boolean
Attribute isMonochrome.VB_Description = "When true, sets all pens to black."
    ' when true sets pens to vbBlack sets line type to dotted or solid
    isMonochrome = mvarIsMonochrome
End Property

Public Property Let isMonochrome(ByVal vNewValue As Boolean)
    ' when true sets pens to vbBlack sets line type to dotted or solid
    mvarIsMonochrome = vNewValue
End Property





