using BookStore.Core.Models;
using BookStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _context;
        public BooksRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Book> GetQueryable() => _context.Books;


        public async Task<Book> Create(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Price, b => price));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
