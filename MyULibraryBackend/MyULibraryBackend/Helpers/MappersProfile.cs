using AutoMapper;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;

namespace MyULibraryBackend.Helpers
{
    public class MappersProfile : Profile
    {
        public MappersProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<BookLog, BookLogDto>()
                .ForMember(d =>
                d.BookTitle,
                s => s.MapFrom(e => e.Book.Title))
                 .ForMember(d =>
                d.FirstName,
                s => s.MapFrom(e => e.User.FirstName))
                   .ForMember(d =>
                d.LastName,
                s => s.MapFrom(e => e.User.LastName))
                      .ForMember(d =>
                d.Email,
                s => s.MapFrom(e => e.User.Email));

            CreateMap<BookLogDto, BookLog>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDto>();
        }
    }
}
