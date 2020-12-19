using ControleVendas.Models;
using ControleVendas.Service;
using ControleVendas.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleVendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryService _repository;
        private readonly IServiceBusController _serviceBus;

        public ProductController(IRepositoryService repository, IServiceBusController serviceBus)
        {
            _repository = repository;
            _serviceBus = serviceBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ProductSale> Put(int id, [FromBody] ProductSale item)
        {
            return await _serviceBus.Sales(id, item);
        }
    }
}
