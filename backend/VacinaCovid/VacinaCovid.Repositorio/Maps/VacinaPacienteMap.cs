using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacinaCovid.Entidade.Entidades;
using VacinaCovid.Entidade.Enum;

namespace VacinaCovid.Repositorio.Map
{
    public class VacinaPacienteMap : IEntityTypeConfiguration<VacinaPaciente>
    {
        public void Configure(EntityTypeBuilder<VacinaPaciente> builder)
        {
            builder.ToTable("tb_agendamento");

            builder.HasKey(vp => vp.Id);

            builder.Property(vp => vp.Id)
                .HasColumnName("id_agendamento")
                .ValueGeneratedOnAdd();

            builder.Property(vp => vp.IdPaciente)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(vp => vp.IdVacina)
                .HasColumnName("id_vacina")
                .IsRequired();

            builder.Property(vp => vp.DataVacinacao)
                .HasColumnName("dat_agendamento")
                .IsRequired();

            builder.Property(vp => vp.HoraVacinacao)
                .HasColumnName("hor_agendamento")
                .IsRequired();

            builder.Property(vp => vp.Status)
                .HasColumnName("dsc_status")
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(),
                    v => (StatusEnum)Enum.Parse(typeof(StatusEnum), v))
                .IsRequired();

            builder.Property(vp => vp.LocalVacinacao)
                .HasColumnName("dsc_local")
                .HasMaxLength(255);

            builder.Property(vp => vp.Observacoes)
                .HasColumnName("dsc_observacoes")
                .HasMaxLength(500);

            builder.Property(vp => vp.DataCriacao)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.Property(vp => vp.DataAtualizacao)
                .HasColumnName("dat_atualizacao");

            builder.HasOne<Paciente>()
                .WithMany()
                .HasForeignKey(vp => vp.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}