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
            var mappedProd = mapper.Map<IEnumerable<ProductDto>>(products);
            return View(mappedProd);
        }


        public async Task<IActionResult> Create() => View();
      

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Picture != null)
                {
                    productViewModel.pictureUrl = await docummentService.uploadFile(productViewModel.Picture, "products");
/*                    await AdminDocummentSettings.uploadFile(productViewModel.Picture, "products");
*/                }

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



        public async Task<IActionResult> Edit(int id)
        {
            var prod = await unitOfWork.GetRepository<Product, int>().GetAsyncByid(id);
            if (prod == null)
            {
                ModelState.AddModelError(string.Empty, "Error has been occured");
                return View();
            }
            else
            {
                var mappedprod=mapper.Map<ProductViewModel>(prod);
                return View(mappedprod);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return BadRequest();
            }

            if (productViewModel.Picture is not null)
            {
                productViewModel.pictureUrl = await docummentService.uploadFile(productViewModel.Picture, "products");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prod = mapper.Map<Product>(productViewModel);
                    unitOfWork.GetRepository<Product, int>().UpdateAsync(prod);
                    unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(productViewModel);
        }




        public async Task<IActionResult> Delete(int id)
        {
            var prod = await unitOfWork.GetRepository<Product, int>().GetAsyncByid(id);
            if (prod == null)
            {
                ModelState.AddModelError(string.Empty, "Error has been occured");
                return View();
            }
            else
            {
                var mappedprod = mapper.Map<ProductViewModel>(prod);
                return View(mappedprod);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id,ProductViewModel productViewModel)
        {
            if(id != productViewModel.Id)
            {
                return BadRequest();
            }

            if (productViewModel.Picture is not null)
            {
                await docummentService.DeleteFile(productViewModel.pictureUrl,"images");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prod = mapper.Map<Product>(productViewModel);
                    unitOfWork.GetRepository<Product, int>().DeleteAsync(prod);
                    unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(productViewModel);


        }







    }
}
