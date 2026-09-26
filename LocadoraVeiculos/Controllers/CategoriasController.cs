using LocadoraVeiculos.Data;
using LocadoraVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ApplicationContext _context;
    public CategoriasController(ApplicationContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetTodos() =>
        Ok(await _context.Categorias.AsNoTracking().OrderBy(x => x.CategoriaId).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Categoria>> GetPorId(int id)
    {
        var item = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.CategoriaId == id);
        if (item == null) return NotFound(new { mensagem = "Registro não encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Post(Categoria item)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        if (item.CategoriaId != 0) return BadRequest(new { mensagem = "O ID é gerado pelo banco; envie zero ou omita o campo." });
        

        _context.Categorias.Add(item);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627 || sql.Number == 547))
        {
            return Conflict(new { mensagem = "Não foi possível gravar: registro duplicado ou relacionamento inválido." });
        }
        return CreatedAtAction(nameof(GetPorId), new { id = item.CategoriaId }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Categoria item)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        if (id != item.CategoriaId) return BadRequest(new { mensagem = "O ID da URL deve ser igual ao ID do corpo." });
        var atual = await _context.Categorias.FindAsync(id);
        if (atual == null) return NotFound(new { mensagem = "Registro não encontrado." });
        

        _context.Entry(atual).CurrentValues.SetValues(item);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new { mensagem = "O registro foi removido durante a atualização. Consulte novamente." });
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627 || sql.Number == 547))
        {
            return Conflict(new { mensagem = "Não foi possível atualizar: registro duplicado ou relacionamento inválido." });
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Categorias.FindAsync(id);
        if (item == null) return NotFound(new { mensagem = "Registro não encontrado." });
        if (await _context.Veiculos.AnyAsync(v => v.CategoriaId == id))
            return Conflict(new { mensagem = "O registro possui vínculos e não pode ser excluído." });
        _context.Categorias.Remove(item);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new { mensagem = "O registro foi removido por outra operação. Consulte novamente." });
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && sql.Number == 547)
        {
            return Conflict(new { mensagem = "O registro possui vínculos e não pode ser excluído." });
        }
        return NoContent();
    }
}