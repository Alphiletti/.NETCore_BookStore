using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.BookOPerations.GetBooks;
using WebApi.BookOPerations.CreateBooks;
using static WebApi.BookOPerations.CreateBooks.CreateBookCommand;
using WebApi.BookOPerations.GetBooksByID;
using static WebApi.BookOPerations.GetBooksByID.GetBooksByIDQuery;
using WebApi.BookOPerations.UpdateBook;
using static WebApi.BookOPerations.UpdateBook.UpdateBookCommand;
using WebApi.BookOPerations.DeleteBook;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; //readonly sadece constructor içinde set edilebilir.

        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id = 1,
        //         Title = "Lean Startup",
        //         GenreID = 1, // Personal Growth
        //         PageCount = 200,
        //         PublishDate = new DateTime(2001,06,12)
        //     },

        //     new Book{
        //         Id = 2,
        //         Title = "Herland",
        //         GenreID = 2, // Personal Growth
        //         PageCount = 200,
        //         PublishDate = new DateTime(2010,05,23)
        // },
        //     new Book{
        //         Id = 3,
        //         Title = "Dune",
        //         GenreID = 3, // Personal Growth
        //         PageCount = 200,
        //         PublishDate = new DateTime(2001,12,21)
        //     }
        // };



        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookIDViewModel result;
            try
            {
                GetBooksByIDQuery queryID = new GetBooksByIDQuery(_context, _mapper);
                queryID.BookId = id;
                result = queryID.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        //Post - Ekleme
        [HttpPost]

        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                ValidationResult result = validator.Validate(command);
                
                validator.ValidateAndThrow(command); 
                command.Handle();
                // if (result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         Console.WriteLine("Özellik" + item.PropertyName + "- Error Message" + item.ErrorMessage);
                //     }
                // }
                // else
                // {
                //     command.Handle();
                // }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }
        //Put - Güncelleme

        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
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
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }

    internal struct NewStruct
    {
        public object Item1;
        public object Item2;

        public NewStruct(object ıtem1, object ıtem2)
        {
            Item1 = ıtem1;
            Item2 = ıtem2;
        }

        public override bool Equals(object obj)
        {
            return obj is NewStruct other &&
                   EqualityComparer<object>.Default.Equals(Item1, other.Item1) &&
                   EqualityComparer<object>.Default.Equals(Item2, other.Item2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2);
        }

        public void Deconstruct(out object ıtem1, out object ıtem2)
        {
            ıtem1 = Item1;
            ıtem2 = Item2;
        }

        public static implicit operator (object, object)(NewStruct value)
        {
            return (value.Item1, value.Item2);
        }

        public static implicit operator NewStruct((object, object) value)
        {
            return new NewStruct(value.Item1, value.Item2);
        }
    }
}