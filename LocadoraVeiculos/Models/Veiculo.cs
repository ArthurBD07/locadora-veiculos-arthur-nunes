using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

    [Range(1886, 2100)]
    public int AnoFabricacao { get; set; }

    [Range(typeof(decimal), "0", "999999999.9", ParseLimitsInInvariantCulture = true)]
    public decimal Quilometragem { get; set; }

    [Required]
    [MaxLength(10)]
    [RegularExpression(@"[A-Za-z]{3}-?[0-9][A-Za-z0-9][0-9]{2}", ErrorMessage = "Informe uma placa como ABC1234 ou ABC1D23, com hífen opcional.")]
    public string Placa { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int FabricanteId { get; set; }

    [ForeignKey(nameof(FabricanteId))]
    [JsonIgnore]
    [ValidateNever]
    public Fabricante Fabricante { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int CategoriaId { get; set; }

    [ForeignKey(nameof(CategoriaId))]
    [JsonIgnore]
    [ValidateNever]
    public Categoria Categoria { get; set; } = null!;

    [JsonIgnore]
    [ValidateNever]
    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}
