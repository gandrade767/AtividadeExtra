namespace AtividadesApi.Models;

public abstract class AtividadeFormativa
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int Ano { get; set; }

    public abstract int CalcularDuracao();

    public virtual string Descricao()
    {
        return $"Atividade: {Titulo}";
    }
}
