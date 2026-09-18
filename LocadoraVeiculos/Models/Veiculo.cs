using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models;

public class Veiculo
{
    [Key]
    public int VeiculoId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Modelo { get; set; } = string.Empty;

    public int AnoFabricacao { get; set; }

    public decimal Quilometragem { get; set; }

    [Required]
    [MaxLength(10)]
    public string Placa { get; set; } = string.Empty;

    public int FabricanteId { get; set; }

    [ForeignKey(nameof(FabricanteId))]
    public Fabricante Fabricante { get; set; } = null!;

    public int CategoriaId { get; set; }

    [ForeignKey(nameof(CategoriaId))]
    public Categoria Categoria { get; set; } = null!;

    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}
