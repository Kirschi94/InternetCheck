Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports System.Threading
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Class Form_Main
#Region "Global Variables"
    Dim Lost_Connection As Boolean = False
    Dim TempAbbruch As Abbruch = Nothing
    Dim ListOfAbbruch As New List(Of Abbruch)
    Dim Abbruchtest As Boolean = False
    Dim Starting As Boolean = True
    Dim NonUserAction As Boolean = False
    Dim LastText As String = ""
    Dim FolderPathJson As String = ""
    Dim FolderPathTxt As String = ""
    Dim OmaeWaMouShindeiru As Boolean = False
    Public Shared Wave1 As New NAudio.Wave.WaveOut
    Dim LastGoodLocation As New Point(0, 0)
    Dim LastKnownTimespan As New TimeSpan
#End Region
#Region "DLL-Imports"
    Private Const SW_RESTORE As Integer = 9
    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Declare Function ShowWindowAsync Lib "user32" _
         (ByVal hWnd As Long,
          ByVal nCmdShow As Long) As Long

    Private Declare Auto Function IsIconic Lib "user32.dll" (ByVal hwnd As IntPtr) As Boolean

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function ShowWindow(ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Boolean
    End Function

    Private Declare Function IsWindowVisible Lib "user32" _
    Alias "IsWindowVisible" (ByVal hwnd As Long) As Long
    'Dim i As IntPtr = (Int64)Handle
#End Region
#Region "Connection Management"
    Private Sub CheckConnection()
        If Abbruchtest Then
            Threading.Thread.Sleep(1000)
            Connection_Lost()
        Else
            Dim DT As DateTime = DateTime.Now
            If Not SingleCheck("google.com") Then
                If Not Lost_Connection Then TheNotifyIcon.Icon = My.Resources.I_4_g
                If Not SingleCheck("microsoft.com") Then
                    If Not Lost_Connection Then AddToLog("Issues connecting to the internet.", DT)
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

    Private Sub Connection_Existent()
        TheNotifyIcon.Icon = My.Resources.I_4_gr
        If Lost_Connection AndAlso Not IsNothing(TempAbbruch) Then
            Lost_Connection = False
            Save_Abbruch()
            TempAbbruch = Nothing
            Update_Listview()
            AddToLog("Internet connection has been reestablished.")
            If CheckBox_ConBack.Checked Then PlaySound(False)
            If CheckBox_Notify.Checked Then ShowBalloonTip("Internet connection reestablished", "Internet connection has been reestablished.")
        End If
    End Sub
#End Region
#Region "Audio Playback"
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
            AddToLog($"Failed in playing the standard sound. The following error occurred: {ex.Message}")
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
#End Region
#Region "Visuals and remainder"
    Private Sub Update_Listview(Optional SearchPara As String = "")
        ListView_Losses.Items.Clear()
        Dim TempTime As New TimeSpan(0)
        For Each Itum In ListOfAbbruch
            Dim TempItem As New ListViewItem($"{Itum.Anfang:yyyy-MM-dd}, {Itum.Anfang:HH:mm:ss}")
            With TempItem
                .SubItems.Add($"{Itum.Ende:yyyy-MM-dd}, {Itum.Ende:HH:mm:ss}")
                .SubItems.Add($"{Duration_Stringbuilder(Itum.Dauer)}")
                .SubItems.Add($"{Itum.Anfang.Ticks}")
            End With
            TempTime += Itum.Dauer

            If SearchPara = "" Then
                ListView_Losses.Items.Add(TempItem)
            Else
                If TempItem.Text.Contains(SearchPara) OrElse TempItem.SubItems(1).Text.Contains(SearchPara) _
                    OrElse TempItem.SubItems(2).Text.Contains(SearchPara) Then
                    ListView_Losses.Items.Add(TempItem)
                Else
                    TempItem = Nothing
                    TempTime -= Itum.Dauer
                End If
            End If
        Next
        LastKnownTimespan = TempTime
        Update_Label(TempTime)
    End Sub

    Private Sub Update_Label(Dauer As TimeSpan)
        Label_LostTime.Left = Button_CheckButton.Left - (Label_LostTime.Width + 2)

        If Dauer.TotalSeconds <= 0 Then
            Label_LostTime.Text = "0 seconds of connection issues."
        Else
            Label_LostTime.Text = $"{ListView_Losses.Items.Count} losses, "
            If Dauer.Days > 0 Then Label_LostTime.Text &= $"{Dauer.Days} days, "
            If Dauer.Hours > 0 Then Label_LostTime.Text &= $"{Dauer.Hours} hours, "
            If Dauer.Minutes > 0 Then Label_LostTime.Text &= $"{Dauer.Minutes} minutes, "
            If Dauer.Seconds > 0 Then Label_LostTime.Text &= $"{Dauer.Seconds} seconds, "

            Label_LostTime.Text = Label_LostTime.Text.Substring(0, Label_LostTime.Text.Length - 2) & " lost."
        End If
        Label_LostTime.Left = Button_CheckButton.Left - (Label_LostTime.Width + 2)

        If Label_LostTime.Left < (TextBox_Search.Left + TextBox_Search.Width) Then
            Label_LostTime.Text = $""
            If Dauer.Days > 0 Then Label_LostTime.Text &= $"{Dauer.Days} days, "
            If Dauer.Hours > 0 Then Label_LostTime.Text &= $"{Dauer.Hours} hours, "
            If Dauer.Minutes > 0 Then Label_LostTime.Text &= $"{Dauer.Minutes} minutes, "
            If Dauer.Seconds > 0 Then Label_LostTime.Text &= $"{Dauer.Seconds} seconds, "

            Label_LostTime.Text = Label_LostTime.Text.Substring(0, Label_LostTime.Text.Length - 2) & " lost."
        End If
        Label_LostTime.Left = Button_CheckButton.Left - (Label_LostTime.Width + 2)

        If Label_LostTime.Left < (TextBox_Search.Left + TextBox_Search.Width) Then
            Label_LostTime.Text = $"{Math.Round(Dauer.TotalSeconds, 2)} seconds lost."
            If Dauer.TotalMinutes >= 1.5 Then Label_LostTime.Text = $"{Math.Round(Dauer.TotalMinutes, 2)} minutes lost."
            If Dauer.TotalHours >= 2 Then Label_LostTime.Text = $"{Math.Round(Dauer.TotalHours, 2)} hours lost."
            If Dauer.TotalDays >= 3 Then Label_LostTime.Text = $"{Math.Round(Dauer.TotalDays, 2)} days lost."
            Label_LostTime.Left = Button_CheckButton.Left - (Label_LostTime.Width + 2)
        End If
    End Sub

    Private Sub ShowBalloonTip(Optional Title As String = "", Optional Text As String = "")
        TheNotifyIcon.BalloonTipTitle = Title
        TheNotifyIcon.BalloonTipText = Text
        TheNotifyIcon.ShowBalloonTip(8000)
    End Sub

    Private Sub DeReActivatedChecking(state As Boolean)
        TheTimer.Enabled = state
        Button_CheckButton.Text = If(state, "Stop checking", "Start checking")
    End Sub

    Private Sub AddToLog(ByVal Text As String, Optional DT As DateTime = Nothing)
        If IsNothing(DT) Or DT.Ticks < 100 Then DT = DateTime.Now
        Dim Output As String = $"[{DT:yyyy-MM-dd}, {DT:HH:mm:ss}] {Text}"
        RichTextBox_Log.Text = Output & vbCrLf & RichTextBox_Log.Text
    End Sub

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

    Private Sub CMS_LV_DeActivate(MoreThanZero As Boolean)
        DeleteItemsToolStripMenuItem.Enabled = MoreThanZero
        ExportToolStripMenuItem.Enabled = MoreThanZero
        TojsonToolStripMenuItem.Enabled = MoreThanZero
        TotxtToolStripMenuItem.Enabled = MoreThanZero
        CopyToClipboardToolStripMenuItem.Enabled = MoreThanZero
    End Sub

    Private Function GetSelectedItems()
        Dim TempListOfAbbruch As New List(Of Abbruch)
        For Each TheItem As ListViewItem In ListView_Losses.SelectedItems
            Dim TheDate As New DateTime(TheItem.SubItems(3).Text)
            For Each RealItem In ListOfAbbruch
                If TheDate = RealItem.Anfang Then TempListOfAbbruch.Add(RealItem) : Exit For
            Next
        Next
        Return TempListOfAbbruch
    End Function

    Private Sub ExportToJson(Path As String)
        If Path = "" Or IsNothing(Path) Or Not IO.Directory.Exists(Path) Then
            IO.Directory.CreateDirectory(Application.StartupPath & "Jsons\")
            Path = Application.StartupPath & "Jsons\"
        End If
        If Not Path.EndsWith("\") Then Path &= "\"

        Dim TempListOA As List(Of Abbruch) = GetSelectedItems()
        For Each TheItem As Abbruch In TempListOA
            IO.File.WriteAllText(Path & $"{TheItem.Anfang:yyyy-MM-dd}, {TheItem.Anfang:HH.mm.ss}.json", TheItem.ToJson())
        Next
        MessageBox.Show($"File/s successfully exported to {Path}.", "Task completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
        AddToLog($"Exported {TempListOA.Count} items to json.")
    End Sub

    Private Sub ExportToTxtFile(Path As String)
        If Path = "" Or IsNothing(Path) Or Not IO.Directory.Exists(Path) Then
            IO.Directory.CreateDirectory(Application.StartupPath & "Txts\")
            Path = Application.StartupPath & "Txts\"
        End If
        If Not Path.EndsWith("\") Then Path &= "\"

        For Each TheItem As ListViewItem In ListView_Losses.SelectedItems
            Dim TheDate As New DateTime(TheItem.SubItems(3).Text)
            IO.File.WriteAllText(Path & $"{TheDate:yyyy-MM-dd}, {TheDate:HH.mm.ss}.txt", GetTextFromItem(TheItem))
        Next
        MessageBox.Show($"File/s successfully exported to {Path}.", "Task completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
        AddToLog($"Exported {ListView_Losses.SelectedItems.Count} items to txt.")
    End Sub

    Private Function GetTextFromItem(TheItem As ListViewItem)
        Return $"Beginning: {TheItem.SubItems(0).Text}{vbCrLf}End: {TheItem.SubItems(1).Text}{vbCrLf}Duration: {TheItem.SubItems(2).Text}"
    End Function

    Private Sub CheckForOtherInstance()
        Dim _process() As Process
        _process = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)
        Dim proc As Process = Process.GetCurrentProcess()
        Dim assemblyName As String = Process.GetCurrentProcess().ProcessName
        For Each otherProc As Process In Process.GetProcessesByName(assemblyName)
            If Not proc.Id = otherProc.Id Then
                Dim hWnd As IntPtr = otherProc.MainWindowHandle
                If IsIconic(hWnd) Then
                    ShowWindow(hWnd, SW_RESTORE)
                End If
                SetForegroundWindow(hWnd)
                Exit For
            End If
        Next
        If _process.Length > 1 Then OmaeWaMouShindeiru = True : _
            MessageBox.Show($"The program is already opened.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning) : _
            Application.Exit() : Close() : Process.GetCurrentProcess().Kill()
    End Sub
#End Region
#Region "Application start and stop"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForOtherInstance()

        If Not OmaeWaMouShindeiru Then
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
            If CheckBox_StartMin.Checked Then Visible = False

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
            If CheckBox_StartMin.Checked Then Timer_Minimize.Enabled = True
            If CheckBox_Autostart.Checked Then Button1_Click(Nothing, Nothing)

            Dim TempBool As Boolean = CheckForStartup()
            CheckBox_WinStart.Checked = TempBool
            WinStartTSMI.Checked = TempBool

            Starting = False
        Else Visible = False
        End If
    End Sub

    Private Sub Form_Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not Starting And Not OmaeWaMouShindeiru Then
            If (TheTimer.Enabled AndAlso
            MessageBox.Show("Do you really want to close the application?", "InternetCheck", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) _
            Or Not TheTimer.Enabled Then

                If TheTimer.Enabled Then Button1_Click(Nothing, Nothing)

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
            Else
                e.Cancel = True
            End If
        End If
    End Sub
#End Region
#Region "Start on Windows start"
    Private Sub AddToStartup()
        Dim CU As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run")
        CU.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        CU.SetValue("InternetCheck", Application.ExecutablePath.Replace(".dll", ".exe"))
        CU.Close()
    End Sub

    Private Sub RemoveFromStartup()
        Dim CU As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run")
        CU.DeleteValue("InternetCheck")
    End Sub

    Private Function CheckForStartup()
        Dim CU As RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run")
        CU.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        If IsNothing(CU.GetValue("InternetCheck")) Then Return False
        If CU.GetValue("InternetCheck") = Application.ExecutablePath.Replace(".dll", ".exe") Then Return True
        Return False
    End Function
#End Region
#Region "Saving and Loading"
    Private Sub Save_Abbruch()
        TempAbbruch.Set_End(DateTime.Now)
        Try
            If TempAbbruch.Dauer.TotalMinutes > Convert.ToDouble(TextBox_Duration.Text) Then ListOfAbbruch.Add(TempAbbruch.Clone())
            Try
                Save_Abbrüche()
            Catch ex As Exception
                Dim TempCount As Integer = 0
                While MessageBox.Show("Connectionlog could not be saved.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry
                    Save_log(Application.StartupPath & $"\connectionslog_{TempCount}.bin")
                    TempCount += 1
                End While
            End Try
        Catch
            ListOfAbbruch.Add(TempAbbruch.Clone())
            Try
                Save_Abbrüche()
            Catch ex As Exception
                Dim TempCount As Integer = 0
                While MessageBox.Show("Connectionlog could not be saved.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = DialogResult.Retry
                    Save_log(Application.StartupPath & $"\connectionslog_{TempCount}.bin")
                    TempCount += 1
                End While
            End Try
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
        iniString &= $"Start checking on program startup:{CheckBox_Autostart.Checked}{vbCrLf}"
        iniString &= $"Start minimized:{CheckBox_StartMin.Checked}{vbCrLf}"
        iniString &= $"Connectionlog min-duration:""{TextBox_Duration.Text}""{vbCrLf}"
        iniString &= $"txt files folder:""{FolderPathTxt}""{vbCrLf}"
        iniString &= $"json files folder:""{FolderPathJson}""{vbCrLf}"
        iniString &= $"Windowsize:{Size.Width},{Size.Height}{vbCrLf}"
        iniString &= $"Windowposition:{LastGoodLocation.X},{LastGoodLocation.Y}{vbCrLf}"
        iniString &= $"Columnwidths:{ListView_Losses.Columns.Item(0).Width},{ListView_Losses.Columns.Item(1).Width},{ListView_Losses.Columns.Item(2).Width}{vbCrLf}"

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
                If Line.StartsWith("Start checking on program startup:") Then CheckBox_Autostart.Checked = Line.Substring(34, Line.Length - (34)) : _
                    AutostartTSMI.Checked = CheckBox_Autostart.Checked : Continue For
                If Line.StartsWith("Start minimized:") Then CheckBox_StartMin.Checked = Line.Substring(16, Line.Length - (16)) : _
                    StartMinTSMI.Checked = CheckBox_StartMin.Checked : Continue For
                If Line.StartsWith("Connectionlog min-duration:") Then TextBox_Duration.Text = Line.Substring(28, Line.Length - (28 + 1)) : Continue For
                If Line.StartsWith("txt files folder:") Then FolderPathTxt = Line.Substring(18, Line.Length - (18 + 1)) : Continue For
                If Line.StartsWith("json files folder:") Then FolderPathTxt = Line.Substring(19, Line.Length - (19 + 1)) : Continue For
                If Line.StartsWith("Windowsize:") Then Size = New Size(Line.Substring(11, Line.IndexOf(",") - (11)), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
                If Line.StartsWith("Windowposition:") Then Location = New Point(Line.Substring(15, Line.IndexOf(",") - (15)), Line.Substring(Line.IndexOf(",") + 1)) : _
                    If Convert.ToInt32(Line.Substring(15, Line.IndexOf(",") - (15))) < 0 OrElse Convert.ToInt32(Line.Substring(Line.IndexOf(",") + 1)) < 0 Then Location = New Point(0, 0) : Continue For
                If Line.StartsWith("Columnwidths:") Then ListView_Losses.Columns.Item(0).Width = Line.Substring(Line.IndexOf(":") + 1, Line.IndexOf(",") - (Line.IndexOf(":") + 1)) : _
                    ListView_Losses.Columns.Item(1).Width = Line.Substring(Line.IndexOf(",") + 1, Line.LastIndexOf(",") - (Line.IndexOf(",") + 1)) : _
                    ListView_Losses.Columns.Item(2).Width = Line.Substring(Line.LastIndexOf(",") + 1, Line.Length - (Line.LastIndexOf(",") + 1)) : Continue For
            Else
                EmptyLineCounter += 1
                End If
                Next
        If (iniLines.Length - EmptyLineCounter) < 14 Then MessageBox.Show("Options file could not be read properly. Some saved options might not have been applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
#End Region
#Region "Windows Forms Controls"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_CheckButton.Click
        Select Case Button_CheckButton.Text
            Case "Start checking"
                DeReActivatedChecking(True)
                AddToLog("Started monitoring the internet connection.")
            Case "Stop checking"
                TheNotifyIcon.Icon = My.Resources.I_4b
                DeReActivatedChecking(False)
                AddToLog("Stopped monitoring the internet connection.")
                If Lost_Connection Then
                    Save_Abbruch()
                    AddToLog("The connection was still lost when monitoring was stopped! The date for reestablishing the connection in this loss is therefore specified too early!")
                End If
            Case Else
                Throw New NotImplementedException("This should not be able to happen at all. Please contact me asap.")
        End Select
        StartCheckingToolStripMenuItem.Text = Button_CheckButton.Text
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

    Private Sub TheTimer_Tick(sender As Object, e As EventArgs) Handles TheTimer.Tick
        If Not TheBackgroundWorker.IsBusy Then TheBackgroundWorker.RunWorkerAsync()
    End Sub

    Private Sub TextBox_Duration_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Duration.TextChanged
        Try
            If IsNumeric(TextBox_Duration.Text) Then
                Dim p = 2 + TextBox_Duration.Text
                LastText = TextBox_Duration.Text
            Else
                TextBox_Duration.Text = LastText
            End If
        Catch
            TextBox_Duration.Text = LastText
            Try
                If IsNumeric(TextBox_Duration.Text) Then
                    Dim p = 2 + TextBox_Duration.Text
                    LastText = TextBox_Duration.Text
                Else
                    TextBox_Duration.Text = LastText
                End If
            Catch
                TextBox_Duration.Text = 2
                LastText = TextBox_Duration.Text
            End Try
        End Try
    End Sub

    Private Sub TheBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles TheBackgroundWorker.DoWork
        CheckConnection()
    End Sub

    Private Sub DeleteItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteItemsToolStripMenuItem.Click
        If MessageBox.Show("Do you really want to delete the selected item/s?", "InternetCheck", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim tmpCount As Integer = ListView_Losses.SelectedItems.Count
            For Each TheItem As ListViewItem In ListView_Losses.SelectedItems
                Dim TheDate As New DateTime(TheItem.SubItems(3).Text)
                For Each RealItem In ListOfAbbruch
                    If TheDate = RealItem.Anfang Then ListOfAbbruch.Remove(RealItem) : Exit For
                Next
            Next
            Update_Listview()
            AddToLog($"Deleted {tmpCount} items from main list.")
        End If
    End Sub

    Private Sub ContextMenuStrip_ListView_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip_ListView.Opening
        CMS_LV_DeActivate(ListView_Losses.SelectedItems.Count > 0)
    End Sub

    Private Sub TojsonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TojsonToolStripMenuItem.Click
        Try
            FolderBrowserDialog_Export.SelectedPath = FolderPathJson
            FolderBrowserDialog_Export.ShowDialog()
            If IO.Directory.Exists(FolderBrowserDialog_Export.SelectedPath) Then FolderPathJson = FolderBrowserDialog_Export.SelectedPath
            ExportToJson(FolderBrowserDialog_Export.SelectedPath)
        Catch ex As Exception
            MessageBox.Show($"The following error occurred while trying to export the item/s: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddToLog($"Failed in exporting item/s to json. The following error occurred: {ex.Message}")
        End Try
    End Sub

    Private Sub TotxtToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TotxtToolStripMenuItem.Click
        Try
            FolderBrowserDialog_Export.SelectedPath = FolderPathTxt
            FolderBrowserDialog_Export.ShowDialog()
            If IO.Directory.Exists(FolderBrowserDialog_Export.SelectedPath) Then FolderPathTxt = FolderBrowserDialog_Export.SelectedPath
            ExportToTxtFile(FolderBrowserDialog_Export.SelectedPath)
        Catch ex As Exception
            MessageBox.Show($"The following error occurred while trying to export the item/s: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddToLog($"Failed in exporting item/s to txt. The following error occurred: {ex.Message}")
        End Try
    End Sub

    Private Sub CopyToClipboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToClipboardToolStripMenuItem.Click
        Dim TempString As String = ""
        For Each TheItem In ListView_Losses.SelectedItems
            TempString &= $"{GetTextFromItem(TheItem)}{vbCrLf}{vbCrLf}"
        Next
        Clipboard.SetText(TempString.Remove(TempString.Length - 2, 2))
        MessageBox.Show($"Successfully copied content/s of item/s to the clipboard.", "Task completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub StartCheckingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartCheckingToolStripMenuItem.Click
        Button1_Click(Nothing, Nothing)
    End Sub

    Private Sub Form_Main_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Not Location.X < 0 And Not Location.Y < 0 Then LastGoodLocation = New Point(Location.X, Location.Y)
    End Sub

    Private Sub Timer_Minimize_Tick(sender As Object, e As EventArgs) Handles Timer_Minimize.Tick
        WindowState = FormWindowState.Minimized
        Timer_Minimize.Enabled = False
    End Sub

    Private Sub TextBox_Search_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Search.TextChanged
        Update_Listview(TextBox_Search.Text)
    End Sub

    Private Sub Form_Main_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Update_Label(LastKnownTimespan)
    End Sub

    Private Sub TabControl_Main_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl_Main.SelectedIndexChanged
        Update_Label(LastKnownTimespan)
    End Sub
#End Region
#Region "CheckedChanged handling"
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

        If Not Starting AndAlso CheckBox_WinStart.Checked Then
            Try
                AddToStartup()
                AddToLog("Application added to windows startup.")
            Catch ex As Exception
                MessageBox.Show($"Application could not be added to windows startup.{vbCrLf}The following error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                AddToLog($"Failed in adding the application to windows startup. The following error occurred: {ex.Message}")
            End Try
        ElseIf Not Starting AndAlso Not CheckBox_WinStart.checked Then
            Try
                RemoveFromStartup()
                AddToLog("Application removed from autostart.")
            Catch ex As Exception
                MessageBox.Show($"Application could not be removed from windows startup.{vbCrLf}The following error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                AddToLog($"Failed in removing the application from windows startup. The following error occurred: {ex.Message}")
            End Try
        End If

        If Not Starting AndAlso Not CheckBox_WinStart.Checked = CheckForStartup() Then
            Dim TempBool As Boolean = CheckForStartup()
            NonUserAction = True
            CheckBox_WinStart.Checked = TempBool
            WinStartTSMI.Checked = TempBool
            AddToLog("The last action could not be performed correctly.")
        End If
    End Sub
#End Region
End Class
