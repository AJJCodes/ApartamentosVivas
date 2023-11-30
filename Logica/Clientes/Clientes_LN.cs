using Datos;
using Modelo.Clientes;
using Modelo.Cuartos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Clientes
{
    public class Clientes_LN
    {
        private readonly Contexto bd;

        public Clientes_LN()
        {
            bd = new Contexto();
        }

        #region Poblar
        public bool ProporcionarListaClientesyContrato(ref List<ClienteYContrato_VM> ListaClientesConContrato, out string errorMessage,int EstadoContrato)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.spObtenerClientesContratosyCuartos(EstadoContrato).ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaClientesConContrato = resultados.Select(c => new ClienteYContrato_VM
                {
                    Nombre_Completo = c.Nombre_Completo,
                    Cedula = c.Cedula,
                    correo = c.correo,
                    Deposito = c.Deposito,
                    CodigoCuarto = c.CodigoCuarto,
                    DescripCuarto = c.DescripCuarto , 
                    TelCliente = c.TelCliente,
                    IdCliente = c.IdCliente,
                    IdContrato = c.IdContrato
                }).ToList();

                errorMessage = null;  // No hay error, establecer el mensaje a null
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public ClienteyContratoConID_VM PoblarVistaModificar(int Id)
        {
            try
            {
                var result = bd.spObtenerClientesContratosyCuartosConID(Id).FirstOrDefault(); // Cambiado a FirstOrDefault para obtener un solo objeto

                if (result != null)
                {
                    var ResultadoConsulta = new ClienteyContratoConID_VM
                    {
                        IdCliente = result.IdCliente,
                        Cedula = result.Cedula ,
                        correo = result.correo , 
                        TelCliente = result.TelCliente,
                        IdContrato = result.IdContrato,
                        IdCuarto = result.IdCuarto ,
                        Deposito = result.Deposito,
                        CodigoCuarto = result.CodigoCuarto,
                        DescripCuarto = result.DescripCuarto,
                        NomCliente = result.NomCliente,
                        Apellido = result.Apellido,
                        FechaIni = result.FechaIni,
                        Fechafin = result.Fechafin
                    };

                    // Puedes realizar más operaciones si es necesario antes de devolver el resultado
                    return ResultadoConsulta;
                }
                else
                {
                    // Manejo del caso en que no se encuentra ningún objeto con el Id proporcionado
                    return null; // O lanzar una excepción u otro manejo según tu lógica
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine($"Se produjo una excepción: {ex.Message}");

                throw; // O return null; o un valor predeterminado dependiendo de tu lógica
            }
        }

        public bool ObtenerListaCuartosDisponibles(ref List<CuartoSelectPicker_VM> ListaCuartosDisponibles, out string errorMessage)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.spConseguirCuartosDisponibles2().ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaCuartosDisponibles = resultados.Select(c => new CuartoSelectPicker_VM
                {
                    IdCuarto = c.IdCuarto,
                    CodigoCuarto = c.CodigoCuarto,
                    DescripCuarto = c.DescripCuarto
                }).ToList();

                errorMessage = null;  // No hay error, establecer el mensaje a null
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        #endregion

        #region Validar existencias
        public bool VerificarExistenciaCorreo(string Correo)
        {
            // Buscar el elemento en la tabla correspondiente
            var elemento = bd.Cliente.FirstOrDefault(e => e.correo == Correo);

            // Verificar si el elemento existe en la tabla
            if (elemento != null)
            {
                // El elemento ya existe en la tabla
                return true;
            }
            else
            {
                // El elemento no existe en la tabla
                return false;
            }
        }

        public bool VerificarExistenciaCorreoConId(string Correo, int ID)
        {
            // Buscar el elemento en la tabla correspondiente
            // verificar si el codigo existe
            // el codigo pertenece a mi id?
            // si pertenece devolver true para permitir edicion
            // si no me pertenece false para no permitir

            if (!VerificarExistenciaCorreo(Correo))
            {
                return true;
            }

            Cliente e = bd.Cliente.Find(ID);

            if (e != null && e.correo == Correo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerificarExistenciaCedula(string Cedula)
        {
            // Buscar el elemento en la tabla correspondiente
            var elemento = bd.Cliente.FirstOrDefault(e => e.Cedula == Cedula);

            // Verificar si el elemento existe en la tabla
            if (elemento != null)
            {
                // El elemento ya existe en la tabla
                return true;
            }
            else
            {
                // El elemento no existe en la tabla
                return false;
            }
        }


        public bool VerificarExistenciaCelular(string Celular)
        {
            // Buscar el elemento en la tabla correspondiente
            var elemento = bd.Cliente.FirstOrDefault(e => e.TelCliente == Celular);

            // Verificar si el elemento existe en la tabla
            if (elemento != null)
            {
                // El elemento ya existe en la tabla
                return true;
            }
            else
            {
                // El elemento no existe en la tabla
                return false;
            }
        }




        //Validaciones conjuntas (ID del cliente y otros datos)

        public bool ValidarCedulaConID(string Cedula, int ID)
        {
            // verificar si el codigo existe
            // el codigo pertenece a mi id?
            // si pertenece devolver true para permitir edicion
            // si no me pertenece false para no permitir

            if (!VerificarExistenciaCedula(Cedula))
            {
                return true;
            }

            Cliente e = bd.Cliente.Find(ID);

            if (e != null && e.Cedula == Cedula)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion\

        #region CRUD
        public bool AgregarClienteyContrato(ClienteContrato_VM e)
        {
            try
            {
                // Ejecuta el procedimiento almacenado
                bd.spAgregarClienteYContrato(e.NombreCliente, e.Apellido, e.Cedula, e.TelefonoCliente, e.Correo, e.FechaInicio, e.FechaFin, e.Deposito, e.IdCuarto,1);

                // Guarda los cambios en la base de datos
                bd.SaveChanges();

                // Si no se producen errores, devuelve true
                return true;
            }
            catch (Exception)
            {
                // Si ocurre un error, registra el mensaje de error (si es necesario) y devuelve false
                return false;
            }
        }

        public bool ModificarClienteyContrato(ClienteContrato_VM e)
        {
            try
            {
                // Ejecuta el procedimiento almacenado
                bd.spModificarClienteYContrato(e.IdCliente, e.IdContrato, e.NombreCliente, e.Apellido, e.Cedula, e.TelefonoCliente, e.Correo, e.FechaInicio, e.FechaFin, e.Deposito, e.IdCuarto,1);

                // Guarda los cambios en la base de datos
                bd.SaveChanges();

                // Si no se producen errores, devuelve true
                return true;
            }
            catch (Exception)
            {
                // Si ocurre un error, registra el mensaje de error (si es necesario) y devuelve false
                return false;
            }
        }

        public bool EliminarCliente(int Id)
        {
            try
            {
                // Ejecuta el procedimiento almacenado generado automáticamente por Entity Framework
                bd.EliminarContratoConCliente(Id,1);

                // Guarda los cambios en la base de datos
                bd.SaveChanges();

                // Si no se producen errores, devuelve true
                return true;
            }
            catch (Exception)
            {
                // Si ocurre un error, registra el mensaje de error (si es necesario) y devuelve false
                // Por ejemplo: _logger.LogError("Error al modificar empleado y contrato", ex);
                return false;
            }
        }
        #endregion
    }
}
