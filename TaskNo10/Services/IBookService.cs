using TaskNo10.Models;

namespace TaskNo10.Services{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task<Book> Create(Book book);
        Task<bool> Update(Book book);
        Task<bool> Delete(int id);
    }

}