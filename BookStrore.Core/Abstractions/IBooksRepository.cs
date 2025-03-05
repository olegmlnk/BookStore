using BookStore.Core.Models;

namespace BookStore.DataAccess.Abstractions
{
    public interface IBooksRepository
    {
        Task<Guid> Create(Book book);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> GetAllAsync();
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}