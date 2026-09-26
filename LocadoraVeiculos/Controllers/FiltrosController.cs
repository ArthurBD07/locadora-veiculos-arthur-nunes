using LocadoraVeiculos.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FiltrosController : ControllerBase
{
    private readonly ApplicationContext _context;
    public FiltrosController(ApplicationContext context) => _context = context;

    // INNER JOIN: somente veículos que correspondem ao fabricante informado.
    [HttpGet("veiculos-por-fabricante/{fabricanteId:int}")]
    public async Task<IActionResult> VeiculosPorFabricante(int fabricanteId)
    {
        if (fabricanteId <= 0) return BadRequest(new { mensagem = "Informe um ID positivo." });
        var consulta = from v in _context.Veiculos
                       join f in _context.Fabricantes on v.FabricanteId equals f.FabricanteId
                       where f.FabricanteId == fabricanteId
                       orderby v.VeiculoId
                       select new { v.VeiculoId, v.Modelo, v.Placa, v.AnoFabricacao, v.Quilometragem, f.FabricanteId, Fabricante = f.Nome };
        return Ok(await consulta.ToListAsync());
    }

    // INNER JOIN entre veículo e categoria.
    [HttpGet("veiculos-por-categoria/{categoriaId:int}")]
    public async Task<IActionResult> VeiculosPorCategoria(int categoriaId)
    {
        if (categoriaId <= 0) return BadRequest(new { mensagem = "Informe um ID positivo." });
        var consulta = from v in _context.Veiculos
                       join c in _context.Categorias on v.CategoriaId equals c.CategoriaId
                       where c.CategoriaId == categoriaId
                       orderby v.VeiculoId
                       select new { v.VeiculoId, v.Modelo, v.Placa, c.CategoriaId, Categoria = c.Nome };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("alugueis-por-cliente/{clienteId:int}")]
    public async Task<IActionResult> AlugueisPorCliente(int clienteId)
    {
        if (clienteId <= 0) return BadRequest(new { mensagem = "Informe um ID positivo." });
        var consulta = from a in _context.Alugueis
                       join c in _context.Clientes on a.ClienteId equals c.ClienteId
                       join v in _context.Veiculos on a.VeiculoId equals v.VeiculoId
                       where c.ClienteId == clienteId
                       orderby a.AluguelId
                       select new { a.AluguelId, c.ClienteId, Cliente = c.Nome, v.VeiculoId, v.Modelo, a.DataRetirada, a.DataPrevistaDevolucao, a.DataDevolucao, a.ValorTotal };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("alugueis-por-veiculo/{veiculoId:int}")]
    public async Task<IActionResult> AlugueisPorVeiculo(int veiculoId)
    {
        if (veiculoId <= 0) return BadRequest(new { mensagem = "Informe um ID positivo." });
        var consulta = from a in _context.Alugueis
                       join v in _context.Veiculos on a.VeiculoId equals v.VeiculoId
                       join c in _context.Clientes on a.ClienteId equals c.ClienteId
                       where v.VeiculoId == veiculoId
                       orderby a.AluguelId
                       select new { a.AluguelId, v.VeiculoId, v.Modelo, v.Placa, Cliente = c.Nome, a.DataRetirada, a.DataDevolucao, a.ValorTotal };
        return Ok(await consulta.ToListAsync());
    }

    // O período filtra a data de retirada, incluindo os dois limites.
    [HttpGet("alugueis-por-periodo")]
    public async Task<IActionResult> AlugueisPorPeriodo(DateTime? inicio, DateTime? fim)
    {
        if (!inicio.HasValue || !fim.HasValue || inicio > fim)
            return BadRequest(new { mensagem = "Informe inicio e fim válidos, com inicio menor ou igual a fim." });
        var consulta = from a in _context.Alugueis
                       join c in _context.Clientes on a.ClienteId equals c.ClienteId
                       where a.DataRetirada >= inicio.Value && a.DataRetirada <= fim.Value
                       orderby a.DataRetirada, a.AluguelId
                       select new { a.AluguelId, Cliente = c.Nome, a.VeiculoId, a.DataRetirada, a.DataPrevistaDevolucao, a.DataDevolucao };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("alugueis-por-status")]
    public async Task<IActionResult> AlugueisPorStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status)) return BadRequest(new { mensagem = "Use aberto ou devolvido." });
        status = status.Trim().ToLowerInvariant();
        if (status != "aberto" && status != "devolvido")
            return BadRequest(new { mensagem = "Use aberto ou devolvido." });
        bool devolvido = status == "devolvido";
        var consulta = from a in _context.Alugueis
                       join c in _context.Clientes on a.ClienteId equals c.ClienteId
                       where a.DataDevolucao.HasValue == devolvido
                       orderby a.AluguelId
                       select new { a.AluguelId, Cliente = c.Nome, a.VeiculoId, a.DataRetirada, a.DataDevolucao, a.ValorTotal };
        return Ok(await consulta.ToListAsync());
    }

    // LEFT JOIN explícito: preserva fabricantes sem veículos (VeiculoId/Modelo nulos).
    // O filtro por nome permite pesquisar parte do nome do fabricante.
    [HttpGet("fabricantes-com-veiculos")]
    public async Task<IActionResult> FabricantesComVeiculos(string? nome)
    {
        nome = nome?.Trim();
        var consulta = from f in _context.Fabricantes
                       join v in _context.Veiculos on f.FabricanteId equals v.FabricanteId into veiculos
                       from v in veiculos.DefaultIfEmpty()
                       where string.IsNullOrEmpty(nome) || f.Nome.Contains(nome)
                       orderby f.FabricanteId, v.VeiculoId
                       select new { f.FabricanteId, Fabricante = f.Nome, VeiculoId = (int?)v.VeiculoId, Modelo = v == null ? null : v.Modelo };
        return Ok(await consulta.ToListAsync());
    }
}
