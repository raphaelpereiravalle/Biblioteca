using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> entity)
        {
            entity.ToTable("Livro");

            entity.HasKey(e => e.IdLivro);

            entity.Property(e => e.IdLivro)
                  .HasColumnName("IdLivro")
                  .IsRequired();

            entity.Property(e => e.Titulo)
                  .HasColumnName("Titulo")
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Editora)
                  .HasColumnName("Editora")
                  .HasMaxLength(60);

            entity.Property(e => e.Edicao)
                  .HasColumnName("Edicao");

            entity.Property(e => e.AnoPublicacao)
                  .HasColumnName("AnoPublicacao")
                  .HasMaxLength(4);

            entity.HasIndex(e => e.Titulo)
                  .HasDatabaseName("IX_Livro_Titulo");
        }
    }
}
