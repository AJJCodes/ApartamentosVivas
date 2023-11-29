using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Cuartos
{
    public class Cuarto_VM
    {
        public string DescripCuarto { get; set; }
        public double Costo { get; set; }
        public Nullable<bool> EstadoRenta { get; set; }
        public Nullable<bool> EstadoMante { get; set; }
        public string CodigoCuarto { get; set; }
        public int IdCuarto { get; set; }
        public Nullable<int> CreadoPor { get; set; }
        public Nullable<int> ModificaPor { get; set; }
        public Nullable<int> EliminaPor { get; set; }
    }
}
