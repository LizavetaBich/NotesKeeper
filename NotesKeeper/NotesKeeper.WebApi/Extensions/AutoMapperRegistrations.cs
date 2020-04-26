using AutoMapper;
using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using NotesKeeper.WebApi.ViewModels;

namespace NotesKeeper.WebApi.Extensions
{
    public class AutoMapperRegistrations : Profile
    {
        public AutoMapperRegistrations()
        {
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<RegistrationViewModel, RegisterModel>();

            CreateMap<RegisterModel, ApplicationUser>()
                .ForMember(dest => dest.Email, options => options.MapFrom(source => source.Email))
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, options => options.MapFrom(src => src.Password));
        }
    }
}
