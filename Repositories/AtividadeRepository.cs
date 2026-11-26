using AtividadesApi.Models;

namespace AtividadesApi.Repositories;

public class AtividadeRepository : IAtividadeRepository
{
    private readonly Dictionary<int, AtividadeFormativa> _atividades = new();

    public AtividadeRepository()
    {
        Adicionar(new Curso
        {
            Titulo = "JavaScript para Interfaces",
            Ano = 2026,
            Professor = "Equipe de Desenvolvimento",
            CargaHoraria = 40,
            Vagas = 30
        });

        Adicionar(new Workshop
        {
            Titulo = "Boas práticas de Git",
            Ano = 2026,
            Instrutor = "Laboratório de Software",
            Local = "Sala 08",
            DuracaoHoras = 4
        });
    }

    public IEnumerable<AtividadeFormativa> ObterTodos()
    {
        return _atividades.Values;
    }

    public AtividadeFormativa? ObterPorId(int id)
    {
        return _atividades.TryGetValue(id, out var atividade) ? atividade : null;
    }

    public AtividadeFormativa Adicionar(AtividadeFormativa atividade)
    {
        atividade.Id = GerarNovoId();
        _atividades.Add(atividade.Id, atividade);
        return atividade;
    }

    public bool Atualizar(int id, AtividadeFormativa atividade)
    {
        if (!_atividades.ContainsKey(id))
        {
            return false;
        }

        atividade.Id = id;
        _atividades[id] = atividade;
        return true;
    }

    public bool Remover(int id)
    {
        return _atividades.Remove(id);
    }

    private int GerarNovoId()
    {
        return _atividades.Count == 0 ? 1 : _atividades.Keys.Max() + 1;
    }
}
