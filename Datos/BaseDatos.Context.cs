﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Controlador> Controlador { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Operaciones> Operaciones { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rol_Operacion> Rol_Operacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<AditExt> AditExt { get; set; }
        public virtual DbSet<AparElec> AparElec { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Cuarto> Cuarto { get; set; }
        public virtual DbSet<DañoCliente> DañoCliente { get; set; }
        public virtual DbSet<Daños> Daños { get; set; }
        public virtual DbSet<DetalleMante> DetalleMante { get; set; }
        public virtual DbSet<Mantenimiento> Mantenimiento { get; set; }
        public virtual DbSet<Mora> Mora { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<TipoPago> TipoPago { get; set; }
    
        public virtual ObjectResult<SpListarUsuariosRoles_Result> SpListarUsuariosRoles(Nullable<bool> estadoUsuario)
        {
            var estadoUsuarioParameter = estadoUsuario.HasValue ?
                new ObjectParameter("EstadoUsuario", estadoUsuario) :
                new ObjectParameter("EstadoUsuario", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpListarUsuariosRoles_Result>("SpListarUsuariosRoles", estadoUsuarioParameter);
        }
    
        public virtual ObjectResult<SpListarRoles_Result> SpListarRoles(Nullable<bool> estadoRol)
        {
            var estadoRolParameter = estadoRol.HasValue ?
                new ObjectParameter("EstadoRol", estadoRol) :
                new ObjectParameter("EstadoRol", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpListarRoles_Result>("SpListarRoles", estadoRolParameter);
        }
    }
}
