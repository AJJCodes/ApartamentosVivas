using Logica.Seguridad;
using Modelo.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser: AuthorizeAttribute
    {
        private readonly Acceso_LN sec;
        private int IdOperacion;

        public AuthorizeUser(int IdOperacion)
        {
            this.IdOperacion = IdOperacion;
            sec = new Acceso_LN();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var oUser = (Usuario_VM)HttpContext.Current.Session["User"];
            if (oUser == null)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
                return;
            }

            var hasPermission = sec.UserHasPermission(oUser, IdOperacion);

            if (!hasPermission)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation");
            }
        }

    }
}