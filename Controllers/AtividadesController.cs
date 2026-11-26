using AtividadesApi.Models;
using AtividadesApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AtividadesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtividadesController : ControllerBase
{
    private readonly IAtividadeRepository _repository;

    public AtividadesController(IAtividadeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AtividadeFormativa>> ObterTodos()
    {
        return Ok(_repository.ObterTodos());
    }

    [HttpGet("{id:int}")]
    public ActionResult<AtividadeFormativa> ObterPorId(int id)
    {
        var atividade = _repository.ObterPorId(id);
        return atividade is null ? NotFound() : Ok(atividade);
    }

    [HttpPost("curso")]
    public ActionResult<Curso> CadastrarCurso(Curso curso)
    {
        var erro = ValidarCurso(curso);
        if (erro is not null)
        {
            return BadRequest(new { mensagem = erro });
        }

        curso.Id = 0;
        var cadastrado = (Curso)_repository.Adicionar(curso);
        return CreatedAtAction(nameof(ObterPorId), new { id = cadastrado.Id }, cadastrado);
    }

    [HttpPost("workshop")]
    public ActionResult<Workshop> CadastrarWorkshop(Workshop workshop)
    {
        var erro = ValidarWorkshop(workshop);
        if (erro is not null)
        {
            return BadRequest(new { mensagem = erro });
        }

        workshop.Id = 0;
        var cadastrado = (Workshop)_repository.Adicionar(workshop);
        return CreatedAtAction(nameof(ObterPorId), new { id = cadastrado.Id }, cadastrado);
    }

    [HttpPut("curso/{id:int}")]
    public IActionResult AtualizarCurso(int id, Curso curso)
    {
        var erro = ValidarCurso(curso);
        if (erro is not null)
        {
            return BadRequest(new { mensagem = erro });
        }

        return _repository.Atualizar(id, curso) ? Ok(curso) : NotFound();
    }

    [HttpPut("workshop/{id:int}")]
    public IActionResult AtualizarWorkshop(int id, Workshop workshop)
    {
        var erro = ValidarWorkshop(workshop);
        if (erro is not null)
        {
            return BadRequest(new { mensagem = erro });
        }

        return _repository.Atualizar(id, workshop) ? Ok(workshop) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Remover(int id)
    {
        return _repository.Remover(id) ? NoContent() : NotFound();
    }

    [HttpGet("{id:int}/descricao")]
    public IActionResult ObterDescricao(int id)
    {
        var atividade = _repository.ObterPorId(id);
        if (atividade is null)
        {
            return NotFound();
        }

        return Ok(new
        {
            atividade.Id,
            Tipo = atividade.GetType().Name,
            Descricao = atividade.Descricao(),
            Duracao = atividade.CalcularDuracao()
        });
    }

    [HttpPost("{id:int}/inscrever")]
    public IActionResult Inscrever(int id)
    {
        var atividade = _repository.ObterPorId(id);
        if (atividade is null)
        {
            return NotFound();
        }

        if (atividade is not IInscrevivel inscrevivel)
        {
            return BadRequest(new { mensagem = "Esta atividade não possui inscrição disponível." });
        }

        return Ok(new { mensagem = inscrevivel.Inscrever() });
    }

    private static string? ValidarAtividade(AtividadeFormativa atividade)
    {
        if (string.IsNullOrWhiteSpace(atividade.Titulo))
        {
            return "O título é obrigatório.";
        }

        return atividade.Ano < 0 ? "O ano não pode ser menor que zero." : null;
    }

    private static string? ValidarCurso(Curso curso)
    {
        var erro = ValidarAtividade(curso);
        if (erro is not null)
        {
            return erro;
        }

        if (string.IsNullOrWhiteSpace(curso.Professor))
        {
            return "O professor é obrigatório.";
        }

        if (curso.CargaHoraria <= 0)
        {
            return "A carga horária deve ser maior que zero.";
        }

        return curso.Vagas <= 0 ? "O número de vagas deve ser maior que zero." : null;
    }

    private static string? ValidarWorkshop(Workshop workshop)
    {
        var erro = ValidarAtividade(workshop);
        if (erro is not null)
        {
            return erro;
        }

        if (string.IsNullOrWhiteSpace(workshop.Instrutor))
        {
            return "O instrutor é obrigatório.";
        }

        if (string.IsNullOrWhiteSpace(workshop.Local))
        {
            return "O local é obrigatório.";
        }

        return workshop.DuracaoHoras <= 0 ? "A duração deve ser maior que zero." : null;
    }
}
