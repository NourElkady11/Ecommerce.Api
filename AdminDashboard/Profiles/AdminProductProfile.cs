using AdminDashboard.Models.ViewModels;
using AdminDashboard.Profiles;
using AutoMapper;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class AdminProductProfile : Profile
    {
        public AdminProductProfile() {

            CreateMap<Product, ProductViewModel>().ForMember(s => s.pictureUrl, op => op.MapFrom<AdminPictureUrlResolver>()).ReverseMap();
       
           


        }
    }
}
