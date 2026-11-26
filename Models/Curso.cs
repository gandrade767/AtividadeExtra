namespace AtividadesApi.Models;

public class Curso : AtividadeFormativa, IInscrevivel
{
    public string Professor { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public int Vagas { get; set; }

    public override int CalcularDuracao()
    {
        return CargaHoraria;
    }

    public override string Descricao()
    {
        return $"Curso: {Titulo} - Professor: {Professor}";
    }

    public string Inscrever()
    {
        return $"A inscrição no curso '{Titulo}' está disponível. Vagas: {Vagas}.";
    }
}
