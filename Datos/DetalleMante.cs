//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleMante
    {
        public string DescripDetalle { get; set; }
        public double Costo { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> IdCuarto { get; set; }
        public Nullable<int> IdMante { get; set; }
        public int IdDetaMante { get; set; }
        public Nullable<int> CreadoPor { get; set; }
        public Nullable<int> ModificaPor { get; set; }
        public Nullable<int> EliminaPor { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
        public virtual Cuarto Cuarto { get; set; }
        public virtual Mantenimiento Mantenimiento { get; set; }
    }
}
