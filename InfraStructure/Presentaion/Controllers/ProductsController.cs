using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManger serviceManger):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProdcts()
        {
            var products = await serviceManger.productService.GetAllProductsAsync();
            return Ok(products);
        } 
        
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllBrands()
        {
            var products = await serviceManger.productService.GetAllBrandsAsync();
            return Ok(products);
        } 
        
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllTypess()
        {
            var products = await serviceManger.productService.GetAllTypesAsync();
            return Ok(products);
        }  
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var products = await serviceManger.productService.GetProductById(id);
            return Ok(products);
        }
    }
}
