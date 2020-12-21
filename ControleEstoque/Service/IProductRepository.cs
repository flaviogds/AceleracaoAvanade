using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Service
{
    public interface IProductRepository
    {
        public Task<ActionResult<IEnumerable<Product>>> GetAllAsync();

        public Task<ActionResult<Product>> GetAsync(int? id);

        public Task<ActionResult<Product>> AddAsync(Product product);

        public Task<ActionResult<Product>> UpdateAsync(int id, Product product);

        public Task<ActionResult<Product>> DeleteAsync(int? id);
    }
}
