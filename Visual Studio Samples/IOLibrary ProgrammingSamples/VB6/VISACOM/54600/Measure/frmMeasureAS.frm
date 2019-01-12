VERSION 5.00
Begin VB.Form frmMeasure 
   Caption         =   "Measure Functions"
   ClientHeight    =   2625
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   6255
   LinkTopic       =   "Form1"
   ScaleHeight     =   2625
   ScaleWidth      =   6255
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtAddress 
      Height          =   375
      Left            =   240
      TabIndex        =   0
      Text            =   "GPIB::7"
      Top             =   600
      Width           =   1815
   End
   Begin VB.ComboBox cboChannel 
      Height          =   315
      Left            =   2280
      TabIndex        =   1
      Text            =   "Combo2"
      Top             =   600
      Width           =   975
   End
   Begin VB.ComboBox cboMeasureDropDown 
      Height          =   315
      Left            =   3720
      TabIndex        =   2
      Text            =   "Combo1"
      Top             =   600
      Width           =   2415
   End
   Begin VB.CommandButton cmdMeasure 
      Caption         =   "Get Measure"
      Height          =   495
      Left            =   4440
      TabIndex        =   3
      Top             =   1920
      Width           =   1695
   End
   Begin VB.Label Label5 
      Caption         =   "Reading"
      Height          =   255
      Left            =   1800
      TabIndex        =   8
      Top             =   1560
      Width           =   1455
   End
   Begin VB.Label Label4 
      Caption         =   "Address"
      Height          =   375
      Left            =   240
      TabIndex        =   7
      Top             =   240
      Width           =   1455
   End
   Begin VB.Label lblReading 
      BorderStyle     =   1  'Fixed Single
      Height          =   375
      Left            =   1800
      TabIndex        =   6
      Top             =   1920
      Width           =   1575
   End
   Begin VB.Label Label2 
      Caption         =   "Measure Function"
      Height          =   255
      Left            =   3720
      TabIndex        =   5
      Top             =   240
      Width           =   2175
   End
   Begin VB.Label Label1 
      Caption         =   "Channel"
      Height          =   375
      Left            =   2280
      TabIndex        =   4
      Top             =   240
      Width           =   1335
   End
End
Attribute VB_Name = "frmMeasure"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 1998-2002 Agilent Technologies Inc. All rights reserved.
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
Dim scope As VisaComLib.FormattedIO488
Dim io_mgr As VisaComLib.ResourceManager

Private Sub InitializeDropDown()
    Dim i As Long
    Dim numberChannels As Long
    
    ' Add channels to the list
    numberChannels = 2
    cboChannel.Clear
    For i = 1 To numberChannels
        cboChannel.AddItem Str$(i)
    Next i
    cboChannel.ListIndex = 0

    ' initialize the measurement combo box
    ' Put the description in drop down, and the
    ' SCPI command in the ItemData property
    With cboMeasureDropDown
        '.AddItem "Measurement"
        .AddItem "Voltage average"      ' index 0
        .AddItem "Voltage rms"          '1
        .AddItem "Voltage Peak-Peak"    '2
        .AddItem "Voltage max"          '3
        .AddItem "Voltage min"          '4
        .AddItem "Rise time"        '5
        .AddItem "Fall time"        '6
        .AddItem "Pos Pulse Width"  '7
        .AddItem "Neg Puse Width"   '8
        .AddItem "Period"           '9
        .AddItem "Frequency"        '10
        .AddItem "Duty Cycle"       '11
        .ListIndex = 1
    End With

End Sub

Private Sub cmdMeasure_Click()
' Reads the address, scope channel, and function
    Dim index As Long
    Dim channel As Long
    Dim channelOn As Boolean
    Dim units As String         ' the units of the reading returned]
    Dim reading As Double       ' the reading from the scope
    Dim strChannel As String
    Dim result As String
    
    On Error GoTo MeasureError
    
    cmdMeasure.Enabled = False
    
    ' do this on initialize to speed up the connection
    ' and not repeat it every time this routine is called
    Set io_mgr = New VisaComLib.ResourceManager
    Set scope = New VisaComLib.FormattedIO488
    Set scope.IO = io_mgr.Open(Me.txtAddress)
  
    ' get the function and channel selection
    index = cboMeasureDropDown.ListIndex
    
    'get channel selection
    channel = cboChannel.ListIndex + 1
    
    If channel = 1 Then
        strChannel = "Channel1"
    ElseIf channel = 2 Then
        strChannel = "channel2"
    End If

    
    ' Check to see if the channel is on
    scope.WriteString ":Status? " & strChannel
    result = scope.ReadString
    
    If InStr(1, result, "ON", vbTextCompare) Or _
        InStr(1, result, "1", vbTextCompare) Then
        channelOn = True
    Else
        MsgBox "Selected channel is off"
        Exit Sub
    End If
    
    ' If the channel is on get the reading
    If channelOn Then ' call the scope for the measurement
        MeasureFunction scope, index, strChannel, reading, units
        lblReading.Caption = Format$(reading) & units
    Else
        MsgBox "This channel is not on or not valid"
    End If
    
    cmdMeasure.Enabled = True
    
    Exit Sub
MeasureError:
   MsgBox Err.Description
   cmdMeasure.Enabled = True
         
End Sub

Private Sub Form_Load()
    InitializeDropDown
End Sub

Sub MeasureFunction(scope As VisaComLib.FormattedIO488, index As Long, strChannel As String, _
                    reading As Double, Optional unitsText As String)
    ' Get the selected measurement defined by drop down index to make the measurement
    '
    ' Input;
    '     Index to indicate which single measurement function
    '     channel; select the scope channel (can only do one at a time)
    ' Output;
    '     reading, a double of the value from scope in basic units
    '     unitsText; The units of the reading (Hz, V, s, %)
    '
    Dim msg As String
    Dim MeasAll(11, 11) As Variant
    Dim i As Long
    Dim result As Double
    Dim cmdArray As Variant
    Dim unitsArray() As String
    
    On Error GoTo MeasureError
    
    ' Create an array of measure SCPI commands
    cmdArray = Array(":Measure:VAverage?", ":Measure:VRMS?", _
                 ":Measure:VPP?", ":Measure:VMax?", _
                 ":Measure:VMin?", ":Measure:RiseTime?", _
                 ":Measure:FallTime?", ":Measure:PWidth?", _
                 ":Measure:NWidth?", ":Measure:Period?", _
                 ":Measure:Frequency?", ":Measure:DutyCycle?")
                 
    unitsArray = Split("V V V V V s s s s s Hz %")
    
    scope.WriteString cmdArray(index)
    reading = scope.ReadNumber
    unitsText = unitsArray(index)

    
    ' change the units to 'error' if the reading is out of range (overload)
    If reading > 1E+37 Then
        unitsText = "error"
    End If
        
Exit Sub

MeasureError:
Debug.Print "measure Error "; Err.Number; Err.Description

End Sub


