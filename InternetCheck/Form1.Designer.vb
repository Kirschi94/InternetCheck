<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Main
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_Main))
        Me.ListView_Losses = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader()
        Me.ContextMenuStrip_ListView = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TojsonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TotxtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label_Losses = New System.Windows.Forms.Label()
        Me.Button_CheckButton = New System.Windows.Forms.Button()
        Me.TabControl_Main = New System.Windows.Forms.TabControl()
        Me.TabPage_Overview = New System.Windows.Forms.TabPage()
        Me.TextBox_Search = New System.Windows.Forms.TextBox()
        Me.Label_Search = New System.Windows.Forms.Label()
        Me.Button_Debug = New System.Windows.Forms.Button()
        Me.TabPage_Options = New System.Windows.Forms.TabPage()
        Me.TextBox_Duration = New System.Windows.Forms.TextBox()
        Me.Label_Dur2 = New System.Windows.Forms.Label()
        Me.Label_Dur1 = New System.Windows.Forms.Label()
        Me.CheckBox_StartMin = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Autostart = New System.Windows.Forms.CheckBox()
        Me.CheckBox_LogSave = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Notify = New System.Windows.Forms.CheckBox()
        Me.CheckBox_WinStart = New System.Windows.Forms.CheckBox()
        Me.TextBox_ConLost = New System.Windows.Forms.TextBox()
        Me.TextBox_ConBack = New System.Windows.Forms.TextBox()
        Me.CheckBox_ConBack = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ConLost = New System.Windows.Forms.CheckBox()
        Me.TabPage_Log = New System.Windows.Forms.TabPage()
        Me.RichTextBox_Log = New System.Windows.Forms.RichTextBox()
        Me.TheNotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip_NotIcon = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartCheckingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConLostTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConBackTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogSaveTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutostartTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartMinTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.WinStartTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TheTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TheBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.FolderBrowserDialog_Export = New System.Windows.Forms.FolderBrowserDialog()
        Me.Timer_Minimize = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip_ListView.SuspendLayout()
        Me.TabControl_Main.SuspendLayout()
        Me.TabPage_Overview.SuspendLayout()
        Me.TabPage_Options.SuspendLayout()
        Me.TabPage_Log.SuspendLayout()
        Me.ContextMenuStrip_NotIcon.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView_Losses
        '
        Me.ListView_Losses.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Losses.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView_Losses.ContextMenuStrip = Me.ContextMenuStrip_ListView
        Me.ListView_Losses.HideSelection = False
        Me.ListView_Losses.Location = New System.Drawing.Point(8, 21)
        Me.ListView_Losses.Name = "ListView_Losses"
        Me.ListView_Losses.Size = New System.Drawing.Size(579, 372)
        Me.ListView_Losses.TabIndex = 0
        Me.ListView_Losses.UseCompatibleStateImageBehavior = False
        Me.ListView_Losses.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Connection lost"
        Me.ColumnHeader1.Width = 200
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Connection reestablished"
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Duration"
        Me.ColumnHeader3.Width = 150
        '
        'ContextMenuStrip_ListView
        '
        Me.ContextMenuStrip_ListView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToClipboardToolStripMenuItem, Me.ExportToolStripMenuItem, Me.DeleteItemsToolStripMenuItem})
        Me.ContextMenuStrip_ListView.Name = "ContextMenuStrip_ListView"
        Me.ContextMenuStrip_ListView.Size = New System.Drawing.Size(172, 70)
        '
        'CopyToClipboardToolStripMenuItem
        '
        Me.CopyToClipboardToolStripMenuItem.Name = "CopyToClipboardToolStripMenuItem"
        Me.CopyToClipboardToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.CopyToClipboardToolStripMenuItem.Text = "Copy to Clipboard"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TojsonToolStripMenuItem, Me.TotxtToolStripMenuItem})
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'TojsonToolStripMenuItem
        '
        Me.TojsonToolStripMenuItem.Enabled = False
        Me.TojsonToolStripMenuItem.Name = "TojsonToolStripMenuItem"
        Me.TojsonToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.TojsonToolStripMenuItem.Text = "To .json"
        '
        'TotxtToolStripMenuItem
        '
        Me.TotxtToolStripMenuItem.Enabled = False
        Me.TotxtToolStripMenuItem.Name = "TotxtToolStripMenuItem"
        Me.TotxtToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.TotxtToolStripMenuItem.Text = "To .txt"
        '
        'DeleteItemsToolStripMenuItem
        '
        Me.DeleteItemsToolStripMenuItem.Name = "DeleteItemsToolStripMenuItem"
        Me.DeleteItemsToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.DeleteItemsToolStripMenuItem.Text = "Delete item/s"
        '
        'Label_Losses
        '
        Me.Label_Losses.AutoSize = True
        Me.Label_Losses.Location = New System.Drawing.Point(8, 0)
        Me.Label_Losses.Name = "Label_Losses"
        Me.Label_Losses.Size = New System.Drawing.Size(44, 15)
        Me.Label_Losses.TabIndex = 1
        Me.Label_Losses.Text = "Losses:"
        '
        'Button_CheckButton
        '
        Me.Button_CheckButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_CheckButton.Location = New System.Drawing.Point(481, 399)
        Me.Button_CheckButton.Name = "Button_CheckButton"
        Me.Button_CheckButton.Size = New System.Drawing.Size(106, 23)
        Me.Button_CheckButton.TabIndex = 2
        Me.Button_CheckButton.Text = "Start checking"
        Me.Button_CheckButton.UseVisualStyleBackColor = True
        '
        'TabControl_Main
        '
        Me.TabControl_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_Main.Controls.Add(Me.TabPage_Overview)
        Me.TabControl_Main.Controls.Add(Me.TabPage_Options)
        Me.TabControl_Main.Controls.Add(Me.TabPage_Log)
        Me.TabControl_Main.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Main.Name = "TabControl_Main"
        Me.TabControl_Main.SelectedIndex = 0
        Me.TabControl_Main.Size = New System.Drawing.Size(601, 456)
        Me.TabControl_Main.TabIndex = 3
        '
        'TabPage_Overview
        '
        Me.TabPage_Overview.Controls.Add(Me.TextBox_Search)
        Me.TabPage_Overview.Controls.Add(Me.Label_Search)
        Me.TabPage_Overview.Controls.Add(Me.Button_Debug)
        Me.TabPage_Overview.Controls.Add(Me.Label_Losses)
        Me.TabPage_Overview.Controls.Add(Me.Button_CheckButton)
        Me.TabPage_Overview.Controls.Add(Me.ListView_Losses)
        Me.TabPage_Overview.Location = New System.Drawing.Point(4, 24)
        Me.TabPage_Overview.Name = "TabPage_Overview"
        Me.TabPage_Overview.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Overview.Size = New System.Drawing.Size(593, 428)
        Me.TabPage_Overview.TabIndex = 0
        Me.TabPage_Overview.Text = "Overview"
        Me.TabPage_Overview.UseVisualStyleBackColor = True
        '
        'TextBox_Search
        '
        Me.TextBox_Search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Search.Location = New System.Drawing.Point(59, 399)
        Me.TextBox_Search.Name = "TextBox_Search"
        Me.TextBox_Search.Size = New System.Drawing.Size(145, 23)
        Me.TextBox_Search.TabIndex = 4
        '
        'Label_Search
        '
        Me.Label_Search.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_Search.AutoSize = True
        Me.Label_Search.Location = New System.Drawing.Point(8, 403)
        Me.Label_Search.Name = "Label_Search"
        Me.Label_Search.Size = New System.Drawing.Size(45, 15)
        Me.Label_Search.TabIndex = 5
        Me.Label_Search.Text = "Search:"
        '
        'Button_Debug
        '
        Me.Button_Debug.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Debug.Enabled = False
        Me.Button_Debug.Location = New System.Drawing.Point(317, 399)
        Me.Button_Debug.Name = "Button_Debug"
        Me.Button_Debug.Size = New System.Drawing.Size(158, 23)
        Me.Button_Debug.TabIndex = 4
        Me.Button_Debug.Text = "Lose Connection [DEBUG]"
        Me.Button_Debug.UseVisualStyleBackColor = True
        Me.Button_Debug.Visible = False
        '
        'TabPage_Options
        '
        Me.TabPage_Options.Controls.Add(Me.TextBox_Duration)
        Me.TabPage_Options.Controls.Add(Me.Label_Dur2)
        Me.TabPage_Options.Controls.Add(Me.Label_Dur1)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_StartMin)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_Autostart)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_LogSave)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_Notify)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_WinStart)
        Me.TabPage_Options.Controls.Add(Me.TextBox_ConLost)
        Me.TabPage_Options.Controls.Add(Me.TextBox_ConBack)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_ConBack)
        Me.TabPage_Options.Controls.Add(Me.CheckBox_ConLost)
        Me.TabPage_Options.Location = New System.Drawing.Point(4, 24)
        Me.TabPage_Options.Name = "TabPage_Options"
        Me.TabPage_Options.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Options.Size = New System.Drawing.Size(593, 428)
        Me.TabPage_Options.TabIndex = 2
        Me.TabPage_Options.Text = "Options"
        Me.TabPage_Options.UseVisualStyleBackColor = True
        '
        'TextBox_Duration
        '
        Me.TextBox_Duration.Location = New System.Drawing.Point(289, 187)
        Me.TextBox_Duration.MaxLength = 5
        Me.TextBox_Duration.Name = "TextBox_Duration"
        Me.TextBox_Duration.Size = New System.Drawing.Size(42, 23)
        Me.TextBox_Duration.TabIndex = 11
        Me.TextBox_Duration.Text = "2"
        Me.TextBox_Duration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label_Dur2
        '
        Me.Label_Dur2.AutoSize = True
        Me.Label_Dur2.Location = New System.Drawing.Point(337, 190)
        Me.Label_Dur2.Name = "Label_Dur2"
        Me.Label_Dur2.Size = New System.Drawing.Size(53, 15)
        Me.Label_Dur2.TabIndex = 10
        Me.Label_Dur2.Text = "minutes."
        '
        'Label_Dur1
        '
        Me.Label_Dur1.AutoSize = True
        Me.Label_Dur1.Location = New System.Drawing.Point(5, 190)
        Me.Label_Dur1.Name = "Label_Dur1"
        Me.Label_Dur1.Size = New System.Drawing.Size(278, 15)
        Me.Label_Dur1.TabIndex = 9
        Me.Label_Dur1.Text = "Do not log connection issues with a duration below"
        '
        'CheckBox_StartMin
        '
        Me.CheckBox_StartMin.AutoSize = True
        Me.CheckBox_StartMin.Location = New System.Drawing.Point(8, 162)
        Me.CheckBox_StartMin.Name = "CheckBox_StartMin"
        Me.CheckBox_StartMin.Size = New System.Drawing.Size(109, 19)
        Me.CheckBox_StartMin.TabIndex = 8
        Me.CheckBox_StartMin.Text = "Start minimized"
        Me.CheckBox_StartMin.UseVisualStyleBackColor = True
        '
        'CheckBox_Autostart
        '
        Me.CheckBox_Autostart.AutoSize = True
        Me.CheckBox_Autostart.Location = New System.Drawing.Point(8, 133)
        Me.CheckBox_Autostart.Name = "CheckBox_Autostart"
        Me.CheckBox_Autostart.Size = New System.Drawing.Size(333, 19)
        Me.CheckBox_Autostart.TabIndex = 7
        Me.CheckBox_Autostart.Text = "Start checking the internet connection on program launch"
        Me.CheckBox_Autostart.UseVisualStyleBackColor = True
        '
        'CheckBox_LogSave
        '
        Me.CheckBox_LogSave.AutoSize = True
        Me.CheckBox_LogSave.Checked = True
        Me.CheckBox_LogSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_LogSave.Location = New System.Drawing.Point(8, 104)
        Me.CheckBox_LogSave.Name = "CheckBox_LogSave"
        Me.CheckBox_LogSave.Size = New System.Drawing.Size(94, 19)
        Me.CheckBox_LogSave.TabIndex = 6
        Me.CheckBox_LogSave.Text = "Save log files"
        Me.CheckBox_LogSave.UseVisualStyleBackColor = True
        '
        'CheckBox_Notify
        '
        Me.CheckBox_Notify.AutoSize = True
        Me.CheckBox_Notify.Checked = True
        Me.CheckBox_Notify.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_Notify.Location = New System.Drawing.Point(8, 75)
        Me.CheckBox_Notify.Name = "CheckBox_Notify"
        Me.CheckBox_Notify.Size = New System.Drawing.Size(311, 19)
        Me.CheckBox_Notify.TabIndex = 5
        Me.CheckBox_Notify.Text = "Show notification if connection is lost or reestablished"
        Me.CheckBox_Notify.UseVisualStyleBackColor = True
        '
        'CheckBox_WinStart
        '
        Me.CheckBox_WinStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_WinStart.AutoSize = True
        Me.CheckBox_WinStart.Location = New System.Drawing.Point(8, 403)
        Me.CheckBox_WinStart.Name = "CheckBox_WinStart"
        Me.CheckBox_WinStart.Size = New System.Drawing.Size(159, 19)
        Me.CheckBox_WinStart.TabIndex = 4
        Me.CheckBox_WinStart.Text = "Start on Windows startup"
        Me.CheckBox_WinStart.UseVisualStyleBackColor = True
        '
        'TextBox_ConLost
        '
        Me.TextBox_ConLost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ConLost.Location = New System.Drawing.Point(243, 14)
        Me.TextBox_ConLost.Name = "TextBox_ConLost"
        Me.TextBox_ConLost.Size = New System.Drawing.Size(344, 23)
        Me.TextBox_ConLost.TabIndex = 3
        '
        'TextBox_ConBack
        '
        Me.TextBox_ConBack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_ConBack.Location = New System.Drawing.Point(243, 43)
        Me.TextBox_ConBack.Name = "TextBox_ConBack"
        Me.TextBox_ConBack.Size = New System.Drawing.Size(344, 23)
        Me.TextBox_ConBack.TabIndex = 2
        '
        'CheckBox_ConBack
        '
        Me.CheckBox_ConBack.AutoSize = True
        Me.CheckBox_ConBack.Checked = True
        Me.CheckBox_ConBack.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_ConBack.Location = New System.Drawing.Point(8, 45)
        Me.CheckBox_ConBack.Name = "CheckBox_ConBack"
        Me.CheckBox_ConBack.Size = New System.Drawing.Size(200, 19)
        Me.CheckBox_ConBack.TabIndex = 1
        Me.CheckBox_ConBack.Text = "Connection reestablished Sound:"
        Me.CheckBox_ConBack.UseVisualStyleBackColor = True
        '
        'CheckBox_ConLost
        '
        Me.CheckBox_ConLost.AutoSize = True
        Me.CheckBox_ConLost.Checked = True
        Me.CheckBox_ConLost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_ConLost.Location = New System.Drawing.Point(8, 16)
        Me.CheckBox_ConLost.Name = "CheckBox_ConLost"
        Me.CheckBox_ConLost.Size = New System.Drawing.Size(150, 19)
        Me.CheckBox_ConLost.TabIndex = 0
        Me.CheckBox_ConLost.Text = "Connection lost Sound:"
        Me.CheckBox_ConLost.UseVisualStyleBackColor = True
        '
        'TabPage_Log
        '
        Me.TabPage_Log.Controls.Add(Me.RichTextBox_Log)
        Me.TabPage_Log.Location = New System.Drawing.Point(4, 24)
        Me.TabPage_Log.Name = "TabPage_Log"
        Me.TabPage_Log.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Log.Size = New System.Drawing.Size(593, 428)
        Me.TabPage_Log.TabIndex = 1
        Me.TabPage_Log.Text = "Log"
        Me.TabPage_Log.UseVisualStyleBackColor = True
        '
        'RichTextBox_Log
        '
        Me.RichTextBox_Log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox_Log.Location = New System.Drawing.Point(8, 6)
        Me.RichTextBox_Log.Name = "RichTextBox_Log"
        Me.RichTextBox_Log.ReadOnly = True
        Me.RichTextBox_Log.Size = New System.Drawing.Size(579, 416)
        Me.RichTextBox_Log.TabIndex = 0
        Me.RichTextBox_Log.Text = ""
        '
        'TheNotifyIcon
        '
        Me.TheNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.TheNotifyIcon.ContextMenuStrip = Me.ContextMenuStrip_NotIcon
        Me.TheNotifyIcon.Icon = CType(resources.GetObject("TheNotifyIcon.Icon"), System.Drawing.Icon)
        Me.TheNotifyIcon.Text = "InternetCheck"
        Me.TheNotifyIcon.Visible = True
        '
        'ContextMenuStrip_NotIcon
        '
        Me.ContextMenuStrip_NotIcon.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowToolStripMenuItem, Me.StartCheckingToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip_NotIcon.Name = "ContextMenuStrip_NotIcon"
        Me.ContextMenuStrip_NotIcon.Size = New System.Drawing.Size(166, 92)
        '
        'ShowToolStripMenuItem
        '
        Me.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem"
        Me.ShowToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.ShowToolStripMenuItem.Text = "Show application"
        '
        'StartCheckingToolStripMenuItem
        '
        Me.StartCheckingToolStripMenuItem.Name = "StartCheckingToolStripMenuItem"
        Me.StartCheckingToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.StartCheckingToolStripMenuItem.Text = "Start checking"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConLostTSMI, Me.ConBackTSMI, Me.NotifyTSMI, Me.LogSaveTSMI, Me.AutostartTSMI, Me.StartMinTSMI, Me.WinStartTSMI})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ConLostTSMI
        '
        Me.ConLostTSMI.Name = "ConLostTSMI"
        Me.ConLostTSMI.Size = New System.Drawing.Size(321, 22)
        Me.ConLostTSMI.Text = "Connection lost sound"
        '
        'ConBackTSMI
        '
        Me.ConBackTSMI.Name = "ConBackTSMI"
        Me.ConBackTSMI.Size = New System.Drawing.Size(321, 22)
        Me.ConBackTSMI.Text = "Connection reestablished sound"
        '
        'NotifyTSMI
        '
        Me.NotifyTSMI.Name = "NotifyTSMI"
        Me.NotifyTSMI.Size = New System.Drawing.Size(321, 22)
        Me.NotifyTSMI.Text = "Show notification if connection status changes"
        '
        'LogSaveTSMI
        '
        Me.LogSaveTSMI.Name = "LogSaveTSMI"
        Me.LogSaveTSMI.Size = New System.Drawing.Size(321, 22)
        Me.LogSaveTSMI.Text = "Save log files"
        '
        'AutostartTSMI
        '
        Me.AutostartTSMI.Name = "AutostartTSMI"
        Me.AutostartTSMI.Size = New System.Drawing.Size(321, 22)
        Me.AutostartTSMI.Text = "Start checking connection status on launch"
        '
        'StartMinTSMI
        '
        Me.StartMinTSMI.Name = "StartMinTSMI"
        Me.StartMinTSMI.Size = New System.Drawing.Size(321, 22)
        Me.StartMinTSMI.Text = "Start minimized"
        '
        'WinStartTSMI
        '
        Me.WinStartTSMI.Name = "WinStartTSMI"
        Me.WinStartTSMI.Size = New System.Drawing.Size(321, 22)
        Me.WinStartTSMI.Text = "Start on Windows startup"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'TheTimer
        '
        Me.TheTimer.Interval = 12000
        '
        'TheBackgroundWorker
        '
        '
        'FolderBrowserDialog_Export
        '
        Me.FolderBrowserDialog_Export.Description = "Choose the folder where the file/s should be exported to"
        Me.FolderBrowserDialog_Export.RootFolder = System.Environment.SpecialFolder.Startup
        Me.FolderBrowserDialog_Export.UseDescriptionForTitle = True
        '
        'Timer_Minimize
        '
        Me.Timer_Minimize.Interval = 10
        '
        'Form_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(613, 468)
        Me.Controls.Add(Me.TabControl_Main)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(377, 347)
        Me.Name = "Form_Main"
        Me.Text = "InternetCheck"
        Me.ContextMenuStrip_ListView.ResumeLayout(False)
        Me.TabControl_Main.ResumeLayout(False)
        Me.TabPage_Overview.ResumeLayout(False)
        Me.TabPage_Overview.PerformLayout()
        Me.TabPage_Options.ResumeLayout(False)
        Me.TabPage_Options.PerformLayout()
        Me.TabPage_Log.ResumeLayout(False)
        Me.ContextMenuStrip_NotIcon.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView_Losses As ListView
    Friend WithEvents Label_Losses As Label
    Friend WithEvents Button_CheckButton As Button
    Friend WithEvents TabControl_Main As TabControl
    Friend WithEvents TabPage_Overview As TabPage
    Friend WithEvents TabPage_Log As TabPage
    Friend WithEvents RichTextBox_Log As RichTextBox
    Friend WithEvents TheNotifyIcon As NotifyIcon
    Friend WithEvents TheTimer As Timer
    Friend WithEvents TabPage_Options As TabPage
    Friend WithEvents TextBox_ConLost As TextBox
    Friend WithEvents TextBox_ConBack As TextBox
    Friend WithEvents CheckBox_ConBack As CheckBox
    Friend WithEvents CheckBox_ConLost As CheckBox
    Friend WithEvents CheckBox_WinStart As CheckBox
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents CheckBox_Notify As CheckBox
    Friend WithEvents CheckBox_LogSave As CheckBox
    Friend WithEvents Button_Debug As Button
    Friend WithEvents CheckBox_Autostart As CheckBox
    Friend WithEvents CheckBox_StartMin As CheckBox
    Friend WithEvents ContextMenuStrip_NotIcon As ContextMenuStrip
    Friend WithEvents ShowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConLostTSMI As ToolStripMenuItem
    Friend WithEvents ConBackTSMI As ToolStripMenuItem
    Friend WithEvents NotifyTSMI As ToolStripMenuItem
    Friend WithEvents LogSaveTSMI As ToolStripMenuItem
    Friend WithEvents AutostartTSMI As ToolStripMenuItem
    Friend WithEvents StartMinTSMI As ToolStripMenuItem
    Friend WithEvents WinStartTSMI As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox_Duration As TextBox
    Friend WithEvents Label_Dur2 As Label
    Friend WithEvents Label_Dur1 As Label
    Friend WithEvents TheBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ContextMenuStrip_ListView As ContextMenuStrip
    Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TojsonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TotxtToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteItemsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FolderBrowserDialog_Export As FolderBrowserDialog
    Friend WithEvents CopyToClipboardToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StartCheckingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer_Minimize As Timer
    Friend WithEvents TextBox_Search As TextBox
    Friend WithEvents Label_Search As Label
End Class
