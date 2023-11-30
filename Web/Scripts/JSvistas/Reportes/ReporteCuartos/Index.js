$(document).ready(function () {

    function inicializarTablaContrato() {
        table = $('#TablaDaños').DataTable({
            ajax: {
                url: '/ReporteCuartos/DañosContratoJSON',
                dataSrc: ''
            },
            columns: [
                { data: 'NomCliente' },
                { data: 'Cedula' },
                { data: 'CodigoCuarto' },
                { data: 'DescripCuarto' },
                { data: 'DescripDaño' },
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
                { data: 'Costo' }
            ],
            order: [[0, 'asc']],
            responsive: true,
            language: {
                url: "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
            },
            dom: 'Bfrtip',
            buttons: [
                {
                    text: "Generar Reporte Masivo",
                    className: "btn btn-primary",
                    action: function (e, dt, node, config) {
                        /*                             Llamar a la acción "Reporte1" y pasar los datos mediante una solicitud AJAX*/
                        $.ajax({
                            url: '/ReporteCuartos/Reporte1',
                            type: 'POST',
                            xhrFields: {
                                responseType: 'blob' // Indicar que la respuesta es un blob (archivo)
                            },
                            success: function (result, status, xhr) {
                                var contentType = xhr.getResponseHeader('Content-Type');
                                var filename = 'ReporteDeDanosMasivo.pdf';

                                // Verificar si el tipo de contenido es PDF y forzar la descarga
                                if (contentType === 'application/pdf') {
                                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                                        // Para navegadores IE/Edge
                                        window.navigator.msSaveBlob(result, filename);
                                    } else {
                                        // Para otros navegadores
                                        var blob = new Blob([result], { type: contentType });
                                        var link = document.createElement('a');
                                        link.href = window.URL.createObjectURL(blob);
                                        link.download = filename;
                                        link.click();
                                        window.URL.revokeObjectURL(link.href);
                                    }
                                }
                            },
                            error: function (error) {
                                // Mostrar SweetAlert en caso de error
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'Ocurrió un error al generar el reporte.',
                                });
                            }
                        });

                    }
                },
                {
                    extend: 'csv',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    }
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    }
                },
                {
                    extend: 'pdf',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    }
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    }
                },
                //{
                //    text: "Generar Reporte Unico",
                //    className: "btn btn-primary",
                //    action: function (e, dt, node, config) {
                //        // Llamar a la acción "Reporte1" y pasar los datos mediante una solicitud AJAX
                //        //$.ajax({
                //        //    url: '/ReporteContratos/Reporte1',
                //        //    type: 'POST',
                //        //    data: { FechaInicio: fechaInicial, FechaFinal: fechaFinal },
                //        //    xhrFields: {
                //        //        responseType: 'blob' // Indicar que la respuesta es un blob (archivo)
                //        //    },
                //        //    success: function (result, status, xhr) {
                //        //        var contentType = xhr.getResponseHeader('Content-Type');
                //        //        var filename = 'ReporteDeContratosFechas.pdf';

                //        //        // Verificar si el tipo de contenido es PDF y forzar la descarga
                //        //        if (contentType === 'application/pdf') {
                //        //            if (typeof window.navigator.msSaveBlob !== 'undefined') {
                //        //                // Para navegadores IE/Edge
                //        //                window.navigator.msSaveBlob(result, filename);
                //        //            } else {
                //        //                // Para otros navegadores
                //        //                var blob = new Blob([result], { type: contentType });
                //        //                var link = document.createElement('a');
                //        //                link.href = window.URL.createObjectURL(blob);
                //        //                link.download = filename;
                //        //                link.click();
                //        //                window.URL.revokeObjectURL(link.href);
                //        //            }
                //        //        }
                //        //    },
                //        //    error: function (error) {
                //        //        // Mostrar SweetAlert en caso de error
                //        //        Swal.fire({
                //        //            icon: 'error',
                //        //            title: 'Error',
                //        //            text: 'Ocurrió un error al generar el reporte.',
                //        //        });
                //        //    }
                //        //});

                //    }
                //}
            ]
        });
    }

    inicializarTablaContrato();
});