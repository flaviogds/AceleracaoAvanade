using ControleVendas.Models;
using ControleVendas.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleVendas.Service
{
    public class ProductRepository : ControllerBase, IProductRepository
    {
        private readonly IRepositoryCRUD _repository;
        private readonly IUnityOfWork _repositoryChanges;

        public ProductRepository (
            IRepositoryCRUD repository,
            IUnityOfWork repositoryChanges)
        {
            _repository = repository;
            _repositoryChanges = repositoryChanges;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync() => Ok(await _repository.GetAllAsync());

        public async Task<ActionResult<Product>> GetAsync(int? id) => await _repository.GetAsync(id);

        public async Task<Product> AddAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _repositoryChanges.SubmitChangesAsync();

            return product;
        }

        public async Task<Product> DeleteAsync(int? id)
        {
            await _repository.DeleteAsync(id);
            await _repositoryChanges.SubmitChangesAsync();

            return null;
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            await _repository.UpdateAsync(id, product);
            await _repositoryChanges.SubmitChangesAsync();

            return product;
        }
    }
}