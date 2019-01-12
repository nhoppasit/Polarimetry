Attribute VB_Name = "DAQ_34970"
Option Explicit
' This code will scan the 34970A and read the result
'
 Sub main_34970A(DAQ_34970 As VisaComLib.FormattedIO488)
    Dim numberChannels As Integer
    Dim numberSweeps As Integer
    Dim Sweep As Integer
    Dim Readings() As Double
    Dim Units() As String
    Dim ChanNumb() As Integer
    Dim Time() As Date

    With DAQ_34970
        ' Set instrument to power on state
        .WriteString "*RST"
    End With
    Configure_34970A DAQ_34970
    SetScan_34970A DAQ_34970, numberSweeps, numberChannels
    ' Size arrays depending on channel and sweep count
    ReDim Readings(numberSweeps, numberChannels)
    ReDim Units(numberSweeps, numberChannels)
    ReDim ChanNumb(numberSweeps, numberChannels)
    ReDim Time(numberSweeps, numberChannels)
    ' Get data one sweep at a time
    For Sweep = 1 To numberSweeps
        ReadData_34970A DAQ_34970, Sweep, numberChannels, Readings(), _
                Units(), ChanNumb(), Time()

    frmScanner.WriteDataToForm Readings, Units, ChanNumb, Time, Sweep, numberChannels

    Next Sweep
End Sub
 Sub Configure_34970A(Instrument As VisaComLib.FormattedIO488)
    With Instrument
        ' Configure all channels in channel list
        ' Channels 101   Volts DC    Auto    4 ½ digits
        .WriteString "Conf:volt:DC Auto,(@101);:Volt:DC:NPLC 0.02,(@101)"
        ' Channels 107   Resistance  Auto    4 ½ digits
        .WriteString "Conf:Res Auto,(@107);:Res:NPLC 0.02,(@107)"
        ' Channels 110   Volts DC    Auto    4 ½ digits
        .WriteString "Conf:volt:DC Auto,(@110);:Volt:DC:NPLC 0.02,(@110)"
        ' Channels 112   Temperature TC  J type  °C
        .WriteString "Conf:Temp TC,J,(@112);:Unit:Temp C,(@112)"
    End With
End Sub
 Sub SetScan_34970A(Instrument As VisaComLib.FormattedIO488, numberSweeps As Integer, numberChannels As Integer)
    Dim replyString As String

    numberSweeps = 5
    With Instrument
        ' Include the Measurement units with reading
        .WriteString "Format:Reading:Unit On"
        ' Include the channel with reading
        .WriteString "Format:Reading:Channel On"
        ' Include the time with reading
        .WriteString "Format:Reading:Time On"
        ' Set up the scan List
        .WriteString "Route:Scan (@101,107,110,112)"
        ' Configure the trigger source
        .WriteString "Trigger:Source Timer"
        ' Set the time between scans
        .WriteString "Trigger:Timer 1"
        ' Set the number of scans
        .WriteString "Trigger:Count " & Str$(numberSweeps)
        ' Read the number of channels on scan list
        .WriteString "Route:Scan:Size?"
        replyString = .ReadString
        numberChannels = Val(replyString)
        ' Arm the trigger with initiate
        .WriteString "Initiate"
    End With
End Sub
 Sub ReadData_34970A(Instrument As VisaComLib.FormattedIO488, Sweeps As Integer, numberChannels As Integer, _
         Readings() As Double, _
         Units() As String, ChanNumb() As Integer, Time() As Date)
    Dim Points As Integer
    Dim channel As Integer
    Dim replyString As String

    With Instrument
        ' Get the data one channel at a time
        For channel = 1 To numberChannels
            ' Wait for reading in instrument memory
            Do
                .WriteString "Data:Points?"
                replyString = .ReadString
                Points = CInt(replyString)
            Loop Until Points >= 1
            ' Get one channel of data from instrument
            .WriteString "Data:Remove? 1"
            replyString = .ReadString
            ' Remove the reading data from string
            Readings(Sweeps, channel) = CDbl(Mid$(replyString, 1, 15))
            ' Remove the units data from string
            Units(Sweeps, channel) = Mid$(replyString, 17, 3)
            ' Remove the time data from string
            Time(Sweeps, channel) = Val(Mid$(replyString, 21, 12)) / 86400
            ' Remove the channel number from string
            ChanNumb(Sweeps, channel) = Val(Mid$(replyString, 34, 3))
        Next channel
    End With
End Sub


