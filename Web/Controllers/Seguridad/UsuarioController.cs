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
        public ActionResult ObtenerListaUsuarios()
        {
            List<UsuarioRoles_VM> listaUsuarios = new List<UsuarioRoles_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ProporcionarListaUsuarios(ref listaUsuarios, out errorMessage);

            if (exito)
            {
                // Aquí puedes devolver la lista de usuarios en el formato que necesites
                return Json(listaUsuarios); // Esto devolverá la lista de usuarios como JSON
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