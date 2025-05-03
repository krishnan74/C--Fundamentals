using TaskNo10.Models;
using TaskNo10.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskNo10.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll() => await _context.Books.ToListAsync();

        public async Task<Book?> GetById(int id) => await _context.Books.FindAsync(id);

        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Update(Book book)
        {
            if (!_context.Books.Any(b => b.Id == book.Id)) return false;
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
