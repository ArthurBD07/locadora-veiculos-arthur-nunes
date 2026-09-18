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
    public string Cpf { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}
