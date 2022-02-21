using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOPerations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get; set;}
        private readonly BookStoreDbContext _dbContext;

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");

            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreID = Model.GenreID;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreID { get; set; }
            public int PageCount { get; set; }

            public DateTime PublishDate { get; set; }

        }
    }
}