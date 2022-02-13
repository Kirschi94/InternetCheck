Imports System.ComponentModel
Imports System.Net.NetworkInformation

Public Class Form_Main
    Dim Lost_Connection As Boolean = False
    Dim TempAbbruch As Abbruch = Nothing
    Dim ListOfAbbruch As New List(Of Abbruch)
    Dim Abbruchtest As Boolean = False
    Dim Starting As Boolean = True
    Dim NonUserAction As Boolean = False
    Public Shared Wave1 As New NAudio.Wave.WaveOut
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_CheckButton.Click
        Select Case Button_CheckButton.Text
            Case "Start checking"
                DeReActivatedChecking(True)
                AddToLog("Started monitoring the internet connection.")
            Case "Stop checking"
                TheNotifyIcon.Icon = My.Resources.I_4b
                DeReActivatedChecking(False)
                AddToLog("Stopped monitoring the internet connection.")
            Case Else
                Throw New NotImplementedException("This should not be able to happen at all. Please contact me asap.")
        End Select
    End Sub

    Private Sub DeReActivatedChecking(state As Boolean)
        TheTimer.Enabled = state
        Button_CheckButton.Text = If(state, "Stop checking", "Start checking")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TheTimer.Tick
        CheckConnection()
    End Sub

    Private Sub CheckConnection()
        If Abbruchtest Then
            Threading.Thread.Sleep(1000)
            Connection_Lost()
        Else
            If Not SingleCheck("google.com") Then
                TheNotifyIcon.Icon = My.Resources.I_4_g
                If Not SingleCheck("microsoft.com") Then
                    AddToLog("Issues connecting to the internet.")
                    If Not SingleCheck("gmx.com") Then
                        If Not SingleCheck("bing.com") Then
                            Connection_Lost()
                        Else
                            Connection_Existent()
                            AddToLog("Issues resolved.")
                        End If
                    Else
                        Connection_Existent()
                        AddToLog("Issues resolved.")
                    End If
                Else
                    Connection_Existent()
                End If
            Else
                Connection_Existent()
            End If
        End If
    End Sub

    Private Sub Connection_Lost()
        TheNotifyIcon.Icon = My.Resources.I_4_r
        If Not Lost_Connection Then
            Lost_Connection = True
            TempAbbruch = New Abbruch(DateTime.Now)
            AddToLog("Internet connection has been lost.")
            If CheckBox_ConLost.Checked Then PlaySound(True)
            If CheckBox_Notify.Checked Then ShowBalloonTip("Internet connection lost", "Internet connection has been lost.")
        End If
    End Sub

    Private Sub ShowBalloonTip(Optional Title As String = "", Optional Text As String = "")
        TheNotifyIcon.BalloonTipTitle = Title
        TheNotifyIcon.BalloonTipText = Text
        TheNotifyIcon.ShowBalloonTip(8000)
    End Sub

    Private Sub PlayStandard(Lost As Boolean)
        Try
            If Lost Then
                Wave1.Stop()
                Dim file As String = Application.StartupPath & "\Audio\Con_Lost1.mp3"
                Dim data As New NAudio.Wave.Mp3FileReader(file)
                Wave1.Init(data)
                Wave1.Play()
            Else
                Wave1.Stop()
                Dim file As String = Application.StartupPath & "\Audio\Con_Back1.mp3"
                Dim data As New NAudio.Wave.Mp3FileReader(file)
                Wave1.Init(data)
                Wave1.Play()
            End If
        Catch ex As Exception
            MessageBox.Show($"Standard Audiofile could not be played.{vbCrLf}The following error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub PlaySound(Lost As Boolean)
        If Lost Then
            If Not TextBox_ConLost.Text = "" AndAlso Not IsNothing(TextBox_ConLost.Text) Then
                Try
                    Wave1.Stop()
                    Dim file As String = TextBox_ConLost.Text
                    Dim data As New NAudio.Wave.Mp3FileReader(file)
                    Wave1.Init(data)
                    Wave1.Play()
                Catch ex As Exception
                    PlayStandard(Lost)
                End Try
            Else
                PlayStandard(Lost)
            End If
        Else
            If Not TextBox_ConBack.Text = "" AndAlso Not IsNothing(TextBox_ConBack.Text) Then
                Try
                    Wave1.Stop()
                    Dim file As String = TextBox_ConBack.Text
                    Dim data As New NAudio.Wave.Mp3FileReader(file)
                    Wave1.Init(data)
                    Wave1.Play()
                Catch ex As Exception
                    PlayStandard(Lost)
                End Try
            Else
                PlayStandard(Lost)
            End If
        End If
    End Sub

    Private Sub AddToLog(ByVal Text As String)
        Dim Output As String = $"[{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH:mm:ss}] {Text}"
        RichTextBox_Log.Text = Output & vbCrLf & RichTextBox_Log.Text
    End Sub

    Private Sub Connection_Existent()
        TheNotifyIcon.Icon = My.Resources.I_4_gr
        If Lost_Connection AndAlso Not IsNothing(TempAbbruch) Then
            TempAbbruch.Set_End(DateTime.Now)
            Lost_Connection = False
            If TempAbbruch.Dauer.TotalMinutes > 2 Then ListOfAbbruch.Add(TempAbbruch.Clone())
            TempAbbruch = Nothing
            AddToLog("Internet connection has been reestablished.")
            If CheckBox_ConBack.Checked Then PlaySound(False)
            If CheckBox_Notify.Checked Then ShowBalloonTip("Internet connection reestablished", "Internet connection has been reestablished.")
        End If
    End Sub

    Private Sub Update_Listview()
        ListView_Losses.Items.Clear()
        For Each Itum In ListOfAbbruch
            With ListView_Losses.Items.Add($"{Itum.Anfang:yyyy-MM-dd}, {Itum.Anfang:HH:mm:ss}")
                .SubItems.Add($"{Itum.Ende:yyyy-MM-dd}, {Itum.Ende:HH:mm:ss}")
                .SubItems.Add($"{Duration_Stringbuilder(Itum.Dauer)}")
            End With
        Next
    End Sub

    Private Function Duration_Stringbuilder(Dauer As TimeSpan)
        Dim Output As String = ""
        If Dauer.Days > 0 Then
            Output &= String.Format("{0:%d} days, {0:%h} hours, {0:%m} minutes", Dauer)
        ElseIf Dauer.Hours > 0 Then
            Output &= String.Format("{0:%h} hours, {0:%m} minutes", Dauer)
        Else
            Output &= String.Format("{0:%m} minutes", Dauer)
        End If
        If Dauer.Seconds > 0 Then
            Output &= String.Format(", {0:%s} seconds", Dauer)
        End If
        Return Output
    End Function

    Private Function SingleCheck(host As String)
        Try
            Dim myPing As New Ping()
            Dim buffer As Byte() = New Byte(31) {}
            Dim timeout As Integer = 1000
            Dim pingOptions As New PingOptions()
            Dim reply As PingReply = myPing.Send(host, timeout, buffer, pingOptions)
            Return (reply.Status = IPStatus.Success)
        Catch
            Return False
        End Try
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddToLog("Application starting..")

        Try
            Load_And_Apply_ini()
            AddToLog("Options loaded.")
        Catch ex As Exception
            AddToLog("Options could not be loaded.")
            If IO.File.Exists(Application.StartupPath & "\options.ini") Then
                MessageBox.Show("Options file could not be read properly. Some saved options might not have been applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else IO.File.Create(Application.StartupPath & "\options.ini") : AddToLog("Options file did not exist and was created.")
            End If
        End Try

        Try
            Load_Abbrüche()
            AddToLog("Connectionlog loaded.")
        Catch ex As Exception
            AddToLog("Connectionlog could not be loaded.")
            If IO.File.Exists(Application.StartupPath & "\connectionlog.bin") Then
                MessageBox.Show("Connectionlog file could not be read properly. Some saved options might not have been applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else IO.File.Create(Application.StartupPath & "\connectionlog.bin") : AddToLog("Connectionlog file did not exist and was created.")
            End If
        End Try

        Update_Listview()
        If CheckBox_StartMin.Checked Then WindowState = FormWindowState.Minimized : Visible = False
        If CheckBox_Autostart.Checked Then Button1_Click(Nothing, Nothing)
        Starting = False
    End Sub

    Private Sub Form_Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AddToLog("Application stopping..")
        Wave1.Dispose()

        Try
            Save_ini()
            AddToLog("Options saved.")
        Catch ex As Exception
            AddToLog("Options could not be saved.")
        End Try

        Try
            Save_log()
        Catch ex As Exception
            Dim TempCount As Integer = 0
            While MessageBox.Show("Logfile could not be saved.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry
                Save_log(Application.StartupPath & $"\Logs\{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH.mm.ss}_{TempCount}.log")
                TempCount += 1
            End While
        End Try

        Try
            Save_Abbrüche()
        Catch ex As Exception
            Dim TempCount As Integer = 0
            While MessageBox.Show("Connectionlog could not be saved.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry
                Save_log(Application.StartupPath & $"\connectionslog_{TempCount}.bin")
                TempCount += 1
            End While
        End Try
    End Sub

    Private Sub Save_ini()
        Dim iniString As String = ""
        iniString &= $"Connection lost Sound:""{TextBox_ConLost.Text}""{vbCrLf}"
        iniString &= $"Connection reestablished Sound:""{TextBox_ConBack.Text}""{vbCrLf}"
        iniString &= $"Play connection lost Sound:{CheckBox_ConLost.Checked}{vbCrLf}"
        iniString &= $"Play connection reestablished Sound:{CheckBox_ConBack.Checked}{vbCrLf}"
        iniString &= $"Notify if connection status changes:{CheckBox_Notify.Checked}{vbCrLf}"
        iniString &= $"Save log files:{CheckBox_LogSave.Checked}{vbCrLf}"
        iniString &= $"Start on Windows startup:{CheckBox_WinStart.Checked}{vbCrLf}"
        iniString &= $"Start checking on program startup:{CheckBox_Autostart.Checked}{vbCrLf}"
        iniString &= $"Start minimized:{CheckBox_StartMin.Checked}{vbCrLf}"
        iniString &= $"Windowsize:{Size.Width},{Size.Height}{vbCrLf}"
        iniString &= $"Windowposition:{Location.X},{Location.Y}{vbCrLf}"

        IO.File.WriteAllText(Application.StartupPath & "\options.ini", iniString)
    End Sub

    Private Sub Save_log(Optional Pfad As String = Nothing)
        If Not IO.Directory.Exists(Application.StartupPath & "\Logs") Then IO.Directory.CreateDirectory(Application.StartupPath & "\Logs")
        If IsNothing(Pfad) Then Pfad = Application.StartupPath & $"\Logs\{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH.mm.ss}.log"
        If CheckBox_LogSave.Checked Then IO.File.WriteAllText(Pfad, RichTextBox_Log.Text)
    End Sub

    Private Sub Load_And_Apply_ini(Optional Pfad As String = Nothing)
        If IsNothing(Pfad) Then Pfad = Application.StartupPath & "\options.ini"
        Dim iniLines As String() = IO.File.ReadAllLines(Pfad)
        Dim EmptyLineCounter As Integer = 0
        For Each Line In iniLines
            If Not Line.Length = 0 AndAlso Not Line = "" AndAlso Not IsNothing(Line) Then
                If Line.StartsWith("Connection lost Sound:") Then TextBox_ConLost.Text = Line.Substring(23, Line.Length - (23 + 1)) : Continue For
                If Line.StartsWith("Connection reestablished Sound:") Then TextBox_ConBack.Text = Line.Substring(32, Line.Length - (32 + 1)) : Continue For
                If Line.StartsWith("Play connection lost Sound:") Then CheckBox_ConLost.Checked = Line.Substring(27, Line.Length - (27)) : _
                    ConLostTSMI.Checked = CheckBox_ConLost.Checked : Continue For
                If Line.StartsWith("Play connection reestablished Sound:") Then CheckBox_ConBack.Checked = Line.Substring(36, Line.Length - (36)) : _
                    ConBackTSMI.Checked = CheckBox_ConBack.Checked : Continue For
                If Line.StartsWith("Notify if connection status changes:") Then CheckBox_Notify.Checked = Line.Substring(36, Line.Length - (36)) : _
                    NotifyTSMI.Checked = CheckBox_Notify.Checked : Continue For
                If Line.StartsWith("Save log files:") Then CheckBox_LogSave.Checked = Line.Substring(15, Line.Length - (15)) : _
                    LogSaveTSMI.Checked = CheckBox_LogSave.Checked : Continue For
                If Line.StartsWith("Start on Windows startup:") Then CheckBox_WinStart.Checked = Line.Substring(25, Line.Length - (25)) : _
                    WinStartTSMI.Checked = CheckBox_WinStart.Checked : Continue For
                If Line.StartsWith("Start checking on program startup:") Then CheckBox_Autostart.Checked = Line.Substring(34, Line.Length - (34)) : _
                    AutostartTSMI.Checked = CheckBox_Autostart.Checked : Continue For
                If Line.StartsWith("Start minimized:") Then CheckBox_StartMin.Checked = Line.Substring(16, Line.Length - (16)) : _
                    StartMinTSMI.Checked = CheckBox_StartMin.Checked : Continue For
                If Line.StartsWith("Windowsize:") Then Size = New Size(Line.Substring(11, Line.IndexOf(",") - (11)), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
                If Line.StartsWith("Windowposition:") Then Location = New Point(Line.Substring(15, Line.IndexOf(",") - (15)), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
                If Location.X < 0 OrElse Location.Y < 0 Then Location = New Point(0, 0)
                'EmptyLineCounter += 1
            Else
                EmptyLineCounter += 1
            End If
        Next
        If (iniLines.Length - EmptyLineCounter) < 11 Then MessageBox.Show("Options file could not be read properly. Some saved options might not have been applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Save_Abbrüche()
        Dim AbbrString As String = ""
        If Not ListOfAbbruch.Count = 0 Then
            For Each Artikel In ListOfAbbruch
                AbbrString &= $"{Artikel.Anfang.Ticks};{Artikel.Dauer.Ticks}{vbCrLf}"
            Next
        End If
        IO.File.WriteAllText(Application.StartupPath & "\connectionlog.bin", AbbrString)
    End Sub

    Private Sub Load_Abbrüche(Optional Pfad As String = Nothing)
        If IsNothing(Pfad) Then Pfad = Application.StartupPath & "\connectionlog.bin"
        Dim AbbrLines As String() = IO.File.ReadAllLines(Pfad)
        For Each Line In AbbrLines
            If Not Line = "" AndAlso Not IsNothing(Line) AndAlso Line.Contains(";") Then _
                ListOfAbbruch.Add(New Abbruch(New DateTime(Convert.ToInt64(Line.Substring(0, Line.IndexOf(";")))), New TimeSpan(Convert.ToInt64(Line.Substring(Line.IndexOf(";") + 1, Line.Length - (Line.IndexOf(";") + 1))))))
        Next
    End Sub

    Private Sub Button_Debug_Click(sender As Object, e As EventArgs) Handles Button_Debug.Click
        If Abbruchtest Then
            Button_Debug.Text = "Lose Connection [DEBUG]"
            Abbruchtest = False
        Else
            Button_Debug.Text = "Reestablish Connection [DEBUG]"
            Abbruchtest = True
        End If
    End Sub

    Private Sub Form_Main_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If WindowState = FormWindowState.Minimized Then
            Visible = False
        Else Visible = True
        End If
    End Sub

    Private Sub TheNotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles TheNotifyIcon.DoubleClick
        Show()
        WindowState = FormWindowState.Normal
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        Show()
        WindowState = FormWindowState.Normal
    End Sub

#Region "CheckedChanged handeling"
    Private Sub CheckedChanged(TheCheckbox As CheckBox, TSMI As ToolStripMenuItem, Optional FromCheckbox As Boolean = False)
        If Not Starting Then
            If FromCheckbox Then
                NonUserAction = True
                If TSMI.Checked Then
                    TheCheckbox.Checked = False
                    TSMI.Checked = False
                Else
                    TheCheckbox.Checked = True
                    TSMI.Checked = True
                End If
            Else
                NonUserAction = True
                If TheCheckbox.Checked Then
                    TheCheckbox.Checked = False
                    TSMI.Checked = False
                Else
                    TheCheckbox.Checked = True
                    TSMI.Checked = True
                End If
            End If
        End If
    End Sub
    Private Sub ConLostTSMI_Click(sender As Object, e As EventArgs) Handles ConLostTSMI.Click
        CheckedChanged(CheckBox_ConLost, ConLostTSMI)
    End Sub

    Private Sub ConBackTSMI_Click(sender As Object, e As EventArgs) Handles ConBackTSMI.Click
        CheckedChanged(CheckBox_ConBack, ConBackTSMI)
    End Sub

    Private Sub NotifyTSMI_Click(sender As Object, e As EventArgs) Handles NotifyTSMI.Click
        CheckedChanged(CheckBox_Notify, NotifyTSMI)
    End Sub

    Private Sub LogSaveTSMI_Click(sender As Object, e As EventArgs) Handles LogSaveTSMI.Click
        CheckedChanged(CheckBox_LogSave, LogSaveTSMI)
    End Sub

    Private Sub AutostartTSMI_Click(sender As Object, e As EventArgs) Handles AutostartTSMI.Click
        CheckedChanged(CheckBox_Autostart, AutostartTSMI)
    End Sub

    Private Sub StartMinTSMI_Click(sender As Object, e As EventArgs) Handles StartMinTSMI.Click
        CheckedChanged(CheckBox_StartMin, StartMinTSMI)
    End Sub

    Private Sub WinStartTSMI_Click(sender As Object, e As EventArgs) Handles WinStartTSMI.Click
        CheckedChanged(CheckBox_WinStart, WinStartTSMI)
    End Sub

    Private Sub CheckBox_Autostart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Autostart.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_Autostart, AutostartTSMI, True)
        End If
    End Sub

    Private Sub CheckBox_ConBack_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ConBack.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_ConBack, ConBackTSMI, True)
            NonUserAction = False
        End If
    End Sub

    Private Sub CheckBox_ConLost_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ConLost.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_ConLost, ConLostTSMI, True)
            NonUserAction = False
        End If
    End Sub

    Private Sub CheckBox_LogSave_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_LogSave.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_LogSave, LogSaveTSMI, True)
            NonUserAction = False
        End If
    End Sub

    Private Sub CheckBox_Notify_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Notify.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_Notify, NotifyTSMI, True)
            NonUserAction = False
        End If
    End Sub

    Private Sub CheckBox_StartMin_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_StartMin.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_StartMin, StartMinTSMI, True)
            NonUserAction = False
        End If
    End Sub

    Private Sub CheckBox_WinStart_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_WinStart.CheckedChanged
        If NonUserAction Then
            NonUserAction = False
        Else
            CheckedChanged(CheckBox_WinStart, WinStartTSMI, True)
            NonUserAction = False
        End If
    End Sub
#End Region
End Class
