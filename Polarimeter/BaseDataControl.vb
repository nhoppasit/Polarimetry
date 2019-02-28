Imports System.IO
Imports System.Text

Public Class BaseDataControl

#Region "Members"

    Structure strucCurveData
        Dim X() As Double
        Dim Y() As Double
        Dim Xm As Double
        Dim Ym As Double
        Dim AngleOfRotation As Double
    End Structure

    Dim mSampleName As String
    Dim mSpecificRotation As Double
    Public Reference As strucCurveData
    Public DataCollection As New Collection
    Public Data() As strucCurveData

#End Region

#Region "Properties"

    Property SampleName() As String
        Get
            Return mSampleName
        End Get
        Set(ByVal value As String)
            mSampleName = value
        End Set
    End Property

#End Region

#Region "Functions/Method"

    'กำหนดค่าต่างๆของ Reference
    Public Sub PatchReference(ByVal PointID As Integer, ByVal X As Double, ByVal Y As Double)
        ReDim Preserve Reference.X(0 To PointID)    'กำหนดขนาดของแกน x เป็นค่าที่เยอะมากๆๆ
        ReDim Preserve Reference.Y(0 To PointID)    'กำหนดขนาดของแกน y เป็นค่าที่เยอะมากๆ
        Reference.X(PointID) = X    'ค่า x ของ reference
        Reference.Y(PointID) = Y    'ค่า y ของ reference
        If Y < Reference.Ym Then    'ถ้า y น้อยกว่า ym ของ reference 
            Reference.Ym = Y    'ym ของ reference จะเท่ากับ y แล้วมันไปแสดงในกราฟ
            Reference.Xm = X    'xm ของ reference จะเท่ากับ x 
        End If
    End Sub

    'กำหนดค่าต่างๆของข้อมูลการทดลอง
    Public Sub PatchData(ByVal RepeatID As Integer, ByVal PointID As Integer, ByVal X As Double, ByVal Y As Double)
        'zero base, careful!!!
        If Data Is Nothing Then     'ถ้ายังไม่ทดลอง
            ReDim Preserve Data(0 To RepeatID)
            Data(RepeatID).Ym = 999999      'สมมุติว่า ym ของการทดลอง=999999
        Else
            If Data.Length - 1 < RepeatID Then      'ถ้ามีค่าการทดลอง-1 จะน้อยกว่า repeatID
                ReDim Preserve Data(0 To RepeatID)
                Data(RepeatID).Ym = 999999      'สมมุติว่า ym ของการทดลอง=999999
            End If
        End If

        ReDim Preserve Data(RepeatID).X(0 To PointID)
        ReDim Preserve Data(RepeatID).Y(0 To PointID)

        Data(RepeatID).X(PointID) = X
        Data(RepeatID).Y(PointID) = Y

        If Y < Data(RepeatID).Ym Then      'ถ้า y น้อยกว่า ym ของการทดลอง
            Data(RepeatID).Ym = Y       'ym จะเท่ากับ y คือ จุดต่ำสุดของ curve
            Data(RepeatID).Xm = X       'xm จะเท่ากับ x 
            AnalyzeData(RepeatID)       'จะได้มุมอนาไรซ์เซอร์ของการทดลอง
        End If

    End Sub

    Public Sub PatchData2(ByVal RepeatID As Integer, ByVal PointID As Integer, ByVal X As Double, ByVal Y As Double)

        Dim _Data As New strucCurveData
        _Data.Ym = 999999

        ReDim Preserve _Data.X(0 To PointID)
        ReDim Preserve _Data.Y(0 To PointID)

        _Data.X(PointID) = X
        _Data.Y(PointID) = Y

        If Y < _Data.Ym Then
            _Data.Ym = Y
            _Data.Xm = X
            AnalyzeData2(RepeatID)
        End If

        If DataCollection.Contains(RepeatID.ToString) Then DataCollection.Remove(RepeatID.ToString)
        DataCollection.Add(_Data, RepeatID.ToString)

    End Sub

    Public Sub DeleteData(ByVal RepeatID As Integer)

    End Sub

    Public Function SaveFile() As Boolean

        Dim dlg As New SaveFileDialog
        dlg.Filter = "Text File (*.txt)|*.txt|CSV File (*.csv)|*.csv|Polarimeter File (*.pom)|*.pom|All File (*.*)|*.*"
        Dim redlg As DialogResult = dlg.ShowDialog()
        If redlg <> Windows.Forms.DialogResult.OK Then Exit Function

        '----------Save---------------------

        Dim path As String = dlg.FileName

        ' Delete the file if it exists.
        If File.Exists(path) Then
            File.Delete(path)
        End If

        'Create the file.
        Dim fs As FileStream = File.Create(path)

        'Start header
        AddText(fs, "Polarimeter Data File") 'Intro
        AddText(fs, "(Copy Right)2011, CIID, KMITL" & Environment.NewLine)
        AddText(fs, Now.ToString() & Environment.NewLine) 'Date-time

        'Start body
        AddText(fs, "[Sample Name]" & Environment.NewLine) 'Sample name
        AddText(fs, mSampleName)
        AddText(fs, "[Specific Rotation]" & Environment.NewLine) 'Specific rotation
        AddText(fs, mSpecificRotation)
        AddText(fs, "[Samples]" & Environment.NewLine) 'Number of samples
        AddText(fs, Data.Length.ToString)

        'Reference
        AddText(fs, "[Reference]")
        For i As Integer = 0 To Reference.X.Length - 1
            AddText(fs, Reference.X(i).ToString & "," & Reference.Y(i).ToString & Environment.NewLine)
        Next

        'Data
        For k As Integer = 0 To Data.Length - 1
            AddText(fs, "[Sample " & (k + 1).ToString & "]")
            For i As Integer = 0 To Data(k).X.Length - 1
                AddText(fs, Data(k).X(i).ToString & "," & Data(k).Y(i).ToString & Environment.NewLine)
            Next
        Next

        'Ending
        fs.Close()

        '----------Save---------------------
    End Function

    Public Sub OpenFile()
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Text File (*.txt)|*.txt"
        Dim redlg As DialogResult = dlg.ShowDialog()
        If redlg <> Windows.Forms.DialogResult.OK Then Exit Sub
        '----------Open---------------------

        '----------Open---------------------
    End Sub

    Private Sub AddText(ByVal fs As FileStream, ByVal value As String)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(value)
        fs.Write(info, 0, info.Length)
    End Sub

    'หาค่าของแผ่นอนาไรซ์
    Private Sub AnalyzeData(ByVal RepeatID As Integer)
        Try
            If Reference.X Is Nothing Then Exit Sub
            If Data(RepeatID).X Is Nothing Then Exit Sub
            Data(RepeatID).AngleOfRotation = Math.Abs(Data(RepeatID).Xm - Reference.Xm)     'มุมของการหมุนในการทดลอง = |xm ของการทดลอง - xm ของ reference|
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub AnalyzeData2(ByVal RepeatID As Integer)
        Try
            If Reference.X Is Nothing Then Exit Sub
            Dim _Data As strucCurveData = DataCollection(RepeatID.ToString)
            If _Data.X Is Nothing Then Exit Sub
            _Data.AngleOfRotation = Math.Abs(_Data.Xm - Reference.Xm)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    'ตำแหน่งของแผ่นอนาไรซ์
    Private Sub AnalyzeAllData()
        Dim i As Integer = 0
        For Each d As strucCurveData In Data
            If d.X IsNot Nothing Then
                AnalyzeData(i)      'มุมของแผ่นอนาไรซ์ที่ตำแหน่ง i โดย i=0
            End If
            i += 1      'i = i+1 ดังนั้นค่า i จะเพิ่มขึ้นเรื่อยๆ ส่งผลให้มุมของแผ่นอนาไรซ์เปลี่ยนไปเรื่อยๆตามตำแหน่ง จาก0,1,2,...
        Next
    End Sub

#End Region

#Region "New"

    Public Sub New()
        Reference.Ym = 9999999
    End Sub

#End Region

End Class
