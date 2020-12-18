using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Service
{
    public interface IRepositoryService
    {
        public Task<ActionResult<IEnumerable<Product>>> GetAllAsync();

        public Task<ActionResult<Product>> GetAsync(int? id);

        public Task<ActionResult<Product>> AddAsync([Bind("Id,Codigo,Nome,Preco,Quantidade")] Product product);

        public Task<ActionResult<Product>> UpdateAsync(int id, [Bind("Id,Codigo,Nome,Preco,Quantidade")] Product product);

        public Task<ActionResult<Product>> DeleteAsync(int? id);
    }
}
