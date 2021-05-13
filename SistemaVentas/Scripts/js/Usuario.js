﻿$(document).ready(function () {
    var table = $("#TableUsuarios").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "lengthChange": false,
        "ajax": {
            url: "/User/GetUserList/",
            method: "POST",
            data: {


            }
        },
        "columns": [
            //{
            //    "visible": true,
            //    "orderable": false,
            //    "data": "idUser"
            //},
            {
                "orderable": false,
                "data": "Nombre"

            },
            {
                "orderable": false,
                "data": "Apellido"

            },
            {
                "orderable": false,
                "data": "direccion"

            },
            {
                "orderable": false,
                "data": "FechaNacimiento"

            },
            {
                "orderable": false,
                "data": "telefono"

            },
            {
                "orderable": false,
                "data": "documento"

            },
            {
                "orderable": false,
                "data": "UserName"

            },
            {
                "orderable": false,
                "data": "email"

            },
            {
                "orderable": false,
                "data": "roleString"

            },
            {
                "orderable": false,
                "render": function (data, type, row) {

                    return '<center><a href="/User/UserEdit?id=' + row.idUser + '" style="width:120px" class="btn btn-info"><i class="bi bi-pencil"></i> Editar</a>' +
                        '<button type="button" onclick="jsDelete(' + row.idUser + ')" name="delete" class="btn btn-danger" style="width:120px"/><i class="bi bi-trash"></i> Eliminar</button></center>';
                }
            }
        ],
        "language": {

            "infoEmpty": "No hay registros disponibles",
            "lengthMenu": "Mostrar _MENU_ registros",
            "search": "",
            "searchPlaceholder": "Buscar Usuarios",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfo": "RESULTADOS _START_ DE  _END_  TOTAL  _TOTAL_",
            "sZeroRecords": "NO EXISTE NINGUN REGISTRO",
            "oPaginate": { "sNext": "Siguiente", "sPrevious": "Anterior" },
            "sProcessing": "Procesando..."
        }
    });
});

function jsDelete(idUser) {
    var ROOT = '/User/UserDelete/' + idUser;
    if (confirm("¿Desea eliminar este registro?")) {
        url = ROOT;
        $.post(url, function(info){
            if (info == "1") {
                document.location.href = "/User/UsersList";
            } else {
                alert("Ocurrio un error, no se puede eliminar el registro.");
            }
        });
    }
}