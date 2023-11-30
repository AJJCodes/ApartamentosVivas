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

        // para poblar la vista modificar de cliente
        [HttpGet]
        public ActionResult ObtenerCuartoPorID(int IdCuarto)
        {
            var Cuarto = ln.PoblarVistaModificar(IdCuarto);
            if (Cuarto == null)
            {
                return HttpNotFound();
            }

            return PartialView("Modificar", Cuarto);
        }
        #endregion

        #region Validaciones

        [HttpPost]
        public ActionResult ValidarCodigo(string Codigo)
        {
            bool resultado = ln.VerificarExistenciaCodigo(Codigo);
            return Json(!resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ValidarCodigoConId(string Codigo, int ID)
        {
            bool resultado = ln.ValidarCodigoConId(Codigo, ID);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CRUD
        [HttpPost]
        public ActionResult AgregarCuarto(CuartoVista_VM CuartoVM)
        {
            try
            {
                CuartoVM.EstadoMante = false;
                CuartoVM.EstadoRenta = true;
                bool resultado = ln.AgregarCuarto(CuartoVM);

                if (resultado)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult ModificarCuarto(CuartoVista_VM CuartoVM)
        {
            try
            {
                bool resultado = ln.ModificarCuarto(CuartoVM);

                if (resultado)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult EliminarCuarto(int IdCuarto)
        {
            try
            {
                bool resultado = ln.EliminarCuarto(IdCuarto);

                if (resultado)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
        #endregion
    }
}