using Microsoft.AspNetCore.Mvc;
using TaskNo10.Models;
using TaskNo10.Services;

namespace TaskNo10.Controllers{
[Route("api/[controller]")]
[ApiController]


    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks() => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _service.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            var created = await _service.Create(book);
            return CreatedAtAction(nameof(GetBook), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest();
            return await _service.Update(book) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return await _service.Delete(id) ? NoContent() : NotFound();
        }
    }
}