using AutoMapper;
using Library.Data.Models;
using Library.Models.Books;

namespace Library.Mapping
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile() 
        {
            this.CreateMap<Book, BookViewModel>()
                .ForMember(d => d.Category, src => src.MapFrom(opt => opt.Category.Name));

            this.CreateMap<Book, MyBookViewModel>()
              .ForMember(d => d.Category, src => src.MapFrom(opt => opt.Category.Name));

            this.CreateMap<AddBookViewModel, Book>()
              .ForMember(d => d.ImageUrl, src => src.MapFrom(opt => opt.Url))
              .ForMember(d => d.CategoryId, src => src.MapFrom(opt => opt.CategoryId))
              .ForSourceMember(src => src.Rating, opt => opt.DoNotValidate())
              .ForSourceMember(src => src.Categories, opt => opt.DoNotValidate());
        }
    }
}
