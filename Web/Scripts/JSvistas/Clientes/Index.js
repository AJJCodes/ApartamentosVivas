$(document).ready(function () {

    window.Componente = {
        UrlControlador: "/Clientes/"
    };

    function ObtenerCuartos() {
        $.ajax({
            url: Componente.UrlControlador + "ObtenerListaCuartos",
            type: "POST",
            dataType: "json",
            success: function (response) {
                console.log('Datos recibidos:', response);  // Agrega esta línea para depurar

                var data = response.data;  // Accede a la propiedad 'data' en la respuesta

                var $IdCuarto = $('#IdCuarto');
                $IdCuarto.empty();

                $IdCuarto.append('<option value=""></option>');

                $.each(data, function (index, cuarto) {
                    $IdCuarto.append('<option value="' + cuarto.IdCuarto + '">' + cuarto.CodigoCuarto + ' - ' + cuarto.DescripCuarto + '</option>');
                });

                $IdCuarto.selectpicker('refresh');
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error("Error al obtener la lista de roles", errorThrown);

                // Obtener el mensaje de error del servidor si está disponible
                var errorMessage = "";
                if (xhr.responseJSON && xhr.responseJSON.error) {
                    errorMessage = xhr.responseJSON.error;
                }

                // Mostrar SweetAlert con el mensaje de error
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: errorMessage
                });
            }
        });
    }



    /*    $('.selectpicker').selectpicker();*/

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



    $('#TablaDeCliente').on('click', '.editar-btn', function () {
        var idCliente = $(this).data('idcliente');
        $.get(Componente.UrlControlador +"ObtenerClienteycontratoPorId", { IdCliente: idCliente }, function (html) {
            $('#DivTablaCliente').hide();
            $("#DivModificarClienteContrato").html(html);
            $('#DivModificarClienteContrato').show();

            // Reinicializar el selectpicker después de agregar el contenido
            $('.selectpicker').selectpicker();
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
    var table = $('#TablaDeCliente').DataTable({
        ajax: {
            type: 'POST',
            url: Componente.UrlControlador + '/ObtenerListaClientes',
            data: { EstadoCliente: 1 },
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
            { data: 'Nombre_Completo' },
            { data: 'Cedula' },
            { data: 'TelCliente' },
            { data: 'correo' },
            {
                "data": "IdCliente",
                "render": function (data, type, row) {
                    return `
                    <div class="d-flex justify-content-between">
                        <div class="btn-group">
                            <button class="btn btn-primary editar-btn" data-idcliente="${row.IdCliente}">
                                <i class="fas fa-pencil-alt"></i>
                            </button>

                            <button class="btn btn-danger eliminar-btn" data-idcliente="${row.IdCliente}">
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
                    $('#DivTablaCliente').hide(); // Ocultar el div de la tabla
                    ObtenerCuartos();
                    $('#DivAgregarCliente').show(); // Mostrar el div del formulario de agregar cliente
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


    //Switch
    const checkbox = document.getElementById('EstadoCliente');
    const label = document.querySelector('label[for="EstadoCliente"]');

    //Cambia el texto
    checkbox.addEventListener('change', function () {
        if (this.checked) {
            label.textContent = 'Clientes activos';
        } else {
            label.textContent = 'Clientes inactivos';
        }
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

    $('#cancelButton').click(function () {

        // Ocultar el div de agregar cliente
        $('#DivAgregarCliente').hide();

        // Mostrar el div de la tabla de clientes
        $('#DivTablaCliente').show();

        // Limpiar el formulario
        $('#AgregarClienteyContratoForm')[0].reset();
    });

    $('#AgregarClienteyContratoForm').submit(function (event) {
        // Evita que el formulario se envíe automáticamente
        event.preventDefault();

        // Verifica si el formulario es válido utilizando jQuery Validation Plugin
        if ($('#AgregarClienteyContratoForm').valid()) {
            // Realiza aquí la lógica que deseas ejecutar al enviar el formulario

            // Ejemplo de llamada a la función "AgregarClienteyContrato" en tu controlador:
            $.ajax({
                url: Componente.UrlControlador + 'AgregarClienteyContrato',
                type: 'POST',
                data: $(this).serialize(),
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Éxito',
                            text: 'El cliente y contrato se agregaron exitosamente',
                            showConfirmButton: true, // Muestra el botón "OK"
                        }).then(function () {
                            $('#DivAgregarCliente').hide();
                            $('#TablaDeCliente').DataTable().ajax.reload();
                            $('#DivTablaCliente').show();
                            // Limpiar el formulario
                            $('#AgregarClienteyContratoForm')[0].reset();
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Hubo un error al agregar el cliente y contrato'
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

    $('#TablaDeCliente').on('click', '.eliminar-btn', function () {
        var idCliente = $(this).data('idcliente');
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción eliminará el registro permanentemente.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    url: Componente.UrlControlador +'EliminarCliente',
                    type: 'POST',
                    data: { IdCliente: idCliente },
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Éxito',
                                text: 'El Registro ha sido eliminado correctamente.',
                                showConfirmButton: true
                            }).then(function () {
                                $('#TablaDeCliente').DataTable().ajax.reload();
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
                            text: 'Hubo un error al enviar la solicitud de eliminación del Registro.'
                        });
                    }
                });
            }
        });
    });
});