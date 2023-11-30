using Datos;
using Modelo.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Reportes
{
    public class ReporteCuartoClientes_LN
    {
        private readonly Contexto bd;

        public ReporteCuartoClientes_LN()
        {
            bd = new Contexto();
        }

        #region Poblar

        public ReportesCuartoConCliente_VM PoblarReporte(int IdCliente)
        {
            try
            {
                var result = bd.spmostrar(IdCliente).FirstOrDefault(); // Cambiado a FirstOrDefault para obtener un solo objeto

                if (result != null)
                {
                    var ResultadoConsulta = new ReportesCuartoConCliente_VM
                    {
                        CodigoCuarto = result.CodigoCuarto,
                        NomCliente = result.NomCliente,
                        DescripCuarto = result.DescripCuarto,
                        Costo = result.Costo,
                        Apellido = result.Apellido,
                        Cedula = result.Cedula,
                        correo = result.correo,
                        TelCliente = result.TelCliente
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

        public List<ClienteDropDown_VM> PoblarDropdownCliente()
        {
            List<ClienteDropDown_VM> ClienteDropdown = bd.Database.SqlQuery<ClienteDropDown_VM>("vivas.spConseguirClientes").ToList();
            return ClienteDropdown;
        }
        #endregion

    }
}
