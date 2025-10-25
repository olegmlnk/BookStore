using System.Runtime.InteropServices.JavaScript;
using BookStore.Application.Extensions;
using BookStore.Application.Interfaces;
using BookStore.Application.Query.Filters;
using BookStore.Application.Query.Paging;
using BookStore.Application.Query.Sorting;
using BookStore.Contracts;
using BookStore.Core.Models;
using BookStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        public BooksService(IBooksRepository bookRepository)
        {
            _booksRepository = bookRepository;
        }

        public async Task<PagedResult> GetAllBooks(
            BookFilter? bookFilter,
            SortParams? sortParams,
            PageParams? pageParams)
        {
            var query = _booksRepository.GetQueryable();
            
            if(bookFilter != null)
                query = query.Filter(bookFilter);
            
            if (sortParams != null)
                 query = query.Sort(sortParams);
            
            var total = query.Count();
            
            if (pageParams != null)
                 query = query.Page(pageParams);

            var books = await query.Select(b => new GetBookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Price = b.Price
            }).ToListAsync();

            return new PagedResult
            {
                Books = books,
                Total = total
            };
            
        }

        public async Task<GetDetailedBookInfoDto> CreateBook(CreateBookDto createBookDto)
        {
            (Book? book, string error) = Book.Create(
                createBookDto.Title,
                createBookDto.Description,
                createBookDto.Price);

            var addedBook = await _booksRepository.Create(book);
            
            return addedBook.ToGetDetailedBookInfoDto();
        }

        public Task<Guid> UpdateBook(Guid id, string title, string description, decimal price)
        {
            return _booksRepository.Update(id, title, description, price);
        }
        
        public async Task<Guid> DeleteBook(Guid id)
        {
            return await _booksRepository.Delete(id);
        }
    }
}
