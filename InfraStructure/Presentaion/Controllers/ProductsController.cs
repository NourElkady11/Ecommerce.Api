using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [Authorize(AuthenticationSchemes ="Bearer",Roles ="Admin")]
    public class productsController(IServiceManger serviceManger):ApiController
    {
        [RedisCach]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProdcts([FromQuery] ProductSpecificationsParamters productSpecificationsParamters)
        {
            var products = await serviceManger.productService.GetAllProductsAsync(productSpecificationsParamters);
            return Ok(products);
        } 
        
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllBrands()
        {
            var products = await serviceManger.productService.GetAllBrandsAsync();
            return Ok(products);
        } 
        
        [HttpGet("types")]
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
