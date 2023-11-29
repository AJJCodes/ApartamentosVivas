$(document).ready(function () {

    //Validaciones
    //$("#AgregarClienteyContratoForm").validate({
    //    rules: {
    //        Correo: {
    //            required: true,
    //            email: true,
    //            remote: {
    //                url: Componente.UrlControlador + 'ValidarCorreo',
    //                type: 'post',
    //                data: {
    //                    Cedula: function () {
    //                        return $('#Correo').val();
    //                    }
    //                }
    //            }
    //        },
    //    },
    //    messages: {
    //        NombrePersona: {
    //            required: "Por favor ingrese su nombre",
    //            minlength: "El nombre debe tener al menos 3 caracteres"
    //        },
    //    },
    //    highlight: function (element) {
    //        $(element).addClass('is-invalid').removeClass('is-valid');
    //    },
    //    unhighlight: function (element) {
    //        $(element).addClass('is-valid').removeClass('is-invalid');
    //    }
    //});




    window.Componente = {
        UrlControlador: "/Cuartos/"
    };


    ObtenerRoles();

    function ObtenerRoles() {
        table = $('#TablaDeCuartos').DataTable({
            ajax: {
                type: 'POST',
                url: Componente.UrlControlador + '/ObtenerListaCuartos',
                data: { UsuariosActivos: true },
                error: function (xhr, error, thrown) {
                    console.log('Error en la solicitud AJAX:', error);
                }
            },
            columns: [
                { data: 'DescripCuarto' },
                { data: 'CodigoCuarto' },
                {
                    "data": "IdRol",
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
/*                        ObtenerRoles();*/
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
    }

});