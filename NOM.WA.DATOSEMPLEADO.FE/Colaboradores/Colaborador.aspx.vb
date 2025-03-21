Imports System.Net.Http
Imports System.Web.Script.Serialization
Imports System.Web.Services

Public Class Colaborador
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ObtenerColaboradoresAPI()
    End Sub

    <WebMethod()>
    Public Shared Function ObtenerColaboradoresAPI() As String
        Dim client As New HttpClient()
        Dim strUrlApi As String = "http://nom.ws.datosempleado.be/api/Colaboradores"
        Dim strJsonColaboradores As String
        Dim lstResultado As New List(Of ColaboradoresEtiqueta)

        Try
            Dim respuesta As HttpResponseMessage = client.GetAsync(strUrlApi).Result

            If respuesta.IsSuccessStatusCode Then
                strJsonColaboradores = respuesta.Content.ReadAsStringAsync().Result
            End If

            Dim serializer As New JavaScriptSerializer()
            Dim strColaborador As List(Of claseColaborador) = serializer.Deserialize(Of List(Of claseColaborador))(strJsonColaboradores)

            For Each c As claseColaborador In strColaborador
                Dim strEtiqueta As String = ""

                If c.EDAD >= 18 AndAlso c.EDAD <= 25 Then
                    strEtiqueta = "FUERA DE PELIGRO"
                ElseIf c.EDAD >= 26 AndAlso c.EDAD <= 50 Then
                    strEtiqueta = "TENGA CUIDADO, TOME TODAS LAS MEDIDAS DE PREVENSION"
                ElseIf c.EDAD >= 51 Then
                    strEtiqueta = "POR FAVOR QUEDARSE EN CASA"
                End If

                lstResultado.Add(New ColaboradoresEtiqueta With {
                    .intIdColaborador = c.IDCOLABORADOR,
                    .strNombre = c.NOMBRE,
                    .strApellido = c.APELLIDO,
                    .strDireccion = c.DIRECCION,
                    .strEdad = c.EDAD,
                    .strProfesion = c.PROFESION,
                    .strEstadoCivil = c.ESTADOCIVIL,
                    .strEtiqueta = strEtiqueta
                })
            Next

        Catch ex As AggregateException
            ' Captura excepciones internas de Task
            For Each innerEx As Exception In ex.InnerExceptions
                Console.WriteLine("Error interno: " & innerEx.Message)
            Next
        Catch ex As Exception
            Return ex.Message
        End Try

        Dim jsonResultado As String = (New JavaScriptSerializer()).Serialize(lstResultado)
        Return jsonResultado

    End Function

    Public Class claseColaborador
        Public IDCOLABORADOR As Integer
        Public Property NOMBRE As String
        Public Property APELLIDO As String
        Public Property DIRECCION As String
        Public Property EDAD As String
        Public Property PROFESION As String
        Public Property ESTADOCIVIL As String
    End Class
    Public Class ColaboradoresEtiqueta
        Public intIdColaborador As Integer
        Public Property strNombre As String
        Public Property strApellido As String
        Public Property strDireccion As String
        Public Property strEdad As String
        Public Property strProfesion As String
        Public Property strEstadoCivil As String
        Public Property strEtiqueta As String
    End Class

End Class