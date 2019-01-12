Attribute VB_Name = "modInstrumentData"
 Option Explicit
'*************************************************
'Date: 03/18/00
'   This module calls the required code depending on
'   instrument. Each instrument is treated differently
'   by data size, timeout required and the way it is
'   called.
' Public routines.  All routines are called by the
'       public function that returns the raw digital
'       data.
'*************************************************

' Sets the object for GPIB IO
Dim Device As VisaComLib.FormattedIO488
'

Public Sub GetPlotterData(modelNumber As String, data() As Byte)
    ' selects the instrument and returns the array of bytes
    ' of the HPGL2 data returned from instrument.
    
    Dim reply As String
    
    Select Case UCase(modelNumber)
        Case "8714ET"
            Get8714ETData Device, data()
        Case "8720ES", "8753E", "8510C"
            Get8720ESData Device, data()
            data(1) = Asc(";")
        Case Else
            MsgBox " Error; no such model number supported"
        
    End Select
    


End Sub




Private Sub Get8714ETData(Device As VisaComLib.FormattedIO488, data() As Byte)
    Dim msg As String


    On Error GoTo scopeError


    Device.IO.Timeout = 30000
    Device.WriteString "mmem:tran? 'data:screen.hgl'"
    data() = Device.IO.Read(150000)

    Exit Sub

scopeError:
    MsgBox "Get 8714ET data " & vbCrLf & "Error: " & Err.Description, vbCritical
End Sub

Private Sub Get8720ESData(NA As VisaComLib.FormattedIO488, data() As Byte)
    Dim msg As String

    On Error GoTo scopeError

    NA.IO.Timeout = 15000
    NA.WriteString "OUTPPLOT"
    data() = NA.IO.Read(150000)

    Exit Sub

scopeError:
    MsgBox "Get data " & vbCrLf & "Error: " & Err.Description, vbCritical
End Sub

Public Sub SetIO(txtBox As TextBox)
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim ioAddress As String
    On Error GoTo ioError

    ioAddress = txtBox.Text
    ioAddress = InputBox("Enter the IO address of the Device", "Set IO address", ioAddress)

    If Len(ioAddress) > 6 Then
        txtBox.Text = ioAddress
        Set mgr = New VisaComLib.ResourceManager
        Set Device = New VisaComLib.FormattedIO488
        Set Device.IO = mgr.Open(ioAddress)
    End If

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


