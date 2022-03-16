namespace Domain.DTO.Requests;

public class UpdatePessoaRequest
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public int Idade { get; set; }
    public int IdCidade { get; set; }
}