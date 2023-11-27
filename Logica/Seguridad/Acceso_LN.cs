using Datos;
using Modelo.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Seguridad
{
    public class Acceso_LN
    {
        private readonly Contexto bd;

        public Acceso_LN()
        {
            bd = new Contexto();
        }



        public bool UserHasPermission(Usuario_VM user, int idOperacion)
        {
            var hasPermission = bd.Rol_Operacion.AsNoTracking().Any(r => r.IdOperacion == idOperacion && r.IdRol == user.IdRol);
            return hasPermission;
        }

        public Usuario_VM GetUserByEmailAndPassword(string email, string password, out string errorMsg)
        {
            errorMsg = null;

            var usuarioEntity = bd.Usuario.Where(u => u.Activo).FirstOrDefault(u => u.NombreUsuario == email);

            // No se encontró el usuario
            if (usuarioEntity == null)
            {
                errorMsg = "Nombre de usuario incorrecto.";
                return null;
            }

            // Se encontró el usuario pero la contraseña no coincide
            if (usuarioEntity.Contraseña != password.Trim())
            {
                errorMsg = "Contraseña incorrecta.";
                return null;
            }

            var usuarioVM = new Usuario_VM
            {
                IdUsuario = usuarioEntity.IdUsuario,
                NombreUsuario = usuarioEntity.NombreUsuario,
                Nombre = usuarioEntity.Nombre,
                Apellidos = usuarioEntity.Apellidos,
                Email = usuarioEntity.Email,
                IdRol = usuarioEntity.IdRol,
                Activo = usuarioEntity.Activo
            };

            return usuarioVM;
        }

        public Dictionary<string, List<Controlador_VM>> GetAllowedControllersForUser(Usuario_VM user)
        {
            // Obtiene los controladores permitidos en una sola consulta
            var allowedControllers = (from ro in bd.Rol_Operacion.AsNoTracking()
                                      join op in bd.Operaciones.AsNoTracking() on ro.IdOperacion equals op.IdOperacion
                                      join co in bd.Controlador.AsNoTracking() on op.IdControlador equals co.IdControlador
                                      where ro.IdRol == user.IdRol && co.Activo == true
                                      select new { op.NombreOperacion, co.NombreControlador, co.Icono, ModuloNombre = co.Modulo.NombreModulo })
                                      .Distinct()
                                      .ToList();

            // Agrupa los controladores por modulo y crea instancias de Controlador_VM
            var groupedByModule = allowedControllers
                .GroupBy(co => co.ModuloNombre)
                .ToDictionary(g => g.Key, g => g.Select(co => new Controlador_VM
                {
                    NombreControlador = co.NombreControlador,
                    Icono = co.Icono
                }).ToList());

            return groupedByModule;
        }

    }
}
