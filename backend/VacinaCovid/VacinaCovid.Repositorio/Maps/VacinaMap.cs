using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacinaCovid.Entidade.Entidades;

namespace VacinaCovid.Repositorio.Map
{
    public class VacinaMap : IEntityTypeConfiguration<Vacina>
    {
        public void Configure(EntityTypeBuilder<Vacina> builder)
        {
            builder.ToTable("tb_vacina");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("id_vacina")
                .ValueGeneratedOnAdd();

            builder.Property(v => v.Nome)
                .HasColumnName("dsc_nome")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(v => v.Fabricante)
                .HasColumnName("dsc_fabricante")
                .HasMaxLength(255);

            builder.Property(v => v.Lote)
                .HasColumnName("dsc_lote")
                .HasMaxLength(100);

            builder.Property(v => v.DataValidade)
                .HasColumnName("dat_validade");

            builder.Property(v => v.Quantidade)
                .HasColumnName("qtd_quantidade");

            builder.Property(v => v.DataCriacao)
                .HasColumnName("dat_criacao")
                .IsRequired();
        }
    }
}