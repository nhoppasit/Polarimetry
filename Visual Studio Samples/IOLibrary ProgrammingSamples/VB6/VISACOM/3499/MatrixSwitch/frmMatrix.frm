VERSION 5.00
Begin VB.Form frmMatrix 
   Caption         =   "Form1"
   ClientHeight    =   5235
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6885
   LinkTopic       =   "Form1"
   ScaleHeight     =   5235
   ScaleWidth      =   6885
   Begin VB.CommandButton cmdReadSlots 
      Caption         =   "Read Slots"
      Height          =   495
      Left            =   4920
      TabIndex        =   29
      Top             =   4560
      Width           =   1575
   End
   Begin VB.ComboBox cboSlot 
      Height          =   315
      ItemData        =   "frmMatrix.frx":0000
      Left            =   600
      List            =   "frmMatrix.frx":000D
      TabIndex        =   26
      Text            =   "Combo1"
      Top             =   4680
      Width           =   3375
   End
   Begin VB.CommandButton cmdOpenAll 
      Caption         =   "Open All"
      Height          =   495
      Left            =   5160
      TabIndex        =   25
      Top             =   960
      Width           =   1455
   End
   Begin VB.CommandButton cmdInstrSettings 
      Caption         =   "Get Instrument settings"
      Height          =   495
      Left            =   5160
      TabIndex        =   24
      Top             =   1560
      Width           =   1455
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   33
      Left            =   2760
      TabIndex        =   15
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   32
      Left            =   2040
      TabIndex        =   14
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   31
      Left            =   1320
      TabIndex        =   13
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   30
      Left            =   600
      TabIndex        =   12
      Top             =   3840
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   23
      Left            =   2760
      TabIndex        =   11
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   22
      Left            =   2040
      TabIndex        =   10
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   21
      Left            =   1320
      TabIndex        =   9
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   20
      Left            =   600
      TabIndex        =   8
      Top             =   3120
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   13
      Left            =   2760
      TabIndex        =   7
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   12
      Left            =   2040
      TabIndex        =   6
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   11
      Left            =   1320
      TabIndex        =   5
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   10
      Left            =   600
      TabIndex        =   4
      Top             =   2400
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   3
      Left            =   2760
      TabIndex        =   3
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   2
      Left            =   2040
      TabIndex        =   2
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   1
      Left            =   1320
      TabIndex        =   1
      Top             =   1680
      Width           =   220
   End
   Begin VB.CheckBox chkCrossPoint 
      Caption         =   "Check1"
      Height          =   220
      Index           =   0
      Left            =   600
      TabIndex        =   0
      Top             =   1680
      Width           =   220
   End
   Begin VB.Line Line1 
      BorderWidth     =   4
      Index           =   0
      X1              =   720
      X2              =   3840
      Y1              =   1800
      Y2              =   1800
   End
   Begin VB.Line Line1 
      BorderWidth     =   4
      Index           =   1
      X1              =   720
      X2              =   3840
      Y1              =   2520
      Y2              =   2520
   End
   Begin VB.Line Line2 
      BorderWidth     =   4
      X1              =   840
      X2              =   3840
      Y1              =   3240
      Y2              =   3240
   End
   Begin VB.Line Line3 
      BorderWidth     =   4
      X1              =   840
      X2              =   3840
      Y1              =   3960
      Y2              =   3960
   End
   Begin VB.Label Label12 
      Caption         =   "Set the address on the 3494A control and set the slot for the Matrix module.  You must have a N2265A module in one of the slots."
      Height          =   375
      Left            =   720
      TabIndex        =   28
      Top             =   240
      Width           =   5775
   End
   Begin VB.Label Label11 
      Caption         =   "Slot for Matrix module"
      Height          =   255
      Left            =   600
      TabIndex        =   27
      Top             =   4440
      Width           =   2055
   End
   Begin VB.Label Label6 
      Caption         =   "Col 0"
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
      TabIndex        =   23
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
      Left            =   2640
      TabIndex        =   22
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
      Left            =   1920
      TabIndex        =   21
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
      Left            =   1200
      TabIndex        =   20
      Top             =   840
      Width           =   495
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
      Left            =   3960
      TabIndex        =   19
      Top             =   2400
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
      Left            =   3960
      TabIndex        =   18
      Top             =   3120
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
      Left            =   3960
      TabIndex        =   17
      Top             =   3840
      Width           =   975
   End
   Begin VB.Label Label1 
      Caption         =   "Row 0"
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
      Left            =   3960
      TabIndex        =   16
      Top             =   1680
      Width           =   975
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
End
Attribute VB_Name = "frmMatrix"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
''' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'''  Copyright © 2001 Agilent Technologies Inc.  All rights reserved.
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
' This program demonstrates:
' 1. How to open and close a relay for the matrix card N2265A in the 3499 Switch Unit
' 2. How to read the slots for the installed card
'
' This example uses the 3494A ActiveX control from the Agilent ISDK for VB 6.0
Dim Switch As VisaComLib.FormattedIO488

Const IO_ADDRESS = "GPIB::9"
Dim m_ioAddress As String


Private Sub cmdInstrSettings_Click()
    ' Gets the settings of the instrument and checks the check boxes of the closed realays
    Dim relay As String
    Dim index As Long
    Dim chk As CheckBox
    Dim isClosed As String


    If isconnected Then
        For Each chk In Me.chkCrossPoint
            relay = slot & Format$(chk.index, "00")
            relay = "Route:Close? (@" & relay & ")"

            Switch.WriteString relay
            isClosed = Switch.ReadString

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
            relay = slot & Format$(chk.index, "00")
            relay = "Route:Open (@" & relay & ")"
            Switch.WriteString relay
        Next chk
    End If

End Sub

Private Sub chkCrossPoint_Click(index As Integer)
    ' Close the selected relay if checked, open if unchecked
    Dim relay As String
    Dim cmd As String

    relay = slot & Format$(index, "00")

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
    Dim cmd As String

    On Error GoTo connectError

    With Switch
        oldTimeout = .IO.Timeout
        .IO.Timeout = 1000
        .WriteString "*IDN?"
        result = .ReadList
        .IO.Timeout = oldTimeout
    End With

    If InStr(1, result(1), "3499", vbTextCompare) = 0 Then
        GoTo WrongInstrumentError
    End If

    If TestForMatrixCard = True Then
        slotNumber = Left$(cboSlot.Text, 1)
        ' See if we have a N2265A card in the module
        cmd = "System:CType? " & slotNumber
        Switch.WriteString cmd
        module = Switch.ReadString

        If InStr(1, module, "N2265A", vbTextCompare) = 0 Then
            GoTo WrongModuleError
        End If
    End If

    isconnected = True

    Exit Function

connectError:
    Switch.IO.Timeout = 10000
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
    MsgBox "There is not a N2265A matrix card in the selected module"

End Function

Private Function slot() As String
    ' Gets the slot setting for the module part of the channel list
    Dim strTemp As String

    strTemp = Me.cboSlot.Text
    slot = Left$(strTemp, 1)


End Function
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

Private Sub cmdReadSlots_Click()
    ' read the slots and put into the dropdown
    Dim module As String
    Dim i As Long

    On Error GoTo ErrReadSlot

    If isconnected(False) Then
        cboSlot.Clear
        cboSlot.AddItem "1-" & ModuleName("1")
        cboSlot.AddItem "2-" & ModuleName("2")
        cboSlot.AddItem "3-" & ModuleName("3")
        cboSlot.AddItem "4-" & ModuleName("4")
        cboSlot.AddItem "5-" & ModuleName("5")
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

    m_ioAddress = InputBox("Enter the IO address of the 3499A/B/C", "Set IO address", m_ioAddress)

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
