Imports System.Net.NetworkInformation

Public Class Form_Main
    Dim Lost_Connection As Boolean = False
    Dim TempAbbruch As Abbruch = Nothing
    Dim ListOfAbbruch As List(Of Abbruch)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button_CheckButton.Click
        Select Case Button_CheckButton.Text
            Case "Start checking"
                DeReActivatedChecking(True)
            Case "Stop checking"
                DeReActivatedChecking(False)
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
        If Not SingleCheck("google.com") AndAlso Not SingleCheck("microsoft.com") AndAlso Not SingleCheck("gmx.com") AndAlso Not SingleCheck("bing.com") Then
            Connection_Lost()
        Else
            Connection_Existent()
        End If
    End Sub

    Private Sub Connection_Lost()
        Lost_Connection = True
        TempAbbruch = New Abbruch(DateTime.Now)
    End Sub

    Private Function AddDate(ByVal Text As String)

    End Function

    Private Sub Connection_Existent()
        If Lost_Connection AndAlso Not IsNothing(TempAbbruch) Then
            TempAbbruch.Set_End(DateTime.Now)
            Lost_Connection = False
            ListOfAbbruch.Add(TempAbbruch.Clone())
            TempAbbruch = Nothing
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

    End Sub
End Class
