using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Repository
{
    public interface IRepositoryCRUD
    {
        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetAsync(int? id);

        public Task<ActionResult<Product>> AddAsync(Product product);

        public Task<ActionResult<Product>> UpdateAsync(int id, Product product);

        public Task<Product> DeleteAsync(int? id);
    }
}
