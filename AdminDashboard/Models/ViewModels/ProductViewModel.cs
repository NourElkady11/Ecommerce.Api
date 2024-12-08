using Domain.Entities;

namespace AdminDashboard.Models.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }

        public string? name { get; set; }
        public string? description { get; set; }
        public string? pictureUrl { get; set; }
        public decimal price { get; set; }

        public IFormFile? Picture { get; set; }

        /*	public string? brandName { get; set; }*/
        public ProductBrand? ProductBrand { get; set; }

        public int brandId { get; set; }

        /*	public string? typeName { get; set; }*/
        public ProductType? productType { get; set; }

        public int typeId { get; set; }



    }
}
