using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Mappings
{
    public class LivroValorMap : IEntityTypeConfiguration<LivroValor>
    {
        public void Configure(EntityTypeBuilder<LivroValor> entity)
        {
            entity.ToTable("LivroValor");

            entity.HasKey(e => e.IdLivroValor);

            entity.Property(e => e.IdLivroValor)
                  .HasColumnName("IdLivroValor")
                  .IsRequired();

            entity.Property(e => e.IdLivro)
                  .HasColumnName("IdLivro")
                  .IsRequired();

            entity.Property(e => e.TipoVenda)
                  .HasColumnName("TipoVenda")
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(e => e.Valor)
                  .HasColumnName("Valor")
                  .HasPrecision(10, 2)
                  .IsRequired();

            entity.HasOne(e => e.Livro)
                  .WithMany(l => l.LivroValores)
                  .HasForeignKey(e => e.IdLivro)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
