using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models;

public class Aluguel : IValidatableObject
{
    [Key]
    public int AluguelId { get; set; }

    public DateTime DataRetirada { get; set; }

    public DateTime DataPrevistaDevolucao { get; set; }

    public DateTime? DataDevolucao { get; set; }

    [Range(typeof(decimal), "0", "999999999.9", ParseLimitsInInvariantCulture = true)]
    public decimal KmInicial { get; set; }

    [Range(typeof(decimal), "0", "999999999.9", ParseLimitsInInvariantCulture = true)]
    public decimal? KmFinal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Range(typeof(decimal), "0.01", "99999999.99", ParseLimitsInInvariantCulture = true)]
    public decimal ValorDiaria { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Range(typeof(decimal), "0", "99999999.99", ParseLimitsInInvariantCulture = true)]
    public decimal ValorTotal { get; set; }

    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }

    [ForeignKey(nameof(ClienteId))]
    [JsonIgnore]
    [ValidateNever]
    public Cliente Cliente { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int VeiculoId { get; set; }

    [ForeignKey(nameof(VeiculoId))]
    [JsonIgnore]
    [ValidateNever]
    public Veiculo Veiculo { get; set; } = null!;
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DataRetirada == default || DataPrevistaDevolucao == default)
            yield return new ValidationResult("Informe retirada e devolução prevista.", new[] { nameof(DataRetirada), nameof(DataPrevistaDevolucao) });
        if (DataPrevistaDevolucao <= DataRetirada)
            yield return new ValidationResult("A devolução prevista deve ser posterior à retirada.", new[] { nameof(DataPrevistaDevolucao) });
        if (DataDevolucao.HasValue && DataDevolucao.Value < DataRetirada)
            yield return new ValidationResult("A devolução não pode anteceder a retirada.", new[] { nameof(DataDevolucao) });
        if (KmFinal.HasValue && KmFinal.Value < KmInicial)
            yield return new ValidationResult("A quilometragem final não pode ser menor que a inicial.", new[] { nameof(KmFinal) });
        if (DataDevolucao.HasValue != KmFinal.HasValue)
            yield return new ValidationResult("Informe a data de devolução e a quilometragem final juntas.", new[] { nameof(DataDevolucao), nameof(KmFinal) });
    }
}
