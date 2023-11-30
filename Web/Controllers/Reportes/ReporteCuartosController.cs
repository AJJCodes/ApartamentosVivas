using Logica.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers.Seguridad;
using Web.Filters;

namespace Web.Controllers.Reportes
{
    [VerificaSession]
    public class ReporteCuartosController : BaseController
    {

        private readonly ReporteCuartoClientes_LN ln;


        //Instanciamos el objeto en el controlador
        public ReporteCuartosController()
        {
            ln = new ReporteCuartoClientes_LN();
        }
        [AuthorizeUser(IdOperacion: 7)]
        public ActionResult Index()
        {
            return View();
        }

        #region Poblar
        [HttpGet]
        public ActionResult ObtenerReporteClienteCuarto(int IdCliente)
        {
            var CuartoyCliente = ln.PoblarReporte(IdCliente);
            if (CuartoyCliente == null)
            {
                return HttpNotFound();
            }

            return PartialView("Index", CuartoyCliente);
        }
        #endregion
    }
}