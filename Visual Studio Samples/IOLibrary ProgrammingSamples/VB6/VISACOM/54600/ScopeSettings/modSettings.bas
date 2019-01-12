Attribute VB_Name = "modSettings"
Option Explicit
Public Sub OpenSettingsFile(hwnd As Long, sfile As String)
    ' retrieves a user file name to get settings
    g_err.AddToStack "OpenDocument"
    Dim filePath As String
    Dim filter As String
    Dim Cancel As Boolean
    Dim position As Long
    Dim fileExtension As String
    Dim frmType As String
    Dim msg As String
    
    Dim fileIndex As Long

    On Error GoTo ErrorOpenFile


    filter = FILE_FILTER_SETTINGS_OPEN
    
    'Get current directory and process
    ' add the directory separator if needed
    If Right$(CurDir, 1) = "\" Then
        filePath = CurDir & DEF_DATA_FILE_NAME
    Else
        filePath = CurDir & "\" & DEF_DATA_FILE_NAME
    End If
    sfile = OpenDlg(hwnd, filter, _
            filePath, Cancel)

    ' check for the extension if included
    position = InStr(1, sfile, ".")
    If position Then
        fileExtension = Right$(sfile, Len(sfile) - position + 1)
    End If


    ' route to the correct form
    If Not Cancel Then
        Select Case UCase(fileExtension)
            Case ".SCP"
'
            Case Else
                msg = "Incorrect file type"
                MsgBox msg, vbCritical, APP_NAME
        End Select    ' default extension
    End If    'not cancel

    Exit Sub
    
ErrorOpenFile:
    g_err.HandleError "OpenSettingsFile"
    sfile = ""
End Sub


Public Sub SaveSettingsToFile(hwnd As Long, sfile As String)
    ' retrieves a user file name for saving settings
    g_err.AddToStack "SaveSettingsToFile"
    Dim filePath As String
    Dim frmType As String
    Dim DefaultExt As String
    Dim filter As String
    Dim Cancel As Boolean
    Dim position As Long
    Dim titleExtension As String
    Dim FilterIndex As Long

    On Error Resume Next
    
    DefaultExt = "scp"

    If (dir(filePath) = "") Or (Len(filePath) < 1) Then
        ' make a file path add the directory separator if needed
        If Right$(CurDir, 1) = "\" Then
            filePath = CurDir & DEF_DATA_FILE_NAME
        Else
            filePath = CurDir & "\" & DEF_DATA_FILE_NAME
        End If
    End If

    ' remove the extension if included
    position = InStr(1, filePath, ".")
    If position Then
        titleExtension = Right$(filePath, Len(filePath) - position + 1)
        filePath = Left$(filePath, position - 1)
    End If

    filter = FILE_FILTER_SETTINGS_SAVE

    ' remove the extension if included
    position = InStr(1, filePath, ".")
    If position Then
        filePath = Left$(filePath, position - 1)
    End If

    ' call the common dialog from system (not the control)
    ' get the user file name
    sfile = SaveAsDlg(hwnd, filter, filePath, _
            DefaultExt, Cancel, FilterIndex)

End Sub


Public Sub getLearnStringFromFile(filePath As String)
    ' Get and array of bytes for the learn string
    Dim LearnString() As Byte
    Dim index As Long
    Dim i As Long
    Dim S As CSetupFile

    Set S = New CSetupFile

    Dim SendString() As Byte

    If S.IsACompatibleFile(filePath) And _
         InStr(1, S.Model, VALID_INSTRUMENT_MASK, vbTextCompare) Then
        
        With S
            .ReadFile filePath
            LearnString = .LearnString
        End With

        If (g_Instrument Is Nothing) Then
            g_err.HandleError "isConnected, Error ", ErrorMsg_UserMsg, ERR_NoConnection
        Else
            index = UBound(LearnString)
            ReDim SendString(index + 9)

            SendString(0) = Asc("S")
            SendString(1) = Asc("y")
            SendString(2) = Asc("s")
            SendString(3) = Asc("t")
            SendString(4) = Asc(":")
            SendString(5) = Asc("S")
            SendString(6) = Asc("e")
            SendString(7) = Asc("t")
            SendString(8) = Asc(" ")

            For i = 0 To index
                SendString(i + 9) = LearnString(i)

            Next i

            With g_Instrument
                .WriteBytes UBound(SendString) + 1, SendString
            End With

        End If

    Else
        MsgBox "Not a compatible file"

    End If

    Set S = Nothing

End Sub

Public Sub SaveLearnStringToFile(filePath As String)
    ' Get and array of bytes for the learn string
    Dim reply(4) As String
    Dim LearnString() As Byte
    Dim index As Long
    Dim ModelNumber As String
    Dim S As CSetupFile
    Dim i As Long
    
   Set S = New CSetupFile
    
    index = 500
    ReDim LearnString(index)
    
    If (g_Instrument Is Nothing) Then

        g_err.HandleError "isConnected, Error ", ErrorMsg_UserMsg, ERR_NoConnection
    Else
    
        With g_Instrument
            .Output "*idn?"
            .Enter reply
            ModelNumber = reply(1)
        
            .ReadTerminator = -1
            .Output "*LRN?"
            
            .timeout = 10000
            .ReadBytes index, LearnString
            
            ReDim Preserve LearnString(index - 2)
            
            S.LearnString = LearnString
        End With
        
        With S
            .Model = reply(1)
            .Manufacturer = reply(0)
            .Version = reply(3)
            .WriteFile filePath
        End With
        
    End If
    
    Set S = Nothing
    
End Sub

