using AnimalReviewApp.Dto;
using AnimalReviewApp.Models;
using AutoMapper;

namespace AnimalReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Animal, AnimalDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<AnimalDto, Animal>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
