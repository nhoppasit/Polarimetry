<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewMeasurement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewMeasurement))
        Me.txtSampleName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.numRepeatation = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.numAverageNumber = New System.Windows.Forms.NumericUpDown
        CType(Me.numRepeatation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numAverageNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtSampleName
        '
        Me.txtSampleName.Location = New System.Drawing.Point(117, 12)
        Me.txtSampleName.Name = "txtSampleName"
        Me.txtSampleName.Size = New System.Drawing.Size(241, 20)
        Me.txtSampleName.TabIndex = 0
        Me.txtSampleName.Text = "Sample Name?"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(35, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Sample Name:"
        '
        'numRepeatation
        '
        Me.numRepeatation.Location = New System.Drawing.Point(117, 64)
        Me.numRepeatation.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numRepeatation.Name = "numRepeatation"
        Me.numRepeatation.Size = New System.Drawing.Size(61, 20)
        Me.numRepeatation.TabIndex = 2
        Me.numRepeatation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numRepeatation.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Number of Samples:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(172, 104)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(90, 31)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(268, 104)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 31)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Average number:"
        '
        'numAverageNumber
        '
        Me.numAverageNumber.Location = New System.Drawing.Point(117, 38)
        Me.numAverageNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAverageNumber.Name = "numAverageNumber"
        Me.numAverageNumber.Size = New System.Drawing.Size(61, 20)
        Me.numAverageNumber.TabIndex = 1
        Me.numAverageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numAverageNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'frmNewMeasurement
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(370, 147)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.numAverageNumber)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.numRepeatation)
        Me.Controls.Add(Me.txtSampleName)
        Me.Controls.Add(Me.Label7)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmNewMeasurement"
        Me.Text = "New Measurement..."
        CType(Me.numRepeatation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numAverageNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSampleName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents numRepeatation As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents numAverageNumber As System.Windows.Forms.NumericUpDown
End Class
