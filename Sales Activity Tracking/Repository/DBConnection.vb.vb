Imports System.Configuration
Imports System.Data.SqlClient

Public Class DBConnection

    Private Shared ReadOnly _connectionString As String =
        ConfigurationManager.ConnectionStrings("DBConnection").ConnectionString

    Public Shared Function GetConnection() As SqlConnection
        Return New SqlConnection(_connectionString)
    End Function

End Class
