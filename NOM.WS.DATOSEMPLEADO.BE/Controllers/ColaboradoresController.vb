Imports System.Net
Imports System.Data
Imports System.Web.Http
Imports NOM.WS.DATOSEMPLEADO.BE.Models
Imports Newtonsoft.Json

Namespace Controllers
    Public Class ColaboradoresController
        Inherits ApiController

        <HttpGet>
        Public Function ObtenerColaboradores() As IHttpActionResult

            Try
                Dim conexion As New ConexionBD()
                Dim dtColaboradores As DataTable = conexion.ObtenerDatos("COLABORADOR")

                Dim strJson As String = JsonConvert.SerializeObject(dtColaboradores, Formatting.Indented)
                Return Ok(Newtonsoft.Json.Linq.JArray.Parse("[{""IDCOLABORADOR"":1,""NOMBRE"":""Jose"",""APELLIDO"":""Pérez"",""DIRECCION"":""San Jose Pinula"",""EDAD"":""25"",""PROFESION"":""carpintero"",""ESTADOCIVIL"":""Soltero""}]"))
            Catch ex As Exception
                Dim errorResponse As New Dictionary(Of String, Object) From {
                    {"Error", True},
                    {"Mensaje", ex.Message},
                    {"Detalle", ex.StackTrace}
                }
                Return Ok(errorResponse)
                'Return Ok(Newtonsoft.Json.Linq.JObject.Parse("{""Error"":[]}"))
            End Try
        End Function

    End Class
End Namespace