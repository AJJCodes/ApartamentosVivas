using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Reportes
{
    public class ReportesCuartoConCliente_VM
    {
        public string CodigoCuarto { get; set; }
        public string NomCliente { get; set; }
        public string DescripCuarto { get; set; }
        public double Costo { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string correo { get; set; }
        public string TelCliente { get; set; }
    }

    public class ClienteDropDown_VM
    {
        public int IdCliente { get; set; }
        public string NomCliente { get; set; }
        public string Apellido { get; set; }
    }
}
