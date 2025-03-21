<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="COlaborador.aspx.vb" Inherits="NOM.WA.DATOSEMPLEADO.FE.Colaborador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/Style.css"/>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Lista de Colaboradores</title>
</head>
<body>
    <div class="container">
        <h2>Lista de Colaboradores</h2> 
        <button id="btnCargar">Cargar Colaboradores</button>
        <table id="tablaColaboradores">
            <thead>
                <tr>
                    <th>Número</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Dirección</th>
                    <th>Edad</th>
                    <th>Profesión</th>
                    <th>Estado civil</th>
                    <th>Nivel de riesgo</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <script>

        <%--document.addEventListener("DOMContentLoaded", function () {
            var colaboradores = <%= strListaColaboradores %>;
            var tbody = document.querySelector("#tablaColaboradores tbody");

            if (colaboradores.length === 0) {
                tbody.innerHTML = "<tr><td colspan='3'>No hay colaboradores disponibles</td></tr>";
                return;
            }

            colaboradores.forEach(function (colaborador) {
                
                var row = "<tr><td>" + colaborador.IDCOLABORADOR + "</td><td>" + colaborador.NOMBRE + "</td><td>$" + colaborador.APELLIDO + "</td>" +
                    "<tr><td>" + colaborador.DIRECCION + "</td><td>" + colaborador.EDAD + "</td><td>$" + colaborador.PROFESION + "</td>" +
                    "<td>$" + colaborador.ESTADOCIVIL + "</td></tr>";

                tbody.innerHTML += row;
            });
        });--%>

        $(document).ready(function () {
            $("#btnCargar").click(function () {
                debugger
                $.ajax({
                    type: "POST",
                    url: "Colaborador.aspx/ObtenerColaboradoresAPI",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        debugger
                        let colaboradores = JSON.parse(response.d);
                        let tbody = $("#tablaColaboradoresBody");

                        tbody.empty();

                        if (colaboradores.length == 0) {
                            tbody.append("<tr><td colspan='7'>No hay registro de colaboradores</td></tr>");
                            return;
                        }

                        colaboradores().forEach(colaborador => {
                            var row = "<tr><td>" + colaborador.intIdColaborador + "</td><td>" + colaborador.strNombre + "</td>" +
                                "<td>$" + colaborador.strApellido + "</td><tr><td>" + colaborador.strDireccion + "</td>" +
                                "<td>" + colaborador.strEdad + "</td><td>$" + colaborador.strProfesion + "</td>" +
                                "<td>$" + colaborador.strEstadoCivil + "</td><td>$" + colaborador.strEtiqueta + "</td></tr>";
                        });
                    },
                    error: function () {
                        alert("Error al obtener los colaboradores.");
                    }
                });
            });
        });

    </script>
</body>
</html>
