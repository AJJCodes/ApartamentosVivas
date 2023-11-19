using Logica.Seguridad;
using Modelo.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Seguridad
{
    public class BaseController : Controller
    {
        private readonly Acceso_LN ln;
        public BaseController()
        {
            ln = new Acceso_LN();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (Session["User"] != null)
            {
                if (Session["AllowedOperationsLoaded"] == null)
                {
                    var user = (Usuario_VM)Session["User"];
                    var allowedControllers = ln.GetAllowedControllersForUser(user);

                    Session["ModulesWithControllers"] = allowedControllers;
                    Session["AllowedOperationsLoaded"] = true;
                }

                // Asigna el valor de Session a ViewBag
                ViewBag.ModulesWithControllers = Session["ModulesWithControllers"];
            }
        }
    }
}