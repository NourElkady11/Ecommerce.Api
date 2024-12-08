using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.pictureUrl))
            {
                return string.Empty;
            }
            else
            {
                return $"{configuration["BaseUrl"].Replace("api/","")}{source.pictureUrl}";

            }

        }
    }
}
