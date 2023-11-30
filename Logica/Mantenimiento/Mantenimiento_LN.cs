using Datos;
using Modelo.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Mantenimiento
{
    public class Mantenimiento_LN
    {
        private readonly Contexto bd;

        public Mantenimiento_LN()
        {
            bd = new Contexto();
        }

        #region Poblar
        public bool ProporcionarListaMantenimiento(ref List<Mantenimiento_VM> ListaMantenimientos, out string errorMessage,int EstadoCuartos)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.spObtenerMantenimiento(EstadoCuartos).ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaMantenimientos = resultados.Select(c => new Mantenimiento_VM
                {
                    DescripCuarto = c.DescripCuarto,
                    Costo = c.Costo,
                    Descripcion = c.Descripcion,
                    CodigoCuarto = c.CodigoCuarto,
                    Fecha = c.Fecha,
                    DescripDetalle = c.DescripDetalle
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
