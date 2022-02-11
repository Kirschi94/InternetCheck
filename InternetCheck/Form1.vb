Imports System.ComponentModel
Imports System.Net.NetworkInformation

Public Class Form_Main
    Dim Lost_Connection As Boolean = False
    Dim TempAbbruch As Abbruch = Nothing
    Dim ListOfAbbruch As List(Of Abbruch)
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
        CheckConnection()
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
        Lost_Connection = True
        TempAbbruch = New Abbruch(DateTime.Now)
        AddToLog("Internet connection has been lost.")
    End Sub

    Private Sub AddToLog(ByVal Text As String)
        Dim Output As String = $"[{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH:mm:ss}] {Text}"
        RichTextBox_Log.Text = Output & vbCrLf & RichTextBox_Log.Text
    End Sub

    Private Sub Connection_Existent()
        If Lost_Connection AndAlso Not IsNothing(TempAbbruch) Then
            TempAbbruch.Set_End(DateTime.Now)
            Lost_Connection = False
            ListOfAbbruch.Add(TempAbbruch.Clone())
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
            While MessageBox.Show("Logfile could not be saved.", MsgBoxStyle.RetryCancel & MsgBoxStyle.Critical) = DialogResult.Retry
                Save_log(Application.StartupPath & $"\Logs\{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH.mm.ss}_{TempCount}.log")
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
        If IsNothing(Pfad) Then Pfad = Application.StartupPath & $"\Logs\{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH.mm.ss}.log"
        If CheckBox_LogSave.Checked Then IO.File.WriteAllText(Pfad, RichTextBox_Log.Text)
    End Sub

    Private Sub Load_And_Apply_ini(Optional Pfad As String = Nothing)
        If IsNothing(Pfad) Then Pfad = Application.StartupPath & "\options.ini"
        Dim iniLines As String() = IO.File.ReadAllLines(Pfad)
        Dim EmptyLineCounter As Integer = 0
        For Each Line In iniLines
            If Line.StartsWith("Connection lost Sound:") Then TextBox_ConLost.Text = Line.Substring(23, Line.Length - 1) : Continue For
            If Line.StartsWith("Connection reestablished Sound:") Then TextBox_ConLost.Text = Line.Substring(32, Line.Length - 1) : Continue For
            If Line.StartsWith("Play connection lost Sound:") Then CheckBox_ConLost.Checked = Line.Substring(27, Line.Length) : Continue For
            If Line.StartsWith("Play connection reestablished Sound:") Then CheckBox_ConBack.Checked = Line.Substring(36, Line.Length) : Continue For
            If Line.StartsWith("Save log files:") Then CheckBox_Notify.Checked = Line.Substring(15, Line.Length) : Continue For
            If Line.StartsWith("Save log files:") Then CheckBox_LogSave.Checked = Line.Substring(15, Line.Length) : Continue For
            If Line.StartsWith("Start on Windows startup:") Then CheckBox_WinStart.Checked = Line.Substring(25, Line.Length) : Continue For
            If Line.StartsWith("Windowsize:") Then Size = New Size(Line.Substring(11, Line.IndexOf(",")), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
            If Line.StartsWith("Windowposition:") Then Location = New Point(Line.Substring(15, Line.IndexOf(",")), Line.Substring(Line.IndexOf(",") + 1)) : Continue For
            EmptyLineCounter += 1
        Next
        If iniLines.Length - EmptyLineCounter < 9 Then MessageBox.Show("Options file could not be read properly. Some saved options might not have been applied.", MsgBoxStyle.OkOnly & MsgBoxStyle.Information)
    End Sub
End Class
