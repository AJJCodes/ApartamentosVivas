using Logica.Cuartos;
using Modelo.Cuartos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers.Seguridad;
using Web.Filters;

namespace Web.Controllers.Cuartos
{
    [VerificaSession]
    public class CuartosController : BaseController
    {

        private readonly Cuarto_LN ln;


        //Instanciamos el objeto en el controlador
        public CuartosController()
        {
            ln = new Cuarto_LN();
        }


        [AuthorizeUser(IdOperacion: 4)]
        public ActionResult Index()
        {
            return View();
        }

        #region Poblar
        [HttpPost]
        public ActionResult ObtenerListaCuartos()
        {
            List<Cuarto_VM> ListaCuartos = new List<Cuarto_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ProporcionarListaCuartos(ref ListaCuartos, out errorMessage);

            if (exito)
            {
                // Devolver la lista de usuarios en el formato esperado por DataTables
                return Json(new { data = ListaCuartos });
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