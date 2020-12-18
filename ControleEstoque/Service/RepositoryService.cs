using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Service
{
    public class RepositoryService : ControllerBase, IRepositoryService
    {
        private readonly IRepositoryCRUD _repository;
        private readonly IRepositoryChanges _repositoryChanges;
        private readonly IMessagePublisher _serviceBus;

        public RepositoryService (
            IRepositoryCRUD repository,
            IRepositoryChanges repositoryChanges,
            IMessagePublisher serviceBus)
        {
            _repository = repository;
            _repositoryChanges = repositoryChanges;
            _serviceBus = serviceBus;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync() => Ok(await _repository.GetAllAsync());
        
        public async Task<ActionResult<Product>> GetAsync(int? id) => await _repository.GetAsync(id);

        public async Task<ActionResult<Product>> AddAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _serviceBus.SendMessage(product, "storage");
            await _repositoryChanges.SubmitChangesAsync();

            return product;
        }

        public async Task<ActionResult<Product>> DeleteAsync(int? id)
        {
            var response = await _repository.DeleteAsync(id);
            await _serviceBus.SendMessage(response, "delete");
            await _repositoryChanges.SubmitChangesAsync();

            return null;
        }

        public async Task<ActionResult<Product>> UpdateAsync(int id, Product product)
        {
            await _repository.UpdateAsync(id, product);
            await _serviceBus.SendMessage(product, "update");
            await _repositoryChanges.SubmitChangesAsync();

            return product;
        }
    }
}