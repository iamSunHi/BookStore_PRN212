using AutoMapper;
using BulkyBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUserVM, ApplicationUser>().ReverseMap();
            CreateMap<CompanyVM, Company>().ReverseMap();
            CreateMap<CategoryVM, Category>().ReverseMap();
        }
    }
}
