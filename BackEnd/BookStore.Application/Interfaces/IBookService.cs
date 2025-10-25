using BookStore.Application.Query.Filters;
using BookStore.Application.Query.Paging;
using BookStore.Application.Query.Sorting;
using BookStore.Contracts;
using BookStore.Core.Models;

namespace BookStore.Application.Interfaces
{
    public interface IBooksService
    {
        Task<GetDetailedBookInfoDto> CreateBook(CreateBookDto createBookDto);
        Task<Guid> DeleteBook(Guid id);

        Task<PagedResult> GetAllBooks(
            BookFilter? bookFilter,
            SortParams? sortParams,
            PageParams? pageParams);
        Task<Guid> UpdateBook(Guid id, string title, string description, decimal price);
    }
}