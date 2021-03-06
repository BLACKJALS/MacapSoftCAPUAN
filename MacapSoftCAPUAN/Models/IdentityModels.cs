﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MacapSoftCAPUAN.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public bool Activo { get; set; }
        public string nombreUsuario { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("macapsoft", throwIfV1Schema: false)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        static ApplicationDbContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Diagnostico> diagnosticoContext { get; set; }
        public DbSet<Paises> paisesContext { get; set; }
        public DbSet<Ciudades> ciudadesContext { get; set; }
        public DbSet<Departamentos> departamentosContext { get; set; }
        public DbSet<Localidades> localidadesContext { get; set; }
        public DbSet<Barrios> barriosContext { get; set; }
        public DbSet<Paciente> pacienteContext { get; set; }
        public DbSet<Consultante> consultanteContext { get; set; }
        //public DbSet<consultantePaciente> consultantePacienteContext { get; set; }
        public DbSet<Remision> remisionContext { get; set; }
        public DbSet<Remitido> remitidoContext { get; set; }
        public DbSet<Eps> epsConext { get; set; }
        public DbSet<Inasistencias> inasistenciasContext { get; set; }
        public DbSet<TiposDocumentos> tipoDocumentoContext { get; set; }
        public DbSet<Consecutivo> consecutivoContext { get; set; }
        public DbSet<Estrato> estratoContext { get; set; }
        public DbSet<MotivosRemisiones> motivosRemisionesContext { get; set; }
        public DbSet<IngresoClinica> ingresoClinicaContext { get; set; }
        //public DbSet<Profesion> profesionContext { get; set; }
        public DbSet<CierreHC> cierreHcContext { get; set; }
        public DbSet<CategorizacionCAP> categorizacionCAPContext { get; set; }
        public DbSet<Ocupacion> ocupacionCAPContext { get; set; }
        public DbSet<Sexo> sexoContext { get; set; }
        public DbSet<NivelEscolaridad> nivelEscolaridadContext { get; set; }
        public DbSet<IngresoEstrategiasEvaluacion> ingresoEstrategiasEvaluacionContext { get; set; }
        public DbSet<Consulta> consultaContext { get; set; }
        public DbSet<ConsultaDiagnostico> consultaDiagnosticoContext { get; set; }
        public DbSet<PermisosUsuariosPaciente> permisosUsuariosPacienteContext { get; set; }
        public DbSet<MotivoCierreHistoriaClinica> motivoCierreHcContext { get; set; }
        public DbSet<CategorizacionHC> categorizcionHcContext { get; set; }
        public DbSet<MotivosCierre> motivosCierreContext { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}