using Datos;
using Modelo.Cuartos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Cuartos
{
    public class Cuarto_LN
    {
        private readonly Contexto bd;

        public Cuarto_LN()
        {
            bd = new Contexto();
        }

        #region Poblar
        public bool ProporcionarListaCuartos(ref List<Cuarto_VM> ListaCuartos, out string errorMessage)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.spConseguirCuartos().ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaCuartos = resultados.Select(c => new Cuarto_VM
                {
                    IdCuarto = c.IdCuarto,
                    Costo = c.Costo , 
                    EstadoRenta = c.EstadoRenta,
                    EstadoMante = c.EstadoMante,
                    CodigoCuarto = c.CodigoCuarto,
                    CreadoPor = c.CreadoPor,
                    ModificaPor = c.ModificaPor
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
