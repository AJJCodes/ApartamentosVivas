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
    
    public partial class Contrato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contrato()
        {
            this.AditExt = new HashSet<AditExt>();
            this.Pago = new HashSet<Pago>();
            this.DañoCliente = new HashSet<DañoCliente>();
        }
    
        public System.DateTime FechaIni { get; set; }
        public Nullable<System.DateTime> Fechafin { get; set; }
        public double Deposito { get; set; }
        public Nullable<int> IdCuarto { get; set; }
        public Nullable<bool> Estado { get; set; }
        public int IdContrato { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> CreadoPor { get; set; }
        public Nullable<int> ModificaPor { get; set; }
        public Nullable<int> AnuladoPor { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AditExt> AditExt { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Cuarto Cuarto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DañoCliente> DañoCliente { get; set; }
    }
}