$(document).ready(function () {
    $.validator.addMethod(
        "cedulaNIC",
        function (value, element) {
            return this.optional(element) || /^\d{3}-\d{6}-\d{4}[A-Z]$/.test(value);
        },
        "Por favor ingrese una cédula nicaragüense válida"
    );

    // Agregar la máscara al campo de cédula
    $("#Cedula").inputmask("999-999999-9999A", {
        placeholder: " ",
        clearMaskOnLostFocus: false
    });

    $("#AgregarClienteyContratoForm").validate({
        rules: {
            NombreCliente: {
                required: true,
                minlength: 3
            },
            Apellido: {
                required: true,
                minlength: 3
            },
            Cedula: {
                required: true,
                cedulaNIC: true,
                remote: {
                    url: '/Clientes/ValidarCedula',
                    type: 'post',
                    data: {
                        Cedula: function () {
                            return $('#Cedula').val();
                        }
                    }
                }
            },
            TelefonoCliente: {
                required: true,
                minlength: 8,
                maxlength: 8,
                digits: true
            },
            Correo: {
                required: true,
                email: true,
                remote: {
                    url: '/Cliente/ValidarCorreo',
                    type: 'post',
                    data: {
                        Cedula: function () {
                            return $('#Correo').val();
                        }
                    }
                }
            },
            Deposito: {
                required: true
            },
            FechaInicio: {
                required: true
            },
            IdCuarto: {
                required: true
            }
        },
        messages: {
            NombreCliente: {
                required: "Por favor ingrese su nombre",
                minlength: "El nombre debe tener al menos 3 caracteres"
            },
            Apellido: {
                required: "Por favor ingrese su apellido",
                minlength: "El apellido debe tener al menos 3 caracteres"
            },
            Cedula: {
                required: "Por favor ingrese su cédula",
                cedulaNIC: "Por favor ingrese una cédula nicaragüense válida",
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
            Deposito: {
                required: "Ingrese Una cantidad de Deposito Inicial"
            },
            FechaInicio: {
                required: "Ingrese una fecha de Inicio"
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
});