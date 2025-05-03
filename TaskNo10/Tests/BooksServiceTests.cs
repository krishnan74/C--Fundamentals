using Xunit;
using TaskNo10.Services;
using TaskNo10.Models;
using TaskNo10.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskNo10.Tests
{
    public class BookServiceTests
    {
        private readonly AppDbContext _context;
        private readonly BookService _service;

        public BookServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BookDb")
                .Options;

            _context = new AppDbContext(options);
            _service = new BookService(_context);
        }

        [Fact]
        public async Task ShouldAddBookToDatabase()
        {
            var newBook = new Book { Title = "New Book", Author = "New Author" };

            var createdBook = await _service.Create(newBook);

            Assert.NotNull(createdBook);
            Assert.Equal("New Book", createdBook.Title);
            Assert.Equal("New Author", createdBook.Author);
        }

        [Fact]
        public async Task ShouldReturnListOfBooks()
        {
            var books = await _service.GetAll();
            Assert.NotNull(books);
            Assert.True(books.Any());
        }

        [Fact]
        public async Task ShouldReturnBookById()
        {
            var newBook = new Book { Title = "New Book", Author = "New Author" };
            await _service.Create(newBook);

            var book = await _service.GetById(newBook.Id);

            Assert.NotNull(book);
            Assert.Equal("New Book", book.Title);
        }

        [Fact]
        public async Task ShouldUpdateBook()
        {
            var newBook = new Book { Title = "New Book", Author = "New Author" };
            await _service.Create(newBook);

            newBook.Title = "Updated Book";
            var result = await _service.Update(newBook);

            Assert.True(result);
            var updatedBook = await _service.GetById(newBook.Id);
            Assert.Equal("Updated Book", updatedBook.Title);
        }

        [Fact]
        public async Task ShouldDeleteBook()
        {
            var newBook = new Book { Title = "New Book", Author = "New Author" };
            await _service.Create(newBook);

            var result = await _service.Delete(newBook.Id);

            Assert.True(result);
            var deletedBook = await _service.GetById(newBook.Id);
            Assert.Null(deletedBook);
        }
    }
}
