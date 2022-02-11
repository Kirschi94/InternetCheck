Public Class Abbruch
    Public Anfang As DateTime
    Private Ende_ As DateTime
    Private Dauer_ As TimeSpan

    Public Property Ende As DateTime
        Get
            Return Ende_
        End Get
        Private Set(value As DateTime)
            Ende_ = value
        End Set
    End Property

    Public Property Dauer As TimeSpan
        Get
            Return Dauer_
        End Get
        Private Set(value As TimeSpan)
            Dauer_ = value
        End Set
    End Property
    Public Sub New(Begin As DateTime)
        Anfang = Begin
    End Sub

    Private Sub New(Begin As DateTime, TheEnd As DateTime, Duration As TimeSpan)
        Anfang = Begin
        Ende = TheEnd
        Dauer = Duration
    End Sub

    Public Sub Set_End(TheEnd As DateTime)
        If IsNothing(Anfang) Then
            Throw New NotImplementedException("Not supposed to happen.")
        Else
            Ende = TheEnd
            Dauer = Ende - Anfang
        End If
    End Sub

    Public Function Clone()
        Dim TempAbbruch As New Abbruch(Anfang, Ende, Dauer)
        Return TempAbbruch
    End Function
End Class
