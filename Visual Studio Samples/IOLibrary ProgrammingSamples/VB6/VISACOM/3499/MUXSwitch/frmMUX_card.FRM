VERSION 5.00
Begin VB.Form frmMUX_card 
   Caption         =   "Form1"
   ClientHeight    =   4365
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9405
   LinkTopic       =   "Form1"
   ScaleHeight     =   4365
   ScaleWidth      =   9405
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   495
      Left            =   6840
      TabIndex        =   29
      Top             =   2280
      Width           =   1695
   End
   Begin VB.TextBox txtGPIBaddress 
      BackColor       =   &H8000000F&
      Enabled         =   0   'False
      Height          =   375
      Left            =   6600
      TabIndex        =   28
      Text            =   "GPIB::9"
      Top             =   480
      Width           =   2655
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   1
      Left            =   4920
      TabIndex        =   27
      Top             =   480
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   10
      Left            =   2160
      TabIndex        =   26
      Top             =   120
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   11
      Left            =   2160
      TabIndex        =   25
      Top             =   480
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   0
      Left            =   4920
      TabIndex        =   24
      Top             =   120
      Width           =   500
   End
   Begin VB.CommandButton cmdReadSlots 
      Caption         =   "Query modules"
      Height          =   375
      Left            =   6840
      TabIndex        =   23
      Top             =   1680
      Width           =   1695
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   2
      Left            =   4920
      TabIndex        =   19
      Top             =   840
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   19
      Left            =   2160
      TabIndex        =   18
      Top             =   3360
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   18
      Left            =   2160
      TabIndex        =   17
      Top             =   3000
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   17
      Left            =   2160
      TabIndex        =   16
      Top             =   2640
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   16
      Left            =   2160
      TabIndex        =   15
      Top             =   2280
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   15
      Left            =   2160
      TabIndex        =   14
      Top             =   1920
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   14
      Left            =   2160
      TabIndex        =   13
      Top             =   1560
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   13
      Left            =   2160
      TabIndex        =   12
      Top             =   1200
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   12
      Left            =   2160
      TabIndex        =   11
      Top             =   840
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   9
      Left            =   4920
      TabIndex        =   10
      Top             =   3360
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   8
      Left            =   4920
      TabIndex        =   9
      Top             =   3000
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   7
      Left            =   4920
      TabIndex        =   8
      Top             =   2640
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   6
      Left            =   4920
      TabIndex        =   7
      Top             =   2280
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   5
      Left            =   4920
      TabIndex        =   6
      Top             =   1920
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   4
      Left            =   4920
      TabIndex        =   5
      Top             =   1560
      Width           =   500
   End
   Begin VB.CheckBox chkRelay 
      Height          =   195
      Index           =   3
      Left            =   4920
      TabIndex        =   4
      Top             =   1200
      Width           =   500
   End
   Begin VB.CommandButton cmdInstrSettings 
      Caption         =   "Get Instr Settings"
      Height          =   495
      Left            =   6840
      TabIndex        =   2
      Tag             =   "134"
      Top             =   3600
      Width           =   1695
   End
   Begin VB.ComboBox cboSlot 
      Height          =   315
      ItemData        =   "frmMUX_card.frx":0000
      Left            =   6600
      List            =   "frmMUX_card.frx":0007
      TabIndex        =   1
      Text            =   "Combo1"
      Top             =   1200
      Width           =   2775
   End
   Begin VB.CommandButton cmdOpenAll 
      Caption         =   "Open All"
      Height          =   495
      Left            =   6840
      TabIndex        =   0
      Tag             =   "133"
      Top             =   3000
      Width           =   1695
   End
   Begin VB.Line Line34 
      BorderWidth     =   3
      Index           =   1
      X1              =   4080
      X2              =   4920
      Y1              =   240
      Y2              =   240
   End
   Begin VB.Line Line29 
      BorderWidth     =   3
      Index           =   1
      X1              =   4080
      X2              =   4920
      Y1              =   600
      Y2              =   600
   End
   Begin VB.Line Line20 
      BorderWidth     =   3
      Index           =   1
      X1              =   5400
      X2              =   5640
      Y1              =   600
      Y2              =   600
   End
   Begin VB.Line Line1 
      BorderWidth     =   3
      Index           =   1
      X1              =   1320
      X2              =   2160
      Y1              =   240
      Y2              =   240
   End
   Begin VB.Line Line2 
      BorderWidth     =   3
      Index           =   1
      X1              =   1320
      X2              =   2160
      Y1              =   600
      Y2              =   600
   End
   Begin VB.Line Line9 
      BorderWidth     =   3
      Index           =   1
      X1              =   2400
      X2              =   2880
      Y1              =   240
      Y2              =   240
   End
   Begin VB.Line Line11 
      BorderWidth     =   3
      Index           =   1
      X1              =   2400
      X2              =   2880
      Y1              =   600
      Y2              =   600
   End
   Begin VB.Line Line22 
      BorderWidth     =   3
      Index           =   1
      X1              =   5400
      X2              =   5640
      Y1              =   240
      Y2              =   240
   End
   Begin VB.Line Line35 
      BorderWidth     =   3
      Index           =   3
      X1              =   120
      X2              =   4080
      Y1              =   4080
      Y2              =   4080
   End
   Begin VB.Label Label4 
      Caption         =   "GPIB address:"
      Height          =   255
      Left            =   6600
      TabIndex        =   22
      Tag             =   "130"
      Top             =   240
      Width           =   1815
   End
   Begin VB.Line Line21 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4080
      Y1              =   240
      Y2              =   4080
   End
   Begin VB.Line Line22 
      BorderWidth     =   3
      Index           =   0
      X1              =   5400
      X2              =   5640
      Y1              =   960
      Y2              =   960
   End
   Begin VB.Label Label3 
      Caption         =   "Com"
      Height          =   255
      Left            =   3000
      TabIndex        =   21
      Top             =   3720
      Width           =   375
   End
   Begin VB.Label Label2 
      Caption         =   "Com"
      Height          =   255
      Left            =   5880
      TabIndex        =   20
      Top             =   3720
      Width           =   375
   End
   Begin VB.Line Line36 
      BorderWidth     =   3
      Index           =   3
      X1              =   1320
      X2              =   2880
      Y1              =   3840
      Y2              =   3840
   End
   Begin VB.Line Line13 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   2040
      Y2              =   2040
   End
   Begin VB.Line Line12 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   1680
      Y2              =   1680
   End
   Begin VB.Line Line11 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   1320
      Y2              =   1320
   End
   Begin VB.Line Line10 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   1320
      Y1              =   240
      Y2              =   4080
   End
   Begin VB.Line Line9 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   960
      Y2              =   960
   End
   Begin VB.Line Line8 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   3480
      Y2              =   3480
   End
   Begin VB.Line Line7 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   3120
      Y2              =   3120
   End
   Begin VB.Line Line6 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   2760
      Y2              =   2760
   End
   Begin VB.Line Line5 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   2400
      Y2              =   2400
   End
   Begin VB.Line Line4 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   2040
      Y2              =   2040
   End
   Begin VB.Line Line3 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   1680
      Y2              =   1680
   End
   Begin VB.Line Line2 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   1320
      Y2              =   1320
   End
   Begin VB.Line Line1 
      BorderWidth     =   3
      Index           =   0
      X1              =   1320
      X2              =   2160
      Y1              =   960
      Y2              =   960
   End
   Begin VB.Line Line14 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   2400
      Y2              =   2400
   End
   Begin VB.Line Line15 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   2760
      Y2              =   2760
   End
   Begin VB.Line Line16 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   3120
      Y2              =   3120
   End
   Begin VB.Line Line17 
      BorderWidth     =   3
      Index           =   0
      X1              =   2400
      X2              =   2880
      Y1              =   3480
      Y2              =   3480
   End
   Begin VB.Line Line18 
      BorderWidth     =   3
      Index           =   0
      X1              =   5160
      X2              =   5640
      Y1              =   2040
      Y2              =   2040
   End
   Begin VB.Line Line19 
      BorderWidth     =   3
      Index           =   0
      X1              =   5160
      X2              =   5640
      Y1              =   1680
      Y2              =   1680
   End
   Begin VB.Line Line20 
      BorderWidth     =   3
      Index           =   0
      X1              =   5400
      X2              =   5640
      Y1              =   1320
      Y2              =   1320
   End
   Begin VB.Line Line23 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   3480
      Y2              =   3480
   End
   Begin VB.Line Line24 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   3120
      Y2              =   3120
   End
   Begin VB.Line Line25 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   2760
      Y2              =   2760
   End
   Begin VB.Line Line26 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   2400
      Y2              =   2400
   End
   Begin VB.Line Line27 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   2040
      Y2              =   2040
   End
   Begin VB.Line Line28 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   1680
      Y2              =   1680
   End
   Begin VB.Line Line29 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   1320
      Y2              =   1320
   End
   Begin VB.Line Line30 
      BorderWidth     =   3
      Index           =   0
      X1              =   5160
      X2              =   5640
      Y1              =   2400
      Y2              =   2400
   End
   Begin VB.Line Line31 
      BorderWidth     =   3
      Index           =   0
      X1              =   5160
      X2              =   5640
      Y1              =   2760
      Y2              =   2760
   End
   Begin VB.Line Line32 
      BorderWidth     =   3
      Index           =   0
      X1              =   5160
      X2              =   5640
      Y1              =   3120
      Y2              =   3120
   End
   Begin VB.Line Line33 
      BorderWidth     =   3
      Index           =   2
      X1              =   5160
      X2              =   5640
      Y1              =   3480
      Y2              =   3480
   End
   Begin VB.Line Line34 
      BorderWidth     =   3
      Index           =   0
      X1              =   4080
      X2              =   4920
      Y1              =   960
      Y2              =   960
   End
   Begin VB.Line Line36 
      BorderWidth     =   3
      Index           =   2
      X1              =   4080
      X2              =   5640
      Y1              =   3840
      Y2              =   3840
   End
   Begin VB.Label Label11 
      Caption         =   "Module:"
      Height          =   255
      Left            =   6600
      TabIndex        =   3
      Tag             =   "131"
      Top             =   960
      Width           =   1335
   End
   Begin VB.Line Line38 
      X1              =   6360
      X2              =   6360
      Y1              =   180
      Y2              =   4200
   End
End
Attribute VB_Name = "frmMUX_card"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
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
'
' This example code demonstrates:
' 1. How to open and close a relay on a MUX card
' 2. How to query the relay on a MUX card
' 3. How to query the name of the slots of a 3499 (check first two slots)
'
' This example code uses the Agilent VISA COM library
'
Dim m_initializing As Boolean

Dim Switch As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::9"
Dim m_ioAddress As String


Private Sub Command1_Click()

    Dim io_mgr As VisaComLib.ResourceManager
    Dim reply As String
    
    Set io_mgr = New VisaComLib.ResourceManager
    Set Switch = New VisaComLib.FormattedIO488
 '   Set Switch.IO = io_mgr.Open(GPIB_ADDRESS)
    Set Switch.IO = io_mgr.Open(m_ioAddress)

   ' Me.txtGPIBaddress.Text = GPIB_ADDRESS
    Switch.WriteString "*IDN?"
    reply = Switch.ReadString
    
Debug.Print reply
End Sub

Private Sub Form_Load()
    Dim chk As CheckBox
    Dim reply As String
    
    On Error Resume Next

    ' create the graphic labels from the index
    For Each chk In Me.chkRelay
        chk.Caption = Format$(chk.index, "00")
    Next chk
    
    ' place holders for modules
    With cboSlot
        .Clear
        .AddItem " 100"    ' index 0
        .AddItem " 200"
        .AddItem " 300"
    End With
    cboSlot.ListIndex = 0
    

    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

   
End Sub

Private Sub chkRelay_Click(index As Integer)
    ' Close the selected relay if checked, open if unchecked
    Dim relay As String
    Dim slot As Long
    
    On Error Resume Next
    
    ' don't respond to the click event during open all and check
    ' instrument state request
    If m_initializing Then Exit Sub

    If isconnected(True) Then
        slot = Format$(cboSlot.ListIndex + 1)
        relay = slot & Format$(index, "00")
        If chkRelay(index).Value = vbChecked Then
            relay = "Route:Close (@" & relay & ")"
        Else
            relay = "Route:Open (@" & relay & ")"
        End If
        Switch.WriteString relay
        InstrumentError Switch
    End If
    
End Sub

Private Sub cmdOpenAll_Click()
    ' opens all relays and unchecks the check boxes
    ' This is a slow way, but it demonstrates the commands
    Dim chk As CheckBox
    Dim relay As String
    Dim index As Long
    Dim slot As String
    
    On Error Resume Next
    
    cmdOpenAll.Enabled = False
    m_initializing = True
    
    If isconnected(True) Then
        slot = Format$(cboSlot.ListIndex + 1)
        For Each chk In Me.chkRelay
            chk.Value = vbUnchecked

            ' The click event is disabled with the cmdOpenAll.Enabled and
            ' will not fire, make sure relay is open
            relay = slot & Format$(chk.index, "00")
            relay = "Route:Open (@" & relay & ")"
            Switch.WriteString relay
        Next chk
    End If
    
    m_initializing = False
    cmdOpenAll.Enabled = True
    
End Sub
Private Sub cmdInstrSettings_Click()
    ' Gets the settings of the instrument and checks the check boxes of the closed relays
    '
    Dim relay As String
    Dim strTemp As String
    Dim index As Long
    Dim chk As CheckBox
    Dim isClosed As Boolean
    Dim slot As Long
    Dim Instrument As String
    
    On Error GoTo SettingsError
    
    
    cmdInstrSettings.Enabled = False
    Me.MousePointer = vbHourglass
    
    ' set initializing so that when checking the check box it does
    ' not send command to instrument
    m_initializing = True


    If isconnected(True) Then
        slot = Format$(cboSlot.ListIndex + 1)
        For Each chk In Me.chkRelay
            relay = slot & Format$(chk.index, "00")
            relay = "Route:Close? (@" & relay & ")"

            Switch.WriteString relay
            strTemp = Switch.ReadString
            isClosed = CBool(strTemp)
            If isClosed Then
                chk.Value = vbChecked
            Else
                chk.Value = vbUnchecked
            End If
        Next chk
        
        InstrumentError Switch
    End If

SettingsError:
    cmdInstrSettings.Enabled = True
    Me.MousePointer = vbDefault
    m_initializing = False
End Sub


Private Function isconnected(Optional TestForMUXcard As Boolean = True) As Boolean
    ' determines if the instrument is connected, and
    ' gives a message if not
    Dim result() As Variant
    Dim module As String
    Dim oldTimeout As Long
    Dim slotNumber As String
    Dim cmd As String

    On Error GoTo connectError
    
    With Switch
        .WriteString "*IDN?"
        result = .ReadList
    End With

    If InStr(1, result(1), "3499", vbTextCompare) = 0 Then
        GoTo WrongInstrumentError
    End If

    If TestForMUXcard = True Then
        slotNumber = Left$(Trim(cboSlot.Text), 1)
        ' See if we have a N2260A card in the module
        cmd = "System:CType? " & slotNumber
        Switch.WriteString cmd
        module = Switch.ReadString

        If InStr(1, module, "N2260A", vbTextCompare) Or _
            InStr(1, module, "N2266A", vbTextCompare) Then
            '
        Else
            GoTo WrongModuleError
        End If
    End If

    isconnected = True

    Exit Function

connectError:
    MsgBox "Instrument not connected. Please check connections and set the GPIB address in the 3494A control properties."
    isconnected = False
    Exit Function
WrongInstrumentError:
    MsgBox "Incorrect instrument: " & vbCrLf & _
            "Expected 3499 " & vbCrLf & _
            "Instrument discovered: " & result(1)
    isconnected = False
    Exit Function
WrongModuleError:
    MsgBox "There is not a N2260A or 44470 matrix card in the selected module"

End Function

Public Function InstrumentError(Switch As VisaComLib.FormattedIO488) As Boolean
    Dim reply As String

    With Switch
        .WriteString "Syst:Error?"
        reply = .ReadString
    End With

    If Val(reply) <> 0 Then
        InstrumentError = True
        MsgBox "Instrument Error: " & vbCrLf & reply, vbCritical
        InstrumentError Switch
    Else
        InstrumentError = False
    End If

End Function

Private Sub cmdReadSlots_Click()
    ' read the slots and put into the dropdown
    Dim module As String
    Dim i As Long

    On Error GoTo ErrReadSlot
    '
    ' Add more slots for different instruments
    If isconnected(False) Then
        cboSlot.Clear
        cboSlot.AddItem "1-" & ModuleName("1")
        cboSlot.AddItem "2-" & ModuleName("2")
        cboSlot.AddItem "3-" & ModuleName("3")
        cboSlot.AddItem "4-" & ModuleName("4")
        cboSlot.AddItem "5-" & ModuleName("5")
    End If
    
    ' select the MUX card if it exists
    cboSlot.ListIndex = 0
    For i = 0 To cboSlot.ListCount - 1
        If InStr(1, cboSlot.List(i), "N2260A", vbTextCompare) Or _
        InStr(1, cboSlot.List(i), "44470", vbTextCompare) Then
            cboSlot.ListIndex = i
            Exit For
        End If
    Next i

ErrReadSlot:

End Sub

Private Function ModuleName(ByVal slot As String) As String
    ' Gets the module model number for a given slot
    Dim result As String

    With Switch
        .WriteString "System:CType? " & slot
        result = .ReadString
        If Len(result) < 3 Then
            ModuleName = "Error"
        Else
            result = Left$(result, Len(result) - 1)
            ModuleName = result
        End If
    End With
    InstrumentError Switch

End Function

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the 3499A/B/C", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set Switch = New VisaComLib.FormattedIO488
    Set Switch.IO = mgr.Open(m_ioAddress)
    
    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub




