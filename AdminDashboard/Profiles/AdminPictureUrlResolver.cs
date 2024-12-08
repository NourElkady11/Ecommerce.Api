using AdminDashboard.Models.ViewModels;
using AutoMapper;
using Domain.Entities;
using Shared;

namespace AdminDashboard.Profiles
{
	public class AdminPictureUrlResolver(IConfiguration configuration): IValueResolver<Product, ProductViewModel, string>
	{
		public string Resolve(Product source, ProductViewModel destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrWhiteSpace(source.pictureUrl))
			{
				return string.Empty;
			}
			else
			{
				return $"{configuration["BaseUrl"].Replace("api/", "")}/{source.pictureUrl}/";

			}

		}
	}
}
