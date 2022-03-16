namespace Domain.DTO.Responses;

public class PessoaResponse
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public int Idade { get; set; }
    public CidadeResponse? Cidade { get; set; }
}