Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Threading

Public Class Form1

#Region "Field"

    Private addDataRunner As Thread

    Private rand As New Random()
    Public Delegate Sub AddDataDelegate()
    Public addDataDel As AddDataDelegate

    Dim minValue, maxValue As DateTime

#End Region

#Region "Construction and Disposing"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Form user event handlers"

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.btnStop_Click(Nothing, EventArgs.Empty)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim addDataThreadStart As New ThreadStart(AddressOf AddDataThreadLoop)
        addDataRunner = New Thread(addDataThreadStart)

        addDataDel = New AddDataDelegate(AddressOf AddData)

        Me.btnStart_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        ' Disable all controls on the form
        btnStart.Enabled = False
        ' and only Enable the Stop button
        btnStop.Enabled = True

        ' Predefine the viewing area of the chart
        minValue = DateTime.Now
        maxValue = minValue.AddSeconds(30)

        Chart1.ChartAreas(0).AxisX.Minimum = minValue.ToOADate()
        Chart1.ChartAreas(0).AxisX.Maximum = maxValue.ToOADate()

        ' Reset number of series in the chart.
        Chart1.Series.Clear()

        ' create a line chart series
        Dim newSeries As New Series("Series1")
        newSeries.ChartType = SeriesChartType.Line
        newSeries.BorderWidth = 2
        newSeries.Color = Color.OrangeRed
        newSeries.XValueType = ChartValueType.DateTime
        Chart1.Series.Add(newSeries)

        ' start worker threads.
        If addDataRunner.IsAlive = True Then
            addDataRunner.Resume()
        Else
            addDataRunner.Start()
        End If

    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        If addDataRunner.IsAlive = True Then
            addDataRunner.Suspend()
            'addDataRunner.Abort()
        End If

        ' Enable all controls on the form
        btnStart.Enabled = True
        ' and only Disable the Stop button
        btnStop.Enabled = False
    End Sub

#End Region

#Region "Add new data thread"

    '/ Main loop for the thread that adds data to the chart.
    '/ The main purpose of this function is to Invoke AddData
    '/ function every 1000ms (1 second).
    Private Sub AddDataThreadLoop()
        Try
            While True
                Chart1.Invoke(addDataDel)

                Thread.Sleep(100)
            End While
        Catch ex As Exception
            'Thread is aborted
        End Try
    End Sub 'AddDataThreadLoop

    Public Sub AddData()
        Dim timeStamp As DateTime = DateTime.Now

        For Each ptSeries As Series In Chart1.Series
            AddNewPoint(timeStamp, ptSeries)
        Next ptSeries
    End Sub 'AddData

    '/ The AddNewPoint function is called for each series in the chart when
    '/ new points need to be added.  The new point will be placed at specified
    '/ X axis (Date/Time) position with a Y value in a range +/- 1 from the previous
    '/ data point's Y value, and not smaller than zero.
    Public Sub AddNewPoint(ByVal timeStamp As DateTime, ByVal ptSeries As System.Windows.Forms.DataVisualization.Charting.Series)
        'Dim newVal As Double = 0

        'If ptSeries.Points.Count > 0 Then
        '    newVal = ptSeries.Points((ptSeries.Points.Count - 1)).YValues(0) + (rand.NextDouble() * 2 - 1)
        'End If

        'If newVal < 0 Then
        '    newVal = 0
        'End If

        ' Add new data point to its series.
        ptSeries.Points.AddXY(timeStamp.ToOADate(), rand.Next(10, 20))
        ' ptSeries.Points.AddXY(timeStamp.Ticks / 10000000000000000, rand.Next(10, 20))



        ' remove all points from the source series older than 1.5 minutes.
        Dim removeBefore As Double = timeStamp.AddSeconds((CDbl(2) * -1)).ToOADate()
        'remove oldest values to maintain a constant number of data points
        While ptSeries.Points(0).XValue < removeBefore
            ptSeries.Points.RemoveAt(0)
        End While

        Chart1.ChartAreas(0).AxisX.Minimum = ptSeries.Points(0).XValue
        Chart1.ChartAreas(0).AxisX.Maximum = DateTime.FromOADate(ptSeries.Points(0).XValue).AddSeconds(1.9).ToOADate()

        Chart1.Invalidate()
    End Sub 'AddNewPoint

#End Region

End Class
