using AtividadesApi.Models;

namespace AtividadesApi.Repositories;

public interface IAtividadeRepository
{
    IEnumerable<AtividadeFormativa> ObterTodos();
    AtividadeFormativa? ObterPorId(int id);
    AtividadeFormativa Adicionar(AtividadeFormativa atividade);
    bool Atualizar(int id, AtividadeFormativa atividade);
    bool Remover(int id);
}
