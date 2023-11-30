using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Clientes
{
    public class ClienteYContrato_VM
    {
        public string Nombre_Completo { get; set; }
        public string Cedula { get; set; }
        public string correo { get; set; }
        public double Deposito { get; set; }
        public string CodigoCuarto { get; set; }
        public string DescripCuarto { get; set; }
        public string TelCliente { get; set; }
        public int IdCliente { get; set; }
        public int IdContrato { get; set; }

        public int IdCuarto { get; set; }
    }
    public class ClienteContrato_VM
    {
        public string NombreCliente { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string TelefonoCliente { get; set; }
        public string Correo { get; set; }
        //Datos del contrato
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; } // Este puede ser null
        public decimal Deposito { get; set; }
        public int IdCuarto { get; set; }

        public int IdCliente { get; set; }
        public int IdContrato { get; set; }

    }

    //Este lo ocupamos para poblar la vista modificar etc

    public class ClienteyContratoConID_VM
    {
        public int IdCliente { get; set; }
        public string Cedula { get; set; }
        public string correo { get; set; }
        public string TelCliente { get; set; }
        public int IdContrato { get; set; }
        public Nullable<int> IdCuarto { get; set; }
        public double Deposito { get; set; }
        public string CodigoCuarto { get; set; }
        public string DescripCuarto { get; set; }
        public string NomCliente { get; set; }
        public string Apellido { get; set; }
        public System.DateTime FechaIni { get; set; }
        public Nullable<System.DateTime> Fechafin { get; set; }

    }
}
