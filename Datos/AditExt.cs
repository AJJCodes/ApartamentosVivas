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
    
    public partial class AditExt
    {
        public double Costo { get; set; }
        public int IdApaElec { get; set; }
        public int IdAditExt { get; set; }
        public Nullable<int> IdContrato { get; set; }
        public Nullable<int> CreadoPor { get; set; }
        public Nullable<int> ModificaPor { get; set; }
        public Nullable<int> EliminaPor { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
        public virtual Contrato Contrato { get; set; }
        public virtual AparElec AparElec { get; set; }
    }
}
