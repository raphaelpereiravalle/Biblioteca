using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class AssuntoMap : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> entity)
        {
            entity.ToTable("Assunto");

            entity.HasKey(e => e.IdAssunto);

            entity.Property(e => e.IdAssunto)
                  .HasColumnName("IdAssunto")
                  .IsRequired();

            entity.Property(e => e.Descricao)
                  .HasColumnName("Descricao")
                  .HasMaxLength(40)
                  .IsRequired();

            entity.HasIndex(e => e.Descricao)
                  .HasDatabaseName("IX_Assunto_Descricao");
        }
    }
}
