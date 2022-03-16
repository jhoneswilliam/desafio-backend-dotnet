namespace Domain.DTO.Requests;

public class PaginatedRequest
{
    public int CountPerPage { get; set; } = 10;
    public int Page { get; set; } = 1;
}