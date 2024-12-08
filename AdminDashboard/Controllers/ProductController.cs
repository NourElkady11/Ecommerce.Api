using AdminDashboard.DocumentService;
using AdminDashboard.Models.ViewModels;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Specfications;
using Shared;

namespace AdminDashboard.Controllers
{
    public class ProductController(IUnitOfWork unitOfWork, IMapper mapper, IDocummentService docummentService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var productSpecificationsParamters = new ProductSpecificationsParamters() { PageSize = int.MaxValue };
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWhithBrandAndTypeSpecfications(productSpecificationsParamters));
            var mappedProd = mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(mappedProd);
        }



        public async Task<IActionResult> Create(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsyncByid(id);

            var mappedProduct = mapper.Map<ProductViewModel>(product);

            return View(mappedProduct);
        }
   





        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Picture != null)
                {
                    productViewModel.pictureUrl = await docummentService.uploadFile(productViewModel.Picture, "products");
                    await AdminDocummentSettings.uploadFile(productViewModel.Picture, "products");
                }

                    var mappedProduct = mapper.Map<Product>(productViewModel);


                /*    var mappedProduct = new Product()
                    {
                        Id = productViewModel.Id,
                        name = productViewModel.name,
                        description = productViewModel.description,
                        brandId = productViewModel.BrandId,
                        typeId = productViewModel.TypeId,
                        pictureUrl = productViewModel.pictureUrl,
                        price = productViewModel.Price,


                    };*/


                await unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "There is an error happend on the creation of the product");
                return View(productViewModel);
            }


        }



    }
}
