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
    }
}
