using FinaFlow.Shared.Models;

namespace FinaFlow.Shared.Requests;

public class PagedRequest : Request
{
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
}