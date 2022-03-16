namespace Domain.Models;

public class Cidade
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? UF { get; set; }
    public IList<Pessoa>? Pessoas { get; set; }
}