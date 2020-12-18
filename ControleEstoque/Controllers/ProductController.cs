using ControleEstoque.Models;
using ControleEstoque.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryService _repository;

        public ProductController(IRepositoryService repository)
        {
            _repository = repository;
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

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product item)
        {
            return await _repository.AddAsync(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product item)
        {
            return await _repository.UpdateAsync(id, item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
