Public Class frmColorTable

    Private Sub frmColorTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadColorTable()
    End Sub

    Private Sub LoadColorTable()
        lvColorTable.Items.Clear()
        Dim LVI As ListViewItem
        LVI = New ListViewItem
        LVI.UseItemStyleForSubItems = False
        LVI.Text = "Reference"
        LVI.SubItems.Add("")
        LVI.SubItems.Add(frmMain.ReferenceColor.ToString)
        LVI.SubItems(1).BackColor = frmMain.ReferenceColor
        lvColorTable.Items.Add(LVI)
        For i As Integer = 0 To 19
            LVI = New ListViewItem
            LVI.UseItemStyleForSubItems = False
            LVI.Text = "Sample " & (i + 1).ToString
            LVI.SubItems.Add("")
            LVI.SubItems.Add(frmMain.ColorTable(i).ToString)
            LVI.SubItems(1).BackColor = frmMain.ColorTable(i)
            lvColorTable.Items.Add(LVI)
        Next
    End Sub

    Private Sub lvColorTable_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvColorTable.MouseDoubleClick
        Try
            Dim i As Integer = lvColorTable.SelectedIndices(0)
            Dim dlg As New ColorDialog
            dlg.Color = lvColorTable.SelectedItems(0).SubItems(1).BackColor
            dlg.ShowDialog()
            If i = 0 Then
                frmMain.ReferenceColor = dlg.Color
            Else
                frmMain.ColorTable(i - 1) = dlg.Color
            End If
            lvColorTable.SelectedItems(0).SubItems(1).BackColor = dlg.Color
            lvColorTable.SelectedItems(0).SubItems(2).Text = dlg.Color.ToString
            Select Case i
                Case 0
                    My.Settings.ReferenceColor = dlg.Color
                Case 1
                    My.Settings.Color1 = dlg.Color
                Case 2
                    My.Settings.Color2 = dlg.Color
                Case 3
                    My.Settings.Color3 = dlg.Color
                Case 4
                    My.Settings.Color4 = dlg.Color
                Case 5
                    My.Settings.Color5 = dlg.Color
                Case 6
                    My.Settings.Color6 = dlg.Color
                Case 7
                    My.Settings.Color7 = dlg.Color
                Case 8
                    My.Settings.Color8 = dlg.Color
                Case 9
                    My.Settings.Color9 = dlg.Color
                Case 10
                    My.Settings.Color10 = dlg.Color
                Case 11
                    My.Settings.Color11 = dlg.Color
                Case 12
                    My.Settings.Color12 = dlg.Color
                Case 13
                    My.Settings.Color13 = dlg.Color
                Case 14
                    My.Settings.Color14 = dlg.Color
                Case 15
                    My.Settings.Color15 = dlg.Color
                Case 16
                    My.Settings.Color16 = dlg.Color
                Case 17
                    My.Settings.Color17 = dlg.Color
                Case 18
                    My.Settings.Color18 = dlg.Color
                Case 19
                    My.Settings.Color19 = dlg.Color
                Case 20
                    My.Settings.Color20 = dlg.Color
            End Select
            My.Settings.Save()
            frmMain.ApplyColorTableToSamples()
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class