using AutoMapper;
using DTOLayer;
using EntityLayer.Concrete;
using System;

namespace NurgulsPortfolio.Mapping.AutoMapperProfile
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<AboutMeAddEditDTO, AboutMe>().ReverseMap();
            CreateMap<AdminCredentialsDto, AppUser>().ReverseMap();
            CreateMap<CvFileAddEditDTO, CvFile>().ReverseMap();
            CreateMap<InterestAddEditDTO, Interest>().ReverseMap();
            CreateMap<ProjectAddEditDTO, Project>().ReverseMap();
            CreateMap<ProjectImageAddEditDTO, ProjectImage>().ReverseMap();
            CreateMap<ServiceAddEditDTO, Service>().ReverseMap();
        }
    }
}
