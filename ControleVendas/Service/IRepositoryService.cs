using ControleVendas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleVendas.Service
{
    public interface IRepositoryService
    {
        public Task<ActionResult<IEnumerable<Product>>> GetAllAsync();

        public Task<ActionResult<Product>> GetAsync(int? id);

        public Task<Product> AddAsync(Product product);

        public Task<Product> UpdateAsync(int id, Product product);

        public Task<Product> DeleteAsync(int? id);
    }
}
