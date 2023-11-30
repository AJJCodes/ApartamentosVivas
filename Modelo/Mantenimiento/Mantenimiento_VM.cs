using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Mantenimiento
{
    public class Mantenimiento_VM
    {
        public string CodigoCuarto { get; set; }
        public string DescripCuarto { get; set; }
        public string DescripDetalle { get; set; }
        public double Costo { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
