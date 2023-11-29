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
    public class RolesController : BaseController
    {

        private readonly Roles_LN ln;


        //Instanciamos el objeto en el controlador
        public RolesController()
        {
            ln = new Roles_LN();
        }
        // GET: Roles
        [AuthorizeUser(IdOperacion: 3)]
        public ActionResult Index()
        {
            return View();
        }

        #region Poblar
        [HttpPost]
        public ActionResult ObtenerListaRoles(bool UsuariosActivos)
        {
            List<Roles_VM> ListaRoles = new List<Roles_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ProporcionarListaRoles(ref ListaRoles, out errorMessage, UsuariosActivos);

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
    }
}