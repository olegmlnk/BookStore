using BookStore.Application.Interfaces;
using BookStore.Application.Query.Filters;
using BookStore.Application.Query.Paging;
using BookStore.Application.Query.Sorting;
using BookStore.Contracts;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("get-books")]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks(
            [FromQuery] BookFilter? filter,
            [FromQuery] SortParams? sortParams,
            [FromQuery] PageParams? pageParams)
        {
            var books = await _booksService.GetAllBooks(filter, sortParams, pageParams);
            
            return Ok(books);
        }

        [HttpPost("create-book")]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] CreateBookDto request)
        {
            var bookId = await _booksService.CreateBook(request);

            return Ok(bookId);
        }

        [HttpPut("update-book/{id}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
        {
             var bookId = await _booksService.UpdateBook(id, request.Title, request.Description, request.Price);
        
            return Ok(bookId);
        }

        [HttpDelete("delete-book/{id}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _booksService.DeleteBook(id));
        }
    }
}
