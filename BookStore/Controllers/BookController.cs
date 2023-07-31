using Microsoft.AspNetCore.Mvc;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.GetByIdQuery;
using BookStore.BookOperations.PutBookCommand;
using BookStore.DBOperations;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.PutBookCommand.PutBookCommand;

namespace BookStore.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController (BookStoreDbContext context)
        { 
            _context = context;
        }
        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }   

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookOperations.GetByIdQuery.BooksViewModel result;
            GetByIdQuery query = new GetByIdQuery(_context,id);
            result=query.Handle();
            return Ok(result);
        }  

  

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel model)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model=model;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

      

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] PutCommandModel updatedBook)
        {
            PutBookCommand command = new PutBookCommand(_context);
            try
            {
                command.Model=updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book =  _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
             _context.Books.Remove(book);
             _context.SaveChanges();
            return Ok();
        }
    }
}