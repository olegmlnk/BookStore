using System.Linq.Expressions;
using BookStore.Application.Query.Filters;
using BookStore.Application.Query.Paging;
using BookStore.Application.Query.Sorting;
using BookStore.Contracts;
using BookStore.Core.Models;

namespace BookStore.Application.Extensions;

public static class BookExtension
{
    public static GetDetailedBookInfoDto ToGetDetailedBookInfoDto(this Book book)
    {
        return new GetDetailedBookInfoDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Price = book.Price
        };
    }
    public static IQueryable<Book> Filter(this IQueryable<Book> query, BookFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(p => p.Title.ToLower().Contains(filter.Title.ToLower()));

        if (filter.MinPrice.HasValue)
            query = query.Where(b => b.Price >= filter.MinPrice);
        
        if(filter.MaxPrice.HasValue)
            query = query.Where(b => b.Price <= filter.MaxPrice);

        return query;
    }

    public static IQueryable<Book> Sort(this IQueryable<Book> query, SortParams sortParams)
    {
        return sortParams.SortDirection == SortDirection.Descending
            ? query.OrderByDescending(GetKeySelector(sortParams.OrderBy))
            : query.OrderBy(GetKeySelector(sortParams.OrderBy));
    }

    private static Expression<Func<Book, object>> GetKeySelector(string? orderBy)
    {
        if(string.IsNullOrEmpty(orderBy))
            return x => x.Title;

        return orderBy switch
        {
            nameof(Book.Description) => x => x.Description,
            nameof(Book.Price) => x => x.Price,
            _ => x => x.Title
        };
    }

    public static IQueryable<Book> Page(this IQueryable<Book> query, PageParams pageParams)
    {
        var page = pageParams.Page ?? 1;
        var pageSize = pageParams.PageSize ?? 10;

        var skip = (page - 1) * pageSize;
        return query.Skip(skip).Take(pageSize);
    }
}