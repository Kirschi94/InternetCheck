﻿Imports Newtonsoft.Json
Public Module JsonHelper
    'This whole module is not my code, I copied it for convenience
    'Source: https://www.codeproject.com/Articles/1201466/Working-with-JSON-in-Csharp-VB
    Public Function FromClass(Of T)(data As T,
                                    Optional isEmptyToNull As Boolean = False,
                                    Optional jsonSettings As JsonSerializerSettings = Nothing) _
                                                          As String

        Dim response As String = String.Empty

        If Not EqualityComparer(Of T).Default.Equals(data, Nothing) Then
            response = JsonConvert.SerializeObject(data, jsonSettings)
        End If

        Return If(isEmptyToNull, (If(response = "{}", "null", response)), response)

    End Function

    Public Function ToClass(Of T)(data As String,
                                  Optional jsonSettings As JsonSerializerSettings = Nothing) _
                                                        As T

        Dim response = Nothing

        If Not String.IsNullOrEmpty(data) Then
            response = If(jsonSettings Is Nothing,
            JsonConvert.DeserializeObject(Of T)(data),
            JsonConvert.DeserializeObject(Of T)(data, jsonSettings))
        End If

        Return response

    End Function
End Module
