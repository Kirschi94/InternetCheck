﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.Label_Losses = New System.Windows.Forms.Label()
        Me.Button_CheckButton = New System.Windows.Forms.Button()
        Me.TabControl_Main = New System.Windows.Forms.TabControl()
        Me.TabPage_Overview = New System.Windows.Forms.TabPage()
        Me.Button_Debug = New System.Windows.Forms.Button()
        Me.TabPage_Options = New System.Windows.Forms.TabPage()
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
        Me.NotifyIcon_ = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Timer_ = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl_Main.SuspendLayout()
        Me.TabPage_Overview.SuspendLayout()
        Me.TabPage_Options.SuspendLayout()
        Me.TabPage_Log.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView_Losses
        '
        Me.ListView_Losses.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView_Losses.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
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
        'Label_Losses
        '
        Me.Label_Losses.AutoSize = True
        Me.Label_Losses.Location = New System.Drawing.Point(8, 3)
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
        'Button_Debug
        '
        Me.Button_Debug.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button_Debug.Location = New System.Drawing.Point(8, 399)
        Me.Button_Debug.Name = "Button_Debug"
        Me.Button_Debug.Size = New System.Drawing.Size(223, 23)
        Me.Button_Debug.TabIndex = 4
        Me.Button_Debug.Text = "Lose Connection [DEBUG]"
        Me.Button_Debug.UseVisualStyleBackColor = True
        '
        'TabPage_Options
        '
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
        'CheckBox_Autostart
        '
        Me.CheckBox_Autostart.AutoSize = True
        Me.CheckBox_Autostart.Location = New System.Drawing.Point(8, 133)
        Me.CheckBox_Autostart.Name = "CheckBox_Autostart"
        Me.CheckBox_Autostart.Size = New System.Drawing.Size(409, 19)
        Me.CheckBox_Autostart.TabIndex = 7
        Me.CheckBox_Autostart.Text = "Automatically start checking the internet connection on program launch"
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
        Me.CheckBox_WinStart.Enabled = False
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
        'NotifyIcon_
        '
        Me.NotifyIcon_.Icon = CType(resources.GetObject("NotifyIcon_.Icon"), System.Drawing.Icon)
        Me.NotifyIcon_.Text = "InternetCheck"
        Me.NotifyIcon_.Visible = True
        '
        'Timer_
        '
        Me.Timer_.Interval = 12000
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
        Me.TabControl_Main.ResumeLayout(False)
        Me.TabPage_Overview.ResumeLayout(False)
        Me.TabPage_Overview.PerformLayout()
        Me.TabPage_Options.ResumeLayout(False)
        Me.TabPage_Options.PerformLayout()
        Me.TabPage_Log.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListView_Losses As ListView
    Friend WithEvents Label_Losses As Label
    Friend WithEvents Button_CheckButton As Button
    Friend WithEvents TabControl_Main As TabControl
    Friend WithEvents TabPage_Overview As TabPage
    Friend WithEvents TabPage_Log As TabPage
    Friend WithEvents RichTextBox_Log As RichTextBox
    Friend WithEvents NotifyIcon_ As NotifyIcon
    Friend WithEvents Timer_ As Timer
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
End Class
