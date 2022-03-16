using Domain.DTO.Requests;

namespace Domain.DTO.Responses;

public class ResultPaginated<T> : Result<IList<T>>
{
    public int TotalCount { get; set; }
    public int CountPerPage { get; set; }
    public int Page { get; set; }

    public ResultPaginated()
    {

    }

    public ResultPaginated(PaginatedRequest paginated)
    {
        Page = paginated.Page;
        CountPerPage = paginated.CountPerPage;
    }
}