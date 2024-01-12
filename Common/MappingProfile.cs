

using AutoMapper;
using PatikaBookStore.AuthorOperations.CreateAuthor;
using PatikaBookStore.AuthorOperations.GetAuthorDetail;
using PatikaBookStore.AuthorOperations.GetAuthors;
using PatikaBookStore.AuthorOperations.UpdateAuthor;
using PatikaBookStore.BookOperations;
using PatikaBookStore.BookOperations.GetBookDetail;
using PatikaBookStore.GenreOperations;
using PatikaBookStore.Models;

namespace PatikaBookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommand.CreateBookModel, Book>(); //CreateBookModel objesi, book objesine maplenebilir olsun

            CreateMap<Book, BookDetailViewModel>()
                .ForMember(
                    dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)); //Enum eşleştirme ayarı

            CreateMap<Book, BooksViewModel>()
                .ForMember(
                    dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)); //Enum eşleştirme ayarı

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Author, GetAuthorsQuery.AuthorsViewModel>();
            CreateMap<Author, GetAuthorDetailQuery.AuthorDetailViewModel>();
            CreateMap<CreateAuthorCommand.CreateAuthorViewModel, Author>();
            CreateMap<UpdateAuthorCommand.UpdateAuthorViewModel, Author>();
            CreateMap< Author,UpdateAuthorCommand.UpdateAuthorViewModel>();


        }
    }
}