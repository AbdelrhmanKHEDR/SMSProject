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
            CreateMap<ApplicationUser , UserViewModel>();
        
        
        }
    }
}
