using Datos;
using Modelo.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Seguridad
{
    public class Roles_LN
    {
        private readonly Contexto bd;

        public Roles_LN()
        {
            bd = new Contexto();
        }


        #region Poblar
        public bool ProporcionarListaRoles(ref List<Roles_VM> ListaRoles, out string errorMessage, bool EstadoRoles)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.SpListarRoles(EstadoRoles).ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaRoles = resultados.Select(r => new Roles_VM
                {
                    IdRol = r.IdRol,
                    NombreRol = r.NombreRol
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
