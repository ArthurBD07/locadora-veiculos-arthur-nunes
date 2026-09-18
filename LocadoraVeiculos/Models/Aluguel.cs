using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculos.Models;

public class Aluguel
{
    [Key]
    public int AluguelId { get; set; }

    public DateTime DataRetirada { get; set; }

    public DateTime DataPrevistaDevolucao { get; set; }

    public DateTime? DataDevolucao { get; set; }

    public decimal KmInicial { get; set; }

    public decimal? KmFinal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorDiaria { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorTotal { get; set; }

    public int ClienteId { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public Cliente Cliente { get; set; } = null!;

    public int VeiculoId { get; set; }

    [ForeignKey(nameof(VeiculoId))]
    public Veiculo Veiculo { get; set; } = null!;
}
