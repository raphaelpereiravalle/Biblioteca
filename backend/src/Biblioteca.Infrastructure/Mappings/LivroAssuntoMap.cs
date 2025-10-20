using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class LivroAssuntoMap : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> entity)
        {
            entity.ToTable("LivroAssunto");

            entity.HasKey(e => new { e.IdLivro, e.IdAssunto });

            entity.Property(e => e.IdLivro)
                  .HasColumnName("IdLivro");

            entity.Property(e => e.IdAssunto)
                  .HasColumnName("IdAssunto");

            entity.HasOne(e => e.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(e => e.IdLivro);

            entity.HasOne(e => e.Assunto)
                .WithMany(a => a.LivroAssuntos)
                .HasForeignKey(e => e.IdAssunto);
        }
    }
}
