Imports System.ComponentModel
Imports System.Net.NetworkInformation

Public Class Form_Main
    Dim Lost_Connection As Boolean = False
    Dim TempAbbruch As Abbruch = Nothing
    Dim ListOfAbbruch As New List(Of Abbruch)
    Dim Abbruchtest As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_CheckButton.Click
        Select Case Button_CheckButton.Text
            Case "Start checking"
                DeReActivatedChecking(True)
                AddToLog("Started monitoring the internet connection.")
            Case "Stop checking"
                DeReActivatedChecking(False)
                AddToLog("Stopped monitoring the internet connection.")
            Case Else
                Throw New NotImplementedException("This should not be able to happen at all. Please contact me asap.")
        End Select
    End Sub

    Private Sub DeReActivatedChecking(state As Boolean)
        Timer1.Enabled = state
        Button_CheckButton.Text = If(state, "Stop checking", "Start checking")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not Abbruchtest Then CheckConnection()
    End Sub

    Private Sub CheckConnection()
        If Not SingleCheck("google.com") Then
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
    End Sub

    Private Sub Connection_Lost()
        If Not Lost_Connection Then
            Lost_Connection = True
            TempAbbruch = New Abbruch(DateTime.Now)
            AddToLog("Internet connection has been lost.")
        End If
    End Sub

    Private Sub AddToLog(ByVal Text As String)
        Dim Output As String = $"[{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH:mm:ss}] {Text}"
        RichTextBox_Log.Text = Output & vbCrLf & RichTextBox_Log.Text
    End Sub

    Private Sub Connection_Existent()
        If Lost_Connection AndAlso Not IsNothing(TempAbbruch) Then
            TempAbbruch.Set_End(DateTime.Now)
            Lost_Connection = False
            If TempAbbruch.Dauer.TotalMinutes > 2 Then ListOfAbbruch.Add(TempAbbruch.Clone())
            TempAbbruch = Nothing
            AddToLog("Internet connection has been reestablished.")
        End If
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddToLog("Application starting..")

        Try
            Load_And_Apply_ini()
            AddToLog("Options loaded.")
        Catch ex As Exception
            AddToLog("Options could not be loaded.")
            If IO.File.Exists(Application.StartupPath & "\options.ini") Then
                MessageBox.Show("Options file could not be read properly. Some saved options might not have been applied.", MsgBoxStyle.OkOnly & MsgBoxStyle.Information)
            Else IO.File.Create(Application.StartupPath & "\options.ini") : AddToLog("Options file did not exist and was created.")
            End If
        End Try

        Try
            Load_Abbrüche()
            AddToLog("Connectionlog loaded.")
        Catch ex As Exception
            AddToLog("Connectionlog could not be loaded.")
            If IO.File.Exists(Application.StartupPath & "\connectionlog.bin") Then
                MessageBox.Show("Connectionlog file could not be read properly. Some saved options might not have been applied.", MsgBoxStyle.OkOnly & MsgBoxStyle.Information)
            Else IO.File.Create(Application.StartupPath & "\connectionlog.bin") : AddToLog("Connectionlog file did not exist and was created.")
            End If
        End Try

    End Sub

    Private Sub Form_Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AddToLog("Application stopping..")

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
                If Line.StartsWith("Play connection lost Sound:") Then CheckBox_ConLost.Checked = Line.Substring(27, Line.Length - (27)) : Continue For
                If Line.StartsWith("Play connection reestablished Sound:") Then CheckBox_ConBack.Checked = Line.Substring(36, Line.Length - (36)) : Continue For
                If Line.StartsWith("Notify if connection status changes:") Then CheckBox_Notify.Checked = Line.Substring(36, Line.Length - (36)) : Continue For
                If Line.StartsWith("Save log files:") Then CheckBox_LogSave.Checked = Line.Substring(15, Line.Length - (15)) : Continue For
                If Line.StartsWith("Start on Windows startup:") Then CheckBox_WinStart.Checked = Line.Substring(25, Line.Length - (25)) : Continue For
                If Line.StartsWith("Windowsize:") Then Size = New Size(Line.Substring(11, Line.IndexOf(",") - (11)), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
                If Line.StartsWith("Windowposition:") Then Location = New Point(Line.Substring(15, Line.IndexOf(",") - (15)), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
                'EmptyLineCounter += 1
            Else
                EmptyLineCounter += 1
            End If
        Next
        If (iniLines.Length - EmptyLineCounter) < 9 Then MessageBox.Show("Options file could not be read properly. Some saved options might not have been applied.", MsgBoxStyle.OkOnly & MsgBoxStyle.Information)
    End Sub

    Private Sub Save_Abbrüche()
        Dim AbbrString As String = ""
        For Each Artikel In ListOfAbbruch
            AbbrString &= $"{Artikel.Anfang.Ticks};{Artikel.Dauer.Ticks}{vbCrLf}"
        Next
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
End Class
