using BookStore.Contracts;
using BookStore.Core.Models;

namespace BookStore.Application.Query.Paging;

public class PagedResult
{
    public IEnumerable<GetBookDto> Books { get; set; }
    public int Total { get; set; }
}