using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exeption;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BacketService(IBacketRepository backetRepository ,IMapper mapper) : IBacketService
    {
        public async Task<BasketDto?> CreateOrUpdateBacketAsync(BasketDto backetDto)
        {
            var customerbacket = mapper.Map<CustomerBacket>(backetDto);
            var updatedbacket = await backetRepository.CreateOrUpdateBacketAsync(customerbacket);
            return updatedbacket is null ? throw new Exception("Cant update Backet Now"):mapper.Map<BasketDto>(updatedbacket);

        }

        public async Task<bool> DeleteBacketAsync(string id)=>await backetRepository.DeleteBacketAsync(id);
   

        public async Task<BasketDto?> GetBacketAsync(string id)
        { 
            var backet=await backetRepository.GetcustomerBacketAsync(id);
           return backet is null ? throw new BacketNotFound(id) : mapper.Map<BasketDto>(backet);
        }
    }
}
