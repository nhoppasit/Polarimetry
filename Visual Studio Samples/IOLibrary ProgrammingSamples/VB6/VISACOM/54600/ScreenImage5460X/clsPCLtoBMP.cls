VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPclToBMP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Description = "Exctracts raster image from PCL byte array and displays in PictureBox"
Option Explicit

''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2001 Agilent Technologies Inc.  All rights reserved.
'''
''' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'
' default background of the PictureBox
Const def_mvarBackcolor = vbWhite   'pictureBox background

'possible errors in PCL byte array
Public Enum errorNumber
    noError = 0
    nodata = 1          'empty file
    noRasterData = 2    'no raster found in PCL data
    compModeNotSupp = 3 'PCL compression modes not supported
    tooHigh = 4         'height beyond max
    tooWide = 5         'width beyond max
End Enum

'as PCL byte array is scanned, various states are set depending on which
'PCL escape commands are found
Private Enum SearchState
    findEscape = 1
    findStar = 2
    findRasterCommand = 3
    findInRasterEscape = 4
    findInRasterStar = 5
    findInRasterCommand = 6
    rasterDone = 7
End Enum
Private state As SearchState

Private Enum compressMode
    noCompress = 0
    runLengthCompress = 1
    tiffCompress = 2
    deltaRowCompress = 3
    zeroRowCompress = 4
    adaptiveCompress = 5
End Enum
Private compMode As compressMode

'local variable(s) to hold property value(s)
Private mvardataLength As Long 'local copy
Private mvarclipHeight As Long 'local copy
Private mvarclipWidth As Long 'local copy - width in bytes
Private mvarmaxHeight As Long 'local copy
Private mvarmaxWidth As Long 'local copy - width in pixels
Private mvarrasterHeight As Long 'local copy - raster height in rows
Private mvarrasterWidth As Long 'local copy - raster width in bytes
Private mvarisClipHeight As Boolean 'local copy
Private mvarisClipWidth As Boolean 'local copy
Private mvarisRasterData As Boolean 'local copy
Private mvarPCLData As Variant 'local copy
Private mvarrasterData As Variant 'local copy
'Private mvarPictureBox As PictureBox 'local copy - object used instead
'Private mvarPictureBox As Printer 'local copy - object used instead
Private mvarPictureBox As Object 'local copy
Private mvarisPBoxScaled As Boolean 'local copy
Private mvarerrNum As errorNumber 'local copy


Public Sub PCLtoRaster()
' Takes PCL binary data (from instrument or file) and parses
' to find PCL escape codes. Various states are set depending on escape
' code. Builds one raster byte array. If more raster images in PCL file,
' they are ignored. Raster width and height are also determined and a constant
' width raster is assumed.
'
    Dim tmpRastData() As Byte
    Dim bufPointer As Long
    Dim rasterPointer As Long
    Dim valNum As Long
    Dim byteVal As Byte
    Dim isEscape As Boolean
    
    On Error GoTo pcltorastererr
    
    ReDim tmpRastData(2500000) 'set raster size conservatively

    mvarrasterData = tmpRastData
    
    'now we march through PCL data, keeping track of state depending on escape sequences,
    'finding raster width and height and filling out raster byte data
    
    state = findEscape
    mvarerrNum = noError
    bufPointer = 0
    rasterPointer = 0
    compMode = noCompress
    isEscape = False
    
    Do While (state <> rasterDone And bufPointer <= UBound(mvarPCLData))
    
        Select Case (state)
        
            Case findEscape
                
                'ESC' char (27)
                Do Until mvarPCLData(bufPointer) = &H1B Or _
                                    bufPointer >= UBound(mvarPCLData)
                    bufPointer = bufPointer + 1
                Loop
                
                If bufPointer = UBound(mvarPCLData) Then
                    If isEscape = True Then
                        state = rasterDone
                        Exit Sub
                    Else    'no escape byte found in data
                        state = rasterDone
                        mvarerrNum = noRasterData
                        Exit Sub
                    End If
                End If

                'we found an escape!
               isEscape = True
                
                state = findStar
                bufPointer = bufPointer + 1
                
            Case findStar   'ESC*' is typical raster type starting command sequence
                '*' char (42)
                If mvarPCLData(bufPointer) = &H2A Then
                    bufPointer = bufPointer + 1
                    state = findRasterCommand   'now look for raster type command
                Else
                    bufPointer = bufPointer + 1
                    state = findEscape  'back to finding next escape
                End If
                
            'ESC*' found, now look for following raster commands
            Case findRasterCommand
                state = findEscape  'default - go back to beginning
                
                'look for 'r' or 'b'
                'r' char (114)
                If mvarPCLData(bufPointer) = &H72 Then
                    bufPointer = bufPointer + 1
                    
                    'now look for 'A', '#A,' '#T', or '#S', or start over
                    'A' (65) - start raster data
                    If mvarPCLData(bufPointer) = &H41 Then
                        bufPointer = bufPointer + 1
                        state = findInRasterEscape
                    End If

                    '#'
                    valNum = getSize(bufPointer)   'get # if exists (number)
                    
                    byteVal = mvarPCLData(bufPointer)
                    
                    '#A' (65) - start raster data
                    If byteVal = &H41 Then
                        bufPointer = bufPointer + 1
                        state = findInRasterEscape

                    
                    '#T' (84) - set clip height
                    ElseIf byteVal = &H54 Then
                        mvarclipHeight = valNum
                        mvarisClipHeight = True
                        If valNum > (maxHeight) Then  'height too big
                            mvarerrNum = tooHigh
                            state = rasterDone
                        End If
                        bufPointer = bufPointer + 1

                    
                    '#S' (83) - set clip width
                    ElseIf byteVal = &H53 Then
                        mvarclipWidth = valNum
                        mvarisClipWidth = True
                        If valNum > (mvarmaxWidth) Then  'width too big
                            mvarerrNum = tooWide
                            state = rasterDone
                        End If
                        bufPointer = bufPointer + 1
                    End If
                
            'if b, look for '#M', '#m#W', '#W' (otherwise, go back to beginning)
            'if #W, process following raster data and set to raster data state
                'b' char (98)
                ElseIf mvarPCLData(bufPointer) = &H62 Then
                    bufPointer = bufPointer + 1
                    valNum = getSize(bufPointer)   'get # if exists
                    
                    byteVal = mvarPCLData(bufPointer)
                    
                    'M' char (77) or 'm' char (109) - set compression mode
                    If byteVal = &H4D Or byteVal = &H6D Then
                        If valNum = 0 Then
                            compMode = noCompress
                        ElseIf valNum = 2 Then
                            compMode = tiffCompress
                        Else
                            mvarerrNum = compModeNotSupp
                            state = rasterDone
                        End If
                        'if was 'm' char (109), get next # for #m#W command
                        If byteVal = &H6D Then
                            bufPointer = bufPointer + 1
                            valNum = getSize(bufPointer)   'get # if exists
                        End If
                    End If
                    
                    
                    'W' char (87) - raster data follows
                    If mvarPCLData(bufPointer) = &H57 Then
                        mvarisRasterData = True
                        state = findInRasterEscape
                        bufPointer = bufPointer + 1
                        buildRaster bufPointer, rasterPointer, valNum, compMode
                        
                    End If
                End If
            
            Case findInRasterEscape
                'now we're expecting ESC* + 1) row of raster data - 'b#W' (or 'b#m#W)
                '                           2) vertical offsets - 'b#Y'
                '                           3) compression modes - 'b#M or 'b#m'
                '                        or 4) end of raster data - 'rC' or 'rB'
                'other escape sequences are ignored
                'if row of raster data, process data
                'if vertical offset, fill rasterData with 0's and update row count
                'if compression mode, make sure # = 0 - otherwise unsupported now
                'if end of raster data, exit scan - additional rasters are ignored
                
                'ESC char (27)
                '########
                Do Until mvarPCLData(bufPointer) = &H1B Or _
                                    bufPointer >= UBound(mvarPCLData)
                    bufPointer = bufPointer + 1
                Loop
                
                If bufPointer > UBound(mvarPCLData) Then
                    state = rasterDone
                    Exit Sub
                End If
                
                state = findInRasterStar
                bufPointer = bufPointer + 1
                                
            Case findInRasterStar
                '* char (42)
                If mvarPCLData(bufPointer) = &H2A Then
                    bufPointer = bufPointer + 1
                    state = findInRasterCommand 'now look for allowed raster command
                Else
                    bufPointer = bufPointer + 1
                    state = findInRasterEscape 'back to looking for raster escapes
                End If

            Case findInRasterCommand
                'b' char (98)
                If mvarPCLData(bufPointer) = &H62 Then
                    state = findEscape  'set default
                    bufPointer = bufPointer + 1
                    valNum = getSize(bufPointer)   'get # if exists
                    
                    byteVal = mvarPCLData(bufPointer)
                    'M' char (77) or 'm' char (109) - set compression mode
                    If byteVal = &H4D Or byteVal = &H6D Then
                        If valNum = 0 Then
                            compMode = noCompress
                        ElseIf valNum = 2 Then
                            compMode = tiffCompress
                        Else
                            mvarerrNum = compModeNotSupp
                            state = rasterDone
                        End If
                    
                        'if was 'm' char (109), get next # for #m#W command
                        If byteVal = &H6D Then
                            bufPointer = bufPointer + 1
                            valNum = getSize(bufPointer)   'get # if exists
                        End If
                    End If
                    
                    'W' char (87) - raster data row follows
                    If state <> rasterDone Then
                        If mvarPCLData(bufPointer) = &H57 Then
                            mvarisRasterData = True
                            state = findInRasterEscape
                            bufPointer = bufPointer + 1
                            buildRaster bufPointer, rasterPointer, valNum, compMode
                        End If
                    End If
            
'                     'if vertical offset 'Y' (89), fill # rows of rasterData with 0's
'                    If state <> rasterDone Then
'                        If mvarPCLData(bufPointer) = &H59 Then
'                            mvarisRasterData = True
'                            state = findInRasterEscape
'                            yOffsetBuildRaster rasterPointer, valNum
'                        End If
'                    End If
'
                'r' char (114) - start of end of raster data command
                ElseIf mvarPCLData(bufPointer) = &H72 Then
                    state = findEscape
                    bufPointer = bufPointer + 1
                    
                    valNum = getSize(bufPointer)    'get # if exists
                    byteVal = mvarPCLData(bufPointer)
    
                    'look for 'C' (66) or 'B' (67) - end of raster data
                    If byteVal = &H42 Or byteVal = &H43 Then
                        state = rasterDone
                    End If
                    
                    'if 'S' (83) or 'T' (84) or 'A' (65) or 'F' (70) - ignore
                    If byteVal = &H41 Or byteVal = &H54 _
                            Or byteVal = &H53 Or byteVal = &H46 Then
                        bufPointer = bufPointer + 1
                        state = findInRasterEscape  'look for other raster escapes
                    End If
                
                't' char (116) - ignore if t#R - graphics resolution set
                ElseIf mvarPCLData(bufPointer) = &H74 Then
                    bufPointer = bufPointer + 1
                    valNum = getSize(bufPointer)   'get # if exists
                    'if 'R' (82) - ignore
                    If byteVal = &H52 Then
                        bufPointer = bufPointer + 1
                        state = findInRasterEscape  'look for other raster escapes
                    End If
                Else    'some other escape sequence causes end of raster
                    state = rasterDone
                End If

                                
            Case Else
                state = rasterDone 'done! if end up here
        End Select
    Loop
    
    rasterPointer = rasterPointer - 1 'back up to previous value
    
    If mvarerrNum = noError Then
        ReDim Preserve mvarrasterData(rasterPointer)
    End If
    
    Exit Sub

    
pcltorastererr:
    MsgBox "PCL to Raster " & vbCrLf & "Error: " & Err.Description, vbCritical

End Sub
    
    
Public Sub fillPictureBox()
Attribute fillPictureBox.VB_Description = "Fills Picture Box with raster image data."
    'uses raster byte array, raster width and raster height
    'information to populate PictureBox.
    'Uses width and height to size PictureBox by sizing to
    'the correct aspect ratio and then using actual width and height
    'dimensions or sizing to maximum space available if data is larger
    'than PictureBox.
    'Next, routine walks through rows of bytes bit by bit,
    'writes (PSet) out a ForeColor (black) dot of lineWidth 1
    'when bit = 1, or moves to next bit
    
    Dim XPos As Long
    Dim XPos2 As Long
    Dim YPos As Long
    Dim row As Long
    Dim col As Long
    Dim colWid As Long
    Dim rastByte As Long
    Dim pixl As Integer
    Dim pixVal As Integer
    Dim pixCol As Long
    Dim pwr2 As Integer
    Dim pixlWidth1 As Long
    Dim pixlHeight1 As Long
    Dim pixlWidth2 As Long
    Dim pixlHeight2 As Long
    Dim oldSM As Integer
    
    On Error GoTo fillpictureboxerr
    
    With mvarPictureBox
        .AutoRedraw = True
        .BackColor = def_mvarBackcolor
        .ForeColor = vbBlack   ' Set foreground to black
        .Cls   ' Clear form
        colWid = mvarrasterWidth * 8 'pixels from bytes
        pixlWidth1 = .ScaleX(.Width, .ScaleMode, 3) 'scale width to pixels
                                        'regardless of mvarPictureBox.ScaleMode
        pixlHeight1 = .ScaleY(.Height, .ScaleMode, 3) 'scale height to pixels
                                        'regardless of mvarPictureBox.ScaleMode

        'checks to see if PictureBox should be scaled down to fit
        If mvarisPBoxScaled Then

            'maximize current area per portrait or landscape emphasis of data
            'scale down height (if necessary)
            If (colWid / pixlWidth1) > (mvarrasterHeight / pixlHeight1) Then

                If colWid <= pixlWidth1 Then   'don't scale down

                    pixlWidth2 = colWid
                    pixlHeight2 = mvarrasterHeight

                Else    'leave width alone and scale down height
                    pixlHeight2 = Round(pixlWidth1 * (mvarrasterHeight / colWid))
                    pixlWidth2 = pixlWidth1    'sets number of full scale width units
                End If

            'else scale down width (if necessary)
            Else

                If mvarrasterHeight <= pixlHeight1 Then  'don't scale down
                    pixlHeight2 = mvarrasterHeight
                    pixlWidth2 = colWid
                Else    'leave height alone and scale down width
                    pixlWidth2 = Round(pixlHeight1 * (colWid / mvarrasterHeight))
                    pixlHeight2 = pixlHeight1
                End If
            End If

        'PictureBox allowed to be larger than display size shown
        Else
            pixlWidth2 = colWid
            pixlHeight2 = mvarrasterHeight
        End If
        
        ' .width and .height reset per correct .ScaleMode
        .Width = Round(.Width * CDbl(pixlWidth2 / pixlWidth1))
        .Height = Round(.Height * CDbl(pixlHeight2 / pixlHeight1))
        
        'rescale PictureBox for pixel counts, capture old scalemode first
        oldSM = .ScaleMode
        .ScaleWidth = colWid
        .ScaleHeight = mvarrasterHeight
        
        'process bits and draw dots or lines if value = 1
        For row = 0 To mvarrasterHeight - 1
            For col = 0 To mvarrasterWidth - 1
                rastByte = (row * mvarrasterWidth) + col
                If rastByte <= UBound(mvarrasterData) Then
                    If mvarrasterData(rastByte) <> 0 Then
                        If mvarrasterData(rastByte) = &HFF Then 'draw 8 pixel line
                            mvarPictureBox.Line (col * 8, row)-(col * 8 + 7, row)
                        Else    'bit by bit
                            For pixl = 7 To 0 Step -1
                                pwr2 = 2 ^ pixl
                                pixVal = (mvarrasterData(rastByte) And pwr2) / pwr2
                                pixCol = col * 8 + 7 - pixl
                                If pixVal = 1 Then
                                    XPos = pixCol
                                    YPos = row
                                    mvarPictureBox.PSet (XPos, YPos)
                                End If
                            Next pixl
                        End If
                    End If
                End If
            Next col
           DoEvents   ' Yield to other
        Next row
            
        'reset scalemode in case re-plot
        .ScaleMode = oldSM
    
    End With
    
    Exit Sub

fillpictureboxerr:
    MsgBox "Fill PictureBox " & vbCrLf & "Error: " & Err.Description, vbCritical

End Sub


Public Property Let rasterData(ByVal vData As Variant)
Attribute rasterData.VB_Description = "Raster image byte array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.rasterData = 5
    mvarrasterData = vData
End Property


Public Property Set rasterData(ByVal vData As Variant)
'used when assigning an Object to the property, on the left side of a Set statement.
'Syntax: Set x.rasterData = Form1
    Set mvarrasterData = vData
End Property


Public Property Get rasterData() As Variant
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.rasterData
    If IsObject(mvarrasterData) Then
        Set rasterData = mvarrasterData
    Else
        rasterData = mvarrasterData
    End If
End Property


Public Property Let pclData(ByVal vData As Variant)
Attribute pclData.VB_Description = "PCL byte array from file or instrument."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.PCLData = 5
    mvarPCLData = vData
End Property


Public Property Set pclData(ByVal vData As Variant)
'used when assigning an Object to the property, on the left side of a Set statement.
'Syntax: Set x.PCLData = Form1
    Set mvarPCLData = vData
End Property


Public Property Get pclData() As Variant
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.PCLData
    If IsObject(mvarPCLData) Then
        Set pclData = mvarPCLData
    Else
        pclData = mvarPCLData
    End If
End Property


Public Property Set PictureBox(ByVal vData As Object)
Attribute PictureBox.VB_Description = "Sets or returns the picture box."
'used when assigning an Object to the property, on the left side of a Set statement.
'Syntax: Set x.PictureBox = Form1
    Set mvarPictureBox = vData

End Property


Public Property Get PictureBox() As Object
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.PictureBox
    Set PictureBox = mvarPictureBox
End Property


Public Property Let isRasterData(ByVal vData As Boolean)
Attribute isRasterData.VB_Description = "If true, a raster image is contained in the PCL byte array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.isRasterData = 5
    mvarisRasterData = vData
End Property


Public Property Get isRasterData() As Boolean
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.isRasterData
    isRasterData = mvarisRasterData
End Property


Public Property Let isClipWidth(ByVal vData As Boolean)
Attribute isClipWidth.VB_Description = "If true, clipping width information is contained in PCL byte array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.isClipWidth = 5
    mvarisClipWidth = vData
End Property


Public Property Get isClipWidth() As Boolean
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.isClipWidth
    isClipWidth = mvarisClipWidth
End Property


Public Property Let isClipHeight(ByVal vData As Boolean)
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.isClipHeight = 5
    mvarisClipHeight = vData
End Property


Public Property Get isClipHeight() As Boolean
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.isClipHeight
    isClipHeight = mvarisClipHeight
End Property


Public Property Let isPBoxScaled(ByVal vData As Boolean)
Attribute isPBoxScaled.VB_Description = "If true, PictureBox is scaled down to fit parent window if bitmap is larger than PictureBox."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.isPBoxScaled = True
    mvarisPBoxScaled = vData
End Property


Public Property Get isPBoxScaled() As Boolean
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.isPBoxScaled
    isPBoxScaled = mvarisPBoxScaled
End Property


Public Property Let dataLength(ByVal vData As Long)
Attribute dataLength.VB_Description = "Size of  PCL byte array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.dataLength = 5
    mvardataLength = vData
End Property


Public Property Get dataLength() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.dataLength
    dataLength = mvardataLength
End Property


Public Property Let rasterWidth(ByVal vData As Long)
Attribute rasterWidth.VB_Description = "Raster image width."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.rasterWidth = 5
    mvarrasterWidth = vData
End Property


Public Property Get rasterWidth() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.rasterWidth
    rasterWidth = mvarrasterWidth
End Property

Public Property Let rasterHeight(ByVal vData As Long)
Attribute rasterHeight.VB_Description = "Raster image height."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.rasterHeight = 5
    mvarrasterHeight = vData
End Property


Public Property Get rasterHeight() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.rasterHeight
    rasterHeight = mvarrasterHeight
End Property


Public Property Let clipWidth(ByVal vData As Long)
Attribute clipWidth.VB_Description = "Clip image width defined in the raster image byte array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.clipWidth = 5
    mvarclipWidth = vData
End Property


Public Property Get clipWidth() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.clipWidth
    clipWidth = mvarclipWidth
End Property


Public Property Let clipHeight(ByVal vData As Long)
Attribute clipHeight.VB_Description = "Clip image height defined in the raster image byte array."
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.clipHeight = 5
    mvarclipHeight = vData
End Property


Public Property Get clipHeight() As Long
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.clipHeight
    clipHeight = mvarclipHeight
End Property


Public Property Get maxHeight() As Long
Attribute maxHeight.VB_Description = "Maximum height allowed for the raster image."
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.maxHeight
    maxHeight = mvarmaxHeight
End Property


Public Property Let maxHeight(ByVal vData As Long)
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.maxHeight = 5
    mvarmaxHeight = vData
End Property


Public Property Get maxWidth() As Long
Attribute maxWidth.VB_Description = "Maximum width allowed for the raster image."
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.maxWidth
    maxWidth = mvarmaxWidth
End Property


Public Property Let maxWidth(ByVal vData As Long)
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.maxWidth = 5
    mvarmaxWidth = vData
End Property


Public Property Get errNum() As Long
Attribute errNum.VB_Description = "If not equal to 0, contains one of several possible PCL data errors."
'used when retrieving value of a property, on the right side of an assignment.
'Syntax: Debug.Print X.errNum
    errNum = mvarerrNum
End Property


Public Property Let errNum(ByVal vData As Long)
'used when assigning a value to the property, on the left side of an assignment.
'Syntax: X.errNum = 5
    mvarerrNum = vData
End Property


Private Sub Class_Initialize()
    ' Set the default values
    ' to default
    mvarclipHeight = -1
    mvarclipWidth = -1
    mvarisClipHeight = False
    mvarisClipWidth = False
    mvarisRasterData = False
    mvarrasterHeight = 0
    mvarrasterWidth = 0
    
    mvarmaxHeight = 600 * 11    '600 dpi by 11" paper size
    mvarmaxWidth = 600 * 11     '600 dpi by 11" paper size
  
End Sub


Private Function getSize(ByRef bufPointer As Long) As Long
'determines numeric value when value may be present. If not, returnes 0.
    Dim valStr As String
    
    valStr = "0"
    Do While CLng(mvarPCLData(bufPointer)) >= &H30 And _
            CLng(mvarPCLData(bufPointer)) <= &H39
        valStr = valStr & Chr(mvarPCLData(bufPointer)) 'build size
        bufPointer = bufPointer + 1
    Loop
    getSize = CLng(valStr)

End Function


Private Sub buildRaster(ByRef bufPointer As Long, _
                        ByRef rasterPointer As Long, _
                        ByVal valNum As Long, _
                        ByVal compMode As compressMode)
    '
    '
    Dim i As Integer
    Dim j As Integer
    Dim numBytes As Long
    Dim numPatternBytes As Long
    Dim repeatVal As Long
    Dim tempRastArray() As Byte
    Dim PCLbytesProcessed As Integer
    
    On Error GoTo buildrastererr
    
    mvarrasterHeight = mvarrasterHeight + 1     'increment row count
    
    'check height against clip height
    If mvarisClipHeight Then
        If mvarrasterHeight > mvarclipHeight Then   'don't bother processing row
            state = rasterDone
            mvarrasterHeight = mvarrasterHeight - 1
            Exit Sub
        End If
    End If

    'check height against max height value
    If mvarrasterHeight > mvarmaxHeight Then
        mvarerrNum = tooHigh
        state = rasterDone
        Exit Sub
    End If
    
    'check compression mode
    If compMode = noCompress Then   'valNum = number of bytes in row
    
        'check width against max width value in pixels
        If (valNum * 8) > mvarmaxWidth Then
            mvarerrNum = tooWide
            state = rasterDone
            Exit Sub
        End If
        
        'check width against clip width
        If mvarisClipWidth Then
            'clip any additional width data
            If valNum > mvarclipWidth Then valNum = mvarclipWidth
        End If
        
        '(mvarrasterWidth is in bytes, not pixels)
        If valNum > mvarrasterWidth Then mvarrasterWidth = valNum  'set new raster width
           
        'fill raster row
        For i = 0 To valNum - 1
            mvarrasterData(rasterPointer) = mvarPCLData(bufPointer)
            rasterPointer = rasterPointer + 1
            bufPointer = bufPointer + 1
            If (bufPointer) > UBound(mvarPCLData) Then
                state = rasterDone
                Exit Sub
            End If
        Next i

    Else    'compMode = tiff compression
        PCLbytesProcessed = 0
        numPatternBytes = 0    'reset for row
        
        'tiff mode: valNum = number of TIFF encoded bytes to follow to make row
        '           mvarPCLData(bufPointer) is control byte:
        '             - positive(1 to 127) = literal bytes
        '                = (number of pattern bytes - 1) following control byte
        '             - negative(-1 to -127)= repeated byte
        '                = (number of times - 1) to use next pattern byte
        '               (represented by two's complement = -(256 - valNum) )
        
'        For i = 0 To valNum - 1   'process valNum bytes til row done
         
         Do While (PCLbytesProcessed < valNum)
            
            If mvarPCLData(bufPointer) = 128 Then    'NOP
                bufPointer = bufPointer + 1
                PCLbytesProcessed = PCLbytesProcessed + 1
                
            ElseIf mvarPCLData(bufPointer) <= 127 Then    'literal byte count
            
                numBytes = mvarPCLData(bufPointer) + 1  'number of bytes to process
                PCLbytesProcessed = PCLbytesProcessed + 1
                bufPointer = bufPointer + 1     'to first pattern byte
            
                'update raster byte count for current row
                numPatternBytes = numPatternBytes + numBytes
                
                'check width against max width value
                If (numPatternBytes * 8) > mvarmaxWidth Then
                    mvarerrNum = tooWide
                    state = rasterDone
                    Exit Sub
                End If
                
                'check width against clip width
                If mvarisClipWidth Then
                    'clip any additional width data
                    If numPatternBytes > mvarclipWidth Then _
                                        numPatternBytes = mvarclipWidth
                End If
                
                '(mvarrasterWidth is in bytes, not pixels)
                If numPatternBytes > mvarrasterWidth Then mvarrasterWidth = numPatternBytes  'set new raster width
        
                'process numBytes times
                For j = 0 To numBytes - 1
                    mvarrasterData(rasterPointer) = mvarPCLData(bufPointer)
                    rasterPointer = rasterPointer + 1
                    bufPointer = bufPointer + 1
                    PCLbytesProcessed = PCLbytesProcessed + 1
                    
                    If (bufPointer) > UBound(mvarPCLData) Then
                        state = rasterDone
                        Exit Sub
                    End If
                    
                Next j
                
            Else   '(mvarPCLData(bufPointer) = -1 to -127) repeat next byte
                   'repeat value is two's complement value of mvarPCLData(bufPointer) + 1
                
                repeatVal = 256 - mvarPCLData(bufPointer) + 1
                
                bufPointer = bufPointer + 1 'go to following repeated pattern byte
                PCLbytesProcessed = PCLbytesProcessed + 1
                
                'update byte count for current row
                numPatternBytes = numPatternBytes + repeatVal
                
                'check width against clip width
                If mvarisClipWidth Then
                    'clip additional data
                    If numPatternBytes > mvarclipWidth Then
                        repeatVal = repeatVal - (numPatternBytes - mvarclipWidth)
                        numPatternBytes = mvarclipWidth
                    End If
                End If
                
                'check width against max width value
                If (numPatternBytes * 8) > mvarmaxWidth Then
                    mvarerrNum = tooWide
                    state = rasterDone
                    Exit Sub
                End If
                
                '(mvarrasterWidth is in bytes, not pixels)
                If numPatternBytes > mvarrasterWidth Then mvarrasterWidth = numPatternBytes  'set new raster width
        
                
                For j = 0 To repeatVal - 1
                    mvarrasterData(rasterPointer) = mvarPCLData(bufPointer)
                    rasterPointer = rasterPointer + 1
                Next j
                
                bufPointer = bufPointer + 1
                PCLbytesProcessed = PCLbytesProcessed + 1
                
            End If

        Loop
        
        End If
    Exit Sub
    
buildrastererr:
    MsgBox "Build Raster " & vbCrLf & "Error: " & Err.Description, vbCritical
    
End Sub


'Private Sub yOffsetBuildRaster(ByRef rasterPointer As Long, _
'                        ByVal numBuildRows As Long)
'    Dim i As Integer
'    Dim j As Integer
'
'    On Error GoTo yoffsetbuildrastererr
'
'    'fill numBuildRows raster rows
'    For j = 0 To numBuildRows - 1
'
'        For i = 0 To mvarrasterWidth - 1
'            mvarrasterData(rasterPointer) = &H0
'            rasterPointer = rasterPointer + 1
'        Next i
'
'        mvarrasterHeight = mvarrasterHeight + 1 'increment and check row height
'
'        If mvarisClipHeight Then
'            If mvarrasterHeight > mvarclipHeight Then   'don't bother processing more rows
'                state = rasterDone
'                mvarrasterHeight = mvarrasterHeight - 1
'                Exit Sub
'            End If
'        End If
'        If mvarrasterHeight > mvarmaxHeight Then
'            mvarerrNum = tooHigh
'            state = rasterDone
'            Exit Sub
'        End If
'
'    Next j
'    Exit Sub
'
'yoffsetbuildrastererr:
'    Debug.Print "stopped here" & Err.Description
'    MsgBox "Y Offset Build Raster " & vbCrLf & "Error: " & Err.Description, vbCritical
'
'End Sub


