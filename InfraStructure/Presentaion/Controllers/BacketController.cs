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

    public class BacketController(IServiceManger serviceManger):ApiController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<BacketDto>> GetBacket(string id)
        {
            var backet=await serviceManger.backetService.GetBacketAsync(id);
            return Ok(backet);
        }

        [HttpPost]
        public async Task<ActionResult<BacketDto>> UpdateBacket(BacketDto backetDto)
        {
            var backet = await serviceManger.backetService.CreateOrUpdateBacketAsync(backetDto);
            return Ok(backet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBacket(string id)
        {
            var backet = await serviceManger.backetService.DeleteBacketAsync(id);
            return NoContent();
        }
    }
}
