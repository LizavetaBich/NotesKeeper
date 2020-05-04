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

            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.AccessToken, options => options.MapFrom(src => src.Token))
                .ForMember(dest => dest.Role, options => options.MapFrom(src => src.Role));

            CreateMap<ApplicationUserViewModel, ApplicationUser>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Token, options => options.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.Role, options => options.MapFrom(src => src.Role));

            CreateMap<ApplicationUser, LoginViewModel>()
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, options => options.MapFrom(src => src.Password));

            CreateMap<RefreshToken, RefreshTokenViewModel>()
                .ForMember(dest => dest.Token, options => options.MapFrom(src => src.Token))
                .ForMember(dest => dest.Expiration, options => options.MapFrom(src => src.ExpirationTime));

            CreateMap<RefreshTokenViewModel, RefreshToken>()
                .ForMember(dest => dest.Token, options => options.MapFrom(src => src.Token))
                .ForMember(dest => dest.ExpirationTime, options => options.MapFrom(src => src.Expiration));

            CreateMap<RefreshAccessTokenViewModel, RefreshAccessTokenModel>();
            CreateMap<RefreshAccessTokenModel, RefreshAccessTokenViewModel>();
        }
    }
}
