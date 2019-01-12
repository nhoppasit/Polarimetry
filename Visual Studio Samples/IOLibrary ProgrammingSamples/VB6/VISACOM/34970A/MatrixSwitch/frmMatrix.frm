VERSION 5.00
Begin VB.Form frmMatrix 
   Caption         =   "Form1"
   ClientHeight    =   5820
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7815
   LinkTopic       =   "Form1"
   ScaleHeight     =   5820
   ScaleWidth      =   7815
   Begin VB.CommandButton cmdReadSlots 
      Caption         =   "Read Slots"
      Height          =   495
      Left            =   3240
      TabIndex        =   49
      Top             =   4680
      Width           =   1575
   End
   Begin VB.ComboBox cboSlot 
      Height          =   315
      ItemData        =   "frmMatrix.frx":0000
      Left            =   600
      List            =   "frmMatrix.frx":000D
      TabIndex        =   46
      Text            =   "Combo1"
      Top             =   4680
      Width           =   2055
   End
   Begin VB.CommandButton cmdOpenAll 
      Caption         =   "Open All"
      Height          =   495
      Left            =   6000
      TabIndex        =   45
      Top             =   4560
      Width           =   1455
   End
   Begin VB.CommandButton cmdInstrSettings 
      Caption         =   "Get Instrument settings"
      Height          =   495
      Left            =   6000
      TabIndex        =   44
      Top             =   5160
      Width           =   1455
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   17
      Left            =   4920
      TabIndex        =   35
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   18
      Left            =   5640
      TabIndex        =   34
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   27
      Left            =   4920
      TabIndex        =   33
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   28
      Left            =   5640
      TabIndex        =   32
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   37
      Left            =   4920
      TabIndex        =   31
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   38
      Left            =   5640
      TabIndex        =   30
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   47
      Left            =   4920
      TabIndex        =   29
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "46"
      Height          =   220
      Index           =   48
      Left            =   5640
      TabIndex        =   28
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "46"
      Height          =   220
      Index           =   46
      Left            =   4200
      TabIndex        =   23
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   45
      Left            =   3480
      TabIndex        =   22
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   44
      Left            =   2760
      TabIndex        =   21
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   43
      Left            =   2040
      TabIndex        =   20
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   42
      Left            =   1320
      TabIndex        =   19
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   41
      Left            =   600
      TabIndex        =   18
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   36
      Left            =   4200
      TabIndex        =   17
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   35
      Left            =   3480
      TabIndex        =   16
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   34
      Left            =   2760
      TabIndex        =   15
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   33
      Left            =   2040
      TabIndex        =   14
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   32
      Left            =   1320
      TabIndex        =   13
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   31
      Left            =   600
      TabIndex        =   12
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   26
      Left            =   4200
      TabIndex        =   11
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   25
      Left            =   3480
      TabIndex        =   10
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   24
      Left            =   2760
      TabIndex        =   9
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   23
      Left            =   2040
      TabIndex        =   8
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   22
      Left            =   1320
      TabIndex        =   7
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   21
      Left            =   600
      TabIndex        =   6
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   16
      Left            =   4200
      TabIndex        =   5
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   15
      Left            =   3480
      TabIndex        =   4
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   14
      Left            =   2760
      TabIndex        =   3
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   13
      Left            =   2040
      TabIndex        =   2
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   12
      Left            =   1320
      TabIndex        =   1
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   11
      Left            =   600
      TabIndex        =   0
      Top             =   1680
      Width           =   220
   End
   Begin VB.Label Label12 
      Caption         =   "Set the address on the 3494A control and set the slot for the Matrix module.  You must have a 34904A module in one of the slots."
      Height          =   375
      Left            =   720
      TabIndex        =   48
      Top             =   240
      Width           =   6255
   End
   Begin VB.Label Label11 
      Caption         =   "Slot for Matrix module"
      Height          =   255
      Left            =   600
      TabIndex        =   47
      Top             =   4440
      Width           =   2055
   End
   Begin VB.Label Label10 
      Caption         =   "Col 8"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   5520
      TabIndex        =   43
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label9 
      Caption         =   "Col 7"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   4800
      TabIndex        =   42
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label8 
      Caption         =   "Col 6"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   4080
      TabIndex        =   41
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label7 
      Caption         =   "Col 5"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   3360
      TabIndex        =   40
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label6 
      Caption         =   "Col 4"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2640
      TabIndex        =   39
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label5 
      Caption         =   "Col 3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1920
      TabIndex        =   38
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label4 
      Caption         =   "Col 2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   1200
      TabIndex        =   37
      Top             =   840
      Width           =   495
   End
   Begin VB.Label Label3 
      Caption         =   "Col 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   480
      TabIndex        =   36
      Top             =   840
      Width           =   495
   End
   Begin VB.Line Line11 
      BorderWidth     =   4
      X1              =   5040
      X2              =   5040
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Line Line10 
      BorderWidth     =   4
      X1              =   5760
      X2              =   5760
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Label Label1 
      Caption         =   "Row 1"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   2
      Left            =   6600
      TabIndex        =   27
      Top             =   1680
      Width           =   975
   End
   Begin VB.Label Label1 
      Caption         =   "Row 2"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   1
      Left            =   6600
      TabIndex        =   26
      Top             =   2400
      Width           =   975
   End
   Begin VB.Label Label2 
      Caption         =   "Row 3"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   6600
      TabIndex        =   25
      Top             =   3120
      Width           =   975
   End
   Begin VB.Label Label1 
      Caption         =   "Row 4"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Index           =   0
      Left            =   6600
      TabIndex        =   24
      Top             =   3840
      Width           =   975
   End
   Begin VB.Line Line9 
      BorderWidth     =   4
      X1              =   4320
      X2              =   4320
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Line Line8 
      BorderWidth     =   4
      X1              =   3600
      X2              =   3600
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Line Line7 
      BorderWidth     =   4
      X1              =   2880
      X2              =   2880
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Line Line6 
      BorderWidth     =   4
      X1              =   2160
      X2              =   2160
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Line Line5 
      BorderWidth     =   4
      X1              =   1440
      X2              =   1440
      Y1              =   1200
      Y2              =   3960
   End
   Begin VB.Line Line4 
      BorderWidth     =   4
      X1              =   720
      X2              =   720
      Y1              =   1200
      Y2              =   3840
   End
   Begin VB.Line Line3 
      BorderWidth     =   4
      X1              =   840
      X2              =   6480
      Y1              =   3960
      Y2              =   3960
   End
   Begin VB.Line Line2 
      BorderWidth     =   4
      X1              =   840
      X2              =   6480
      Y1              =   3240
      Y2              =   3240
   End
   Begin VB.Line Line1 
      BorderWidth     =   4
      Index           =   1
      X1              =   720
      X2              =   6480
      Y1              =   2520
      Y2              =   2520
   End
   Begin VB.Line Line1 
      BorderWidth     =   4
      Index           =   0
      X1              =   720
      X2              =   6480
      Y1              =   1800
      Y2              =   1800
   End
End
Attribute VB_Name = "frmMatrix"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2001, 2002 Agilent Technologies Inc.  All rights reserved.
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
' This program demonstrates how to open and close a relay for the
' Matrix card
Dim Switch As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::9"
Dim m_ioAddress As String


Private Sub cmdInstrSettings_Click()
    ' Gets the settings of the instrument and checks the check boxes of the closed realays
    Dim relay As String
    Dim strTemp As String
    Dim index As Long
    Dim chk As CheckBox
    Dim isClosed As Long


    If isconnected Then
        For Each chk In Me.chkCrossPoint
            relay = slot & Format$(chk.index)
            relay = "Route:Close? (@" & relay & ")"

            Switch.WriteString relay
            strTemp = Switch.ReadString
            isClosed = CBool(strTemp)

            If CBool(isClosed) Then
                chk.Value = vbChecked
            Else
                chk.Value = vbUnchecked
            End If
        Next chk

        InstrumentError Switch
    End If

End Sub

Private Sub cmdOpenAll_Click()
    ' opens all relays and unchecks the check boxes
    Dim chk As CheckBox
    Dim relay As String
    Dim index As Long

    If isconnected Then
        For Each chk In Me.chkCrossPoint
            chk.Value = vbUnchecked

            ' if the checkbox is already unchecked, the click event
            ' will not fire, so make sure relay is open
            relay = slot & Format$(chk.index)
            relay = "Route:Open (@" & relay & ")"
            Switch.WriteString relay
        Next chk
    End If

End Sub

Private Sub chkCrossPoint_Click(index As Integer)
    ' Close the selected relay if checked, open if unchecked
    Dim relay As String

    relay = slot & Format$(index)

    If isconnected Then
        If chkCrossPoint(index) = vbChecked Then
            relay = "Route:Close (@" & relay & ")"
        Else
            relay = "Route:Open (@" & relay & ")"
        End If

        Switch.WriteString relay
        Debug.Print "Command sent to the instrument = "; relay
        InstrumentError Switch
    End If


End Sub



Public Sub InstrumentError(Switch As VisaComLib.FormattedIO488)
    Dim reply As String

    With Switch
        .WriteString "Syst:Error?"
        reply = .ReadString
    End With

    If Val(reply) <> 0 Then
        MsgBox "Instrument Error: " & vbCrLf & reply, vbCritical
        InstrumentError Switch
    End If

End Sub


Private Function isconnected(Optional TestForMatrixCard As Boolean = True) As Boolean
    ' determines if the instrument is connected, and
    ' gives a message if not
    Dim result() As Variant
    Dim module As String
    Dim oldTimeout As Long
    Dim slotNumber As String

    On Error GoTo connectError

    With Switch
        oldTimeout = .IO.Timeout
        .IO.Timeout = 1000
        .WriteString "*IDN?"
        result = .ReadList
        .IO.Timeout = oldTimeout
    End With

    If InStr(1, result(1), "34970A", vbTextCompare) = 0 Then
        GoTo WrongInstrumentError
    End If

    If TestForMatrixCard = True Then
        slotNumber = Left$(cboSlot.Text, 3)
        ' See if we have a 34904A card in the module
        Switch.WriteString "System:CType? " & slotNumber
        module = Switch.ReadString

        If InStr(1, module, "34904A", vbTextCompare) = 0 Then
            GoTo WrongModuleError
        End If
    End If

    isconnected = True

    Exit Function

connectError:
    Switch.IO.Timeout = 10000
    MsgBox "Instrument not connected. Please check connections and use the Set I/O button to set the instrument connection."
    isconnected = False
    Exit Function
WrongInstrumentError:
    MsgBox "Incorrect instrument: " & vbCrLf & _
            "Expected 34970A " & vbCrLf & _
            "Instrument discovered: " & result(1)
    isconnected = False
    Exit Function
WrongModuleError:
    MsgBox "There is not a 34904A matrix card in the selected module"

End Function

Private Function slot() As String
    ' Gets the slot setting for the module part of the channel list
    Dim strTemp As String

    strTemp = Me.cboSlot.Text
    slot = Left$(strTemp, 1)


End Function
Private Function ModuleName(ByVal slot As String) As String
    ' Gets the module model number for a given slot
    Dim result() As Variant

    With Switch
        .WriteString "System:CType? " & slot
        result = .ReadList
        If Len(result(1)) < 3 Then
            ModuleName = "Empty"
        Else
            ModuleName = result(1)
        End If
    End With
    InstrumentError Switch

End Function

Private Sub cmdReadSlots_Click()
    ' read the slots and put into the dropdown
    Dim module As String
    Dim i As Long

    On Error GoTo ErrReadSlot

    If isconnected(False) Then
        cboSlot.Clear
        cboSlot.AddItem "100-" & ModuleName("100")
        cboSlot.AddItem "200-" & ModuleName("200")
        cboSlot.AddItem "300-" & ModuleName("300")
    End If
    
    ' select the 34904 if it exists
    cboSlot.ListIndex = 0
    For i = 0 To cboSlot.ListCount - 1
        If InStr(1, cboSlot.List(i), "34904", vbTextCompare) Then
            cboSlot.ListIndex = i
            Exit For
        End If
    Next i

ErrReadSlot:

End Sub

Private Sub cmdSetIO_Click()
' set the I/O address to the text box in case the
' user changed it.
' bring up the input dialog and save any changes to the
' text box
    Dim mgr As VisaComLib.ResourceManager
    
    On Error GoTo ioError

    m_ioAddress = InputBox("Enter the IO address of the 34970A", "Set IO address", m_ioAddress)

    Set mgr = New VisaComLib.ResourceManager
    Set Switch = New VisaComLib.FormattedIO488
    Set Switch.IO = mgr.Open(m_ioAddress)
    
    Exit Sub
ioError:
    MsgBox "Set IO error:" & vbCrLf & Err.Description

End Sub


Private Sub Form_Load()
    Me.cboSlot.ListIndex = 0
    m_ioAddress = IO_ADDRESS
    cmdSetIO_Click

End Sub


