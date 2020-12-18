using ControleVendas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleVendas.Repository
{
    public interface IRepositoryCRUD
    {
        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetAsync(int? id);

        public Task<Product> AddAsync(Product product);

        public Task<Product> UpdateAsync(int id, Product product);

        public Task<Product> DeleteAsync(int? id);
    }
}
