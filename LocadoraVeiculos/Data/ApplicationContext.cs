using LocadoraVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos { get; set; } = null!;
    public DbSet<Fabricante> Fabricantes { get; set; } = null!;
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Aluguel> Alugueis { get; set; } = null!;
    public DbSet<Categoria> Categorias { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Veiculo>()
            .HasOne(veiculo => veiculo.Fabricante)
            .WithMany(fabricante => fabricante.Veiculos)
            .HasForeignKey(veiculo => veiculo.FabricanteId);

        modelBuilder.Entity<Veiculo>()
            .HasOne(veiculo => veiculo.Categoria)
            .WithMany(categoria => categoria.Veiculos)
            .HasForeignKey(veiculo => veiculo.CategoriaId);

        modelBuilder.Entity<Aluguel>()
            .HasOne(aluguel => aluguel.Cliente)
            .WithMany(cliente => cliente.Alugueis)
            .HasForeignKey(aluguel => aluguel.ClienteId);

        modelBuilder.Entity<Aluguel>()
            .HasOne(aluguel => aluguel.Veiculo)
            .WithMany(veiculo => veiculo.Alugueis)
            .HasForeignKey(aluguel => aluguel.VeiculoId);

        modelBuilder.Entity<Veiculo>()
            .Property(veiculo => veiculo.Quilometragem)
            .HasColumnType("decimal(10,1)");

        modelBuilder.Entity<Aluguel>()
            .Property(aluguel => aluguel.KmInicial)
            .HasColumnType("decimal(10,1)");

        modelBuilder.Entity<Aluguel>()
            .Property(aluguel => aluguel.KmFinal)
            .HasColumnType("decimal(10,1)");
    }
}
