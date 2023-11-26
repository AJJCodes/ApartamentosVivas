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
        [AuthorizeUser(IdOperacion: 1)]
        public ActionResult IndexUsuario()
        {
            return View();
        }
    }
}