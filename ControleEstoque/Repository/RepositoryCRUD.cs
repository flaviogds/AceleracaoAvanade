using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleEstoque.Repository
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
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetAsync(int? id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
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
            Product product = await _context.Produtos.FirstOrDefaultAsync(e => e.Id == id);

            if (product == null) return null;

            _context.Produtos.Remove(product);

            return product;
        }
    }
}
