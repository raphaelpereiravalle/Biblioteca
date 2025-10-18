using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

    public DbSet<Livro> Livros => Set<Livro>();
    public DbSet<Autor> Autores => Set<Autor>();
    public DbSet<Assunto> Assuntos => Set<Assunto>();
    public DbSet<LivroValor> LivroValores => Set<LivroValor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity => 
        {
            entity.ToTable("Autor");
            entity.HasKey(x => x.IdAutor);
            
            entity.Property(x=>x.IdAutor)
                .HasColumnName("IdAutor")
                .IsRequired();

            entity.Property(x => x.Nome)
                .HasColumnName("Nome");
        });

        modelBuilder.Entity<Assunto>(entity =>
        {
            entity.ToTable("Assunto");
            entity.HasKey(x => x.IdAssunto);

            entity.Property(x => x.IdAssunto)
                .HasColumnName("IdAssunto")
                .IsRequired();

            entity.Property(l => l.Descricao)
                .HasColumnName("Descricao");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.ToTable("Livro");
            entity.HasKey(l => l.IdLivro);

            entity.Property(x => x.IdLivro)
                .HasColumnName("IdLivro")
                .IsRequired();

            entity.Property(l => l.Titulo)
                .HasColumnName("Titulo")
                .IsRequired();

            entity.Property(l => l.Editora)
                .HasColumnName("Editora");

            entity.Property(l => l.Edicao)
                .HasColumnName("Edicao");

            entity.Property(l => l.AnoPublicacao)
                .HasColumnName("AnoPublicacao");
        });

        modelBuilder.Entity<LivroValor>(entity =>
        {
            entity.ToTable("LivroValor");
            entity.HasKey(x => x.IdLivroValor);

            entity.Property(x => x.IdLivroValor)
                .HasColumnName("IdLivroValor")
                .IsRequired();

            entity.Property(l => l.IdLivro)
                .HasColumnName("IdLivro")
                .IsRequired();

            entity.Property(l => l.TipoVenda)
                .HasColumnName("TipoVenda");

            entity.Property(l => l.Valor)
                .HasColumnName("Valor");
        });

        modelBuilder.Entity<LivroAutor>()
            .HasKey(x => new { x.IdLivro, x.IdAutor });

        modelBuilder.Entity<LivroAssunto>()
            .HasKey(x => new { x.IdLivro, x.IdAssunto });
    }
}
