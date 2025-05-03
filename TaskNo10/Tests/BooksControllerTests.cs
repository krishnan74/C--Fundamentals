using Xunit;
using TaskNo10.Controllers;
using TaskNo10.Services;
using TaskNo10.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskNo10.Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookService> _mockService;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mockService = new Mock<IBookService>();
            _controller = new BooksController(_mockService.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
          
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }
            };
            _mockService.Setup(x => x.GetAll())
                .ReturnsAsync(books);
  
            var result = await _controller.GetBooks();
 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetBook_WithValidId_ReturnsOkResult()
        {
          
            var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author" };
            _mockService.Setup(x => x.GetById(1))
                .ReturnsAsync(book);
            
            var result = await _controller.GetBook(1);
        
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetBook_WithInvalidId_ReturnsNotFound()
        {
          
            _mockService.Setup(x => x.GetById(1))
                .ReturnsAsync((Book)null);
            
            var result = await _controller.GetBook(1);
          
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateBook_WithValidBook_ReturnsCreatedResult()
        {
          
            var book = new Book { Title = "New Book", Author = "New Author" };
            var createdBook = new Book { Id = 1, Title = "New Book", Author = "New Author" };
            _mockService.Setup(x => x.Create(book))
                .ReturnsAsync(createdBook);
            
            var result = await _controller.CreateBook(book);
           
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Book>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task UpdateBook_WithValidBook_ReturnsNoContent()
        {
          
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author" };
            _mockService.Setup(x => x.Update(book))
                .ReturnsAsync(true);

            var result = await _controller.UpdateBook(1, book);

           
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBook_WithInvalidId_ReturnsBadRequest()
        {
          
            var book = new Book { Id = 2, Title = "Updated Book", Author = "Updated Author" };

            var result = await _controller.UpdateBook(1, book);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateBook_WithNonExistentBook_ReturnsNotFound()
        {
          
            var book = new Book { Id = 1, Title = "Updated Book", Author = "Updated Author" };
            _mockService.Setup(x => x.Update(book))
                .ReturnsAsync(false);
            
            var result = await _controller.UpdateBook(1, book);
           
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteBook_WithValidId_ReturnsNoContent()
        {
          
            _mockService.Setup(x => x.Delete(1))
                .ReturnsAsync(true);
            
            var result = await _controller.DeleteBook(1);
          
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_WithInvalidId_ReturnsNotFound()
        {          
            _mockService.Setup(x => x.Delete(1))
                .ReturnsAsync(false);
           
            var result = await _controller.DeleteBook(1);
           
            Assert.IsType<NotFoundResult>(result);
        }
    }
} 