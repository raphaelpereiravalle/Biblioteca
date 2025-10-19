using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> entity)
        {
            entity.ToTable("Livro_Autor");

            entity.HasKey(e => new { e.IdLivro, e.IdAutor });

            entity.Property(e => e.IdLivro)
                  .HasColumnName("IdLivro")
                  .IsRequired();

            entity.Property(e => e.IdAutor)
                  .HasColumnName("IdAutor")
                  .IsRequired();

            entity.HasOne<Livro>()
                  .WithMany()
                  .HasForeignKey(e => e.IdLivro)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Autor>()
                  .WithMany()
                  .HasForeignKey(e => e.IdAutor)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
