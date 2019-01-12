VERSION 5.00
Begin VB.Form frmMeasConfig 
   Caption         =   "Form1"
   ClientHeight    =   2655
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4785
   LinkTopic       =   "Form1"
   ScaleHeight     =   2655
   ScaleWidth      =   4785
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdSetIO 
      Caption         =   "Set I/O"
      Height          =   495
      Left            =   3240
      TabIndex        =   4
      Top             =   240
      Width           =   1455
   End
   Begin VB.CommandButton cmdConfigure 
      Caption         =   "using Configure"
      Height          =   495
      Left            =   3240
      TabIndex        =   3
      Top             =   1440
      Width           =   1455
   End
   Begin VB.CommandButton cmdMeasure 
      Caption         =   "using Measure?"
      Height          =   495
      Left            =   3240
      TabIndex        =   2
      Top             =   840
      Width           =   1455
   End
   Begin VB.TextBox txtResult 
      Height          =   1695
      Left            =   240
      LinkItem        =   "txtResult"
      MultiLine       =   -1  'True
      TabIndex        =   0
      Top             =   600
      Width           =   2295
   End
   Begin VB.Label Label1 
      Caption         =   "Result"
      Height          =   375
      Left            =   240
      TabIndex        =   1
      Top             =   240
      Width           =   1695
   End
End
Attribute VB_Name = "frmMeasConfig"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 1999, 2000 Agilent Technologies Inc.  All rights reserved.
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
Dim DMM As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::22"
Dim m_ioAddress As String

Private Sub cmdMeasure_Click()
    ' The following example uses Measure? command to make a single
    ' ac current measurement. This is the easiest way to program the
    ' multimeter for measurements. However, MEASure? does not offer
    ' much flexibility.
    '
    ' Be sure to check set the instrument address on the control
    ' to match the instrument
    '
    Dim reply As Double
    
    ' As part of the example, we check to see if this is RS232,
    ' If it is RS232 we first set the instrument to remote
    '
    ' We need to do a Connect because we are using the io layer
    If InStr(1, DMM.IO.ResourceName, "ASRL") Then ' the io is RS232
        ' send the remote for RS232
        DMM.WriteString "Syst:Rem"
        ' for fast PC's > 300MHz add a delay of >= 50 ms
        delay 50
    End If
    
    ' Clear the text box
    txtResult.Text = ""
    txtResult.Refresh
    
    ' EXAMPLE for using the Measure command
    With DMM
        .WriteString "*RST"
        .WriteString "*CLS"
        ' Set meter to 1 amp ac range
        .WriteString "Measure:Current:AC? 1A,0.001MA"
        ' for fast PC's add a delay of => 50 ms
        delay 200
        reply = .ReadNumber
    End With
        
    txtResult.Text = reply & " amps AC"
    
End Sub
Private Sub cmdConfigure_Click()
    ' The following example uses CONFigure with the dBm math operation.
    ' The CONFigure command gives you a little more programming flexibility
    ' than the MEASure? command. This allows you to 'incrementally'
    ' change the multimeter's configuration.
    '
    ' Be sure to check set the instrument address on the control
    ' to match the instrument address setting
    '
    Dim Readings() As Variant
    Dim i As Long
    Dim status As Long
    
    On Error GoTo ConfigError
    
    ' EXAMPLE RS232
    ' As part of the example, we check to see if this is RS232,
    ' If it is RS232 we first set the instrument to remote
    '
    ' We need to do a Connect once because we are using the io layer
    If InStr(1, DMM.IO.ResourceName, "ASRL", vbTextCompare) > 0 Then ' the io is RS232
        ' send the remote for RS232
        DMM.WriteString "Syst:Rem"
    End If
    
    ' clear the text box so we can tell when new data arrives
    txtResult.Text = ""
    txtResult.Refresh
    
    ' EXAMPLE for using the CONFigure command
    With DMM
        .IO.Timeout = 10000                    ' Set timeout to 10 sec to allow time to take reading
        .WriteString "*RST"                     ' Reset the dmm
        .WriteString "*CLS"                     ' Clear dmm status registers
        .WriteString "CALC:DBM:REF 50"          ' set 50 ohm reference for dBm
        ' the CONFigure command sets range and resolution for AC
        ' all other AC function parameters are defaulted but can be
        ' set before a READ?
        .WriteString "Conf:Volt:AC 1, 0.001"     ' set dmm to 1 amp ac range"
        .WriteString "Det:Band 200"             ' Select the 200 Hz (fast) ac filter
        .WriteString "Trig:Coun 5"              ' dmm will accept 5 triggers
        .WriteString "Trig:Sour IMM"            ' Trigger source is IMMediate
        .WriteString "Calc:Func DBM"            ' Select dBm function
        .WriteString "Calc:Stat ON"             ' Enable math and request operation complete
        ' for fast PC's (RS232 only) add a delay before a query of > 50 ms
        delay 200
        .WriteString "Calc:Stat ON;*OPC?"       ' Enable math and request operation complete
        status = .ReadNumber                     ' A returned value indicates dmm is ready
        ' for fast PC's (RS232 only) add a delay before a query of > 50 ms
        delay 200
        .WriteString "Read?"                    ' Take readings; send to output buffer
        Readings = .ReadList()                  ' Get readings and parse into array of doubles
    End With
        
    ' print to Text box
    txtResult.Text = ""
    For i = 0 To 4
        txtResult.SelText = Readings(i) & " dBm" & vbCrLf
    Next i
    
    Exit Sub
    
ConfigError:
    MsgBox "Error in Config: " & Err.Description

End Sub

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    Dim sfc As VisaComLib.ISerial

    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the DMM", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set DMM = New VisaComLib.FormattedIO488
    Set DMM.IO = mgr.Open(m_ioAddress)
    ' Set RS232 parameters if this is RS232
    If DMM.IO.HardwareInterfaceName = "ASRL" Then
        Set sfc = DMM.IO
        sfc.BaudRate = 9600
        sfc.FlowControl = ASRL_FLOW_DTR_DSR
        ' For RS232 set the termination character to LF
        DMM.IO.TerminationCharacter = 10
        DMM.IO.TerminationCharacterEnabled = True
        ' send the remote for RS232 only
        DMM.WriteString "Syst:Rem"
    End If

    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

End Sub

