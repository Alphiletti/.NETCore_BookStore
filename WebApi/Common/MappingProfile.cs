using AutoMapper;
using WebApi.BookOPerations.GetBooks;
using static WebApi.BookOPerations.CreateBooks.CreateBookCommand;
using static WebApi.BookOPerations.GetBooksByID.GetBooksByIDQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookIDViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
        }


    }
}