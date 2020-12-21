using ControleEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Repository
{
    public class RepositoryCRUD : ControllerBase, IRepositoryCRUD
    {
        private readonly RepositoryContext _context;

        public RepositoryCRUD(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetAsync(int? id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ActionResult<Product>> AddAsync(Product item )
        {
            if (await _context.Produtos.AnyAsync(e => 
                  e.Nome == item.Nome 
               || e.Quantidade == item.Quantidade)) return BadRequest("Objet allready exists!");

            var response = await _context.Produtos.AddAsync(item);

            return Created("", response.Entity);
        }

        public async Task<ActionResult<Product>> UpdateAsync( int id, Product item )
        {
            if(!await _context.Produtos.AnyAsync(e => e.Id == id)) return NotFound();

             _context.Entry(item).State = EntityState.Modified;

            return NoContent();
        }

        public async Task<Product> DeleteAsync( int? id )
        {
            if (!await _context.Produtos.AnyAsync(e => e.Id == id)) return null;

            var product = await _context.Produtos.FirstOrDefaultAsync(e => e.Id == id);

            _context.Produtos.Remove(product);

            return product;
        }
    }
}
