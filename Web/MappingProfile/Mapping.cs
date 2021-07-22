using AutoMapper;
using BL.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModel;

namespace Web.MappingProfile
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Project, ProjectViewModel>().
            ReverseMap()
           .ForMember(dest => dest.ImageProject, src => src.MapFrom(src => src.File.FileName));                
        }

    }
}
