﻿$(document).ready(function () {

    window.Componente = {
        UrlControlador: "/Cuartos/"
    };


    $('#cancelButton').click(function () {

        // Ocultar el div de agregar cliente
        $('#DivAgregarCuarto').hide();

        // Mostrar el div de la tabla de clientes
        $('#DivTablaCuarto').show();

        // Limpiar el formulario
        $('#AgregarCuartoForm')[0].reset();
    });


    //Validaciones
    $("#AgregarCuartoForm").validate({
        rules: {
            CodigoCuarto: {
                required: true,
                minlength: 3,
                maxlength: 10,
                remote: {
                    url: Componente.UrlControlador + 'ValidarCodigo',
                    type: 'post',
                    data: {
                        Codigo: function () {
                            return $('#CodigoCuarto').val();
                        }
                    }
                }
            },
            DescripCuarto: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            Costo: {
                required: true,
                min: 100
            }
        },
        messages: {
            CodigoCuarto: {
                required: "Por favor ingrese un Codigo",
                minlength: "El Codigo del tiene que tener una longitud de minimo 3",
                maxlength: "El Codigo del tiene que tener una longitud maxima de  10 caracteres",
                remote: "Este codigo ya esta en uso"
            },
            DescripCuarto: {
                required: "Por favor ingrese una Descripcion para el Cuarto",
                minlength: "La longitud minima debe de ser 2 caracteres",
                maxlength: "La longitud maxima de la descripcion es de 50 caracteres"
            },
            Costo: {
                required: "No dejar esta Campo En blanco",
                min: "El monto minimo debe de ser de 100"
            }
        },
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        },
        unhighlight: function (element) {
            $(element).addClass('is-valid').removeClass('is-invalid');
        }
    });

    //Lo que sucede al hacerle click
    function format(d) {
        return (
            '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
            '<tr>' +
            '<td>N° Cuarto:</td>' +
            '<td>' + d.CodigoCuarto + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td>Descripcion:</td>' +
            '<td>' + d.DescripCuarto + '</td>' +
            '</tr>' +
            '</table>'
        );
    }

    //Estilo CSS de los botones
    var style = document.createElement('style');
    style.innerHTML = `
    td.details-control {
        text-align: center;
        width: 20px;
    }

    td.details-control::before {
        height: 1em;
        width: 1em;
        margin-top: -0.5em;
        display: inline-block;
        color: white;
        border: 0.15em solid white;
        border-radius: 1em;
        box-shadow: 0 0 0.2em #444;
        box-sizing: content-box;
        text-align: center;
        text-indent: 0 !important;
        font-family: "Courier New",Courier,monospace;
        line-height: 1em;
        content: '+';
        background-color: #31b131;
    }

    tr.shown td.details-control::before {
        content: '-';
        color: white;
        background-color: #F44336; /* Rojo */
    }
`;

    document.head.appendChild(style);



    $('#TablaCuarto').on('click', '.editar-btn', function () {
        var idCuarto = $(this).data('idcuarto');
        $.get(Componente.UrlControlador + "ObtenerCuartoPorID", { IdCuarto: idCuarto }, function (html) {
            $('#DivTablaCuarto').hide();
            $("#DivModificarCuartoCopia").html(html);
            $('#DivModificarCuartoCopia').show();

        }).fail(function (error) {
            console.error("Error al obtener los datos del registro:", error);

            Swal.fire({
                icon: 'error',
                title: 'Error al obtener los datos del registro',
                text: 'Ha ocurrido un error al intentar obtener los datos del registro. Por favor, inténtalo de nuevo.',
                showConfirmButton: true
            });

        });
    });


    //Parte que alimenta la Tabla
    var table = $('#TablaCuarto').DataTable({
        ajax: {
            type: 'POST',
            url: Componente.UrlControlador + '/ObtenerListaCuartos',
            data: { UsuariosActivos: true },
            error: function (xhr, error, thrown) {
                console.log('Error en la solicitud AJAX:', error);
            }
        },
        columns: [
            {
                className: 'details-control',
                orderable: false,
                data: null,
                defaultContent: ''
            },
            { data: 'CodigoCuarto' },
            { data: 'DescripCuarto' },
            { data: 'Costo' },
            {
                data: 'EstadoRenta',
                render: function (data) {
                    if (data === false) {
                        return '<span class="estadoRenta rojo">No disponible</span>';
                    } else if (data === true) {
                        return '<span class="estadoRenta verde">Disponible</span>';
                    } else {
                        return '';
                    }
                }
            },
            {
                data: 'EstadoMante',
                render: function (data) {
                    if (data === false) {
                        return '<span class="estadoMante verde" style="text-align: center;">No</span>';
                    } else if (data === true) {
                        return '<span class="estadoMante rojo" style="text-align: center;">Si</span>';
                    } else {
                        return '';
                    }
                }
            },
            {
                "data": "IdCuarto",
                "render": function (data, type, row) {
                    return `
                    <div class="d-flex justify-content-between">
                        <div class="btn-group">
                            <button class="btn btn-primary editar-btn" data-idCuarto="${row.IdCuarto}">
                                <i class="fas fa-pencil-alt"></i>
                            </button>
                            <button type="button" class="btn btn-danger eliminar-btn" data-idCuarto="${row.IdCuarto}">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </div>
                    </div>
                `;
                }
            }
        ],
        order: [[0, 'asc']],
        responsive: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        dom: 'Bfrtip',
        buttons: [
            {
                text: "Nuevo",
                className: "btn btn-primary",
                action: function (e, dt, node, config) {
                    $('#DivTablaCuarto').hide(); // Ocultar el div de la tabla
                    $('#DivAgregarCuarto').show(); // Mostrar el div del formulario de agregar cliente
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            },
            {
                extend: 'excel',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            },
            {
                extend: 'pdf',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                }
            }
        ]
    });

    //Controlador de los botones de mas Informacion
    $('#TablaDeCliente tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });

    $('#AgregarCuartoForm').submit(function (event) {
        // Evita que el formulario se envíe automáticamente
        event.preventDefault();

        // Verifica si el formulario es válido utilizando jQuery Validation Plugin
        if ($('#AgregarCuartoForm').valid()) {
            // Realiza aquí la lógica que deseas ejecutar al enviar el formulario

            $.ajax({
                url: Componente.UrlControlador +'AgregarCuarto',
                type: 'POST',
                data: $(this).serialize(),
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Éxito',
                            text: 'El cuarto Fue agregado Exitosamente',
                            showConfirmButton: true, // Muestra el botón "OK"
                        }).then(function () {
                            $('#DivAgregarCuarto').hide();
                            $('#TablaCuarto').DataTable().ajax.reload();
                            $('#DivTablaCuarto').show();
                            // Limpiar el formulario
                            $('#AgregarCuartoForm')[0].reset();

                            $('#TablaCuarto').DataTable().ajax.reload();
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Hubo un error al agregar el Cuarto'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Hubo un error al enviar el formulario'
                    });
                }
            });
        }
    });

    $('#TablaCuarto').on('click', '.eliminar-btn', function () {
        var idCuarto = $(this).data('idcuarto');

        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción eliminará el cuarto permanentemente.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    url: Componente.UrlControlador +'EliminarCuarto',
                    type: 'POST',
                    data: { IdCuarto: idCuarto },
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Éxito',
                                text: 'El cuarto ha sido eliminado correctamente.',
                                showConfirmButton: true
                            }).then(function () {
                                $('#TablaCuarto').DataTable().ajax.reload();
                            });
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: 'Hubo un error al eliminar el cuarto.'
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Hubo un error al enviar la solicitud de eliminación del cuarto.'
                        });
                    }
                });
            }
        });
    });
});