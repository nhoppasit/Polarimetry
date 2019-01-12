VERSION 5.00
Begin VB.Form frmGPIB_MeasConfig 
   Caption         =   "GPIB_MeasConfig"
   ClientHeight    =   5280
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6330
   LinkTopic       =   "Form1"
   ScaleHeight     =   5280
   ScaleWidth      =   6330
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtAddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   2760
      TabIndex        =   10
      Text            =   "GPIB::22"
      Top             =   4440
      Width           =   1455
   End
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set IO"
      Height          =   495
      Left            =   4560
      TabIndex        =   9
      Top             =   4440
      Width           =   1455
   End
   Begin VB.TextBox txtReadings 
      Height          =   288
      Left            =   2760
      TabIndex        =   1
      Text            =   "10"
      Top             =   2160
      Width           =   972
   End
   Begin VB.TextBox timeResult 
      Height          =   288
      Left            =   2760
      Locked          =   -1  'True
      TabIndex        =   6
      Top             =   3240
      Width           =   972
   End
   Begin VB.CommandButton cmdMeasureNorm 
      Caption         =   "Multiple-Norm"
      Height          =   495
      Left            =   2760
      TabIndex        =   3
      Top             =   480
      Width           =   1455
   End
   Begin VB.CommandButton cmdMeasureFast 
      Caption         =   "Multiple-Fast"
      Height          =   495
      Left            =   2760
      TabIndex        =   4
      Top             =   1080
      Width           =   1455
   End
   Begin VB.CommandButton cmdMeasure 
      Caption         =   "Single"
      Height          =   495
      Left            =   4560
      TabIndex        =   2
      Top             =   480
      Width           =   1455
   End
   Begin VB.TextBox txtResults 
      Height          =   4572
      Left            =   240
      LinkItem        =   "txtResult"
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   0
      Top             =   480
      Width           =   2295
   End
   Begin VB.Label Label3 
      Caption         =   "Number of readings"
      Height          =   615
      Left            =   2760
      TabIndex        =   8
      Top             =   1680
      Width           =   1335
   End
   Begin VB.Label Label2 
      Caption         =   "Time to receive reading(s) (msec)"
      Height          =   615
      Left            =   2760
      TabIndex        =   7
      Top             =   2640
      Width           =   1335
   End
   Begin VB.Label Label1 
      Caption         =   "Results:"
      Height          =   375
      Left            =   240
      TabIndex        =   5
      Top             =   240
      Width           =   1695
   End
End
Attribute VB_Name = "frmGPIB_MeasConfig"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Declare Function timeGetTime Lib "winmm.dll" () As Long

Public lngStartTime As Long                                'time in msec

''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2002 Agilent Technologies Inc.  All rights reserved.
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

' The following procedures measure one or more dc or ac voltages on the
' 3458A DMM and return the values to the computer. The 3458A DMM uses its
' own non-SCPI commands and does not understand IEEE-488.2 commands or
' return IEEE-488.2 compliant data. Therefore, when the instrument is
' not internally storing and returning values using ASCII (15 bytes/reading),
' the 3494A control "Enter" command is not used. The IO commmand,
' "readBytes" is used instead to receive instrument data. The following
' data types are read in using the readBytes command:
'   SINT - 2-bytes/reading
'   DINT - 4-bytes/reading
'   SREAL - 4-bytes/reading
'   DREAL - 8-bytes/reading

' The expected measurement values are then "rebuilt" from the byte data.

' Be sure to check and set the IO Address to
' match the instrument.

Dim DMM As VisaComLib.FormattedIO488

Private Sub cmdMeasure_Click()
' The following procedure makes a single ac voltage measurement on the
' 3458A multimeter using OFORMAT ASCII and measures time to obtain value
'
    Dim reply As Double

    timeResult.Text = ""
    txtResults.Text = ""
    timeResult.Refresh
    txtResults.Refresh
    StartTimer

    With DMM

        .WriteString "PRESET NORM"
        'PRESET NORM sets following paramaters
        'ACBAND 20,2E+6 AC bandwidth 20Hz - 2MHz
        'AZERO ON Autozero enabled
        'BEEP ON Beeper enabled
        'DCV AUTO DC voltage measurements, autorange
        'DELAY -1 Default delay
        'DISP ON Display enabled
        'FIXEDZ OFF Disable fixed input resistance
        'FSOURCE ACV Frequency and period source is AC voltage
        'INBUF OFF Disable input buffer
        'LOCK OFF Keyboard enabled
        'MATH OFF Disable real-time math
        'MEM OFF Disable reading memory
        'MFORMAT SREAL Single real reading memory format
        'MMATH OFF Disable post-process math
        'NDIG 6 Display 6.5 digits
        'NPLC 1 1 power line cycle of integration time
        'NRDGS 1,AUTO 1 reading per trigger, auto sample event
        'OCOMP OFF Disable offset compensated ohms
        'OFORMAT ASCII  ASCII output format
        'TARM AUTO Auto trigger arm event
        'TIMER 1 1 second timer interval
        'TRIG SYN Synchronous trigger event
        'All math registers set to 0 except:
        'DEGREE = 20
        'PERC = 1
        'REF = 1
        'RES = 50
        'SCALE = 1

        .WriteString "OFORMAT ASCII"
        'Set to measure AC Volts
        .WriteString "ACV"
        reply = .ReadNumber                                        'can use Enter since return value in ASCII format
    End With

    timeResult.Text = EndTimer                             'reading in hand
    timeResult.Refresh

    txtResults.Text = Str$(Round(reply, 8)) & " volts AC"
End Sub

Private Sub cmdMeasureNorm_Click()
' The following procedure makes a 100 dc voltage measurements on the
' 3458A multimeter using PRESET NORM and OFORMAT DINT
' and measures elapsed time to obtain values
'
    Dim readByte() As Byte
    Dim rdg() As Long
    Dim reading() As Double
    Dim i As Integer
    Dim iscale As String
    Dim numReadings As Integer

    numReadings = txtReadings.Text

    ReDim reading(numReadings)
    ReDim rdg(numReadings)

    timeResult.Text = ""
    txtResults.Text = ""
    timeResult.Refresh
    txtResults.Refresh
    StartTimer

    With DMM
        .WriteString "PRESET NORM"                         'TARM AUTO, TRIG SYN, DCV AUTORANGE
        'DINT good for 5-1/2 or more digits
        .WriteString "OFORMAT DINT"                        'FAST AND ACCURATE - 4-BYTE INTEGERS
        .WriteString "NRDGS 15, SYN"                       '15 READINGS PER TRIGGER, SYN SAMPLE EVENT
        .WriteString "TRIG AUTO"                           'AUTO TRIGGER EVENT

        For i = 1 To numReadings
            readByte = .IO.Read(4)                         'SYN EVENT, 4-bytes per DINT value
            'build 4-byte long values
            If readByte(0) > 127 Then                      'negative
                rdg(i) = CLng(-2147483648#) + (readByte(0) - CLng(128)) * CLng(16777216) _
                        + readByte(1) * CLng(65536) _
                        + readByte(2) * CLng(256) + readByte(3)
            Else                                           'positive
                rdg(i) = readByte(0) * CLng(16777216) + readByte(1) * CLng(65536) _
                        + readByte(2) * CLng(256) + readByte(3)
            End If
        Next i

        'get instrument scale value and multiply
        'with all long values to get instrument measurement
        .WriteString "ISCALE?"                             'scale factor for DINT format
        iscale = .ReadString

        For i = 1 To numReadings
            reading(i) = Round((rdg(i) * Val(iscale)), 6)
        Next i

        timeResult.Text = EndTimer                         'readings in hand
        timeResult.Refresh

        'output to textbox
        For i = 1 To numReadings
            txtResults.SelText = i & ": " & Str$(reading(i)) & " volts DC" & vbCrLf
        Next i
    End With

End Sub

Private Sub cmdMeasureFast_Click()

' The following procedure makes a 100 dc voltage measurements on the
' 3458A multimeter using PRESET FAST and OFORMAT SINT
' and measures elapsed time to obtain values
'
    Dim rdg() As String
    Dim reading() As Single
    Dim i As Integer
    Dim iscale As String
    Dim numReadings As Integer

    numReadings = txtReadings.Text

    ReDim rdg(numReadings)
    ReDim reading(numReadings)

    timeResult.Text = ""
    txtResults.Text = ""
    timeResult.Refresh
    txtResults.Refresh
    StartTimer

    With DMM

        .WriteString "PRESET FAST"                         'DCV, 10V RANGE, TARM SYN, TRIG AUTO
        'PRESET FAST sets additional following paramaters to PRESET NORM
        'DCV 10  disables autorange.
        'AZERO OFF no zero measurement is made
        'DISP OFF no time required for multimeter to update display
        'MATH OFF no real-time math operations
        'MFORMAT DINT fast way to transfer readings to reading memory
        'OFORMAT DINT fast way to transfer readings to output buffer

        .WriteString "APER 1.4E-6"                         'LONGEST INTEGRATION TIME POSSIBLE FOR
        '>100K READINGS PER SECOND
        'SINT good for 3-1/2 or 4-1/2 digits output
        .WriteString "MFORMAT SINT"                        'fastest to reading memory - 2 bytes/reading
        .WriteString "OFORMAT ASCII"                       'output in ASCII to computer
        'numReadings READINGS/TRIGGER, AUTO SAMPLE EVENT"
        .WriteString "NRDGS " & numReadings & ", AUTO"
        .WriteString "MEM FIFO"                            'first in, first out memory access
        .WriteString "TARM SGL"                            'trigger readings
        .WriteString "MCOUNT?"
        i = .ReadNumber
        .WriteString "RMEM 1," & i                         'recall readings from memory

        For i = 1 To numReadings
            rdg(i) = .ReadString

        Next i

    End With

    For i = 1 To numReadings
        'round to 4 digits
        reading(i) = Round(Val(rdg(i)), 4)
    Next i

    timeResult.Text = EndTimer                             'readings in hand
    timeResult.Refresh

    'output to textbox
    For i = 1 To numReadings
        txtResults.SelText = i & ": " & Str$(reading(i)) & " volts DC" & vbCrLf
    Next i

End Sub

Public Sub StartTimer()
    lngStartTime = timeGetTime()
End Sub

Public Function EndTimer() As Double
' time is in msec
    EndTimer = timeGetTime() - lngStartTime
End Function

Private Sub Form_Load()
    cmdSetIO_Click
End Sub

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim ioAddress As String
    On Error GoTo ioError

    ioAddress = txtAddress.Text
    ioAddress = InputBox("Enter the IO address of the DMM", "Set IO address", ioAddress)

    txtAddress.Text = ioAddress
    Set mgr = New VisaComLib.ResourceManager
    Set DMM = New VisaComLib.FormattedIO488
    Set DMM.IO = mgr.Open(ioAddress)
    DMM.WriteString "END ALWAYS"                          'set EOI True - necessary for 3458A
    DMM.WriteString "PRESET NORM"

    Exit Sub
    
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description
End Sub


