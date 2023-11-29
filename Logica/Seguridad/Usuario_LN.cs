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
        public bool ProporcionarListaUsuarios(ref List<UsuarioRoles_VM> ListaUsuarios, out string errorMessage, bool EstadoUsuario)
        {
            try
            {
                // Suponiendo que bd es tu objeto de contexto de base de datos
                var resultados = bd.SpListarUsuariosRoles(EstadoUsuario).ToList();

                // Mapear los resultados a la lista de UsuarioRoles_VM
                ListaUsuarios = resultados.Select(r => new UsuarioRoles_VM
                {
                    IdUsuario = r.IdUsuario,
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

        public bool PropocionarListaRoles(ref List<Roles_VM> ListaRoles, out string errorMessage, bool EstadoRoles)
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

        #region Validaciones
        public bool VerificarExistenciaUsuario(string NombreUsuario)
        {
            // Buscar el elemento en la tabla correspondiente
            var elemento = bd.Usuario.FirstOrDefault(e => e.NombreUsuario == NombreUsuario);

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
        #endregion
    }
}
