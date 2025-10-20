using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> entity)
        {
            entity.ToTable("LivroAutor");

            entity.HasKey(e => new { e.IdLivro, e.IdAutor });

            entity.Property(e => e.IdLivro)
                  .HasColumnName("IdLivro")
                  .IsRequired();

            entity.Property(e => e.IdAutor)
                  .HasColumnName("IdAutor")
                  .IsRequired();

            entity.HasOne(e => e.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(e => e.IdLivro);

            entity.HasOne(e => e.Autor)
                .WithMany(a => a.LivroAutores)
                .HasForeignKey(e => e.IdAutor);
        }
    }
}
