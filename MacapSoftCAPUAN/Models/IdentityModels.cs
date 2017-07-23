using System.Data.Entity;
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
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("macapsoft", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Diagnostico> diagnosticoContext { get; set; }
        public DbSet<Paises> paisesContext { get; set; }
        public DbSet<Ciudades> ciudadesContext { get; set; }
        public DbSet<Localidades> localidadesContext { get; set; }
        public DbSet<Barrios> barriosContext { get; set; }
        public DbSet<Paciente> pacienteContext { get; set; }
        public DbSet<Consultante> consultanteContext { get; set; }
        public DbSet<consultantePaciente> consultantePacienteContext { get; set; }
        public DbSet<Remision> remisionContext { get; set; }
        public DbSet<Remitido> remitidoContext { get; set; }
        public DbSet<Eps> epsConext { get; set; }
        public DbSet<Inasistencias> inasistenciasContext { get; set; }
        public DbSet<TiposDocumentos> tipoDocumentoContext { get; set; }
        public DbSet<Consecutivo> consecutivoContext { get; set; }
        public DbSet<Estrato> estratoContext { get; set; }
        public DbSet<MotivosRemisiones> motivosRemisionesContext { get; set; }
    }
}