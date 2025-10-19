using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class AutorMap : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> entity)
        {
            entity.ToTable("Autor");

            entity.HasKey(e => e.IdAutor);

            entity.Property(e => e.IdAutor)
                  .HasColumnName("IdAutor")
                  .IsRequired();

            entity.Property(e => e.Nome)
                  .HasColumnName("Nome")
                  .HasMaxLength(60)
                  .IsRequired();

            entity.HasIndex(e => e.Nome)
                  .HasDatabaseName("IX_Autor_Nome");
        }
    }
}
