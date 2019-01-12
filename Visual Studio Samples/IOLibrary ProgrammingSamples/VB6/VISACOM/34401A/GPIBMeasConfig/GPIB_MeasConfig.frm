VERSION 5.00
Begin VB.Form frmGPIB_MeasConfig 
   Caption         =   "GPIB_MeasConfig"
   ClientHeight    =   2655
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4785
   LinkTopic       =   "Form1"
   ScaleHeight     =   2655
   ScaleWidth      =   4785
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdConfigure 
      Caption         =   "using Configure"
      Height          =   495
      Left            =   3000
      TabIndex        =   3
      Top             =   720
      Width           =   1455
   End
   Begin VB.CommandButton cmdMeasure 
      Caption         =   "using Measure?"
      Height          =   495
      Left            =   3000
      TabIndex        =   2
      Top             =   120
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
Attribute VB_Name = "frmGPIB_MeasConfig"
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

Private Sub cmdMeasure_Click()
    ' The following example uses Measure? command to make a single
    ' ac current measurement. This is the easiest way to program the
    ' multimeter for measurements. However, MEASure? does not offer
    ' much flexibility.
    '
    ' Be sure to set the instrument address in the Form.Load routine
    ' to match the instrument.
    Dim reply As Double
    
    ' EXAMPLE for using the Measure command
    DMM.WriteString "*RST"
    DMM.WriteString "*CLS"
    ' Set meter to 1 amp ac range
    DMM.WriteString "Measure:Current:AC? 1A,0.001MA"
    reply = DMM.ReadNumber
        
    txtResult.Text = reply & " amps AC"
    
End Sub
Private Sub cmdConfigure_Click()
    ' The following example uses CONFigure with the dBm math operation
    ' The CONFigure command gives you a little more programming flexibility
    ' than the MEASure? command. This allows you to 'incrementally'
    ' change the multimeter's configuration.
    '
    ' Be sure to set the instrument address
    ' to match the instrument
    '
    Dim Readings() As Variant
    Dim i As Long
    Dim status As Long
    
    cmdConfigure.Enabled = False
    
    ' Taking five AC voltage measurements takes several seconds, so make the timeout
    ' value large enough to let the 34401 finish taking the measurements.
    DMM.IO.Timeout = 10000
    
    ' EXAMPLE for using the CONFigure command
    DMM.WriteString "*RST"                      ' Reset the dmm
   DMM.WriteString "*CLS"                      ' Clear dmm status registers
   DMM.WriteString "CALC:DBM:REF 50"           ' set 50 ohm reference for dBm
        ' the CONFigure command sets range and resolution for AC
        ' all other AC function parameters are defaulted but can be
        ' set before a READ?
   DMM.WriteString "Conf:Volt:AC 1, 0.001"      ' set dmm to 1 amp ac range"
   DMM.WriteString ":Det:Band 200"              ' Select the 200 Hz (fast) ac filter
   DMM.WriteString "Trig:Coun 5"               ' dmm will accept 5 triggers
   DMM.WriteString "Trig:Sour IMM"             ' Trigger source is IMMediate
   DMM.WriteString "Calc:Func DBM"             ' Select dBm function
   DMM.WriteString "Calc:Stat ON"        ' Enable math and request operation complete
   DMM.WriteString "Read?"                     ' Take readings; send to output buffer
   Readings = DMM.ReadList                     ' Get readings and parse into array of doubles
                                            ' Enter will wait until all readings are completed
    
    ' print to Text box
    txtResult.Text = ""
    For i = 0 To UBound(Readings)
        txtResult.SelText = Readings(i) & " dBm" & vbCrLf
    Next i
    
    cmdConfigure.Enabled = True


End Sub

Private Sub Form_Load()
    ' Set up the IO for address 22
    ' bring up the input dialog and save any changes to the
    ' text box
    Dim ioAddress As String
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError
    
    ioAddress = InputBox("Enter the IO address of the DMM", "Set IO address", "GPIB::22")
    
    Set mgr = New VisaComLib.ResourceManager
    Set DMM = New VisaComLib.FormattedIO488
    Set DMM.IO = mgr.Open(ioAddress)
    
    Exit Sub
    
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub
