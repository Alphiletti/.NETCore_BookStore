using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOPerations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public int BookId { get; set; }

        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı.");
            }

            book.GenreID = Model.GenreID != default ? Model.GenreID : book.GenreID;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreID { get; set; }
        }
    }
}
