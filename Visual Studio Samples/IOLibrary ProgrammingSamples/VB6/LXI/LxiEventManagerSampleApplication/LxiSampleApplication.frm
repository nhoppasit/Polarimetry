VERSION 5.00
Begin VB.Form LxiSampleApplication 
   Caption         =   "LxiEventManager Sample Application"
   ClientHeight    =   7455
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   8355
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7455
   ScaleWidth      =   8355
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton SendCommand 
      Caption         =   "Send Event"
      Height          =   375
      Left            =   600
      TabIndex        =   4
      Top             =   1680
      Width           =   1815
   End
   Begin VB.ComboBox EventCombo 
      Height          =   315
      Left            =   2760
      TabIndex        =   3
      Text            =   "Combo1"
      Top             =   480
      Width           =   1575
   End
   Begin VB.OptionButton CustomOption 
      Caption         =   "Custom Event"
      Height          =   375
      Left            =   600
      TabIndex        =   2
      Top             =   1080
      Width           =   1455
   End
   Begin VB.OptionButton SimpleOption 
      Caption         =   "Simple Event"
      Height          =   255
      Left            =   600
      TabIndex        =   1
      Top             =   480
      Value           =   -1  'True
      Width           =   1455
   End
   Begin VB.TextBox ReceiveText 
      Height          =   4695
      Left            =   480
      MultiLine       =   -1  'True
      ScrollBars      =   2  'Vertical
      TabIndex        =   0
      Text            =   "LxiSampleApplication.frx":0000
      Top             =   2280
      Width           =   7455
   End
End
Attribute VB_Name = "LxiSampleApplication"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
''  Copyright © 2007 - 2007 Agilent Technologies Inc.  All rights reserved.
''
'' You have a royalty-free right to use, modify, reproduce and distribute
'' the Sample Application Files (and/or any modified version) in any way
'' you find useful, provided that you agree that Agilent Technologies has no
'' warranty, obligations or liability for any Sample Application Files.
''
'' Agilent Technologies provides programming examples for illustration only,
'' This sample program assumes that you are familiar with the programming
'' language being demonstrated and the tools used to create and debug
'' procedures. Agilent Technologies support engineers can help explain the
'' functionality of Agilent Technologies software components and associated
'' commands, but they will not modify these samples to provide added
'' functionality or construct procedures to meet your specific needs.
'' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

'' To develop LxiEventManagerC applications in Microsoft Visual Basic, you first need
'' to add the Visual Basic (VB) declaration file in your VB project as a
'' Module. This file contains the LxiEventManagerC function definitions and constant
'' declarations needed to make LxiEventManagerC calls from Visual Basic.
'' To add this module to your project in VB 6, from the menu, select
'' Project->Add Module, select the 'Existing' tab, and browse to the
'' directory containing the VB Declaration file, select LxiEventManagerC.bas, and
'' press 'Open'.
''
'' The location of the VB declaration file depends on where IO Libaries Suite is installed.
'' Assuming the default IO Libraries Suite install point this file can be found at:
''
''  C:\Program Files\Agilent\IO Libraries Suite\Include\LxiEventManagerC.bas
''

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'  LxiSampleApplication.frm
'
'  This program allows sending a simple LXI event message or a custom one.
'  It registers to be notified when LXI event messages are received by this PC,
'  and hence sees the messages it sends.  It prints a summary of the events received
'  to its text box.
'
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Private m_lxiSession As Long
Private m_sessionValid As Boolean
Private m_newSummary As Boolean
Private m_messageSummary As String

Private Sub Form_Load()
    Dim result As Long
    
    ReceiveText.Text = ""
    
    ' Load the Standard Event Names in the combo box
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN0)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN1)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN2)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN3)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN4)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN5)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN6)
    EventCombo.AddItem (LxiEventManagerC.LXI_MGR_EVENT_ID_LAN7)
    EventCombo.ListIndex = 0
    
    ' Open a send connection for the default destination (all instruments on the local subnet)
    result = lximgrOpen(LxiEventManagerC.LXI_MGR_EVENT_PATH_DEFAULT, m_lxiSession)
    If (result <> 0) Then
        DisplayError "Unable to open an LXI session", result
    Else
        m_sessionValid = True
    End If
    
    ' Install our handler to receive LXI Messages
    ' (note it resides in the "EventHandling.bas" module so its address can be obtained)
    Call SetParentForm(Me)
    result = lximgrInstallHandler(AddressOf RawEventReceivedHandler)
    If (result <> 0) Then DisplayError "Unable to Install Handler", result
    
    'Start Message notification
    result = lximgrStartMessageNotification(LxiEventManagerC.LXI_MGR_EVENT_PATH_LXIPORT)
    If (result <> 0) Then DisplayError "Unable to start message notification", result
    
    ReceiveText.Text = ReceiveText.Text & "Receiving events" & vbCrLf
End Sub

Public Function EventReceivedHandler( _
              ByVal eventId As Long, _
              ByVal receiptSeconds As Double, _
              ByVal receiptFractionalSeconds As Double, _
              ByVal eventSource As Long, _
              ByVal receivedViaUdp As Boolean, _
              ByVal message As Long) _
              As Long
                  
    On Error Resume Next
    
    Dim result As Long
    ' Allocate fixed strings to hold string variables
    Dim msgEventID As String
    Dim receiveTime As String * 32
    
    Dim domain As Byte
    Dim flags As Integer
    Dim seq As Long
    Dim summary As String
    
    ' The 'message' parameter is the received message.
    ' It is only valid for the duration of this call.
    
    ' This is how to get the strings passed as parameters to this function
    Dim fixedStr As String * 256
    Dim eventIdStr As String
    Call lximgrCopyCString(fixedStr, 255, eventId)
    eventIdStr = TrimFixedString(fixedStr)
    
    ' Get the EventID as a fixed string from the event message
    '(note this is the same as the 'eventID' parameter string above)
    result = lximgrGetStringAttribute(message, LXI_ATTR_EVENT_EVENTID, msgEventID)
    If (result <> 0) Then DisplayError "Unable to get event ID", result
    
    ' Get a few more attributes
    result = lximgrGetAttribute(message, LXI_ATTR_EVENT_DOMAIN, domain)
    If (result <> 0) Then DisplayError "Unable to get domain", result
    
    result = lximgrGetAttribute(message, LXI_ATTR_EVENT_FLAGS, flags)
    If (result <> 0) Then DisplayError "Unable to get flags", result
    
    result = lximgrGetAttribute(message, LXI_ATTR_EVENT_SEQUENCE, seq)
    If (result <> 0) Then DisplayError "Unable to get sequence", result
    
    result = lximgrTimeToString(receiveTime, 32, receiptSeconds, receiptFractionalSeconds)
    
    summary = "Received '" & eventIdStr _
        & "', domain " & domain _
        & ", flags " & flags _
        & ", sequence " & seq _
        & " at " & TrimFixedString(receiveTime) & vbCrLf
            
    ReceiveText.Text = ReceiveText.Text & summary
                    
    EventReceivedHandler = result
End Function

Sub DisplayError(msg As String, result As Long)
    ' Allocate a fixed string to receive the error message
    Dim errorMsg As String * 256
    Dim res As Long
    If (result <> 0) Then
        res = lximgrGetLastStatusDesc(errorMsg, 255)
        MsgBox msg & vbCrLf & TrimFixedString(errorMsg), vbOKOnly, "LxiEventManagerC Error"
    End If
    Unload Me
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Dim result As Long
    
    On Error Resume Next
    
    If (m_sessionValid) Then
        ' Clean up
        m_sessionValid = False
        result = lximgrUninstallHandler(AddressOf RawEventReceivedHandler)
        If (result <> 0) Then DisplayError "Unable to Uninstall Handler", result
        result = lximgrClose(m_lxiSession)
        If (result <> 0) Then DisplayError "Unable to close session", result
        result = lximgrStopMessageNotification()
        If (result <> 0) Then DisplayError "Unable to stop messages", result
        result = lximgrShutdown()
        If (result <> 0) Then DisplayError "Unable to shutdown LxiEventManagerC", result
    End If
End Sub

Private Sub SendCommand_Click()
    Dim result As Long
    If (SimpleOption.Value = True) Then
        result = lximgrSendEvent(m_lxiSession, EventCombo.Text)
        If (result <> 0) Then DisplayError "Unable to send simple event", result
    End If
    
    If (CustomOption.Value = True) Then
        Dim lxiEvent As Long
        Dim seq As Long
        
        ' Get the next sequence number for this session and the default LXI domain
        result = lximgrGetSequenceNumber(m_lxiSession, LXI_MGR_DEFAULT_DOMAIN, seq)
        If (result <> 0) Then DisplayError "Unable to get sequence number", result
                   
        ' Create an LXI Message to send
        
        ' We specify the error ID here, but the ID could be any string up to 17 characters
        result = lximgrCreateEventMessage(LXI_MGR_EVENT_ID_ERROR, seq, lxiEvent)
        If (result <> 0) Then DisplayError "Unable to create event message", result
        
        ' Set the error flag to match the error ID (only for errors like this)
        result = lximgrSetAttribute(lxiEvent, LXI_ATTR_EVENT_FLAGS, LXI_ATTR_EVENT_FLAG_ERROR)
        If (result <> 0) Then DisplayError "Unable to set error flag", result
        
        ' Add a data field with an ID of 99
        result = LxiEventManagerC.lximgrAddDataField(lxiEvent, 99, 3, "123")
        If (result <> 0) Then DisplayError "Unable to add data field", result
        
        ' Get the time now and use that as the timestamp in the event message
        ' Unless the PC has a 1588 clock that LxiEventManagerC knows about,
        ' this time will not be 1588-synchronized
        Dim seconds, fraction As Double
        result = lximgrTimeNow(seconds, fraction)
        If (result <> 0) Then DisplayError "Unable to get time now", result
        
        result = lximgrSetTimeAttribute(lxiEvent, LXI_ATTR_EVENT_SECONDS, seconds)
        If (result <> 0) Then DisplayError "Unable to set seconds", result

        result = lximgrSetTimeAttribute(lxiEvent, LXI_ATTR_EVENT_FRACTIONALSECONDS, fraction)
        If (result <> 0) Then DisplayError "Unable to set fractional seconds", result
        
        ' Send the event message
        result = lximgrSendEventMessage(m_lxiSession, lxiEvent)
        If (result <> 0) Then DisplayError "Unable to send custom message", result
        
        ' Now clean up by deleting the custom message
        result = lximgrDeleteEventMessage(lxiEvent)
        If (result <> 0) Then DisplayError "Unable to delete custom message", result
    End If
End Sub

Private Sub SimpleOption_Click()
    EventCombo.Enabled = True
End Sub

Private Sub CustomOption_Click()
    EventCombo.Enabled = False
End Sub
