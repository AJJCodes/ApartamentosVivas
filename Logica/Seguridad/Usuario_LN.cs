using Datos;
using Modelo.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Seguridad
{
    public class Usuario_LN
    {
        private readonly Contexto bd;

        public Usuario_LN()
        {
            bd = new Contexto();
        }

        #region Poblar
        public bool ProporcionarListaUsuarios(ref List<UsuarioRoles_VM> ListaUsuarios, out string errorMessage)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.SpListarUsuariosRoles().ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaUsuarios = resultados.Select(r => new UsuarioRoles_VM
                {
                    Nombre = r.Nombre,
                    Apellidos = r.Apellidos,
                    NombreUsuario = r.NombreUsuario,
                    Email = r.Email,
                    FechaCreacion = r.FechaCreacion,
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
