using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class LivroAssuntoMap : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> entity)
        {
            entity.ToTable("Livro_Assunto");

            entity.HasKey(e => new { e.IdLivro, e.IdAssunto });

            entity.Property(e => e.IdLivro)
                  .HasColumnName("IdLivro")
                  .IsRequired();

            entity.Property(e => e.IdAssunto)
                  .HasColumnName("IdAssunto")
                  .IsRequired();

            entity.HasOne<Livro>()
                  .WithMany()
                  .HasForeignKey(e => e.IdLivro)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Assunto>()
                  .WithMany()
                  .HasForeignKey(e => e.IdAssunto)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
