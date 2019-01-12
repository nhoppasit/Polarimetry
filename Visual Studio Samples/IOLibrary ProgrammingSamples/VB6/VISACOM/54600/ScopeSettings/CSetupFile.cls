VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CSetupFile"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'
' The CSetupFile.cls file defines a interface used to support saving and
' reading setup files in the EMD standard format
'
' Revision History:
'
' Revision    Date   Developer      Comments
'   1.0     5/28/99   DAV     Initial release
'   2.0     2/20/01   WOH     Modified for use with Data Capture
'   3.0     11/14/02  WOH     Modified for use with VISA COM
Option Explicit
'
' constants
'
Private Const FILE_NAME = "CSetupFile"
Const FILE_EXTENSION = "scp"
Const FILE_VERSION = "1"
Const FILE_COMMENT = "Instrument Standard Setup File, Version: " & FILE_VERSION
Const FIELD_SEPARATOR = "|"
'
Const DEFAULT_MANUFACTURER = "Agilent Technologies"
'
' member variables
'
Dim m_strModel As String
Dim m_strManufacturer As String
Dim m_strVersion As String
Dim m_strUserComments As String
Dim m_strSerialNumber As String
Dim m_strReservedField1 As String
Dim m_strReservedField2 As String
Dim m_strReservedField3 As String
Dim m_vLearnString As Variant
Dim m_lLearnStringLength As Long


Public Sub Class_Initialize()
'
' The sub Class_Initialize initaizes the CSetupClass member variables
'
    m_lLearnStringLength = 0
    m_vLearnString = Empty
    m_strModel = ""
    m_strManufacturer = DEFAULT_MANUFACTURER
    m_strVersion = ""
    m_strSerialNumber = ""
    m_strUserComments = ""
    m_strReservedField1 = ""
    m_strReservedField2 = ""
    m_strReservedField3 = ""
End Sub

Public Function IsACompatibleFile(ByVal strPath As String) As Boolean
'
' The function  IsACompatibleFile reads the setup file to the file
' version and returns true if their are no errors and the extension and
' file version match
'
    On Error GoTo ErrorHandler
    '
    ' open the new file
    '
    Close #1
    Open strPath For Binary Access Read As #1
    On Error GoTo ErrorHandler
    '
    ' read the file
    '
    Dim lOffset As Long
    Dim strOffset As String
    Dim strFileExtension As String
    Dim strFileVersion As String
    '
    ReadFileTypeAndVersion lOffset, strOffset, strFileExtension, strFileVersion
    '
    ' check to see if we have the same model,manufacturer and firmware version
    '
    strFileExtension = UCase(strFileExtension)
    strFileVersion = UCase(strFileVersion)
    Dim strCorrectFileExtension As String
    Dim strCorrectFileVersion As String
    strCorrectFileExtension = UCase(FILE_EXTENSION)
    strCorrectFileVersion = UCase(FILE_VERSION)
    If ((strFileExtension = strCorrectFileExtension) _
        And (strFileVersion = strCorrectFileVersion)) Then
         IsACompatibleFile = True
    Else
         IsACompatibleFile = False
    End If
    Close #1
Exit Function
ErrorHandler:
    '
    ' any error causes us to return false
    '
    Close #1
     IsACompatibleFile = False
End Function

Public Property Let LearnString(ByVal vLearnString As Variant)
'
' the property let  LearnString sets the current learn string
'
    m_vLearnString = vLearnString
End Property

Public Property Get LearnString() As Variant
'
' the property get  LearnString gets the current learn string
'
     LearnString = m_vLearnString
End Property

Public Property Let Manufacturer(ByVal strManufacturer As String)
'
' the property let  Manufacturer sets the current manufacturer
'
    m_strManufacturer = strManufacturer
End Property

Public Property Get Manufacturer() As String
'
' the property get  Manufacturer gets the current manufacturer
'
     Manufacturer = m_strManufacturer
End Property

Public Property Let Model(ByVal strModel As String)
'
' the property let  Model sets the current model
'
    m_strModel = strModel
End Property

Public Property Get Model() As String
'
' the property let  Model sets the current model
'
     Model = m_strModel
End Property

Public Sub ReadFile(ByVal strPath As String)
'
' The sub  ReadFile reads the given EMD setup file
'
    Dim strSource As String
    Dim strMessage As String
    Dim strChar As String
    Dim strTemp As String
    '
    ' open the new file
    '
    Close #1
    Open strPath For Binary Access Read As #1
    On Error GoTo ErrorHandler
    '
    ' read the first part of the file
    '
    Dim lOffset As Long
    Dim strFileExtension As String
    Dim strFileVersion As String
    Dim strOffset As String
    '
    lOffset = 0
    ReadFileTypeAndVersion lOffset, strOffset, strFileExtension, _
        strFileVersion
    '
    ' Model, Manufacturer, Serial and Version Number
    '
    m_strModel = ReadField(lOffset)
    m_strManufacturer = ReadField(lOffset)
    m_strSerialNumber = ReadField(lOffset)
    m_strVersion = ReadField(lOffset)
    '
    ' User Comment and Code Comment
    '
    m_strUserComments = ReadField(lOffset)
    Dim strFileComments As String
    strFileComments = ReadField(lOffset)
    '
    ' Reserved Fields
    '
    m_strReservedField1 = ReadField(lOffset)
    m_strReservedField2 = ReadField(lOffset)
    m_strReservedField3 = ReadField(lOffset)
    '
    ' length of settings field
    '
    Dim strSettingsFieldLength As String
    Dim lSettingsFieldLength As Long
    strSettingsFieldLength = ReadField(lOffset)
    lSettingsFieldLength = CLng(strSettingsFieldLength)
    If (lSettingsFieldLength < 0) Then
        '
        ' error condition
        '
        ' debug.Assert False, "false", _
            FILE_NAME, "ReadFile", "1"
        GoTo ErrorHandler
    End If
    '
    ' see if we are in the correct location in the file
    '
    If (lOffset <> CLng(strOffset)) Then
        '
        ' error condition
        '
        ' debug.Assert False, "false", _
            FILE_NAME, "ReadFile", "2"
        GoTo ErrorHandler
    End If
    '
    ' learn string
    '
    Dim byteArray() As Byte
    byteArray = InputB(lSettingsFieldLength, #1)
    If (UBound(byteArray, 1) - LBound(byteArray, 1) > 0) Then
        m_vLearnString = byteArray
    Else
        m_vLearnString = Empty
    End If
    
    Close #1
    Exit Sub
ErrorHandler:
    Close #1
   ' strSource = LoadResStringWrapper(IDS_OPEN_SETUPS + g_lResourceOffset)
    'strMessage = LoadResStringWrapper(IDS_OPEN_SETUP_FILE_FORMAT_ERROR + g_lResourceOffset)
    
   ' Err.Raise ERROR_LOADING_FROM_FILE, strSource, strMessage
   Debug.Print "error in ReadFile " & Err.Description
End Sub

Public Property Let ReservedField1(ByVal strReservedField1 As String)
'
' the property let  ReservedField1 sets reserved field 1
'
    m_strReservedField1 = strReservedField1
End Property

Public Property Get ReservedField1() As String
'
' the property get  ReservedField1 gets reserved field 1
'
     ReservedField1 = m_strReservedField1
End Property

Public Property Let ReservedField2(ByVal strReservedField2 As String)
'
' the property let  ReservedField2 sets reserved field 2
'
    m_strReservedField2 = strReservedField2
End Property

Public Property Get ReservedField2() As String
'
' the property get  ReservedField2 gets reserved field 2
'
     ReservedField2 = m_strReservedField2
End Property

Public Property Let ReservedField3(ByVal strReservedField3 As String)
'
' the property let  ReservedField3 sets reserved field 3
'
    m_strReservedField3 = strReservedField3
End Property

Public Property Get ReservedField3() As String
'
' the property get  ReservedField3 gets reserved field 3
'
     ReservedField3 = m_strReservedField3
End Property

Public Property Let SerialNumber(ByVal strSerialNumber As String)
'
' the property let  SerialNumber sets the serial number
'
    m_strSerialNumber = strSerialNumber
End Property

Public Property Get SerialNumber() As String
'
' the property get  SerialNumber gets the serial number
'
     SerialNumber = m_strSerialNumber
End Property

Public Property Let UserComments(ByVal strUserComments As String)
'
' the property let  SerialNumber sets the user comments
'
    m_strUserComments = strUserComments
End Property

Public Property Get UserComments() As String
'
' the property get  UserComments gets the user comments
'
     UserComments = m_strUserComments
End Property

Public Property Let Version(ByVal strVersion As String)
'
' the property let  Version sets the version number
'
    m_strVersion = strVersion
End Property

Public Property Get Version() As String
'
' the property get  Version gets the version number
'
     Version = m_strVersion
End Property

Public Sub WriteFile(ByVal strPath As String)
'
' The function ISetupFile writes the objects data out in EMD standard file
' format to the given path
'
    Dim strSource As String
    Dim strMessage As String
    '
    ' caculate the offset to the learn string
    '
    Dim lOffset As Long
    Dim strOffset
    Dim lSettingsFieldLength As Long
    Dim strSettingsFieldLength As String
    '
    lOffset = Len(FILE_EXTENSION) + 1
    lOffset = lOffset + FILE_VERSION + 1
    lOffset = lOffset + Len(m_strModel) + 1
    lOffset = lOffset + Len(m_strManufacturer) + 1
    lOffset = lOffset + Len(m_strSerialNumber) + 1
    lOffset = lOffset + Len(m_strVersion) + 1
    lOffset = lOffset + Len(m_strUserComments) + 1
    lOffset = lOffset + Len(FILE_COMMENT) + 1
    lOffset = lOffset + Len(m_strReservedField1) + 1
    lOffset = lOffset + Len(m_strReservedField2) + 1
    lOffset = lOffset + Len(m_strReservedField3) + 1
    '
    ' calculate the settings field length
    '
    If (IsEmpty(m_vLearnString)) Then
        lSettingsFieldLength = 0
    Else
        lSettingsFieldLength = UBound(m_vLearnString) - LBound(m_vLearnString) + 1
    End If
    strSettingsFieldLength = CStr(lSettingsFieldLength)
    lOffset = lOffset + Len(strSettingsFieldLength) + 1
    '
    ' calculate the total offset with the offset field
    '
    strOffset = CStr(lOffset)
    lOffset = lOffset + Len(strOffset) + 1
    If (Len(CStr(lOffset)) = Len(strOffset)) Then
        '
        ' no carry
        '
        strOffset = CStr(lOffset)
    ElseIf (Len(CStr(lOffset)) = Len(strOffset) + 1) Then
        '
        ' carry
        '
        strOffset = CStr(lOffset) + 1
    Else
        '
        ' we shouldn't get here
        '
        ' debug.Assert   False, "false",            FILE_NAME , "WriteFile", "1"
        GoTo ErrorHandler
    End If
    '
    ' open the new file
    '
    Close #1
    Open strPath For Output Access Write Lock Write As #1
 '   Open strPath For Binary Access Write Lock Write As #1
    On Error GoTo ErrorHandler
    '
    ' location field
    '
    
    Print #1, strOffset & FIELD_SEPARATOR;
    '
    ' file type ID and format version
    '
    Print #1, FILE_EXTENSION & FIELD_SEPARATOR _
        & FILE_VERSION & FIELD_SEPARATOR;
    '
    ' Model, Manufacturer, Serial and Version Number
    '
    Print #1, m_strModel & FIELD_SEPARATOR _
        & m_strManufacturer & FIELD_SEPARATOR _
        & m_strSerialNumber & FIELD_SEPARATOR _
        & m_strVersion & FIELD_SEPARATOR;
    '
    ' User Comment and Code Comment
    '
    Print #1, m_strUserComments & FIELD_SEPARATOR _
        & FILE_COMMENT & FIELD_SEPARATOR;
    '
    ' Reserved Fields
    '
    Print #1, m_strReservedField1 & FIELD_SEPARATOR _
        & m_strReservedField2 & FIELD_SEPARATOR _
        & m_strReservedField3 & FIELD_SEPARATOR;
    '
    ' length of settings field
    '
    Print #1, strSettingsFieldLength & FIELD_SEPARATOR;
    Dim lRecord As Long
    lRecord = Seek(1)
    '
    ' learn string
    '
    Close #1
    If (lSettingsFieldLength > 0) Then
      Open strPath For Binary Access Write Lock Write As #1
      Dim abyteLearnString() As Byte
      abyteLearnString = m_vLearnString
      Put #1, lRecord, abyteLearnString
      
      Close #1
    End If
    Exit Sub
ErrorHandler:
    Close #1
    Debug.Print "error in WriteFile,  " & Err.Description
     'debug.Assert False, "false", _
        FILE_NAME, "WriteFile", "ErrorHandler"
    'strSource = LoadResStringWrapper(IDS_OPEN_SETUPS + g_lResourceOffset)
    'strMessage = LoadResStringWrapper(IDS_SAVE_SETUP_FILE_FORMAT_ERROR + g_lResourceOffset)
    
    'Err.Raise ERROR_LOADING_FROM_FILE, strSource, strMessage
    
End Sub

Private Function ReadField(ByRef lCount As Long, Optional ByVal lMaxCount As Long = 0) As String
'
' the function ReadField reads the next field from  the setup file.
'
    '
    ' read till the next separator
    '
    Dim strChar As String
    Dim strTemp As String
    Dim strSource As String
    Dim strMessage As String
    '
    ' check for end of file
    '
    If (EOF(1)) Then
         'debug.Assert False, "false", _
            FILE_NAME, "ReadField", "1"
        'strSource = LoadResStringWrapper(IDS_OPEN_SETUPS + g_lResourceOffset)
        'strMessage = LoadResStringWrapper(IDS_OPEN_SETUP_FILE_FORMAT_ERROR + g_lResourceOffset)
       ' Err.Raise ERROR_LOADING_FROM_FILE, strSource, strMessage
    End If
    '
    ' read next character
    '
    Dim lStartingCount As Long
    lStartingCount = lCount
    strChar = Input(1, #1)
    lCount = lCount + 1
    While (strChar <> FIELD_SEPARATOR)
        strTemp = strTemp & strChar
        '
        ' check for end of file, or beyond maximum count
        '
        If (EOF(1) Or ((Not IsMissing(lMaxCount)) And (lMaxCount > 0) And (lCount > lStartingCount + lMaxCount))) Then
            'strSource = LoadResStringWrapper(IDS_OPEN_SETUPS + g_lResourceOffset)
            'strMessage = LoadResStringWrapper(IDS_OPEN_SETUP_FILE_FORMAT_ERROR + g_lResourceOffset)
            'Err.Raise ERROR_LOADING_FROM_FILE, strSource, strMessage
        End If
        '
        ' read next character
        '
        strChar = Input(1, #1)
        lCount = lCount + 1
    Wend
    '
    ' return the result
    '
    ReadField = strTemp
End Function
Private Sub ReadFileTypeAndVersion(ByRef lOffset As Long, _
    ByRef strOffset As String, ByRef strFileExtension As String, _
    ByRef strFileVersion As String)
'
' The sub ReadFileTypeAndVersion reads the given EMD setup file extension and
' version. It is assumed the file has been
' opened with index 1
'
    Dim strSource As String
    Dim strMessage As String
    Dim strChar As String
    Dim strTemp As String
    '
    ' location field
    '
    lOffset = 0
    strOffset = ReadField(lOffset)
    '
    ' file type ID and format version
    '
    strFileExtension = ReadField(lOffset, Len(FILE_EXTENSION) + 1)
    '
    ' check for valid extension
    '
    If (UCase(strFileExtension) <> UCase(FILE_EXTENSION)) Then
        '
        ' error condition
        '
        ' debug.Assert False, "false", _
            FILE_NAME, "ReadFileTypeAndVersion", "1"
        GoTo ErrorHandler
    End If
    strFileVersion = ReadField(lOffset)
    '
    ' check for valid version
    '
    If (strFileVersion <> FILE_VERSION) Then
        '
        ' error condition
        '
        ' debug.Assert False, "false", _
            FILE_NAME, "ReadFileTypeAndVersion", "2"
        GoTo ErrorHandler
    End If
    '
    Exit Sub
ErrorHandler:
    '
    ' don't close file handle
    '
   ' strSource = LoadResStringWrapper(IDS_OPEN_SETUPS + g_lResourceOffset)
   ' strMessage = LoadResStringWrapper(IDS_SAVE_SETUP_FILE_FORMAT_ERROR + g_lResourceOffset)
    
   ' Err.Raise ERROR_LOADING_FROM_FILE, strSource, strMessage
    
End Sub
