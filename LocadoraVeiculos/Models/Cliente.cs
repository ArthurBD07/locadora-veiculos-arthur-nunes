using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Models;

public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(14)]
    [RegularExpression(@"\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "Informe o CPF com 11 dígitos ou no formato 000.000.000-00.")]
    public string Cpf { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    [ValidateNever]
    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}
