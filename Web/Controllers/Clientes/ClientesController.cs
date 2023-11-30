using Logica.Clientes;
using Modelo.Clientes;
using Modelo.Cuartos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers.Seguridad;
using Web.Filters;

namespace Web.Controllers.Clientes
{
    [VerificaSession]
    public class ClientesController : BaseController
    {

        private readonly Clientes_LN ln;


        //Instanciamos el objeto en el controlador
        public ClientesController()
        {
            ln = new Clientes_LN();
        }

        [AuthorizeUser(IdOperacion: 6)]
        public ActionResult Index()
        {
            return View();
        }

        #region Poblar
        [HttpPost]
        public ActionResult ObtenerListaClientes(int EstadoCliente)
        {
            List<ClienteYContrato_VM> ListaClientes = new List<ClienteYContrato_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ProporcionarListaClientesyContrato(ref ListaClientes, out errorMessage, EstadoCliente);

            if (exito)
            {
                // Devolver la lista de usuarios en el formato esperado por DataTables
                return Json(new { data = ListaClientes });
            }
            else
            {
                // Devolver el mensaje de error en caso de fallo
                return Json(new { error = errorMessage });
            }
        }

        [HttpPost]
        public ActionResult ObtenerListaCuartos()
        {
            List<CuartoSelectPicker_VM> ListaCuartos = new List<CuartoSelectPicker_VM>();
            string errorMessage = null;

            // Llamar a tu función para obtener la lista de usuarios
            bool exito = ln.ObtenerListaCuartosDisponibles(ref ListaCuartos, out errorMessage);

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
        public ActionResult ObtenerClienteycontratoPorId(int IdCliente)
        {
            var ClienteyContrato = ln.PoblarVistaModificar(IdCliente);
            if (ClienteyContrato == null)
            {
                return HttpNotFound();
            }

            return PartialView("Modificar", ClienteyContrato);
        }
        #endregion
    }
}