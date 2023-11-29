$(document).ready(function () {

    //Validaciones
    $("#AgregarClienteyContratoForm").validate({
        rules: {
            NombrePersona: {
                required: true,
                minlength: 3
            },
            Apellido: {
                required: true,
                minlength: 3
            },
            NombreUsuario: {
                required: true,
                remote: {
                    url: Componente.UrlControlador +'ValidarUsuario',
                    type: 'post',
                    data: {
                        Usuario: function () {
                            return $('#NombreUsuario').val();
                        }
                    }
                }
            },
            CampoContraseña: {
                required: true,
                minlength: 8
            },
            Correo: {
                required: true,
                email: true,
                remote: {
                    url: Componente.UrlControlador +'ValidarCorreo',
                    type: 'post',
                    data: {
                        Cedula: function () {
                            return $('#Correo').val();
                        }
                    }
                }
            },
            IdCuarto: {
                required: true
            }
        },
        messages: {
            NombrePersona: {
                required: "Por favor ingrese su nombre",
                minlength: "El nombre debe tener al menos 3 caracteres"
            },
            Apellido: {
                required: "Por favor ingrese su apellido",
                minlength: "El apellido debe tener al menos 3 caracteres"
            },
            NombreUsuario: {
                required: "Por favor ingrese su cédula",
                remote: "Esa cedula ya esta registrada en el sistema"
            },
            TelefonoCliente: {
                required: "Por favor ingrese su número de teléfono",
                minlength: "El número de teléfono debe tener 8 dígitos",
                maxlength: "El número de teléfono debe tener 8 dígitos",
                digits: "El número de teléfono debe contener solo números"
            },
            Correo: {
                required: "Por favor ingrese su correo electrónico",
                email: "Por favor ingrese un correo electrónico válido",
                remote: "Ese Correo ya esta registrado en el sistema"
            },
            IdCuarto: {
                required: "Porfavor seleccione un cuarto"
            }
        },
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        },
        unhighlight: function (element) {
            $(element).addClass('is-valid').removeClass('is-invalid');
        }
    });




    window.Componente = {
        UrlControlador: "/Usuario/"
    };


    ObtenerUsuarios();

    function ObtenerUsuarios() {
        var groupColumn = 4; // Cambiado a la posición de la columna 'NombreRol'

        table = $('#TablaDeUsuarios').DataTable({
            ajax: {
                type: 'POST',
                url: Componente.UrlControlador + '/ObtenerListaUsuarios',
                data: { UsuariosActivos: true },
                error: function (xhr, error, thrown) {
                    console.log('Error en la solicitud AJAX:', error);
                }
            },
            columns: [
                {
                    data: null,
                    render: function (data, type, full, meta) {
                        return data.Nombre + ' ' + data.Apellidos;
                    },
                    name: 'NombreCompleto',
                    orderable: true,
                    searchable: true
                },
                { data: 'NombreUsuario' },
                { data: 'Email' },
                {
                    data: 'FechaCreacion',
                    render: function (data) {
                        var date = moment(parseInt(data.replace(/\/Date\((-?\d+)\)\//, '$1')));
                        return date.format('DD/MM/YYYY');
                    },
                    type: 'date',
                    orderData: [2]
                },
                { data: 'NombreRol' },
                {
                    "data": "IdUsuario",
                    "render": function (data, type, row) {
                        return `
                    <div class="d-flex justify-content-between">
                        <div class="btn-group">
                            <button class="btn btn-primary editar-btn" data-idusuario="${row.IdCliente}">
                                <i class="fas fa-pencil-alt"></i>
                            </button>

                            <button class="btn btn-danger eliminar-btn" data-idusuario="${row.IdCliente}">
                                <i class="fas fa-trash-alt"></i>
                            </button>
                        </div>
                    </div>
                `;
                    }
                }
            ],
            columnDefs: [{ visible: false, targets: groupColumn }],
            order: [[groupColumn, 'asc']],
            drawCallback: function (settings) {
                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;

                api.column(groupColumn, { page: 'current' })
                    .data()
                    .each(function (group, i) {
                        if (last !== group) {
                            $(rows)
                                .eq(i)
                                .before(
                                    '<tr class="group"><td colspan="6">' + // Ajustado a 6 columnas
                                    group +
                                    '</td></tr>'
                                );

                            last = group;
                        }
                    });
            },
            responsive: true,
            language: {
                url: "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
            },
            dom: 'Bfrtip',
            buttons: [
                {
                    text: '<i class="fas fa-plus"></i> Nuevo',
                    className: "btn btn-primary",
                    action: function (e, dt, node, config) {
                        $('#DivTablaUsuario').hide(); // Ocultar el div de la tabla
                        ObtenerRoles();
                        $('#DivAgregarUsuario').show(); // Mostrar el div del formulario de agregar cliente
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

        $('#TablaDeUsuarios tbody').on('click', 'tr.group', function () {
            var currentOrder = table.order()[0];
            if (currentOrder[0] === groupColumn && currentOrder[1] === 'asc') {
                table.order([groupColumn, 'desc']).draw();
            } else {
                table.order([groupColumn, 'asc']).draw();
            }
        });
    }


    function ObtenerRoles() {
        $.ajax({
            url: Componente.UrlControlador + "ObtenerListaRoles",
            type: "POST",
            dataType: "json",
            data: { RolesActivos: true },
            success: function (data) {
                var $IdRol = $('#IdRol');
                $IdRol.empty();

                $IdRol.append('<option value=""></option>');

                $.each(data.data, function (index, rol) {
                    $IdRol.append('<option value="' + rol.IdRol + '">' + rol.NombreRol + '</option>');
                });

                $IdRol.selectpicker('refresh');
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



});