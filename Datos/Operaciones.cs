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
    
    public partial class Operaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operaciones()
        {
            this.Rol_Operacion = new HashSet<Rol_Operacion>();
        }
    
        public int IdOperacion { get; set; }
        public string NombreOperacion { get; set; }
        public Nullable<int> IdControlador { get; set; }
    
        public virtual Controlador Controlador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rol_Operacion> Rol_Operacion { get; set; }
    }
}