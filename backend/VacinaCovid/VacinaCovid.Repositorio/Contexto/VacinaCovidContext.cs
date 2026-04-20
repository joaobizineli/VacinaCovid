using Microsoft.EntityFrameworkCore;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Repositorio.Map;

namespace VacinaCovid.Repositorio.Context
{
    public class VacinaCovidContext : DbContext
    {
        public VacinaCovidContext(DbContextOptions<VacinaCovidContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Vacina> Vacinas { get; set; }
        public DbSet<VacinaPaciente> VacinaPacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new VacinaMap());
            modelBuilder.ApplyConfiguration(new VacinaPacienteMap());
        }
    }
}