using Logica.Seguridad;
using Modelo.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.Seguridad
{
    [VerificaSession]
    public class UsuarioController : BaseController
    {

        private readonly Usuario_LN ln;


        //Instanciamos el objeto en el controlador
        public UsuarioController()
        {
            ln = new Usuario_LN();
        }

        [AuthorizeUser(IdOperacion: 1)]
        public ActionResult Index()
        {
            return View();
        }

        #region Poblar
        [HttpPost]
        public ActionResult ObtenerListaUsuarios(bool UsuariosActivos)
        {
            List<UsuarioRoles_VM> listaUsuarios = new List<UsuarioRoles_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ProporcionarListaUsuarios(ref listaUsuarios, out errorMessage, UsuariosActivos);

            if (exito)
            {
                // Devolver la lista de usuarios en el formato esperado por DataTables
                return Json(new { data = listaUsuarios });
            }
            else
            {
                // Devolver el mensaje de error en caso de fallo
                return Json(new { error = errorMessage });
            }
        }

        [HttpPost]
        public ActionResult ObtenerListaRoles(bool RolesActivos)
        {
            List<Roles_VM> ListaRoles = new List<Roles_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.PropocionarListaRoles(ref ListaRoles, out errorMessage, RolesActivos);

            if (exito)
            {
                // Devolver la lista de usuarios en el formato esperado por DataTables
                return Json(new { data = ListaRoles });
            }
            else
            {
                // Devolver el mensaje de error en caso de fallo
                return Json(new { error = errorMessage });
            }
        }
        #endregion

        #region Validaciones
        //Validar Usuario
        [HttpPost]
        public ActionResult ValidarUsuario(string Usuario)
        {
            bool resultado = ln.VerificarExistenciaUsuario(Usuario);
            return Json(!resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}