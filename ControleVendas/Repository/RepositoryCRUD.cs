using ControleVendas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleVendas.Repository
{
    public class RepositoryCRUD : IRepositoryCRUD
    {
        private readonly RepositoryContext _context;

        public RepositoryCRUD(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var response = await _context.Produtos
                .AsNoTracking()
                .ToListAsync();

            return response.FindAll(e => e.Quantidade > 0);
        }

        public async Task<Product> GetAsync(int? id)
        {
            return await _context.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.Quantidade > 0);
        }

        public async Task<Product> AddAsync( Product item )
        {
            if (await _context.Produtos.AnyAsync(e => 
                e.Id == item.Id 
               || e.Nome == item.Nome 
               || e.Quantidade == item.Quantidade)) return null;

            await _context.Produtos.AddAsync(item);

            return item;
        }

        public async Task<Product> UpdateAsync( int id, Product item )
        {
            if (!await _context.Produtos.AnyAsync(e => e.Id == id)) return null;

            _context.Entry(item).State = EntityState.Modified;

            return item;
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
