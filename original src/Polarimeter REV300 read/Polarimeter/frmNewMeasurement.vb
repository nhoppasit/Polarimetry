Public Class frmNewMeasurement

    Dim mSampleName As String
    Dim mAverageNumber As Integer
    Dim mNumberOfRepeatation As Integer

    ReadOnly Property SampleName() As String
        Get
            Return mSampleName
        End Get
    End Property

    ReadOnly Property RepeatNumber() As Integer
        Get
            Return mAverageNumber
        End Get
    End Property

    ReadOnly Property NumberOfRepeatation() As Integer
        Get
            Return mNumberOfRepeatation
        End Get
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        mSampleName = txtSampleName.Text
        mAverageNumber = numAverageNumber.Value
        mNumberOfRepeatation = numRepeatation.Value
        Close()
    End Sub

    Public Function Verify() As Boolean
        If txtSampleName.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

End Class