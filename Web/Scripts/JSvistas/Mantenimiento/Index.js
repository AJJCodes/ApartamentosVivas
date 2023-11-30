$(document).ready(function () {
    window.Componente = {
        UrlControlador: "/Mantenimiento/"
    };

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



    //$('#TablaDeCliente').on('click', '.editar-btn', function () {
    //    var idCliente = $(this).data('idcliente');
    //    $.get("/Cliente/ObtenerClienteycontratoPorId", { IdCliente: idCliente }, function (html) {
    //        $('#DivTablaCliente').hide();
    //        $("#DivModificarClienteContrato").html(html);
    //        $('#DivModificarClienteContrato').show();

    //        // Reinicializar el selectpicker después de agregar el contenido
    //        $('.selectpicker').selectpicker();
    //    }).fail(function (error) {
    //        console.error("Error al obtener los datos del registro:", error);

    //        Swal.fire({
    //            icon: 'error',
    //            title: 'Error al obtener los datos del registro',
    //            text: 'Ha ocurrido un error al intentar obtener los datos del registro. Por favor, inténtalo de nuevo.',
    //            showConfirmButton: true
    //        });

    //    });
    //});



    //Parte que alimenta la Tabla
    var table = $('#TablaMantenimientos').DataTable({
        ajax: {
            type: 'POST',
            url: Componente.UrlControlador + '/ObtenerListaMantenimientos',
            data: { EstadoCuarto: 1 },
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
                data: 'Fecha',
                render: function (data) {
                    // Parse the date string and format it using Moment.js
                    var date = moment(parseInt(data.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    return date.format('DD/MM/YYYY');
                },
                type: 'date',
                orderData: [2]
            },
            { data: 'Descripcion' },
            {
                "data": "IdDetaMante",
                "render": function (data, type, row) {
                    return `
                    <div class="d-flex justify-content-between">
                        <div class="btn-group">
                            <button class="btn btn-primary editar-btn" data-idcliente="${row.IdDetaMante}">
                                <i class="fas fa-pencil-alt"></i>
                            </button>

                            <button type="button" class="btn btn-danger mx-1">
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
                    $('#DivTablaMantenimientos').hide(); // Ocultar el div de la tabla

                    $('#DivAgregarMantenimiento').show(); // Mostrar el div del formulario de agregar cliente
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
    //checkbox.addEventListener('change', function () {
    //    if (this.checked) {
    //        label.textContent = 'Clientes activos';
    //    } else {
    //        label.textContent = 'Clientes inactivos';
    //    }
    //});

    //Controlador de los botones de mas Informacion
    $('#TablaMantenimientos tbody').on('click', 'td.details-control', function () {
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
});