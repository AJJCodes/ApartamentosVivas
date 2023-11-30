using Logica.Mantenimiento;
using Modelo.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers.Seguridad;
using Web.Filters;

namespace Web.Controllers.Mantenimiento
{
    [VerificaSession]
    public class MantenimientoController : BaseController
    {

        private readonly Mantenimiento_LN ln;


        //Instanciamos el objeto en el controlador
        public MantenimientoController()
        {
            ln = new Mantenimiento_LN();
        }
        [AuthorizeUser(IdOperacion: 5)]
        public ActionResult Index()
        {
            return View();
        }

        #region Poblar
        [HttpPost]
        public ActionResult ObtenerListaMantenimientos(int EstadoCuarto)
        {
            List<Mantenimiento_VM> ListaMantenimientos = new List<Mantenimiento_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ProporcionarListaMantenimiento(ref ListaMantenimientos, out errorMessage, EstadoCuarto);

            if (exito)
            {
                // Devolver la lista de usuarios en el formato esperado por DataTables
                return Json(new { data = ListaMantenimientos });
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