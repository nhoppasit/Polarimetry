VERSION 5.00
Object = "{65E121D4-0C60-11D2-A9FC-0000F8754DA1}#2.0#0"; "mschrt20.ocx"
Begin VB.Form frmWaveform 
   Caption         =   "Form1"
   ClientHeight    =   4800
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   6255
   LinkTopic       =   "Form1"
   ScaleHeight     =   4800
   ScaleWidth      =   6255
   StartUpPosition =   3  'Windows Default
   Begin MSChart20Lib.MSChart MSChart1 
      Height          =   3495
      Left            =   360
      OleObjectBlob   =   "frmWaveformAS.frx":0000
      TabIndex        =   5
      Top             =   1080
      Width           =   5535
   End
   Begin VB.TextBox txtPoint 
      Height          =   288
      Left            =   2160
      TabIndex        =   1
      Top             =   360
      Width           =   2172
   End
   Begin VB.TextBox txtAddress 
      Height          =   288
      Left            =   240
      TabIndex        =   0
      Text            =   "GPIB0::7"
      Top             =   360
      Width           =   1692
   End
   Begin VB.CommandButton cmdGetData 
      Caption         =   "Get Waveform"
      Height          =   372
      Left            =   4680
      TabIndex        =   2
      Top             =   240
      Width           =   1212
   End
   Begin VB.Label Label2 
      Caption         =   "Selected Point"
      Height          =   252
      Left            =   2160
      TabIndex        =   3
      Top             =   120
      Width           =   1692
   End
   Begin VB.Label Label1 
      Caption         =   "Address"
      Height          =   252
      Left            =   240
      TabIndex        =   4
      Top             =   120
      Width           =   1092
   End
End
Attribute VB_Name = "frmWaveform"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 1999-2002 Agilent Technologies Inc.  All rights reserved.
''
'' You have a royalty-free right to use, modify, reproduce and distribute
'' the Sample Application Files (and/or any modified version) in any way
'' you find useful, provided that you agree that Agilent has no
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
'
Dim scope As VisaComLib.FormattedIO488
Dim io_mgr As VisaComLib.ResourceManager

Private Sub cmdGetData_Click()
    Dim ydata() As Double
    Dim timedata() As Double
    Dim Channel As Long
    Dim Points As Long
    Dim data() As Variant
    Dim i As Long
    Dim addr As String

    cmdGetData.Enabled = False

    ' initiate the scope connection with the address
    ' you can speed things up by doing this only once on load
    addr = txtAddress.Text
    Set io_mgr = New VisaComLib.ResourceManager
    Set scope = New VisaComLib.FormattedIO488
    Set scope.IO = io_mgr.Open(addr)

    ' valid number of points = 100, 250, 500,1000, 2000, 4000
    ' depending on model number
    Points = 250
    Channel = 1    ' be sure channel is on

    GetWaveformData Channel, Points, timedata, ydata

    ' Create a 2 dimensional array of variants for the graph
    ReDim data(0 To UBound(ydata) + 1, 1 To 2)
    ' legend data goes into row 0
    data(0, 1) = "time"
    data(0, 2) = "ch" & Str$(Channel)

    ' put scope data into array of variants for graph
    For i = 0 To UBound(ydata)
        data(i + 1, 1) = timedata(i)
        data(i + 1, 2) = ydata(i)
    Next i

    ' Send the variant data to chart but don't display the first
    ' column, the first column is time data
    With MSChart1
        .Plot.Axis(VtChAxisIdX).CategoryScale.Auto = False
        .Plot.Axis(VtChAxisIdX).CategoryScale.DivisionsPerTick = Points / 10
        .Plot.Axis(VtChAxisIdX).CategoryScale.DivisionsPerLabel = Points + 1
        .Plot.SeriesCollection(1).Position.Excluded = True
        .chartType = VtChChartType2dLine
        .ChartData = data
    End With

    cmdGetData.Enabled = True

    Exit Sub

getWaveformError:
    MsgBox "cmdGetData_click, error:" & vbCrLf & Err.Number & Err.Description
    cmdGetData.Enabled = True
End Sub

Private Sub MSChart1_PointSelected(Series As Integer, DataPoint As Integer, _
                            MouseFlags As Integer, Cancel As Integer)
   ' Put the point data into the text box
   
   Dim time As Double
   Dim strtime As String
   Dim Volts As Double
   Dim strVolts As String
   
   On Error Resume Next
   
   With MSChart1
      .Row = DataPoint
      .Column = Series
      Volts = .data
      .Column = 1
      time = .data
   End With
   ' Debug.Print " Series = "; Series; " Point = "; DataPoint; "  time/volts = "; time; Volts
   strtime = Format$(time * 1000, "general number") & "msec, "
   strVolts = Format$(Volts, "General Number") & "Volts"
   txtPoint.Text = strtime & strVolts
   
End Sub

Sub GetWaveformData(Channel As Long, _
                  Points As Long, time, data)
' Gets the waveform data from the HP54600 series
' scope given the channel and number of points
' This handles only one channel
   
   Dim Preamble()
   Dim ydata As Variant
   Dim address As String
   Dim strChannel As String
   Dim i As Long
   
   On Error GoTo GraphError
   
   ' set scope for number of points and then
   ' get the preamble and set to return waveform
   ' as byte data
   
   strChannel = "Channel" & Format$(Channel)
   With scope
      .WriteString "Waveform:points " & Points
      .WriteString "Waveform:Source " & strChannel
      .WriteString "Waveform:Preamble?"
      Preamble() = scope.ReadList
      .WriteString "Waveform:Format byte"
      .WriteString "Waveform:data?"
   End With
      ' this gets the IEEE block data and puts it into array
      ydata = scope.ReadIEEEBlock(BinaryType_UI1)

   ' dimension data for the needed size
   ReDim data(Points - 1)
   ReDim time(Points - 1)
   
   ' scale the data to volts and seconds
   For i = 0 To UBound(ydata)
      data(i) = (ydata(i) - Preamble(9)) * _
                     Preamble(7) + Preamble(8)
      time(i) = ((i - Preamble(6)) * _
                     Preamble(4)) + Preamble(5)
   Next i ' 0  to Ubound of ydata
   
   Exit Sub
   
GraphError:
   Debug.Print "Inside GetWaveformData, Error "; Err.Number; Err.Description
End Sub

