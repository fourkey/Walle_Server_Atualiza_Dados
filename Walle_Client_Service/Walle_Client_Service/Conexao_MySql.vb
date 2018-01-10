Imports MySql.Data.MySqlClient

Public Class Conexao_MySql

    Private Shared StringConexao As String = My.Settings.ConnectionString

    Public Function GetConexao() As MySqlConnection

        Dim oSQLConn As MySqlConnection = New MySqlConnection()

        oSQLConn.ConnectionString = StringConexao

        Try

            oSQLConn.Open()

        Catch ex As Exception


        End Try

        Return oSQLConn

    End Function

End Class
