using BookStore.Core.Models;

namespace BookStore.DataAccess.Interfaces
{
    public interface IBooksRepository
    {
        Task<Book> Create(Book book);
        Task<Guid> Delete(Guid id);
        IQueryable<Book> GetQueryable();
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}