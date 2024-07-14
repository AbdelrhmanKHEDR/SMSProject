using AutoMapper;
using SMSProject.Models;
using SMSProject.ViewModels;

namespace SMSProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            //Users
            CreateMap<ApplicationUser , UserViewModel>().ReverseMap(); ;
            CreateMap<UserFormViewModel, ApplicationUser>()
               .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
				.ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Username.ToUpper()))
				.ReverseMap();
			CreateMap<UserViewModel, UserFormViewModel>().ReverseMap(); ;
        
        
        }
    }
}
