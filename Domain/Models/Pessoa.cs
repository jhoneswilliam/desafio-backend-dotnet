namespace Domain.Models;

public class Pessoa
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public int Idade { get; set; }
    public Cidade? Cidade { get; set; }
}