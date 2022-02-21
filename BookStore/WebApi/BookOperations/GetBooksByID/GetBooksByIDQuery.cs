using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOPerations.GetBooksByID
{
    public class GetBooksByIDQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public GetBooksByIDQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookIDViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if(book is null){
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            BookIDViewModel vm = new BookIDViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)book.GenreID).ToString();
            return vm;
        }

        public class BookIDViewModel{
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}