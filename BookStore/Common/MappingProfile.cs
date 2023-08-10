using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.BookOperations.CreateBook;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.GenreOperations.GetGenreDetail;
using BookStore.Application.GenreOperations.GetGenres;
using BookStore.Application.UserOperations.Commands.Create;
using BookStore.Entities;
namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
			CreateMap<CreateBookCommand.CreateBookViewModel, Book>();
			CreateMap<UpdateBookViewModel, Book>();
			
			CreateMap<Genre, GenresViewModel>();
			CreateMap<Genre, GenreDetailViewModel>();
			CreateMap<CreateGenreViewModel, Genre>();
			CreateMap<UpdateGenreViewModel, Genre>();
			
			CreateMap<Author, AuthorsViewModel>();
			CreateMap<Author, AuthorDetailViewModel>();
			CreateMap<CreateAuthorViewModel, Author>();
			CreateMap<UpdateAuthorViewModel, Author>();
			
			CreateMap<CreateUserViewModel, User>();
            
        }

    }

    internal class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    internal class Author
    {
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
    }

    internal class UpdateGenreViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }

    internal class CreateGenreViewModel
    {
        public string Name { get; set; }
    }

    internal class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}