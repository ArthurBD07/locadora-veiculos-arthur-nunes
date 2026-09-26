using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models;

public class Fabricante
{
    [Key]
    public int FabricanteId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [JsonIgnore]
    [ValidateNever]
    public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
