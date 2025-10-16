using BookStore.Application.Abstractions;
using BookStore.DataAccess.Abstractions;
using BookStore.Core.Models;

namespace BookStore.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        public BooksService(IBooksRepository bookRepository)
        {
            _booksRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _booksRepository.GetAllAsync();
        }

        public Task<Guid> CreateBook(Book book)
        {
            return _booksRepository.Create(book);
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
