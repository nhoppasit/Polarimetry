Imports System
Imports System.IO
Imports System.Text

Public Class frmMain

#Region "DECRALATION"

    'CONSTANTS
    Const StepFactor = 0.013325 'Deg/Step

    'Dynaplot objects
    'Dim ReferenceCurve As DYNAPLOT3Lib.Curve
    'Dim TreatmentCurve() As DYNAPLOT3Lib.Curve
    'Dim TreatmentMinMarker As DYNAPLOT3Lib.Marker
    'Dim ReferenceMinMarker As DYNAPLOT3Lib.Marker

    'SCANING & Data
    Dim TheData As BaseDataControl
    Dim IsScanning As Boolean = False
    Dim IsContinuing As Boolean = False
    Dim CurrentPointIndex As Integer = 0
    Dim SpecificRotation As Double
    Dim NumberOfRepeatation As Integer
    Dim SelectedIndex As Integer

    'COLOR TABLE
    Public ReferenceColor As Color = Color.Red
    Public ColorTable(0 To 19) As Color

#End Region

#Region "DEVICES"

    Private DMM As Ivi.Visa.Interop.IFormattedIO488
    Private MMC As Ivi.Visa.Interop.IFormattedIO488

    Private Sub DisconnectDevices()
        Try
            DMM.IO.Close()
            DMM.IO = Nothing
            MMC.IO.Close()
            MMC.IO = Nothing
            'MsgBox("Devices have been disconnected.")
            lblDMM.Text = "Disconnected"
            lblDMM.BackColor = Color.Red
            lblMMC.Text = "Disconncected"
            lblMMC.BackColor = Color.Red
        Catch ex As Exception
            MsgBox("IO Error: " & ex.Message, MsgBoxStyle.Critical)
            lblDMM.Text = "Disconnected"
            lblDMM.BackColor = Color.Red
            lblMMC.Text = "Disconncected"
            lblMMC.BackColor = Color.Red
        End Try
    End Sub

    Private Sub ConnectDevices()

        Try
            '-------------------------------------------
            'CONNECT DMM
            '-------------------------------------------
            Dim mgr1 As Ivi.Visa.Interop.ResourceManager
            Dim DMMAddress As String
            DMMAddress = txtDMMAddress.Text
            mgr1 = New Ivi.Visa.Interop.ResourceManager
            DMM = New Ivi.Visa.Interop.FormattedIO488
            DMM.IO() = mgr1.Open(DMMAddress)
            DMM.IO.Timeout = 7000
            DMM.WriteString("CONF:VOLT:DC " & txtVoltageRange.Text & ", " & txtVoltageResolution.Text)
            DMM.WriteString("TRIG:SOUR IMM")
            DMM.WriteString("TRIG:DEL 1.5E-3")
            DMM.WriteString("TRIG:COUNT 1")
            DMM.WriteString("SAMP:COUNT 1")

            '-------------------------------------------
            'CONNECT MMC
            '-------------------------------------------
            Dim mgr2 As Ivi.Visa.Interop.ResourceManager
            Dim MMCAddress As String
            MMCAddress = txtMMCAddress.Text
            mgr2 = New Ivi.Visa.Interop.ResourceManager
            MMC = New Ivi.Visa.Interop.FormattedIO488
            MMC.IO() = mgr2.Open(MMCAddress)
            MMC.IO.Timeout = 7000

            'MsgBox("Connect devices are successful.")
            lblDMM.Text = "Connected"
            lblDMM.BackColor = Color.Lime
            lblMMC.Text = "Conncected"
            lblMMC.BackColor = Color.Lime

        Catch ex As Exception
            MsgBox("InitIO Error:" & vbCrLf & ex.Message)
            lblDMM.Text = "Disconnected"
            lblDMM.BackColor = Color.Red
            lblMMC.Text = "Disconncected"
            lblMMC.BackColor = Color.Red
        End Try

    End Sub

#End Region

#Region "Control Panel"

    Private Sub DoStart()
        'Add curve
        Dim x(0) As Double
        Dim y(0) As Double

        ResetDynaplot()

        PlotReferenceCurve()

        '----------------------------------------
        '1. Update buttons
        '----------------------------------------
        btnStart.Enabled = False
        btnStop.Enabled = True
        btnPause.Enabled = True

        '----------------------------------------
        'disable box
        '----------------------------------------
        btnNewMeas.Enabled = False
        gbSample.Enabled = False
        gbScanCondition.Enabled = False

        '----------------------------------------
        '2. start Test loop of reading light intensity
        '----------------------------------------
        CurrentPointIndex = 0
        IsScanning = True
        lblMainStatus.Text = "Measuring..."

        DoScanLightIntensity()

        lblMainStatus.Text = "Ready"
    End Sub

    Private Sub DoStop()
        '----------------------------------------
        '1. stop Test loop of reading light intensity
        '----------------------------------------
        StopScanning()

        '----------------------------------------
        '2. Update buttons
        '----------------------------------------
        btnStart.Enabled = True
        btnStop.Enabled = False
        btnPause.Enabled = False
        btnPause.Text = "PAUSE"
    End Sub

    Private Sub DoPause()
        '----------------------------------------
        '1. pause/continue Test loop of reading light intensity
        '----------------------------------------
        If btnPause.Text = "PAUSE" Then
            DoPauseScanning()
        Else
            DoContinueScanning()
        End If

        '----------------------------------------
        '2. Update buttons
        '----------------------------------------
        btnStart.Enabled = False
        btnStop.Enabled = True
        btnPause.Enabled = True
        If btnPause.Text = "PAUSE" Then
            btnPause.Text = "CONTINUE"
        Else
            btnPause.Text = "PAUSE"
            DoScanLightIntensity()
        End If
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        DoStart()
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        DoStop()
    End Sub

    Private Sub btnPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPause.Click
        DoPause()
    End Sub

#End Region

#Region "Scanning Procedure"

    Private Sub DoScanLightIntensity()
        '--------------------------------------------
        'validate selected index of repeats
        '--------------------------------------------
        If lvSummary.SelectedItems.Count <= 0 Then
            MsgBox("Please select item in samples list view that you want to measure!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            btnStart.Enabled = True
            btnStop.Enabled = False
            btnPause.Enabled = False
            btnPause.Text = "PAUSE"
            btnNewMeas.Enabled = True
            gbSample.Enabled = True
            gbScanCondition.Enabled = True
            Exit Sub
        End If

        '--------------------------------------------
        'Confirmation
        '--------------------------------------------
        'If Not IsContinuing Then
        '    Dim trt As String
        '    If SelectedIndex = 0 Then
        '        trt = "Reference data"
        '    Else
        '        trt = "Sample " & SelectedIndex
        '    End If
        '    If MsgBox("Are you sure to measure " & trt & "?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
        '        btnStart.Enabled = True
        '        btnStop.Enabled = False
        '        btnPause.Enabled = False
        '        btnPause.Text = "PAUSE"
        '        btnNewMeas.Enabled = True
        '        gbSample.Enabled = True
        '        gbScanCondition.Enabled = True
        '        Exit Sub
        '    End If
        'End If

        If Not mnuOptionsDemomode.Checked Then
            ConnectDevices()
        End If

        Try
            '--------------------------------------------
            'get read conditions
            '--------------------------------------------
            Dim ThetaA As Double = CDbl(txtStart.Text)
            Dim ThetaB As Double = CDbl(txtStop.Text)
            Dim Delta As Double = CDbl(txtResolution.Text)

            '--------------------------------------------
            'initialize minimum finder
            '--------------------------------------------
            If Not IsContinuing Then
                If SelectedIndex = 0 Then
                    TheData.Reference.Ym = 99999999
                Else
                    If TheData.Data IsNot Nothing Then
                        If SelectedIndex <= TheData.Data.Length Then
                            TheData.Data(SelectedIndex - 1).Ym = 99999999
                        End If
                    End If
                End If
            End If

            '----------------------------------------------------------------
            'REAL INTERFACE YES OR NOT (Theta,I)
            '----------------------------------------------------------------
            Dim CurrentLightIntensity As Double
            Dim StepNumber As Integer
            Dim MSG As String
            Dim CurrentTheta As Double
            If ThetaA < ThetaB Then
                CurrentTheta = ThetaA + CurrentPointIndex * Delta
            ElseIf ThetaA > ThetaB Then
                CurrentTheta = ThetaA - CurrentPointIndex * Delta
            End If
            'check demo mode
            If mnuOptionsDemomode.Checked = False Then
                '0.4 GOTO Theta A
                StepNumber = -1 * CInt(CurrentTheta / StepFactor) 'step
                MSG = "A:WP" & StepNumber.ToString & "P" & StepNumber.ToString
                MMC.WriteString(MSG)

                '0.5 Read first
                Dim nAvg As Integer = numRepeatNumber.Value
                CurrentLightIntensity = 0
                For tt As Integer = 0 To nAvg - 1
                    DMM.WriteString("READ?")
                    CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber
                Next
                CurrentLightIntensity = CurrentLightIntensity / nAvg

            Else 'IN DEMO MODE
                CurrentLightIntensity = Rnd() * 0.1 + Math.Cos((CurrentTheta - Rnd() * 50) * Math.PI / 180) + 2
            End If

            '----------------------------------------------------------------
            'STORE DATA AND PLOT
            '----------------------------------------------------------------
            'Save to memory
            If SelectedIndex = 0 Then 'Blank
                TheData.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity)
            Else 'Treatments
                TheData.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity)
            End If
            DefineAngleOfRotation()
            PlotReferenceCurve()
            PlotTreatmentsCurve()
            PlotSelectedTRTMarker()

            'auto scale
            'AxDynaPlot1.Axes.Autoscale()

            '--------------------------------------------
            'MAIN READING LOOP (^0^)
            '--------------------------------------------
            While IsScanning

                Application.DoEvents()

                'Update current THETA
                If ThetaA < ThetaB Then
                    CurrentTheta = ThetaA + CurrentPointIndex * Delta
                ElseIf ThetaA > ThetaB Then
                    CurrentTheta = ThetaA - CurrentPointIndex * Delta
                End If

                '--------------------------------------------
                'CHECK DEMO MODE
                '--------------------------------------------
                If mnuOptionsDemomode.Checked = False Then

                    '--------------------------------------------
                    'REAL INTERFACING
                    '--------------------------------------------
                    '1. Move polarizer 
                    StepNumber = -1 * CInt(CurrentTheta / StepFactor) 'step
                    MSG = "A:WP" & StepNumber.ToString & "P" & StepNumber.ToString
                    MMC.WriteString(MSG)

                    '3. Read light intensity
                    Dim nAvg As Integer = numRepeatNumber.Value
                    CurrentLightIntensity = 0
                    For tt As Integer = 0 To nAvg - 1
                        DMM.WriteString("READ?")
                        CurrentLightIntensity = CurrentLightIntensity + DMM.ReadNumber
                    Next
                    CurrentLightIntensity = CurrentLightIntensity / nAvg

                Else

                    '--------------------------------------------
                    'DEMO MODE
                    '--------------------------------------------
                    'Delay.
                    'Dim sw As New Stopwatch
                    'sw.Start()
                    'Do
                    '    'do nothing
                    'Loop Until sw.ElapsedMilliseconds > 50 'ms
                    ''Simulation
                    CurrentLightIntensity = Rnd() * 0.1 + Math.Cos((CurrentTheta - Rnd() * 50) * Math.PI / 180) + 2

                End If

                'Save to memory and update curve
                If SelectedIndex = 0 Then 'Blank
                    TheData.PatchReference(CurrentPointIndex, CurrentTheta, CurrentLightIntensity)
                Else 'Treatments
                    TheData.PatchData(SelectedIndex - 1, CurrentPointIndex, CurrentTheta, CurrentLightIntensity)
                End If
                DefineAngleOfRotation()
                PlotReferenceCurve()
                PlotTreatmentsCurve()
                PlotSelectedTRTMarker()

                'auto scale
                'AxDynaPlot1.Axes.Autoscale()

                'check stop condition!!!
                If ThetaA < ThetaB Then
                    If ThetaB < CurrentTheta Then IsScanning = False
                ElseIf ThetaA > ThetaB Then
                    If CurrentTheta < ThetaB Then IsScanning = False
                End If
                '--------------------------------------------

                'next point
                CurrentPointIndex += 1

            End While
            '--------------------------------------------(^0^)

            'if stop update buttons to a new start
            If btnPause.Text <> "CONTINUE" Then
                If Not mnuOptionsDemomode.Checked Then
                    MSG = "A:WP" & CInt(-1 * ThetaA / StepFactor).ToString & "P" & CInt(-1 * ThetaA / StepFactor).ToString
                    MMC.WriteString(MSG)
                    DisconnectDevices()
                End If
                btnStart.Enabled = True
                btnStop.Enabled = False
                btnPause.Enabled = False
                btnPause.Text = "PAUSE"
                btnNewMeas.Enabled = True
                gbSample.Enabled = True
                gbScanCondition.Enabled = True
            Else 'if pause update buttons to continue
                If Not mnuOptionsDemomode.Checked Then DisconnectDevices()
                btnStart.Enabled = False
                btnStop.Enabled = True
                btnPause.Enabled = True
            End If

        Catch ex As Exception

            MsgBox(ex.Message)

            '----------------------------------------
            '1. stop Test loop of reading light intensity
            '----------------------------------------
            StopScanning()

            '----------------------------------------
            '2. Update buttons
            '----------------------------------------
            btnStart.Enabled = True
            btnStop.Enabled = False
            btnPause.Enabled = False
            btnPause.Text = "PAUSE"
            btnNewMeas.Enabled = True
            gbSample.Enabled = True
            gbScanCondition.Enabled = True

        End Try

    End Sub

    Private Sub StopScanning()
        IsScanning = False
        IsContinuing = False
    End Sub

    Private Sub DoPauseScanning()
        IsScanning = False
        IsContinuing = False
    End Sub

    Private Sub DoContinueScanning()
        IsScanning = True
        IsContinuing = True
    End Sub

#End Region

#Region "Menus"

#Region "File"

    Private Sub mnuFileNewMeasurement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileNewMeasurement.Click
        NewMeasurement()
    End Sub

    Private Sub mnuFileLoadMC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub mnuFileSaveDataAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSaveDataAs.Click
        Try
            If TheData.SaveFile() Then MsgBox("The data have been save.")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuFileSaveMC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub mnuSaveMCAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub mnuFileOpendata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileOpendata.Click

    End Sub

    Private Sub mnuFileSaveData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSaveData.Click
        TheData.SaveFile()
    End Sub

    Private Sub mnuFileQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileQuit.Click
        Close()
    End Sub

#End Region


    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        ConnectDevices()
    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        DisconnectDevices()
    End Sub

#End Region

#Region "Form Event"

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        LoadSetting()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("Do you want to quit program?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Quit") = MsgBoxResult.Yes Then
            IsScanning = False
            SaveSetting()
        Else
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "PLOT when select curve"

    Private Sub ResetDynaplot()
        Try
            Dim x(0) As Double
            Dim y(0) As Double

            'AxDynaPlot1.DataCurves.RemoveAll()
            'AxDynaPlot1.Markers.RemoveAll()

            'ReferenceCurve = AxDynaPlot1.DataCurves.Add("REF", x, y, 0, False).Curve
            'ReferenceCurve.Penstyle.MaxWidth = 2
            'ReferenceCurve.Penstyle.Color = RGB(255, 0, 0)
            'ReferenceMinMarker = AxDynaPlot1.Markers.Add(0.0, 0.0, 0, DYNAPLOT3Lib.dpsMARKERTYPE.dpsMARKER_CIRCLE)

            For i As Integer = 0 To NumberOfRepeatation - 1
                'TreatmentCurve(i) = AxDynaPlot1.DataCurves.Add("TRT" & i.ToString, x, y, 0, False).Curve
                'TreatmentCurve(i).Penstyle.MaxWidth = 2
                'TreatmentCurve(i).Penstyle.Color = RGB(0, 0, 255)
            Next
            'TreatmentMinMarker = AxDynaPlot1.Markers.Add(0.0, 0.0, 0, DYNAPLOT3Lib.dpsMARKERTYPE.dpsMARKER_SQUARE)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Function PlotReferenceCurve() As Boolean
        Dim e As Boolean = False
        Try
            If lvSummary.Items(0).Checked = True Then
                If TheData.Reference.X IsNot Nothing Then
                    'ReferenceCurve.UpdateData(TheData.Reference.X, TheData.Reference.Y, TheData.Reference.X.Length)
                    'ReferenceCurve.Penstyle.Color = RGB(ReferenceColor.R, ReferenceColor.G, ReferenceColor.B)
                    'ReferenceMinMarker.PositionX = TheData.Reference.Xm
                    'ReferenceMinMarker.PositionY = TheData.Reference.Ym
                    lblNullPoint.Text = TheData.Reference.Xm.ToString("0.0000") & " deg"
                    e = True
                End If
            End If
        Catch ex As Exception
            'do nothing
        End Try
        Return e
    End Function

    Private Function PlotTreatmentsCurve() As Boolean
        If TheData Is Nothing Then Return False
        If TheData.Data Is Nothing Then Return False
        For i As Integer = 0 To NumberOfRepeatation - 1
            Try
                If lvSummary.Items(i + 1).Checked Then
                    'TreatmentCurve(i).UpdateData(TheData.Data(i).X, _
                    '                          TheData.Data(i).Y, _
                    '                          TheData.Data(i).X.Length)
                    'TreatmentCurve(i).Penstyle.Color = RGB( _
                    '    ColorTable(i Mod ColorTable.Length).R, _
                    '    ColorTable(i Mod ColorTable.Length).G, _
                    '    ColorTable(i Mod ColorTable.Length).B)
                End If
            Catch ex As Exception
                Err.Clear()
            End Try
        Next
        Return True
    End Function

    Private Sub PlotSelectedTRTMarker()
        Try
            If lvSummary.Items(SelectedIndex).Checked Then
                'TreatmentMinMarker.PositionX = TheData.Data(SelectedIndex - 1).Xm
                'TreatmentMinMarker.PositionY = TheData.Data(SelectedIndex - 1).Ym
                lblNullPoint.Text = TheData.Data(SelectedIndex - 1).Xm.ToString("0.0000") & " deg"
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

#End Region

#Region "Sub-Routine"

    Private Sub NewMeasurement()
        'verify user
        If lvSummary.Items.Count > 0 Then
            If MsgBox("Data will be deleted. Do you want to new measurement?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If

        'load dialog
        Dim f As New frmNewMeasurement
        f.StartPosition = FormStartPosition.CenterScreen
        f.ShowDialog()

        'do update the job
        If f.Verify() = True Then

            Try
                'get information
                txtSampleName.Text = f.SampleName
                NumberOfRepeatation = f.NumberOfRepeatation
                numRepeatNumber.Value = f.RepeatNumber

                'initialize the data object
                TheData = New BaseDataControl
                TheData.SampleName = txtSampleName.Text

                'clear
                lvSummary.Items.Clear()
                Dim lvi As ListViewItem

                'add ref.
                lvi = New ListViewItem
                lvi.Text = "Reference"
                lvi.SubItems.Add("-")
                lvi.SubItems.Add("-")
                lvi.SubItems.Add("-")
                lvi.Checked = True
                lvi.BackColor = ReferenceColor
                lvi.UseItemStyleForSubItems = False
                lvSummary.Items.Add(lvi)

                'add repeats
                For i As Integer = 1 To NumberOfRepeatation
                    lvi = New ListViewItem
                    lvi.Text = "Sample " & i.ToString
                    lvi.SubItems.Add("-")
                    lvi.SubItems.Add("-")
                    lvi.SubItems.Add("-")
                    lvi.Checked = True
                    lvi.BackColor = ColorTable((i - 1) Mod ColorTable.Length)
                    lvi.UseItemStyleForSubItems = False
                    lvSummary.Items.Add(lvi)
                Next

                'clear treatment curve
                'ReDim TreatmentCurve(0 To NumberOfRepeatation - 1)

                gbMeasurement.Enabled = True
                btnNewMeas.Enabled = True
                gbSample.Enabled = True
                gbScanCondition.Enabled = True

                lvSummary.Items(0).Selected = True
                lvSummary.Focus()

            Catch ex As Exception
                'do nothing
            End Try

        End If
    End Sub

    Private Sub DefineAngleOfRotation()
        Dim lvi As ListViewItem
        lvi = lvSummary.Items(SelectedIndex)
        If SelectedIndex = 0 Then
            lvi.SubItems(1).Text = _
                "(" & _
                TheData.Reference.Xm.ToString("0.00") & _
                ", " & _
                TheData.Reference.Ym.ToString("0.0000") & _
                ")"
        Else
            lvi = lvSummary.Items(SelectedIndex)
            lvi.SubItems(1).Text = _
                "(" & _
                TheData.Data(SelectedIndex - 1).Xm.ToString("0.00") & _
                ", " & _
                TheData.Data(SelectedIndex - 1).Ym.ToString("0.0000") & _
                ")"
            lvi.SubItems(2).Text = TheData.Data(SelectedIndex - 1).AngleOfRotation.ToString("0.00")
        End If
    End Sub

    Private Sub LoadSetting()
        txtVoltageRange.Text = My.Settings.VoltageRange.ToString
        txtVoltageResolution.Text = My.Settings.VoltageResolution.ToString
        mnuOptionsDemomode.Checked = My.Settings.IsDemo
        ReferenceColor = My.Settings.ReferenceColor
        ColorTable(0) = My.Settings.Color1
        ColorTable(1) = My.Settings.Color2
        ColorTable(2) = My.Settings.Color3
        ColorTable(3) = My.Settings.Color4
        ColorTable(4) = My.Settings.Color5
        ColorTable(5) = My.Settings.Color6
        ColorTable(6) = My.Settings.Color7
        ColorTable(7) = My.Settings.Color8
        ColorTable(8) = My.Settings.Color9
        ColorTable(9) = My.Settings.Color10
        ColorTable(10) = My.Settings.Color11
        ColorTable(11) = My.Settings.Color12
        ColorTable(12) = My.Settings.Color13
        ColorTable(13) = My.Settings.Color14
        ColorTable(14) = My.Settings.Color15
        ColorTable(15) = My.Settings.Color16
        ColorTable(16) = My.Settings.Color17
        ColorTable(17) = My.Settings.Color18
        ColorTable(18) = My.Settings.Color19
        ColorTable(19) = My.Settings.Color20
    End Sub

    Private Sub SaveSetting()
        My.Settings.IsDemo = mnuOptionsDemomode.Checked
        My.Settings.Save()
    End Sub

    Public Sub ApplyColorTableToSamples()
        Try
            lvSummary.Items(0).BackColor = ReferenceColor
            For i As Integer = 1 To lvSummary.Items.Count - 1
                lvSummary.Items(i).BackColor = ColorTable((i - 1) Mod ColorTable.Length)
            Next
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

#End Region

#Region "Summary"

    Private Sub lvSummary_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvSummary.ItemSelectionChanged
        If lvSummary.SelectedIndices Is Nothing Then Exit Sub
        If lvSummary.SelectedIndices.Count <= 0 Then Exit Sub
        SelectedIndex = lvSummary.SelectedIndices(0)
        Try
            If SelectedIndex = 0 Then
                lblSample.Text = "Reference"
            Else
                lblSample.Text = "Sample " & SelectedIndex.ToString
            End If
            ResetDynaplot()
            PlotReferenceCurve()
            PlotTreatmentsCurve()
            PlotSelectedTRTMarker()
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

#End Region

    'try

    Private Sub btnINIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnINIT.Click
        Try
            ConnectDevices()
            Dim MSG As String = "A:WP" & CInt(-1 * Val(txtStart.Text) / StepFactor).ToString & "P" & CInt(-1 * Val(txtStart.Text) / StepFactor).ToString
            MMC.WriteString(MSG)
            DisconnectDevices()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewMeas.Click
        NewMeasurement()
    End Sub

    Private Sub txtAvageNumber_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(Keys.Enter) Then
            btnStart.Focus()
        End If
    End Sub

    Private Sub lvSummary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lvSummary.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub lvSummary_ItemChecked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvSummary.ItemChecked
        ResetDynaplot()
        PlotReferenceCurve()
        PlotTreatmentsCurve()
        PlotSelectedTRTMarker()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Polarimeter program. (c)2010, Physics, KMITL. Design by S. Saejia.")
    End Sub

    Private Sub mnuOptionsColorTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptionsColorTable.Click
        Dim f As New frmColorTable
        f.ShowDialog()
        ResetDynaplot()
        PlotReferenceCurve()
        PlotTreatmentsCurve()
        PlotSelectedTRTMarker()
    End Sub

    Private Sub mnuMeasureStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMeasureStart.Click
        DoStart()
    End Sub

    Private Sub mnuMeasureStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMeasureStop.Click
        DoStop()
    End Sub

    Private Sub mnuMeasurePause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMeasurePause.Click
        DoPause()
    End Sub

    Private Sub mnuMeasureContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMeasureContinue.Click
        DoPause()
    End Sub

    Private Sub txtVoltageRange_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVoltageRange.TextChanged
        Try
            My.Settings.VoltageRange = Val(txtVoltageRange.Text)
            My.Settings.Save()
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub txtVoltageResolution_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVoltageResolution.TextChanged
        Try
            My.Settings.VoltageResolution = Val(txtVoltageResolution.Text)
            My.Settings.Save()
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub mnuDevicesClearDMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDevicesClearDMM.Click
        Try
            ConnectDevices()
            DMM.WriteString("*CLS")
            DisconnectDevices()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuDevicesResetDMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDevicesResetDMM.Click
        Try
            ConnectDevices()
            DMM.WriteString("*RST")
            DisconnectDevices()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mnuFileExportImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExportImage.Click
        Try
            Dim dlg As New SaveFileDialog
            dlg.Filter = "Bit map (*.bmp)|*.bmp|All File (*.*)|*.*"
            Dim redlg As DialogResult = dlg.ShowDialog()
            If redlg <> Windows.Forms.DialogResult.OK Then Exit Sub
            Dim path As String = dlg.FileName

            'AxDynaPlot1.ToFile(path)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetFormImage(ByVal include_boders As Boolean) As Bitmap
        Dim wid As Integer = Me.Width
        Dim hgt As Integer = Me.Height
        Dim bm As New Bitmap(wid, hgt)

        Me.DrawToBitmap(bm, New Rectangle(0, 0, wid, hgt))

        If include_boders Then Return bm

        wid = Me.ClientSize.Width
        hgt = Me.ClientSize.Height
        Dim bm2 As New Bitmap(wid, hgt)

        Dim pt As New Point(0, 0)
        pt = PointToScreen(pt)
        Dim dx As Integer = pt.X - Me.Left
        Dim dy As Integer = pt.Y - Me.Top

        Dim g As Graphics = Graphics.FromImage(bm2)
        g.DrawImage(bm, 0, 0, New Rectangle(dx, dy, wid, hgt), GraphicsUnit.Pixel)
        Return bm2
    End Function

    Private Sub mnuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAdd.Click

    End Sub

    Private Sub mnuDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        If Not lvSummary.SelectedItems Is Nothing Then

        End If
    End Sub

End Class
