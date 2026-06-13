using ConhecimentoBiblico.Domain.Conhecimento;
using ConhecimentoBiblico.Domain.Ensino;
using Microsoft.EntityFrameworkCore;

namespace ConhecimentoBiblico.Infrastructure.Data;

public class ConhecimentoBiblicoDbContext : DbContext
{
    public ConhecimentoBiblicoDbContext(DbContextOptions<ConhecimentoBiblicoDbContext> options)
        : base(options) { }

    public DbSet<ElementoBiblico> ElementosBiblicos => Set<ElementoBiblico>();
    public DbSet<ConexaoBiblica> ConexoesBiblicas => Set<ConexaoBiblica>();
    public DbSet<PerguntaBiblica> PerguntasBiblicas => Set<PerguntaBiblica>();
    public DbSet<ReflexaoBiblica> ReflexoesBiblicas => Set<ReflexaoBiblica>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConhecimentoBiblicoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
