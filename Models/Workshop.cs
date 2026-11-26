namespace AtividadesApi.Models;

public class Workshop : AtividadeFormativa
{
    public string Instrutor { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public int DuracaoHoras { get; set; }

    public override int CalcularDuracao()
    {
        return DuracaoHoras;
    }

    public override string Descricao()
    {
        return $"Workshop: {Titulo} - Instrutor: {Instrutor} - Local: {Local}";
    }
}
