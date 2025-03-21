Imports System.Data.SqlClient

Public Class ConexionBD
    Private ReadOnly strServidor As String = "LOCALHOST"
    Private ReadOnly strBaseDatos As String = "TEST"
    Private ReadOnly strUsuario As String = ""
    Private ReadOnly strContrasenia As String = "8ac3etuq"
    Private ReadOnly strAutenticacionWindows As Boolean = True

    Public Function ObtenerConexion() As SqlConnection
        Dim cadenaConexion As String

        If strAutenticacionWindows Then
            cadenaConexion = $"Server={strServidor};Database={strBaseDatos};Integrated Security=True;"
        Else
            cadenaConexion = $"Server={strServidor};Database={strBaseDatos};User Id={strUsuario};Password={strContrasenia};"
        End If

        Return New SqlConnection(cadenaConexion)
    End Function

    Public Function ObtenerDatos(ByVal strTabla As String, Optional strCondicion As String = "") As DataTable
        Dim dtDatos As New DataTable()
        Dim strConsulta As String

        Try
            strConsulta = "Select * from " & strTabla
            If strCondicion <> "" Then
                'agregar condiciones
            End If

            Using conexion As SqlConnection = ObtenerConexion()
                Using cmd As New SqlCommand(strConsulta, conexion)
                    Using da As New SqlDataAdapter(cmd)
                        conexion.Open()
                        da.Fill(dtDatos)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            dtDatos = New DataTable()
        End Try
        Return dtDatos
    End Function

End Class
