<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mbMainMenus = New System.Windows.Forms.MenuStrip
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFileNewMeasurement = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFileOpendata = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFileSaveData = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFileSaveDataAs = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFileExportImage = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuFileQuit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDevices = New System.Windows.Forms.ToolStripMenuItem
        Me.ConnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DisconnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ConfigAddressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtDMMAddress = New System.Windows.Forms.ToolStripTextBox
        Me.MMC2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtMMCAddress = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuDevicesClearDMM = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDevicesResetDMM = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMeasure = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMeasureStart = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMeasureStop = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuMeasurePause = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMeasureContinue = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOptionsDemomode = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOptionsColorTable = New System.Windows.Forms.ToolStripMenuItem
        Me.VoltageRangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.txtVoltageRange = New System.Windows.Forms.ToolStripTextBox
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.txtVoltageResolution = New System.Windows.Forms.ToolStripTextBox
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gbSample = New System.Windows.Forms.GroupBox
        Me.numRepeatNumber = New System.Windows.Forms.NumericUpDown
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtSampleName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lvSummary = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.mnuTable = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAdd = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.gbScanCondition = New System.Windows.Forms.GroupBox
        Me.btnINIT = New System.Windows.Forms.Button
        Me.txtStop = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtStart = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtResolution = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnPause = New System.Windows.Forms.Button
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        'Me.AxDynaPlot1 = New AxDYNAPLOT3Lib.AxDynaPlot
        Me.sbMainStatus = New System.Windows.Forms.StatusStrip
        Me.lblMainStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblSample = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblNullPoint = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblDMM = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblMMC = New System.Windows.Forms.ToolStripStatusLabel
        Me.gbMeasurement = New System.Windows.Forms.GroupBox
        Me.btnNewMeas = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.mbMainMenus.SuspendLayout()
        Me.gbSample.SuspendLayout()
        CType(Me.numRepeatNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuTable.SuspendLayout()
        Me.gbScanCondition.SuspendLayout()
        'CType(Me.AxDynaPlot1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sbMainStatus.SuspendLayout()
        Me.gbMeasurement.SuspendLayout()
        Me.SuspendLayout()
        '
        'mbMainMenus
        '
        Me.mbMainMenus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuDevices, Me.mnuMeasure, Me.mnuOptions, Me.HelpToolStripMenuItem})
        Me.mbMainMenus.Location = New System.Drawing.Point(0, 0)
        Me.mbMainMenus.Name = "mbMainMenus"
        Me.mbMainMenus.Size = New System.Drawing.Size(945, 24)
        Me.mbMainMenus.TabIndex = 0
        Me.mbMainMenus.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileNewMeasurement, Me.ToolStripSeparator5, Me.mnuFileOpendata, Me.ToolStripSeparator4, Me.mnuFileSaveData, Me.mnuFileSaveDataAs, Me.ToolStripSeparator7, Me.mnuFileExportImage, Me.ToolStripSeparator1, Me.mnuFileQuit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuFileNewMeasurement
        '
        Me.mnuFileNewMeasurement.Name = "mnuFileNewMeasurement"
        Me.mnuFileNewMeasurement.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mnuFileNewMeasurement.Size = New System.Drawing.Size(226, 22)
        Me.mnuFileNewMeasurement.Text = "New Measurement..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(223, 6)
        '
        'mnuFileOpendata
        '
        Me.mnuFileOpendata.Enabled = False
        Me.mnuFileOpendata.Name = "mnuFileOpendata"
        Me.mnuFileOpendata.Size = New System.Drawing.Size(226, 22)
        Me.mnuFileOpendata.Text = "Open Data..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(223, 6)
        '
        'mnuFileSaveData
        '
        Me.mnuFileSaveData.Enabled = False
        Me.mnuFileSaveData.Name = "mnuFileSaveData"
        Me.mnuFileSaveData.Size = New System.Drawing.Size(226, 22)
        Me.mnuFileSaveData.Text = "Save Data"
        '
        'mnuFileSaveDataAs
        '
        Me.mnuFileSaveDataAs.Name = "mnuFileSaveDataAs"
        Me.mnuFileSaveDataAs.Size = New System.Drawing.Size(226, 22)
        Me.mnuFileSaveDataAs.Text = "Save Data As..."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(223, 6)
        '
        'mnuFileExportImage
        '
        Me.mnuFileExportImage.Name = "mnuFileExportImage"
        Me.mnuFileExportImage.Size = New System.Drawing.Size(226, 22)
        Me.mnuFileExportImage.Text = "Export to image file..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(223, 6)
        '
        'mnuFileQuit
        '
        Me.mnuFileQuit.Name = "mnuFileQuit"
        Me.mnuFileQuit.Size = New System.Drawing.Size(226, 22)
        Me.mnuFileQuit.Text = "Quit"
        '
        'mnuDevices
        '
        Me.mnuDevices.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectToolStripMenuItem, Me.DisconnectToolStripMenuItem, Me.ToolStripSeparator2, Me.ConfigAddressToolStripMenuItem, Me.MMC2ToolStripMenuItem, Me.ToolStripSeparator6, Me.mnuDevicesClearDMM, Me.mnuDevicesResetDMM})
        Me.mnuDevices.Name = "mnuDevices"
        Me.mnuDevices.Size = New System.Drawing.Size(59, 20)
        Me.mnuDevices.Text = "Devices"
        '
        'ConnectToolStripMenuItem
        '
        Me.ConnectToolStripMenuItem.Name = "ConnectToolStripMenuItem"
        Me.ConnectToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.ConnectToolStripMenuItem.Text = "Connect"
        '
        'DisconnectToolStripMenuItem
        '
        Me.DisconnectToolStripMenuItem.Name = "DisconnectToolStripMenuItem"
        Me.DisconnectToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.DisconnectToolStripMenuItem.Text = "Disconnect"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(214, 6)
        '
        'ConfigAddressToolStripMenuItem
        '
        Me.ConfigAddressToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtDMMAddress})
        Me.ConfigAddressToolStripMenuItem.Name = "ConfigAddressToolStripMenuItem"
        Me.ConfigAddressToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.ConfigAddressToolStripMenuItem.Text = "DMM 34401A"
        '
        'txtDMMAddress
        '
        Me.txtDMMAddress.Name = "txtDMMAddress"
        Me.txtDMMAddress.Size = New System.Drawing.Size(100, 23)
        Me.txtDMMAddress.Text = "GPIB0::26::INSTR"
        '
        'MMC2ToolStripMenuItem
        '
        Me.MMC2ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtMMCAddress})
        Me.MMC2ToolStripMenuItem.Name = "MMC2ToolStripMenuItem"
        Me.MMC2ToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.MMC2ToolStripMenuItem.Text = "MMC-2"
        '
        'txtMMCAddress
        '
        Me.txtMMCAddress.Name = "txtMMCAddress"
        Me.txtMMCAddress.Size = New System.Drawing.Size(100, 23)
        Me.txtMMCAddress.Text = "GPIB0::7::INSTR"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(214, 6)
        '
        'mnuDevicesClearDMM
        '
        Me.mnuDevicesClearDMM.Name = "mnuDevicesClearDMM"
        Me.mnuDevicesClearDMM.Size = New System.Drawing.Size(217, 22)
        Me.mnuDevicesClearDMM.Text = "Clear Error of DMM 34401A"
        '
        'mnuDevicesResetDMM
        '
        Me.mnuDevicesResetDMM.Name = "mnuDevicesResetDMM"
        Me.mnuDevicesResetDMM.Size = New System.Drawing.Size(217, 22)
        Me.mnuDevicesResetDMM.Text = "Reset DMM 34401A"
        '
        'mnuMeasure
        '
        Me.mnuMeasure.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMeasureStart, Me.mnuMeasureStop, Me.ToolStripSeparator3, Me.mnuMeasurePause, Me.mnuMeasureContinue})
        Me.mnuMeasure.Name = "mnuMeasure"
        Me.mnuMeasure.Size = New System.Drawing.Size(64, 20)
        Me.mnuMeasure.Text = "Measure"
        '
        'mnuMeasureStart
        '
        Me.mnuMeasureStart.Name = "mnuMeasureStart"
        Me.mnuMeasureStart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuMeasureStart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMeasureStart.Text = "Start"
        '
        'mnuMeasureStop
        '
        Me.mnuMeasureStop.Name = "mnuMeasureStop"
        Me.mnuMeasureStop.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.mnuMeasureStop.Size = New System.Drawing.Size(165, 22)
        Me.mnuMeasureStop.Text = "Stop"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(162, 6)
        '
        'mnuMeasurePause
        '
        Me.mnuMeasurePause.Name = "mnuMeasurePause"
        Me.mnuMeasurePause.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.mnuMeasurePause.Size = New System.Drawing.Size(165, 22)
        Me.mnuMeasurePause.Text = "Pause"
        '
        'mnuMeasureContinue
        '
        Me.mnuMeasureContinue.Name = "mnuMeasureContinue"
        Me.mnuMeasureContinue.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuMeasureContinue.Size = New System.Drawing.Size(165, 22)
        Me.mnuMeasureContinue.Text = "Continue"
        '
        'mnuOptions
        '
        Me.mnuOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOptionsDemomode, Me.mnuOptionsColorTable, Me.VoltageRangeToolStripMenuItem})
        Me.mnuOptions.Name = "mnuOptions"
        Me.mnuOptions.Size = New System.Drawing.Size(61, 20)
        Me.mnuOptions.Text = "Options"
        '
        'mnuOptionsDemomode
        '
        Me.mnuOptionsDemomode.Checked = Global.Polarimeter.My.MySettings.Default.IsDemo
        Me.mnuOptionsDemomode.CheckOnClick = True
        Me.mnuOptionsDemomode.Name = "mnuOptionsDemomode"
        Me.mnuOptionsDemomode.Size = New System.Drawing.Size(190, 22)
        Me.mnuOptionsDemomode.Text = "Demo mode"
        '
        'mnuOptionsColorTable
        '
        Me.mnuOptionsColorTable.Name = "mnuOptionsColorTable"
        Me.mnuOptionsColorTable.Size = New System.Drawing.Size(190, 22)
        Me.mnuOptionsColorTable.Text = "Color Table"
        '
        'VoltageRangeToolStripMenuItem
        '
        Me.VoltageRangeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2})
        Me.VoltageRangeToolStripMenuItem.Name = "VoltageRangeToolStripMenuItem"
        Me.VoltageRangeToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.VoltageRangeToolStripMenuItem.Text = "Voltage Measurement"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtVoltageRange})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(130, 22)
        Me.ToolStripMenuItem1.Text = "Range"
        '
        'txtVoltageRange
        '
        Me.txtVoltageRange.Name = "txtVoltageRange"
        Me.txtVoltageRange.Size = New System.Drawing.Size(100, 23)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtVoltageResolution})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(130, 22)
        Me.ToolStripMenuItem2.Text = "Resolution"
        '
        'txtVoltageResolution
        '
        Me.txtVoltageResolution.Name = "txtVoltageResolution"
        Me.txtVoltageResolution.Size = New System.Drawing.Size(100, 23)
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.AboutToolStripMenuItem.Text = "About program"
        '
        'gbSample
        '
        Me.gbSample.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbSample.Controls.Add(Me.numRepeatNumber)
        Me.gbSample.Controls.Add(Me.Label9)
        Me.gbSample.Controls.Add(Me.txtSampleName)
        Me.gbSample.Controls.Add(Me.Label7)
        Me.gbSample.Controls.Add(Me.lvSummary)
        Me.gbSample.Enabled = False
        Me.gbSample.Location = New System.Drawing.Point(12, 95)
        Me.gbSample.Name = "gbSample"
        Me.gbSample.Size = New System.Drawing.Size(322, 242)
        Me.gbSample.TabIndex = 1
        Me.gbSample.TabStop = False
        Me.gbSample.Text = "Samples"
        '
        'numRepeatNumber
        '
        Me.numRepeatNumber.Location = New System.Drawing.Point(100, 53)
        Me.numRepeatNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numRepeatNumber.Name = "numRepeatNumber"
        Me.numRepeatNumber.Size = New System.Drawing.Size(76, 20)
        Me.numRepeatNumber.TabIndex = 1
        Me.numRepeatNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numRepeatNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Average number:"
        '
        'txtSampleName
        '
        Me.txtSampleName.Location = New System.Drawing.Point(100, 27)
        Me.txtSampleName.Name = "txtSampleName"
        Me.txtSampleName.Size = New System.Drawing.Size(213, 20)
        Me.txtSampleName.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Sample Name:"
        '
        'lvSummary
        '
        Me.lvSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvSummary.CheckBoxes = True
        Me.lvSummary.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader4, Me.ColumnHeader2})
        Me.lvSummary.FullRowSelect = True
        Me.lvSummary.GridLines = True
        Me.lvSummary.Location = New System.Drawing.Point(6, 81)
        Me.lvSummary.Name = "lvSummary"
        Me.lvSummary.Size = New System.Drawing.Size(310, 155)
        Me.lvSummary.TabIndex = 2
        Me.lvSummary.UseCompatibleStateImageBehavior = False
        Me.lvSummary.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Sample"
        Me.ColumnHeader1.Width = 77
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Null Point"
        Me.ColumnHeader4.Width = 119
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Angle of Rotation"
        Me.ColumnHeader2.Width = 103
        '
        'mnuTable
        '
        Me.mnuTable.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAdd, Me.mnuDelete})
        Me.mnuTable.Name = "mnuTable"
        Me.mnuTable.Size = New System.Drawing.Size(108, 48)
        '
        'mnuAdd
        '
        Me.mnuAdd.Name = "mnuAdd"
        Me.mnuAdd.Size = New System.Drawing.Size(152, 22)
        Me.mnuAdd.Text = "Add"
        '
        'mnuDelete
        '
        Me.mnuDelete.Name = "mnuDelete"
        Me.mnuDelete.Size = New System.Drawing.Size(152, 22)
        Me.mnuDelete.Text = "Delete"
        '
        'gbScanCondition
        '
        Me.gbScanCondition.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbScanCondition.Controls.Add(Me.btnINIT)
        Me.gbScanCondition.Controls.Add(Me.txtStop)
        Me.gbScanCondition.Controls.Add(Me.Label5)
        Me.gbScanCondition.Controls.Add(Me.Label6)
        Me.gbScanCondition.Controls.Add(Me.txtStart)
        Me.gbScanCondition.Controls.Add(Me.Label3)
        Me.gbScanCondition.Controls.Add(Me.Label4)
        Me.gbScanCondition.Controls.Add(Me.txtResolution)
        Me.gbScanCondition.Controls.Add(Me.Label1)
        Me.gbScanCondition.Controls.Add(Me.Label2)
        Me.gbScanCondition.Enabled = False
        Me.gbScanCondition.Location = New System.Drawing.Point(12, 343)
        Me.gbScanCondition.Name = "gbScanCondition"
        Me.gbScanCondition.Size = New System.Drawing.Size(322, 107)
        Me.gbScanCondition.TabIndex = 2
        Me.gbScanCondition.TabStop = False
        Me.gbScanCondition.Text = "Scan conditions"
        '
        'btnINIT
        '
        Me.btnINIT.Location = New System.Drawing.Point(233, 20)
        Me.btnINIT.Name = "btnINIT"
        Me.btnINIT.Size = New System.Drawing.Size(80, 43)
        Me.btnINIT.TabIndex = 3
        Me.btnINIT.Text = "Go to start point"
        Me.btnINIT.UseVisualStyleBackColor = True
        '
        'txtStop
        '
        Me.txtStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtStop.Location = New System.Drawing.Point(76, 47)
        Me.txtStop.Name = "txtStop"
        Me.txtStop.Size = New System.Drawing.Size(119, 20)
        Me.txtStop.TabIndex = 1
        Me.txtStop.Text = "360"
        Me.txtStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(41, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Stop"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(201, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "deg."
        '
        'txtStart
        '
        Me.txtStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtStart.Location = New System.Drawing.Point(76, 21)
        Me.txtStart.Name = "txtStart"
        Me.txtStart.Size = New System.Drawing.Size(119, 20)
        Me.txtStart.TabIndex = 0
        Me.txtStart.Text = "0"
        Me.txtStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Start"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(201, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "deg."
        '
        'txtResolution
        '
        Me.txtResolution.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtResolution.Location = New System.Drawing.Point(76, 73)
        Me.txtResolution.Name = "txtResolution"
        Me.txtResolution.Size = New System.Drawing.Size(119, 20)
        Me.txtResolution.TabIndex = 2
        Me.txtResolution.Text = "2.0"
        Me.txtResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Resolution"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(201, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "deg."
        '
        'btnPause
        '
        Me.btnPause.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPause.BackColor = System.Drawing.Color.Gold
        Me.btnPause.Enabled = False
        Me.btnPause.Location = New System.Drawing.Point(216, 15)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(100, 55)
        Me.btnPause.TabIndex = 2
        Me.btnPause.Text = "PAUSE"
        Me.btnPause.UseVisualStyleBackColor = False
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStop.BackColor = System.Drawing.Color.Red
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(112, 15)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(100, 55)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "STOP"
        Me.btnStop.UseVisualStyleBackColor = False
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStart.BackColor = System.Drawing.Color.Green
        Me.btnStart.Location = New System.Drawing.Point(6, 15)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(100, 55)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "START"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'AxDynaPlot1
        '
        'Me.AxDynaPlot1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
        '            Or System.Windows.Forms.AnchorStyles.Left) _
        '            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        'Me.AxDynaPlot1.Location = New System.Drawing.Point(340, 33)
        'Me.AxDynaPlot1.Name = "AxDynaPlot1"
        'Me.AxDynaPlot1.OcxState = CType(resources.GetObject("AxDynaPlot1.OcxState"), System.Windows.Forms.AxHost.State)
        'Me.AxDynaPlot1.Size = New System.Drawing.Size(593, 499)
        'Me.AxDynaPlot1.TabIndex = 4
        '
        'sbMainStatus
        '
        Me.sbMainStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblMainStatus, Me.ToolStripStatusLabel1, Me.lblSample, Me.ToolStripStatusLabel7, Me.lblNullPoint, Me.ToolStripStatusLabel2, Me.lblDMM, Me.ToolStripStatusLabel4, Me.lblMMC})
        Me.sbMainStatus.Location = New System.Drawing.Point(0, 542)
        Me.sbMainStatus.Name = "sbMainStatus"
        Me.sbMainStatus.Size = New System.Drawing.Size(945, 24)
        Me.sbMainStatus.TabIndex = 26
        Me.sbMainStatus.Text = "StatusStrip1"
        '
        'lblMainStatus
        '
        Me.lblMainStatus.Name = "lblMainStatus"
        Me.lblMainStatus.Size = New System.Drawing.Size(39, 19)
        Me.lblMainStatus.Text = "Ready"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(53, 19)
        Me.ToolStripStatusLabel1.Text = "Sample:"
        '
        'lblSample
        '
        Me.lblSample.Name = "lblSample"
        Me.lblSample.Size = New System.Drawing.Size(59, 19)
        Me.lblSample.Text = "Reference"
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(67, 19)
        Me.ToolStripStatusLabel7.Text = "Null Point:"
        '
        'lblNullPoint
        '
        Me.lblNullPoint.Name = "lblNullPoint"
        Me.lblNullPoint.Size = New System.Drawing.Size(16, 19)
        Me.lblNullPoint.Text = "..."
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(44, 19)
        Me.ToolStripStatusLabel2.Text = "DMM:"
        '
        'lblDMM
        '
        Me.lblDMM.BackColor = System.Drawing.Color.Red
        Me.lblDMM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblDMM.Image = CType(resources.GetObject("lblDMM.Image"), System.Drawing.Image)
        Me.lblDMM.Name = "lblDMM"
        Me.lblDMM.Size = New System.Drawing.Size(79, 19)
        Me.lblDMM.Text = "Disconnected"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(66, 19)
        Me.ToolStripStatusLabel4.Text = "MMMC-2:"
        '
        'lblMMC
        '
        Me.lblMMC.BackColor = System.Drawing.Color.Red
        Me.lblMMC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblMMC.Image = CType(resources.GetObject("lblMMC.Image"), System.Drawing.Image)
        Me.lblMMC.Name = "lblMMC"
        Me.lblMMC.Size = New System.Drawing.Size(79, 19)
        Me.lblMMC.Text = "Disconnected"
        '
        'gbMeasurement
        '
        Me.gbMeasurement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbMeasurement.Controls.Add(Me.btnPause)
        Me.gbMeasurement.Controls.Add(Me.btnStart)
        Me.gbMeasurement.Controls.Add(Me.btnStop)
        Me.gbMeasurement.Enabled = False
        Me.gbMeasurement.Location = New System.Drawing.Point(12, 456)
        Me.gbMeasurement.Name = "gbMeasurement"
        Me.gbMeasurement.Size = New System.Drawing.Size(322, 76)
        Me.gbMeasurement.TabIndex = 3
        Me.gbMeasurement.TabStop = False
        Me.gbMeasurement.Text = "Measurement"
        '
        'btnNewMeas
        '
        Me.btnNewMeas.Location = New System.Drawing.Point(19, 38)
        Me.btnNewMeas.Name = "btnNewMeas"
        Me.btnNewMeas.Size = New System.Drawing.Size(128, 42)
        Me.btnNewMeas.TabIndex = 0
        Me.btnNewMeas.Text = "New measurement..."
        Me.btnNewMeas.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Location = New System.Drawing.Point(169, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(132, 67)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "1. New measurement." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. Complete new dialog." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3. Define scan conditions." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4. Clic" & _
            "k Go to start point." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "5. Click Start."
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 566)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnNewMeas)
        Me.Controls.Add(Me.gbMeasurement)
        Me.Controls.Add(Me.sbMainStatus)
        Me.Controls.Add(Me.gbSample)
        Me.Controls.Add(Me.mbMainMenus)
        Me.Controls.Add(Me.gbScanCondition)
        'Me.Controls.Add(Me.AxDynaPlot1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mbMainMenus
        Me.Name = "frmMain"
        Me.Text = "Polarimeter"
        Me.mbMainMenus.ResumeLayout(False)
        Me.mbMainMenus.PerformLayout()
        Me.gbSample.ResumeLayout(False)
        Me.gbSample.PerformLayout()
        CType(Me.numRepeatNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuTable.ResumeLayout(False)
        Me.gbScanCondition.ResumeLayout(False)
        Me.gbScanCondition.PerformLayout()
        'CType(Me.AxDynaPlot1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sbMainStatus.ResumeLayout(False)
        Me.sbMainStatus.PerformLayout()
        Me.gbMeasurement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mbMainMenus As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileOpendata As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileSaveData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFileQuit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDevices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisconnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ConfigAddressToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMeasure As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMeasureStart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMeasureStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMeasurePause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMeasureContinue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtResolution As System.Windows.Forms.TextBox
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gbScanCondition As System.Windows.Forms.GroupBox
    Friend WithEvents txtStop As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtStart As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDMMAddress As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents MMC2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtMMCAddress As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents gbSample As System.Windows.Forms.GroupBox
    'Friend WithEvents AxDynaPlot1 As AxDYNAPLOT3Lib.AxDynaPlot
    Friend WithEvents sbMainStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents lblMainStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSample As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblDMM As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblMMC As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOptionsDemomode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbMeasurement As System.Windows.Forms.GroupBox
    Friend WithEvents lvSummary As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFileSaveDataAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileNewMeasurement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSampleName As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblNullPoint As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnINIT As System.Windows.Forms.Button
    Friend WithEvents btnNewMeas As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents numRepeatNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents mnuOptionsColorTable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VoltageRangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtVoltageRange As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtVoltageResolution As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDevicesClearDMM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDevicesResetDMM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuFileExportImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTable As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDelete As System.Windows.Forms.ToolStripMenuItem

End Class
