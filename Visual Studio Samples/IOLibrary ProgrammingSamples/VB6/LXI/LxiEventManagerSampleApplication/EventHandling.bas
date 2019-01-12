Attribute VB_Name = "EventHandling"
' This module is required to allow passing the address of an event handler function
' to LxiEventManagerC.

Private m_parentForm As Form

Public Sub SetParentForm(parent As Form)
    Set m_parentForm = parent
End Sub

Public Function RawEventReceivedHandler(ByVal eventId As Long, _
    ByVal receiptSeconds As Double, _
    ByVal receiptFractionalSeconds As Double, _
    ByVal eventSource As Long, _
    ByVal receivedViaUdp As Boolean, _
    ByVal eventData As Long) As Long
    
    On Error Resume Next
    
    RawEventReceivedHandler = m_parentForm.EventReceivedHandler(eventId, receiptSeconds, receiptFractionalSeconds, eventSource, receivedViaUdp, eventData)
End Function
