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
    
    public partial class Controlador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Controlador()
        {
            this.Operaciones = new HashSet<Operaciones>();
        }
    
        public int IdControlador { get; set; }
        public string NombreControlador { get; set; }
        public Nullable<int> IdModulo { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string Icono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operaciones> Operaciones { get; set; }
        public virtual Modulo Modulo { get; set; }
    }
}