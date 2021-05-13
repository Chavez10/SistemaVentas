$(document).ready(function () {
    var table = $("#TableProductos").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "lengthChange": false,
        "ajax": {
            url: "/Producto/ObtenerProductos/",
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
                "data": "nombre"

            },
            {
                "orderable": false,
                "data": "categoria"

            },
            {
                "orderable": false,
                "data": "existencia"

            },
            {
                "orderable": false,
                "data": "precio"

            },
            {
                "orderable": false,
                "render": function (data, type, row) {
                    return '<center><a href="/Producto/CreateOrUpdateProducto?id=' + row.idProducto + '" style="width:120px" class="btn btn-info"><i class="bi bi-pencil"></i> Editar</a>' +
                        '<a href="#" onclick="EliminarProducto(' + row.idProducto + ')" class="btn btn-danger" style="width:120px"><i class="bi bi-trash"></i> Eliminar</a></center>';
                }
            }
        ],
        "language": {

            "infoEmpty": "No hay registros disponibles",
            "lengthMenu": "Mostrar _MENU_ registros",
            "search": "",
            "searchPlaceholder": "Buscar productos",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfo": "RESULTADOS _START_ DE  _END_  TOTAL  _TOTAL_",
            "sZeroRecords": "NO EXISTE NINGUN REGISTRO",
            "oPaginate": { "sNext": "Siguiente", "sPrevious": "Anterior" },
            "sProcessing": "Procesando..."
        }
    });



    var table2 = $("#Table-Productos").DataTable({
        "processing": true,
        "serverSide": true,
        "searching": true,
        "lengthMenu": [5, 10, 15, 25],
        "ajax": {
            url: "/Producto/ObtenerProductosIndex/",
            method: "POST",
            data: {


            }
        },
        "columns": [
            {
                "orderable": false,
                "render": function (data, type, row) {

                    return '<img style = "margin-top: 10%;" src = "'+row.foto+'" width = "250" height = "125" />';
                }               
            },
            {
                "orderable": false,
                "render": function (data, type, row) {

                    var HTML = '<center><h4 style = "color:blue;font-weight:bold" >' + row.nombre + '</h4></center ><br />' +
                        '<p style="text-align:justify"><b>Descripcion: </b>' + row.descripcion + '</p>' +
                        '<p style="text-align:center"><b>Precio:  ' + row.precio + '</b></p>';                           

                    return HTML;
                }              
            },
            {
                "orderable": false,
                "render": function (data, type, row) {

                    return '<button class="btn btn-success"><i class="bi bi-cart-plus"></i> Agregar al Carrito</button>&nbsp;&nbsp;' +
                        '<button class="btn btn-default"><i class="bi bi-plus-circle"></i> Ver más...</button>';
                }
            }
        ],
        "language": {

            "infoEmpty": "No hay registros disponibles",
            "lengthMenu": "Mostrar _MENU_ registros",
            "search": "",
            "searchPlaceholder": "Buscar productos",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfo": "RESULTADOS _START_ DE  _END_  TOTAL  _TOTAL_",
            "sZeroRecords": "NO EXISTE NINGUN REGISTRO",
            "oPaginate": { "sNext": "Siguiente", "sPrevious": "Anterior" },
            "sProcessing": "Procesando..."
        }
    });
});

function EliminarProducto(idProd)
{
    alertify.confirm('Confirmación', '¿Desea eliminar el usuario?',
        function () { DeleteUser(idProd) }
        , function () { alertify.error('Cancel') });
}

function DeleteUser(idProd)
{
    $.post("/Producto/DeleteProducto", { IdProducto: idProd }, function (response) {
        if (response.IsSuccess) {
            table.ajax.reload();
            alertify.success("El usuario se elimino correctamente");
        } else {
            alertify.error("Hubo un problema al eliminar el usuario")
        }
    });
}