using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Service
{
    public class ProductRepository : ControllerBase, IProductRepository
    {
        private readonly IRepositoryCRUD _repository;
        private readonly IUnityOfWork _repositoryChanges;
        private readonly IMessagePublisher _serviceBus;

        public ProductRepository (
            IRepositoryCRUD repository,
            IUnityOfWork repositoryChanges,
            IMessagePublisher serviceBus)
        {
            _repository = repository;
            _repositoryChanges = repositoryChanges;
            _serviceBus = serviceBus;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            return Ok(await _repository.GetAllAsync());
        }

        public async Task<ActionResult<Product>> GetAsync(int? id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<ActionResult<Product>> AddAsync(Product product)
        {
            var response = await _repository.AddAsync(product);

            if (JsonConvert.SerializeObject(response.Result).Contains($"\"StatusCode\":{201}"))
            {
                await _serviceBus.SendMessage(product, "storage");
                await _repositoryChanges.SubmitChangesAsync();
            }
            return response;
        }

        public async Task<ActionResult<Product>> DeleteAsync(int? id)
        {
            var response = await _repository.DeleteAsync(id);

            if (response != null)
            {
                await _serviceBus.SendMessage(response, "delete");
                await _repositoryChanges.SubmitChangesAsync();

                return NoContent();
            }
            return NotFound();
        }

        public async Task<ActionResult<Product>> UpdateAsync(int id, Product product)
        {
            var response = await _repository.UpdateAsync(id, product);

            if (JsonConvert.SerializeObject(response.Result).Contains($"\"StatusCode\":{204}"))
            {
                await _serviceBus.SendMessage(product, "update");
                await _repositoryChanges.SubmitChangesAsync();
            }
            return response;
        }
    }
}