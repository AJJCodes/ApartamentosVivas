$(document).ready(function () {

    window.Componente = {
        UrlControlador: "/Cuartos/"
    };


    $('#cancelButton').click(function () {

        // Ocultar el div de agregar cliente
        $('#DivModificarCuartoCopia').hide();

        // Mostrar el div de la tabla de clientes
        $('#DivTablaCuarto').show();

        // Limpiar el formulario
        $('#ModificarCuartoForm')[0].reset();
    });

    $("#ModificarCuartoForm").validate({
        rules: {
            CodigoCuartoMod: {
                required: true,
                minlength: 3,
                maxlength: 10,
                remote: {
                    url: Componente.UrlControlador + 'ValidarCodigoConId',
                    type: 'post',
                    data: {
                        Codigo: function () {
                            return $('#CodigoCuartoMod').val();
                        },
                        id: $('#IdCuarto').val()
                    }

                }
            },
            DescripCuartoMod: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            CostoMod: {
                required: true,
                min: 100
            }
        },
        messages: {
            CodigoCuartoMod: {
                required: "Por favor ingrese un Codigo",
                minlength: "El Codigo del tiene que tener una longitud de minimo 3",
                maxlength: "El Codigo del tiene que tener una longitud maxima de  10 caracteres",
                remote: "Ese Codigo ya esta en uso , intente otro"
            },
            DescripCuartoMod: {
                required: "Por favor ingrese una Descripcion para el Cuarto",
                minlength: "La longitud minima debe de ser 2 caracteres",
                maxlength: "La longitud maxima de la descripcion es de 50 caracteres"
            },
            CostoMod: {
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



    $('#ModificarCuartoForm').on('submit', function (event) {
        event.preventDefault(); // Evita que el formulario se envíe de la manera predeterminada


        if ($('#ModificarCuartoForm').valid()) {
            //Consigue los valores en los inputs
            var idCuarto = $('#IdCuarto').val();
            var descripCuarto = $('#DescripCuartoMod').val();
            var costo = $('#CostoMod').val();
            var codigo = $('#CodigoCuartoMod').val();

            /*Le asignamos todos los valores que retiramos del form*/
            var CuartoVM = {
                CodigoCuarto: codigo,
                IdCuarto: idCuarto,
                DescripCuarto: descripCuarto,
                Costo: costo
            };

            $.ajax({
                url: Componente.UrlControlador + "ModificarCuarto",
                type: "POST",
                contentType: "application/json",
                //Lo convierte en JSON
                data: JSON.stringify({
                    CuartoVM
                }),
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'guardado',
                            showConfirmButton: true
                        });

                        $('#DivTablaCuarto').show();
                        $('#DivModificarCuartoCopia').hide();
                        $('#TablaCuarto').DataTable().ajax.reload();
                    } else {
                        //Dinamico
                        Swal.fire({
                            icon: 'error',
                            title: 'Error al modificar Cuarto',
                            text: 'Hubo un error al modificar el Cuarto',
                            showConfirmButton: true
                        });
                    }
                },
                error: function (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error al agregar el registro',
                        text: 'Ha ocurrido un error al intentar agregar el registro. Por favor, inténtalo de nuevo o más tarde.',
                        showConfirmButton: true
                    });
                }
            });
        }
    });
});